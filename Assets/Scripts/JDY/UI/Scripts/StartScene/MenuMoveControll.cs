using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuMoveControll : MonoBehaviour
{
    public RectTransform backGround;
    public RectTransform menus;

    public float moveValue;

    private Vector2 movePosition;
    private Vector2 basePosition;
    public float speed;

    private void Start()
    {
        basePosition = backGround.anchoredPosition;
        movePosition = new Vector2(moveValue, backGround.anchoredPosition.y);
    }

    [ContextMenu("Test/MenuMove")]
    public void MenuMove()
    {
        //backGround.anchoredPosition = movePosition;
        //while (true)
        //{
        //    backGround.anchoredPosition = Vector2.Lerp(backGround.anchoredPosition, movePosition, speed * Time.deltaTime);
        //    if ((backGround.anchoredPosition - movePosition).magnitude <= 0.5f)
        //    {
        //        backGround.anchoredPosition = movePosition;
        //        break;
        //    }
        //}

        StartCoroutine(MenuMoveCoroutine());
    }

    private IEnumerator MenuMoveCoroutine()
    {
        while (true)
        {
            backGround.anchoredPosition = Vector2.Lerp(backGround.anchoredPosition, movePosition, speed * Time.deltaTime);
            if ((backGround.anchoredPosition - movePosition).magnitude <= 0.5f)
            {
                backGround.anchoredPosition = movePosition;
                break;
            }
            yield return null;
        }
    }

    [ContextMenu("Test/MenuReset")]
    public void MenuReset()
    {

    }
}
