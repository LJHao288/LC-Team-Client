using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reigster : MonoBehaviour
{
    public static String name1 = "";
    public static String password = "";
    public static List<string> datalist = new List<string>();
    public Button Register;
    public Button Cancel;

    // Use this for initialization
    private void Start()
    {
        readData(datalist);
        Register.onClick.AddListener(click);
        Cancel.onClick.AddListener(click1);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void click()
    {
        if (exist(getname(), datalist))
        {
            InputField username = GameObject.Find("username").GetComponent<InputField>();
            username.text = "the name already existed or null";
            InputField PW = GameObject.Find("password").GetComponent<InputField>();
            PW.text = "";
        }
        else
        {
            inputData(userID(), getname(), getPW());
            //跳回loging
            SceneManager.LoadScene("Login");
        }
    }


public void click1()
{
    SceneManager.LoadScene("Login");
}

public static string getname()
{
    InputField username = GameObject.Find("username").GetComponent<InputField>();
    return name1 = username.text;
}

public static string getPW()
{
    InputField PW = GameObject.Find("password").GetComponent<InputField>();
    return password = PW.text;
}

public static bool inputData(int id, string name, string PW)
{
    try
    {
        FileStream fs = new FileStream(Path(), FileMode.Append);
        StreamWriter sw = new StreamWriter(fs);
        //开始写入
        sw.WriteLine(id.ToString() + "," + name + "," + PW);
        //清空缓冲区
        sw.Flush();
        //关闭流
        sw.Close();
        fs.Close();
        return true;
    }
    catch
    {
        return false;
    }
}

public static void readData(List<string> datalist)
{
    StreamReader sr = new StreamReader(Path(), System.Text.Encoding.Default);
    String line;

    while ((line = sr.ReadLine()) != null)
    {
        string[] a = line.Split(',');

        datalist.Add(a[0]);
        datalist.Add(a[1]);
        datalist.Add(a[2]);
    }
}

public static String Path()
{
    string str = System.Environment.CurrentDirectory; ;
    str = str + "\\Assets\\Data.txt";
    return str;
}

public int userID()
{
    FileStream F = new FileStream(Path(), FileMode.OpenOrCreate, FileAccess.Read);
    string[] strings = File.ReadAllLines(Path());
    F.Close();
    return strings.Length;
}

public static bool exist(string name, List<string> datalist)
{
    bool existed = false;
    for (int i = 1; i < datalist.Count(); i += 3)
    {
        if (name.Equals(datalist[i]) || name.Equals("the name already existed or null") || name.Equals(""))
        {
            existed = true;
            break;
        }
    }
    return existed;
}
}
