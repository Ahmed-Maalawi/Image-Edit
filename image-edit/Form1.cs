using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using openCV;
using System.Drawing.Imaging;
using System.Threading;

namespace Final_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IplImage imageSource1, imageSource2, imageSource_resized1, imageSource_resized2;
        IplImage image1, image2;

        Bitmap bmp, bmp2, outImage; 
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void disabledButtons()
        {
            convertToGrayLevelToolStripMenuItem.Enabled = true;
            getToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            filterToolStripMenuItem.Enabled = true;
        }
        // Open Image 
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFileDialog1.FileName = " ";
            openFileDialog1.Filter = "JPEG|*JPG|Bitmap|*.bmp|All|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                disabledButtons();
                try
                {
                    imageSource1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
                    CvSize size = new CvSize(pictureBox1.Width, pictureBox1.Height);
                    imageSource_resized1 = cvlib.CvCreateImage(size, imageSource1.depth, imageSource1.nChannels);
                    cvlib.CvResize(ref imageSource1, ref imageSource_resized1, cvlib.CV_INTER_LINEAR);
                    pictureBox1.BackgroundImage = (Image)imageSource_resized1;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void convertToGrayLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)imageSource_resized1;
            int width = bmp.Width;
            int height = bmp.Height;
            Color p;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    p = bmp.GetPixel(x, y);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;
                    int avg = (r + g + b) / 3;
                    bmp.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                    pictureBox2.BackgroundImage = (Image)bmp;
                }
            }
        }
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {

            image1 = cvlib.CvCreateImage(new CvSize(imageSource1.width, imageSource1.height), imageSource1.depth, imageSource1.nChannels);
            int srcAdd = imageSource1.imageData.ToInt32();
            int dstAdd = image1.imageData.ToInt32();
            unsafe
            {
                int srcIndex, dstIndex;
                for (int r = 0; r < image1.height; r++)
                    for (int c = 0; c < image1.width; c++)
                    {
                        srcIndex = dstIndex = (image1.width * r * image1.nChannels) + (c * image1.nChannels);
                        *(byte*)(dstAdd + dstIndex + 0) = 0;
                        *(byte*)(dstAdd + dstIndex + 1) = 0;
                        *(byte*)(dstAdd + dstIndex + 2) = *(byte*)(srcAdd + srcIndex + 2);
                    }
            }
            CvSize size = new CvSize(pictureBox3.Width, pictureBox3.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
            cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);
            pictureBox2.BackgroundImage = (Image)resized_image;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image1 = cvlib.CvCreateImage(new CvSize(imageSource1.width, imageSource1.height), imageSource1.depth, imageSource1.nChannels);
            int srcAdd = imageSource1.imageData.ToInt32();
            int dstAdd = image1.imageData.ToInt32();
            unsafe
            {
                int srcIndex, dstIndex;
                for (int r = 0; r < image1.height; r++)
                    for (int c = 0; c < image1.width; c++)
                    {
                        srcIndex = dstIndex = (image1.width * r * image1.nChannels) + (c * image1.nChannels);
                        *(byte*)(dstAdd + dstIndex + 0) = 0;
                        *(byte*)(dstAdd + dstIndex + 1) = *(byte*)(srcAdd + srcIndex + 1);
                        *(byte*)(dstAdd + dstIndex + 2) = 0;
                    }
            }


            CvSize size = new CvSize(pictureBox3.Width, pictureBox3.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
            cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);
            pictureBox2.BackgroundImage = (Image)resized_image;


        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image1 = cvlib.CvCreateImage(new CvSize(imageSource1.width, imageSource1.height), imageSource1.depth, imageSource1.nChannels);
            int srcAdd = imageSource1.imageData.ToInt32();
            int dstAdd = image1.imageData.ToInt32();

            unsafe
            {
                int srcIndex, dstIndex;
                for (int r = 0; r < image1.height; r++)
                    for (int c = 0; c < image1.width; c++)
                    {
                        srcIndex = dstIndex = (image1.width * r * image1.nChannels) + (c * image1.nChannels);
                        *(byte*)(dstAdd + dstIndex + 0) = *(byte*)(srcAdd + srcIndex + 0);
                        *(byte*)(dstAdd + dstIndex + 1) = 0;
                        *(byte*)(dstAdd + dstIndex + 2) = 0;

                    }
            }

            CvSize size = new CvSize(pictureBox3.Width, pictureBox3.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, image1.depth, image1.nChannels);
            cvlib.CvResize(ref image1, ref resized_image, cvlib.CV_INTER_LINEAR);
            pictureBox2.BackgroundImage = (Image)resized_image;
        }

        private void openAnotherImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = " ";
            openFileDialog1.Filter = "JPEG|*JPG|Bitmap|*.bmp|All|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                addTwoImagesToolStripMenuItem.Enabled = true;
                try
                {

                    imageSource2= cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
                    CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
                    imageSource_resized2 = cvlib.CvCreateImage(size, imageSource2.depth, imageSource2.nChannels);
                    cvlib.CvResize(ref imageSource2, ref imageSource_resized2, cvlib.CV_INTER_LINEAR);
         
                    pictureBox4.BackgroundImage = (Image)imageSource_resized2;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
       

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        public static Bitmap AdjustBrightness(Bitmap resized_image, int Value)
        {
            Bitmap TempBitmap = resized_image;   //here I create temporary bitmap variable to store image

            float Finalvalue = (float)Value / 255.0f; // I will take the value that I entered before divided by 255

            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height); //here I create a new bitmap image variable to store the new image

            Graphics NewGraphics = Graphics.FromImage(NewBitmap); // this line I dealed with graphics from new bitmap the blank one

            float[][] FloatColorMatrix = {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] { Finalvalue, Finalvalue, Finalvalue, 1, 1 }
            };

            ColorMatrix ColorMatrix = new ColorMatrix(FloatColorMatrix);
            ImageAttributes Attributes = new ImageAttributes();
            Attributes.SetColorMatrix(ColorMatrix);
            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), 0, 0, TempBitmap.Width, TempBitmap.Height, GraphicsUnit.Pixel, Attributes);
            Attributes.Dispose();
            NewGraphics.Dispose();
            return NewBitmap;
        }

        private void prigtnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bmp2 = (Bitmap)imageSource_resized1;
            pictureBox7.BackgroundImage = AdjustBrightness(bmp2, 100);
        }

        private void blurreddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outImage = (Bitmap)imageSource_resized1;
            int width = outImage.Width;
            int height = outImage.Height;

            for (int h = 10; h <= width - 10; h++)
            {
                for (int w = 10; w <= height - 10; w++)
                {
                    try
                    {
                        Color H1 = outImage.GetPixel(h - 10, w);
                        Color H2 = outImage.GetPixel(h + 10, w);
                        Color W1 = outImage.GetPixel(h, w - 10);
                        Color W2 = outImage.GetPixel(h, w + 10);

                        int avrgeR = (int)((H1.R + H2.R + W1.R + W2.R) / 4);
                        int avrgeG = (int)((H1.G + H2.G + W1.G + W2.G) / 4);
                        int avegeB = (int)((H1.B + H2.B + W1.B + W2.B) / 4);

                        outImage.SetPixel(h, w, Color.FromArgb(avrgeR, avrgeG, avegeB));
                    }
                    catch (Exception) {}
                }
            }
            pictureBox7.BackgroundImage = outImage;
        }

        private void unloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox1.BackgroundImage = null;
            pictureBox2.BackgroundImage = null;
            pictureBox3.BackgroundImage = null;
        }

        private void whiteAndBlackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bit = (Bitmap)imageSource_resized1;
            int width = bit.Width, height = bit.Height;

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Color color = bit.GetPixel(x, y);
                    var ava = (color.R + color.B + color.G) / 3;

                    int temp;
                    if (ava > 128)
                    {
                        temp = 255;
                    }
                    else
                    {
                        temp = 0;
                    }
                    bit.SetPixel(x, y, Color.FromArgb(color.A, temp, temp, temp));
                }

            pictureBox2.BackgroundImage = (Bitmap)bit;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {

            chart1.Series["Red"].Points.Clear();
            chart1.Series["Green"].Points.Clear();
            chart1.Series["Blue"].Points.Clear();

            bmp = (Bitmap)imageSource1;
            int width = bmp.Width;
            int height = bmp.Height;

            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixelColor = bmp.GetPixel(i, j);

                    ni_Red[pixelColor.R]++;
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;

                }
            }


            for (int i = 0; i < 256; i++)
            {
                chart1.Series["Red"].Points.AddY(ni_Red[i]);
                chart1.Series["Green"].Points.AddY(ni_Green[i]);
                chart1.Series["Blue"].Points.AddY(ni_Blue[i]);
            }

        }

        private void histogramEqualizedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            chart2.Series["Red"].Points.Clear();
            chart2.Series["Green"].Points.Clear();
            chart2.Series["Blue"].Points.Clear();



            bmp2 = (Bitmap)imageSource_resized1;
            int width = bmp2.Width;
            int height = bmp2.Height;


            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixelColor = bmp2.GetPixel(i, j);

                    ni_Red[pixelColor.R]++;
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;

                }
            }


            for (int i = 0; i < 256; i++)
            {
                chart2.Series["Red"].Points.AddY(ni_Red[i]);
                chart2.Series["Green"].Points.AddY(ni_Green[i]);
                chart2.Series["Blue"].Points.AddY(ni_Blue[i]);
            }
        }

        private void equalizedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            chart2.Series["Red"].Points.Clear();
            chart2.Series["Green"].Points.Clear();
            chart2.Series["Blue"].Points.Clear();

            bmp2 = (Bitmap)imageSource_resized1;
            int width = bmp2.Width;
            int height = bmp2.Height;


            //******************* Calculate N(i) **************//

            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];





            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixelColor = bmp2.GetPixel(i, j);

                    ni_Red[pixelColor.R]++;
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;
                }
            }

            //******************* Calculate P(Ni) **************//
            decimal[] prob_ni_Red = new decimal[256];
            decimal[] prob_ni_Green = new decimal[256];
            decimal[] prob_ni_Blue = new decimal[256];

            for (int i = 0; i < 256; i++)
            {
                prob_ni_Red[i] = (decimal)ni_Red[i] / (decimal)(width * height);
                prob_ni_Green[i] = (decimal)ni_Green[i] / (decimal)(width * height);
                prob_ni_Blue[i] = (decimal)ni_Blue[i] / (decimal)(width * height);
            }

            //******************* Calculate CDF **************//

            decimal[] cdf_Red = new decimal[256];
            decimal[] cdf_Green = new decimal[256];
            decimal[] cdf_Blue = new decimal[256];

            cdf_Red[0] = prob_ni_Red[0];
            cdf_Green[0] = prob_ni_Green[0];
            cdf_Blue[0] = prob_ni_Blue[0];

            for (int i = 1; i < 256; i++)
            {
                cdf_Red[i] = prob_ni_Red[i] + cdf_Red[i - 1];
                cdf_Green[i] = prob_ni_Green[i] + cdf_Green[i - 1];
                cdf_Blue[i] = prob_ni_Blue[i] + cdf_Blue[i - 1];
            }


            //******************* Calculate CDF(L-1) **************//


            int red, green, blue;
            int constant = 255;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixelColor = bmp2.GetPixel(i, j);

                    red = (int)Math.Round(cdf_Red[pixelColor.R] * constant);
                    green = (int)Math.Round(cdf_Red[pixelColor.G] * constant);
                    blue = (int)Math.Round(cdf_Red[pixelColor.B] * constant);

                    Color newColor = Color.FromArgb(red, green, blue);
                    bmp2.SetPixel(i, j, newColor);

                }
            }

            pictureBox6.Image = (Image)bmp2;
        }


        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outImage = (Bitmap)imageSource_resized1;
            for (int y = 0; (y <= (outImage.Height - 1)); y++)
            {
                for (int x = 0; (x <= (outImage.Width - 1)); x++)
                {
                    Color inv = outImage.GetPixel(x, y);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    outImage.SetPixel(x, y, inv);
                }
            }
            pictureBox7.BackgroundImage = outImage;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void addTwoImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CvSize size = new CvSize(pictureBox3.Width, pictureBox3.Height);
            image2 = cvlib.CvCreateImage(size, imageSource1.depth, imageSource1.nChannels);



            int srcX = imageSource_resized1.imageData.ToInt32();
            int srcY = imageSource_resized2.imageData.ToInt32();
            int dstAddress = image2.imageData.ToInt32();
            unsafe
            {
                for (int r = 0; r < imageSource_resized1.height; r++)
                {
                    for (int c = 0; c < imageSource_resized1.width; c++)
                    {

                        int srcIndexX, srcIndexY, disIndex;
                        srcIndexX = (imageSource_resized1.width * r * imageSource_resized1.nChannels) + (imageSource_resized1.nChannels * c);
                        srcIndexY = (imageSource_resized2.width * r * imageSource_resized2.nChannels) + (imageSource_resized2.nChannels * c);
                        disIndex = (image2.width * r * image2.nChannels) + (image2.nChannels * c);

                        byte* redX = (byte*)(srcX + srcIndexX + 2);
                        byte* greenX = (byte*)(srcX + srcIndexX + 1);
                        byte* blueX = (byte*)(srcX + srcIndexX + 0);

                        byte* redY = (byte*)(srcY + srcIndexY + 2);
                        byte* greenY = (byte*)(srcY + srcIndexY + 1);
                        byte* blueY = (byte*)(srcY + srcIndexY + 0);

                        byte red = (byte)Math.Min(255, (*redX + *redY));
                        byte green = (byte)Math.Min(255, (*greenX + *greenY));
                        byte blue = (byte)Math.Min(255, (*blueX + *blueY));

                        *(byte*)(dstAddress + disIndex + 2) = red;
                        *(byte*)(dstAddress + disIndex + 1) = green;
                        *(byte*)(dstAddress + disIndex + 0) = blue;
                    }

                }
            }
            pictureBox3.BackgroundImage = (Image) image2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
