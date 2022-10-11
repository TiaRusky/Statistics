namespace RandomTimerC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int[] counter = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        Random rnd = new Random();      //Oggetto Random per generare numeri casuali




        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Double value = rnd.NextDouble();

            if (value == 1.0)
            {
                counter[9] += 1;
            }
            else
            {
                counter[(int)(value * 10)] += 1;
            }

            foreach(int counter in counter)
            {
                richTextBox1.AppendText(counter.ToString() + " ");
            }

            richTextBox1.AppendText("\n");         
        }
    }
}