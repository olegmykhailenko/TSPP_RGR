using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LandscapeEditor
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            if (name != null && email != null && password != null)
            {
                bool flag = false;
                try {
                        string[] users = System.IO.File.ReadAllLines("users.txt");
                        foreach (string userData in users)
                        {
                            string[] user = userData.Split(' ');
                            if (user[0] == name || user[1] == password)
                                flag = true;
                        }
                    }
                catch (System.IO.FileNotFoundException)
                {
                    flag = false;
                }




                string line = name + " " + email + " " + password;
                using(System.IO.StreamWriter file = new System.IO.StreamWriter("users.txt", true))
                {
                    file.WriteLine(line);
                }
                this.Close();
            }
            else
                MessageBox.Show("Enter data first!");
        }
    }
}
