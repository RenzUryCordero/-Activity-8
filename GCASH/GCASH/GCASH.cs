using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace GCASH
{
    public partial class GCASH : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public GCASH()
        {
            InitializeComponent();
        }

        private void GCASH_Load(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }

        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (
                string.IsNullOrWhiteSpace(tbName.Text)
                || string.IsNullOrWhiteSpace(tbAmount.Text)
                || string.IsNullOrWhiteSpace(tbNumber.Text)
            )
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            var paymentData = new
            {
                name = tbName.Text,
                amount = tbAmount.Text,
                number = tbNumber.Text
            };

            string json = JsonConvert.SerializeObject(paymentData);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(
                    "http://localhost/renzApi/Payment.php",
                    content
                );
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Payment added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void TransactionButton_Click(object sender, EventArgs e)
        {
            // Create an instance of the TransactionForm
            Transaction transactionForm = new Transaction();

            // Hide the current form
            this.Hide();

            // Show the TransactionForm
            transactionForm.ShowDialog();

            // Once the TransactionForm is closed, you can close the current form if needed
            this.Close();
        }
    }
}
