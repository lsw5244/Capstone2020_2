using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrItem : MonoBehaviour
{
    public float str;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "PLAYER")
        {
            SaveManager.instance.AddStr(str);
            Destroy(this.gameObject);
        }
        if (coll.transform.tag == "MONSTER")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "PLAYER")
        {
            SaveManager.instance.AddStr(str);
            Destroy(this.gameObject);
        }
       
    }
}
