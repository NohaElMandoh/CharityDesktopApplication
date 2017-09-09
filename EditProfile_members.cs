using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace Human_Rights
{
    public partial class EditProfile_members : Form
    {
        DataClasses1DataContext human_rights = new DataClasses1DataContext();
        
        public EditProfile_members()
        {
            InitializeComponent();
        }
        
        public void setpanal1(bool x)
        {
            txt_address.ReadOnly=x;
            txt_member_descrip.ReadOnly = x;
            txt_allname.ReadOnly = x;
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
            setpanal1(false);
            button2.Enabled = false;
            button1.Enabled = true;
            button6.Enabled = true;
            textBox1.ReadOnly = false;
            try
            {
                if (txt_allname.Text != "")
                {
                    var items = human_rights.Tbl_members_details.Where(s => s.all_name == txt_allname.Text).Select(s => new { s.all_name, s.membership_descrip, s.mobile, s.National_iD, s.address,s.State,s.ID,s.image,s.TBL_Branches_ID });
                    foreach (var item in items)
                    {
                        txt_allname.Text = item.all_name.ToString();
                        txt_member_descrip.Text = item.membership_descrip.ToString();
                        txt_tel.Text = item.mobile.ToString();
                        txt_national_iD.Text = item.National_iD.ToString();
                        txt_address.Text = item.address.ToString();
                        txt_membership_ID.Text = item.ID.ToString();
                        //var img_arr1 = (Byte[])(item.image.ToArray());
                        
                        var combo = human_rights.Tbl_Branches.Where(s => s.ID == item.TBL_Branches_ID).Select(s => s.Branches).FirstOrDefault();

                        comboBox1.Text = Convert.ToString(combo);
                        select_image_Edit_Button();
                        //pictureBox1.Image = Image.FromFile(item.image.ToString());
                        //MemoryStream ms1 = new MemoryStream();
                        //pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);            
                        
                    }
                }
            }
            catch (Exception)
            {
                label13.Text = "didn't view member's Picture";
            }
        }
        public bool isitemexit_id_member_details(int x)
        {
            try
            {
                var allitems = human_rights.Tbl_members_details.Select(s => new { s.ID}).ToList();
                foreach (var item in allitems)
                {
                    if (item.ID == x)
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

        public bool isitemexit(string x)
        {
            try
            {
                var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name }).ToList();
                foreach (var item in allitems)
                {
                    if (item.all_name == x)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null&&fd1.CheckPathExists==false)//&&fd1.CheckPathExists==false
            {
                update_image_from_picture_box_without_change();
            }
            else if (pictureBox1.Image != null&&fd1.CheckPathExists==true)
            {
                update_image_from_picture_box_without_change();
            }
            else if (fd1.CheckPathExists == true&&pictureBox1.Image==null)
            {
                update_image_from_File_dialouge_path();    
            }
            else if(fd1.CheckPathExists==false&&pictureBox1.Image==null)
            {
                select_image_from_blank_image_table_then_update_member_details_table();    
            }
            //else
            //{//blank image
            //    var blank_image = human_rights.Blank_Images.Where(s => s.ID == 1).Select(s => s.image).FirstOrDefault();
            //    var img_arr2 = (Byte[])(blank_image.ToArray());
            //    MemoryStream ms1 = new MemoryStream(img_arr2);
            //    ms1.Seek(0, SeekOrigin.Begin);
            //    pictureBox1.Image = Image.FromStream(ms1);
            //    var maxid = human_rights.Tbl_members_details.Max(s => s.ID);
            //    maxid++;
            //    if (!isitemexit(txt_allname.Text))
            //    {
            //        label13.Text = "العضو غير موجود إضغط إضافة ، أدخل البيانات ، ثم قم بالتأكيد";
            //    }
            //    else
            //    {
            //        button1.Enabled = true;
            //        if (txt_allname.Text != "" && txt_member_descrip.Text != "" && txt_tel.Text != "" && txt_address.Text != "" && txt_national_iD.Text != "" && textBox1.Text != "")
            //        {
            //            human_rights.updatemember_details(Class1.TheID,
            //                txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 1, img_arr2);
            //            button1.Text = "حفظ";
            //            DialogResult dialogResult = MessageBox.Show("تم الحفظ ......هل تريد التعديل ؟", "Some Title", MessageBoxButtons.YesNo);
            //            if (dialogResult == DialogResult.Yes)
            //            {
            //                setpanal1(false);
            //                button2.Enabled = false;
            //                button1.Enabled = true;
            //                button1.Text = "تم التعديل";
            //            }
            //            else if (dialogResult == DialogResult.No)
            //            {
            //                this.Close();
            //            }
            //        }
            //        else label13.Text = "تأكد من ادخال كافة البيانات";
            //    }
 
            //}
        }
        public void fill_combo()
        {
            var allitems = human_rights.Tbl_Branches.Select(s => new { s.Branches,s.ID }).ToList();
            comboBox1.DataSource = allitems;
            comboBox1.DisplayMember = "Branches";
            comboBox1.ValueMember = "ID";
            

        }
        private void EditProfile_members_Load(object sender, EventArgs e)
        {

            fill_combo();
            fd1.Reset();
            setpanal1(true);
            button2.Enabled = true;
            button1.Enabled = false;
            button5.Enabled = false;
            var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name,s.ID }).ToList();
            //foreach (var item in allitems)
            //{
            ////    listBox1.Items.Add(item.all_name.ToString());
                
            //}
            listBox1.DataSource = allitems;
            listBox1.DisplayMember = "all_name";
            listBox1.ValueMember = "ID";
            
            
        }

        private void txt_tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_address.Focus();
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
                txt_tel.Focus();
            }
        }

        private void txt_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_national_iD.Focus();
            }
        }

        

        
        private void EditProfile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button2.Focus();
            }
        }

        private void txt_allname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_member_descrip.Focus();
                button2.Enabled = true;

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label13.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            setpanal1(false);
            //button2.Enabled = false;
            //button1.Enabled = true;
            label10.Text = "صورة العضو لا تزيد \nعن 220 بكسل عرض \nو 164 بكسل إرتفاع";
            button1.Text = "حفظ";
            txt_membership_ID.Text=label13.Text= txt_allname.Text = txt_address.Text = txt_member_descrip.Text = txt_national_iD.Text = txt_tel.Text = textBox1.Text = "";
            button5.Enabled = true;
            button6.Enabled = true;
            textBox1.ReadOnly = false;
            pictureBox1.Image = null;
            button5.Text = "تأكيد الإضافة";
            comboBox1.ResetText();
        }

        private void txt_allname_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true; 
            button1.Enabled = false;
            button1.Enabled = true;
            button5.Enabled = true;
            autocomplete();
        }

        public void autocomplete()
        {
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name }).ToList();
            foreach (var item in allitems)
            { MyCollection.Add(item.all_name); }
            txt_allname.AutoCompleteMode = AutoCompleteMode.Suggest;
            txt_allname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection DataCollection = (MyCollection);
            txt_allname.AutoCompleteCustomSource = DataCollection;
        }

        private void txt_allname_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            label13.Text = "";
            if (pictureBox1.Image!=null)
            {
                upload_image_exist_in_picture_box();    
            }
            else
            {
                upload_image_exist_blank_table();
            }
            
            //try
            //{
                //try
                //{
                    //var maxid = human_rights.Tbl_members_details.Max(s => s.ID);
                    //maxid++;
                    //if (txt_allname.Text != "" )
                    //{
                    //    human_rights.ins_members_details(txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 1,);
                    //}
                    //button5.Text = "تم الإضافة";
                    //button5.Enabled = false;
                    //var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name, s.ID }).ToList();
                    //listBox1.DataSource = allitems;
                    //listBox1.DisplayMember = "all_name";
                    //listBox1.ValueMember = "ID";
                //}
                //catch (Exception)
                //{

                //    label13.Text = "أدخل بياناتك كاملة";
                //}
            
            //}
            //catch (Exception)
            //{

            //    try
            //    {
            //        if (txt_allname.Text != "" )
            //        {
            //            human_rights.ins_members_details(1, txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 1);
            //     اه   }
            //        button5.Text = "تم الإضافة";
            //        button5.Enabled = false;
            //    }
            //    catch (Exception)
            //    {
            //        label13.Text = "أدخل بياناتك كاملة";
            //    }
            //}
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            button1.Text = "حفظ التعديل";
            button1.Enabled = false;
            try
            {
                    var items = human_rights.Tbl_members_details.Where(s => s.ID == int.Parse( listBox1.SelectedValue.ToString())).Select(s => new
                    { s.all_name, s.membership_descrip, s.mobile, s.National_iD, s.address, s.State,s.ID ,s.image,s.TBL_Branches_ID}).ToList();
                    foreach (var item in items)
                    {
                        txt_allname.Text = item.all_name.ToString();
                        txt_member_descrip.Text = item.membership_descrip.ToString();
                        txt_tel.Text = item.mobile.ToString();
                        txt_national_iD.Text = item.National_iD.ToString();
                        txt_address.Text = item.address.ToString();
                        txt_membership_ID.Text = item.ID.ToString();
                        //var img_arr1 = (Byte[])(item.image.ToArray());
                        var combo = human_rights.Tbl_Branches.Where(s => s.ID == item.TBL_Branches_ID).Select(s => s.Branches).FirstOrDefault();

                        comboBox1.Text = Convert.ToString(combo);
                        select_image_listbox();
                    }
            }
            catch (Exception)
            {
                //label13.Text = "تأكد من الإسم";
            }
        }
        OpenFileDialog fd1 = new OpenFileDialog();

        private void button6_Click(object sender, EventArgs e)
        {
            fd1.Filter = "image files|*.jpg;*.png;*.gif;*.icon;.*;";//
            DialogResult dres1 = fd1.ShowDialog();
            if (dres1 == DialogResult.Abort)
                return;
            if (dres1 == DialogResult.Cancel)
                return;
            textBox1.Text = fd1.FileName;
            pictureBox1.Image = Image.FromFile(fd1.FileName);
            MemoryStream ms1 = new MemoryStream();
            pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public void select_image_Edit_Button()
        {
            var fetch_img = human_rights.Tbl_members_details.Where(s => s.all_name == txt_allname.Text).Select(s => s.image).FirstOrDefault();
            var img_arr2 = (Byte[])(fetch_img.ToArray());
            MemoryStream ms1 = new MemoryStream(img_arr2);
            ms1.Seek(0, SeekOrigin.Begin);
            pictureBox1.Image = Image.FromStream(ms1);
        }
      
        public void select_image_listbox()
        {
            var fetch_img = human_rights.Tbl_members_details.Where(s => s.ID == int.Parse(listBox1.SelectedValue.ToString())).Select(s => s.image).FirstOrDefault();
             var img_arr2 = (Byte[])(fetch_img.ToArray());
            MemoryStream ms1 = new MemoryStream(img_arr2);
            ms1.Seek(0, SeekOrigin.Begin);
            pictureBox1.Image = Image.FromStream(ms1);

        }
        public void update_image_from_File_dialouge_path()
        {
            //MemoryStream ms1 = new MemoryStream();
            //pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
            //byte[] img_arr1 = new byte[ms1.Length];
            //ms1.Read(img_arr1, 0, img_arr1.Length);

            var x = fd1.FileName;
            byte[] bytes = File.ReadAllBytes(@x);
            var maxid = human_rights.Tbl_members_details.Max(s => s.ID);
            maxid++;
            if (!isitemexit(txt_allname.Text))
            {
                label13.Text = "العضو غير موجود إضغط إضافة ، أدخل البيانات ، ثم قم بالتأكيد";
            }
            else
            {
                button1.Enabled = true;
                if (txt_allname.Text != "" && txt_member_descrip.Text != "" && txt_tel.Text != "" && txt_address.Text != "" && txt_national_iD.Text != ""&& comboBox1.SelectedValue!="")
                {
                    var combo = int.Parse(comboBox1.SelectedValue.ToString());
                    human_rights.updatemember_details(Class1.TheID,
                        txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 1, bytes,combo);
                    button1.Text = "حفظ";
                    DialogResult dialogResult = MessageBox.Show("تم الحفظ ......هل تريد التعديل ؟", "Some Title", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        setpanal1(false);
                        button2.Enabled = false;
                        button1.Enabled = true;
                        button1.Text = "تم التعديل";
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        this.Close();
                    }
                }
                else label13.Text = "تأكد من ادخال كافة البيانات";
            }
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public void select_image_from_blank_image_table_then_update_member_details_table()
        {
            var blank_table = human_rights.Blank_Images.Where(s => s.ID == 1).Select(s => s.image).FirstOrDefault();
            //MemoryStream ms1 = new MemoryStream();
            //pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
            //byte[] img_arr1 = new byte[blank_table];
            //ms1.Read(img_arr1, 0, img_arr1.Length);
            //var x = fd1.FileName;
            //byte[] bytes = File.ReadAllBytes(@x);
            var maxid = human_rights.Tbl_members_details.Max(s => s.ID);
            maxid++;
            if (!isitemexit(txt_allname.Text))
            {
                label13.Text = "العضو غير موجود إضغط إضافة ، أدخل البيانات ، ثم قم بالتأكيد";
            }
            else
            {
                button1.Enabled = true;
                if (txt_allname.Text != "" && txt_member_descrip.Text != "" && txt_tel.Text != "" && txt_address.Text != "" && txt_national_iD.Text != ""&&comboBox1.SelectedValue!="")
                {
                    var combo = int.Parse(comboBox1.SelectedValue.ToString());
                    human_rights.updatemember_details(Class1.TheID,
                        txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 1, blank_table,combo);
                    button1.Text = "حفظ";
                    DialogResult dialogResult = MessageBox.Show("تم الحفظ ......هل تريد التعديل ؟", "Some Title", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        setpanal1(false);
                        button2.Enabled = false;
                        button1.Enabled = true;
                        button1.Text = "تم التعديل";
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        this.Close();
                    }
                }
                else label13.Text = "تأكد من ادخال كافة البيانات";
            }
        }

        public void update_image_from_picture_box_without_change()
        {
                MemoryStream ms1 = new MemoryStream();
                pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] img_arr1 = new byte[ms1.Length];
                ms1.Read(img_arr1, 0, img_arr1.Length);
                    //var x = fd1.FileName;
                    //byte[] bytes = File.ReadAllBytes(@x);
                byte[] bytes = imageToByteArray(pictureBox1.Image);
                    var maxid = human_rights.Tbl_members_details.Max(s => s.ID);
                    maxid++;
                    if (!isitemexit(txt_allname.Text))
                    {
                        label13.Text = "العضو غير موجود ، إضغط تأكيد الإضافة بعد التأكد من إدخال البيانات ";
                    }
                    else
                    {
                        button1.Enabled = true;
                        if (txt_allname.Text != "" && txt_member_descrip.Text != "" && txt_tel.Text != "" && txt_address.Text != "" && txt_national_iD.Text != ""&&comboBox1.SelectedValue!="")
                        {
                            var combo = int.Parse(comboBox1.SelectedValue.ToString());
                            human_rights.updatemember_details(Class1.TheID,
                                txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 1, bytes,combo);
                            
                            button1.Text = "حفظ";
                            DialogResult dialogResult = MessageBox.Show("تم الحفظ ......هل تريد التعديل ؟", "Some Title", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                setpanal1(false);
                                button2.Enabled = false;
                                button1.Enabled = true;
                                button1.Text = "تم التعديل";
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                this.Close();
                            }
                        }
                        else label13.Text = "تأكد من ادخال كافة البيانات";
                    }
        }
        public void upload_image_exist_blank_table()
        {
            if (!isitemexit(txt_allname.Text))
            {
                var blank_table = human_rights.Blank_Images.Where(s => s.ID == 1).Select(s => s.image).FirstOrDefault();
                try
                {
                    if (txt_allname.Text != ""&&comboBox1.SelectedValue!="")
                    {
                        var combo = int.Parse(comboBox1.SelectedValue.ToString());
                        human_rights.ins_members_details(1, txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 1, blank_table,combo);
                        MessageBox.Show("Data inserted successfully");
                        button5.Text = "تم الإضافة";
                        button5.Enabled = false;
                        var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name, s.ID }).ToList();
                        listBox1.DataSource = allitems;
                        listBox1.DisplayMember = "all_name";
                        listBox1.ValueMember = "ID";
                    }
                    else
                    {
                        MessageBox.Show("فضلا تأكد من إتمام إدخال جميع البيانات بما فيهم صورة العضو");
                    }
                    
                }
                catch (Exception)
                {
                    if (txt_allname.Text != ""&&comboBox1.SelectedValue!="")
                    {
                        var combo = int.Parse(comboBox1.SelectedValue.ToString());
                        var maxid = human_rights.Tbl_members_details.Max(s => s.ID);
                        maxid++;
                        human_rights.ins_members_details(maxid, txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 1, blank_table,combo);
                        MessageBox.Show("Data inserted successfully");
                        button5.Text = "تم الإضافة";
                        button5.Enabled = false;
                        var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name, s.ID }).ToList();
                        listBox1.DataSource = allitems;
                        listBox1.DisplayMember = "all_name";
                        listBox1.ValueMember = "ID";
                    }
                    else
                    {
                        MessageBox.Show("فضلا تأكد من إتمام إدخال جميع البيانات بما فيهم صورة العضو");
                    }
                    
                }
                
            }
            else
            {
                MessageBox.Show("الإسم موجود ، أضف رقم على الإسم للدلالة على صاحبه");
            }
        }
        public void upload_image_exist_in_picture_box()
        {
            if (!isitemexit(txt_allname.Text))
            {
                MemoryStream ms1 = new MemoryStream();
                pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] img_arr1 = new byte[ms1.Length];
                ms1.Read(img_arr1, 0, img_arr1.Length);
                var x = fd1.FileName;
                byte[] bytes = File.ReadAllBytes(@x);
                var maxid = human_rights.Tbl_members_details.Max(s => s.ID);
                maxid++;
                try
                {
                    if (txt_allname.Text != "" && textBox1.Text != "" && comboBox1.SelectedValue!="")
                    {
                        var combo = int.Parse(comboBox1.SelectedValue.ToString());
                        //select//var Branch_combo = human_rights.Tbl_members_details.Where(s => s.TBL_Branches_ID == combo).Select(s => s.all_name);
                        human_rights.ins_members_details(maxid, txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 1, bytes,combo);
                        MessageBox.Show("Data inserted successfully");
                        button5.Text = "تم الإضافة";
                        button5.Enabled = false;
                        var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name, s.ID }).ToList();
                        listBox1.DataSource = allitems;
                        listBox1.DisplayMember = "all_name";
                        listBox1.ValueMember = "ID";
                    }
                    else
                    {
                        MessageBox.Show("فضلا تأكد من إتمام إدخال جميع البيانات بما فيهم صورة العضو");
                    }
                }
                catch (Exception)
                {
                    if (txt_allname.Text != "" && textBox1.Text != ""&&comboBox1.SelectedValue!="")
                    {
                        var combo = int.Parse(comboBox1.SelectedValue.ToString());
                        human_rights.ins_members_details(1, txt_allname.Text, txt_tel.Text, txt_member_descrip.Text, txt_address.Text, txt_national_iD.Text, 1, bytes,combo);
                        MessageBox.Show("Data inserted successfully");
                        button5.Text = "تم الإضافة";
                        button5.Enabled = false;
                        var allitems = human_rights.Tbl_members_details.Select(s => new { s.all_name, s.ID }).ToList();
                        listBox1.DataSource = allitems;
                        listBox1.DisplayMember = "all_name";
                        listBox1.ValueMember = "ID";
                    }
                    else
                    {
                        MessageBox.Show("فضلا تأكد من إتمام إدخال جميع البيانات بما فيهم صورة العضو");
                    }
                }
            }
            else
            {
                MessageBox.Show("الإسم موجود ، أضف رقم على الإسم للدلالة على صاحبه");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Image==null)
            {
                label8.Text = "Empty Image";
            }
            else
            {
                label8.Text = "";
            }
        }    
    }
}