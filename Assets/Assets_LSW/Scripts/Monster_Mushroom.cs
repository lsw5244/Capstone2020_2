using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO : 애니메이션 넣기
public class Monster_Mushroom : MonoBehaviour
{

    Rigidbody2D rigi;
    int moveDir = 1;
    [SerializeField] float speed = 10f;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
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
        yield return new WaitForSeconds(2.0f);

        moveDir = Random.Range(-1, 2);
        StartCoroutine("ChangeDir");
    }
}
