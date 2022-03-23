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
namespace bank32020
{
    public partial class AddAccount : Form
    {
        public AddAccount()
        {
            InitializeComponent();
            DisplayAccounts();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\vivek\Documents\BankDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayAccounts()
        {
            Con.Open();
            string Query = "select * from AccountTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query,Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AccountDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset ()
        {
            AcNameTb.Text = "";
            AcPhoneTb.Text = "";
            AcAddressTb.Text = "";
            Gencb.SelectedIndex = -1;
            OccupationTb.Text = "";
            IncomeTb.Text = "";
            Educationcb.SelectedIndex = -1;
        }
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
          if(AcNameTb.Text == "" || AcPhoneTb.Text == "" || AcAddressTb.Text == "" || Gencb.SelectedIndex == -1 || OccupationTb.Text == "" || Educationcb.SelectedIndex == -1 || IncomeTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AccountTbl(ACName,AcPhone,AcAddress,AcGen,AcOccup,AcEduc,AcInc,AcBal)values(@AN,@AP,@AA,@AG,@AO,@AE,@AI,@AB)", Con);
                    cmd.Parameters.AddWithValue("@AN",AcNameTb.Text);
                    cmd.Parameters.AddWithValue("@AP",AcPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@AA",AcAddressTb.Text);
                    cmd.Parameters.AddWithValue("@AG",Gencb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AO",OccupationTb.Text);
                    cmd.Parameters.AddWithValue("@AE",Educationcb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AI",IncomeTb.Text);
                    cmd.Parameters.AddWithValue("@AB",0);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Created !!!");
                    Con.Close();
                    Reset();
                    DisplayAccounts();

                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
                    
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (Key ==0 )
            {
                MessageBox.Show("Select The account");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from AccountTbl where AcNum=@AcKey", Con);
                    cmd.Parameters.AddWithValue("@AcKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Deleted !!!");
                    Con.Close();
                    Reset();
                    DisplayAccounts();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void AccountDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        
        {
            AcNameTb.Text = AccountDGV.SelectedRows[0].Cells[1].Value.ToString();
            AcPhoneTb.Text = AccountDGV.SelectedRows[0].Cells[2].Value.ToString();
            AcAddressTb.Text = AccountDGV.SelectedRows[0].Cells[3].Value.ToString();
            Gencb.SelectedItem = AccountDGV.SelectedRows[0].Cells[4].Value.ToString();
            OccupationTb.Text = AccountDGV.SelectedRows[0].Cells[5].Value.ToString();
            Educationcb.SelectedItem = AccountDGV.SelectedRows[6].Cells[1].Value.ToString();
            IncomeTb.Text= AccountDGV.SelectedRows[0].Cells[7].Value.ToString();
            if(AcNameTb.Text =="")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(AccountDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (AcNameTb.Text == "" || AcPhoneTb.Text == "" || AcAddressTb.Text == "" || Gencb.SelectedIndex == -1 || OccupationTb.Text == "" || Educationcb.SelectedIndex == -1 || IncomeTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateAccountTbl set ACName=@AN,AcPhone=@AP,AcAddress=@AA,AcGen=@AG,AcOccup=@AO,AcEduc=@AE,AcInc=@AI where ACNum=@AcKey", Con);
                    cmd.Parameters.AddWithValue("@AN", AcNameTb.Text);
                    cmd.Parameters.AddWithValue("@AP", AcPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@AA", AcAddressTb.Text);
                    cmd.Parameters.AddWithValue("@AG", Gencb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AO", OccupationTb.Text);
                    cmd.Parameters.AddWithValue("@AE", Educationcb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AI", IncomeTb.Text);
                    cmd.Parameters.AddWithValue("@AcKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Updated !!!");
                    Con.Close();
                    Reset();
                    DisplayAccounts();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void AddAccount_Load(object sender, EventArgs e)
        {

        }

        private void close_Icon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}


