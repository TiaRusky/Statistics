namespace FirstHomework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Hai cliccato il bottone";
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.Text = "Mouse Hover";
            button1.BackColor = Color.Green;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Text = "Mouse leave";
            button1.BackColor = Color.Gray;
        }
    }
}