using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO : 애니메이션 넣기
public class Monster_Mushroom : MonoBehaviour
{

    Rigidbody2D rigi;
    int moveDir = 1;
    [SerializeField] float speed = 10f;
    float hp = 3;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        StartCoroutine("ChangeDir");
    }

    void Update()
    {
        CheckFloor();
        rigi.velocity = new Vector2(moveDir * speed, rigi.velocity.y);
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
        yield return new WaitForSeconds(1.0f);

       // moveDir = Random.Range(-1, 2);
        moveDir = Random.Range(0, 2) < 1 ? -1 : 1;
        // 반대방향 이동할 때 보는 방향 전환
        if((temp == 1 && moveDir == -1) || (temp == -1 && moveDir == 1))
        {
            transform.Rotate(0, 180, 0);
        }
        StartCoroutine("ChangeDir");
    }
}
