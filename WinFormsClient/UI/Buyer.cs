using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsClient.Models;

namespace WinFormsClient.UI
{
    public partial class frmBuyer : Form
    {
        static HttpClient client;
        //ID variable used in Updating and Deleting Record
        int ID = 0;
        public frmBuyer()
        {
            InitializeComponent();
            client = Helper.getHttpClientWithToken();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Add Buyer information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                Buyer buyer = new Buyer(); 

                //Bind Data of buyer
                buyer = BindData();
                var response = await client.PostAsJsonAsync("Buyers/AddBuyer", buyer);
                if (response.IsSuccessStatusCode == true)
                {
                    MessageBox.Show("Buyer Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                DisplayData();
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonback_Click(object sender, EventArgs e)
        {
           
            //frmMenu.Show();
            ClearData();
            this.Hide();
        }

        private void flastNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cityTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private async void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        /// <summary>
        /// Display Grid data on page load of buyer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void frmBuyer_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ageTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Bind data on cell content click from data grid table to form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt16(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            firstNameTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            lastNameTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            ageTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            phoneTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cityTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            addressTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

        }

        /// <summary>
        /// Update Buyer information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (firstNameTextBox.Text != "" && lastNameTextBox.Text != "")
                {
                    Buyer buyer = new Buyer();
                    buyer = BindData();
                    var response = await client.PutAsJsonAsync("Buyers/UpdateBuyer", buyer);
                    if (response.IsSuccessStatusCode == true)
                    {
                        MessageBox.Show("Buyer Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    DisplayData();
                    ClearData(); //Clear Data
                }
                else
                {
                    MessageBox.Show("Please Select Record to Update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Display Data in DataGridView
        private async void DisplayData()
        {
            var response = await client.GetAsync("Car/GetAllCar");
            var result1 = response.Content.ReadAsAsync<IEnumerable<Car>>();
            ((ListBox)this.checkedListBox1).DataSource = result1.Result.ToList();
            ((ListBox)this.checkedListBox1).DisplayMember = "Make";

            var listBuyers = await client.GetAsync("Buyers/GetAllBuyer");
            var resultBuyers = await listBuyers.Content.ReadAsAsync<IList<Buyer>>();
            this.dataGridView1.DataSource = resultBuyers;

        }
        //Clear Data
        private void ClearData()
        {
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            ageTextBox.Text = "";
            phoneTextBox.Text = "";
            cityTextBox.Text = "";
            addressTextBox.Text = "";
            ID = 0;
            checkedListBox1.ClearSelected();

        }
       
        /// <summary>
        /// Bind data
        /// </summary>
        /// <returns></returns>
        private Buyer BindData()
        {

            Buyer buyer = new Buyer();
            buyer.Id = ID;
            buyer.FirstName = firstNameTextBox.Text;
            buyer.LastName = lastNameTextBox.Text;
            buyer.Age = Convert.ToInt16(ageTextBox.Text);
            buyer.Phone = phoneTextBox.Text;
            buyer.City = cityTextBox.Text;
            buyer.Address = addressTextBox.Text;
            List<int> list = new List<int>();
            foreach (Car item in (this.checkedListBox1).CheckedItems)
            {
                list.Add(item.Id);
            }
            buyer.CarIdList = list;
            return buyer;
        }
    }
}
