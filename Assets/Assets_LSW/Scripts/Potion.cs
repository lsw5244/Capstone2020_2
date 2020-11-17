using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public float hpPoint;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.transform.tag == "PLAYER")
        {
            coll.gameObject.GetComponent<Player>().Heal(hpPoint);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "PLAYER")
        {
            coll.gameObject.GetComponent<Player>().Heal(hpPoint);
            Destroy(this.gameObject);
        }
    }

}
