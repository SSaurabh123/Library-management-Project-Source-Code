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
    public partial class Form9 : Form
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-J067EJBG;database=Library;integrated security=true");
        public Form9()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            fillgrid(textBox1.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form9_Load(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        public void fillgrid(string enrollment)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from issue_book where student_enrollment ='" + enrollment.ToString() + "' and book_return_date=' '";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel3.Visible = true;
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from issue_book where id ='" + i + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            foreach(DataRow dr in dt.Rows)
            {
                //label4.ForeColor = "Red";
                label7.Text = dr["book_name"].ToString();
                label8.Text = Convert.ToString(dr["book_issue_date"].ToString());
            }
        }
        Class1 c = new Class1();
        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            c.ins_del_up("update issue_book set book_return_date='" + dateTimePicker1.Value.ToString() + "' where id=" + i + "");

            c.ins_del_up("update Books set available_quantity=available_quantity+1 where book_name='" + label7.Text + "'");

            MessageBox.Show("Book Returned Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            panel3.Visible = true;

            fillgrid(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 p = new MDIParent1();
            p.Show();
        }

    }
}
