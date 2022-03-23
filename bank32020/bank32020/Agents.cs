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

namespace bank32020
{
    public partial class Agents : Form
    {
        public Agents()
        {
            InitializeComponent();
            DisplayAgents();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\vivek\Documents\BankDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayAgents ()
        {
            Con.Open();
            string Query = "select * from AgentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AgentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            ANameTb.Text = "";
            PAsswordTb.Text = "";
            AAddressTb.Text = "";
            APhoneTb.Text = "";
        }
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || PAsswordTb.Text == "" || AAddressTb.Text == "" ||  APhoneTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AgentTbl(AName,APass,APhone,Aaddress)values(@AN,@APA,@APh,@AA)", Con);
                    cmd.Parameters.AddWithValue("@AN", ANameTb.Text);
                    cmd.Parameters.AddWithValue("@APA", PAsswordTb.Text);
                    cmd.Parameters.AddWithValue("@APh", APhoneTb.Text);
                    cmd.Parameters.AddWithValue("@AA", AAddressTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Added !!!");
                    Con.Close();
                    Reset();
                    DisplayAgents();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The account");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from AgentTbl where AId=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Deleted !!!");
                    Con.Close();
                    Reset();
                    DisplayAgents();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;    
        private void AgentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ANameTb.Text = AgentDGV.SelectedRows[0].Cells[1].Value.ToString();
            PAsswordTb.Text = AgentDGV.SelectedRows[0].Cells[2].Value.ToString();
            APhoneTb.Text = AgentDGV.SelectedRows[0].Cells[3].Value.ToString();
            AAddressTb.Text = AgentDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (ANameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(AgentDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || PAsswordTb.Text == "" || AAddressTb.Text == "" || APhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateAgentTbl set AName=@AN,APass=@AP,APhone=@APh,Aaddress=@AA where AId=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AA", ANameTb.Text);
                    cmd.Parameters.AddWithValue("@AP", PAsswordTb.Text);
                    cmd.Parameters.AddWithValue("@APh", APhoneTb.Text);
                    cmd.Parameters.AddWithValue("@AA", AAddressTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Updated !!!");
                    Con.Close();
                    Reset();
                    DisplayAgents();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
