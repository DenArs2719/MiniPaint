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
        SolidBrush solidBrush;  ///fill dla figury
        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = "Grafika BMP|*.bmp|Grafika PNG|*.png|Grafika JPG|*.jpg";
            saveFileDialog.Filter = "Grafika BMP|*.bmp|Grafika PNG|*.png|Grafika JPG|*.jpg";
            myPen = new Pen(buttonColor.BackColor, (float)numericUpDownWidth.Value); ///inicjalizacja Pena
            myPen.EndCap = myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            solidBrush = new SolidBrush(Color.Black);
            newToolStripMenuItem_Click(null, null);
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxMyImage.Image = new Bitmap(800,600); 
            graphics = Graphics.FromImage(pictureBoxMyImage.Image);
            graphics.Clear(Color.White);
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
                tempPoint = e.Location; ///zapamientujemy punkt
            }
        }

        private void pictureBoxMyImage_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if(radioButtonCurve.Checked)
                {
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                    pictureBoxMyImage.Refresh();
                    tempPoint = e.Location;
                }
            }
            
           
        }

        private void pictureBoxMyImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (radioButtonCurve.Checked)
                {
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                }
                else if(radioButtonLine.Checked)
                {
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                }
                else if (radioButtonRectangle.Checked)
                {
                    graphics.DrawRectangle(myPen,Math.Min(tempPoint.X,e.X),Math.Min(tempPoint.Y,e.Y),Math.Abs(tempPoint.X - e.X),Math.Abs(tempPoint.Y - e.Y));
                    graphics.FillRectangle(solidBrush, Math.Min(tempPoint.X, e.X), Math.Min(tempPoint.Y, e.Y), Math.Abs(tempPoint.X - e.X), Math.Abs(tempPoint.Y - e.Y));
                }
                else if (radioButtonEllipse.Checked)
                {
                    graphics.DrawEllipse(myPen, Math.Min(tempPoint.X, e.X), Math.Min(tempPoint.Y, e.Y), Math.Abs(tempPoint.X - e.X), Math.Abs(tempPoint.Y - e.Y));
                    graphics.FillEllipse(solidBrush, Math.Min(tempPoint.X, e.X), Math.Min(tempPoint.Y, e.Y), Math.Abs(tempPoint.X - e.X), Math.Abs(tempPoint.Y - e.Y));
                }
                pictureBoxMyImage.Refresh();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            myPen.Width = (float)numericUpDownWidth.Value;
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                buttonColor.BackColor = colorDialog.Color;
                myPen.Color = colorDialog.Color;
            }
        }

        private void buttonbuttonColorKsztalt_Click(object sender, EventArgs e)
        {
            if (colorDialogKsztalt.ShowDialog() == DialogResult.OK)
            {
                buttonColorKsztalt.BackColor = colorDialogKsztalt.Color;
                solidBrush.Color = colorDialogKsztalt.Color;
            }
        }
    }
}
