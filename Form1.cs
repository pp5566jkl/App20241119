namespace App20241119
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "圖像文件(JPeg, Gif, Bmp, etc.)|.jpg;*jpeg;*.gif;*.bmp;*.tif;*.tiff;*.png|所有文件(*.*)|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Bitmap MyBitmap = new Bitmap(openFileDialog1.FileName);
                    this.pictureBox1.Image = MyBitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息顯示");
            }

            try
            {
                int Height = this.pictureBox1.Image.Height;
                int Width = this.pictureBox1.Image.Width;
                Bitmap newBitmap = new Bitmap(Width, Height);
                Bitmap oldBitmap = (Bitmap)this.pictureBox1.Image;
                Color pixel;
                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                    {
                        pixel = oldBitmap.GetPixel(x, y);
                        int r, g, b, Result = 0;
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        Result = (299 * r + 587 * g + 114 * b) / 1000;
                        newBitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                    }
                this.pictureBox1.Image = newBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息顯示");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // 確保 TextBox 有有效的門檻值
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("請輸入門檻值！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 嘗試將 TextBox 中的門檻值轉換為浮點數
                float A;
                if (!float.TryParse(textBox1.Text, out A) || A < 0 || A > 255)
                {
                    MessageBox.Show("請輸入有效的門檻值（0-255）！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int Height = this.pictureBox1.Image.Height;
                int Width = this.pictureBox1.Image.Width;
                Bitmap oldBitmap = (Bitmap)this.pictureBox1.Image;

                // 四方向高增幅濾波器遮罩
                float[,] mask4 = { { 0, -1, 0 }, { -1, A + 4, -1 }, { 0, -1, 0 } };

                Bitmap newBitmap = new Bitmap(Width, Height);

                // 使用四方向濾波器進行濾波
                for (int x = 1; x < Width - 1; x++)
                {
                    for (int y = 1; y < Height - 1; y++)
                    {
                        float r = 0, g = 0, b = 0;

                        // 四方向高增幅濾波
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                Color pixel = oldBitmap.GetPixel(x + i, y + j);
                                float maskValue = mask4[i + 1, j + 1];

                                r += pixel.R * maskValue;
                                g += pixel.G * maskValue;
                                b += pixel.B * maskValue;
                            }
                        }

                        r = Math.Min(Math.Max(r, 0), 255);
                        g = Math.Min(Math.Max(g, 0), 255);
                        b = Math.Min(Math.Max(b, 0), 255);

                        newBitmap.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                    }
                }

                // 顯示四方向高增幅濾波結果
                this.pictureBox2.Image = newBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息顯示");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // 確保 TextBox 有有效的門檻值
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("請輸入門檻值！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 嘗試將 TextBox 中的門檻值轉換為浮點數
                float A;
                if (!float.TryParse(textBox1.Text, out A) || A < 0 || A > 255)
                {
                    MessageBox.Show("請輸入有效的門檻值（0-255）！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int Height = this.pictureBox1.Image.Height;
                int Width = this.pictureBox1.Image.Width;
                Bitmap oldBitmap = (Bitmap)this.pictureBox1.Image;

                // 八方向高增幅濾波器遮罩
                float[,] mask8 = { { -1, -1, -1 }, { -1, A + 8, -1 }, { -1, -1, -1 } };

                Bitmap newBitmap = new Bitmap(Width, Height);

                // 使用八方向濾波器進行濾波
                for (int x = 1; x < Width - 1; x++)
                {
                    for (int y = 1; y < Height - 1; y++)
                    {
                        float r = 0, g = 0, b = 0;

                        // 八方向高增幅濾波
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                Color pixel = oldBitmap.GetPixel(x + i, y + j);
                                float maskValue = mask8[i + 1, j + 1];

                                r += pixel.R * maskValue;
                                g += pixel.G * maskValue;
                                b += pixel.B * maskValue;
                            }
                        }

                        r = Math.Min(Math.Max(r, 0), 255);
                        g = Math.Min(Math.Max(g, 0), 255);
                        b = Math.Min(Math.Max(b, 0), 255);

                        newBitmap.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                    }
                }

                // 顯示八方向高增幅濾波結果
                this.pictureBox3.Image = newBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息顯示");
            }
        }
    }
}