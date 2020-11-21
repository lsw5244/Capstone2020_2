using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerCheck : MonoBehaviour
{

    [SerializeField] GameObject boss;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "PLAYER")
        {
            boss.GetComponent<RogueBoss>().playerCheck = true;
            boss.GetComponent<RogueBoss>().TracePlayer();
            Destroy(this.gameObject);
        }
    }
}
