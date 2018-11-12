using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Lzw;
using ICSharpCode.SharpZipLib.Core;

namespace CT_Vaja1
{
    public partial class Form1 : Form
    {
        struct RGB
        {
            public byte red;
            public byte green;
            public byte blue;
        };
        
        RGB[] LUT = new RGB[256];
        String file_name;
        String lut_file_name;
        int width, height;
        short[,] matrix;
        Image<Gray, short> imageGrayscale;
        String lzwInputfile;

        List<short> list;
        Dictionary<string, short> map;

        public Form1()
        {
            InitializeComponent();

            txtHeight.Text = "512";
            txtWidth.Text = "512";
            txtWidth.Focus();
        }

        private void btn_uploadImg_Click(object sender, EventArgs e)
        {
            FileDialog fd = new OpenFileDialog
            {
                Filter = "IMG files (*.img)|*.img",
                Title = "Select image"
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                file_name = fd.FileName;
                displayImage(file_name);
            }
        }

        // Overload function for image in color
        private void displayImage(string file_name, String lut_file_name)
        {
            RGB rgb;
            // default image 512x512 bits
            width = txtWidth.Text != "" ? Convert.ToInt32(txtWidth.Text) : 512;
            height = txtHeight.Text != "" ? Convert.ToInt32(txtHeight.Text) : 512;
            Image<Rgb, byte> image = new Image<Rgb, byte>(width, height);
            matrix = new short[width, height];

            int offsetData = 0, offsetColors = 0;
            short x = 0;
            int color = 0;

            // open IMG file for reading
            using (BinaryReader img = new BinaryReader(File.Open(file_name, FileMode.Open)))
            {
                // open LUT file for reading
                using (BinaryReader colors = new BinaryReader(File.Open(lut_file_name, FileMode.Open)))
                {

                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            img.BaseStream.Seek(offsetData, SeekOrigin.Begin);
                            // get values in range -2048, 2048
                            x = img.ReadInt16();
                            // matrix for later compression
                            matrix[i, j] = x;

                            offsetData += sizeof(short);
                            // get values between 0 and 255
                            color = (int)(((double)(x + 2048) / 4095) * 255);

                            // offset of in the file is color number (0-255) multiplied by 3 bytes for the color (rgb)
                            offsetColors = color * 3;
                            // jump to the offset byte in the file
                            colors.BaseStream.Seek(offsetColors, SeekOrigin.Begin);

                            // read 3 bytes starting from the offset in the file
                            rgb.red = colors.ReadByte();
                            rgb.green = colors.ReadByte();
                            rgb.blue = colors.ReadByte();


                            // put rgb color at i,j position
                            image[i, j] = new Rgb(rgb.red, rgb.green, rgb.blue);
                        }
                    }
                }
            }

            // add EmguCV image to the windows forms image box
            imageBox.Image = image;
        }

        private void displayImage(String file_name)
        {
            // default image 512x512 bits
            width = txtWidth.Text != "" ? Convert.ToInt32(txtWidth.Text) : 512;
            height = txtHeight.Text != "" ? Convert.ToInt32(txtHeight.Text) : 512;
            imageGrayscale = new Image<Gray, short>(width, height);
            matrix = new short[width, height];

            int offsetData = 0;
            short x = 0;

            // open IMG file for reading
            using (BinaryReader img = new BinaryReader(File.Open(file_name, FileMode.Open)))
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        img.BaseStream.Seek(offsetData, SeekOrigin.Begin);
                        // get values in range -2048, 2048
                        x = img.ReadInt16();
                        offsetData += sizeof(short);

                        // put rgb color at i,j position
                        imageGrayscale[i, j] = new Gray(x);
                    }
                }
            }

            // add EmguCV image to the windows forms image box
            imageBox.Image = imageGrayscale;
        }

        private void btn_uploadLut_Click(object sender, EventArgs e)
        {
            FileDialog fd = new OpenFileDialog
            {
                Filter = "LUT files (*.lut)|*.lut",
                Title = "Select lookup table"
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                lut_file_name = fd.FileName;
                displayImage(file_name, lut_file_name);
            }
        }

        private void btn_lzwCompress_Click(object sender, EventArgs e)
        {
            Algorithms.LZW lzw = new Algorithms.LZW();
            lzw.Compress(lzwInputfile, "lzw_output.bin", 256);
        }

        private void btn_compress_Click(object sender, EventArgs e)
        {
            int halfHeight = height / 2;
            int halfWidth = width / 2;
            list = new List<short>();
            map = new Dictionary<string, short>();

            // I quadrant
            checkNeighbors(0, 0, halfHeight, halfWidth, false);

            // II quadrant
            checkNeighbors(0, halfHeight, halfHeight, width, false);

            // III quadrant
            checkNeighbors(halfWidth, 0, height, halfWidth, false);

            // IV quadrant
            checkNeighbors(halfWidth, halfHeight, width, height, false);

            using (BinaryWriter bw = new BinaryWriter(File.OpenWrite("compressed.bin")))
            {
                foreach(short item in list)
                {
                    bw.Write(item);
                }
            }
        }

        private void checkNeighbors(int z, int w, int width, int height, bool recursive)
        {
            // color of the first pixel of the quadrant
            short color = imageGrayscale.Data[z, w, 0];
            
            for (int i = z; i < width - 1; i++)
            {
                for (int j = w; j < height - 1; j++)
                {
                    // pixel and its three neighbors: east, south, and southeast of it
                    short x = imageGrayscale.Data[i, j, 0];
                    short x1 = imageGrayscale.Data[i, j + 1, 0];
                    short x2 = imageGrayscale.Data[i + 1, j, 0];
                    short x3 = imageGrayscale.Data[i + 1, j + 1, 0];

                    // checking if some of the neighbor pixels are different, add 1 bit and recursive execute this function
                    if (x != x1 || x != x2 || x != x3)
                    {
                        list.Add(1);
                        checkNeighbors(z, w, (width / 2) - 1, (height / 2) - 1, true);
                    }
                }
            }

            list.Add(0);
            list.Add(color);
        }
    }
}
