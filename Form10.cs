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
using System.Net.Mail;
using System.Net;


namespace LibraryManagementSystem
{
    public partial class Form10 : Form
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-J067EJBG;database=Library;integrated security=true");
        public Form10()
        {
            InitializeComponent();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form10_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            Fill_books();
        }

        public void Fill_books()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select book_name,book_author_name,book_quantity,available_quantity from Books";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string i;
            i = (dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from issue_book where book_name='"+ i.ToString() +"' and book_return_date=' ' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select book_name,book_author_name,book_quantity,available_quantity from Books where book_name like('%"+textBox1.Text+"%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            groupBox1.Show();
            string i;
            i = (dataGridView2.SelectedCells[6].Value.ToString());
            textBox2.Text = i.ToString();
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var textBoxes = new TextBox[] { textBox2, textBox3, textBox4,
                                 };
                if (textBoxes.Any(tb => tb.Text == String.Empty))
                {
                MessageBox.Show("Fill All Entries", "Messsage!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else
            {
                SmtpClient stp = new SmtpClient("smtp.gmail.com", 587);
                stp.EnableSsl = true;
                stp.UseDefaultCredentials = false;
                stp.Credentials = new NetworkCredential("saurabhsharma0520@gmail.com", textBox4.Text);
                MailMessage mail = new MailMessage("saurabhsharma0520@gmail.com", textBox2.Text, "College Library notice", textBox3.Text);
                mail.Priority = MailPriority.High;
                stp.Send(mail);
                MessageBox.Show("Mail Sent Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox2.Text = textBox3.Text = textBox4.Text = null;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 p = new MDIParent1();
            p.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
