using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : SingletonManager<UIManager>
{
    public GameObject gameSceneUI;
    public GameObject startSceneUI;

    private TextMeshProUGUI flyText;
    private TextMeshProUGUI ammoText;


    private void Start()
    {
        gameSceneUI.SetActive(false);
        startSceneUI.SetActive(true);
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


}
