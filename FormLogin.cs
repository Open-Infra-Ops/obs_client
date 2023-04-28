using OBS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace obs_client 
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }     

        private void confirm_btn_Click(object sender, EventArgs e)
        {
            var configDict = ObsXml.ReadXml();
            if (this.username.Text.ToString() == configDict["Username"] && this.password.Text.ToString() == configDict["Password"]) {
                this.DialogResult = DialogResult.OK;
            }
            else {
                MessageBox.Show("登录失败，请重新登录。");
                this.username.Text = "请输入用户名";
                this.password.Text = "请输入密码";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.username.Text = "请输入用户名";
            this.password.Text = "请输入密码";
        }
    }
}
