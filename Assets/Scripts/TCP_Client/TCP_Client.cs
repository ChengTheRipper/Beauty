using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using UnityEngine;

public class TCP_Client
{

    private string staInfo = "NULL";             //状态信息
    private string inputIp = "127.0.0.1";   //输入ip地址
    private string inputPort = "1080";           //输入端口号
    private string inputMes = "NULL";             //发送的消息
    private int recTimes = 0;                    //接收到信息的次数
    private string recMes = "NULL";              //接收到的消息
    private Socket socketSend;                   //客户端套接字，用来链接远端服务器
    private bool clickSend = false;              //是否点击发送按钮

    public string InputMes
    {
        set { inputMes = value; }
        get { return inputMes; }
    }

    public string MesReceived
    {
        get { return recMes; }
    }

    Thread r_thread;
    //建立链接
    public void InitServer()
    {
        try
        {
            int _port = Convert.ToInt32(inputPort);             //获取端口号
            string _ip = inputIp;                               //获取ip地址

            //创建客户端Socket，获得远程ip和端口号
            socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(_ip);
            IPEndPoint point = new IPEndPoint(ip, _port);

            socketSend.Connect(point);
            Debug.Log("连接成功 , " + " ip = " + ip + " port = " + _port);
            staInfo = ip + ":" + _port + "  连接成功";

            //r_thread = new Thread(ReceivedMessage);             //开启新的线程，不停的接收服务器发来的消息
            //r_thread.IsBackground = true;
            //r_thread.Start();

            //Thread s_thread = new Thread(SendMessage);          //开启新的线程，不停的给服务器发送消息
            //s_thread.IsBackground = true;
            //s_thread.Start();
        }
        catch (Exception)
        {
            Debug.Log("IP或者端口号错误......");
            staInfo = "IP或者端口号错误......";
        }
        return;
    }

    /// <summary>
    /// 接收服务端返回的消息
    /// </summary>
    public void ReceivedMessage()
    {
        while (true)
        {
            try
            {
                byte[] buffer = new byte[1024 * 6];
                //实际接收到的有效字节数
                int len = socketSend.Receive(buffer);
                if (len == 0)
                {
                    continue;
                }


                recMes = Encoding.ASCII.GetString(buffer, 0, len);

                if (recMes == "Face Detected")
                {
                    WebCameraManager.face_detected = true;
                    Debug.Log("客户端接收到的数据 " + recMes);
                    return;
                }
            }
            catch
            {

            }
        }
    }

    /// <summary>
    /// 向服务器发送消息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void SendMes(string input)
    {
        try
        {

            byte[] buffer = new byte[1024 * 6];
            buffer = Encoding.UTF8.GetBytes(input);
            socketSend.Send(buffer);
            Debug.Log("发送的数据为：" + input);

        }
        catch { }
        return;
    }


    public void DisableServer()
    {
        Debug.Log("begin OnDisable()");

        if (socketSend.Connected)
        {
            try
            {
                socketSend.Shutdown(SocketShutdown.Both);    //禁用Socket的发送和接收功能
                socketSend.Close();                          //关闭Socket连接并释放所有相关资源

            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        Debug.Log("end OnDisable()");
    }

    ~TCP_Client()
    {
        DisableServer();
    }
}
