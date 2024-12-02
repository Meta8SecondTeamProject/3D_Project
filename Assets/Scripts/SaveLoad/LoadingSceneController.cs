using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//보류
//public class LoadingSceneController : MonoBehaviour
//{
//    [SerializeField] private Image progressBar;

//    static string nextScene;

//    private void Start()
//    {
//        StartCoroutine(LoadSceneProcess());
//    }

//    public static void LoadScene(string sceneName)
//    {
//        nextScene = sceneName;
//        SceneManager.LoadScene("LoadingScene");
//    }

//    private IEnumerator LoadSceneProcess()
//    {

//        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
//        //씬을 비동기로 불러올 때 로딩이 끝나면 자동으로 불러온 씬으로 이동할것인지 설정하는 옵션
//        op.allowSceneActivation = false; //false로 설정하면 90%까지만 로딩하고, true가 됐을때 남은 10%를 로딩함

//        float timer = 0f;
//        while (op.isDone == false) //씬의 로딩이 끝나지 않은 동안
//        {
//            yield return null;

//            if (op.progress < 0.9f) //씬의 로딩 진행도가 90% 미만이면
//            {
//                progressBar.fillAmount = op.progress; //로딩바를 진행도만큼 채움
//            }
//            else //90% 까지 채우고 나면
//            {
//                timer += Time.unscaledDeltaTime;
//                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer); //페이크 로딩을 진행함

//                if (progressBar.fillAmount >= 1) //이미지를 다 채우고 나면
//                {
//                    op.allowSceneActivation = true; //다음 씬으로 넘어감
//                }
//            }
//        }
//    }
//}
