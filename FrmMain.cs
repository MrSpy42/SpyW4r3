using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.IO;

namespace SpyW4r3
{
    public partial class FrmMain : Form
    {
        static int port;
        static IPAddress ip;
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
                ip = IPAddress.Parse("127.0.0.1");
                port = Int32.Parse(portTextBox.Text);
                server = new messageClient(ip, port);
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
                        str.Append(client.Client.LocalEndPoint.ToString());
                        server.encryptionKey = server.tempPort;
                        server.keyPort = server.tempPort;
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

                    d = Encoding.ASCII.GetBytes(server.DecryptString(d, server.encryptionKey, server.IV));

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
                if(!(server == null))
                {
                    if (server.sendQueue != null)
                    {
                        TcpClient client = new TcpClient(ipS.ToString(), portS);
                        NetworkStream str = client.GetStream();
                        byte[] msg = { 0xFE };
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
            server.send(server.EncryptString(s, server.encryptionKey, server.IV));
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

    //
    // MESSAGE CLIENT CLASS
    //
    public class messageClient
    {
        public byte[] sendQueue = null;
        public TcpListener listener;
        public byte[] keyPort = new byte[16];
        public readonly byte[] tempPort = new byte[16];
        public byte[] encryptionKey = new byte[16];
        public byte[] IV = Encoding.ASCII.GetBytes("spywarebestware1");
        
        public messageClient(IPAddress ip,int port)
        {
            listener = new TcpListener(ip, port);
            listener.Start();
            tempPort = Encoding.ASCII.GetBytes(port.ToString() + "000000000000");
        }
        public byte[] receive(NetworkStream stream)
        {
            byte[] bytes = new Byte[256];
            stream.Read(bytes, 0, bytes.Length);
            
            return bytes;
        }

        public void send(byte[] s)
        {
            sendQueue = s;
        }

        public byte[] EncryptString(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = PaddingMode.Zeros;
                

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public string DecryptString(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = PaddingMode.Zeros;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string. 
                            plaintext = srDecrypt.ReadLine();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
