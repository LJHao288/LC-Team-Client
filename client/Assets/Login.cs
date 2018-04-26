using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private List<string> datalist = new List<string>();
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
        if (exist() && check())
        {
            SceneManager.LoadScene("Chat");
        }
    }

    public void click2()
    {
        //跳转到注册
        SceneManager.LoadScene("Reigster");
    }

    public bool exist()
    {
        bool existed = false;
        for (int i = 1; i < datalist.Count(); i += 3)
        {
            if (Reigster.getname().Equals(datalist[i]))
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

    public bool check()
    {
        bool pw = false;
        for (int i = 1; i < datalist.Count(); i += 3)
        {
            if (Reigster.getname().Equals(datalist[i]) && Reigster.getPW().Equals(datalist[i + 1]))
            {
                pw = true;
                break;
            }
        }
        if (!pw)
        {
            InputField username = GameObject.Find("username").GetComponent<InputField>();
            username.text = "password wrong.";
        }
        return pw;
    }
}