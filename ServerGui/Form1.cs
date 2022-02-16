namespace ServerGui;

public partial class Form1 : Form
{
    private readonly Server server = new Server();
    private Action<string> sendMessage;
    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        sendMessage = server.StartReal();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        sendMessage.Invoke(textBox1.Text);
    }
}
