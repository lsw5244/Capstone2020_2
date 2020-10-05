using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        OnTriggerStay2D(col);
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("TILE"))
        {
            player.isground = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("TILE"))
        {
            player.isground = false;
        }
    }
}