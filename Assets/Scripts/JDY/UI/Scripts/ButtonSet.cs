using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//버튼위에 마우스가 올라갔을 떄 나갔을 떄 호출되는 이벤트들
[RequireComponent(typeof(UnityEngine.UI.Button), typeof(Image))]
public class ButtonSet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        image.color *= 0;
    }
}
