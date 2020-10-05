using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{   //조이스틱
    public Vector3 joystickVector;
    public RectTransform background, handle;
    //핸들이 이동할 수 있는 범위
    public float handleRange;

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out Vector2 localpoint)) {
            var sizebackgroundJoystic = background.sizeDelta;
            localpoint /= sizebackgroundJoystic;

            joystickVector = new Vector3(localpoint.x * 2 - 1, localpoint.y * 2 - 1, 0);
            joystickVector = joystickVector.magnitude > 1f ? joystickVector.normalized : joystickVector;

            float handlepos = sizebackgroundJoystic.x / 2 * handleRange;
            //핸들 위치
            handle.anchoredPosition = new Vector2(joystickVector.x * handlepos, joystickVector.y * handlepos);
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {//핸들을 놓았을 시에 원래자리로 돌아가게함
        joystickVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}
