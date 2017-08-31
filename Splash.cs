using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class Splash : Form
    {
        public Splash()
        {
           
            InitializeComponent();
        }
      /*  public void  startform()
        {
            Application.Run(new Form1());
        }*/


        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private void Splash_Load(object sender, EventArgs e)
        {
            timer.Interval = 5000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
            timer.Stop();
        }
      /*  private void Splash_Load(object sender, EventArgs e)
        {

        }*/

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
