using System;
using System.Net.Sockets;
using System.Text;

namespace Halcon.EthernetPort
{
    public class SocketDataReceivedEventArgs : EventArgs
    {
        public SocketDataReceivedEventArgs(Socket socket, byte[] buffer, int count)
        {
            Socket = socket;
            this.count = count;
            this.buffer = new byte[count];
            Array.Copy(buffer, this.buffer, count);
        }


        private byte[] buffer;
        private int count;


        public Socket Socket { get; }

        public byte[] Bytes
        {
            get { return buffer; }
        }


        public string String
        {
            get { return Encoding.Default.GetString(buffer); }
        }


        public string HexString
        {
            get
            {
                string hex = string.Empty;

                for (int i = 0; i < count; i++)
                {
                    hex += buffer[i].ToString("X2") + " ";
                }

                return hex;
            }
        }
    }
}
