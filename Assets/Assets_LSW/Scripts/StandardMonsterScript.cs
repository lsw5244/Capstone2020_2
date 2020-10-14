using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class StandardMonsterScript : MonoBehaviour
{
    Rigidbody2D rigi;
    Animator ani;

    [SerializeField] float speed = 10f;
    int moveDir = 1;
    bool isAttack = false;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        CheckFloor();
        rigi.velocity = new Vector2(moveDir * speed, rigi.velocity.y);
    }

    void CheckFloor()
    {
        Vector2 frontVector = new Vector2(rigi.position.x + moveDir * 0.8f, rigi.position.y);
        RaycastHit2D hit = Physics2D.Raycast(frontVector, Vector3.down, 1);
        if (hit.collider == null)
        {
            moveDir *= -1;
            transform.Rotate(0, 180, 0);
            StopAllCoroutines();
            StartCoroutine("ChangeDir");
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "PLAYER")
        {
            if (!isAttack)
            {
                StopAllCoroutines();
                // 공격 시 플레이어 바라보도록 함
                if ((coll.transform.position.x > transform.position.x) && (moveDir == -1)
                    || (coll.transform.position.x < transform.position.x) && (moveDir == 1))
                {
                    moveDir *= -1;
                    transform.Rotate(0, 180, 0);
                }
                StartCoroutine("Attack");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.tag == "PLAYER")
        {
            if (!isAttack)
            {
                StopAllCoroutines();
                // 공격 시 플레이어 바라보도록 함
                if ((coll.transform.position.x > transform.position.x) && (moveDir == -1)
                    || (coll.transform.position.x < transform.position.x) && (moveDir == 1))
                {
                    moveDir *= -1;
                    transform.Rotate(0, 180, 0);
                }
                StartCoroutine("Attack");
            }
        }
    }

    IEnumerator Attack()
    {
        isAttack = true;
        int temp = moveDir;
        moveDir = 0;
        ani.SetTrigger("Attack");
        yield return new WaitForSeconds(1.5f);
        moveDir = temp;
        isAttack = false;
    }
}
