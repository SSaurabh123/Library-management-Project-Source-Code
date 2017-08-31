using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class1 c = new Class1();
            var textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4,
                                 textBox6,};
            if (textBoxes.Any(tb => tb.Text == String.Empty))
            {
                MessageBox.Show("Fill All Entries", "Message!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c.ins_del_up("insert into Books(book_name,book_author_name,book_publication_name,book_purchase_date,book_price,book_quantity,available_quantity) values('" + textBox1.Text + "','" + textBox6.Text + "','" + textBox2.Text + "','" + dateTimePicker1.Value.ToString() + "'," + textBox4.Text + "," + textBox3.Text + "," + textBox3.Text + ")");
                MessageBox.Show("Book Saved Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox6.Text = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 p = new MDIParent1();
            p.Show();
        }
    }
 }
