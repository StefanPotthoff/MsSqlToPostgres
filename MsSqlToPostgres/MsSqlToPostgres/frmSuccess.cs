using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MsSqlToPostgres
{
	public partial class frmSuccess : Form
	{
		public frmSuccess()
		{
			InitializeComponent();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnDonate_Click(object sender, EventArgs e)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "https://www.paypal.com/donate?business=Stefan.Potthoff@gmx.net&currency_code=EUR",
				UseShellExecute = true
			});
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "https://github.com/StefanPotthoff/MsSqlToPostgres",
				UseShellExecute = true
			});
		}
	}
}
