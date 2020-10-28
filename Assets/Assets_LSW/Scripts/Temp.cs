using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(SaveManager.saveManager.Str);
        }
        if(Input.GetMouseButtonUp(0))
        {
            SaveManager.saveManager.PlusStr(5f);
        }
    }
}
