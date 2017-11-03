using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    public partial class frmMain : Form
    {
        //被测函数对象
        private static AbstractFunction _function;

        //遗传算法参数
        private readonly GaParameterInfo _gaParameters = new GaParameterInfo();

        //当前期望路径下标
        private int _currentTargetPathIndex;

        //被测函数参数集合
        private List<ParaInfo> _paras = new List<ParaInfo>();

        //期望路径集合
        private List<string> _targetPaths = new List<string>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //参数数据类型下拉框
            cmbParaDataType.DisplayMember = "Text";
            cmbParaDataType.ValueMember = "Value";
            cmbParaDataType.DataSource = new[]
            {
                new {Text = "整型", Value = "Integer"},
                new {Text = "浮点型", Value = "Double"}
            };
            cmbParaDataType.SelectedValue = "Double";
            //被测函数下拉框
            cmbFunction.DisplayMember = "Text";
            cmbFunction.ValueMember = "Value";
            cmbFunction.DataSource = new[]
            {
                new {Text = "Function1", Value = "Function1"},
                new {Text = "Function2", Value = "Function2"},
                new {Text = "Branch1", Value = "Branch1"},
                new {Text = "Branch2", Value = "Branch2"},
                new {Text = "TriangleType", Value = "TriangleType"},
                new {Text = "NextDate", Value = "NextDate"}
            };
            cmbFunction.SelectedValue = "NextDate";
            //适应度计算方法下拉框
            cmbFitnessCaculationType.DisplayMember = "Text";
            cmbFitnessCaculationType.ValueMember = "Value";
            cmbFitnessCaculationType.DataSource = new[]
            {
                new {Text = "基本", Value = "Basic"},
                new {Text = "面向距离", Value = "Distance"},
                new {Text = "路径匹配", Value = "PathMatch"},
                new {Text = "节点匹配", Value = "NodeMatch"}
            };
            cmbFitnessCaculationType.SelectedValue = "NodeMatch";
            //演化策略
            cmbStrategy.DisplayMember = "Text";
            cmbStrategy.ValueMember = "Value";
            cmbStrategy.DataSource = new[]
            {
                new {Text = "轮盘赌", Value = "Roulette"},
                new {Text = "精英", Value = "Elite"},
                new {Text = "混合", Value = "Hybrid"}
            };
            cmbStrategy.SelectedValue = "Hybrid";
        }

        private void btnGA_Click(object sender, EventArgs e)
        {
            //路径覆盖测试数据集
            var pathCoverageAssertions = new List<AssertionInfo>();
            var bounaryTestAssertions = new List<AssertionInfo>();
            //种群
            //加载参数
            LoadParameters();
            txtResult.Clear();
            //新建一个种群
            var population = new Population(_gaParameters, _function);
            var builder = new StringBuilder();
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            //随机生成染色体
            population.RandomGenerateChromosome();

            foreach (var targetPath in _targetPaths)
            {
                txtResult.AppendText(
                    $"========================{targetPath}========================{Environment.NewLine}");
                for (var i = 0; i < _gaParameters.GenerationQuantity; i++)
                {
                    //找出拥有每一代最高 Fitnetss 值的那个实际的解
                    var maxFitness = population.Chromosomes.Max(n => n.Fitness);
                    var mostFittest = population.Chromosomes.First(c => Equals(c.Fitness, maxFitness));

                    builder.Clear();
                    builder.Append($"after {i + 1:000} envolve(s): timecost: {stopwatch.ElapsedMilliseconds} ms");
                    builder.Append($" | {OutputHelper.GetChromosomeInfo(mostFittest)}");
                    txtResult.AppendText(builder.ToString());
                    txtResult.ScrollToCaret();

                    //进化过程中不同的选择策略进行演化
                    population.Evolve(_gaParameters.SelectionType);

                    //以下为终止条件
                    if (mostFittest.ExecutionPath.ToString().Contains(targetPath))
                    {
                        //将找到的数据添加到测试数据集中
                        var assertion = new AssertionInfo();
                        assertion.InputValues.AddRange(mostFittest.DecodedSubValues.Select(v => v).ToList());
                        pathCoverageAssertions.Add(assertion);

                        if (_currentTargetPathIndex < _targetPaths.Count - 1)
                        {
                            _currentTargetPathIndex++;
                            _function.TargetPath = _targetPaths[_currentTargetPathIndex];
                        }
                        txtResult.AppendText(
                            $"========================FOUND!========================{Environment.NewLine}");
                        break;
                    }
                }
            }
            //得到边界值测试用例集
            bounaryTestAssertions.AddRange(BoundaryTestHelper.GetBoundaryTestDataSet(_function));

            var testSuite = new TestSuiteInfo
            {
                Name = $"针对{cmbFunction.Text}函数的测试套件",
                Target = $"{GetFunction(cmbFunction.SelectedValue.ToString())}"
            };
            testSuite.TestCases.Add(new TestCaseInfo {Name = "路径覆盖测试", Assertions = pathCoverageAssertions});
            testSuite.TestCases.Add(new TestCaseInfo {Name = "边界值测试", Assertions = bounaryTestAssertions});

            GenerateTestSuiteFile(testSuite);
        }

        private void LoadParameters()
        {
            _gaParameters.ChromosomeQuantity = Convert.ToInt32(txtChromosomeQuantity.Text);
            _gaParameters.ChromosomeLengthForOneSubValue = Convert.ToInt32(txtChromosomeLength.Text);
            _gaParameters.RetainRate = Convert.ToDouble(txtRetainRate.Text) / 100;
            _gaParameters.MutationRate = Convert.ToDouble(txtMutationRate.Text) / 100;
            _gaParameters.SelectionRate = Convert.ToDouble(txtSelectionRate.Text) / 100;
            _gaParameters.GenerationQuantity = Convert.ToInt32(txtGenerationQuantity.Text);
            _gaParameters.SelectionType = (Population.SelectionType) Enum.Parse(typeof(Population.SelectionType),
                cmbStrategy.SelectedValue.ToString());
            //读取文本框中所有的期望路径
            _targetPaths = GetTargetPaths();
            //被测函数
            _function = GetFunction(cmbFunction.SelectedValue.ToString());
            //被测函数适应度计算方法
            _function.FitnessCaculationType = (AbstractFunction.FitnessType) Enum.Parse(
                typeof(AbstractFunction.FitnessType),
                cmbFitnessCaculationType.SelectedValue.ToString());
            //被测函数参数列表（_paras 中的项目由 AddFunctionPara() 或 LoadSettings() 添加）

            _paras = GetParas();

            _function.Paras = _paras;
            //设定被测函数用于匹配的路径为第一条路径
            _function.TargetPath = _targetPaths[0];
        }

        //从文本框获取参数信息
        private List<ParaInfo> GetParas()
        {
            var pattern = @"\[(\d+),(\d+)\]\((\w+)\)";
            var paras = new List<ParaInfo>();
            foreach (var line in txtParaList.Lines)
            {
                var match = Regex.Match(line, pattern);
                var lowerBound = Convert.ToDouble(match.Groups[1].Value);
                var upperBound = Convert.ToDouble(match.Groups[2].Value);
                var dataType = match.Groups[3].Value;

                paras.Add(new ParaInfo
                {
                    LowerBound = lowerBound,
                    UpperBound = upperBound,
                    DataType = (ParaDataType) Enum.Parse(typeof(ParaDataType), dataType)
                });
            }
            return paras;
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            LoadParameters();

            var rnd = new Random();
            var builder = new StringBuilder();

            txtResult.Clear();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (var targetPath in _targetPaths)
            {
                for (long i = 0; i < _gaParameters.GenerationQuantity; i++)
                {
                    foreach (var para in _paras)
                    {
                        para.Value = rnd.NextDouble() * (para.UpperBound - para.LowerBound) + para.LowerBound;
                    }

                    var fitness = _function.Fitness;
                    var result = _function.Result;
                    var executionPath = _function.ExecutionPath;

                    stopwatch.Stop();

                    builder.Clear();
                    builder.Append($"after {i + 1:0000} time(s): timecost: {stopwatch.ElapsedMilliseconds} ms");
                    builder.Append(
                        $" | value: {string.Join(" ", _paras.Select(p => p.Value).ToArray())} | fitness: {fitness} | execution path: {executionPath} | result: {result}");
                    builder.Append(Environment.NewLine);
                    txtResult.AppendText(builder.ToString());
                    txtResult.ScrollToCaret();

                    //以下为终止条件
                    //如果当前执行路径包含任何目标路径
                    if (executionPath.Contains(targetPath))
                    {
                        if (_currentTargetPathIndex < _targetPaths.Count - 1)
                        {
                            _currentTargetPathIndex++;
                            _function.TargetPath = _targetPaths[_currentTargetPathIndex];
                        }
                        txtResult.AppendText(
                            $"========================FOUND!========================{Environment.NewLine}");
                        break;
                    }

                    stopwatch.Start();
                }
            }
        }

        //通过 UI 添加被测函数参数
        private void AddFunctionPara()
        {
            var upperBound = Convert.ToDouble(txtParaValueUpperBound.Text.Trim());
            var lowerBound = Convert.ToDouble(txtParaValueLowerBound.Text.Trim());
            var paraDataType = cmbParaDataType.SelectedValue.ToString();
            var paraCount = txtParaList.Lines.Length + 1;

            if (txtParaList.Text.Trim() != "")
            {
                txtParaList.AppendText(Environment.NewLine);
            }

            txtParaList.AppendText($"参数 {paraCount}：[{lowerBound},{upperBound}]({paraDataType})");
        }

        private void btnAddPara_Click(object sender, EventArgs e)
        {
            AddFunctionPara();
        }

        //获取当前期望执行路径列表
        private List<string> GetTargetPaths()
        {
            var targetPaths = new List<string>();
            foreach (var line in txtTargetPathList.Lines)
            {
                targetPaths.Add(line);
            }
            return targetPaths;
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        //从 XML 文件载入设置并显示在 UI 上
        private void LoadSettings()
        {
            if (ofdSetting.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = ofdSetting.FileName;
                    var xdoc = XDocument.Load(filePath);
                    //染色体个数
                    txtChromosomeQuantity.Text =
                        xdoc.Descendants("ChromosomeQuantity").Select(n => n.Value).FirstOrDefault();
                    //染色体进化代数
                    txtGenerationQuantity.Text =
                        xdoc.Descendants("GenerationQuantity").Select(n => n.Value).FirstOrDefault();
                    //存活率
                    txtRetainRate.Text = (Convert.ToDouble(
                                              xdoc.Descendants("RetainRate").Select(n => n.Value)
                                                  .FirstOrDefault()) * 100).ToString();
                    //变异率
                    txtMutationRate.Text = (Convert.ToDouble(
                                                xdoc.Descendants("MutationRate").Select(n => n.Value)
                                                    .FirstOrDefault()) * 100).ToString();
                    //随机选择率
                    txtSelectionRate.Text =
                    (Convert.ToDouble(
                         xdoc.Descendants("SelectionRate").Select(n => n.Value)
                             .FirstOrDefault()) * 100).ToString();
                    //演化策略
                    cmbStrategy.SelectedValue =
                        xdoc.Descendants("EvolutionStrategy").Select(n => n.Value).FirstOrDefault();
                    //函数名
                    cmbFunction.Text = xdoc.Descendants("FunctionName").Select(n => n.Value).FirstOrDefault();
                    //函数参数
                    var paras = xdoc.Descendants("Parameter").ToList();
                    txtParaList.Clear();
                    foreach (var para in paras)
                    {
                        var paraDataType =
                            (ParaDataType) Enum.Parse(typeof(ParaDataType), para.Element("DataType")?.Value);
                        var lowerBound = Convert.ToDouble(para.Element("LowerBound")?.Value);
                        var upperBound = Convert.ToDouble(para.Element("UpperBound")?.Value);

                        txtParaList.AppendText(
                            $"参数 {paras.IndexOf(para) + 1}：[{lowerBound},{upperBound}]({paraDataType}){Environment.NewLine}");
                    }
                    //移除最后的空行
                    txtParaList.Text = txtParaList.Text.Remove(txtParaList.Text.Length - 2);
                    //期望路径
                    txtTargetPathList.Clear();
                    var targetPaths =
                        xdoc.Descendants("TargetPath").Select(n => n.Value).ToList();

                    foreach (var targetPath in targetPaths)
                    {
                        txtTargetPathList.AppendText($"{targetPath}{Environment.NewLine}");
                    }
                    //移除最后的空行
                    txtTargetPathList.Text = txtTargetPathList.Text.Remove(txtTargetPathList.Text.Length - 2);
                    //期望值计算方法
                    cmbFitnessCaculationType.SelectedValue = xdoc.Descendants("FitnessCaculationType")
                        .Select(n => n.Value).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        //将 UI 上的设置保存为 XML 文件
        private void SaveSettings()
        {
            if (sfdSetting.ShowDialog() == DialogResult.OK)
            {
                var filePath = sfdSetting.FileName;
                var paras = GetParas();
                var targetPaths = GetTargetPaths();
                try
                {
                    var xdoc = new XElement("TestSettings",
                        new XElement("GaSettings",
                            new XElement("ChromosomeSettings",
                                new XElement("ChromosomeQuantity", Convert.ToInt32(txtChromosomeQuantity.Text)),
                                new XElement("GenerationQuantity", Convert.ToInt32(txtGenerationQuantity.Text)),
                                new XElement("ChromosomeLengthForOneSubValue",
                                    Convert.ToInt32(txtChromosomeLength.Text)),
                                new XElement("RetainRate", Convert.ToDouble(txtRetainRate.Text) / 100),
                                new XElement("MutationRate", Convert.ToDouble(txtMutationRate.Text) / 100),
                                new XElement("SelectionRate", Convert.ToDouble(txtSelectionRate.Text) / 100),
                                new XElement("EvolutionStrategy", (Population.SelectionType) Enum.Parse(
                                    typeof(Population.SelectionType),
                                    cmbStrategy.SelectedValue.ToString())))),
                        new XElement("FunctionSettings",
                            new XElement("FunctionName", cmbFunction.SelectedValue.ToString()),
                            new XElement("FitnessCaculationType", (AbstractFunction.FitnessType) Enum.Parse(
                                typeof(AbstractFunction.FitnessType),
                                cmbFitnessCaculationType.SelectedValue.ToString())),
                            new XElement("Parameters", paras.Select(p => new XElement("Parameter",
                                new XElement("DataType", p.DataType),
                                new XElement("LowerBound", p.LowerBound),
                                new XElement("UpperBound", p.UpperBound)))),
                            new XElement("TargetPaths", targetPaths.Select(p => new XElement("TargetPath", p))
                            )));
                    xdoc.Save(filePath);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        //生成测试数据文档
        private void GenerateTestSuiteFile(TestSuiteInfo testSuite)
        {
            var outputDir = Application.StartupPath + "\\TestSuiteFiles";
            var fileName = $"{testSuite.Name}_{DateTime.Now:yyyyMMddHHmmssfff}.xml";
            var filePath = Path.Combine(outputDir, fileName);
            try
            {
                //若输出文件夹不存在，则创建该文件夹
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                var xdoc = new XElement("TestSuite", new XElement("Name", testSuite.Name),
                    new XElement("Target", $"{testSuite.Target}()"),
                    new XElement("TestCases", testSuite.TestCases.Select(c => new XElement("TestCase",
                        new XAttribute("ID", $"TC_{testSuite.TestCases.IndexOf(c) + 1:000}"),
                        new XElement("Name", c.Name),
                        new XElement("Assertions",
                            c.Assertions.Select(a => new XElement("Assertion",
                                new XAttribute("ID", $"TA_{c.Assertions.IndexOf(a) + 1:0000}"),
                                new XElement("InputValues",
                                    a.InputValues.Select(v => new XElement("InputValue", v))),
                                new XElement("ExpectedOutput", ""),
                                new XElement("FactOutput", ""),
                                new XElement("Result", ""))))))));
                xdoc.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private AbstractFunction GetFunction(string functionName)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            return ReflectionHelper.CreateInstance<AbstractFunction>($"GaAutoTestSystem.{functionName}", assemblyName);
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}