using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//����
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
//        //���� �񵿱�� �ҷ��� �� �ε��� ������ �ڵ����� �ҷ��� ������ �̵��Ұ����� �����ϴ� �ɼ�
//        op.allowSceneActivation = false; //false�� �����ϸ� 90%������ �ε��ϰ�, true�� ������ ���� 10%�� �ε���

//        float timer = 0f;
//        while (op.isDone == false) //���� �ε��� ������ ���� ����
//        {
//            yield return null;

//            if (op.progress < 0.9f) //���� �ε� ���൵�� 90% �̸��̸�
//            {
//                progressBar.fillAmount = op.progress; //�ε��ٸ� ���൵��ŭ ä��
//            }
//            else //90% ���� ä��� ����
//            {
//                timer += Time.unscaledDeltaTime;
//                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer); //����ũ �ε��� ������

//                if (progressBar.fillAmount >= 1) //�̹����� �� ä��� ����
//                {
//                    op.allowSceneActivation = true; //���� ������ �Ѿ
//                }
//            }
//        }
//    }
//}
