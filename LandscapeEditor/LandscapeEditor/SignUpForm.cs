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
        SignInForm parentSignInForm;

        public SignUpForm(SignInForm signInForm)
        {
            parentSignInForm = signInForm;
            InitializeComponent();
        }

        //private void ereiseEmpty()
        //{
        //    List<string> users = System.IO.File.ReadAllLines("users.txt").ToList<string>();
        //    foreach(string user in users)
        //    {
        //        if(user)
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)  //sign up confirm
        {
            string name = nameTextBox.Text;
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            System.Text.RegularExpressions.Regex emailRegex = new System.Text.RegularExpressions.Regex(@".+@.+\..+");
            //emailRegex.IsMatch(email);
            if (name != "" && email != "" && password != "" && emailRegex.IsMatch(email))          //all fields filled
            {
                bool flag = false;
                try
                {
                    string[] users = System.IO.File.ReadAllLines("users.txt");      //try search user in file
                    foreach (string userData in users)
                    {
                        if (userData != "")         //if user is not empty string
                        {
                            string[] user = userData.Split(' ');
                            if (user[0] == name || user[2] == email)
                                flag = true;                            //user already exists
                        }
                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                    flag = false;
                }


                if (!flag)              //if data is unique
                {
                    string line = name + " " + email + " " + password;
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("users.txt", true))
                    {
                        file.WriteLine(line);       //append data to file
                        parentSignInForm.autoSignIn(name, password, email);
                    }
                    this.Close();
                }
                else
                    MessageBox.Show("This user already exists!");
            }
            else
                MessageBox.Show("Check your data again");
        }
    }
}
