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
//using System.Data;
namespace LibraryManagementSystem
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        DataTable dt;
        private void Form4_Load(object sender, EventArgs e)
        {
            Class1 c = new Class1();
            SqlDataReader dr = c.readrecord("select * from Books");
             dt = new DataTable();
            dt.Load(dr);
           dataGridView1.DataSource = dt;
           showall();
           groupBox2.Hide();
           groupBox3.Hide();
           groupBox4.Hide();
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        public void Click()
        {
             SqlDataReader dr = c.readrecord("select * from Books");
            dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
        }
        Class1 c =new Class1();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridViewRow drv = dataGridView1.Rows[e.RowIndex];
                c.ins_del_up("delete from Books where book_id=" + drv.Cells[2].Value.ToString());
            }
          Click();

            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[5].Value.ToString();
                textBox7.Text = row.Cells[7].Value.ToString();
                textBox8.Text = row.Cells[8].Value.ToString();
                textBox9.Text = row.Cells[2].Value.ToString();
                showall();
            }
            
        }
        public void showall()
        {
           // dataGridView1.Rows.Clear();
           DataSet ds = c.FillData("select * from Books");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["Se"].Value = "Select";
                dataGridView1.Rows[i].Cells["DEL"].Value = "Delete";
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            groupBox2.Show();
            else
            {
                groupBox2.Hide();
                groupBox3.Hide();
                groupBox4.Hide();
            }
            showall();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
                groupBox3.Show();
            else
            {
                groupBox2.Hide();
                groupBox3.Hide();
                groupBox4.Hide();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                groupBox4.Show();
            else
            {
                groupBox2.Hide();
                groupBox3.Hide();
                groupBox4.Hide();
            }
        } 
       private void textBox1_TextChanged(object sender, EventArgs e)
        {
           /* DataView dv = new DataView(dt);
            dv.RowFilter = string.Format("book_name LIKE '%{0}%'", textBox1.Text);
            dataGridView1.DataSource = dv;
            showall();*/
        }



       private void textBox2_TextChanged(object sender, EventArgs e)
       {
      /*     DataView dv = new DataView(dt);
           dv.RowFilter = string.Format("book_publication_name LIKE '%{0}%'", textBox2.Text);
           dataGridView1.DataSource = dv;
           showall();*/

       }

       private void textBox3_TextChanged(object sender, EventArgs e)
       {
         /*  DataView dv = new DataView(dt);
           dv.RowFilter = string.Format("book_author_name LIKE '%{0}%'", textBox3.Text);
           dataGridView1.DataSource = dv;
           showall();*/
       }

       private void panel1_Paint(object sender, PaintEventArgs e)
       {

       }

       private void button1_Click(object sender, EventArgs e)
       {
           var textBoxes = new TextBox[] { textBox4, textBox5, textBox6, textBox7,
                                 textBox8,textBox9,};
           if (textBoxes.Any(tb => tb.Text == String.Empty))
           {
               MessageBox.Show("Fill All Entries", "Message!", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
           else
           {
               c.ins_del_up("update Books set book_name='" + textBox4.Text + "',book_author_name='" + textBox5.Text + "',book_publication_name='" + textBox6.Text + "',book_purchase_date='" + dateTimePicker1.Value.Date + "',book_price=" + textBox7.Text + ",book_quantity=" + textBox8.Text + "where book_id= " + textBox9.Text + " ");
               MessageBox.Show("Book Updated Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
               Click();
               showall();
           }

       }

       private void button2_Click(object sender, EventArgs e)
       {
           this.Hide();
           MDIParent1 p = new MDIParent1();
           p.Show();
       }
       SqlConnection con = new SqlConnection(@"server=LAPTOP-J067EJBG;database=Library;integrated security=true");
       private void textBox2_KeyUp(object sender, KeyEventArgs e)
       {
           SqlCommand cmd = con.CreateCommand();
           cmd.CommandType = CommandType.Text;
           cmd.CommandText = "select * from Books where book_publication_name like('%" + textBox2.Text + "%')";
           cmd.ExecuteNonQuery();
           DataTable dt = new DataTable();
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           da.Fill(dt);
           dataGridView1.DataSource = dt;
           dataGridView1.Rows[0].Cells["Se"].Value = "Select";
           dataGridView1.Rows[0].Cells["DEL"].Value = "Delete";
       }

          private void textBox3_KeyUp(object sender, KeyEventArgs e)
       {
           SqlCommand cmd = con.CreateCommand();
           cmd.CommandType = CommandType.Text;
           cmd.CommandText = "select * from Books where book_author_name like('%" + textBox3.Text + "%')";
           cmd.ExecuteNonQuery();
           DataTable dt = new DataTable();
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           da.Fill(dt);
           dataGridView1.DataSource = dt;
       }

         private void textBox1_KeyUp(object sender, KeyEventArgs e)
          {
              SqlCommand cmd = con.CreateCommand();
              cmd.CommandType = CommandType.Text;
              cmd.CommandText = "select * from Books where book_name like('%" + textBox3.Text + "%')";
              cmd.ExecuteNonQuery();
              DataTable dt = new DataTable();
              SqlDataAdapter da = new SqlDataAdapter(cmd);
              da.Fill(dt);
              dataGridView1.DataSource = dt;
          }

    }
}
