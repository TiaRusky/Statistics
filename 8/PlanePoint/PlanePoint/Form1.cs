using System.Drawing;

namespace PlanePoint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int n = 100000;                                       //NUmber of trials
            double maxRadius = (double)numericUpDown1.Value;
            Dictionary<int, int> XDistribution = new Dictionary<int, int>();
            Dictionary<int, int> YDistribution = new Dictionary<int, int>();

            double radius;              //RV radius
            double gamma;               //RV angle
            int x;
            int y;

            double virtualX;
            double virtualY;

            Bitmap b3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            Graphics g3 = Graphics.FromImage(b3);


            for(int i = 0; i < n; i++)
            {
                radius = rand.NextDouble() * maxRadius;
                gamma = rand.NextDouble() * 360;

                //x = r cos(gamma) y = r sen(gamma)
                //System.Diagnostics.Debug.WriteLine("r: " + radius + " g: " + gamma);
                x = (int)(radius * Math.Cos(gamma));
                y = (int)(radius * Math.Sin(gamma));

                if (!XDistribution.ContainsKey(x)) XDistribution[x] = 1;
                else XDistribution[x]++;

                if (!YDistribution.ContainsKey(y)) YDistribution[y] = 1;
                else YDistribution[y]++;

                virtualX = fromRealToVirtualX(x, (double)(-radius), (double)(radius), pictureBox3.Width);
                virtualY = fromRealToVirtualY(y, (double)(-radius), (double)(radius), pictureBox3.Height);

                g3.FillRectangle((Brush)Brushes.Black, (int)virtualX, (int)virtualY, 1, 1);
            }


            Bitmap bitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g1 = Graphics.FromImage(bitmap1);

            Bitmap bitmap2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics g2 = Graphics.FromImage(bitmap2);

            drawVerticalChart(bitmap1, g1, pictureBox1, XDistribution, n);
            drawVerticalChart(bitmap2, g2, pictureBox2, YDistribution, n);

            pictureBox1.Image = bitmap1;
            pictureBox2.Image = bitmap2;
            pictureBox3.Image = b3;

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private double fromRealToVirtualY(double y, double minY, double maxY, double height)
        {
            return height - ((y - minY) / (maxY - minY)) * height;
        }

        private double fromRealToVirtualX(double x, double minX, double maxX, double width)
        {
            return (x - minX) / (maxX - minX) * width;
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}