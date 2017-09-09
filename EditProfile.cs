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
    public partial class EditProfile : Form
    {
        DataClasses1DataContext human_rights = new DataClasses1DataContext();
        public EditProfile()
        {
            InitializeComponent();
        }
        
        public void setpanal1(bool x)
        {
            txt_address.ReadOnly=x;
            txt_member_descrip.ReadOnly = x;
            txt_allname.ReadOnly = x;
            txt_username.ReadOnly = x;
            txt_tel.ReadOnly = x;
            txt_national_iD.ReadOnly = x;    
        }
    
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {this.Close();}

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            setpanal1(false);
            button2.Enabled = false;
            button1.Enabled = true;
            label14.Visible = true;
            comboBox1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            if (txt_allname.Text !=""&&txt_password.Text!=""&&txt_confpassword.Text !=""&&txt_username.Text!="")
            {
                if (txt_password.Text == txt_confpassword.Text&&comboBox1.SelectedIndex==0)
                {
                    human_rights.updateProfile(Class1.TheID, txt_username.Text, txt_password.Text,
                        txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text,0);
                    button1.Text = "تم التعديل";
                    DialogResult dialogResult = MessageBox.Show("تم التعديل......هل تريد التعديل مره أخرى ؟", "Some Title", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        setpanal1(false);
                        txt_password.Text = txt_confpassword.Text = "";
                        panel2.Visible = true;
                        button2.Enabled = false;
                        button1.Enabled = true;
                        button1.Text = "حفظ";

                    }
                    if (dialogResult == DialogResult.No)
                    {
                        this.Hide();
                        Form1 f1 = new Form1();
                        f1.ShowDialog();
                    }
                }
                else if (txt_password.Text == txt_confpassword.Text && comboBox1.SelectedIndex == 1)
                {
                    human_rights.updateProfile(Class1.TheID, txt_username.Text, txt_password.Text,
                        txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text,1);
                    button1.Text = "تم التعديل";
                    DialogResult dialogResult = MessageBox.Show("تم التعديل......هل تريد التعديل مره أخرى ؟", "Some Title", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        setpanal1(false);
                        txt_password.Text = txt_confpassword.Text = "";
                        panel2.Visible = true;
                        button2.Enabled = false;
                        button1.Enabled = true;
                        button1.Text = "حفظ";

                    }
                    if (dialogResult == DialogResult.No)
                    {
                        this.Hide();
                        Form1 f1 = new Form1();
                        f1.ShowDialog();
                    }
                } 
                else label13.Text = "الباسورد غير متتطابق";
            }
            else label13.Text = "تأكد من ادخال الاسم الاول واسم المستخدم والباسورد ";
        }

        private void EditProfile_Load(object sender, EventArgs e)
        {
            button5.Enabled = false;
            setpanal1(true);
            panel2.Visible = false;
            button2.Enabled = true;
            button1.Enabled = false;
            label9.Text = Class1.TheID.ToString();
            var details = human_rights.Tbl_User_Datas.Where(s => s.ID == Class1.TheID).Select(s =>new
                {
                    username = s.UserName,
                    password = s.Password,
                    allname = s.all_name,
                    mob = s.mobile,
                    member_description = s.membership_descrip,
                    address=s.address,
                    national_iD = s.National_iD,
                    state = s.State,

                }).SingleOrDefault();
            txt_address.Text = details.address;
            txt_member_descrip.Text = details.member_description;
            txt_allname.Text = details.allname;
            txt_tel.Text = details.mob;
            txt_username.Text = details.username;
            txt_national_iD.Text = details.national_iD;
            if (details.state == 0)
            {
                label9.Text = "Admin";
            }
            else label9.Text = "Member";
        }

        private void txt_firstname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_allname.Focus();
            }
        }

        private void txt_lastname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_username.Focus();
            }
        }

        private void txt_tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_member_descrip.Focus();
            }
        }

        private void txt_username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_tel.Focus();
            }
        }

        private void txt_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_address.Focus();
            }
        }

        private void txt_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_national_iD.Focus();
            }
        }

        private void txt_hint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_password.Focus();
            }
        }

        private void txt_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_confpassword.Focus();
            }
        }

        private void txt_confpassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.Focus();
            }
        }

        private void EditProfile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button2.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            setpanal1(false);
            comboBox1.Visible = true;
            label14.Visible = true;
            panel2.Visible = true;
            button5.Enabled = true;
            txt_username.Text = txt_allname.Text = txt_address.Text = txt_member_descrip.Text = txt_national_iD.Text = txt_tel.Text = "";
            comboBox1.ResetText();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = true;
            button5.Text = "تأكيد الإضافة";
            try
            {
                if (txt_allname.Text != "" && txt_password.Text == txt_confpassword.Text)
                {
                    try
                    {
                        var maxid = human_rights.Tbl_User_Datas.Max(s => s.ID);
                        maxid++;
                        human_rights.ins_used_data(maxid, txt_username.Text, txt_password.Text, txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 0);
                        button5.Text = "تم الإضافة";
                        button5.Enabled = false;
                        label13.Text = "تم الإضافة";
                    }
                    catch (Exception)
                    {
                        label13.Text = "أدخل بياناتك كاملة";
                    }
                }
                else
                {
                    label13.Text = "أدخل بياناتك كاملة او تحقق من الباسوورد";
                }
            }
            catch (Exception)
            {
                if (txt_allname.Text != "" && txt_password.Text == txt_confpassword.Text)
                {
                    try
                    {
                        human_rights.ins_used_data(1, txt_username.Text, txt_password.Text, txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 0);
                        button5.Text = "تم الإضافة";
                        button5.Enabled = false;
                        label13.Text = "تم الإضافة";
                    }
                    catch (Exception)
                    {
                        label13.Text = "أدخل بياناتك كاملة";
                    }
                }
                else
                {
                    label13.Text = "أدخل بياناتك كاملة او تحقق من الباسوورد";
                }
            }
        }
    }
}
