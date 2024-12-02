using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class UIManager : SingletonManager<UIManager>
{

    //start씬을 빼게 될경우
    //
    [SerializeField] private GameObject gameSceneUI;
    [SerializeField] private GameObject startSceneUI;
    [SerializeField] private GameObject loadingSceneUI;
    [SerializeField] private GameObject gameBaseText;

    [SerializeField] private TextMeshProUGUI flyText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI pressText;
    [SerializeField] private TextMeshProUGUI interactionText;

    private CurrentScene currentScene;

    private void Start()
    {
        ChangeScene();
    }

    private void CurrentSceneUI()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        switch (index)
        {
            case 0:
                currentScene = CurrentScene.Start;
                break;
            case 1:
                currentScene = CurrentScene.End;
                break;
            case 2:
                currentScene = CurrentScene.Loading;
                break;
            default:
                currentScene = CurrentScene.Game;
                break;
        }

    }

    public void ChangeScene()
    {
        CurrentSceneUI();

        gameSceneUI.SetActive(false);
        startSceneUI.SetActive(false);
        loadingSceneUI.SetActive(false);

        switch (currentScene)
        {
            case CurrentScene.Start:
                startSceneUI.SetActive(true);
                break;
            case CurrentScene.Game:
                gameSceneUI.SetActive(true);
                break;
            case CurrentScene.End:
                //아직 없음.
                break;
            case CurrentScene.Loading:
                loadingSceneUI.SetActive(true);
                break;
            default:
                Debug.LogError("UIManager / ChangeScene");
                break;
        }
    }

    public void GameSceneTextUpdate()
    {
        flyText.text = DataManager.Instance.money.ToString();
        ammoText.text = DataManager.Instance.ammo.ToString();
    }

    public void ChangeInteractionText(string str)
    {
        interactionText.text = str;
    }

    public void OnOffInteractionText()
    {
        gameBaseText.SetActive(!gameBaseText.activeSelf);
        if (gameBaseText.activeSelf == false)
        {
            interactionText = null;
        }
    }
}
[Serializable]
public enum CurrentScene
{
    Start,
    Game,
    End,
    Loading
}
