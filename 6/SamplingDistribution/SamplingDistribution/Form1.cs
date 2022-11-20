using Microsoft.VisualBasic.FileIO;

namespace SamplingDistribution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            Dictionary<string, int> caloriesDistribution = getDistribution();       //Distribution on all population
            List<string[]> dataset = getDataSet();      //List of all row of csv  (needed to get random samples)
            Dictionary<string, int> meanDistribution = new Dictionary<string, int>();
            Dictionary<string, int> varianceDistribution = new Dictionary<string, int>();


            int n = 5;         //Sample size
            int m = 1000;        //Number of reps
            Random rnd = new Random();

            for (int i = 0; i < m; i++)
            {
                List<string[]> sample = dataset.OrderBy(x => rnd.Next()).Take(n).ToList();  //Getting the sample of n element
                double mean;            //Mean of this sample
                double variance = 0.0;        //Variance of this sample
                List<int> calories = new List<int>();       //List of value of the RV of this sample


                //The property is the 4-th (index==3)
                foreach(var item in sample)
                {
                    calories.Add(Int32.Parse(item[3]));
                    //System.Diagnostics.Debug.WriteLine(item[3]);
                }

                mean =  calories.Sum()/calories.Count;
                
                foreach(var elem in calories) variance += ((elem-mean)*(elem-mean))/calories.Count;  //Compute the variance
                System.Diagnostics.Debug.WriteLine(variance);

                if (meanDistribution.ContainsKey(((int)mean).ToString())) meanDistribution[((int)mean).ToString()] += 1;
                else meanDistribution.Add(((int)mean).ToString(), 1);

                if(varianceDistribution.ContainsKey(((int)variance).ToString())) varianceDistribution[((int)variance).ToString()] += 1;
                else varianceDistribution.Add(((int)variance).ToString(), 1);

            }

            Bitmap bitmap1 = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            Graphics g1 = Graphics.FromImage(bitmap1);

            Bitmap bitmap2 = new Bitmap(pictureBox2.Width,pictureBox2.Height);
            Graphics g2 =  Graphics.FromImage(bitmap2);

            drawVerticalChart(bitmap1, g1, pictureBox1, meanDistribution, meanDistribution.Values.Max());

            drawVerticalChart(bitmap2, g2, pictureBox2, varianceDistribution, varianceDistribution.Values.Max());

            double allDataSetMean = 0.0;
            foreach(var item in dataset)
            {
                allDataSetMean += Int32.Parse(item[3]);
            }

            allDataSetMean /= dataset.Count;

            //System.Diagnostics.Debug.WriteLine("Population mean: " + allDataSetMean);
            label1.Text = "Mean over all population: " + (int)allDataSetMean;
        }

        private Dictionary<string, int> getDistribution()   //Function to get distribution from cereal.csv
        {
            int numElement = -1;        //Header doesn't count

            Dictionary<string, int> caloriesDistribution = new Dictionary<string, int>();

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

            caloriesDistribution.Remove("calories");    //Remove extra field

            return caloriesDistribution;
        }

        private List<string[]> getDataSet() //Function to get all dataset in a list of string[]
        {
            List<string[]> dataset = new List<string[]>();

            using (TextFieldParser parser = new TextFieldParser(@"cereal.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                int i = 0;

                while (!parser.EndOfData)
                {
                    //Process row
                    
                    string[] fields = parser.ReadFields();
                    if (i != 0) dataset.Add(fields);        //Avoid first line
                    i++;

                }

            }
            return dataset;

        }

        private double fromRealToVirtualX(double x, double minX, double maxX, double width)
        {
            return (x - minX) / (maxX - minX) * width;
        }

        private double fromRealToVirtualY(double y, double minY, double maxY, double height)
        {
            return height - ((y - minY) / (maxY - minY)) * height;
        }

        private void drawVerticalChart(Bitmap b, Graphics g, PictureBox pictureBox, Dictionary<string, int> distr, int numElement)
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
                g.DrawString(item.Key, new Font("Arial", 7), new SolidBrush(Color.Black), j, pictureBox.Height - 20);
                j += step;
            }

            pictureBox.Image = b;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}