using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using TwainDotNet;
using TwainDotNet.TwainNative;
using TwainDotNet.WinFroms;

namespace Avs.Server
{
    public class Scaner : Form
    {
        Server server =null;
        public Twain _twain;
        private Label label1;
        private Button selectSource;
        ScanSettings _settings;
        private CheckBox useUICheckBox;
        private PictureBox pictureBox1;
        private Label label2;
        private Button btstop;
        private Button btstart;
        public string base64 = string.Empty;
        private NotifyIcon trayIcon;
        private System.ComponentModel.IContainer components;
        private NumericUpDown quality;
        private CheckBox imgcrop;
        private Label label3;
        private GroupBox groupBox1;
        private static AreaSettings AreaSettings = new AreaSettings(Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scaner));
            this.label1 = new System.Windows.Forms.Label();
            this.selectSource = new System.Windows.Forms.Button();
            this.useUICheckBox = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btstop = new System.Windows.Forms.Button();
            this.btstart = new System.Windows.Forms.Button();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.quality = new System.Windows.Forms.NumericUpDown();
            this.imgcrop = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quality)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = ".........";
            // 
            // selectSource
            // 
            this.selectSource.BackColor = System.Drawing.Color.FloralWhite;
            this.selectSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectSource.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.selectSource.Location = new System.Drawing.Point(9, 135);
            this.selectSource.Name = "selectSource";
            this.selectSource.Size = new System.Drawing.Size(203, 28);
            this.selectSource.TabIndex = 2;
            this.selectSource.Text = "Select Default Scaner";
            this.selectSource.UseVisualStyleBackColor = false;
            this.selectSource.Click += new System.EventHandler(this.selectSource_Click_1);
            // 
            // useUICheckBox
            // 
            this.useUICheckBox.AutoSize = true;
            this.useUICheckBox.BackColor = System.Drawing.Color.Cornsilk;
            this.useUICheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useUICheckBox.ForeColor = System.Drawing.Color.Black;
            this.useUICheckBox.Location = new System.Drawing.Point(6, 35);
            this.useUICheckBox.Name = "useUICheckBox";
            this.useUICheckBox.Size = new System.Drawing.Size(101, 17);
            this.useUICheckBox.TabIndex = 5;
            this.useUICheckBox.Text = "Show Setting";
            this.useUICheckBox.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Avs.Server.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(130, 180);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(88, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Honeydew;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Portable Twain Scanner Server";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btstop
            // 
            this.btstop.BackColor = System.Drawing.Color.Red;
            this.btstop.Enabled = false;
            this.btstop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btstop.ForeColor = System.Drawing.Color.White;
            this.btstop.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btstop.Location = new System.Drawing.Point(112, 105);
            this.btstop.Name = "btstop";
            this.btstop.Size = new System.Drawing.Size(100, 28);
            this.btstop.TabIndex = 2;
            this.btstop.Text = "Stop Server";
            this.btstop.UseVisualStyleBackColor = false;
            this.btstop.Click += new System.EventHandler(this.btstop_Click);
            // 
            // btstart
            // 
            this.btstart.BackColor = System.Drawing.Color.Green;
            this.btstart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btstart.ForeColor = System.Drawing.Color.White;
            this.btstart.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btstart.Location = new System.Drawing.Point(9, 105);
            this.btstart.Name = "btstart";
            this.btstart.Size = new System.Drawing.Size(100, 28);
            this.btstart.TabIndex = 2;
            this.btstart.Text = "Start Server";
            this.btstart.UseVisualStyleBackColor = false;
            this.btstart.Click += new System.EventHandler(this.btstart_Click);
            // 
            // trayIcon
            // 
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Atasoy Simple Web Server";
            this.trayIcon.Visible = true;
            this.trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseDoubleClick);
            // 
            // quality
            // 
            this.quality.Location = new System.Drawing.Point(137, 35);
            this.quality.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.quality.Name = "quality";
            this.quality.Size = new System.Drawing.Size(44, 20);
            this.quality.TabIndex = 9;
            this.quality.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // imgcrop
            // 
            this.imgcrop.AutoSize = true;
            this.imgcrop.Checked = true;
            this.imgcrop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.imgcrop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgcrop.Location = new System.Drawing.Point(6, 17);
            this.imgcrop.Name = "imgcrop";
            this.imgcrop.Size = new System.Drawing.Size(90, 17);
            this.imgcrop.TabIndex = 10;
            this.imgcrop.Text = "Crop Image";
            this.imgcrop.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(102, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Scan Quality";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imgcrop);
            this.groupBox1.Controls.Add(this.quality);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.useUICheckBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 60);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // Scaner
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(220, 210);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btstart);
            this.Controls.Add(this.btstop);
            this.Controls.Add(this.selectSource);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Scaner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Scaner_FormClosed);
            this.Load += new System.EventHandler(this.Scaner_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quality)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
       
        public Scaner()
        {
            InitializeComponent();
            _twain = new Twain(new WinFormsWindowMessageHook(this));
            _twain.TransferImage += delegate(Object sender, TransferImageEventArgs args)
            {
                if (args.Image != null)
                {
                    if (!InvokeRequired) { 
                     this.Invoke(new MethodInvoker(delegate
                     {
                         Bitmap bitmap = new Bitmap(args.Image);
                         if(imgcrop.Checked)
                             bitmap = CropBitmap(args.Image, 0, 0, 800, 700);
                         Bitmap newBitmap = new Bitmap(bitmap);
                         float fdpi =(float)quality.Value;
                         newBitmap.SetResolution(fdpi, fdpi);
                         base64 = Convert_ImageTo_Base64(newBitmap, System.Drawing.Imaging.ImageFormat.Png);
                     }));
                    }
                   
                }
            };
            _twain.ScanningComplete += delegate { Enabled = true; };
            server = new Server(this);
        }

        public Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }

        public string Convert_ImageTo_Base64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                //First Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                //Then Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        private void selectSource_Click(object sender, EventArgs e)
        {
            _twain.SelectSource();
        }
        public string GetBase() {
            while (!Enabled){
                continue;
            }
          return base64;
        }
        public void _StartScan()
        {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate
                {
                    Enabled = false;
                    _settings = new ScanSettings();
                    _settings.UseDocumentFeeder = false;
                    _settings.ShowTwainUI = useUICheckBox.Checked;
                    _settings.ShowProgressIndicatorUI = true;
                    _settings.UseDuplex = false;
                    _settings.Resolution =
                        false
                        ? ResolutionSettings.Fax : ResolutionSettings.ColourPhotocopier;
                    _settings.Area = !false ? null : AreaSettings;
                    _settings.ShouldTransferAllPages = true;
                    _settings.Rotation = new RotationSettings()
                    {
                        AutomaticRotate = false,
                        AutomaticBorderDetection = false
                    };
                    try
                    {
                        _twain.StartScanning(_settings);
                    }
                    catch (TwainException ex)
                    {
                        MessageBox.Show(ex.Message);
                        Enabled = true;
                    }
                }));
            }
        }
        private void Scaner_Load(object sender, System.EventArgs e)
        {
            
        }

        private void selectSource_Click_1(object sender, EventArgs e)
        {
            _twain.SelectSource();
        }

        
        private void btstart_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            if (server.start(ipAddress, Convert.ToInt32(8055), 100))
            {
                label1.Text = "Starting Server.";
                btstart.Enabled = false;
                btstop.Enabled = true;
            }
            else
                MessageBox.Show(this, "Couldn't start the server. Make sure port is not being listened by other servers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
        private void btstop_Click(object sender, EventArgs e)
        {
            btstart.Enabled = true;
            btstop.Enabled = false;
            server.stop();
            label1.Text = "Server Stoped.";
        }
        public string _StartListner()
        {
            _StartScan();
            GetBase();
            return base64;
        }
        
        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            trayIcon.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                trayIcon.Visible = true;
                this.Hide();
            }
        }

        private void Scaner_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
 
    }
}

