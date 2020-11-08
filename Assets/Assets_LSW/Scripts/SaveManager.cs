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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            str = 50f;
            PlayerPrefs.SetFloat("CurrentStr", str);
        }
    }
    void Start()
    {
        str = PlayerPrefs.GetFloat("CurrentStr");
    }

    public void ResetStatus()
    {
        str = 50f;
        str = PlayerPrefs.GetFloat("CurrentStr");
    }
}
