using System.Windows.Forms;

namespace TestGrafico
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

       

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            Graphics g = Graphics.FromImage(bitmap);

            g.DrawRectangle(new Pen(Color.Blue,1),0,0,pictureBox1.Width-1,pictureBox1.Height-1);
            //g.FillRectangle(new SolidBrush(Color.Blue),10,10,50,100);

            int trialCount = 100;               //Lancio di una moneta 100 volte
            Random rnd = new Random();
            int success = 0;                //Number of victory
            double prvX = 0;
            double prvY = pictureBox1.Height;



            for (int i = 0; i < trialCount; i++)
            {
                double value = rnd.NextDouble();

                if( value >= 0.5)
                {
                    success++;
                }

                richTextBox1.AppendText("success: " + success.ToString() + " i: " + i.ToString() +"\n");
                g.DrawLine(new Pen(Color.Black, 3), Convert.ToInt32(prvX), Convert.ToInt32(prvY), Convert.ToInt32(fromRealToVirtualX(i,0,trialCount,pictureBox1.Width)), Convert.ToInt32(fromRealToVirtualY(success,0,trialCount,pictureBox1.Height)));
                prvX = fromRealToVirtualX(i, 0, trialCount, pictureBox1.Width);
                prvY = fromRealToVirtualY(success, 0, trialCount, pictureBox1.Height);
            }
            
            

            pictureBox1.Image = bitmap;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private double fromRealToVirtualX(double x,double minX,double maxX,double width)
        {

            return (x - minX)/(maxX-minX)*width;            
        }

        private double fromRealToVirtualY(double y,double minY,double maxY,double height)
        {
            return height - ((y - minY) / (maxY - minY)) * height;
        }
    }
}