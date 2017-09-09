using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Human_Rights
{
    public partial class Register : Form
    {
        DataClasses1DataContext human_rights = new DataClasses1DataContext();
        public Register()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (new_name.Text != "" && new_pass.Text != "" && conf_pass.Text != "")
            {
                if (new_pass.Text == conf_pass.Text)
                {
                    if (!ifexist_item(new_name.Text))
                    {
                        try
                        {
                            var maxid = human_rights.Tbl_User_Datas.Max(s => s.ID);
                            human_rights.register(maxid + 1, new_name.Text, new_pass.Text, 0);
                        }
                        catch (Exception) { human_rights.register(1, new_name.Text, new_pass.Text, 0); }
                        Class1.TheID = human_rights.Tbl_User_Datas.Max(s => s.ID);
                        Class1.TheValue = new_name.Text;
                        
                        this.Hide();
                        //Home h = new Home();

                        //h.ShowDialog();
                        MessageBox.Show("تم تسجيل مستخدم جديد ، أخرج من البرنامج للتأكد من التسجيل");
                       
                    }
                    else label1.Text = "الاسم موجود بالفعل";
                }
                else label1.Text = "الرقم السرى غير متطابق";
            }
            else label1.Text = "رجاء تاكد من ادخال البيانات كامله";
        }

        private void Register_Load(object sender, EventArgs e)
        {
                new_name.Focus();
            
        }

        private bool ifexist_item(string x)
        {
            try
            {
                var allnames = human_rights.Tbl_User_Datas.Select(s => s.UserName).ToList();
                foreach (var item in allnames)
                {
                    if (x==item)
                    {

                        return true;
                    }
                }
               

                return false;
            }

            catch (Exception)
            {
                return false;
            }

        }
        private void new_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                new_pass.Focus();
            }
        }

        private void new_pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                conf_pass.Focus();
            }
        }

        private void conf_pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.Focus();
            }
        }

        

    }
}
