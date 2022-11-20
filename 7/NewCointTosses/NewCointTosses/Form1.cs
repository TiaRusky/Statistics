namespace NewCointTosses
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int trials = 100;   //Number of toss
            int reps = 100; //Number of repetion
            int lambda = (int) numericUpDown1.Value;
            int interrarival = 0;

            Dictionary<int, int> successDistribution = new Dictionary<int, int>();
            Dictionary<int, int> interrarivalDistribution = new Dictionary<int, int>();

            Random rand = new Random();
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graphics = Graphics.FromImage(bitmap);


            Bitmap bitmap2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics g2 = Graphics.FromImage(bitmap2);


            Bitmap bitmap3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            Graphics g3 = Graphics.FromImage(bitmap3);

            g2.DrawRectangle(new Pen(Color.Blue, 1), 0, 0, pictureBox2.Width - 1, pictureBox2.Height - 1);
            g3.DrawRectangle(new Pen(Color.Blue, 1), 0, 0, pictureBox3.Width - 1, pictureBox3.Height - 1);

            graphics.DrawRectangle(new Pen(Color.Blue, 1), 0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);

            //System.Diagnostics.Debug.WriteLine("Labmda: " + lambda, "/: " + (double)lambda / (double)trials);

            for (int i = 0; i < reps; i++)
            {
                int success = 0;        //Number of victory (= Head)
                double prvX = 0;

                double prvY = pictureBox1.Height;
                double prvYR = pictureBox1.Height;
                double prvYN = pictureBox1.Height;
                double rfreq = 0.0;       //Relative freq
                double nfreq = 0.0;       //Normalize freq

                for (int j = 0; j < trials; j++)
                {
                    double value = rand.NextDouble();
                    if (value <= (double)lambda / (double)trials)
                    {
                        success++;    //Absolute freq
                        if(interrarival != 0)
                        {
                            if (!interrarivalDistribution.ContainsKey(interrarival)) interrarivalDistribution.Add(interrarival, 1);
                            else interrarivalDistribution[interrarival]++;
                            interrarival = 0;
                        }
                        
                    }

                    else interrarival++;

                    rfreq = (success * trials) / (j + 1);
                    nfreq = (success * Math.Sqrt(trials)) / Math.Sqrt(j + 1);

                    graphics.DrawLine(new Pen(Color.Blue, 2), Convert.ToInt32(prvX), Convert.ToInt32(prvY), Convert.ToInt32(fromRealToVirtualX(j, 0, trials, pictureBox1.Width)), Convert.ToInt32(fromRealToVirtualY(success, 0, trials, pictureBox1.Height)));
                    graphics.DrawLine(new Pen(Color.Red, 2), Convert.ToInt32(prvX), Convert.ToInt32(prvYR), Convert.ToInt32(fromRealToVirtualX(j, 0, trials, pictureBox1.Width)), Convert.ToInt32(fromRealToVirtualY(rfreq, 0, trials, pictureBox1.Height)));
                    graphics.DrawLine(new Pen(Color.Green, 2), Convert.ToInt32(prvX), Convert.ToInt32(prvYN), Convert.ToInt32(fromRealToVirtualX(j, 0, trials, pictureBox1.Width)), Convert.ToInt32(fromRealToVirtualY(nfreq, 0, trials, pictureBox1.Height)));



                    prvX = fromRealToVirtualX(j, 0, trials, pictureBox1.Width);
                    prvY = fromRealToVirtualY(success, 0, trials, pictureBox1.Height);
                    prvYR = fromRealToVirtualY(rfreq, 0, trials, pictureBox1.Height);
                    prvYN = fromRealToVirtualY(nfreq, 0, trials, pictureBox1.Height);


                }

                if (!successDistribution.ContainsKey(success))
                {
                    successDistribution.Add(success, 1);
                }
                else
                {
                    successDistribution[success]++;
                }

            }

            pictureBox1.Image = bitmap;

            drawHorizontalChart(bitmap2, g2, pictureBox2, successDistribution, trials);
            pictureBox2.Image = bitmap2;


            drawVerticalChart(bitmap3, g3, pictureBox3, interrarivalDistribution, trials);
            pictureBox3.Image = bitmap3;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private double fromRealToVirtualX(double x, double minX, double maxX, double width)
        {
            return (x - minX) / (maxX - minX) * width;
        }

        private double fromRealToVirtualY(double y, double minY, double maxY, double height)
        {
            return height - ((y - minY) / (maxY - minY)) * height;
        }

        private void drawHorizontalChart(Bitmap b, Graphics g, PictureBox pictureBox, Dictionary<int, int> distr, int numElement)
        {
            int j = 0;
            int step = pictureBox.Height / distr.Count;
            g.DrawRectangle(new Pen(Color.Blue), 0, 0, pictureBox.Width - 1, pictureBox.Height - 1);

            var sorted = distr.OrderBy(x => x.Key);

            foreach (var item in sorted)
            {
                double virtualX = fromRealToVirtualX(item.Value, 0, numElement, pictureBox.Width);
                g.DrawRectangle(new Pen(Color.Red), 0, j, (int)virtualX, step);
                j += step;
            }

            pictureBox.Image = b;
        }


        private void drawVerticalChart(Bitmap b, Graphics g, PictureBox pictureBox, Dictionary<int, int> distr, int numElement)
        {
            //Drawing the data
            int j = 0;
            int step = pictureBox.Width / distr.Count;

            g.DrawRectangle(new Pen(Color.Blue), 0, 0, pictureBox.Width - 1, pictureBox.Height - 1);

            var sorted = distr.OrderBy(x => x.Key);

            foreach (var item in sorted)
            {
                double virtualX = fromRealToVirtualX(item.Value, 0, numElement, pictureBox.Height);
                g.DrawRectangle(new Pen(Color.Red), j + 1, pictureBox.Height - (int)virtualX - 1, step, (int)virtualX);
                //g.DrawString(item.Key, new Font("Arial", 8), new SolidBrush(Color.Black), j, pictureBox.Height - 20);
                j += step;
            }

            pictureBox.Image = b;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}