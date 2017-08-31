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
namespace LibraryManagementSystem
{
    public partial class Form2 : Form
    {
        string s=null;
        public Form2(string name)
        {
            InitializeComponent();
            s = name;
            us_text.Text = s;
        }
        private void Form2_Load(object sender,EventArgs e)
        {
            us_text.Text = s;

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class1 c = new Class1();
            if(textBox2.Text!="")
            {
            c.ins_del_up("update Login set password='" + textBox2.Text + "'where username='" + us_text.Text + "'");
            MessageBox.Show("Password Reset Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Incorrect Password", "Message!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = (checkBox1.Checked ? char.MinValue : '*');
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
