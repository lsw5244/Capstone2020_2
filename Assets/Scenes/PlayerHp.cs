using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    
    void Start()
    {
        player.GetComponent<Player>().HP = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
