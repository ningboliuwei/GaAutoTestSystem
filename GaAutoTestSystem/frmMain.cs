using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //在此修改不同的被测函数对象
        private static AbstractFunction _function;

        //被测函数参数集合
        private readonly List<ParaInfo> _paras = new List<ParaInfo>();

        //期望路径集合
        private readonly List<string> _targetPaths = new List<string>();

        //染色体长度
        private int _chromosomeLengthForOneSubValue = 10;

        //染色体数量
        private int _chromosomeQuantity = 1000;

        //当前期望路径下标
        private int _currentTargetPathIndex;

        //进化代数
        private int _generationQuantity = 200;

        //变异率
        private double _mutationRate = 0.3;

        //种群
        private Population _population;

        //存活率
        private double _retainRate = 0.2;

        //随机选择率
        private double _selectionRate = 0.01;

        //演化策略
        private Population.SelectType _selectType = Population.SelectType.Hybrid;

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
            //加载参数
            LoadParameters();
            txtResult.Clear();
            //新建一个种群
            _population = new Population
            {
                RelatedFunction = _function,
                RetainRate = _retainRate,
                SelectionRate = _selectionRate,
                MutationRate = _mutationRate,
                ChromosomeLengthForOneSubValue = _chromosomeLengthForOneSubValue,
                SubValueQuantity = _function.Paras.Count,
                ChromosomeLength = _chromosomeLengthForOneSubValue * _function.Paras.Count,
                ChromosomeQuantity = _chromosomeQuantity
            };

            var builder = new StringBuilder();
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            //随机生成染色体
            _population.RandomGenerateChromosome();

            foreach (var targetPath in _targetPaths)
            {
                txtResult.AppendText(
                    $"========================{targetPath}========================{Environment.NewLine}");
                for (var i = 0; i < _generationQuantity; i++)
                {
                    //找出拥有每一代最高 Fitnetss 值的那个实际的解
                    var maxFitness = _population.Chromosomes.Max(n => n.Fitness);
                    var mostFittest = _population.Chromosomes.First(c => Equals(c.Fitness, maxFitness));

                    builder.Clear();
                    builder.Append($"after {i + 1:000} envolve(s): timecost: {stopwatch.ElapsedMilliseconds} ms");
                    builder.Append($" | {OutputHelper.GetChromosomeInfo(mostFittest)}");
                    txtResult.AppendText(builder.ToString());
                    txtResult.ScrollToCaret();

                    //进化过程中不同的选择策略进行演化
                    _population.Evolve(_selectType);

                    //以下为终止条件
                    if (mostFittest.ExecutionPath.ToString().Contains(targetPath))
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
                }
            }
        }

        private void LoadParameters()
        {
            _chromosomeQuantity = Convert.ToInt32(txtChromosomeQuantity.Text);
            _chromosomeLengthForOneSubValue = Convert.ToInt32(txtChromosomeLength.Text);
            _retainRate = Convert.ToDouble(txtRetainRate.Text) / 100;
            _mutationRate = Convert.ToDouble(txtMutationRate.Text) / 100;
            _selectionRate = Convert.ToDouble(txtSelectionRate.Text) / 100;
            _generationQuantity = Convert.ToInt32(txtGenerationQuantity.Text);
            _selectType = (Population.SelectType) Enum.Parse(typeof(Population.SelectType),
                cmbStrategy.SelectedValue.ToString());
            //读取文本框中所有的期望路径
            BindTargetPaths();
            //被测函数
            _function = GetFunction(cmbFunction.SelectedValue.ToString());
            //被测函数适应度计算方法
            _function.FitnessCaculationType = (AbstractFunction.FitnessType) Enum.Parse(
                typeof(AbstractFunction.FitnessType),
                cmbFitnessCaculationType.SelectedValue.ToString());
            //被测函数参数列表（_paras 中的项目由 AddPara() 或 LoadSettings() 添加）

            BindParas();

            _function.Paras = _paras;
            //设定被测函数用于匹配的路径为第一条路径
            _function.TargetPath = _targetPaths[0];
        }
        //从文本框获取参数信息
        private void BindParas()
        {
            var pattern = @"\[(\d+),(\d+)\]\((\w+)\)";
            _paras.Clear();
            foreach (var line in txtParaList.Lines)
            {
                var match = Regex.Match(line, pattern);
                var lowerBound = Convert.ToDouble(match.Groups[1].Value);
                var upperBound = Convert.ToDouble(match.Groups[2].Value);
                var dataType = match.Groups[3].Value;

                _paras.Add(new ParaInfo
                {
                    LowerBound = lowerBound,
                    UpperBound = upperBound,
                    DataType = (ParaDataType) Enum.Parse(typeof(ParaDataType), dataType.ToString())
                });
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            LoadParameters();

            var rnd = new Random();
            var builder = new StringBuilder();

            txtResult.Clear();


            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (long i = 0; i < _population.ChromosomeQuantity; i++)
            {
                var paras = _population.RelatedFunction.Paras;
                foreach (var para in paras)
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
                    $" | value: {string.Join(" ", paras.Select(p => p.Value).ToArray())} | fitness: {fitness} | execution path: {executionPath} | result: {result}");
                builder.Append(Environment.NewLine);
                txtResult.AppendText(builder.ToString());
                txtResult.ScrollToCaret();

                //以下为随机生成终止条件
//                if (fitness == 0)
//                    break;

                stopwatch.Start();
            }
        }

        private void AddPara()
        {
            var upperBound = Convert.ToDouble(txtParaValueUpperBound.Text.Trim());
            var lowerBound = Convert.ToDouble(txtParaValueLowerBound.Text.Trim());
            var paraDataType =cmbParaDataType.SelectedValue.ToString();
            var paraCount = txtParaList.Lines.Length + 1;

            if (txtParaList.Text.Trim() != "")
            {
                txtParaList.AppendText(Environment.NewLine);
            }
            
            txtParaList.AppendText($"参数 {paraCount}：[{lowerBound},{upperBound}]({paraDataType})");
        }

        private void btnAddPara_Click(object sender, EventArgs e)
        {
            AddPara();
        }

        private void BindTargetPaths()
        {
            _targetPaths.Clear();
            foreach (var line in txtTargetPathList.Lines)
            {
                _targetPaths.Add(line);
            }
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        //从 XML 文件载入设置
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
                        var paraDataType = (ParaDataType) Enum.Parse(typeof(ParaDataType),
                            cmbParaDataType.SelectedValue.ToString());
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private AbstractFunction GetFunction(string functionName)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            return ReflectionHelper.CreateInstance<AbstractFunction>($"GaAutoTestSystem.{functionName}", assemblyName);
        }
    }
}