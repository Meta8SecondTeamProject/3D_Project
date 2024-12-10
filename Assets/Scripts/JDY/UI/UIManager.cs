using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class UIManager : SingletonManager<UIManager>
{
	[SerializeField] private GameObject gameSceneUI;
	[SerializeField] private GameObject startSceneUI;
	[SerializeField] private GameObject loadingSceneUI;
	[SerializeField] private LoadingController loadingController;
	[SerializeField] private GameObject gameBaseText;
	[SerializeField] private MainMenuButtons mainButtons;

	[SerializeField] private TextMeshProUGUI flyText;
	[SerializeField] private TextMeshProUGUI ammoText;
	[SerializeField] private TextMeshProUGUI pressText;
	public TextMeshProUGUI interactionText;

	public GameMenuController gameMenuController;

	public CurrentScene currentScene;

	private void Start()
	{
		ChangeScene();
		OnOffInteractionText(false);
		GameSceneTextUpdate();
	}

	private void CurrentSceneUI()
	{
		int index = SceneManager.GetActiveScene().buildIndex;
		//Debug.Log($"�� ���� �ε��� {index}");
		switch (index)
		{
			case 0:
				currentScene = CurrentScene.Start;
				Cursor.visible = true;
				//Debug.Log("Start �� ��������� ����");
				break;
			case 1:
				currentScene = CurrentScene.End;
				//Debug.Log("End �� �ε� ��������� ����");
				break;
			case 2:
				currentScene = CurrentScene.Loading;
				//Debug.Log("Loading �ε� �� ��������� ����");
				break;
			default:
				currentScene = CurrentScene.Game;
				//Debug.Log("Game �� �ε� ��������� ����");
				break;
		}
	}

	public void ChangeScene()
	{
		//Debug.Log("ChangeScene ȣ���");
		gameSceneUI.SetActive(false);
		startSceneUI.SetActive(false);
		loadingSceneUI.SetActive(false);

		CurrentSceneUI();

		switch (currentScene)
		{
			case CurrentScene.Start:
				startSceneUI.SetActive(true);
				mainButtons.ButtonInitialization();
				Cursor.lockState = CursorLockMode.None;
				break;
			case CurrentScene.Game:
				gameSceneUI.SetActive(true);
				GameManager.Instance.PlayerInstantiate();
				break;
			case CurrentScene.End:
				//���� ����.
				break;
			case CurrentScene.Loading:
				//Debug.Log("�ε���");
				loadingSceneUI.SetActive(true);
				break;
			default:
				//Debug.LogError("UIManager / ChangeScene");
				break;
		}
		GameManager.Instance.EnemyListReset();
		AudioManager.Instance.BGMChange(SceneManager.GetActiveScene().buildIndex);
		DataManager.Instance.triggerOn = 0;
		DistanceCulling.Instance.SetPlayerAndTarget();
	}

	public void GameSceneTextUpdate()
	{
		flyText.text = DataManager.Instance.data.money.ToString();
		ammoText.text = DataManager.Instance.data.ammo.ToString();
	}

	public void ChangeInteractionText(string str)
	{
		interactionText.text = str;
	}

	public void OnOffInteractionText(bool OnOff)
	{
		gameBaseText.SetActive(OnOff);
		if (gameBaseText.activeSelf == false)
		{
			interactionText.text = null;
		}
	}

	public void TransitionToLoadScene(string nextSceneName)
	{
		StartCoroutine(Loading(nextSceneName));
	}

	private IEnumerator Loading(string nextSceneName)
	{
		//�ε��� Culling ����
		DistanceCulling.Instance.ResetPlayerAndTarget();

		//SceneManager.LoadScene("LoadingScene");
		//yield return null;
		//Debug.Log("��Ż ���� �� �ε���");
		AsyncOperation ap = SceneManager.LoadSceneAsync("LoadingScene");
		Debug.Log("Loading");
		yield return new WaitUntil(() => ap.isDone);
		//Debug.Log("��Ż ���� �� �ε� ��");
		Debug.Log("Loading End");
		//���������� UI�� �������� �ʴ� ������ �߻��Ͽ� �� �ε尡 ���������� ��ٸ�
		//��ٸ��� �ߴµ� �������� ����
		//yield return StartCoroutine(LoadTest());

		ChangeScene();
		//yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
		loadingController.StartLoadingScene(nextSceneName);

	}

	private IEnumerator LoadTest()
	{
		yield return null;
		SceneManager.LoadScene("LoadingScene");
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
