using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueBoss : MonoBehaviour
{
    private Rigidbody2D rigi;
    private Animator ani;

    [SerializeField] float speed = 10f;
    private int moveDir = 1; // 처음 이동은 오른쪽으로 이동함
    [SerializeField] float maxHP = 100;
    private float currentHP;
    [SerializeField] Transform playerTr;

    void Start()
    {
        currentHP = maxHP;
        rigi = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        // 걷도록 애니메이션 변경
        ani.SetBool("Grounded", true);
        ani.SetInteger("AnimState", 1);
    }

    void Update()
    {
        //CheckFloor();
        CheckPlayerPosition();
        rigi.velocity = new Vector2(moveDir * speed, rigi.velocity.y);
    }
    void CheckFloor()
    {
        Vector2 frontVector = new Vector2(rigi.position.x + moveDir * 1f, rigi.position.y);
        RaycastHit2D hit = Physics2D.Raycast(frontVector, Vector3.down, 1);
        if (hit.collider == null)
        {
            moveDir *= -1;
            transform.Rotate(0, 180, 0);
        }
    }
    void CheckPlayerPosition()
    {
        // 플레이어가 오른쪽에 있을 때
        if (playerTr.position.x > transform.position.x)
        {
            if (moveDir == 1)
                return;
            moveDir = 1;
            transform.Rotate(0, 180, 0);
        }
        else if (playerTr.position.x < transform.position.x) // 플레이어가 왼쪽에 있을 때
        {
            if (moveDir == -1)
                return;
            moveDir = -1;
            transform.Rotate(0, 180, 0);
        }
    }
}
