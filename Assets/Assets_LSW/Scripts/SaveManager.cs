using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    static public SaveManager saveManager;
    private float str = 10f;
    public float Str
    {
        get { return str; } 
    }
    public void PlusStr(float add)
    {
        str += add;
        //Debug.Log(str);
    }

    float dex;
    float def;
    float luk;
    float mov;
    float crt;

    private void Awake()
    {
        if(saveManager == null)
        {
            saveManager = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
