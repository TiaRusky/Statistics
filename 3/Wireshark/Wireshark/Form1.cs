using Microsoft.VisualBasic.FileIO;

namespace Wireshark
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        double numElement = -1;        //Header doesn't count

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            using (TextFieldParser parser = new TextFieldParser(@"AllAddressStatistics.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                //We want to calculate the distribution (univariate) on rate(ms) (int) 
                Dictionary<string, int> rateDistribution = new Dictionary<string, int>();



                while (!parser.EndOfData)
                {
                    //Process row
                    numElement += 1;
                    string[] fields = parser.ReadFields();
                    int i = 0;      //Counter to know in wich field I am


                    foreach (string field in fields)
                    {

                        richTextBox1.AppendText(field + " "); //Stampo a video il csv
                        if (i == 8)  //I'm on the rate's field
                        {

                            if (rateDistribution.ContainsKey(field))
                            {
                                rateDistribution[field] += 1;
                            }
                            else
                            {
                                rateDistribution.Add(field, 1);
                            }
                        }

                        i += 1;

                    }


                }

                richTextBox1.AppendText("\n\nThe univariate distribution of field 'Rate (ms)' is :\n");



                foreach (var item in rateDistribution)
                {
                    richTextBox1.AppendText("Attribute Value: " + item.Key + " Relative frequency: " + (item.Value / numElement) + "\n\n");
                }
            }
        }
    }
}