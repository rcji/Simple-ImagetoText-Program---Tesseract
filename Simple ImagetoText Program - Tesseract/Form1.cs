using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using Tesseract.Interop; 

namespace Simple_ImagetoText_Program___Tesseract
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        
        private void selectImageButton_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = new Bitmap(ofd.FileName);
            }

            // make sure to change the path to the tessdata folder to your own path
            //you need to download the tessdata folder from the tesseract github page "https://github.com/tesseract-ocr/tessdata"
            try
            {
                using (var engine = new TesseractEngine(@"C:\Users\Predator\Source\Repos\tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(ofd.FileName))
                    {
                        using (var page = engine.Process(img))
                        {

                            var text = page.GetText();

                            textBox.Text = text;
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            addressTextBox.Text = ofd.FileName;
            fileNameTextBox.Text = ofd.SafeFileName;
            copyTextButton.Text = "Copy Text to Clipboard";
        }

        private void copyTextButton_Click(object sender, EventArgs e)
        {
            try
            {
                copyTextButton.Text = "Copied!";
                Clipboard.SetText(textBox.Text);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

        
    }

