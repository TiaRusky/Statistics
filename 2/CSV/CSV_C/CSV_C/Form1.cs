using Microsoft.VisualBasic.FileIO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Xml.Linq;

namespace CSV_C
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double numElement = -1;        //Header doesn't count

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            using (TextFieldParser parser = new TextFieldParser(@"cereal.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                //We want to calculate the distribution (univariate) on calories (int) 
                Dictionary<string, int> caloriesDistribution = new Dictionary<string, int>();

                

                while (!parser.EndOfData)
                {
                    //Process row
                    numElement += 1;
                    string[] fields = parser.ReadFields();
                    int i = 0;      //Counter to know in wich field I am

                    
                    foreach (string field in fields)    
                    {
                        //name; mfr; type; calories; protein; fat; sodium; fiber; carbo; sugars; potass; vitamins; shelf; weight; cups; rating

                        richTextBox1.AppendText(field+ " "); //Stampo a video il csv
                        if (i == 3)  //I'm on the calories' field
                        {
                            
                            if (caloriesDistribution.ContainsKey(field)){
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

                richTextBox1.AppendText("\n\nThe univariate distribution of field 'Calories' is :\n");

                

                foreach(var item in caloriesDistribution)
                {
                    richTextBox1.AppendText("Attribute Value: "+item.Key+ " Relative frequency: "+ (item.Value/numElement) + "\n\n");
                }

                
            }
        }
    }
}