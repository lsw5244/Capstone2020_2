using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrItem : MonoBehaviour
{
    public float str;
    private void OnCollisionEnter(Collision coll)
    {
        if(coll.transform.tag == "PLAYER")
        {
            SaveManager.instance.AddStr(str);
            Destroy(this.gameObject);
        }
    }
}
