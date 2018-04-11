using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class Reigster : MonoBehaviour {
    public static String name1 = "";
    public static String password = "";
    public static List<string> datalist = new List<string>();
    public Button Register;
    // Use this for initialization
    void Start () {
        readData(datalist);
       Register.onClick.AddListener(click);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void click()
    {      
        if (exist())
        {
            InputField username = GameObject.Find("username").GetComponent<InputField>();
            username.text= "the name already existed or null";
            InputField PW = GameObject.Find("password").GetComponent<InputField>();
            PW.text = "";
            
        }
        else
        {
            inputData();
            //跳回loging
            SceneManager.LoadScene("Login");
                
            
        }
          
    }
    public static string getname()
    {
        InputField username = GameObject.Find("username").GetComponent<InputField>();
        return  name1 = username.text;
    }
    public static string getPW()
    {
        InputField PW = GameObject.Find("password").GetComponent<InputField>();
        return  password = PW.text;
    }
    public void inputData()
    {   int Id = userID();
        FileStream fs = new FileStream(Path(), FileMode.Append);
        StreamWriter sw = new StreamWriter(fs);
        //开始写入
        sw.WriteLine(Id.ToString()+","+getname()+","+getPW());
        //清空缓冲区
        sw.Flush();
        //关闭流
        sw.Close();
        fs.Close();
    }
    public static void  readData(List<string> datalist)
    { 
        StreamReader sr = new StreamReader(Path(), System.Text.Encoding.Default);
        String line;
        
        while ((line = sr.ReadLine()) != null)
        {   string[] a = line.Split(',');
            
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
        FileStream F = new FileStream(Path(),FileMode.OpenOrCreate, FileAccess.Read);
        string[] strings = File.ReadAllLines(Path());
        F.Close();
        return strings.Length;
        
    }
    public static bool exist()
    { bool existed = false;
        for (int i = 1; i < datalist.Count(); i += 3)
        {

            if (getname().Equals(datalist[i]) || getname().Equals("the name already existed or null")|| getname().Equals(""))
            {
                existed = true;
                break;
            }

        }
        return existed;
    }
}
