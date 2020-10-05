using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerState
{
    public Joystick joystick;
    Rigidbody2D rb;
   public bool isground =false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (HP<=0) {
            //죽으면 게임오버? 다시하기?
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
        if (rb.velocity.y>=0&&isground==true) {
            rb.AddForce(Vector2.up * jumpPower);
        }
    }
    //공격
    public void Attack() {
        //공격했을 시 속도를 0으로 하여 움직이지 못하게 시도해봄
        rb.velocity = new Vector2(0, rb.velocity.y);


    }
    //피격
    public void Hit() {

    }
}
