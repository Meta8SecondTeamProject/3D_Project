using UnityEngine;
using UnityEngine.UI;

//MainMenu�� ��ư�� ����
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
        //����� �����Ͱ� ���� �� Ȱ��ȭ
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
        //NOTE : ���� �߰�
        AudioManager.Instance.PlaySFX(clickClip);
        UIManager.Instance.TransitionToLoadScene(DataManager.Instance.data.currentSceneName);
    }


    private void NewGameButtonOnClick()
    {
        //NOTE : ���� �߰�
        AudioManager.Instance.PlaySFX(clickClip);
        moveController.MenuMovePosition();
        menuController.MenuChange(StartMenuController.StartMenus.difficulty);
    }

    private void SettingsButtonOnClick()
    {
        //NOTE : ���� �߰�
        AudioManager.Instance.PlaySFX(clickClip);
        moveController.MenuMovePosition();
        menuController.MenuChange(StartMenuController.StartMenus.setting);
    }

    private void QuitButtonOnClick()
    {
        //NOTE : ���� �߰�
        AudioManager.Instance.PlaySFX(clickClip);
        Application.Quit();
    }
}