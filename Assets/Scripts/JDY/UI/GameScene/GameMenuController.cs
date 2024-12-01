using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GameMenuController : MonoBehaviour
{
    private Image background;
    private Color baseColor;

    public GameObject pausedMenu;
    public GameObject interactionText;

    public Button resumeButton;
    public Button settingsButton;
    public Button savequitButton;

    private float baseTimeScale;

    private void Start()
    {
        background = GetComponent<Image>();
        baseColor = background.color;
        pausedMenu.SetActive(false);
        ButtonInitialization();
        background.color *= 0;
        baseTimeScale = Time.timeScale;
        interactionText.SetActive(false);
    }

    private void OnDisable()
    {
        //pausedMenu.SetActive(false);
        ResumeButtonOnClick();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Application.isFocused == false && pausedMenu.activeSelf==false))
        {
            Time.timeScale = Time.timeScale == 0 ? baseTimeScale : 0f;
            print(Time.timeScale);
            pausedMenu.SetActive(!pausedMenu.activeSelf);
            background.color = background.color == baseColor ? background.color * 0f : baseColor;
        }
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

    private void ResumeButtonOnClick()
    {
        background.color *= 0;
        pausedMenu.SetActive(false);
        Time.timeScale = baseTimeScale;
    }

    private void SettingsButtonOnClick()
    {
        //세팅 창이 열리게...
    }

    private void SaveQuitButtonOnClick()
    {
        //데이터를 저장할 기능을 추가할 시
        if (DataManager.Instance.data != null)
        {
            //저장
            //파일이 없으면 생성해서 저장
            DataManager.Instance.Save();
        }

        background.color *= 0;

        //씬 전환하고...
        //UIManager.Instance.StartCoroutine(UIManager.Instance.Loading("GameStartScene"));
        UIManager.Instance.TransitionToLoadScene("GameStartScene");
    }

}
