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
    public partial class Form11 : Form
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-J067EJBG;database=Library;integrated security=true");
        public Form11()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet1 ds = new DataSet1();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from issue_book where book_return_date= ' '";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds.DataTable1);
            CrystalReport1 cp = new CrystalReport1();
            cp.SetDataSource(ds);
            CrystalReportViewer1 cpv = new CrystalReportViewer1();
            crystalReportViewer1.ReportSource = cp;
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 p = new MDIParent1();
            p.Show();
        }
    }
}
