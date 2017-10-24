using System;
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

        private Population.SelectType selectType = Population.SelectType.Hybrid;

        private double SolutionLowerBound;

        private double SolutionUpperBound = 9;

        private int SubValueQuantity = 1;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cmbStrategy.SelectedIndex = 2;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var population = new Population(RetainRate, SelectionRate, MutationRate, ChromosomeLength,
                ChromosomeQuantity, SubValueQuantity, SolutionLowerBound, SolutionUpperBound, TestFunction.Function1);
            var builder = new StringBuilder();
            //加载参数
            LoadParameters();
            //指定测试函数的解空间上下界

            //随机生成染色体
            population.RandomGenerateChromosome();

            #region 随机生成三角形测试数据

            //            var rnd = new Random();
            //            
            //            for (int i = 0; i < 1000; i++)
            //            {
            //                var x = rnd.Next(1, 10);
            //                var y = rnd.Next(1, 10);
            //                var z = rnd.Next(1, 10);
            //            
            //                Console.WriteLine($"{x},{y},{z} {TestFunction.TriangleTypeTest(x,y,z)}");
            //            }

            #endregion

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

            //
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
                builder.Append($"after {i + 1:000} envolve(s): ");

                //所有映射到解空间的值（若有级联）
                var decodedSubValues = mostFittest.SubValues.Select(v =>
                    mostFittest.GetDecodedValue(v)).ToArray();
                var decodedSubValuesString = string.Join(" ", decodedSubValues);

                //染色体二进制表示 | 所有子值（即多输入参数拼接） | 适应度
                builder.Append(
                    $"{OutputHelper.DisplayChromosomeBinaryValue(mostFittest)} | {decodedSubValuesString} | fitness: {mostFittest.Fitness}");
                // builder.Append ($"fitness: {TestFunction.StubbedTriangleTypeTestPathCoverage(a, b, c)} ");
                builder.Append($" | result: {TestFunction.Function1(decodedSubValues)} ");
                // builder.Append ($"path: {TestFunction.TriangleTypeTestPathCoverage(a, b, c)}");
                builder.Append(Environment.NewLine);
                textBox1.AppendText(builder.ToString());
                textBox1.ScrollToCaret();
                //进化过程中不同的选择策略
                population.Envolve(selectType);
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

            SolutionLowerBound = Convert.ToInt32(txtSolutionLowerBound.Text);
            SolutionUpperBound = Convert.ToInt32(txtSolutionUpperBound.Text);
        }
    }
}