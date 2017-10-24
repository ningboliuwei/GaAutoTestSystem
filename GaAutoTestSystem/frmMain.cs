using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestDataGenerator;

namespace GaAutoTestSystem
{
    public partial class frmMain : Form
    {
        //染色体长度
        private int ChromosomeLength = 11;

        //染色体数量
        private int ChromosomeQuantity = 1000;

        //进化代数
        private int GenerationQuantity = 200;

        //变异率
        private double MutationRate = 0.3;

        //精确度
        private double Precision = 0.00000000000001;

        //存活率
        private double RetainRate = 0.2;

        //随机选择率
        private double SelectionRate = 0.01;

        //演化策略——轮盘赌 / 精英 / 混合
        private Population.SelectType selectType = Population.SelectType.Hybrid;

        //解空间下界
        private double SolutionLowerBound;

        //解空间上界
        private double SolutionUpperBound = 9;

        //子值个数（被测函数参数个数）
        private int SubValueQuantity = 1;

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
            LoadParameters();
            
            var population = new Population(RetainRate, SelectionRate, MutationRate, ChromosomeLength,
                ChromosomeQuantity, SubValueQuantity, SolutionLowerBound, SolutionUpperBound, StubbedFunction.StubbedBranchTest1_A);
            var builder = new StringBuilder();
            //加载参数
            
            //随机生成染色体
            population.RandomGenerateChromosome();

            txtResult.Clear();
            var totalTimeCost = TimeSpan.Zero;
            for (var i = 0; i < GenerationQuantity; i++)
            {
                //找出拥有每一代最高 Fitnetss 值的那个实际的解
                var maxFitness = population.Chromosomes.Max(n => n.Fitness);
                var mostFittest = population.Chromosomes.First(c => Equals(c.Fitness, maxFitness));

                builder.Clear();
                builder.Append($"after {i + 1:000} envolve(s): timecost: {totalTimeCost.TotalMilliseconds: 0.0000} ms");
                builder.Append($" | {OutputHelper.GetChromosomeInfo(mostFittest, TestFunction.BranchTest1)}");
                txtResult.AppendText(builder.ToString());
                txtResult.ScrollToCaret();
                //进化过程中不同的选择策略
                var beginTime = DateTime.Now;
                population.Envolve(selectType);
                totalTimeCost += DateTime.Now - beginTime;
            }
        }

        private void LoadParameters()
        {
            ChromosomeQuantity = Convert.ToInt32(txtChromosomeQuantity.Text);
            ChromosomeLength = Convert.ToInt32(txtChromosomeLength.Text);
            RetainRate = Convert.ToDouble(txtRetainRate.Text) / 100;
            MutationRate = Convert.ToDouble(txtMutationRate.Text) / 100;
            SelectionRate = Convert.ToDouble(txtSelectionRate.Text) / 100;
            GenerationQuantity = Convert.ToInt32(txtGenerationQuantity.Text);
            SubValueQuantity = Convert.ToInt32(txtSubValueQuantity.Text);

            switch (cmbStrategy.SelectedIndex)
            {
                case 0:
                    selectType = Population.SelectType.Roulette;
                    break;
                case 1:
                    selectType = Population.SelectType.Elite;
                    break;
                case 2:
                    selectType = Population.SelectType.Hybrid;
                    break;
            }

            SolutionLowerBound = Convert.ToDouble(txtSolutionLowerBound.Text);
            SolutionUpperBound = Convert.ToDouble(txtSolutionUpperBound.Text);
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            LoadParameters();
            
            var rnd = new Random();
            var builder = new StringBuilder();
            var beginTime = DateTime.Now;
           
            txtResult.Clear();

            for (long i = 0; i < ChromosomeQuantity * GenerationQuantity; i++)
            {
                var paras = new List<double>();

                for (var j = 0; j < SubValueQuantity; j++)
                {
                    var value = rnd.NextDouble() * (SolutionUpperBound - SolutionLowerBound) + SolutionLowerBound;
                    paras.Add(value);
                }
                
                var timeCost = DateTime.Now - beginTime;
                var result = TestFunction.BranchTest1(paras.ToArray());

                builder.Clear();
                builder.Append($"after {i + 1:0000} time(s): timecost: {timeCost.TotalMilliseconds: 0.0000} ms");
                builder.Append(
                    $" | value: {string.Join(" ", paras.ToArray())} | result: {result}");
                builder.Append(Environment.NewLine);
                txtResult.AppendText(builder.ToString());
                txtResult.ScrollToCaret();

                //以下为终止条件
                if (Math.Abs(result - 2) < 0.000001)
                {
                    break;
                }
            }
        }
    }
}