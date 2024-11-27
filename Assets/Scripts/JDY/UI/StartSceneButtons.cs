using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//StartScene�� ��ư�� ����
public class StartSceneButtons : MonoBehaviour
{
    public Button continueButton;
    public Button newGameButton;
    public Button settingsButton;
    public Button quitButton;

    private void Start()
    {
        continueButton.gameObject.SetActive(false);
        ButtonInitialization();
    }

    private void ButtonInitialization()
    {
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

    }

    private void SettingsButtonOnClick()
    {

    }

    private void QuitButtonOnClick()
    {

    }
}