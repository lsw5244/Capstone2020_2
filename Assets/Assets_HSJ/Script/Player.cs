using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerState
{
    //플레이어 상태

    public bool isground = false;
    public bool isAttack = false;
    public bool isAlive = true;
    public bool conv;
    [SerializeField] bool ondmg = false;
    public GM gm;
    public Joystick joystick;
    Rigidbody2D rb;
    public float walkcooltime;
    //사운드
    AudioSource audioSource;
   public AudioClip jumpSound;
   public AudioClip walkSound;
    public AudioClip[] attackSound;
    //애니메이션
    string playerAnimationState;
    Animator animator;

    string idle = "HeroKnight_Idle";
    string jump = "HeroKnight_Jump";
    string jumpToFall = "HeroKnight_Fall";
    string run = "HeroKnight_Run";
    string attack = "HeroKnight_Attack1";
    string attack2 = "HeroKnight_Attack2";
    string attack3 = "HeroKnight_Attack3";
    string death = "HeroKnight_Death";
    string hurt = "HeroKnight_Hurt";

    //공격
    public GameObject attackCollider;
    public float attackcooltime;
    public int combo;
    private void Start()
    {
        gm = FindObjectOfType<GM>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        HP = SaveManager.instance.HP;
    }
    private void Update()
    {
        if (HP <= 0)
        {
            isAlive = false;
            gm.gameOver(true);
            changeAnimation(death);
        }
        if (isground == true && isAttack == false&&isAlive==true)
        {
            if (joystick.joystickVector.x != 0)
            {
                if (conv == true)
                {
                   
                }
                if (walkcooltime > 0.4f)
                {
                    audioSource.PlayOneShot(walkSound, 1);
                    walkcooltime = 0;
                }
                changeAnimation(run);
            }
            else
            {
                changeAnimation(idle);
            }
            if (walkcooltime <= 0.6f)
            {
                walkcooltime += Time.deltaTime;
            }
        }
        if (isAlive==true&& isground == false && isAttack == false)
        {
            if (rb.velocity.y > 0 )
            {
                changeAnimation(jump);
            }
            else if (rb.velocity.y < 0)
            {
                changeAnimation(jumpToFall);
            }
        }
      
        if (attackcooltime <= 1)
        {
            attackcooltime += Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {//이동
        if (joystick.joystickVector.x > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if (joystick.joystickVector.x < 0) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        rb.velocity = new Vector2(joystick.joystickVector.x * speed * Time.deltaTime, rb.velocity.y);
    }
    //점프
    public void Jump() {
        if (rb.velocity.y>=0&&isground) {
            rb.AddForce(Vector2.up * jumpPower);     
            audioSource.PlayOneShot(jumpSound);
        }
    }
    //공격
    public void Attack()
    {
        if (attackcooltime >= 0.5f) {
            isAttack = true;
            attackcooltime = 0;
            attackCollider.SetActive(true);
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (combo == 0)
            {
                changeAnimation(attack);
                audioSource.PlayOneShot(attackSound[0]);
                combo++;
            }
            else if (combo == 1)
            {
                changeAnimation(attack2);
                audioSource.PlayOneShot(attackSound[1]);
                combo++;
            }
            else {
                changeAnimation(attack3);
                audioSource.PlayOneShot(attackSound[2]);
                combo = 0;
            }
            Invoke("attackDelay", 0.3f);
        }
        else
        {
            return;
        }
    }
    void attackDelay()
    {
        isAttack = false;
        attackCollider.SetActive(false);
    }
    //피격
    public void Hit() {
        changeAnimation(hurt);
        HP--;
        SaveManager.instance.SaveHp(HP);
        Debug.Log(HP);
        onDMG();
    }

    void onDMG()
    {
        ondmg = true;
        //hitsound();
        gameObject.layer = 9;//레이어 12로 변경
        rb.AddForce(new Vector2(0, 2f), ForceMode2D.Impulse);//forcemode2D.impulse==충돌하면 지정된 힘을 가함
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        Invoke("offDMG", 2.0f);//2초 있다가 off데미지로 이동
    }
    void offDMG()
    {
        gameObject.layer = 8;//레이어 원래대로
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        ondmg = false;
    }

    public void Heal(float point)
    {
        if (HP <= 5)
            return;
        HP += point;
        SaveManager.instance.SaveHp(HP);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.tag == "MONSTER") {
        //    Hit();
        //}
    }

    void changeAnimation(string state)
    {
        if (state == playerAnimationState)
        {
            return;
        }
        animator.Play(state);
        playerAnimationState = state;
    }

}
