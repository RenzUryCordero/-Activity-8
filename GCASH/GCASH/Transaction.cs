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
    public partial class Transaction : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Transaction()
        {
            InitializeComponent();
            this.Load += LoadTransaction;
        }

        private async void LoadTransaction(object sender, EventArgs e)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(
                    "http://localhost/renzApi/Transaction.php"
                );
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Transaction2> transactions = JsonConvert.DeserializeObject<List<Transaction2>>(
                    jsonResponse
                );

                dataGridView1.DataSource = transactions;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Transaction_FormClosed(object sender, FormClosedEventArgs e)
        {
            GCASH gcash = new GCASH();

            // Hide the current form
            this.Hide();

            // Show the TransactionForm
            gcash.ShowDialog();

            // Once the TransactionForm is closed, you can close the current form if needed
            this.Close();
        }
    }

    public class Transaction2
    {
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Number { get; set; }
    }
}
