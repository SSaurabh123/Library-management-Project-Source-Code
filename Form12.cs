using System;
using System.Drawing;
using System.Windows.Forms;

using Dynamsoft.Barcode;
using TouchlessLib;
using System.Diagnostics;

namespace LibraryManagementSystem
{
    public partial class Form12 : Form
    {
        private BarcodeReader _barcodeReader;
        private TouchlessMgr _touch;
        private const int _previewWidth = 640;
        private const int _previewHeight = 480;

        public Form12()
        {
            InitializeComponent();

            // Initialize Dynamsoft Barcode Reader
            _barcodeReader = new BarcodeReader();
            // Initialize Touchless
            _touch = new TouchlessMgr();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                //dlg.Filter = "bmp files (*.bmp)|*.bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bitmap = null;

                    try
                    {
                        bitmap = new Bitmap(dlg.FileName);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("File not supported.");
                        return;
                    }

                    pictureBox1.Image = new Bitmap(dlg.FileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image!");
                return;
            }

            ReadBarcode((Bitmap)pictureBox1.Image);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string button_text = button3.Text;
            if (button_text.Equals("Start Webcam"))
            {
                button3.Text = "Stop Webcam";
                StartCamera();
            }
            else
            {
                button3.Text = "Start Webcam";
                StopCamera();
            }
        }

        private void StartCamera()
        {
            if (_touch.Cameras.Count == 0)
            {
                MessageBox.Show("No USB webcam connected");
                button3.Text = "Start Webcam";
                return;
            }

            // Start to acquire images
            _touch.CurrentCamera = _touch.Cameras[0];
            _touch.CurrentCamera.CaptureWidth = _previewWidth; // Set width
            _touch.CurrentCamera.CaptureWidth = _previewHeight; // Set height
            _touch.CurrentCamera.OnImageCaptured += new EventHandler<CameraEventArgs>(OnImageCaptured); // Set preview callback function
        }

        private void StopCamera()
        {
            button3.Text = "Start Webcam";
            if (_touch.CurrentCamera != null)
            {
                _touch.CurrentCamera.OnImageCaptured -= new EventHandler<CameraEventArgs>(OnImageCaptured);
            }
        }

        private void OnImageCaptured(object sender, CameraEventArgs args)
        {
            // Get the bitmap
            Bitmap bitmap = args.Image;

            // Read barcode and show results in UI thread
            this.Invoke((MethodInvoker)delegate
            {
                pictureBox1.Image = bitmap;
                ReadBarcode(bitmap);
            });
        }

        private void ReadBarcode(Bitmap bitmap)
        {
            // Read barcodes with Dynamsoft Barcode Reader
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            BarcodeResult[] results = _barcodeReader.DecodeBitmap(bitmap);
            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds + "ms");

            // Clear previous results
            textBox1.Clear();

            if (results == null)
            {
                textBox1.Text = "No barcode detected!";
                return;
            }

            // Display barcode results
            foreach (BarcodeResult result in results)
            {
                textBox1.AppendText(result.BarcodeText + "\n");
                textBox1.AppendText("\n");
            }
        }
    }
}
