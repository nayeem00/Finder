using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finder
{
    public partial class Loginframe : Form
    {
        int count=0;
        MainForm mnf;
        LdatabaseDataContext ldtcx = new LdatabaseDataContext();
        public Loginframe(MainForm f)
        {
            InitializeComponent();
            mnf = f;
            this.Location = new Point(mnf.Location.X+40,mnf.Location.Y+130);
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent; 
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            password_field.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mnf.Enabled = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            ;
            if (ldtcx.LoginTables.Where(x => x.UserName == Username_field.Text && x.PassWord == password_field.Text).Count()>0)
            {
                mnf.Enabled = true;
            mnf.ifLogin();
            this.Close();
            }
            else
            {
                MessageBox.Show("Enter Valid Username or Password","Warnning",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
            }
            
        }

        private void password_field_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(count%2==0){
                password_field.PasswordChar = '\0';
            }
            else{
                password_field.PasswordChar = '*';
            }
            count++;
        }
    }
}
