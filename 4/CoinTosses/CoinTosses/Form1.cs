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
                double rfreq;       //Relative freq
                double nfreq;       //Normalize freq

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
    }
}