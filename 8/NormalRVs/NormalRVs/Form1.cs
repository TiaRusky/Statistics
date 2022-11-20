namespace NormalRVs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = 1000;
            int mean = (int)numericUpDown1.Value;
            int stdDev = (int)numericUpDown2.Value;

            Dictionary<int, int> xDistr = new Dictionary<int, int>();
            Dictionary<int, int> x2Distr = new Dictionary<int, int>();
            Dictionary<int, int> xyDistr = new Dictionary<int, int>();
            Dictionary<int, int> xDy2Distr = new Dictionary<int, int>();
            Dictionary<int, int> x2Dy2Distr = new Dictionary<int, int>();


            Random rand = new Random();

            Bitmap bitmapX = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g1 = Graphics.FromImage(bitmapX);

            Bitmap bitmapX2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics g2 = Graphics.FromImage(bitmapX2);

            Bitmap bitmapXY = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            Graphics g3 = Graphics.FromImage(bitmapXY);

            Bitmap bitmapXDY2 = new Bitmap(pictureBox4.Width, pictureBox4.Height);
            Graphics g4 = Graphics.FromImage(bitmapXDY2);

            Bitmap bitmapX2DY2 = new Bitmap(pictureBox5.Width, pictureBox5.Height);
            Graphics g5 = Graphics.FromImage(bitmapX2DY2);


            for (int i = 0; i < n; i++)
            {
                //X rand Normal
                double u1 = 1.0 - rand.NextDouble();
                double u2 = 1.0 - rand.NextDouble();

                double XrandStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                double XrandNormal = mean + stdDev * XrandStdNormal;                                   //random normal(mean,stdDev^2)

                //Y rand Normal
                double u3 = 1.0 - rand.NextDouble();
                double u4 = 1.0 - rand.NextDouble();

                double YrandStdNormal = Math.Sqrt(-2.0 * Math.Log(u3)) * Math.Sin(2.0 * Math.PI * u4);
                double YrandNormal = mean + stdDev * YrandStdNormal;

                //X^2
                double X2randNormal = XrandNormal * XrandNormal;

                //X*Y
                double xyrandNormal = YrandNormal * XrandNormal;

                //X/Y^2
                double y2randNormal = YrandNormal * YrandNormal;
                double xDy2randNormal = XrandNormal / y2randNormal;

                //X2/Y2
                double x2y2randNormal = X2randNormal / y2randNormal;


                if (!xDistr.ContainsKey((int)XrandNormal)) xDistr[(int)XrandNormal] = 1;
                else xDistr[(int)XrandNormal]++;

                if (!x2Distr.ContainsKey((int)X2randNormal)) x2Distr[(int)X2randNormal] = 1;
                else x2Distr[(int)X2randNormal]++;

                if(!xyDistr.ContainsKey((int)xyrandNormal)) xyDistr[(int)xyrandNormal] = 1; 
                else xyDistr[(int)xyrandNormal]++;

                if (!xDy2Distr.ContainsKey((int)xDy2randNormal)) xDy2Distr[(int)xDy2randNormal] = 1;
                else xDy2Distr[(int)xDy2randNormal]++;


                if (!x2Dy2Distr.ContainsKey((int)x2y2randNormal)) x2Dy2Distr[(int)x2y2randNormal] = 1;
                else x2Dy2Distr[(int)x2y2randNormal]++;

            }

            drawVerticalChart(bitmapX, g1, pictureBox1, xDistr, xDistr.Values.Max()+50);
            drawVerticalChart(bitmapX2, g2, pictureBox2, x2Distr, x2Distr.Values.Max() + 50);
            drawVerticalChart(bitmapXY,g3,pictureBox3,xyDistr,xyDistr.Values.Max() + 50);
            drawVerticalChart(bitmapXDY2, g4, pictureBox4, xDy2Distr, xDy2Distr.Values.Max() + 50);
            drawVerticalChart(bitmapX2DY2, g5, pictureBox5, x2Dy2Distr, x2Dy2Distr.Values.Max() + 50);


            pictureBox1.Image = bitmapX;
            pictureBox2.Image = bitmapX2;
            pictureBox3.Image = bitmapXY;
            pictureBox4.Image = bitmapXDY2;
            pictureBox5.Image = bitmapX2DY2;
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


        private double fromRealToVirtualX(double x, double minX, double maxX, double width)
        {
            return (x - minX) / (maxX - minX) * width;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}