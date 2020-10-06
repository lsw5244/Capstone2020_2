using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
PLAYER테그 있는 오브젝트를 플레이어로 인식하여 공격, trace함

TODO : HP감소 자세하게 구현
*/
public class Monster_Slime : MonoBehaviour
{
    Animator ani;
    Rigidbody2D rigi;

    float moveDir = -1;
    public float speed = 3;
    bool isAttack = false;

    public GameObject lootItem;
    public Image HPBar;
    public Canvas HPBarCanvas;

    float maxHP = 100;
    float currentHP;

    void Start()
    {
        ani = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        //ani = GetComponentInChildren<Animator>();
        //rigi = GetComponentInChildren<Rigidbody2D>();
        currentHP = maxHP;
    }

    void Update()
    {
        /* if(Input.GetMouseButtonDown(0))
         {
             ani.SetBool("Walk", true);
         }
         else if(Input.GetMouseButtonUp(0))
         {
             ani.SetBool("Walk", false);
         }

         if(Input.GetMouseButtonDown(1))
         {
             ani.SetTrigger("Attack");
         }*/
        CheckFloor();
        PlayerCheck();
        rigi.velocity = new Vector2(moveDir * speed, rigi.velocity.y);
        if(Input.GetMouseButtonDown(0))
        {
            Die();
        }

        if(Input.GetMouseButtonDown(1))
        {
            GetDamage(10f);
        }
    }
    void CheckFloor()
    {
        // 바닥 체크
        Vector2 frontVector = new Vector2(rigi.position.x + moveDir * 0.8f, rigi.position.y);
        //Debug.DrawRay(frontVector, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D hit = Physics2D.Raycast(frontVector, Vector3.down, 1);
        if (hit.collider == null)
        {
            moveDir *= -1;
            transform.Rotate(0, 180, 0);
            HPBarCanvas.transform.Rotate(0, 180, 0);
        }
        ani.SetBool("Walk", true);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "PLAYER")
        {
            Debug.Log("Enter!!!!!!!!!!!!!!!!!");
            speed *= 2;
            // 플레이어가 오른쪽에 있는데 왼쪽으로 가는중이라면
            /*if((coll.transform.position.x > transform.position.x) && (moveDir == -1))
            {
                moveDir *= -1;
                transform.Rotate(0, 180, 0);
            }
            // 플레이어가 왼쪽에 있는데 오른쪽으로 가는 중이라면
            if((coll.transform.position.x < transform.position.x) && (moveDir == 1))
            {
                moveDir *= -1;
                transform.Rotate(0, 180, 0);
            }*/
            // 몬스터의 진행방향과 플레이어의 방향이 반대일 때
            if((coll.transform.position.x > transform.position.x) && (moveDir == -1)
                || (coll.transform.position.x < transform.position.x) && (moveDir == 1))
            {
                moveDir *= -1;
                transform.Rotate(0, 180, 0);
                HPBarCanvas.transform.Rotate(0, 180, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "PLAYER")
        {
            speed *= 0.5f;
        }
    }

    void Die()
    {
        GameObject item = Instantiate(lootItem, transform.position, Quaternion.identity);
        item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150f);
        Destroy(this.transform.parent.gameObject);
    }

    void PlayerCheck()
    {
        float radius = 0.5f;
        float Range = 0.5f;  // 원 넓이(공격 범위)
        //RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward, Range);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, new Vector2(1, 0) * moveDir, Range, LayerMask.GetMask("Player"));
        //RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(10, 1), 0f, new Vector2(0, 0), 10, LayerMask.GetMask("Player"));
        if(hits.Length > 0 && !isAttack)
        {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        isAttack = true;
        //StopAllCoroutines();
        float temp = moveDir;
        moveDir = 0;
        ani.SetTrigger("Attack");
        yield return new WaitForSeconds(1.0f);
        isAttack = false;
        moveDir = temp;
    }

    void GetDamage(float damage)
    {
        currentHP -= damage;
        HPBar.fillAmount = 0.5f;
        Debug.Log("@@@@@@@@@@@@@@@@@@");
    }
}