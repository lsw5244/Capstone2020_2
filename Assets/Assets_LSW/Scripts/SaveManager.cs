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
        Debug.Log(hp);
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            str = 50f;
            hp = 5f;
            PlayerPrefs.SetFloat("CurrentStr", str);
            PlayerPrefs.SetFloat("CurrentHp", hp);
        }
    }
    void Start()
    {
        str = PlayerPrefs.GetFloat("CurrentStr");
        hp = PlayerPrefs.GetFloat("CurrentHp");
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
