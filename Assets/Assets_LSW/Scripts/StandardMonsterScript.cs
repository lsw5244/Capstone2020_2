using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// TODO : 아이템 랜덤 드랍되도록 하기
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class StandardMonsterScript : MonoBehaviour
{
    private Rigidbody2D rigi;
    private Animator ani;

    [SerializeField] float speed = 10f;
    private int moveDir = 1; // 처음 이동은 오른쪽으로 이동함
    private bool isAttack = false;
    [SerializeField] int maxHP = 100;
    private int currentHP;
    [SerializeField] Canvas hpCanvas;
    [SerializeField] Image hpImage;

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
        Vector2 frontVector = new Vector2(rigi.position.x + moveDir * 0.8f, rigi.position.y);
        RaycastHit2D hit = Physics2D.Raycast(frontVector, Vector3.down, 1);
        if (hit.collider == null)
        {
            moveDir *= -1;
            transform.Rotate(0, 180, 0);
            hpCanvas.transform.Rotate(0, 180, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "MONSTER")
        {
            moveDir *= -1;
            transform.Rotate(0, 180, 0);
            hpCanvas.transform.Rotate(0, 180, 0);
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
                    hpCanvas.transform.Rotate(0, 180, 0);
                }
                StartCoroutine("Attack");
            }
        }
    }

    public void GetDamage(int damage)
    {
        currentHP -= damage;
        hpImage.fillAmount = currentHP / maxHP;
        if (currentHP <= 0)
        {
            ani.SetTrigger("Death");
            Destroy(this.gameObject, 4);
            rigi.isKinematic = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator Attack()
    {
        isAttack = true;
        int temp = moveDir;
        moveDir = 0;
        ani.SetTrigger("Attack");
        //TODO : 공격 기능 쓰기
        yield return new WaitForSeconds(1.5f);
        moveDir = temp;
        isAttack = false;
    }
}
