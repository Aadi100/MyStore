using MyStore.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyStore
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());

            try
            {
                SqlConnectionVerifier verifier = new SqlConnectionVerifier("MyDB");

                if (verifier.VerifyConnection())
                {
                    Console.WriteLine("Connection successful!");
                    Application.Run(new Login());
                }
                else
                {
                    Console.WriteLine("Connection failed. Exiting application.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                // Optionally, handle the error (e.g., show a message box)
                MessageBox.Show("Error: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
