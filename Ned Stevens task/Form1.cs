using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Ned_Stevens_task
{
    public partial class frmContact : Form
    {
        DataSet dataSet = new DataSet();
        SqlConnection connectionString = new SqlConnection("Data Source=ALAN-FAMILY\\TESTLAB;Initial Catalog=testLab;Integrated Security=True");
        SqlDataAdapter dataAdapter = new SqlDataAdapter();


        public frmContact()
        {
            InitializeComponent();
        }

        void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            dataAdapter.InsertCommand = new SqlCommand("INSERT INTO tblContacts VALUES(@fullName, @fullAddress, @phoneNumber)", connectionString);
            dataAdapter.InsertCommand.Parameters.Add("@fullName", SqlDbType.VarChar).Value = txtfullName.Text;
            dataAdapter.InsertCommand.Parameters.Add("@fullAddress", SqlDbType.VarChar).Value = txtfullAddress.Text;
            dataAdapter.InsertCommand.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = txtphoneNumber.Text;

            connectionString.Open();
            dataAdapter.InsertCommand.ExecuteNonQuery();
            connectionString.Close();

            dataAdapter.SelectCommand = new SqlCommand("SELECT * FROM tblContacts ", connectionString);
            dataSet.Clear();
            dataAdapter.Fill(dataSet);
            dgContact.DataSource = dataSet.Tables[0];

            ClearAllText(this);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataAdapter.SelectCommand = new SqlCommand("SELECT * FROM tblContacts WHERE phoneNumber = @phoneNumber  ", connectionString);
            dataAdapter.SelectCommand.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = txtphoneNumber.Text;
            dataSet.Clear();
            dataAdapter.Fill(dataSet);
            dgContact.DataSource = dataSet.Tables[0];
            ClearAllText(this);

        }
    }
}
