using Microsoft.VisualBasic.FileIO;
using System.Data.Common;
using System.Windows.Forms;

namespace ColumnChart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int numElement = -1;        //Header doesn't count
            button1.Enabled = false;

            //We want to calculate the distribution (univariate) on calories (int) 
            Dictionary<string, int> caloriesDistribution = new Dictionary<string, int>();

            //Getting data in a dictionary
            using (TextFieldParser parser = new TextFieldParser(@"cereal.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                while (!parser.EndOfData)
                {
                    //Process row
                    numElement += 1;
                    string[] fields = parser.ReadFields();
                    int i = 0;      //Counter to know in wich field I am


                    foreach (string field in fields)
                    {
                        //name; mfr; type; calories; protein; fat; sodium; fiber; carbo; sugars; potass; vitamins; shelf; weight; cups; rating

                        if (i == 3)  //I'm on the calories' field
                        {

                            if (caloriesDistribution.ContainsKey(field))
                            {
                                caloriesDistribution[field] += 1;
                            }
                            else
                            {
                                caloriesDistribution.Add(field, 1);
                            }
                        }
                        i += 1;
                    }
                }

            }

            caloriesDistribution.Remove("calories");

            Bitmap bitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g1 = Graphics.FromImage(bitmap1);

            Bitmap bitmap2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics g2 = Graphics.FromImage(bitmap2);

            
            drawVerticalChart(bitmap1, g1, pictureBox1, caloriesDistribution, numElement);
            drawHorizontalChart(bitmap2, g2, pictureBox2, caloriesDistribution, numElement);

        }

        private double fromRealToVirtualX(double x, double minX, double maxX, double width)
        {
            return (x - minX) / (maxX - minX) * width;
        }

        private double fromRealToVirtualY(double y, double minY, double maxY, double height)
        {
            return height - ((y - minY) / (maxY - minY)) * height;
        }

        private void drawVerticalChart(Bitmap b,Graphics g,PictureBox pictureBox, Dictionary<string, int> distr,int numElement)
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
                g.DrawString(item.Key, new Font("Arial", 8), new SolidBrush(Color.Black), j, pictureBox.Height - 20);
                j += step;
            }

            pictureBox.Image = b;
        }

        private void drawHorizontalChart(Bitmap b, Graphics g, PictureBox pictureBox, Dictionary<string, int> distr, int numElement)
        {
            int j = 0;
            int step = pictureBox.Height / distr.Count;
            g.DrawRectangle(new Pen(Color.Blue), 0, 0, pictureBox.Width - 1, pictureBox.Height - 1);

            var sorted = distr.OrderBy(x => x.Key);

            foreach (var item in sorted)
            {
                double virtualX = fromRealToVirtualX(item.Value,0, numElement, pictureBox.Width);
                g.DrawRectangle(new Pen(Color.Red), 0, j, (int) virtualX, step);
                j += step;
            }

            pictureBox.Image = b;
        }

    }

}