using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GameMenuController : MonoBehaviour
{
    public GameObject pausedMenu;

    public Button resumeButton;
    public Button settingsButton;
    public Button savequitButton;

    private float baseTimeScale;

    private void Start()
    {
        pausedMenu.SetActive(false);
        ButtonInitialization();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            baseTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            pausedMenu.SetActive(true);
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

        //�� ��ȯ�ϰ�...

        UIManager.Instance.ChangeScene();
    }

}
