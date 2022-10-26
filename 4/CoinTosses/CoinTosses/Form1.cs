namespace CoinTosses
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double probSuccess = Decimal.ToDouble(numericUpDown1.Value) / 100;
            int trials = 100;   //Number of toss
            int reps = 100; //Number of repetion

            Random rand = new Random();
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.DrawRectangle(new Pen(Color.Blue, 1), 0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);

            for(int i = 0; i < reps; i++)
            {
                int success = 0;        //Number of victory (= Head)
                double prvX = 0;

                double prvY = pictureBox1.Height;
                double prvYR = pictureBox1.Height;
                double prvYN = pictureBox1.Height;
                double rfreq = 0.0;       //Relative freq
                double nfreq = 0.0;       //Normalize freq

                for(int j = 0; j < trials; j++)
                {
                    double value = rand.NextDouble();
                    if (value <= probSuccess) success++;    //Absolute freq

                    rfreq = (success * trials) / (j+1);
                    nfreq = (success * Math.Sqrt(trials)) / Math.Sqrt(j+1);
                   
                    graphics.DrawLine(new Pen(Color.Blue, 2), Convert.ToInt32(prvX), Convert.ToInt32(prvY), Convert.ToInt32(fromRealToVirtualX(j, 0, trials, pictureBox1.Width)), Convert.ToInt32(fromRealToVirtualY(success, 0, trials, pictureBox1.Height)));
                    graphics.DrawLine(new Pen(Color.Red, 2), Convert.ToInt32(prvX), Convert.ToInt32(prvYR), Convert.ToInt32(fromRealToVirtualX(j, 0, trials, pictureBox1.Width)), Convert.ToInt32(fromRealToVirtualY(rfreq, 0, trials, pictureBox1.Height)));
                    graphics.DrawLine(new Pen(Color.Green, 2), Convert.ToInt32(prvX), Convert.ToInt32(prvYN), Convert.ToInt32(fromRealToVirtualX(j, 0, trials, pictureBox1.Width)), Convert.ToInt32(fromRealToVirtualY(nfreq, 0, trials, pictureBox1.Height)));



                    prvX = fromRealToVirtualX(j, 0, trials, pictureBox1.Width);
                    prvY = fromRealToVirtualY(success, 0, trials, pictureBox1.Height);
                    prvYR = fromRealToVirtualY(rfreq, 0, trials, pictureBox1.Height);
                    prvYN = fromRealToVirtualY(nfreq, 0, trials, pictureBox1.Height);

                    
                }

                if (i == (reps - 1))       //I'm on the last rep
                {

                    Bitmap bAbsolute = new Bitmap(pictureBoxAbsolute.Width, pictureBoxAbsolute.Height);
                    Bitmap bRelative = new Bitmap(pictureBoxRelative.Width, pictureBoxRelative.Height);
                    Bitmap bNormalized = new Bitmap(pictureBoxNormalized.Width, pictureBoxNormalized.Height);

                    Graphics gAbsolute = Graphics.FromImage(bAbsolute);
                    Graphics gRelative = Graphics.FromImage(bRelative);
                    Graphics gNormalized = Graphics.FromImage(bNormalized);

                    //Absolute
                    gAbsolute.FillRectangle(new SolidBrush(Color.Green), 0, 0, 75, Convert.ToInt32((Convert.ToDouble(success) / trials) * pictureBoxAbsolute.Height));
                    gAbsolute.FillRectangle(new SolidBrush(Color.Red), 80, 0, 75, Convert.ToInt32((Convert.ToDouble(trials - success) / trials) * pictureBoxAbsolute.Height));


                    //Relative
                    gRelative.FillRectangle(new SolidBrush(Color.Green), 0, 0, 75, Convert.ToInt32(fromRealToVirtualY(rfreq, 0, trials, pictureBoxRelative.Height)));
                    gRelative.FillRectangle(new SolidBrush(Color.Red), 80, 0, 75, Convert.ToInt32(fromRealToVirtualY((1 * trials) - rfreq, 0, trials, pictureBoxRelative.Height)));


                    //Normalized
                    gNormalized.FillRectangle(new SolidBrush(Color.Green), 0, 0, 75, Convert.ToInt32(fromRealToVirtualY(nfreq, 0, trials, pictureBoxNormalized.Height)));
                    gNormalized.FillRectangle(new SolidBrush(Color.Red), 80, 0, 75, Convert.ToInt32(fromRealToVirtualY((1 * trials) - nfreq, 0, trials, pictureBoxNormalized.Height)));


                    pictureBoxAbsolute.Image = bAbsolute;
                    pictureBoxRelative.Image = bRelative;
                    pictureBoxNormalized.Image = bNormalized;
                }

            }

            pictureBox1.Image = bitmap;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Conversion functs
        private double fromRealToVirtualX(double x, double minX, double maxX, double width)
        {
            return (x - minX) / (maxX - minX) * width;
        }

        private double fromRealToVirtualY(double y, double minY, double maxY, double height)
        {
            return height - ((y - minY) / (maxY - minY)) * height;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxRelative_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}