using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.QrCode;
using System.Net.Mail;
using System.Web;
using System.Net;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using System.Data.SqlClient;

namespace LibraryManagementSystem
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;
        private void Form5_Load(object sender, EventArgs e)
        {
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo VideoCaptureDevice in webcam)
            {
                comboBox1.Items.Add(VideoCaptureDevice.Name);
            }
            comboBox1.SelectedIndex = 0;
            pictureBox2.Height = pictureBox2.Width = 200;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {
            if(cam.IsRunning)
            {
                cam.Stop();
            } 
           // imagecapt();
        }
        byte[] PhotoByte2 = null;
        private void button2_Click(object sender, EventArgs e)
        {
            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pictureBox2.Image = qrcode.Draw(textBox7.Text, 100);
            MemoryStream ms = new MemoryStream();
            byte[] PhotoByte2 = null;
            pictureBox2.Image.Save(ms, ImageFormat.Jpeg);
            PhotoByte2 = ms.ToArray(); 
        }
        public void imagecapt()
        { 
               MemoryStream ms = new MemoryStream();
                byte[] PhotoByte2 = null;
            pictureBox2.Image.Save(ms, ImageFormat.Jpeg);
            PhotoByte2 = ms.ToArray(); 
        }

     //   private void button4_Click(object sender, EventArgs e)
       // {
           /* MailMessage maile = new MailMessage(textBox8.Text, textBox9.Text, textBox10.Text, textBox14.Text);
            SmtpClient client = new SmtpClient(textBox11.Text);
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential(textBox12.Text, textBox13.Text);
            client.EnableSsl = true;
            client.Send(maile);
            MessageBox.Show("email sent");*/
         /*   SmtpClient stp = new SmtpClient("smtp.gmail.com", 587);
            stp.EnableSsl = true;
           stp.UseDefaultCredentials = false;
            stp.Credentials = new NetworkCredential("saurabhsharma0520@gmail.com", "golu58295829golu");
           // MailMessage mail = new MailMessage("saurabhsharma0520@gmail.com", "saurabhsharma0520@gmail.com", "testing",pictureBox3.Text);
            mail.Priority = MailPriority.High;
            stp.Send(mail);
            MessageBox.Show("mail sent");
            string attachmentPath = Environment.CurrentDirectory + @"\im2.jpg";
            Attachment inline = new Attachment(attachmentPath);
            inline.ContentDisposition.Inline = true;
            inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
           // inline.ContentId = contentID;
            inline.ContentType.MediaType = "image/jpg";
            inline.ContentType.Name = Path.GetFileName(attachmentPath);
             mail.Attachments.Add(inline);*/
       // }
     /*   public void click()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int width = Convert.ToInt32(pictureBox2.Width);
                int height = Convert.ToInt32(pictureBox2.Height);
                Bitmap bmp = new Bitmap(width, height);
                pictureBox2.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                bmp.Save(dialog.FileName, ImageFormat.Jpeg);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            click();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
               // pictureBox3.Image = new Bitmap(open.FileName);
                // image file path
               // textBox1.Text = open.FileName;
         //   }
        //}

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Height = pictureBox2.Width = 200;
        }*/


        byte [] b;
        string t;
        byte [] PhotoByte1;
        private void button5_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                t = op.FileName;
                pictureBox1.ImageLocation = t;
                b = File.ReadAllBytes(t);
             //   MemoryStream ms = new MemoryStream();
                //byte[] PhotoByte1 = null;
               // pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                //PhotoByte1 = ms.ToArray(); 
            }
        }
        Class1 c=new Class1();
        private void button4_Click_1(object sender, EventArgs e)
        {
            var textBoxes = new TextBox[] { textBox1, textBox2, textBox5, textBox6,
                                 textBox7,};
            if (textBoxes.Any(tb => tb.Text == String.Empty) || pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Fill All Entries", "Message!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c.ins_del_up("insert into Student(student_name,student_image,student_enrollment,student_department,student_semester,student_contact,student_email,barcode_name) values('" + textBox1.Text + "','" + b + "','" + textBox2.Text + "','" + comboBox2.SelectedItem + "','" + comboBox3.SelectedItem + "','" + textBox5.Text + "','" + textBox6.Text + "','" + PhotoByte2 + "') ");
                MessageBox.Show("Record Inserted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox1.Text = textBox2.Text = textBox5.Text = textBox6.Text = textBox7.Text = " ";
                pictureBox1.Image = pictureBox2.Image = null;
            }
        }
     /*   private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
if (dialog.ShowDialog(
      * 
      * ) == DialogResult.OK)
{
   int width = Convert.ToInt32(pictureBox2.Width); 
   int height = Convert.ToInt32(pictureBox2.Height); 
   Bitmap bmp = new Bitmap(width,height);        
   pictureBox2.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
   bmp.Save(dialog.FileName, ImageFormat.Jpeg);
}*
        }*/

       
    }
}
