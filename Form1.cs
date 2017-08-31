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
using System.Data;
using System.Threading;
namespace LibraryManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            //t.Abort();
            loadcaptcha();
          //  Txt_nameValidating(sender,e);
        }
     
        int number = 0;
        
        private void loadcaptcha()
        {
            Random r = new Random();
            number = r.Next(1500, 5000);
            var image = new Bitmap(this.pictureBox2.Width,this.pictureBox2.Height);
            var font = new Font("TimesNewRoman",25,FontStyle.Bold,GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(image);
            graphics.DrawString(number.ToString(), font, Brushes.BlanchedAlmond, new Point(0, 0));
            pictureBox2.Image = image;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            Class1 c = new Class1();
            if (textBox3.Text == number.ToString())
            {
                c.ins_del_up("insert into Login values('" + textBox1.Text + "','" + textBox2.Text + "')");
                MessageBox.Show("Successfully Registered", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Incorrect Submission","MessageBox",MessageBoxButtons.OK,MessageBoxIcon.Error);
                loadcaptcha();
               // textBox1.Text = textBox2.Text = textBox3.Text = null;
            }
            textBox1.Text = textBox2.Text = textBox3.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class1 c = new Class1();
            SqlDataReader dr = c.readrecord("select * from Login where username='"+textBox1.Text+"'and password='"+textBox2.Text+"'");
            int count=0;
            while(dr.Read())
            {
                count = count + 1;
            }
            if(count==1 && textBox3.Text==number.ToString())
            {
                MessageBox.Show("Successfully Logged In ", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Hide();
                MDIParent1 p = new MDIParent1();
                p.Show();
            }
            else
            {
                MessageBox.Show("Check Your Username and Password or Captcha", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
                loadcaptcha();
            }
            textBox1.Text = textBox2.Text =textBox3.Text =null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Class1 c = new Class1();
            if (textBox3.Text == number.ToString())
            {
                c.ins_del_up("delete from Login where username='" + textBox1.Text + "'and password='" + textBox2.Text + "'");
                MessageBox.Show("Admin Removed Successfully", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Incorrect Deletion" ,"MessageBox",MessageBoxButtons.OK,MessageBoxIcon.Error);
                loadcaptcha();
                textBox1.Text = textBox2.Text = textBox3.Text = null;
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox1, "Required");
            }
            else
            {
                errorProvider1.SetError(textBox1, "");
            }

        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                e.Cancel = true;
                errorProvider2.SetError(textBox2, "Required");
            }
            else
            {
                errorProvider2.SetError(textBox2, "");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            loadcaptcha();
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                e.Cancel = true;
                errorProvider3.SetError(textBox3, "Required");
            }
            else
            {
                errorProvider3.SetError(textBox3, "");
            }
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Enter your Username First!", "Message!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
                //MessageBox.Show("Enter your Username First!", "Message!",MessageBoxButtons.OK, MessageBoxIcon.Error);
                Form1 f = new Form1();
                f.Show();
            }
            else
            { 
            this.Hide();
            Form2 f = new Form2(textBox1.Text);
            f.Show();
        }
        }
       
    }
}
