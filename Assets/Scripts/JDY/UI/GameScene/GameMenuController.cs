using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GameMenuController : MonoBehaviour
{
    private Image background;
    private Color baseColor;
    private Color changeColor;

    public GameObject pausedMenu;
    public GameObject interactionText;

    public Button resumeButton;
    public Button settingsButton;
    public Button savequitButton;

    private float baseTimeScale;

    public AudioClip clickClip;

    private void Awake()
    {
        background = GetComponent<Image>();
        baseColor = background.color;
        changeColor = baseColor;
        changeColor.a = 0;
        pausedMenu?.SetActive(false);
        ButtonInitialization();
        background.color = changeColor;
        baseTimeScale = Time.timeScale;
    }

    private void ButtonInitialization()
    {
        resumeButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        savequitButton.onClick.RemoveAllListeners();

        resumeButton.onClick.AddListener(ResumeButtonOnClick);
        settingsButton.onClick.AddListener(SettingsButtonOnClick);
        savequitButton.onClick.AddListener(SaveQuitButtonOnClick);
    }

    private void OnDisable()
    {
        ResumeButtonOnClick();
    }

    private void Update()
    {
        if (pausedMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void SettingMenuOnOff()
    {
        //NOTE : 사운드 추가
        //AudioManager.Instance.PlaySFX(clickClip);

        Time.timeScale = Time.timeScale == 0 ? baseTimeScale : 0f;
        pausedMenu.SetActive(!pausedMenu.activeSelf);
        background.color = Mathf.Approximately(background.color.a, baseColor.a) ? changeColor : baseColor;
    }

    private void ResumeButtonOnClick()
    {
        //NOTE : 사운드 추가
        //AudioManager.Instance.PlaySFX(clickClip);
        background.color = changeColor;
        pausedMenu?.SetActive(false);
        Time.timeScale = baseTimeScale;
    }

    private void SettingsButtonOnClick()
    {
        //세팅 창이 열리게...
    }

    private void SaveQuitButtonOnClick()
    {
        //NOTE : 사운드 추가
        //AudioManager.Instance.PlaySFX(clickClip);

        if (DataManager.Instance.data != null)
        {
            //파일이 없으면 생성해서 저장
            DataManager.Instance.Save();
        }

        background.color = changeColor;

        //씬 전환
        UIManager.Instance?.TransitionToLoadScene("GameStartScene");
    }
}
