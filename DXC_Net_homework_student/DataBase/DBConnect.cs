using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace DXC_Net_homework_student
{
    internal class DBConnect
    {
       
        SqlConnection mconn=new SqlConnection();
        SqlCommand mcmd=new SqlCommand();
        SqlDataAdapter mda=new SqlDataAdapter();
        public DBConnect()
        {
            mconn.ConnectionString = "Server=localhost;Database=TestDB;Trusted_Connection=true";
            mcmd.Connection = mconn;
            mcmd.CommandText = "select * from student";
        }


        //更新数据库，包括增、删、改
        public int ExecuteUpdate(string sql)
        {
            mconn.Open();


            mcmd.CommandText = sql;
            int n=mcmd.ExecuteNonQuery();


            mconn.Close();
            return n;
        }

        //查询数据库，返回结果集
        public DataTable ExecuteQuery(string sql)
        {
            mconn.Open();

            mcmd.CommandText = sql;
            SqlDataReader reader = mcmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);//这里有异常就会出现问题(下面的close不执行)s

            reader.Close();
            mconn.Close();
            return dt;
        }


    }
}
