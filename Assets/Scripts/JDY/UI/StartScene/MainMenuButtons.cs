using UnityEngine;
using UnityEngine.UI;

//MainMenu의 버튼들 관리
[DisallowMultipleComponent]
public class MainMenuButtons : MonoBehaviour
{
    public StartMenuController menuController;
    public MenuMoveControll moveController;

    public Button continueButton;
    public Button newGameButton;
    public Button settingsButton;
    public Button quitButton;

    public AudioClip clickClip;
    private void Start()
    {
        ButtonInitialization();
    }

    public void ButtonInitialization()
    {
        continueButton.gameObject.SetActive(false);
        //저장된 데이터가 있을 떄 활성화
        if (DataManager.Instance.data.isPlaying == true)
        {
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(ContinueButtonOnClick);
        }

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
        //NOTE : 사운드 추가
        AudioManager.Instance.PlaySFX(clickClip);
        UIManager.Instance.TransitionToLoadScene(DataManager.Instance.data.currentSceneName);
    }


    private void NewGameButtonOnClick()
    {
        //NOTE : 사운드 추가
        AudioManager.Instance.PlaySFX(clickClip);
        moveController.MenuMovePosition();
        menuController.MenuChange(StartMenuController.StartMenus.difficulty);
    }

    private void SettingsButtonOnClick()
    {
        //NOTE : 사운드 추가
        AudioManager.Instance.PlaySFX(clickClip);
        moveController.MenuMovePosition();
        menuController.MenuChange(StartMenuController.StartMenus.setting);
    }

    private void QuitButtonOnClick()
    {
        //NOTE : 사운드 추가
        AudioManager.Instance.PlaySFX(clickClip);
        Application.Quit();
    }
}