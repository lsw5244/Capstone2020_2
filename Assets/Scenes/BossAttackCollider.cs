using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.transform.tag == "PLAYER")
        {
            coll.GetComponent<Player>().Hit();
        }
    }
}
