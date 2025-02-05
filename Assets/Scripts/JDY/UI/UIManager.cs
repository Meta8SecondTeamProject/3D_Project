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
    [HideInInspector] public CurrentScene currentScene;

    private void Start()
    {
        ChangeScene();
        OnOffInteractionText(false);
        GameSceneTextUpdate();
    }

    private void CurrentSceneUI()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        switch (index)
        {
            case 0:
                currentScene = CurrentScene.Start;
                Cursor.visible = true;
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

    //�� ���� ���缭 UI�� Ȱ��ȭ
    public void ChangeScene()
    {
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
                loadingSceneUI.SetActive(true);
                break;
            default:
                break;
        }

        GameManager.Instance.EnemyListReset();
        AudioManager.Instance.BGMChange(SceneManager.GetActiveScene().buildIndex);
        DataManager.Instance.triggerOn = 0;
        DistanceCulling.Instance.SetPlayerAndTarget();
    }

    /// <summary>
    /// �Ѿ�, �Ӵ� �ؽ�Ʈ ������Ʈ
    /// </summary>
    public void GameSceneTextUpdate()
    {
        if (DataManager.Instance?.data == null)
        {
            Debug.LogError("UIManager / GameSceneTextUpdate / DataManager.Instance.data is NULL Error");
            return;
        }
        flyText.text = DataManager.Instance.data.money.ToString();
        ammoText.text = DataManager.Instance.data.ammo.ToString();
    }

    /// <summary>
    /// NPC�� Player�� ��ȣ�ۿ� �� ��� �ؽ�Ʈ ������Ʈ
    /// </summary>
    /// <param name="str"></param>
    public void ChangeInteractionText(string str)
    {
        interactionText.text = str;
    }

    public void OnOffInteractionText(bool isOnOff)
    {
        gameBaseText.SetActive(isOnOff);
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

        AsyncOperation ap = SceneManager.LoadSceneAsync("LoadingScene");
        yield return new WaitUntil(() => ap.isDone);
        ChangeScene();
        loadingController.StartLoadingScene(nextSceneName);
    }
}

public enum CurrentScene
{
    Start,
    Game,
    End,
    Loading
}
