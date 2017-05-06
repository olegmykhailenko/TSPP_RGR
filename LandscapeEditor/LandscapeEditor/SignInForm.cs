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
    public partial class SignInForm : Form
    {
        private MainForm parent;

        public SignInForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)      //sign in confirm
        {
            try {
                string[] users = System.IO.File.ReadAllLines("users.txt");
                bool usersFound = false;
                bool userMatched = false;
                foreach (string userData in users)
                {
                    if (userData != "")
                    {
                        string[] user = userData.Split(' ');
                        if (emailTextBox.Text == user[1] && passwordTextBox.Text == user[2])
                        {                                    //if user data matches with file
                            parent.setUser(user[0]);
                            this.Close();
                            userMatched = true;             //correct user found
                        }
                        usersFound = true;                  //any user found
                    }
                }
                if (usersFound != true)
                    MessageBox.Show("No users found.");
                if (userMatched != true)
                    MessageBox.Show("Wrong email or password!");

            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("No data found");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            SignUpForm aForm = new SignUpForm(this);
            aForm.StartPosition = FormStartPosition.CenterParent;
            aForm.ShowDialog();
        }

        public void autoSignIn(string name, string password, string email)
        {
            emailTextBox.Text = email;
            passwordTextBox.Text = password;
        }

        public SignInForm(MainForm mainForm)
        {
            InitializeComponent();
            parent = mainForm;
        }
    }
}
