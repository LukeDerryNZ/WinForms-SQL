using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_SQL2
{
    public partial class Form1 : Form
    {

        // Initialize the form.
        public Form1()
        {
            InitializeComponent();

            //dataGridView1.Dock = DockStyle.Fill;

            //FlowLayoutPanel panel = new FlowLayoutPanel();
            //panel.Dock = DockStyle.Top;
            //panel.AutoSize = true;
            //panel.Controls.AddRange(new Control[] { reloadButton, submitButton });

            //this.Controls.AddRange(new Control[] { dataGridView1, panel });
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            // TODO: This line of code loads data into the 'nORTHWNDDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.nORTHWNDDataSet.Customers);
            // Bind the DataGridView to the BindingSource
            // and load the data from the database.
            customersDataGridView.DataSource = nORTHWNDDataSetBindingSource;
            GetData("SELECT * FROM CUSTOMERS;");
        }

        private void resetButton_Click(object sender, System.EventArgs e)
        {
            // Reload the data from the database.
            //GetData(dataAdapter.SelectCommand.CommandText);
            GetData("SELECT * FROM CUSTOMERS;");
        }

        private void submitButton_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("submitButton_Click running");
            // Update the database with the user's changes.
            dataAdapter.Update((DataTable)nORTHWNDDataSetBindingSource.DataSource);
        }

        private void GetData(string selectCommand)
        {
            try
            {
                // Specify a connection string. Replace the given value with a 
                // valid connection string for a Northwind SQL Server sample
                // database accessible to your system.
                String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NORTHWND.MDF;Integrated Security=True;Connect Timeout=30";

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter(selectCommand, connectionString);

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand. These are used to
                // update the database.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                nORTHWNDDataSetBindingSource.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                customersDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }
        }

        private void Button_SubmitQuery_Click(object sender, EventArgs e)
        {
            GetData(textBox1.Text);
        }
    }
}
