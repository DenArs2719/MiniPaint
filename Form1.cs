﻿using System;
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
        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = "Grafika BMP|*.bmp|Grafika PNG|*.png|Grafika JPG|*.jpg";
            saveFileDialog.Filter = "Grafika BMP|*.bmp|Grafika PNG|*.png|Grafika JPG|*.jpg";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK) ///sprawdziamy rezultat,został znalieziony poprawny plik
            {
                pictureBoxMyImage.Image = Image.FromFile(openFileDialog.FileName); ///nazwa naszego pliku 
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
                Graphics graphics = Graphics.FromImage(pictureBoxMyImage.Image); ///z naszego obrazka tworzymy grafikę
                graphics.DrawEllipse(new Pen(Color.Red), e.X,e.Y,20,20);///w mejsce gzie kliknemy będzie narysowany ellipse
                pictureBoxMyImage.Refresh(); ///odresowanie image  
            }
        }

        private void pictureBoxMyImage_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pictureBoxMyImage_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
