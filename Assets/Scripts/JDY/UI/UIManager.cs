using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class UIManager : SingletonManager<UIManager>
{
    [SerializeField] private GameObject gameSceneUI;
    [SerializeField] private GameObject startSceneUI;
    [SerializeField] private GameObject gameBaseText;

    [SerializeField] private TextMeshProUGUI flyText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI pressText;
    [SerializeField] private TextMeshProUGUI interactionText;


    private void Start()
    {
        gameSceneUI.SetActive(false);
        gameBaseText.SetActive(false);
        startSceneUI.SetActive(true);
        GameSceneTextUpdate();
    }

    public void ChangeScene()
    {
        gameSceneUI.SetActive(!gameSceneUI.activeSelf);
        startSceneUI.SetActive(!startSceneUI.activeSelf);
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
