using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GameMenuController : MonoBehaviour
{
    private Image background;
    private Color baseColor;

    public GameObject pausedMenu;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        //���� â�� ������...
    }

    private void SaveQuitButtonOnClick()
    {
        //�����͸� ������ ����� �߰��� ��
        //if (Data != null)
        //{
        //    //����...
        //    //������ ������ �����ؼ� ����...
        //}

        background.color *= 0;

        //�� ��ȯ�ϰ�...

        UIManager.Instance.ChangeScene();
    }

}
