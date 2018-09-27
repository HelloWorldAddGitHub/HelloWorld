using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Halcon.EthernetPort
{
    [DefaultEvent("DataReceived")]
    public partial class EthernetPort : Component
    {
        public EthernetPort()
        {
            InitializeComponent();
        }

        public EthernetPort(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        public EthernetPort(string host, int port)
        {
            Host = host;
            Port = port;
        }


        public enum EthernetPortMode { Server, Client }
        

        // 监听套接字
        private Socket listener;

        // 建立通信套接字列表
        public List<Socket> SocketList = new List<Socket>();

        // 连接套接字
        private Socket connector;



        [Browsable(true)]
        [DefaultValue(EthernetPortMode.Server)]
        [Description("获取或设置网口模式")]
        public EthernetPortMode PortMode { get; set; }


        [Browsable(true)]
        [DefaultValue("127.0.0.1")]
        [Description("获取或设置IP地址")]
        public string Host { get; set; } = "127.0.0.1";


        [Browsable(true)]
        [DefaultValue(5000)]
        [Description("获取或设置端口")]
        public int Port { get; set; } = 5000;


        // 自动连接
        [Browsable(true)]
        [DefaultValue(true)]
        [Description("获取或设置是否自动连接服务端")]
        public bool AutoConnect { get; set; } = true;


        /// <summary>
        /// 接收和发送数据最大缓存
        /// </summary>
        [Browsable(true)]
        [DefaultValue(1024)]
        [Description("获取或设置缓存区大小")]
        public int BufferSize { get; set; } = 1024;


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsOpen { get; private set; }


        [Description("处理接收数据的事件")]
        public event EventHandler<SocketDataReceivedEventArgs> SocketDataReceived;
        





        /// <summary>
        /// 打开服务端
        /// </summary>
        public void Open()
        {
            if (PortMode == EthernetPortMode.Server)
            {
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(IPAddress.Parse(Host), Port));
                listener.Listen(5);
                IsOpen = true;

                Task.Run(() =>
                {
                    try
                    {
                        while (true)
                        {
                            Socket socket = listener.Accept();
                            Task.Run(() => Recv(socket));
                            SocketList.Add(socket);
                        }
                    }
                    catch (Exception)
                    {

                    }
                });
            }
            else
            {
                Task.Run(() =>
                {
                    do
                    {
                        try
                        {
                            if (!IsOpen)
                            {
                                connector = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                IsOpen = true;
                            }

                            connector.Connect(Host, Port);
                            Recv(connector);
                        }
                        catch (Exception)
                        {

                        }
                    } while (AutoConnect);
                });
            }
        }


        /// <summary>
        /// 接收来自socket的数据
        /// </summary>
        /// <param name="socket">套接字</param>
        private void Recv(Socket socket)
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[BufferSize];
                    int count = socket.Receive(buffer);
                    SocketDataReceived?.BeginInvoke(this, new SocketDataReceivedEventArgs(socket, buffer, count), null, null);

                    // 断开连接或异常则关闭套接字并跳出返回
                    if (count <= 0)
                    {
                        if (PortMode == EthernetPortMode.Server)
                        {
                            SocketList.Remove(socket);
                        }
                        else
                        {
                            IsOpen = false;
                        }

                        socket.Close();
                        break;
                    }
                }
                catch (Exception)
                {

                }
            }
        }


        /// <summary>
        /// 给socket发送数据
        /// </summary>
        /// <param name="socket">套接字</param>
        /// <param name="data">数据</param>
        public void Send(string data, Socket socket = null, bool isHex = false)
        {
            // 将需要发送的数据转换为byte[]类型
            byte[] buffer;

            if (isHex)
            {
                data = data.Replace(" ", "");
                int length = data.Length / 2;
                buffer = new byte[length];

                for (int i = 0; i < length; i++)
                {
                    string temp = data.Substring(i * 2, 2);
                    buffer[i] = Convert.ToByte(temp, 16);
                }
            }
            else
            {
                buffer = Encoding.Default.GetBytes(data);
            }

            // 发送数据
            if (PortMode == EthernetPortMode.Server)
            {
                if (socket == null)
                {
                    foreach (var item in SocketList)
                    {
                        item.Send(buffer);
                    }
                }
                else
                {
                    socket.Send(buffer);
                }
            }
            else
            {
                connector.Send(buffer);
            }
        }



        /// <summary>
        /// 关闭所有套接字
        /// </summary>
        public void Close()
        {
            if (PortMode == EthernetPortMode.Server)
            {
                foreach (var item in SocketList)
                {
                    item.Close();
                }

                listener.Close();
                SocketList.Clear();
            }
            else
            {
                connector.Close();
            }

            IsOpen = false;
        }
    }
}
