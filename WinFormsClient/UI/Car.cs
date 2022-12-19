using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WinFormsClient.Models;

namespace WinFormsClient
{
    public partial class frmCar : Form
    {
        static HttpClient client;

        //ID variable used in Updating and Deleting Record
        int ID = 0;
        public frmCar()
        {
            InitializeComponent();
            client = Helper.getHttpClientWithToken();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Add Car Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void submitButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                Car car = new Car();
                car = BindData(car);
                var response = await client.PostAsJsonAsync("Car/AddCar", car);
                if (response.IsSuccessStatusCode == true)
                {
                    MessageBox.Show("Car Addedd Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void typeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void vinTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void manualRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void autoRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mileageTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void priceTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void yearTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void modelTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void makeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Bind data for update form when click on cell of the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            ID = Convert.ToInt16(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            makeTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            modelTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            yearTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            priceTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            mileageTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            vinTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            typeTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }

        /// <summary>
        /// Update Car Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (makeTextBox.Text != "" && modelTextBox.Text != "")
                {
                    Car car = new Car();
                    car = BindData(car);
                    car.Id = ID;
                    var response = await client.PutAsJsonAsync("Car/UpdateCar", car);

                    if (response.IsSuccessStatusCode == true)
                    {
                        MessageBox.Show("Car Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    DisplayData();
                    ClearData();
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

        /// <summary>
        /// Display data
        /// </summary>
        private async void DisplayData()
        {
            var listCars = await client.GetAsync("Car/GetAllCar");
            var resultCars = await listCars.Content.ReadAsAsync<IList<Car>>();
            dataGridView1.DataSource = resultCars;

        }
        //Clear Data
        private void ClearData()
        {
            makeTextBox.Text = "";
            modelTextBox.Text = "";
            yearTextBox.Text = "";
            priceTextBox.Text = "";
            mileageTextBox.Text = "";
            manualRadioButton.Text = "";
            vinTextBox.Text = "";
            typeTextBox.Text = "";
            ID = 0;
        }
        private Car BindData(Car car)
        {
            //car.Id = ID;

            car.Make = makeTextBox.Text;
            car.Model = modelTextBox.Text;
            car.Price = Convert.ToDecimal(priceTextBox.Text);
            car.Vin = vinTextBox.Text;
            car.Mileage = Convert.ToInt16(mileageTextBox.Text);
            car.Transmission = autoRadioButton.Checked ? autoRadioButton.Text : manualRadioButton.Text;
            car.Type = typeTextBox.Text;
            car.Year = Convert.ToInt16(yearTextBox.Text);

            return car;
        }

        /// <summary>
        /// Bind data of grid on load Car page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCar_Load(object sender, EventArgs e)
        {
            this.DisplayData();
        }
    }
}