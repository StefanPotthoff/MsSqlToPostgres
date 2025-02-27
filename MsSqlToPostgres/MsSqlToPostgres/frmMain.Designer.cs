namespace MsSqlToPostgres
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			groupBox1 = new GroupBox();
			btnTestMsqSql = new Button();
			txtMsqlConnectionString = new TextBox();
			label1 = new Label();
			groupBox2 = new GroupBox();
			btnTestPostgres = new Button();
			txtPostgresConnectionString = new TextBox();
			label2 = new Label();
			label3 = new Label();
			btnDonate = new Button();
			btnTransfer = new Button();
			pbTotal = new ProgressBar();
			lblTotal = new Label();
			lnkGithub = new LinkLabel();
			lblCurrentStep = new Label();
			pbCurrentStep = new ProgressBar();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(btnTestMsqSql);
			groupBox1.Controls.Add(txtMsqlConnectionString);
			groupBox1.Controls.Add(label1);
			groupBox1.Location = new Point(2, 49);
			groupBox1.Margin = new Padding(3, 2, 3, 2);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new Padding(3, 2, 3, 2);
			groupBox1.Size = new Size(466, 183);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "MSSQL";
			// 
			// btnTestMsqSql
			// 
			btnTestMsqSql.Location = new Point(5, 157);
			btnTestMsqSql.Margin = new Padding(3, 2, 3, 2);
			btnTestMsqSql.Name = "btnTestMsqSql";
			btnTestMsqSql.Size = new Size(452, 22);
			btnTestMsqSql.TabIndex = 2;
			btnTestMsqSql.Text = "Test Connection";
			btnTestMsqSql.UseVisualStyleBackColor = true;
			btnTestMsqSql.Click += btnTestMsqSql_Click;
			// 
			// txtMsqlConnectionString
			// 
			txtMsqlConnectionString.Location = new Point(9, 41);
			txtMsqlConnectionString.Margin = new Padding(3, 2, 3, 2);
			txtMsqlConnectionString.Multiline = true;
			txtMsqlConnectionString.Name = "txtMsqlConnectionString";
			txtMsqlConnectionString.Size = new Size(452, 112);
			txtMsqlConnectionString.TabIndex = 1;
			txtMsqlConnectionString.Text = "Server=localhost\\SQLExpress;Connection Timeout=30;Command Timeout=30;Persist Security Info=False;TrustServerCertificate=True;Integrated Security=True;Initial Catalog=YourDb";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(9, 17);
			label1.Name = "label1";
			label1.Size = new Size(100, 15);
			label1.TabIndex = 0;
			label1.Text = "ConnectionString";
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(btnTestPostgres);
			groupBox2.Controls.Add(txtPostgresConnectionString);
			groupBox2.Controls.Add(label2);
			groupBox2.Location = new Point(472, 49);
			groupBox2.Margin = new Padding(3, 2, 3, 2);
			groupBox2.Name = "groupBox2";
			groupBox2.Padding = new Padding(3, 2, 3, 2);
			groupBox2.Size = new Size(466, 183);
			groupBox2.TabIndex = 1;
			groupBox2.TabStop = false;
			groupBox2.Text = "Postgres";
			// 
			// btnTestPostgres
			// 
			btnTestPostgres.Location = new Point(0, 157);
			btnTestPostgres.Margin = new Padding(3, 2, 3, 2);
			btnTestPostgres.Name = "btnTestPostgres";
			btnTestPostgres.Size = new Size(452, 22);
			btnTestPostgres.TabIndex = 3;
			btnTestPostgres.Text = "Test Connection";
			btnTestPostgres.UseVisualStyleBackColor = true;
			btnTestPostgres.Click += btnTestPostgres_Click;
			// 
			// txtPostgresConnectionString
			// 
			txtPostgresConnectionString.Location = new Point(0, 41);
			txtPostgresConnectionString.Margin = new Padding(3, 2, 3, 2);
			txtPostgresConnectionString.Multiline = true;
			txtPostgresConnectionString.Name = "txtPostgresConnectionString";
			txtPostgresConnectionString.Size = new Size(452, 112);
			txtPostgresConnectionString.TabIndex = 2;
			txtPostgresConnectionString.Text = "Host=localhost:5432;Database=YourDb;Username=postgres;Password=YourPassword;Timeout=500";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(5, 17);
			label2.Name = "label2";
			label2.Size = new Size(100, 15);
			label2.TabIndex = 1;
			label2.Text = "ConnectionString";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(10, 14);
			label3.Name = "label3";
			label3.Size = new Size(271, 15);
			label3.TabIndex = 2;
			label3.Text = "If this project help you, you can buy me a coffee :)";
			// 
			// btnDonate
			// 
			btnDonate.Image = (Image)resources.GetObject("btnDonate.Image");
			btnDonate.Location = new Point(312, 4);
			btnDonate.Margin = new Padding(3, 2, 3, 2);
			btnDonate.Name = "btnDonate";
			btnDonate.Size = new Size(115, 34);
			btnDonate.TabIndex = 3;
			btnDonate.UseVisualStyleBackColor = true;
			btnDonate.Click += btnDonate_Click;
			// 
			// btnTransfer
			// 
			btnTransfer.Location = new Point(2, 244);
			btnTransfer.Margin = new Padding(3, 2, 3, 2);
			btnTransfer.Name = "btnTransfer";
			btnTransfer.Size = new Size(936, 22);
			btnTransfer.TabIndex = 3;
			btnTransfer.Text = "Transfer";
			btnTransfer.UseVisualStyleBackColor = true;
			btnTransfer.Click += btnTransfer_Click;
			// 
			// pbTotal
			// 
			pbTotal.Location = new Point(7, 290);
			pbTotal.Margin = new Padding(3, 2, 3, 2);
			pbTotal.Name = "pbTotal";
			pbTotal.Size = new Size(931, 22);
			pbTotal.TabIndex = 4;
			pbTotal.Visible = false;
			// 
			// lblTotal
			// 
			lblTotal.AutoSize = true;
			lblTotal.Location = new Point(4, 273);
			lblTotal.Name = "lblTotal";
			lblTotal.Size = new Size(46, 15);
			lblTotal.TabIndex = 5;
			lblTotal.Text = "lblTotal";
			lblTotal.Visible = false;
			// 
			// lnkGithub
			// 
			lnkGithub.AutoSize = true;
			lnkGithub.Location = new Point(846, 14);
			lnkGithub.Name = "lnkGithub";
			lnkGithub.Size = new Size(84, 15);
			lnkGithub.TabIndex = 8;
			lnkGithub.TabStop = true;
			lnkGithub.Text = "Visit on github";
			// 
			// lblCurrentStep
			// 
			lblCurrentStep.AutoSize = true;
			lblCurrentStep.Location = new Point(4, 325);
			lblCurrentStep.Name = "lblCurrentStep";
			lblCurrentStep.Size = new Size(83, 15);
			lblCurrentStep.TabIndex = 10;
			lblCurrentStep.Text = "lblCurrentStep";
			lblCurrentStep.Visible = false;
			// 
			// pbCurrentStep
			// 
			pbCurrentStep.Location = new Point(7, 342);
			pbCurrentStep.Margin = new Padding(3, 2, 3, 2);
			pbCurrentStep.Name = "pbCurrentStep";
			pbCurrentStep.Size = new Size(931, 22);
			pbCurrentStep.TabIndex = 9;
			pbCurrentStep.Visible = false;
			// 
			// frmMain
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(951, 374);
			Controls.Add(lblCurrentStep);
			Controls.Add(pbCurrentStep);
			Controls.Add(lnkGithub);
			Controls.Add(lblTotal);
			Controls.Add(pbTotal);
			Controls.Add(btnTransfer);
			Controls.Add(btnDonate);
			Controls.Add(label3);
			Controls.Add(groupBox2);
			Controls.Add(groupBox1);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			Margin = new Padding(3, 2, 3, 2);
			Name = "frmMain";
			Text = "MS SQL to Postgres";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private GroupBox groupBox1;
		private TextBox txtMsqlConnectionString;
		private Label label1;
		private GroupBox groupBox2;
		private Label label2;
		private Label label3;
		private Button btnDonate;
		private Button btnTestMsqSql;
		private Button btnTestPostgres;
		private TextBox txtPostgresConnectionString;
		private Button btnTransfer;
		private ProgressBar pbTotal;
		private Label lblTotal;
		private LinkLabel lnkGithub;
		private Label lblCurrentStep;
		private ProgressBar pbCurrentStep;
	}
}
