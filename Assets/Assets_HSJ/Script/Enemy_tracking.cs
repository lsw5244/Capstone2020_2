using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_tracking : MonoBehaviour
{
    public int speed;
    Vector3 startPos;
    string dist;
    public float trackingLange;
    private void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position,trackingLange);
        Vector3 moveV = Vector3.zero;
        if (col.tag == "Player")
        {
            if (col.transform.position.x < transform.position.x)
                dist = "Left";
            else if (col.transform.position.x > transform.position.x)
                dist = "Right";
        }
        if (dist == "Left")
        {
            moveV = Vector3.left;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (dist == "Right")
        {
            moveV = Vector3.right;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        transform.position += moveV * speed * Time.deltaTime;
       
    }
}
