using Microsoft.Exchange.WebServices.Auth.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Windows.Forms;
using WinFormsClient.Models;

namespace WinFormsClient.UI
{
    public partial class frmLogin : Form
    {
        static HttpClient client;
        public frmLogin()
        {
            InitializeComponent();
            client = Helper.getHttpClient();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User();
                user.Email = emailTextBox.Text;
                user.Password = passwordTextBox.Text;

                var response = await client.PostAsJsonAsync("Accounts/Login", user);
                var readRes = await response.Content.ReadFromJsonAsync<User>();
                if (readRes?.Role != "Admin")
                {
                    MessageBox.Show("Login Unsuccessful", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoginInfo.Access_Token = readRes.Token;
                if (response.IsSuccessStatusCode == true)
                {
                    frmMenu frmMenu = new frmMenu();
                    frmMenu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
