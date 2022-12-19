using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsClient.UI
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open Car Entry Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            frmCar frmCar = new frmCar();
            frmCar.Show();
            this.Hide();
        }

        /// <summary>
        /// Open Buyer Entry Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            frmBuyer frmBuyer = new frmBuyer();
            frmBuyer.Show();
            this.Hide();
        }
    }
}
