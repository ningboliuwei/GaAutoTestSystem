using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using TestCodeGenerator;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    public partial class frmMain : Form
    {
        //被测函数对象
        private static AbstractFunction _function;

        //遗传算法参数
        private readonly GaParameterInfo _gaParameters = new GaParameterInfo();

        //默认保存设置的文件夹路径
        private readonly string _settingFilesDir = $"{Application.StartupPath}\\FunctionSettings";

        //默认保存测试套件的文件夹路径
        private readonly string _testSutieFilesDir = $"{Application.StartupPath}\\TestSuiteFiles";

        //默认保存单元测试代码的文件夹路径
        private readonly string _unitTestFilesDir = $"{Application.StartupPath}\\UnitTestFiles";

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
                new {Text = "值", Value = "Value"},
                new {Text = "代码覆盖率", Value = "CoverageRate"},
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
            var gaAssertions = new List<AssertionInfo>();
            //边界值测试数据集
            var bounaryTestAssertions = new List<AssertionInfo>();
            //加载参数
            LoadParameters();
            txtResult.Clear();
            //通过遗传算法得到测试数据
            var assertions = Task.Run(() => GaTestDataGenerator.GetAssertions(_gaParameters, _function, _targetPaths))
                .Result;
            gaAssertions.AddRange(assertions);

            //得到边界值测试数据
            //            bounaryTestAssertions.AddRange(BoundaryTestDataGenerator.GetAssertions(_function));

            //            var testSuite = new TestSuiteInfo
            //            {
            //                Name = $"针对{cmbFunction.Text}函数的测试套件",
            //                Target = $"{AbstractFunction.CreateInstance(cmbFunction.SelectedValue.ToString())}"
            //            };
            //            testSuite.TestCases.Add(new TestCaseInfo {Name = "路径覆盖测试", Assertions = gaAssertions});
            //            testSuite.TestCases.Add(new TestCaseInfo {Name = "边界值测试", Assertions = bounaryTestAssertions});
            //
            //            GenerateTestSuiteFile(testSuite);
            //            ShowTestData(gaAssertions.Union(bounaryTestAssertions).ToList());
            //            ShowAssertions(gaAssertions);

            //将 galog 显示到文本框中
            const string logPath = @"c:\#GA_DEMO\galog.txt";
            txtResult.AppendText(File.ReadAllText(logPath));
            //滚动到光标处
            txtResult.ScrollToCaret();
        }

        private TestSuiteInfo GetTestSuiteInfoFromTestSuiteFile(string filePath)
        {
            var testSuite = new TestSuiteInfo();
            XDocument xdoc;

            try
            {
                xdoc = XDocument.Load(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            testSuite.Name = xdoc.XPathSelectElement("/TestSuite/Name")?.Value;
            testSuite.Target = xdoc.XPathSelectElement("/TestSuite/Target")?.Value;

            foreach (var testcaseElement in xdoc.XPathSelectElements("/TestSuite/TestCases/TestCase"))
            {
                var testCase = new TestCaseInfo {Id = testcaseElement.Attribute("ID")?.Value};

                foreach (var assertionElement in testcaseElement.XPathSelectElements("Assertions/Assertion"))
                {
                    var assertion = new AssertionInfo
                    {
                        Id = assertionElement.Attribute("ID")?.Value,
                        InputValues = assertionElement.XPathSelectElements("InputValues/InputValue")
                            .Select(v => Convert.ToDouble(v.Value)).ToList(),
                        ExpectedOutput = assertionElement.XPathSelectElement("ExpectedOutput")?.Value,
                        ActualOutput = assertionElement.XPathSelectElement("ActualOutput")?.Value,
                        Result = assertionElement.XPathSelectElement("Result")?.Value
                    };
                    testCase.Assertions.Add(assertion);
                }

                testSuite.TestCases.Add(testCase);
            }

            return testSuite;
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
            _function = AbstractFunction.CreateInstance(cmbFunction.SelectedValue.ToString());
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
            var pattern = @"\[(-?\d+),(-?\d+)\]\((\w+)\)";
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
            txtResult.Clear();

            var randomAssertions = new List<AssertionInfo>();
            var result = Task.Run(() => RandomTestDataGenerator.GetAssertions(_gaParameters, _function, _targetPaths))
                .Result;
            randomAssertions.AddRange(result);

            //将 rndlog 显示到文本框中
            const string logPath = @"c:\#GA_DEMO\rndlog.txt";
            txtResult.AppendText(File.ReadAllText(logPath));
            //滚动到光标处
            txtResult.ScrollToCaret();
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
            if (ofdFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = ofdFile.FileName;
                    var xdoc = XDocument.Load(filePath);
                    //染色体个数
                    txtChromosomeQuantity.Text =
                        xdoc.Descendants("ChromosomeQuantity").Select(n => n.Value).FirstOrDefault();
                    //单个染色体编码长度
                    txtChromosomeLength.Text =
                        xdoc.Descendants("ChromosomeLengthForOneSubValue").Select(n => n.Value).FirstOrDefault();
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
            //保存设置前创建 FunctionSettings 文件夹
            try
            {
                if (!Directory.Exists(_settingFilesDir))
                {
                    Directory.CreateDirectory(_settingFilesDir);
                }

                sfdFile.InitialDirectory = _settingFilesDir;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (sfdFile.ShowDialog() == DialogResult.OK)
            {
                var filePath = sfdFile.FileName;
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
            var fileName = $"TS_{testSuite.FunctionName}_{DateTime.Now:yyyyMMddHHmmss}.xml";
            var filePath = Path.Combine(_testSutieFilesDir, fileName);

            try
            {
                //若输出文件夹不存在，则创建该文件夹
                if (!Directory.Exists(_testSutieFilesDir))
                {
                    Directory.CreateDirectory(_testSutieFilesDir);
                }

                var xdoc = new XElement("TestSuite", new XElement("Name", testSuite.Name),
                    new XElement("Target", $"{testSuite.Target}"),
                    new XElement("TestCases", testSuite.TestCases.Select(c => new XElement("TestCase",
                        new XAttribute("ID", $"TC_{testSuite.TestCases.IndexOf(c) + 1:000}"),
                        new XElement("Name", c.Name),
                        new XElement("Assertions",
                            c.Assertions.Select(a => new XElement("Assertion",
                                new XAttribute("ID", $"TA_{c.Assertions.IndexOf(a) + 1:0000}"),
                                new XElement("InputValues",
                                    a.InputValues.Select(v => new XElement("InputValue", v))),
                                new XElement("ExpectedOutput", a.ExpectedOutput),
                                new XElement("ActualOutput", a.ActualOutput),
                                new XElement("Result", a.Result))))))),
                    new XElement("TestSummery",
                        new XElement("Executed", testSuite.TestSummery.Executed),
                        new XElement("Passed", testSuite.TestSummery.Passed),
                        new XElement("Failed", testSuite.TestSummery.Failed)));
                xdoc.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //生成单元测试代码
        public void GenerateUnitTestFile(TestSuiteInfo testSuite)
        {
            var fileName = $"{testSuite.FunctionName}Test.cs";
            var filePath = Path.Combine(_unitTestFilesDir, fileName);

            try
            {
                //若单元测试代码输出文件夹不存在，则创建该文件夹
                if (!Directory.Exists(_unitTestFilesDir))
                {
                    Directory.CreateDirectory(_unitTestFilesDir);
                }

                UnitTestCodeGenerator.GenerateNUnitCode(testSuite, filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void ShowAssertions(List<AssertionInfo> assertions)
        {
            foreach (var assertion in assertions)
            {
                ShowAssertion(assertion);
            }
        }

        public void ShowAssertion(AssertionInfo assertion)
        {
//            var builder = new StringBuilder();
//            foreach (var para in _function.Paras)
//            {
//                para.Value = assertion.InputValues[_function.Paras.IndexOf(para)];
//            }
//
//            builder.Clear();
//            builder.Append(
//                $"value(s): {string.Join(" ", assertion.InputValues.Select(v => v).ToArray())} |  execution path: {_function.ExecutionPath} | result: {_function.Result}");
//            builder.Append(Environment.NewLine);
//            txtResult.AppendText(builder.ToString());
//            txtResult.ScrollToCaret();
        }

        private void btnExecuteTest_Click(object sender, EventArgs e)
        {
            ofdFile.InitialDirectory = _testSutieFilesDir;

            if (ofdFile.ShowDialog() == DialogResult.OK)
            {
                var testSuite = GetTestSuiteInfoFromTestSuiteFile(ofdFile.FileName);
                var testSummery = testSuite.TestSummery;

                var assertions = testSuite.TestCases.SelectMany(c => c.Assertions).ToList();
                _function = AbstractFunction.CreateInstance(testSuite.FunctionName);
                GenerateUnitTestFile(testSuite);

                foreach (var assertion in assertions)
                {
                    assertion.ActualOutput = _function.OriginalFunction(assertion.InputValues.ToArray());
                    assertion.Result = assertion.ActualOutput == assertion.ExpectedOutput ? "Passed" : "Failed";
                }

                testSummery.Executed = assertions.Count;
                testSummery.Passed = assertions.Count(a => a.Result == "Passed");
                testSummery.Failed = assertions.Count(a => a.Result == "Failed");

                GenerateTestSuiteFile(testSuite);
            }
        }
    }
}