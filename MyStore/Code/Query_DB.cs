using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace MyStore.Code
{
    public class Query_DB
    {
        BussinessLogic bl = new BussinessLogic();

        public int ID { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public int TYPE { get; set; }

        public bool VerifyUser(Query_DB qdb)
        {
            bool temp = false;
            string query = string.Format("SELECT COUNT(*) FROM Admin WHERE Email='{0}' AND Password='{1}'", qdb.EMAIL, qdb.PASSWORD);
            SqlDataReader rec = bl.SelectQuery(query);
            while (rec.Read())
            {
                if (rec[0].ToString().Equals("1"))
                {
                    temp = true;
                }
            }
            return temp;
        }

        public Query_DB GetUserDetails(Query_DB cdb)
        {
            string query = string.Format("SELECT ID, Email, Password, Type FROM Admin WHERE Email='{0}' AND Password='{1}'", cdb.EMAIL, cdb.PASSWORD);
            SqlDataReader rec = bl.SelectQuery(query);

            while (rec.Read())
            {
                if (!rec[0].ToString().Equals(" ") && Convert.ToInt32(rec[0].ToString()) >= 1)
                {
                    Query_DB utb = new Query_DB
                    {
                        ID = Convert.ToInt32(rec[0].ToString()),
                        EMAIL = rec[1].ToString(),
                        PASSWORD = rec[2].ToString(),
                        TYPE = Convert.ToInt32(rec[3].ToString())
                    };
                    return utb;
                }
            }
            return null;
        }

        public bool CheckUser(string email)
        {
            bool temp = false;
            string query = string.Format("SELECT COUNT(*) FROM UserData WHERE UEmail='{0}'", email);
            SqlDataReader rec = bl.SelectQuery(query);
            while (rec.Read())
            {
                if (Convert.ToInt32(rec[0].ToString()) >= 1)
                {
                    temp = true;
                }
            }
            return temp;
        }

        public int CheckUserExist(string name, string mobile)
        {
            string query = string.Format("SELECT ID FROM UserData WHERE UName='{0}' AND UMobile='{1}'", name, mobile);
            SqlDataReader rec = bl.SelectQuery(query);
            while (rec.Read())
            {
                return Convert.ToInt32(rec[0].ToString());
            }
            return 0;
        }

        public int GetMaxID(string tab)
        {
            string query = string.Format("SELECT MAX(ID) FROM {0}", tab);
            SqlDataReader rec = bl.SelectQuery(query);
            while (rec.Read())
            {
                if (!rec[0].ToString().Equals(""))
                {
                    return Convert.ToInt32(rec[0].ToString());
                }
            }
            return 0;
        }

        public bool CreateUser(string email, string name, string mobile, string address, string gender)
        {
            string query = string.Format("INSERT INTO UserData VALUES({0} + 1, '{1}', '{2}', '{3}', '{4}', '{5}')", GetMaxID("UserData"), email, name, mobile, address, gender);
            return bl.NonQuery(query) == 1;
        }

        public bool ChangePassword(Query_DB cdb)
        {
            string query = string.Format("UPDATE Admin SET Password='{0}' WHERE ID={1}", cdb.PASSWORD, cdb.ID);
            return bl.NonQuery(query) == 1;
        }

        public bool CreateSell(string date, int uid, int discount, int total, int aby)
        {
            string query = string.Format("INSERT INTO Sell VALUES({0} + 1, '{1}', {2}, {3}, {4}, {5})", GetMaxID("Sell"), date, uid, discount, total, aby);
            return bl.NonQuery(query) == 1;
        }

        public bool CreateSellProduct(int sid, int pid, int qty, int price)
        {
            string query = string.Format("INSERT INTO SellProduct VALUES({0} + 1, {1}, {2}, {3}, {4})", GetMaxID("SellProduct"), sid, pid, qty, price);
            return bl.NonQuery(query) == 1;
        }

        public bool AddProduct(string name, int price, int qty)
        {
            string query = string.Format("INSERT INTO Product VALUES({0} + 1, '{1}', {2}, {3})", GetMaxID("Product"), name, price, qty);
            return bl.NonQuery(query) == 1;
        }

        public void GetProduct(DataGridView dg)
        {
            string query = "SELECT PName, PPrice, PQuantity, ID AS pid FROM Product";
            bl.filldataGrid(dg, query);
        }

        public void GetProductBySearch(DataGridView dg, string se)
        {
            string query = "SELECT PName, PPrice, PQuantity, ID AS pid FROM Product WHERE PName LIKE('%" + se + "%')";
            bl.filldataGrid(dg, query);
        }

        public void GetSell(DataGridView dg)
        {
            string query = "SELECT UName, Date, Discount, Total, Sell.ID AS pid, Email AS ad FROM Sell, UserData, Admin WHERE Sell.Uid=UserData.ID AND Admin.ID=Sell.ABy";
            bl.filldataGrid(dg, query);
        }

        public void GetSaleBySearch(DataGridView dg, string se)
        {
            string query = "SELECT UName, Date, Discount, Total, Sell.ID AS pid, Email AS ad FROM Sell, UserData, Admin WHERE Sell.Uid=UserData.ID AND Admin.ID=Sell.ABy AND UName LIKE('%" + se + "%')";
            bl.filldataGrid(dg, query);
        }

        public void GetUserSell(DataGridView dg, int id)
        {
            string query = "SELECT PName, Quantity, Price, (Price * Quantity) AS total FROM SellProduct, Product WHERE SellProduct.Productid=Product.ID AND Sellid=" + id;
            bl.filldataGrid(dg, query);
        }

        public void GetUserSaleBySearch(DataGridView dg, int id, string se)
        {
            string query = "SELECT PName, Quantity, Price, (Price * Quantity) AS total FROM SellProduct, Product WHERE SellProduct.Productid=Product.ID AND Sellid=" + id + " AND PName LIKE('%" + se + "%')";
            bl.filldataGrid(dg, query);
        }

        public void GetUser(DataGridView dg)
        {
            string query = "SELECT UName, UMobile FROM UserData";
            bl.filldataGrid(dg, query);
        }

        public void GetUserBySearch(DataGridView dg, string ae)
        {
            string query = "SELECT UName, UMobile FROM UserData WHERE UName LIKE('%" + ae + "%')";
            bl.filldataGrid(dg, query);
        }

        public void GetSellProduct(DataGridView dg, int sid)
        {
            string query = "SELECT PName, Quantity, Price, (Quantity * Price) AS total FROM SellProduct, Product WHERE SellProduct.Sellid=" + sid + " AND SellProduct.Productid=Product.ID";
            bl.filldataGrid(dg, query);
        }

        public bool UpdateProduct(string name, string price, string qty, int id)
        {
            string query = string.Format("UPDATE Product SET PName='{0}', PPrice={1}, PQuantity={2} WHERE ID={3}", name, price, qty, id);
            return bl.NonQuery(query) == 1;
        }
    }
}
