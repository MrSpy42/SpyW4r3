using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace SpyW4r3
{
    public partial class FrmMain : Form
    {
        static int port;
        static int portS = -1;
        static IPAddress ipS;
        static messageClient server;
        static Thread t;
        static Thread sendThread;
        static string username;
        static bool sendBool = true;
        static bool receiveBool = true;

        public FrmMain()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(FrmMain_FormClosing);
            
        }

        private void FrmMain_FormClosing(Object sender, FormClosingEventArgs e)
        {
            //cancels everything if threads are still alive.
            
            if (!(t == null) || !(sendThread == null))
            {
                if (t.IsAlive || sendThread.IsAlive)
                {
                    sendBool = false;
                    receiveBool = false;
                }
            }
        }

        //Setup connections/start up all threads.
        private void connectButton_Click(object sender, EventArgs e)
        {
            mainTextBox.AppendText("Opening listener...");
            try {
                port = Int32.Parse(portTextBox.Text);
                server = new messageClient(port);
                sendThread = new Thread(sendHandler);
                sendThread.Start();
                t = new Thread(handler);
                t.Start();

                this.portTextBox.ReadOnly = true;
                this.connectButton.Enabled = false;
                mainTextBox.AppendText(" Connected\n");
            } catch(Exception exc) {
                mainTextBox.AppendText("Failed : "+ exc.Message + "\n");
            }
            
        }

        //
        //  INCOMING CONNECTION THREAD
        //
        public void handler()
        {
            byte[] d;
            MethodInvoker mi;
            while (receiveBool)
            {
                if(server.listener.Pending())
                {
                    TcpClient client = server.listener.AcceptTcpClient();
                    NetworkStream s = client.GetStream();

                    d = server.receive(s);

                    if (Encoding.ASCII.GetString(d).Contains("!requestConnect"))
                    {
                        StringBuilder str = new StringBuilder();
                        StringBuilder str2 = new StringBuilder();
                        str2.Append(Encoding.ASCII.GetString(d));
                        str.Append(client.Client.RemoteEndPoint.ToString());
                        server.encryptionKey = server.tempPort;
                        string[] Ip = str.ToString().Split(":");
                        string[] Port = str2.ToString().Split(",");
                        ipS = IPAddress.Parse(Ip[0]);
                        portS = Int32.Parse(Port[1]);
                        mi = delegate ()
                        {
                            this.mainTextBox.AppendText("Established connection with client. \n");
                            this.portSend.Text = Port[1];
                            this.ipSend.Text = Ip[0];
                            this.portSend.Enabled = false;
                            this.ipSend.Enabled = false;
                            this.requestButton.Enabled = false;
                        };
                        this.Invoke(mi);
                    }

                    d = Encoding.ASCII.GetBytes(Encryptor.DecryptString(d, server.encryptionKey, server.IV));

                    mi = delegate () {
                        this.mainTextBox.AppendText(Encoding.ASCII.GetString(d));
                        this.mainTextBox.AppendText("\n");
                    };
                    this.Invoke(mi);
                    client.Close();
                    Thread.Sleep(15);
                }
            }
            Thread.Sleep(15);
        }
        //
        // SEND THREAD
        //
        public void sendHandler()
        {
            while(sendBool)
            {
                byte[] msg;
                if(!(server == null))
                {
                    if (server.sendQueue != null)
                    {
                        TcpClient client = new TcpClient(ipS.ToString(), portS);
                        NetworkStream str = client.GetStream();
                        
                        if (Encoding.ASCII.GetString(server.sendQueue).Contains("!requestConnect"))
                        {
                            msg = server.sendQueue;
                        } else {
                            msg = server.sendQueue;
                            MethodInvoker mi = delegate () { this.mainTextBox.AppendText("YOU: " + this.sendBox.Text + "\n"); };
                            this.Invoke(mi);
                        }
                        str.Write(msg, 0, msg.Length);

                        server.sendQueue = null;
                        client.Close();
                    }
                }
                Thread.Sleep(15);
            }
        }
        //sends message ( sets port/ip send and username )
        private void sendButton_Click(object sender, EventArgs e)
        {
            portS = Int32.Parse(this.portSend.Text);
            ipS = IPAddress.Parse(this.ipSend.Text);
            username = this.usernameBox.Text;

            String s = username + " : " + this.sendBox.Text;
            server.send(Encryptor.EncryptString(s, server.encryptionKey, server.IV));
        }

        //REQUEST CONNECT WITH ALREADY LISTENING CLIENT
        private void button1_Click(object sender, EventArgs e)
        {
            portS = Int32.Parse(this.portSend.Text);
            ipS = IPAddress.Parse(this.ipSend.Text);

            this.portSend.Enabled = false;
            this.ipSend.Enabled = false;
            this.requestButton.Enabled = false;

            server.encryptionKey = Encoding.ASCII.GetBytes(portS.ToString() + "0an003vb0!0e");
            server.keyPort = Encoding.ASCII.GetBytes(portS.ToString() + "0an003vb0!0e");

            server.send(Encoding.ASCII.GetBytes("!requestConnect," + port.ToString()));
            Thread.Sleep(50);
        }
    }
}
