using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JanetTalkManager : MonoBehaviour
{
    //해당 스크립트는 노가다용 코드로 NPC당 하나당 1개의 파일로 복사해서 사용해야하는 스크립트 입니다.

    //----------------------------------반드시 넣어야함-------------------------------------
    public GameObject canvas; //껐다 켤 캔버스 UI
    public GameObject canvas2;
    public GameObject player;
    public Text text; //글자 바꿀 텍스트 UI
    public Image CharaterUi; //이미지 바꿀 UI
    public Sprite[] Charaterimage; //캐릭터 이미지파일
    //-------------------------------------------------------------------------------------


    bool isPlayerCheck = false; //플레이어 충돌 체크
    bool isTalking = false; //플레이어 대화중 체크
    int i = 0;



    void Update()
    {
        //UI 껐다 켜기
        if (isPlayerCheck && Input.GetMouseButtonDown(0) && !isTalking)
        {
            canvas.SetActive(!canvas.activeSelf);
            if (canvas.activeSelf) //UI가 켜져있을때
            {
                isTalking = true;
            }
            else //UI가 꺼져있을때
            {
                isTalking = false;
            }
        }

        //-----------------------------------------------------------------코드 바꾸면 되는 곳 ----------------------------------------------------------------

        if (isTalking && Input.GetMouseButtonDown(0)) //대화
        {
            if (i == 0) //--------대화마다 숫자 임의로 늘려 주기-----------
            {
                canvas2.SetActive(false);
                player.GetComponent<Player>().joystickVector = 0;
                player.GetComponent<Player>().changeAnimation(player.GetComponent<Player>().idle);
                text.text = "흐응? 내가 누구냐고?"; // 복붙으로 대화 입력
                CharaterUi.sprite = Charaterimage[0]; // 표정 바꾸기
            }
            if (i == 1)
            {
                text.text = "내 이름은 자넷. 대마법사 알렌의 직계후손이야";
                CharaterUi.sprite = Charaterimage[1]; // 표정 바꾸기
            }
            if (i == 2)
            {
                text.text = "세나를 도와서 미궁수호를 돕고 있어.";
                CharaterUi.sprite = Charaterimage[2]; // 표정 바꾸기
                isTalking = false; //마지막 대화 시 넣는 코드.
                canvas2.SetActive(true);
                player.GetComponent<Player>().joystickVector = 1;
                player.GetComponent<Player>().changeAnimation(player.GetComponent<Player>().idle);
            }

            //----------------------------------------------------------------------------------------------------------------------------------------------------

            //마지막 대화 체크 후, 초기 맨처음 대사로 돌림.
            if (!isTalking)
            {
                i = 0;
            }
            else
            {
                i++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //플레이어가 부딪칠때
    {
        if (collision.transform.tag.Equals("PLAYER")) //태그 변경 주의
        {
            isPlayerCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //플레이어가 안 부딪칠때
    {
        if (collision.transform.tag.Equals("PLAYER")) //태그 변경 주의
        {
            isPlayerCheck = false;
        }
    }
}
