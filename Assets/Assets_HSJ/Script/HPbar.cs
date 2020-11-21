using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPbar : MonoBehaviour {
    public Sprite[] Heart;
    public Image HeartUI;
    private Player player;
    void Start()   {
        HeartUI= GameObject.FindGameObjectWithTag("HPUI").GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Player>();
    }
void Update()    {
        HeartUI.sprite = Heart[(int)player.HP];
    }
}
