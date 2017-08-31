using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace LibraryManagementSystem
{
    class Class1
    {
         string con_str="server=LAPTOP-J067EJBG;database=Library;integrated security=true;";
        public void ins_del_up(string str)
        {
            SqlConnection con = new SqlConnection(con_str);
            con.Open();
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public SqlDataReader readrecord(string str)
        {
            SqlConnection con = new SqlConnection(con_str);
            con.Open();
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public DataSet FillData(string str)
        {
            SqlDataAdapter dr=new SqlDataAdapter(str,con_str);
            DataSet ds=new DataSet();
            dr.Fill(ds);
            return ds;
        }
        public void ins_del_up(string str,SqlParameter[] para)
        {
            SqlConnection con = new SqlConnection("server=SAURABH\\SQLEXPRESS;database=login1;integrated security=true;");
            con.Open();
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.Parameters.AddRange(para);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
