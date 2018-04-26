using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour {
    public Button Send;
    public Button Game1;
    public Button Rename;
    public Button History;
    public Button Picture;

    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    public GameObject message;
    string a1 = "";
    public string clientName = "user";
    string a = "";
    // Use this for initialization
    void Start ()
    {
		Send.onClick.AddListener(send);
        Game1.onClick.AddListener(game1);
        Rename.onClick.AddListener(rename);
        History.onClick.AddListener(history);
        Picture.onClick.AddListener(picture);

    }
	
	// Update is called once per frame
	void Update () {
        if (socketReady) {
            if (stream.DataAvailable) {
                string data = reader.ReadLine();
                if (data != null)
                    OnIncomingData(data);
            }
        }
	}

    public void OnIncomingData(string data) {
        Debug.Log(data);

        a1 += data + "\r\n";
        Text m = GameObject.Find("messages1").GetComponent<Text>();
        m.text = a1;
        
        //write data to text
        FileStream fs = new FileStream(Path1(), FileMode.Append);
        StreamWriter sw = new StreamWriter(fs);
        //开始写入
        sw.WriteLine(data);
        //清空缓冲区
        sw.Flush();
        //关闭流
        sw.Close();
        fs.Close();
    }
    public static String Path1()
    {
        //string str = System.Environment.CurrentDirectory; ;
        //str = str + "\\Assets\\history.txt";
        string DPath = Application.dataPath;
        string url = DPath + "/StreamingAssets/history.txt";
        return url;
       
    }

    public void connectedToServer()
    {
        if (socketReady) {
            return;
        }

        string host = "127.0.0.1";
        int port = 4321;

        try {
            socket = new TcpClient(host,port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
        }
        catch(Exception e)
        {
            Debug.Log("Socket error: " + e.Message);

        }

    }

    public void sendm(string data)
    {
        if (!socketReady) {
            return;
        }

        writer.WriteLine(data);
        writer.Flush();
    }

    public void send() {
        string m = GameObject.Find("input").GetComponent<InputField>().text;
        sendm(m);
        InputField input = GameObject.Find("input").GetComponent<InputField>();
        input.text = "";
    }

    public void game1()
    {
        connectedToServer();
    }
    public void rename()
    {
        Debug.Log("change");
        InputField m = GameObject.Find("rename").GetComponent<InputField>();
        //clientName = "chen";
        clientName = m.text;
        Debug.Log(clientName);
        sendm("&Name|" + clientName);
        m.text = "";
    }
    public void history()
    {
        StreamReader sr = new StreamReader(Path1());
        String line;
        String l1 = "";
        while ((line = sr.ReadLine()) != null)
        {
            Debug.Log(line.ToString());
            l1 += line.ToString() + "\r\n";
        }
        Text hs = GameObject.Find("messages1").GetComponent<Text>();
        hs.text =l1;
    }
    public void picture()
    {
        //
    }
}
