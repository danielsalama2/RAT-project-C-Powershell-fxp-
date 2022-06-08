using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Text;
//this.BeginInvoke((MethodInvoker)(() => ניתן להשתמש בזה בטרידס!!!!!!!!!!
namespace WindowsFormsApp21
{
    public partial class Form1 : Form
    {
        Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static string ip2;
        public Socket client1;
        public Socket streamer;
        public string ip;
        public static string ip3;

        public string newLine = Environment.NewLine;

        public static ListViewItem item2 = new ListViewItem(ip2, 1);

        Dictionary<string, Socket> map = new Dictionary<string, Socket>();



        public string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        public Form1()
        {
            InitializeComponent();
            string hostName = Dns.GetHostName();
            try
            {
                ip = Dns.GetHostByName(hostName).AddressList[0].ToString();
            }
            catch { ip = Dns.GetHostByName(hostName).AddressList[1].ToString(); }
            ipd.Text = ip;
            port.Text = "443";

            MessageBox.Show("Created by Expl0itics Fan from FXP \n ניתן למצוא את הקוד הפתוח של התוכנה בגיטהאב שלי \n נא לצאת מהתוכנה רק דרך כפתור היציאה שהגדרתי.");

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ipd.Text), int.Parse(port.Text));
                socketServer.Bind(ip);
                socketServer.Listen(1);
                this.BeginInvoke((MethodInvoker)(() => textBox1.Text += ("listener began on " + ip + newLine)));
                backgroundWorker1.RunWorkerAsync();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {


            int i = 1;
            while (i == 1)
            {



                try
                {

                    client1 = socketServer.Accept(); // כל פעם שמערכת מנסה להתחבר היא ת אושר
                    ip2 = client1.RemoteEndPoint.ToString();
                    map[ip2] = client1;
                    textBox1.Invoke((MethodInvoker)delegate
                    {
                        textBox1.Text += ("Connection has been maded from " + ip2 + " " + userName + newLine);
                    });
                    Thread clientThread = new Thread(new ThreadStart(() => User(client1)));
                    clientThread.Start();

                }
                catch { }

            }
            
        }

        public void User(Socket client)
        {
            
            while (true)
            {
                try
                {
                    
                    listView1.Invoke((MethodInvoker)delegate
                    {
                        ListViewItem hasMatch = listView1.FindItemWithText(ip2);


                        if (hasMatch == null)
                        {

                            listView1.Invoke((MethodInvoker)delegate
                            {
                                try
                                {
                                    listView1.Items.Add(ip2);
                                }
                                catch (Exception ex)
                                { MessageBox.Show(ex.Message); }

                            });
                        }
                    });


                    byte[] msg = new byte[1024];
                    int size = client.Receive(msg);

                    ListView findListView = new ListView();

                    ip2 = client.RemoteEndPoint.ToString();

                    textBox1.Invoke((MethodInvoker)delegate
                    {
                        textBox1.Text += userName + " " + ip2 + " >> " + System.Text.Encoding.ASCII.GetString(msg, 0, size) + newLine;
                    });
                    
                }
                catch
                {


                }

                
            }
        }

        public void tred()
        {
            

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    ip3 = listView1.SelectedItems[0].Text;
                    Socket clientToSend = map[ip3];
                    try
                    {
                        byte[] Message = System.Text.Encoding.ASCII.GetBytes(send.Text);
                        clientToSend.Send(Message);
                    }
                    catch
                    {
                        //                        listView1.SelectedItems[0].Remove();  // לא מצליח למחוק מהרשימה.
                        textBox1.Text += "Connection has been closed. " + newLine;
                    }
                }
                catch
                {
                    textBox1.Text += "No client selected. " + newLine;

                }
            }
            catch
            { }
        }


        private void button3_Click_2(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void treaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.fxp.co.il/showthread.php?t=21287600");

        }
    }
}
