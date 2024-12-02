using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���̵� ���� ��ư�� ����
[DisallowMultipleComponent]
public class DifficultyMenuButtons : MonoBehaviour
{
    #region ���̵� ����(���� ���� ���ɼ� ����)
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public Button extaemeButton;

    private void EasyButtonOnClick()
    {
        //UIManager.Instance.StartCoroutine(UIManager.Instance.Loading("BBH_Scene"));
        UIManager.Instance.TransitionToLoadScene("BBH_Scene");
    }
    private void NormalButtonOnClick()
    {
        //UIManager.Instance.StartCoroutine(UIManager.Instance.Loading("BBH_Scene"));
        UIManager.Instance.TransitionToLoadScene("BBH_Scene");

    }
    private void HardButtonOnClick()
    {
        //UIManager.Instance.StartCoroutine(UIManager.Instance.Loading("BBH_Scene"));
        UIManager.Instance.TransitionToLoadScene("BBH_Scene");

    }
    private void ExtaemeButtonOnClick()
    {
        //UIManager.Instance.StartCoroutine(UIManager.Instance.Loading("BBH_Scene"));
        UIManager.Instance.TransitionToLoadScene("BBH_Scene");

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

        //Hard ���̵��� Ŭ���� ���� �� Ȱ��ȭ
        //if (DataManager.Instance.data.isHardClear)
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