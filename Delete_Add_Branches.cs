using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Human_Rights
{
    public partial class Delete_Add_Branches : Form
    {
        DataClasses1DataContext human_rights = new DataClasses1DataContext();

        public Delete_Add_Branches()
        {
            InitializeComponent();
        }

        

        public bool isitemexit(string x)
        {
            try
            {
                var allitems = human_rights.Tbl_Branches.Select(s => new { s.Branches }).ToList();
                foreach (var item in allitems)
                {
                    if (item.Branches == x)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label4.Text = "";
            autocomplete();
            
        }
        public void autocomplete()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            var allitems = human_rights.Tbl_Branches.Select(s => new { s.Branches }).ToList();
            foreach (var item in allitems)
            { MyCollection.Add(item.Branches); }
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection DataCollection = (MyCollection);
            textBox1.AutoCompleteCustomSource = DataCollection;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {   
                    human_rights.ins_new_office_branch(1, textBox1.Text);
                    MessageBox.Show("Data inserted successfully");
                    var allitems = human_rights.Tbl_Branches.Select(s => new { s.Branches ,s.ID}).ToList();
                    listBox1.DataSource = allitems;
                    listBox1.DisplayMember = "Branches";
                    listBox1.ValueMember = "ID";

                    listBox2.DataSource = allitems;
                    listBox2.DisplayMember = "Branches";
                    listBox2.ValueMember = "ID";
            }
            catch (Exception)
            {
                    var maxid = human_rights.Tbl_Branches.Max(s => s.ID);
                    maxid++;
                    human_rights.ins_new_office_branch(maxid, textBox1.Text);
                    MessageBox.Show("Data inserted successfully");
                    var allitems = human_rights.Tbl_Branches.Select(s => new { s.Branches,s.ID }).ToList();
                    listBox1.DataSource = allitems;
                    listBox1.DisplayMember = "Branches";
                    listBox1.ValueMember = "ID";

                    listBox2.DataSource = allitems;
                    listBox2.DisplayMember = "Branches";
                    listBox2.ValueMember = "ID";
            }
        }

        private void Delete_Add_Branches_Load(object sender, EventArgs e)
        {
                var allitems = human_rights.Tbl_Branches.Select(s => new { s.Branches ,s.ID }).ToList();
                listBox1.DataSource = allitems;
                listBox1.DisplayMember = "Branches";
                listBox1.ValueMember = "ID";
                
                listBox2.DataSource = allitems;
                listBox2.DisplayMember = "Branches";
                listBox2.ValueMember = "ID";   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                human_rights.del_offices_branches(int.Parse(listBox2.SelectedValue.ToString()));
                label4.Text = "تم المسح";
                var allitems = human_rights.Tbl_Branches.Select(s => new { s.Branches, s.ID }).ToList();
                listBox2.DataSource = allitems;
                listBox2.DisplayMember = "Branches";
                listBox2.ValueMember = "ID";
                listBox1.DataSource = allitems;
                listBox1.DisplayMember = "Branches";
                listBox1.ValueMember = "ID";
            }
            catch (Exception)
            {
                label4.Text = "لم يتم المسح";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
