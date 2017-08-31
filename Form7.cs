using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
//using System.Data;
using System.Drawing.Imaging;
using System.IO;

namespace LibraryManagementSystem
{
    public partial class Form7 : Form
    {
         string pwd = Class2.GetRandomPassword(20);
         string wanted_path;
         DialogResult result;
        SqlConnection con = new SqlConnection(@"server=LAPTOP-J067EJBG;database=Library;integrated security=true");
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'libraryDataSet.Student' table. You can move, or remove it, as needed.
            //   this.studentTableAdapter.Fill(this.libraryDataSet.Student);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Students";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            // Bitmap img;
            /* DataGridViewImageColumn imgcol = new DataGridViewImageColumn();
              imgcol.Width = 100;
              imgcol.HeaderText = "student_image";
             imgcol.ImageLayout = DataGridViewImageCellLayout.Zoom;
              dataGridView1.Columns.Add(imgcol);*/


              int i = 0;
              foreach(DataRow dr in dt.Rows)
              {
                  Bitmap img = new Bitmap(@"..\..\" + dr["student_image"].ToString());
                   dataGridView1.Rows[i].Cells[0].Value=img;

                   Bitmap img1 = new Bitmap(@"..\..\" + dr["student_barcode"].ToString());
                   dataGridView1.Rows[i].Cells[1].Value = img1;
                  
                   dataGridView1.Rows[i].Height = 150;
                 
                 i = i + 1;
              }

        }


        public void FillGrid()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Students";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            // Bitmap img;
            /* DataGridViewImageColumn imgcol = new DataGridViewImageColumn();
              imgcol.Width = 100;
              imgcol.HeaderText = "student_image";
             imgcol.ImageLayout = DataGridViewImageCellLayout.Zoom;
              dataGridView1.Columns.Add(imgcol);*/


            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Bitmap img = new Bitmap(@"..\..\" + dr["student_image"].ToString());
                dataGridView1.Rows[i].Cells[0].Value = img;

                Bitmap img1 = new Bitmap(@"..\..\" + dr["student_barcode"].ToString());
                dataGridView1.Rows[i].Cells[1].Value = img1;

                dataGridView1.Rows[i].Height = 150;

                i = i + 1;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText="Select * from Students where student_id="+i+"";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                textBox2.Text = dr["student_name"].ToString();
                textBox3.Text = dr["student_enrollment"].ToString();
                textBox4.Text = dr["student_department"].ToString();
                textBox5.Text = dr["student_semester"].ToString();
                textBox6.Text = dr["student_contact"].ToString();
                textBox7.Text = dr["student_email"].ToString();
            }

  
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Students where student_name like('%"+textBox1.Text+"%') ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            // Bitmap img;
            /* DataGridViewImageColumn imgcol = new DataGridViewImageColumn();
              imgcol.Width = 100;
              imgcol.HeaderText = "student_image";
             imgcol.ImageLayout = DataGridViewImageCellLayout.Zoom;
              dataGridView1.Columns.Add(imgcol);*/


            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Bitmap img = new Bitmap(@"..\..\" + dr["student_image"].ToString());
                dataGridView1.Rows[i].Cells[0].Value = img;

                Bitmap img1 = new Bitmap(@"..\..\" + dr["student_barcode"].ToString());
                dataGridView1.Rows[i].Cells[1].Value = img1;

                dataGridView1.Rows[i].Height = 150;

                i = i + 1;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
              if (result == DialogResult.OK)
              {
                  int i;
                  i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                  string img_path;
                  File.Copy(openFileDialog1.FileName, wanted_path + "\\student_images\\" + pwd + ".jpg");
                  img_path = "student_images\\" + pwd + ".jpg";

                  SqlCommand cmd = con.CreateCommand();
                  cmd.CommandType = CommandType.Text;
                  cmd.CommandText = "update Students set student_name='" + textBox2.Text + "',student_image='"+img_path.ToString()+"',student_enrollment= '" + textBox3.Text + "',student_department='" + textBox4.Text + "',student_semester='" + textBox5.Text + "',student_contact='" + textBox6.Text + "',student_email='" + textBox7.Text + "' where student_id='"+i+"'";
                  cmd.ExecuteNonQuery();
                  FillGrid();
                  MessageBox.Show("Information Updated Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                  textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = null;
              }
              else if(result==DialogResult.Cancel)
              {
                  int i;
                  i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                  SqlCommand cmd = con.CreateCommand();
                  cmd.CommandType = CommandType.Text;
                  cmd.CommandText = "update Students set student_name='" + textBox2.Text +"',student_enrollment= '" + textBox3.Text + "',student_department='" + textBox4.Text + "',student_semester='" + textBox5.Text + "',student_contact='" + textBox6.Text + "',student_email='" + textBox7.Text + "' where student_id='" + i + "'";
                  cmd.ExecuteNonQuery();
                  FillGrid();
                  MessageBox.Show("Information Updated Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                  textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = null;
              }
              else
              {
                  int i;
                  i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                  SqlCommand cmd = con.CreateCommand();
                  cmd.CommandType = CommandType.Text;
                  cmd.CommandText = "update Students set student_name='" + textBox2.Text + "',student_enrollment= '" + textBox3.Text + "',student_department='" + textBox4.Text + "',student_semester='" + textBox5.Text + "',student_contact='" + textBox6.Text + "',student_email='" + textBox7.Text + "' where student_id='" + i + "'";
                  cmd.ExecuteNonQuery();
                  FillGrid();
                  MessageBox.Show("Information Updated Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                  textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = null;
              }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           /* int i;
            i = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            //MessageBox.Show(i.ToString());
             SqlCommand cmd = con.CreateCommand();
             cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Students where student_contact="+i+"";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox2.Text = dr["student_name"].ToString();
                textBox3.Text = dr["student_enrollment"].ToString();
                textBox4.Text = dr["student_department"].ToString();
                textBox5.Text = dr["student_semester"].ToString();
                textBox6.Text = dr["student_contact"].ToString();
                textBox7.Text = dr["student_email"].ToString();
            }*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
             result = openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|* .gif";
           
               
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 p = new MDIParent1();
            p.Show();
        }


        }
    }
