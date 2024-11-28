using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//MainMenu�� ��ư�� ����
public class MainMenuButtons : MonoBehaviour
{
    public StartMenuController menuController;
    public MenuMoveControll moveController;

    public Button continueButton;
    public Button newGameButton;
    public Button settingsButton;
    public Button quitButton;

    private void Start()
    {
        ButtonInitialization();
    }

    private void ButtonInitialization()
    {
        continueButton.gameObject.SetActive(false);
        //����� �����Ͱ� ���� �� Ȱ��ȭ
        //if (Data != null)
        //{
        //    continueButton.gameObject.SetActive(true);
        //    continueButton.onClick.RemoveAllListeners();
        //    continueButton.onClick.AddListener(ContinueButtonOnClick);
        //}

        newGameButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        newGameButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        newGameButton.onClick.AddListener(NewGameButtonOnClick);
        settingsButton.onClick.AddListener(SettingsButtonOnClick);
        quitButton.onClick.AddListener(QuitButtonOnClick);
    }

    private void ContinueButtonOnClick()
    {

    }

    private void NewGameButtonOnClick()
    {
        moveController.MenuMovePosition();
        menuController.MenuChange(StartMenuController.StartMenus.difficulty);
    }

    private void SettingsButtonOnClick()
    {
        moveController.MenuMovePosition();
        menuController.MenuChange(StartMenuController.StartMenus.setting);
    }

    private void QuitButtonOnClick()
    {
        Application.Quit();
    }
}