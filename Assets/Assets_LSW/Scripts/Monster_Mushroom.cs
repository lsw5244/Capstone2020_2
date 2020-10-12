using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO : HP, 사망 모션 추가하기
// FIX : 공격 애니메이션 때 가스 나오는 위치 조절
public class Monster_Mushroom : MonoBehaviour
{

    Rigidbody2D rigi;
    Animator ani;

    int moveDir = 1;
    [SerializeField] float speed = 10f;
    float hp = 3;

    string attack = "Mushroom_Purple_Attack 1";
    string die = "Mushroom_Purple_Death 1";
    string hurt = "Mushroom_Purple_Hurt 1";
    string walk = "Mushroom_Purple_Walk 1";
    string currentState;

    bool isAttack = false;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        StartCoroutine("ChangeDir");
        ChangeAnimation(walk);
    }

    void Update()
    {
        CheckFloor();
        rigi.velocity = new Vector2(moveDir * speed, rigi.velocity.y);
        if(Input.GetMouseButtonDown(0))
        {
            if(!isAttack)
            {
                StopAllCoroutines();
                StartCoroutine("Attack");
            }        
        }
    }

    IEnumerator Attack()
    {
        isAttack = true;
        ChangeAnimation(attack);
        int temp = moveDir;
        moveDir = 0;
        yield return new WaitForSeconds(1.0f);
        ChangeAnimation(walk);
        moveDir = temp;
        StartCoroutine("ChangeDir");
        isAttack = false;
    }

    IEnumerator Die()
    {
        ani.Play(die);
        moveDir = 0;
        yield return new WaitForSeconds(2.0f);
        Destroy(GetComponentInParent<Transform>().gameObject);
    }

    void CheckFloor()
    {
        // 바닥 체크
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

    IEnumerator ChangeDir()
    {
        int temp = moveDir;
        yield return new WaitForSeconds(2.0f);

       // moveDir = Random.Range(-1, 2);
        moveDir = Random.Range(0, 2) < 1 ? -1 : 1;
        // 반대방향 이동할 때 보는 방향 전환
        if((temp == 1 && moveDir == -1) || (temp == -1 && moveDir == 1))
        {
            transform.Rotate(0, 180, 0);
        }
        StartCoroutine("ChangeDir");
    }

    void ChangeAnimation(string State)
    {
        if (currentState == State)
            return;
        currentState = State;
        ani.Play(State);
    }
}
