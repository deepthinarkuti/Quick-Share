using System;
using System.Windows.Forms;
using WeShareProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WeShareProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TxtUserEnter(object sender, EventArgs e)
        {
            if (UserName.Text.Equals(@"User Name"))
            {
                UserName.Text = "";
            }
        }

        private void TxtUserLeave(object sender, EventArgs e)
        {
            if (UserName.Text.Equals(""))
            {
                UserName.Text = @"User Name";
            }
        }

        private void TxtPassEnter(object sender, EventArgs e)
        {
            if (Password.Text.Equals("Password"))
            {
                Password.Text = "";
            }
        }

        private void TxtPassLeave(object sender, EventArgs e)
        {
            if (Password.Text.Equals(""))
            {
                Password.Text = "Password";
            }
        }

        private async void Login_Click(object sender, EventArgs e)
        {
            var userFound = await DocumentOperations.LoginOperations(UserName.Text, Password.Text);
            if (userFound)
            {
                MessageBox.Show("Login Successful", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Incorrect User Name or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void UserName_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Register_Click(object sender, EventArgs e)
        {
            await DocumentOperations.RegisterOperations(UserName.Text, Password.Text);

            MessageBox.Show("Registration Successful", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
