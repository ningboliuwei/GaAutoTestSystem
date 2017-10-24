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
            var population = new Population(RetainRate, SelectionRate, MutationRate, ChromosomeLength,
                ChromosomeQuantity, SubValueQuantity, SolutionLowerBound, SolutionUpperBound, TestFunction.Function2);
            var builder = new StringBuilder();
            //加载参数
            LoadParameters();
            //随机生成染色体
            population.RandomGenerateChromosome();

          

            #region 针对分支函数随机生成测试数据

            //            var rnd = new Random();
            //
            //            for (int i = 0; i < GenerationQuantity * ChromosomeQuantity; i++)
            //            {
            //                int dice = rnd.Next(0, 10001);
            //
            //                builder.Clear();
            //                builder.Append(i + 1).Append(": ");
            //                builder.Append(dice);
            //                builder.Append(" ");
            //                builder.Append($"{TestFunction.BranchTest1(dice)} ");
            //
            //
            //                Console.WriteLine(builder);
            //
            //                if (TestFunction.BranchTest1(dice) == "#ab")
            //                {
            //                    break;
            //                }
            //
            //            }

            #endregion

            #region 针对日期函数随机生成测试数据

            //            var rnd = new Random();
            //
            //            for (int i = 0; i < 10000; i++)
            //            {
            //                int year = rnd.Next(1950, 2050);
            //                int month = rnd.Next(1, 12);
            //                int day = rnd.Next(1, 31);
            //
            //                builder.Clear();
            //                builder.Append(i + 1).Append(": ");
            //                builder.Append($"{year}/{month}/{day} ");
            //                builder.Append($"{TestFunction.NextDate(year, month, day)} ");
            //
            //                Console.WriteLine(builder);
            //
            //                if (DateTime.IsLeapYear(year) && month == 2 && day == 29)
            //                {
            //                    break;
            //                }
            //            }

            #endregion


            #region 进化 N 代

            txtResult.Clear();
            TimeSpan totalTimeCost = TimeSpan.Zero;
            for (var i = 0; i < GenerationQuantity; i++)
            {
                #region 显示本代中所有染色体及适应度

                //                foreach (var c in population.Chromosomes)
                //                {
                //                    builder.Clear();
                //                    builder.Append($"{OutputHelper.DisplayChromosomeBinaryValue(c)} ");
                //
                //                    //                            所有映射到解空间的值（若有级联）
                //                    //                    foreach (var value in d.SubValues)
                //                    //                    {
                //                    //                        builder.Append($"{d.GetDecodedValue(value)} ");
                //                    //                    }
                //
                //                    var x = c.GetDecodedValue(c.SubValues[0]);
                //                    builder.Append($"{x}");
                //                    builder.Append(" | fitness: ");
                //                    builder.Append(c.Fitness);
                //
                //                    //                    Console.WriteLine(builder);
                //                }

                #endregion

                //找出拥有每一代最高 Fitnetss 值的那个实际的解
                var maxFitness = population.Chromosomes.Max(n => n.Fitness);
                var mostFittest = population.Chromosomes.First(c => Equals(c.Fitness, maxFitness));
                
                builder.Clear();
                builder.Append($"after {i + 1:000} envolve(s): timecost: {totalTimeCost.TotalMilliseconds : 0.0000} ms");
                builder.Append($" | {OutputHelper.GetChromosomeInfo(mostFittest, TestFunction.Function2)}");
                txtResult.AppendText(builder.ToString());
                txtResult.ScrollToCaret();
                //进化过程中不同的选择策略
                var beginTime = DateTime.Now;
                population.Envolve(selectType);
                totalTimeCost += DateTime.Now - beginTime;

            }

            //            for (var i = 0; i < 100; i++)
            //            {
            //                var s = Console.ReadLine();
            //                var year = Convert.ToInt32(s.Split(' ')[0]);
            //                var month = Convert.ToInt32(s.Split(' ')[1]);
            //                var day = Convert.ToInt32(s.Split(' ')[2]);
            //
            //                Console.WriteLine(TestFunction.NextDate(year, month, day));
            //            }

            #endregion
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
            var rnd = new Random();
            var builder = new StringBuilder();
            TimeSpan timeCost = TimeSpan.Zero;
            var beginTime = DateTime.Now;

            LoadParameters();
            txtResult.Clear();

            for (long i = 0; i < ChromosomeQuantity * GenerationQuantity; i++)
            {
                var paras = new List<double>();
                
                for (var j = 0; j < SubValueQuantity; j++)
                {
                    var value = rnd.NextDouble() * (SolutionUpperBound - SolutionLowerBound) + SolutionLowerBound;
                    paras.Add(value);
                }
                timeCost = DateTime.Now - beginTime;
                
                var result = TestFunction.Function2(paras.ToArray());

                builder.Clear();
                builder.Append($"after {i + 1:0000} time(s): timecost: {timeCost.TotalMilliseconds: 0.0000} ms");
                builder.Append($" | value: {string.Join(" ", paras.ToArray())} | result: {result}");
                builder.Append(Environment.NewLine);
                txtResult.AppendText(builder.ToString());
                txtResult.ScrollToCaret();
            }
        }
    }
}