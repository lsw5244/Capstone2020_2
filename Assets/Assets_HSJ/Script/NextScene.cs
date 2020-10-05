using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{ public bool canNextLV=false;
    public Animator mainCameraAnimator;

    private void OnTriggerEnter2D(Collider2D col)
    {
        OnTriggerStay2D(col);
    }
    private void OnTriggerStay2D(Collider2D col)
    {//맵을 이동하는데 조건을 부여하고 싶다면 if문에 추가
        if (col.tag=="Player") {
            canNextLV = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "Player")
        {
            canNextLV = false;
        }

    }
    public void selectLV(int LV)
    {
        if (canNextLV==true) {
            mainCameraAnimator.enabled = true;
        switch (LV)
        {
            case 0:
                StartCoroutine(WaitOneSecond(0));//menu
                break;
            case 1:
                StartCoroutine(WaitOneSecond(1));//LV1
                break;
            case 2:
                StartCoroutine(WaitOneSecond(2));//LV2
                break;
            case 3:
                StartCoroutine(WaitOneSecond(3));//LV3
                break;
            case 4:
                StartCoroutine(WaitOneSecond(4));//LV4
                break;
            case 5:
                StartCoroutine(WaitOneSecond(5));//LV5
                break;
        }
    }
    }
    IEnumerator WaitOneSecond(int LV)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(LV);
    }
   
}
