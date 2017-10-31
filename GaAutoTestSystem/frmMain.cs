using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    public partial class frmMain : Form
    {
        private static readonly List<ParaInfo> _paras = new List<ParaInfo>();
        private static readonly List<string> _targetPaths = new List<string>();

        private static int _currentTargetPathIndex;

        //在此修改不同的被测函数对象
        private static AbstractFunction _function;

        //染色体长度
        private int _chromosomeLengthForOneSubValue = 10;

        //染色体数量
        private int _chromosomeQuantity = 1000;

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

        //演化策略——轮盘赌 / 精英 / 混合
        private Population.SelectType _selectType = Population.SelectType.Hybrid;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cmbStrategy.SelectedIndex = 2;
            //为参数类型下拉框添加选项
            cmbParaDataType.Items.Add("Double");
            cmbParaDataType.Items.Add("Integer");
            cmbParaDataType.SelectedIndex = 0;
        }

        private void btnGA_Click(object sender, EventArgs e)
        {
            //被测函数
            _function = new NextDate
            {
                FitnessCaculationType = AbstractFunction.FitnessType.NodeMatch,
                Paras = _paras
            };

            //加载参数
            LoadParameters();
            txtResult.Clear();
            _function.TargetPath = _targetPaths[0];

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
                    //                stopwatch.Stop();

                    builder.Clear();
                    builder.Append($"after {i + 1:000} envolve(s): timecost: {stopwatch.ElapsedMilliseconds} ms");
                    builder.Append($" | {OutputHelper.GetChromosomeInfo(mostFittest)}");
                    txtResult.AppendText(builder.ToString());
                    txtResult.ScrollToCaret();

                    //                stopwatch.Start();
                    //进化过程中不同的选择策略
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

            switch (cmbStrategy.SelectedIndex)
            {
                case 0:
                    _selectType = Population.SelectType.Roulette;
                    break;
                case 1:
                    _selectType = Population.SelectType.Elite;
                    break;
                case 2:
                    _selectType = Population.SelectType.Hybrid;
                    break;
            }
            //得到期望路径
            GetTargetPaths();
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
            var paraDataType = (ParaDataType) Enum.Parse(typeof(ParaDataType), cmbParaDataType.Text);

            _paras.Add(new ParaInfo {LowerBound = lowerBound, UpperBound = upperBound, DataType = paraDataType});

            if (txtParaList.Text.Trim() != "")
            {
                txtParaList.AppendText(Environment.NewLine);
            }

            txtParaList.AppendText($"参数 {_paras.Count}：[{lowerBound}, {upperBound}]({paraDataType})");
        }

        private void btnAddPara_Click(object sender, EventArgs e)
        {
            AddPara();
        }

        private void GetTargetPaths()
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
                    var evolutionStrategy =
                        xdoc.Descendants("EvolutionStrategy").Select(n => n.Value).FirstOrDefault();

                    if (evolutionStrategy == "Roulette")
                    {
                        cmbStrategy.SelectedIndex = 0;
                    }
                    else if (evolutionStrategy == "Elite")
                    {
                        cmbStrategy.SelectedIndex = 1;
                    }
                    else if (evolutionStrategy == "Hybrid")
                    {
                        cmbStrategy.SelectedIndex = 2;
                    }
                    //函数参数
                    var paras = xdoc.Descendants("Parameter").ToList();
                    txtParaList.Clear();
                    _paras.Clear();
                    foreach (var para in paras)
                    {
                        var paraDataType = (ParaDataType) Enum.Parse(typeof(ParaDataType), cmbParaDataType.Text);
                        var lowerBound = Convert.ToDouble(para.Element("LowerBound")?.Value);
                        var upperBound = Convert.ToDouble(para.Element("UpperBound")?.Value);

                        _paras.Add(new ParaInfo
                        {
                            LowerBound = lowerBound,
                            UpperBound = upperBound,
                            DataType = paraDataType
                        });

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
    }
}