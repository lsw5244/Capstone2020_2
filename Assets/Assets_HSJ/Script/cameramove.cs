using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour
{
    private Vector2 velocity;
 
    public float Y;
    public float X;
    public GameObject player;
    
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, X);//X축 따라오는 속도
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, Y);//Y축 따라오는 속도
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
