using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonManager<UIManager>
{
    public GameObject gameSceneUI;
    public GameObject startSceneUI;

    private void Start()
    {
        gameSceneUI.SetActive(false);
        startSceneUI.SetActive(true);
    }
}
