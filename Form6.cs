using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppApi.Account;
using WhatsAppApi.Parser;
using WhatsAppApi.Helper;
using WhatsAppApi.Register;
using WhatsAppApi;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.QrCode;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using System.Data.SqlClient;


namespace LibraryManagementSystem
{
    public partial class Form6 : Form
    {
        
        string pwd = Class2.GetRandomPassword(20);
        string pwd1 = Class2.GetRandomPassword(20);
        string wanted_path,wanted_path1;
        public Form6()
        {
            InitializeComponent();
        }
        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;

        byte[] PhotoByte2;
        private void button2_Click(object sender, EventArgs e)
        {
            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pictureBox2.Image = qrcode.Draw(textBox7.Text, 100);
            MemoryStream ms = new MemoryStream();
           // byte[] PhotoByte2 = null;
            pictureBox2.Image.Save(ms, ImageFormat.Jpeg);
            PhotoByte2 = ms.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
            cam.Start();
        }

        private void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bit;

        }

        private void Form6_Load(object sender, EventArgs e)
        {

            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in webcam)
            {
                comboBox1.Items.Add(VideoCaptureDevice.Name);
            }
            comboBox1.SelectedIndex = 0;
            pictureBox2.Height = pictureBox2.Width = 200;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cam.IsRunning)
            {
                cam.Stop();
            }
        }
        Class1 c = new Class1();
        private void button4_Click(object sender, EventArgs e)
        {
            var textBoxes = new TextBox[] { textBox1, textBox2, textBox5, textBox6,
                                 };
            if (textBoxes.Any(tb => tb.Text == String.Empty) || pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Fill All Entries", "Message!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string img_path;
                File.Copy(openFileDialog1.FileName,wanted_path + "\\student_images\\" + pwd +".jpg");
                img_path = "student_images\\" + pwd + ".jpg";

                string img_path1;
                File.Copy(openFileDialog2.FileName, wanted_path1 + "\\student_barcodes\\" + pwd1 + ".jpg");
                img_path1 = "student_barcodes\\" + pwd1 + ".jpg";

                c.ins_del_up("insert into Students(student_name,student_image,student_enrollment,student_department,student_semester,student_contact,student_email,student_barcode) values('" + textBox1.Text + "','" + img_path.ToString() + "','" + textBox2.Text + "','" + comboBox2.SelectedItem + "','" + comboBox3.SelectedItem + "','" + textBox5.Text + "','" + textBox6.Text + "','" +img_path1.ToString()+ "') ");
                MessageBox.Show("Record Inserted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox1.Text = textBox2.Text = textBox5.Text = textBox6.Text = textBox7.Text = " ";
                pictureBox1.Image = pictureBox2.Image = null;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
            }
        }
      
        private void button5_Click(object sender, EventArgs e)
        {
            wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            DialogResult result = openFileDialog1.ShowDialog();
            openFileDialog1.Filter="JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|* .gif";
            if (result == DialogResult.OK)
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                pictureBox2.Image.Save(sfd.FileName, format);
                pictureBox2.Image = null;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                pictureBox1.Image.Save(sfd.FileName, format);
                pictureBox1.Image = null;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            wanted_path1 = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            DialogResult result = openFileDialog2.ShowDialog();
            openFileDialog2.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|* .gif";
            if (result == DialogResult.OK)
                pictureBox2.ImageLocation = openFileDialog2.FileName;
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 p = new MDIParent1();
            p.Show();
        }
    }
}