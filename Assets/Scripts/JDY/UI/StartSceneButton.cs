using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//버튼위에 마우스가 올라갔을 떄 나갔을 떄 호출되는 이벤트들
[RequireComponent(typeof(Button), typeof(Image))]
public class StartSceneButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color baseColor;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        baseColor = image.color;
        image.color *= 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = baseColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color *= 0;
    }



}
