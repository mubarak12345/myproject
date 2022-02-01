using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
//COMMANDED
namespace TCP
{
    public partial class Form2 : Form
    {
        string rd;
        byte[] b1;
        string v;
        int m;
        TcpListener list;
        Int32 port = 5050;
        Int32 port1 = 5055;
        public Form2()
        {
            InitializeComponent();
        }
    
        SimpleTcpServer server;
  
        private void Form2_Load(object sender, EventArgs e)
        {
            try { 
            Form1 fm = new Form1();
            fm.Show();
            textBox1.Text = "192.168.11.210";
            textBox2.Text = "8910";
            IPAddress localAddr = IPAddress.Parse(textBox1.Text);
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;//enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TCP-IP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            //Update mesage to txtStatus
            richTextBox1.Invoke((MethodInvoker)delegate ()
            {
                richTextBox1.Text += e.MessageString;
                e.ReplyLine(string.Format("You said: {0}", e.MessageString));
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Start server host
                richTextBox1.Text += "Server starting...";
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(textBox1.Text);
            server.Start(ip, Convert.ToInt32(textBox2.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TCP-IP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
         
        }
    }
}
