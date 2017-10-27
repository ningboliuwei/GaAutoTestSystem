using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    public partial class frmMain : Form
    {
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

        //解空间下界
        private double _solutionLowerBound;

        //解空间上界
        private double _solutionUpperBound = 9;

        //子值个数（被测函数参数个数）
        private int _subValueQuantity = 1;

        //演化策略——轮盘赌 / 精英 / 混合
        private Population.SelectType _selectType = Population.SelectType.Hybrid;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cmbStrategy.SelectedIndex = 2;
        }

        private void btnGA_Click(object sender, EventArgs e)
        {
            //加载参数
            LoadParameters();
            txtResult.Clear();
            
            _population = new Population
            {
                RetainRate = _retainRate,
                SelectionRate = _selectionRate,
                MutationRate = _mutationRate,
                ChromosomeLengthForOneSubValue = _chromosomeLengthForOneSubValue,
                SubValueQuantity = _subValueQuantity,
                ChromosomeLength = _chromosomeLengthForOneSubValue * _subValueQuantity,
                ChromosomeQuantity = _chromosomeQuantity,
                SolutionLowerBound = _solutionLowerBound,
                SolutionUpperBound = _solutionUpperBound,
                FitnessFunction = FitnessFunctionLib.Branch2_BasicPath_Coverage,
                ResultFunction = TestFunctionLib.Branch2
            };
            var builder = new StringBuilder();
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();
            //随机生成染色体
            _population.RandomGenerateChromosome();

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
                _population.Envolve(_selectType);

                //以下为终止条件
                if ( mostFittest.Result.ToString().Contains("-2-29"))
                    break;
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
            _subValueQuantity = Convert.ToInt32(txtSubValueQuantity.Text);

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

            _solutionLowerBound = Convert.ToDouble(txtSolutionLowerBound.Text);
            _solutionUpperBound = Convert.ToDouble(txtSolutionUpperBound.Text);
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            LoadParameters();

            var rnd = new Random();
            var builder = new StringBuilder();


            txtResult.Clear();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (long i = 0; i < _population.ChromosomeQuantity * _generationQuantity; i++)
            {
                var paras = new List<double>();

                for (var j = 0; j < _subValueQuantity; j++)
                {
                    var value = rnd.NextDouble() * (_solutionUpperBound - _solutionLowerBound) + _solutionLowerBound;
                    paras.Add(value);
                }

                var fitness = _population.FitnessFunction(paras.ToArray());
                var result = _population.ResultFunction(paras.ToArray());
                stopwatch.Stop();

                builder.Clear();
                builder.Append($"after {i + 1:0000} time(s): timecost: {stopwatch.ElapsedMilliseconds} ms");
                builder.Append(
                    $" | value: {string.Join(" ", paras.ToArray())} | fitness: {fitness} | result: {result}");
                builder.Append(Environment.NewLine);
                txtResult.AppendText(builder.ToString());
                txtResult.ScrollToCaret();

                //以下为随机生成终止条件
//                if (fitness == 0)
//                    break;

                stopwatch.Start();
            }
        }
    }
}