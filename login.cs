using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Collections;
using BU_HRights;


namespace Human_Rights
{   
    public partial class login : Form
    {
        DataClasses1DataContext human_rights = new DataClasses1DataContext();
        //mazayaEntities mob_sys = new mazayaEntities();

       public static string name;
       public static int ID;
       
        public login()
        {
            InitializeComponent();
        }

        //string hd_serial_num = "            5LSH8ARP";
        //------------
        
    
        
    DateTime Startdate = new DateTime();
    DateTime finaldate = new DateTime(2016, 5, 8);
    public void Session_Closed()
    {
        Startdate = DateTime.Now;
        label1.Text = Startdate.ToString();
        if (Startdate >= finaldate)
        {
            for (int i = 0; i < finaldate.Ticks; i++)
            {
                if (Startdate >= finaldate)
                {
                    MessageBox.Show("License requires Upgrade\n إنت الى عملت فى نفسك كدة");
                }
            }
        }
        else
        {
            MessageBox.Show("Welcome");
        }
    }

        private void login_Load(object sender, EventArgs e)
        {
            Session_Closed();
            ArrayList hdCollection = new ArrayList();
            //serial number is here
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            //int I1 = 0;
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                // get the hard drive from collection
                // using index
                 HardDrive hd = new HardDrive();

                // get the hardware serial no.
                if (wmi_HD["SerialNumber"] == null)
                    hd.SerialNo = "None";
                else
                    hd.SerialNo = wmi_HD["SerialNumber"].ToString();
                //hd = (HardDrive)hdCollection[i];
                hdCollection.Add(hd.SerialNo);
                //++I1;

                for (int i = 0; i < hdCollection.Count - 1; i++)
                {
                    label5.Text = hdCollection[0].ToString();
                }

                foreach (var item in hdCollection)
                {
                    //listBox1.Items.Add(item);
                    //comboBox1.Items.Add(item);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (label5.Text == hd_serial_num)
            //{
                Form1 f1 = new Form1();
                bool x = false;
                if (txt_name.Text != "" && txt_pass.Text != "")
                {
                    var logintbl = human_rights.Tbl_User_Datas.Select(s => new { ID = s.ID, Password = s.Password, Name = s.UserName }).ToList();
                    foreach (var item in logintbl)
                    {
                        if (txt_name.Text == item.Name && txt_pass.Text == item.Password)
                        {
                            x = true;
                            ID = item.ID;
                            break;
                        }
                        else x = false;
                    }
                    if (x)
                    {
                        name = txt_name.Text;
                        label4.Text = "مسموح الدخول";
                        Class1.TheValue = name;
                        Class1.TheID = ID;
                        this.Hide();
                        f1.Show();
                    }
                    else label4.Text = "access denied";
                }
                else label4.Text = "verify user name & password";
            //}
            //else
            //{
            //    label4.Text = "إحذر ! هكذا أنت تلعب بعداد عمر البرنامج ";
            //}
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    if (listBox1.SelectedItem != null)
        //    {
        //        textBox1.Text = listBox1.SelectedItem.ToString();
        //        label5.Text = listBox1.SelectedItem.ToString();
        //    }
        //    else
        //    {
        //        MessageBox.Show("select your item on listbox");
        //    }
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    if (label5.Text == textBox1.Text)
        //    {
        //        MessageBox.Show("Done");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Not yet");
        //    }
        //}

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {   
            label1.Refresh();
            //TimeSpan baseInterval = new TimeSpan(3);
            //TimeSpan span = TimeSpan.FromMilliseconds(500);
            //int Compared_0 = TimeSpan.Compare(baseInterval, span);
            
            //if (baseInterval >= span)
            //{
            
            //}
            //else
            //{
            
            //}
        }
    }
}