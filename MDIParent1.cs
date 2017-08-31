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
    public partial class MDIParent1 : Form
    {
       

        public MDIParent1()
        {
            InitializeComponent();
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {

        }

        private void bOOKToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aDDBOOKSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f = new Form3();
            f.Show();
        }

        private void vIEWBOOKSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f = new Form4();
            f.Show();
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f = new Form6();
            f.Show();
        }

        private void vIEWSTUDENTINFORMATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f = new Form7();
            f.Show();
        }

        private void iSSUEBOOKSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 f = new Form8();
            f.Show();
        }

        private void rETURNBOOKSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form9 f = new Form9();
            f.Show();
        }

        private void bOOKSSTOCKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 f = new Form10();
            f.Show();
        }

        private void rEMAINBOOKSREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form11 f = new Form11();
            f.Show();
        }

     }
}
