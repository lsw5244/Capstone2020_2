using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 스탯종류는 STR, DEX, DEF, LUK, MOV, CRT
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
        //Debug.Log(str);
    }

    private float dex;
    public float Dex
    {
        get { return dex; }
    }
    public void AddDex(float add)
    {
        dex += add;
    }

    private float def;
    public float Def
    {
        get { return def; }
    }
    public void AddDef(float add)
    {
        def += add;
    }

    private float luk;
    public float Luk
    {
        get { return luk; }
    }
    public void AddLuk(float add)
    {
        luk += add;
    }

    private float mov;
    public float Mov
    {
        get { return mov; }
    }
    public void AddMov(float add)
    {
        mov += add;
    }

    private float crt;
    public float Crt
    {
        get { return crt; }
    }
    public void AddCrt(float add)
    {
        crt += add;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        str = PlayerPrefs.GetFloat("CurrentStr");
        dex = PlayerPrefs.GetFloat("CurrentDex");
        luk = PlayerPrefs.GetFloat("CurrentLuk");
        mov = PlayerPrefs.GetFloat("CurrentMov");
        crt = PlayerPrefs.GetFloat("CurrentCrt");
    }

    void Update()
    {
        
    }
}
