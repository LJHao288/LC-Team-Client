using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public static List<string> datalist = new List<string>();
    public Button login;
    public Button gotuRegister;
    // Use this for initialization
    private void Start()
    {
        Reigster.readData(datalist);
        login.onClick.AddListener(click1);
        gotuRegister.onClick.AddListener(click2);
    }


    // Update is called once per frame
    private void Update()
    {
    }

    public void click1()
    {
        {
            if (check(Reigster.getname(), Reigster.getPW(), datalist))
            {
                SceneManager.LoadScene("Chat");
            }
            else
            {
                InputField username = GameObject.Find("username").GetComponent<InputField>();
                username.text = "password wrong.";
            }
        }
    }

    public void click2()
    {
        //跳转到注册
        SceneManager.LoadScene("Reigster");
    }

    public static bool exist(string name)
    {
        bool existed = false;
        for (int i = 1; i < datalist.Count(); i += 3)
        {
            if (name.Equals(datalist[i]))
            {
                existed = true;
                break;
            }
        }
        if (!existed)
        {
            InputField username = GameObject.Find("username").GetComponent<InputField>();
            username.text = "the name not existed.";
        }
        return existed;
    }

    public static bool check(string name, string password, List<string> datalist)
    {
        bool pw = false;
        for (int i = 1; i < datalist.Count(); i += 3)
        {
            if (name.Equals(datalist[i]) && password.Equals(datalist[i + 1]))
            {
                pw = true;
                break;
            }
        }

        return pw;
    }
}