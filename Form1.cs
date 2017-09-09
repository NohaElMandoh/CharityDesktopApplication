using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BU_HRights;


namespace Human_Rights
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
                EditProfile_members epm = new EditProfile_members();
                epm.ShowDialog();
           // }
            //catch (Exception)
            //{
            //    DialogResult dialogResult = MessageBox.Show("إذهب للقائمة الرئيسية و إنتظر قليلا ، جارى تهيئة البيانات", "تحذير", MessageBoxButtons.OK,MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            //    if (dialogResult == DialogResult.OK)
            //    {
            //        //this.ShowDialog();
            //        this.Close();
            //        Form1 f = new Form1();
            //        f.ShowDialog();
            //    }
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {   
            Query_Item query = new Query_Item();
            query.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {   
            EditProfile edit = new EditProfile();
            edit.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Delete_members dm = new Delete_members();
            dm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Back_HURights b = new Back_HURights();
                b.Implement_Backup();
                MessageBox.Show("Database Backup is up to date in : ");
            }
            catch (Exception)
            {
                MessageBox.Show("Insuccessfull Operation");
            }    
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Delete_Add_Branches dab = new Delete_Add_Branches();
            dab.ShowDialog();
        }
    }
}