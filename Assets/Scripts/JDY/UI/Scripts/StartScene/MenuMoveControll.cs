using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class MenuMoveControll : MonoBehaviour
{
    public RectTransform backGround;
    public RectTransform menus;

    private Vector2 backGroundMovePosition;
    private Vector2 menusMovePosition;
    private Vector2 backGroundBasePosition;
    private Vector2 menusBasePosition;

    private const float moveValue = 150f;
    private const float moveTime = 0.5f;
    private float durationTime = 0f;
    private float moveSpeed = 0f;

    private void Start()
    {
        backGroundBasePosition = backGround.anchoredPosition;
        backGroundMovePosition = new Vector2(moveValue + backGround.anchoredPosition.x, backGround.anchoredPosition.y);
        menusBasePosition = menus.anchoredPosition;
        menusMovePosition = new Vector2((moveValue / 2f) + menus.anchoredPosition.x, menus.anchoredPosition.y);
    }

    public void MenuMovePosition()
    {
        StartCoroutine(MenuMovePositionCoroutine());
    }

    private IEnumerator MenuMovePositionCoroutine()
    {
        durationTime = 0f;
        while ((backGround.anchoredPosition - backGroundMovePosition).magnitude >= 0.1f)
        {
            durationTime += Time.deltaTime;
            moveSpeed = durationTime / moveTime;
            backGround.anchoredPosition = Vector2.Lerp(backGroundBasePosition, backGroundMovePosition, moveSpeed);
            menus.anchoredPosition = Vector2.Lerp(menusBasePosition, menusMovePosition, moveSpeed);
            yield return null;
        }
        backGround.anchoredPosition = backGroundMovePosition;
        menus.anchoredPosition = menusMovePosition;
    }

    public void MenuResetPosition()
    {
        StartCoroutine(MenuResetPositionCoroutine());
    }

    private IEnumerator MenuResetPositionCoroutine()
    {
        durationTime = 0f;
        while ((backGround.anchoredPosition - backGroundBasePosition).magnitude >= 0.1f)
        {
            durationTime += Time.deltaTime;
            moveSpeed = durationTime / moveTime;
            backGround.anchoredPosition = Vector2.Lerp(backGroundMovePosition, backGroundBasePosition, moveSpeed);
            menus.anchoredPosition = Vector2.Lerp(menusMovePosition, menusBasePosition, moveSpeed);
            yield return null;
        }
        menus.anchoredPosition = menusBasePosition;
        backGround.anchoredPosition = backGroundBasePosition;
    }
}
