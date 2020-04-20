using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPaint
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Point tempPoint; ///punkt pomocniczy
        Pen myPen; 
        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = "Grafika BMP|*.bmp|Grafika PNG|*.png|Grafika JPG|*.jpg";
            saveFileDialog.Filter = "Grafika BMP|*.bmp|Grafika PNG|*.png|Grafika JPG|*.jpg";
            myPen = new Pen(Color.Red, 5); ///inicjalizacja Pena
            myPen.EndCap = myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK) ///sprawdziamy rezultat,został znalieziony poprawny plik
            {
                pictureBoxMyImage.Image = Image.FromFile(openFileDialog.FileName); ///nazwa naszego pliku 
                graphics = Graphics.FromImage(pictureBoxMyImage.Image); ///z naszego obrazka tworzymy grafikę
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) ///sprawdziamy rezultat,został znalieziony poprawny plik
            {
                string extension = Path.GetExtension(saveFileDialog.FileName); ///format pliku
                ImageFormat imageFormat = ImageFormat.Bmp;
                switch (extension)
                {
                    case ".bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;

                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;

                    case ".jpg":
                        imageFormat = ImageFormat.Jpeg;
                        break;
                }
                pictureBoxMyImage.Image.Save(saveFileDialog.FileName, imageFormat);
            }
        }

        private void pictureBoxMyImage_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                //graphics.DrawEllipse(new Pen(Color.Red), e.X,e.Y,20,20);///w mejsce gzie kliknemy będzie narysowany ellipse
                //pictureBoxMyImage.Refresh(); ///odresowanie image  
                tempPoint = e.Location; ///zapamientujemy punkt
            }
        }

        private void pictureBoxMyImage_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                graphics.DrawLine(myPen, tempPoint, e.Location);
                pictureBoxMyImage.Refresh();
            }
            tempPoint = e.Location;
           
        }

        private void pictureBoxMyImage_MouseUp(object sender, MouseEventArgs e)
        {
           /*( if (e.Button == MouseButtons.Left)
            {
                //graphics.DrawLine(new Pen(Color.Red, 5), tempPoint, e.Location);
                pictureBoxMyImage.Refresh();
            }*/
        }
    }
}
