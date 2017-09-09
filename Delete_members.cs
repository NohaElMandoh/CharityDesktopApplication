using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Human_Rights
{
    public partial class Delete_members : Form
    {
        DataClasses1DataContext human_rights = new DataClasses1DataContext();

        public Delete_members()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex==0)
            {
                var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name, s.ID }).ToList();
                //foreach (var item in allitems)
                //{
                //    listBox1.Items.Add(item.all_name.ToString());
            
                //}
                listBox1.DataSource = allitems;
                listBox1.DisplayMember = "all_name";
                listBox1.ValueMember = "ID";
                //listBox2.DataSource = "";
                listBox2.Enabled = false;
                button2.Enabled = false;
                listBox1.Enabled = true;
                button1.Enabled = true;
            }
            else if (comboBox1.SelectedIndex==1)
            {
                var allitems = human_rights.Tbl_User_Datas.Select(s => new { s.all_name, s.ID }).ToList();
                listBox2.DataSource = allitems;
                listBox2.DisplayMember = "all_name";
                listBox2.ValueMember = "ID";
                //listBox1.DataSource = "";
                listBox1.Enabled = false;
                button1.Enabled = false;
                listBox2.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                human_rights.del_members_details(int.Parse(listBox1.SelectedValue.ToString()));
                label2.Text = "تم المسح";
                var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name, s.ID }).ToList();
                listBox1.DataSource = allitems;
                listBox1.DisplayMember = "all_name";
                listBox1.ValueMember = "ID";
            }
            catch (Exception)
            {
                label2.Text = "لم يتم المسح";
            }
           
        }

        //public bool isitemexist_allname_member_details(string x, object id)
        //{
        //    try
        //    {
        //        var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name, s.ID }).ToList();
        //        foreach (var item in allitems)
        //        {
        //            if (item.all_name == x && item.ID ==(int) id)
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public bool isitemexist_allname_member_details(string x, int id)
        {
            try
            {
                var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name, s.ID }).ToList();
                foreach (var item in allitems)
                {
                    if (item.all_name == x && item.ID == id)
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

        private void button3_Click(object sender, EventArgs e)
        {this.Close();}

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                human_rights.del_used_data(int.Parse(listBox2.SelectedValue.ToString()));
                label2.Text = "تم المسح";
                var allitems = human_rights.Tbl_User_Datas.Select(s => new { s.all_name, s.ID }).ToList();
                listBox2.DataSource = allitems;
                listBox2.DisplayMember = "all_name";
                listBox2.ValueMember = "ID";
            }
            catch (Exception)
            {
                label2.Text = "لم يتم المسح";
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            label5.Text = "";
            select_image();
        }

        public void select_image()
        {
            try
            {
                var fetch_img = human_rights.Tbl_members_details.Where(s => s.ID == int.Parse(listBox1.SelectedValue.ToString())).Select(s => s.image).FirstOrDefault();
                var img_arr2 = (Byte[])(fetch_img.ToArray());
                MemoryStream ms1 = new MemoryStream(img_arr2);
                ms1.Seek(0, SeekOrigin.Begin);
                pictureBox1.Image = Image.FromStream(ms1);
            }
            catch (Exception)
            {

                label5.Text = "Empty Image";
            }
            
        }
    }
}