using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MyStore
{
    public class BussinessLogic
    {
        string con = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        SqlConnection cn;
        SqlCommand cmd;

        void openConnection()
        {
            cn = new SqlConnection(con);
            cn.Open();
        }

        void closeConnection()
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }

        public SqlDataReader SelectQuery(string query)
        {
            openConnection();
            cmd = new SqlCommand(query, cn);
            return cmd.ExecuteReader();
        }

        public int NonQuery(string query)
        {
            openConnection();
            cmd = new SqlCommand(query, cn);
            int temp = cmd.ExecuteNonQuery();
            closeConnection();
            return temp;
        }

        public void FillComboBox(ComboBox cb, string query)
        {
            openConnection();
            SqlDataReader rec = SelectQuery(query);
            while (rec.Read())
            {
                cb.Items.Add(rec[0].ToString());
            }
            closeConnection();
        }

        public void filldataGrid(DataGridView dg, string query)
        {
            openConnection();
            cmd = new SqlCommand(query, cn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            dg.DataSource = ds.Tables[0];
            closeConnection();
        }
    }
}
