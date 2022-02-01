using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP
{
    public partial class Form1 : Form
    {
        bool connect = false;
        string n;
        byte[] b1;
        OpenFileDialog op;
        public Form1()
        {
            InitializeComponent();
        }
        SimpleTcpClient client;
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "192.168.11.210";
            textBox2.Text = "8910";
            //11000
            client_call();
            label4.Text = string.Empty;
        }
        public void client_call()
        {
            try { 
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TCP-IP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            //Update message to txtStatus
            richTextBox1.Invoke((MethodInvoker)delegate ()
            {
                richTextBox1.Text += e.MessageString;
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            //Connect to server
            client.Connect(textBox1.Text, Convert.ToInt32(textBox2.Text));
            connect = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (connect == true)
            {
                client.WriteLineAndGetReply(textBox3.Text + "\n", TimeSpan.FromSeconds(3));
            }
            else
            {
                MessageBox.Show("Please Connect", "TCP-IP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //client.WriteLineAndGetReply(textBox3.Text + "\n", TimeSpan.FromSeconds(3));
   
        
            textBox3.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (connect == true) {
       
            op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                string t = textBox4.Text;
                t = op.FileName;
                FileInfo fi = new FileInfo(textBox4.Text = op.FileName);
                n = fi.Name + "." + fi.Length;
                TcpClient client = new TcpClient(textBox1.Text, Convert.ToInt32(textBox2.Text));
                StreamWriter sw = new StreamWriter(client.GetStream());
                sw.WriteLine(n);
                sw.Flush();
                label4.Text = "File Transferred....";
            }
            }else
            {
                MessageBox.Show("Please Connect","TCP-IP",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text != "")
                {

             
                TcpClient client = new TcpClient(textBox1.Text, Convert.ToInt32(textBox2.Text));
                Stream s = client.GetStream();
                b1 = File.ReadAllBytes(op.FileName);
                s.Write(b1, 0, b1.Length);
                client.Close();
                label4.Text = "File Transferred....";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "TCP-IP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            label4.Text = "";

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        //public void WriteToExcel(String data)
        //{
        //    DateTime today = DateTime.Today;
        //    DateTime now = DateTime.Now;
        //    DateTime dateTime = DateTime.UtcNow.Date;
        //    String _data_path = Application.StartupPath + "\\CHAT HISTORY\\"+ "History.xlsx";
        //    //C:\Users\mubarak\source\repos\Muba-file share\TCP\bin\Debug\netcoreapp3.1
        //    using (StreamWriter writer = new StreamWriter(_data_path))
        //    {
                
        //        writer.WriteLine(data);

        //    }

           
        //}
       

    }
}
