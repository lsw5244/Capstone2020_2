using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RogueBoss : MonoBehaviour
{
    private Rigidbody2D rigi;
    private Animator ani;

    [SerializeField] float speed;
    private int moveDir = 1; // 처음 이동은 오른쪽으로 이동함
    [SerializeField] float maxHP = 100;
    private float currentHP;
    [SerializeField] Transform playerTr;
    [SerializeField] CircleCollider2D attackCollider;
    [SerializeField] GameObject bullet;
    [SerializeField] Vector3 projectionSpawnOffset;
    [SerializeField] Canvas hpCanvas;
    [SerializeField] Image hpImage;
    private bool isAttack = false;
    public bool playerCheck = false;
    private bool isDie = false;

    void Start()
    {
        currentHP = maxHP;
        Debug.Log(currentHP);
        rigi = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if (!playerCheck)
            return;
        if (isDie)
            return;
        if(Input.GetMouseButtonDown(0))
        {
            //ani.SetTrigger("Throw");
            //Die();
        }
        //if (!playerCheck)
          //  return;
        CheckPlayerPosition();
        if(!isAttack)
            rigi.velocity = new Vector2(moveDir * speed, rigi.velocity.y);
    }
    public void PlayerDetect()
    {
        playerCheck = true;
        // 걷도록 애니메이션 변경
        ani.SetBool("Grounded", true);
        ani.SetInteger("AnimState", 1);
        StartCoroutine("SpeedUp");
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
            hpCanvas.transform.Rotate(0, 180, 0);
        }
        else if (playerTr.position.x < transform.position.x) // 플레이어가 왼쪽에 있을 때
        {
            if (moveDir == -1)
                return;
            moveDir = -1;
            transform.Rotate(0, 180, 0);
            hpCanvas.transform.Rotate(0, 180, 0);
        }
    }
    IEnumerator Attack()
    {
        isAttack = true;
        ani.SetTrigger("Attack");
        yield return new WaitForSeconds(0.4f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        attackCollider.enabled = false;
        isAttack = false;
    }
    //돌진
    IEnumerator SpeedUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            speed *= 2;
            yield return new WaitForSeconds(3.0f);
            speed /= 2;
        }
    }

    public void TracePlayer()
    {
        ani.SetBool("Grounded", true);
        ani.SetInteger("AnimState", 1);
        playerCheck = true;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (isDie)
            return;

        if (coll.gameObject.tag == "ATTACK")
        {
            GetDamage(50);
        }

        if(coll.transform.tag == "PLAYER" && !isAttack)
        {
            StartCoroutine("Attack");
        }
    }
    private void OnCollisionStay2D(Collision2D coll)
    {
        if (isDie)
            return;
        if (coll.transform.tag == "PLAYER" && !isAttack)
        {
            StartCoroutine("Attack");
        }
    }
    void Die()
    {
        StopAllCoroutines();
        isDie = true;
        rigi.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<BoxCollider2D>().enabled = false;
        ani.SetTrigger("Death");
    }
    void GetDamage(float damage)
    {
        currentHP -= damage;
        hpImage.fillAmount = currentHP / maxHP;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    // Animation Event: Called in Rogue Throw animation.
    public void SpawnProjectile()
    {
        if (bullet != null)
        {
            // Set correct arrow spawn position
            Vector3 facingVector = new Vector3(moveDir, 1, 1);
            Vector3 projectionSpawnPosition = transform.localPosition + Vector3.Scale(projectionSpawnOffset, facingVector);
            //GameObject bolt = Instantiate(bullet, projectionSpawnPosition, gameObject.transform.localRotation) as GameObject;
            GameObject bolt = Instantiate(bullet, projectionSpawnPosition, transform.rotation) as GameObject;
            Destroy(bolt, 10f);
            // Turn arrow in correct direction
            bolt.transform.localScale = facingVector;
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (isDie)
            return;

        if (coll.gameObject.tag == "ATTACK")
        {
            GetDamage(SaveManager.instance.Str);
        }
    }
}
