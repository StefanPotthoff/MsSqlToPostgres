namespace MsSqlToPostgres
{
	partial class frmSuccess
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSuccess));
			label1 = new Label();
			btnOk = new Button();
			btnDonate = new Button();
			label3 = new Label();
			linkLabel1 = new LinkLabel();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			label1.Location = new Point(88, 19);
			label1.Name = "label1";
			label1.Size = new Size(314, 25);
			label1.TabIndex = 0;
			label1.Text = "Migration completed successfully!";
			// 
			// btnOk
			// 
			btnOk.Location = new Point(224, 64);
			btnOk.Margin = new Padding(3, 2, 3, 2);
			btnOk.Name = "btnOk";
			btnOk.Size = new Size(82, 22);
			btnOk.TabIndex = 1;
			btnOk.Text = "&Ok";
			btnOk.UseVisualStyleBackColor = true;
			btnOk.Click += btnOk_Click;
			// 
			// btnDonate
			// 
			btnDonate.Image = (Image)resources.GetObject("btnDonate.Image");
			btnDonate.Location = new Point(375, 96);
			btnDonate.Margin = new Padding(3, 2, 3, 2);
			btnDonate.Name = "btnDonate";
			btnDonate.Size = new Size(115, 34);
			btnDonate.TabIndex = 5;
			btnDonate.UseVisualStyleBackColor = true;
			btnDonate.Click += btnDonate_Click;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(74, 105);
			label3.Name = "label3";
			label3.Size = new Size(271, 15);
			label3.TabIndex = 4;
			label3.Text = "If this project help you, you can buy me a coffee :)";
			// 
			// linkLabel1
			// 
			linkLabel1.AutoSize = true;
			linkLabel1.Location = new Point(222, 134);
			linkLabel1.Name = "linkLabel1";
			linkLabel1.Size = new Size(84, 15);
			linkLabel1.TabIndex = 6;
			linkLabel1.TabStop = true;
			linkLabel1.Text = "Visit on github";
			linkLabel1.LinkClicked += linkLabel1_LinkClicked;
			// 
			// frmSuccess
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(564, 158);
			Controls.Add(linkLabel1);
			Controls.Add(btnDonate);
			Controls.Add(label3);
			Controls.Add(btnOk);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			Margin = new Padding(3, 2, 3, 2);
			Name = "frmSuccess";
			Text = "Migration successful";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Button btnOk;
		private Button btnDonate;
		private Label label3;
		private LinkLabel linkLabel1;
	}
}