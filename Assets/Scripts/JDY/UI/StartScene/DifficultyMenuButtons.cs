using UnityEngine;
using UnityEngine.UI;

//난이도 관련 버튼들 관리
[DisallowMultipleComponent]
public class DifficultyMenuButtons : MonoBehaviour
{
	#region 난이도 관련(추후 삭제 가능성 있음)
	public Button easyButton;
	public Button normalButton;
	public Button hardButton;
	public Button extaemeButton;

	private void EasyButtonOnClick()
	{
		DataManager.Instance.fliesMaxSpawnCount = 30;
		DataManager.Instance.fishMaxSpawnCount = 10;
		DataManager.Instance.birdMaxSpawnCount = 4;
		DataManager.Instance.birdBlackMaxSpawnCount = 4;
		DataManager.Instance.fishKillCount = 16;
		DataManager.Instance.birdKillCount = 16;
		DataManager.Instance.difficulty = Difficulty.EASY;
		DataManager.Instance.NewGame();
		UIManager.Instance.TransitionToLoadScene("BBH_Scene");
	}
	private void NormalButtonOnClick()
	{
		DataManager.Instance.fliesMaxSpawnCount = 30;
		DataManager.Instance.fishMaxSpawnCount = 15;
		DataManager.Instance.birdMaxSpawnCount = 6;
		DataManager.Instance.birdBlackMaxSpawnCount = 6;
		DataManager.Instance.fishKillCount = 20;
		DataManager.Instance.birdKillCount = 20;
        DataManager.Instance.difficulty = Difficulty.NORMAL;
        DataManager.Instance.NewGame();
		UIManager.Instance.TransitionToLoadScene("BBH_Scene");
	}
	private void HardButtonOnClick()
	{
		DataManager.Instance.fliesMaxSpawnCount = 30;
		DataManager.Instance.fishMaxSpawnCount = 20;
		DataManager.Instance.birdMaxSpawnCount = 10;
		DataManager.Instance.birdBlackMaxSpawnCount = 10;
		DataManager.Instance.fishKillCount = 25;
		DataManager.Instance.birdKillCount = 25;
        DataManager.Instance.difficulty = Difficulty.HARD;
        DataManager.Instance.NewGame();
		UIManager.Instance.TransitionToLoadScene("BBH_Scene");
	}
	private void ExtaemeButtonOnClick()
	{
		DataManager.Instance.fliesMaxSpawnCount = 30;
		DataManager.Instance.fishMaxSpawnCount = 40;
		DataManager.Instance.birdMaxSpawnCount = 20;
		DataManager.Instance.birdBlackMaxSpawnCount = 20;
		DataManager.Instance.fishKillCount = 50;
		DataManager.Instance.birdKillCount = 50;
		DataManager.Instance.bombFliesSpeed = 10;
		DataManager.Instance.fishSpeed = 30;
		DataManager.Instance.birdSpeed = 20;
        DataManager.Instance.difficulty = Difficulty.EXTREAM;
        DataManager.Instance.NewGame();
		UIManager.Instance.TransitionToLoadScene("BBH_Scene");
	}
	#endregion

	public StartMenuController menuController;
	public MenuMoveControll moveController;

	public Button backButton;

	private void OnEnable()
	{
		ButtonInitialization();
	}

	private void ButtonInitialization()
	{
		easyButton.gameObject.SetActive(true);
		normalButton.gameObject.SetActive(true);
		hardButton.gameObject.SetActive(true);
		extaemeButton.gameObject.SetActive(false);
		backButton.gameObject.SetActive(true);

		//Hard 난이도를 클리어 했을 시 활성화
		if (DataManager.Instance.data.isHardClear)
		{
			extaemeButton.gameObject.SetActive(true);
			extaemeButton.onClick.RemoveAllListeners();
			extaemeButton.onClick.AddListener(ExtaemeButtonOnClick);
		}

		easyButton.onClick.RemoveAllListeners();
		normalButton.onClick.RemoveAllListeners();
		hardButton.onClick.RemoveAllListeners();
		backButton.onClick.RemoveAllListeners();

		easyButton.onClick.AddListener(EasyButtonOnClick);
		normalButton.onClick.AddListener(NormalButtonOnClick);
		hardButton.onClick.AddListener(HardButtonOnClick);
		backButton.onClick.AddListener(BackButtonOnClick);
	}


	private void BackButtonOnClick()
	{
		moveController.MenuResetPosition();
		menuController.MenuChange(StartMenuController.StartMenus.main);
	}
}