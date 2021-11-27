using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace Game
{
    public partial class Form1 : Form
    {
        private NetworkStream stream;
        private TcpClient tcpClient = new TcpClient();
        
       
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //向指定的IP地址的服务器发出连接请求
                tcpClient.Connect("10.1.230.74", 3900);
                listBox1.Items.Add("连接成功！");
                stream = tcpClient.GetStream();
                byte[] data = new byte[1024];
                //判断网络流是否可读            
                if (stream.CanRead)
                {
                    int len = stream.Read(data, 0, data.Length);
                    //Encoding ToEncoding = Encoding.GetEncoding("UTF-8");
                    //Encoding FromEncoding = Encoding.GetEncoding("GB2312");
                    //data=Encoding.Convert(FromEncoding, ToEncoding, data);
                    //string msg = Encoding.UTF8.GetString(data, 0, data.Length);
                    string msg = Encoding.Default.GetString(data, 0, data.Length);
                    string str = "\r\n";
                    char[] str1 = str.ToCharArray();
                    string[] msg1 = msg.Split(str1);
                    for (int j = 0; j < msg1.Length; j++)
                    {
                        listBox1.Items.Add(msg1[j]);
                    }
                }
             }
            catch
            {
                listBox1.Items.Add("连接失败！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //判断连接是否断开
            if (tcpClient.Connected)
            {
                //向服务器发送数据
                string msg = textBox2.Text;
                Byte[] outbytes = System.Text.Encoding.Default.GetBytes(msg + "\n");
                stream.Write(outbytes, 0, outbytes.Length);
                byte[] data = new byte[1024];
                //接收服务器回复数据
                if (stream.CanRead)
                {
                    int len = stream.Read(data, 0, data.Length);
                    string msg1 = Encoding.Default.GetString(data, 0, data.Length);
                    string str = "\r\n";
                    char[] str1 = str.ToCharArray();
                    string[] msg2 = msg1.Split(str1);
                    for (int j = 0; j < msg2.Length; j++)
                    {
                        listBox1.Items.Add(msg2[j]);
                    }
                }
            }
            else
            {
                listBox1.Items.Add("连接已断开");
            }
            textBox2.Clear();
        }

        private int picture = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            picture++;
            string picturePath= @"D:\新建文件夹2\image\" + picture+".jpg";
            pictureBox1.Image = Image.FromFile(picturePath);
            if(picture==6)
            {
                picture = 0;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            
            string music = @"D:\新建文件夹2\天龙八部.mp3";
            axWindowsMediaPlayer1.URL = music;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //暂停播放
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }
    }
}
