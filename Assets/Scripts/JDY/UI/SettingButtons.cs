using UnityEngine;
using UnityEngine.UI;

public class SettingButtons : MonoBehaviour
{
    public StartMenuController menuController;
    public MenuMoveControll moveController;

    public Button backButton;
    public Button setButton;
    public Slider bgmSlider;
    public Slider sfxSlider;


    private void Start()
    {
        //bgmSlider.value = AudioManager.Instance.BGM.volume;
        //sfxSlider.value = AudioManager.Instance.SFX.volume;
        ButtonInitialization();

        bgmSlider.value = AudioManager.Instance.BGMVolume;
        sfxSlider.value = AudioManager.Instance.SFXVolume;
    }

    private void ButtonInitialization()
    {
        backButton.onClick.RemoveAllListeners();
        setButton.onClick.RemoveAllListeners();

        backButton.onClick.AddListener(BackButtonClick);
        setButton.onClick.AddListener(SetButtonClick);
    }

    private void BackButtonClick()
    {
        moveController.MenuResetPosition();
        menuController.MenuChange(StartMenuController.StartMenus.main);
    }

    public void SetButtonClick()
    {
        #region 임시 수정
        AudioManager.Instance.BGMVolume = bgmSlider.value;
        AudioManager.Instance.SFXVolume = sfxSlider.value;
        AudioManager.Instance.BGM.volume = AudioManager.Instance.BGMVolume;
        AudioManager.Instance.SFX.volume = AudioManager.Instance.SFXVolume;
        #endregion

        //AudioManager.Instance.BGM.volume = bgmSlider.value;
        //AudioManager.Instance.SFX.volume = sfxSlider.value;
    }
}
