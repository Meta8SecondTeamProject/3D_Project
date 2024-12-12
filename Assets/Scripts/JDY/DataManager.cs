using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DataManager : SingletonManager<DataManager>
{
	public Data data;
	//������������ ������ �÷��̾��� ��ġ ����.
	public Vector3[] playerStartPos;
	public Vector3 currentStartPos;

	public int jumpCount;
	public int triggerOn;

	public float bombFliesSpeed;
	public float fishSpeed;
	public float birdSpeed;

	//�߰� ���� �̼�
	public float birdBossSpeed;
	public float fishBossSpeed;

	//�ִ� ������ �� �ִ� ���� ��
	public int fliesMaxSpawnCount;
	public int fishMaxSpawnCount;
	public int birdMaxSpawnCount;
	public int birdBlackMaxSpawnCount;

	//��ǥ ų ��
	public int fishKillCount;
	public int birdKillCount;

	public int totalKillCount;

	public Difficulty difficulty;

	public Vector3 StartPosition()
	{
		//NewGamePositionSet();

		int index = SceneManager.GetActiveScene().buildIndex;
		if (playerStartPos.Length >= index - 2)
			currentStartPos = playerStartPos[index - 3];
		return currentStartPos;
	}

	protected override void Awake()
	{
		base.Awake();
		//NewGame();
		//SaveManager.SaveGame(data);

		//�����͸� ó������ �ε��Ͽ��� ���� ������ ���� �� �ְ�
		if (data != null)
		{
			data = SaveManager.LoadGame();
			//Debug.Log(data.isClear);
		}
		else
		{
			//Debug.Log("������ ����!");
		}

		//if (data == null)
		//{
		//    NewGame();

		//}
	}

	private void NewGamePositionSet()
	{

		if (DataManager.Instance.data.isPlaying == false)
		{
			playerStartPos[0] = new Vector3(27, 45, 5);
		}
		else
		{
			playerStartPos[0] = new Vector3(-359.5f, 18f, 365.5f);
		}
	}

	[ContextMenu("Test")]
	private void SaveTest()
	{
		SaveManager.SaveGame(data);
	}

	public void Save()
	{
		data.isPlaying = true;
		data.currentSceneName = SceneManager.GetActiveScene().name;
		data.difficulty = difficulty;
		SaveManager.SaveGame(data);
	}

	public void Load()
	{
		data = SaveManager.LoadGame();
		difficulty = data.difficulty;	
	}

	#region ���̺� �ε� �׽�Ʈ�� 
	private void Update()
	{

		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
		{
			data.ammo = 16;
			data.money++;
			UIManager.Instance.GameSceneTextUpdate();
			Debug.Log("ź��, �ĸ� ������");

		}
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Keypad2))
		{
			Debug.Log("SaveGameȣ��");
			//SaveManager.SaveGame(data);
			Save();
		}
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Keypad3))
		{
			//data = SaveManager.LoadGame();
			Load();
			Debug.Log("LoadGameȣ��");
			UIManager.Instance.TransitionToLoadScene(data.currentSceneName);
		}
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Keypad4))
		{
			Debug.Log("BBH������ �̵�");
			UIManager.Instance.TransitionToLoadScene("BBH_Scene");
		}

		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Keypad5))
		{
			Debug.Log("JDY������ �̵�");
			UIManager.Instance.TransitionToLoadScene("JDY_Scene");
		}
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Keypad6))
		{
			Debug.Log("KCY������ �̵�");
			UIManager.Instance.TransitionToLoadScene("KCY_Scene");
		}
	}
	#endregion

	public void RetryGame()
	{
		data.HP = 2;
		data.ammo = 4;
		Save();
		Load();
		UIManager.Instance.TransitionToLoadScene(data.currentSceneName);
	}

	public void EndGame()
	{
		if (difficulty == Difficulty.HARD)
		{
			data.isHardClear = true;
		}
		data.isPlaying = false;
		Save();
		UIManager.Instance.TransitionToLoadScene("GameEndScene");
	}

	public void EnemyCountSet()
	{
		switch (difficulty)
		{
			case Difficulty.EASY:
				fishKillCount = 16;
				birdKillCount = 16;
				break;
			case Difficulty.NORMAL:
				fishKillCount = 20;
				birdKillCount = 20;
				break;
			case Difficulty.HARD:
				fishKillCount = 25;
				birdKillCount = 25;
				break;
			case Difficulty.EXTREAM:
				fishKillCount = 50;
				birdKillCount = 50;
				break;
			default:
				break;
		}
	}

	public void NewGame()
	{
		data.currentSceneName = "";
		data.currentHP = 2;
		data.currentAmmo = 4;
		data.maxAmmo = 16;
		data.money = 0;
		jumpCount = 1;
		data.isHat = false;
		data.isAmmoBelt = false;
		data.isDoubleJump = false;
		data.isPlaying = false;
		data.isKilledBossBird = false;
		data.isKilledBossFish = false;
	}
}


[Serializable] //�� Ŭ������ JSON���� ��Ȱ�� �� �ֵ��� �����ϴµ� �ʿ���
public class Data
{

	public Difficulty difficulty;
	public string currentSceneName;
	public int HP { get { return currentHP; } set { currentHP = Mathf.Clamp(value, 0, 2); } }
	public int currentHP;
	public int ammo { get { return currentAmmo; } set { currentAmmo = Mathf.Clamp(value, 0, maxAmmo); } }
	public int currentAmmo;
	public int maxAmmo;

	public int money;

	public bool isHat;
	public bool isAmmoBelt;
	public bool isDoubleJump;

	public bool isPlaying;
	public bool isHardClear;

	public bool isKilledBossBird;
	public bool isKilledBossFish;

}

public enum Difficulty
{
	EASY,
	NORMAL,
	HARD,
	EXTREAM
}


