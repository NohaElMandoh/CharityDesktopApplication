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
    public partial class Query_Item : Form
    {
        DataClasses1DataContext human_rights = new DataClasses1DataContext();

        DataTable dt = new DataTable();
        public Query_Item()
        {
            InitializeComponent();
        }

        private void Query_Item_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("رقم عضو", typeof(string));
            dt.Columns.Add("إسم العضو",typeof(string));
            dt.Columns.Add("صفة العضوية", typeof(string));
            dt.Columns.Add("رقم موبايل", typeof(string));
            dt.Columns.Add("رقم قومى", typeof(string));
            dt.Columns.Add("عنوان", typeof(string));
            autocomplete();
            button1.Enabled = false;
            fill_combo();

            label3.Visible = true;
            button2.Visible = true;
            textBox1.Visible = true;
            label15.Visible = false;
            button4.Visible = false;
            textBox7.Visible = false;
        }

        public void fill_combo()
        {
            var allitems = human_rights.Tbl_Branches.Select(s => new { s.Branches, s.ID }).ToList();
            comboBox1.DataSource = allitems;
            comboBox1.DisplayMember = "Branches";
            comboBox1.ValueMember = "ID";
        }

        //private void set_datagridview_header_width_per_column()
        //{
        //    dataGridView1.ColumnCount = 5;
        //    DataGridViewColumn column = dataGridView1.Columns[5];
        //    //column.Width = 60;
        //    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        //}

        public void InitializeDataGridView()
        {
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         
            dataGridView1.AutoSize = true;
            dataGridView1.Anchor.ToString();
         //   dataGridView1.AutoSizeColumnsMode=;


        }
        
        private void Btn_Show_Store_Items_Click(object sender, EventArgs e)
        {InitializeDataGridView();

        //    var combo= human_rights.
        //if (comboBox1.SelectedValue.ToString())
        //{
            
        //}
            //set_datagridview_header_width_per_column();
        
            label8.Text = "";
            dt.Clear();
            var items = human_rights.Tbl_members_details.Where(s=>s.TBL_Branches_ID== int.Parse(comboBox1.SelectedValue.ToString())). Select(s => new {s.ID, s.all_name, s.membership_descrip, s.mobile, s.National_iD,s.address});
            var maxid = human_rights.Tbl_members_details.Max(s => s.ID);
            if (comboBox1.SelectedIndex==0)
            {
                var all = human_rights.Tbl_members_details.Select(s => new {s.ID, s.all_name, s.membership_descrip, s.mobile, s.National_iD, s.address });
                foreach (var item_all in all)
                {
                    dt.Rows.Add(item_all.ID.ToString(), item_all.all_name.ToString(), item_all.membership_descrip.ToString(), item_all.mobile.ToString(), item_all.National_iD.ToString(), item_all.address.ToString());
                    dataGridView1.DataSource = dt;    
                }
            }
            else
            {
                foreach (var item in items)
                {
                    dt.Rows.Add(item.ID.ToString(), item.all_name.ToString(), item.membership_descrip.ToString(), item.mobile.ToString(), item.National_iD.ToString(), item.address.ToString());
                    dataGridView1.DataSource = dt;
                }
            }
            
            label1.Text = dataGridView1.Rows.Count.ToString();
        }

        public void autocomplete()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            var allitems = human_rights.Tbl_members_details.Select(s => new{s.all_name} ).ToList();
            foreach (var item in allitems)
            {   MyCollection.Add(item.all_name);}
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection DataCollection = (MyCollection);
            textBox1.AutoCompleteCustomSource = DataCollection;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label7.Text = "";
                pictureBox1.Image = null;
                if (textBox1.Text != "")
                {
                    var items = human_rights.Tbl_members_details.Where(s => s.all_name== textBox1.Text).Select(s => new { s.all_name, s.membership_descrip, s.mobile, s.National_iD,s.address,s.TBL_Branches_ID});
                    foreach (var item in items)
                    {
                        textBox2.Text = item.all_name.ToString();
                        textBox3.Text = item.membership_descrip.ToString();
                        textBox4.Text = item.mobile.ToString();
                        textBox5.Text = item.National_iD.ToString();
                        textBox6.Text = item.address.ToString();
                        var branch=human_rights.Tbl_Branches.Where(s=>s.ID==item.TBL_Branches_ID).Select(s=>s.Branches).SingleOrDefault();
                        if (branch==null)
                        {
                            label14.Text = "تم حذف المكتب";
                        }
                        else
                        {
                            label14.Text = branch.ToString();
                        }
                        select_image();
                    }
                }
            }
            catch (Exception)
            {
                label13.Text = "تأكد من الإسم";
            }   
        }

        public void select_image()
        {
            var fetch_img = human_rights.Tbl_members_details.Where(s => s.all_name == textBox1.Text).Select(s => s.image).FirstOrDefault();
            var img_arr2 = (Byte[])(fetch_img.ToArray());
            MemoryStream ms1 = new MemoryStream(img_arr2);
            ms1.Seek(0, SeekOrigin.Begin);
            pictureBox1.Image = Image.FromStream(ms1);
        }

        public void select_image_Search_id()
        {
            var fetch_img = human_rights.Tbl_members_details.Where(s => s.ID ==int.Parse( textBox7.Text)).Select(s => s.image).FirstOrDefault();
            var img_arr2 = (Byte[])(fetch_img.ToArray());
            MemoryStream ms1 = new MemoryStream(img_arr2);
            ms1.Seek(0, SeekOrigin.Begin);
            pictureBox1.Image = Image.FromStream(ms1);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button2.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.DataSource!=null)
            //{   
            //    View_report_store_items v = new View_report_store_items();
            //    v.Show();
            //}
            //else
            //{
            //    label8.Text = "إضغط عرض أصناف المخزن أولا";
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {this.Close();}

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Image==null)
            {
                label13.Text = "Empty Image";
            }
            else
            {
                label13.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                label7.Text = "";
                pictureBox1.Image = null;
                if (textBox7.Text != "")
                {
                    var items = human_rights.Tbl_members_details.Where(s => s.ID == int.Parse( textBox7.Text)).Select(s => new { s.all_name, s.membership_descrip, s.mobile, s.National_iD, s.address, s.TBL_Branches_ID });
                    foreach (var item in items)
                    {
                        textBox2.Text = item.all_name.ToString();
                        textBox3.Text = item.membership_descrip.ToString();
                        textBox4.Text = item.mobile.ToString();
                        textBox5.Text = item.National_iD.ToString();
                        textBox6.Text = item.address.ToString();
                        var branch = human_rights.Tbl_Branches.Where(s => s.ID == item.TBL_Branches_ID).Select(s => s.Branches).SingleOrDefault();
                        if (branch == null)
                        {
                            label14.Text = "تم حذف المكتب";
                        }
                        else
                        {
                            label14.Text = branch.ToString();
                        }
                        select_image_Search_id();
                    }
                }
            }
            catch (Exception)
            {
                label13.Text = "تأكد من الإسم";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex==0)
            {
                label3.Visible = true;
                button2.Visible = true;
                textBox1.Visible = true;
                textBox1.Clear();
                label15.Visible = false;
                button4.Visible = false;
                textBox7.Visible = false;

            }
            else if (comboBox2.SelectedIndex==1)
            {
                label15.Visible = true;
                button4.Visible = true;
                textBox7.Visible = true;
                textBox7.Clear();
                label3.Visible = false;
                button2.Visible = false;
                textBox1.Visible = false;
            }
        }
    }
}