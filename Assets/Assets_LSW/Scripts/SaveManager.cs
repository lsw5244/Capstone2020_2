using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 스탯종류는 STR
 */
public class SaveManager : MonoBehaviour
{
    static public SaveManager instance;
    private float str;
    public float Str
    {
        get { return str; }
    }
    public void AddStr(float add)
    {
        str += add;
        PlayerPrefs.SetFloat("CurrentStr", str);
        //Debug.Log(str);
    }

    private float hp;
    public float HP
    {
        get { return hp; }
    }
    public void SaveHp(float hp)
    {
        this.hp = hp;
        PlayerPrefs.SetFloat("CurrentHp", hp);
        //Debug.Log(hp);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //Debug.Log("@@@@@");
            //PlayerPrefs.SetFloat("CurrentStr", 50f);
            //PlayerPrefs.SetFloat("CurrentHp", 5f);
        }
    }
    void Start()
    {
        if (PlayerPrefs.GetFloat("CurrentHp") > 0)
        {
            hp = PlayerPrefs.GetFloat("CurrentHp");
        }
        else
        {
            hp = 5;
        }

        if (PlayerPrefs.GetFloat("CurrentStr") > 50)
        {
            str = PlayerPrefs.GetFloat("CurrentStr");
        }
        else
        {
            str = 50;
        }

    }

    public void ResetStatus()
    {
        str = 50f;
        str = PlayerPrefs.GetFloat("CurrentStr");
    }

    public void ResetHp()
    {
        hp = 5f;
        PlayerPrefs.SetFloat("CurrentHp", hp);
    }
}
