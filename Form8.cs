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

namespace LibraryManagementSystem
{
    public partial class Form8 : Form
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-J067EJBG;database=Library;integrated security=true");
        
        public Form8()
        {
            InitializeComponent();
        }
        Class1 c = new Class1();
        private void button2_Click(object sender, EventArgs e)
        {
            int count1=0;
            SqlDataReader dr2 = c.readrecord("select * from issue_book where student_name='" + textBox2.Text + "'");
            while (dr2.Read())
            {
                count1 = count1 + 1;
            }
            if (count1 != 3)
            {

                int books_qty = 0;
                var textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4,
                                 textBox6,textBox7,};
                if (textBoxes.Any(tb => tb.Text == String.Empty))
                {
                    MessageBox.Show("Fill All Entries", "Message!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //  SqlDataReader dr2=c.readrecord("select * from issue_book where student_name='"+textBox2.Text+"'");

                else
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from Books where book_name ='" + textBox7.Text + "'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        books_qty = Convert.ToInt32(dr["available_quantity"].ToString());
                    }

                    if (books_qty > 0)
                    {

                        c.ins_del_up("insert into issue_book(student_enrollment,student_name,student_dept,student_sem,student_contact,student_email,book_name,book_issue_date,book_return_date) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + dateTimePicker1.Value.ToString() + "',' ')");
                        MessageBox.Show("Data Inserted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = null;

                        c.ins_del_up("update Books set available_quantity=available_quantity-1 where book_name='" + textBox7.Text + "'");
                        textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = null;
                    }
                    else
                    {

                        MessageBox.Show("Book not available Now", "Message!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = null;
                    }
                }
            }
            else
            {
                MessageBox.Show("Maximum Issue of Book Reached","Message!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = null;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }
        int i = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Students where student_enrollment='"+textBox1.Text+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if(i==0)
            {
                MessageBox.Show("No Information Found", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                foreach(DataRow dr in dt.Rows)
                {
                    textBox2.Text = dr["student_name"].ToString();
                    textBox3.Text = dr["student_department"].ToString();
                    textBox4.Text = dr["student_semester"].ToString();
                    textBox5.Text = dr["student_contact"].ToString();
                    textBox6.Text = dr["student_email"].ToString();
                }
            }
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            int count = 0;
            if(e.KeyCode !=Keys.Enter)
            {
                listBox1.Items.Clear();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Books where book_name like('%" + textBox7.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());

                if(count>0)
                {
                    listBox1.Visible = true;
                    foreach(DataRow  dr in dt.Rows)
                    {
                        listBox1.Items.Add(dr["book_name"].ToString());
                    }
                }
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                textBox7.Text = listBox1.SelectedItem.ToString();
                listBox1.Visible = false;
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox7.Text = listBox1.SelectedItem.ToString();
            listBox1.Visible = false;
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 p = new MDIParent1();
            p.Show();
        }
    }
}
