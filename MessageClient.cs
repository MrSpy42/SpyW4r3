using System;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace SpyW4r3
{
    public class messageClient
    {
        public byte[] sendQueue = null;
        public TcpListener listener;
        public byte[] keyPort = new byte[16];
        public readonly byte[] tempPort = new byte[16];
        public byte[] encryptionKey = new byte[16];
        public byte[] IV = Encoding.ASCII.GetBytes("spywarebestware1");

        public messageClient(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            tempPort = Encoding.ASCII.GetBytes(port.ToString() + "0an003vb0!0e");
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

    }
}