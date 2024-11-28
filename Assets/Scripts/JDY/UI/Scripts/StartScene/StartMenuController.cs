using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class StartMenuController : MonoBehaviour
{
    public GameObject mainMenu;

    #region 삭제 가능성 있음
    public GameObject difficultyMenu;
    public GameObject settingMenu;
    #endregion

    private void OnEnable()
    {
        MenuInitialization(true);
    }

    private void MenuInitialization(bool mainControll)
    {
        mainMenu.SetActive(mainControll);
        difficultyMenu.SetActive(false);
        settingMenu.SetActive(false);
    }

    public void MenuChange(StartMenus menu)
    {
        MenuInitialization(false);

        switch (menu)
        {
            case StartMenus.main:
                mainMenu.SetActive(true);
                break;
            case StartMenus.difficulty:
                difficultyMenu.SetActive(true);
                break;
            case StartMenus.setting:
                settingMenu.SetActive(true);
                break;
            default:
                Debug.LogError("StartMenu / MenuChange / default");
                break;
        }
    }

    [SerializeField]
    public enum StartMenus
    {
        main,
        difficulty,
        setting
    }
}