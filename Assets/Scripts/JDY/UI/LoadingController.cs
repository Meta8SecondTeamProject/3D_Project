using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    public Image progressBar;
    [HideInInspector]
    public string nextSceneName;

    public void StartLoadingScene(string nextScene)
    {
        nextSceneName = nextScene;
        StartCoroutine(LoadSceneProcess());
    }

    //비동기로 씬을 전환
    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneName);

        operation.allowSceneActivation = false;
        float timer = 0f;
        while (operation.isDone == false)
        {
            yield return null;

            if (operation.progress < 0.9f)
            {
                progressBar.fillAmount = operation.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                if (progressBar.fillAmount >= 1f)
                {
                    DataManager.Instance.EnemyCountSet();
                    operation.allowSceneActivation = true;
                    //씬 전환시 랜덤 값으로 잠시 대기
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 2f));
                    yield return new WaitUntil(() => operation.isDone);
                    yield return null;
                    UIManager.Instance.ChangeScene();
                }

            }
        }

    }
}
