using System.Windows.Forms;

namespace Drag6Drop
{
    public partial class Form1 : Form

    {

        Bitmap bitmap;
        Graphics g;
        Rectangle rect;

        int xDown;          //Initial position of rect when mouse down
        int yDown;

        int xMouse;         //Position of mouse when down
        int yMouse;


        int rWidth;         //Rect dim
        int rHeight;

        bool drag = false;      //to know if I grabbed the rect
        bool resize = false;    //to know if I'm resizing the rect

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            g = Graphics.FromImage(bitmap);

            rect = new Rectangle(0, 0, 50, 200);
            g.DrawRectangle(new Pen(Color.Blue, 1), rect);

            pictureBox1.Image = bitmap;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            xMouse = e.X;
            yMouse = e.Y;

            xDown = rect.X;
            yDown = rect.Y;


            rWidth = rect.Width;
            rHeight = rect.Height;


            //System.Diagnostics.Debug.WriteLine("xRect:"+xRect+" yRect: "+yRect);

            //System.Diagnostics.Debug.WriteLine("xMouse:" + xMouse + " yMouse: " + yMouse);



            if (xMouse >= rect.X && xMouse <= rect.X + rect.Width && yMouse >= rect.Y && yMouse <= rect.Y + rect.Height) {

                if (e.Button == MouseButtons.Left) {
                    drag = true;
                }
                
                else if(e.Button == MouseButtons.Right)
                {
                    resize = true;
                }
                
            } 
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            resize = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int delta_x = e.X - xMouse;
            int delta_y = e.Y - yMouse;

            if (drag)
            {

                rect.X = xDown + delta_x;
                rect.Y = yDown +  delta_y;

                //System.Diagnostics.Debug.WriteLine("Sto muovendo il mouse");

                g.Clear(Color.White);

                g.DrawRectangle(Pens.Blue, rect);
                pictureBox1.Image = bitmap;
            }

            if (resize)
            {
                rect.Width = rWidth + delta_x;
                rect.Height = rHeight + delta_y;


                g.Clear(Color.White);

                g.DrawRectangle(Pens.Blue, rect);
                pictureBox1.Image = bitmap;
            }
        }
    }
}