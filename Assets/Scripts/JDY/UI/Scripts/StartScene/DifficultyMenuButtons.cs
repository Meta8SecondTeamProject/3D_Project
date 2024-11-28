using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//난이도 관련 버튼들 관리
public class DifficultyMenuButtons : MonoBehaviour
{
    #region 난이도 관련(추후 삭제 가능성 있음)
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public Button extaemeButton;

    private void EasyButtonOnClick()
    {
        UIManager.Instance.ChangeScene();
    }
    private void NormalButtonOnClick()
    {
        UIManager.Instance.ChangeScene();
    }
    private void HardButtonOnClick()
    {
        UIManager.Instance.ChangeScene();
    }
    private void ExtaemeButtonOnClick()
    {
        UIManager.Instance.ChangeScene();
    }
    #endregion

    public StartMenuController menuController;
    public MenuMoveControll moveController;

    public Button backButton;

    private void Start()
    {
        ButtonInitialization();
    }

    private void ButtonInitialization()
    {
        easyButton.gameObject.SetActive(true);
        normalButton.gameObject.SetActive(true);
        hardButton.gameObject.SetActive(true);
        extaemeButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);

        //Hard 난이도를 클리어 했을 시 활성화
        //if (Data != null)
        //{
        //    extaemeButton.gameObject.SetActive(true);
        //    extaemeButton.onClick.RemoveAllListeners();
        //    extaemeButton.onClick.AddListener(ExtaemeButtonOnClick);
        //}

        easyButton.onClick.RemoveAllListeners();
        normalButton.onClick.RemoveAllListeners();
        hardButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();

        easyButton.onClick.AddListener(EasyButtonOnClick);
        normalButton.onClick.AddListener(NormalButtonOnClick);
        hardButton.onClick.AddListener(HardButtonOnClick);
        backButton.onClick.AddListener(BackButtonOnClick);
    }


    private void BackButtonOnClick()
    {
        moveController.MenuResetPosition();
        menuController.MenuChange(StartMenuController.StartMenus.main);
    }
}