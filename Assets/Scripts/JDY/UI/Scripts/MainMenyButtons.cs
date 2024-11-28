using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//StartScene의 버튼들 관리
public class MainMenyButtons : MonoBehaviour
{
    public UnityEngine.UI.Button continueButton;
    public UnityEngine.UI.Button newGameButton;
    public UnityEngine.UI.Button settingsButton;
    public UnityEngine.UI.Button quitButton;

    private void Start()
    {
        continueButton.gameObject.SetActive(false);
        ButtonInitialization();
    }

    private void ButtonInitialization()
    {
        //저장된 데이터가 있을 떄 활성화
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