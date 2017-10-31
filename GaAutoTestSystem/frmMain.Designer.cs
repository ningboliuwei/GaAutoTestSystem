namespace GaAutoTestSystem
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRandom = new System.Windows.Forms.Button();
            this.btnGA = new System.Windows.Forms.Button();
            this.txtGenerationQuantity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSelectionRate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMutationRate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRetainRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtChromosomeLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtChromosomeQuantity = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtParaValueLowerBound = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtParaValueUpperBound = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbStrategy = new System.Windows.Forms.ComboBox();
            this.btnAddPara = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtParaList = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbParaDataType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTargetPathList = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdSetting = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.Controls.Add(this.txtResult, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 678F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1125, 678);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(3, 4);
            this.txtResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(769, 670);
            this.txtResult.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnRandom, 0, 14);
            this.tableLayoutPanel2.Controls.Add(this.btnGA, 1, 14);
            this.tableLayoutPanel2.Controls.Add(this.txtGenerationQuantity, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.txtSelectionRate, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtMutationRate, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtRetainRate, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtChromosomeLength, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtChromosomeQuantity, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.txtParaValueLowerBound, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.txtParaValueUpperBound, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 13);
            this.tableLayoutPanel2.Controls.Add(this.cmbStrategy, 1, 13);
            this.tableLayoutPanel2.Controls.Add(this.btnAddPara, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.txtParaList, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.cmbParaDataType, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.label12, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this.txtTargetPathList, 0, 12);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(778, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 15;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(344, 670);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btnRandom
            // 
            this.btnRandom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRandom.Location = new System.Drawing.Point(3, 640);
            this.btnRandom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(134, 26);
            this.btnRandom.TabIndex = 18;
            this.btnRandom.Text = "随机生成(&R)";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // btnGA
            // 
            this.btnGA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGA.Location = new System.Drawing.Point(143, 640);
            this.btnGA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGA.Name = "btnGA";
            this.btnGA.Size = new System.Drawing.Size(198, 26);
            this.btnGA.TabIndex = 1;
            this.btnGA.Text = "演化生成(&G)";
            this.btnGA.UseVisualStyleBackColor = true;
            this.btnGA.Click += new System.EventHandler(this.btnGA_Click);
            // 
            // txtGenerationQuantity
            // 
            this.txtGenerationQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGenerationQuantity.Location = new System.Drawing.Point(143, 174);
            this.txtGenerationQuantity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGenerationQuantity.Name = "txtGenerationQuantity";
            this.txtGenerationQuantity.Size = new System.Drawing.Size(198, 23);
            this.txtGenerationQuantity.TabIndex = 11;
            this.txtGenerationQuantity.Text = "200";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 34);
            this.label6.TabIndex = 10;
            this.label6.Text = "进化代数";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSelectionRate
            // 
            this.txtSelectionRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSelectionRate.Location = new System.Drawing.Point(143, 140);
            this.txtSelectionRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSelectionRate.Name = "txtSelectionRate";
            this.txtSelectionRate.Size = new System.Drawing.Size(198, 23);
            this.txtSelectionRate.TabIndex = 9;
            this.txtSelectionRate.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 34);
            this.label5.TabIndex = 8;
            this.label5.Text = "随机选择率（%）";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMutationRate
            // 
            this.txtMutationRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMutationRate.Location = new System.Drawing.Point(143, 106);
            this.txtMutationRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMutationRate.Name = "txtMutationRate";
            this.txtMutationRate.Size = new System.Drawing.Size(198, 23);
            this.txtMutationRate.TabIndex = 7;
            this.txtMutationRate.Text = "30";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 34);
            this.label4.TabIndex = 6;
            this.label4.Text = "变异率（%）";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRetainRate
            // 
            this.txtRetainRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRetainRate.Location = new System.Drawing.Point(143, 72);
            this.txtRetainRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRetainRate.Name = "txtRetainRate";
            this.txtRetainRate.Size = new System.Drawing.Size(198, 23);
            this.txtRetainRate.TabIndex = 5;
            this.txtRetainRate.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 34);
            this.label3.TabIndex = 4;
            this.label3.Text = "存活率（%）";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtChromosomeLength
            // 
            this.txtChromosomeLength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChromosomeLength.Location = new System.Drawing.Point(143, 38);
            this.txtChromosomeLength.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtChromosomeLength.Name = "txtChromosomeLength";
            this.txtChromosomeLength.Size = new System.Drawing.Size(198, 23);
            this.txtChromosomeLength.TabIndex = 3;
            this.txtChromosomeLength.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 34);
            this.label2.TabIndex = 2;
            this.label2.Text = "每个子值染色体长度";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "染色体数量";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtChromosomeQuantity
            // 
            this.txtChromosomeQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChromosomeQuantity.Location = new System.Drawing.Point(143, 4);
            this.txtChromosomeQuantity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtChromosomeQuantity.Name = "txtChromosomeQuantity";
            this.txtChromosomeQuantity.Size = new System.Drawing.Size(198, 23);
            this.txtChromosomeQuantity.TabIndex = 1;
            this.txtChromosomeQuantity.Text = "1000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 204);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 34);
            this.label9.TabIndex = 14;
            this.label9.Text = "参数值下界";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtParaValueLowerBound
            // 
            this.txtParaValueLowerBound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtParaValueLowerBound.Location = new System.Drawing.Point(143, 208);
            this.txtParaValueLowerBound.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtParaValueLowerBound.Name = "txtParaValueLowerBound";
            this.txtParaValueLowerBound.Size = new System.Drawing.Size(198, 23);
            this.txtParaValueLowerBound.TabIndex = 15;
            this.txtParaValueLowerBound.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 238);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(134, 34);
            this.label10.TabIndex = 14;
            this.label10.Text = "参数值上界";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtParaValueUpperBound
            // 
            this.txtParaValueUpperBound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtParaValueUpperBound.Location = new System.Drawing.Point(143, 242);
            this.txtParaValueUpperBound.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtParaValueUpperBound.Name = "txtParaValueUpperBound";
            this.txtParaValueUpperBound.Size = new System.Drawing.Size(198, 23);
            this.txtParaValueUpperBound.TabIndex = 15;
            this.txtParaValueUpperBound.Text = "9";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 602);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 34);
            this.label7.TabIndex = 16;
            this.label7.Text = "进化策略";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbStrategy
            // 
            this.cmbStrategy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStrategy.FormattingEnabled = true;
            this.cmbStrategy.Items.AddRange(new object[] {
            "轮盘赌",
            "精英",
            "混合"});
            this.cmbStrategy.Location = new System.Drawing.Point(143, 606);
            this.cmbStrategy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbStrategy.Name = "cmbStrategy";
            this.cmbStrategy.Size = new System.Drawing.Size(198, 25);
            this.cmbStrategy.TabIndex = 17;
            // 
            // btnAddPara
            // 
            this.btnAddPara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddPara.Location = new System.Drawing.Point(143, 310);
            this.btnAddPara.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddPara.Name = "btnAddPara";
            this.btnAddPara.Size = new System.Drawing.Size(198, 26);
            this.btnAddPara.TabIndex = 20;
            this.btnAddPara.Text = "添加参数(&P)";
            this.btnAddPara.UseVisualStyleBackColor = true;
            this.btnAddPara.Click += new System.EventHandler(this.btnAddPara_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 306);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 34);
            this.label8.TabIndex = 14;
            this.label8.Text = "参数列表";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtParaList
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txtParaList, 2);
            this.txtParaList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtParaList.Location = new System.Drawing.Point(3, 344);
            this.txtParaList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtParaList.Multiline = true;
            this.txtParaList.Name = "txtParaList";
            this.txtParaList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtParaList.Size = new System.Drawing.Size(338, 106);
            this.txtParaList.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, 272);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 34);
            this.label11.TabIndex = 14;
            this.label11.Text = "参数数据类型";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbParaDataType
            // 
            this.cmbParaDataType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbParaDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParaDataType.FormattingEnabled = true;
            this.cmbParaDataType.Location = new System.Drawing.Point(143, 276);
            this.cmbParaDataType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbParaDataType.Name = "cmbParaDataType";
            this.cmbParaDataType.Size = new System.Drawing.Size(198, 25);
            this.cmbParaDataType.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(3, 454);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(134, 34);
            this.label12.TabIndex = 14;
            this.label12.Text = "目标路径列表";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTargetPathList
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txtTargetPathList, 2);
            this.txtTargetPathList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTargetPathList.Location = new System.Drawing.Point(3, 492);
            this.txtTargetPathList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTargetPathList.Multiline = true;
            this.txtTargetPathList.Name = "txtTargetPathList";
            this.txtTargetPathList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTargetPathList.Size = new System.Drawing.Size(338, 106);
            this.txtTargetPathList.TabIndex = 22;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1125, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSettingsToolStripMenuItem,
            this.loadSettingsToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.saveSettingsToolStripMenuItem.Text = "保存设置(&S)";
            // 
            // loadSettingsToolStripMenuItem
            // 
            this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
            this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.loadSettingsToolStripMenuItem.Text = "载入设置(&L)";
            this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
            // 
            // ofdSetting
            // 
            this.ofdSetting.Filter = "*.xml|*.xml";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 703);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnGA;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtMutationRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRetainRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtChromosomeLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChromosomeQuantity;
        private System.Windows.Forms.TextBox txtSelectionRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtGenerationQuantity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbStrategy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtParaValueLowerBound;
        private System.Windows.Forms.TextBox txtParaValueUpperBound;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAddPara;
        private System.Windows.Forms.TextBox txtParaList;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbParaDataType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTargetPathList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdSetting;
    }
}

