﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class StandardMonsterScript : MonoBehaviour
{
    private Rigidbody2D rigi;
    private Animator ani;

    [SerializeField] float speed = 10f;
    private int moveDir = 1; // 처음 이동은 오른쪽으로 이동함
    private bool isAttack = false;
    private bool isDeath = false;
    [SerializeField] float maxHP = 100;
    private float currentHP;
    [SerializeField] Canvas hpCanvas;
    [SerializeField] Image hpImage;
    [SerializeField] GameObject[] items;
    [SerializeField] float attackAnimationLength = 1.5f;

    void Start()
    {
        currentHP = maxHP;
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
        Vector2 frontVector = new Vector2(rigi.position.x + moveDir * 1f, rigi.position.y);
        RaycastHit2D hit = Physics2D.Raycast(frontVector, Vector3.down, 1);
        if (hit.collider == null)
        {
            moveDir *= -1;
            Debug.Log(moveDir);
            transform.Rotate(0, 180, 0);
            hpCanvas.transform.Rotate(0, 180, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        // 공격 받기
        if (coll.gameObject.tag == "ATTACK")
        {
            GetDamage(SaveManager.instance.Str);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (isDeath)
            return;

        if (coll.gameObject.tag == "MONSTER")
        {
            // 공격중일 땐 회전 X
            if (moveDir == 0)
                return;

            moveDir *= -1;
            transform.Rotate(0, 180, 0);
            hpCanvas.transform.Rotate(0, 180, 0);
           
            return;
        }

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
                    hpCanvas.transform.Rotate(0, 180, 0);
                    speed = 0;
                }
                speed = 2;
                StartCoroutine("Attack", coll);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (isDeath)
            return;

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
                    hpCanvas.transform.Rotate(0, 180, 0);
                }
                StartCoroutine("Attack", coll);
            }
        }
    }

    public void GetDamage(float damage)
    {
        currentHP -= damage;
        hpImage.fillAmount = currentHP / maxHP;
        ani.SetTrigger("Hurt");

        if (currentHP <= 0 && !isDeath)
        {
            moveDir = 0;
            isDeath = true;
            StopAllCoroutines();
            ani.SetTrigger("Death");
            Destroy(this.gameObject, 4);
            rigi.isKinematic = true;
            GetComponent<BoxCollider2D>().enabled = false;
            if (items == null)
                return;
            if((int)Random.Range(0, 5) == 0)
            {
                GameObject item = Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
                item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150f);
            }
        }
    }

    IEnumerator Attack(Collision2D coll)
    {
        isAttack = true;
        int temp = moveDir;
        moveDir = 0;
        ani.SetTrigger("Attack");
        coll.transform.gameObject.GetComponent<Player>().Hit();
        yield return new WaitForSeconds(attackAnimationLength);
        moveDir = temp;
        isAttack = false;
    }
    // 임시 몬스터를 위한 함수, 삭제 가능
    void spawnGas()
    {
        /*if (m_gas != null)
            Instantiate(m_gas, m_gasSpawnLocation);*/
    }
}