using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyStore
{
    public partial class Login : Form
    {
        Code.Query_DB qd=new Code.Query_DB();
        public Login()
        {
            InitializeComponent();
        }

        // Make these fields public or use properties
        public TextBox EmailTextBox => Email; // Property for Email TextBox
        public TextBox PasswordTextBox => Password; // Property for Password TextBox
        public Label WrongLabel => Wrong; // Property for Wrong Label

        public void button1_Click(object sender, EventArgs e)
        {
            qd.EMAIL = Email.Text;
            qd.PASSWORD = Password.Text;
            if (qd.VerifyUser(qd))
            {
                qd = qd.GetUserDetails(qd);
                DeshBoard ad = new DeshBoard(qd);
                
                ad.Show();
                Email.Text = "";
                Password.Text = "";
                Wrong.Text = "";

            }
            else
                {
                    Wrong.Text = "** Wrong Username OR Password **";

                }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            Email.Text = "";
            Password.Text = "";
        }

        public void Login_Load(object sender, EventArgs e)
        {
            Email.Text = "";
            Password.Text = "";
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
