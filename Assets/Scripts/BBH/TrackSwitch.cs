using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class TrackSwitch : MonoBehaviour
{
    public CinemachineDollyCart cart;
    public CinemachineSmoothPath[] tracks;
    public GameObject lookAtTarget;
    public Transform[] lookAtPos;
    public FadeController fadeController;
    public Image[] creditsImages;

    public float[] cartSpeed; 
    public float waitTime = 2f;

    private bool isSwitching = false;
    private int currentTrackIndex = 0;

    private void Start()
    {
        StartCoroutine(fadeController.FadeOut());
        cart.m_Speed = cartSpeed[0];

        //�̻��� �Ǽ� ���������� �̹��� ���İ� �����ؼ� ���� �����ϰ� �ٲ���
        for (int i = 0; i < creditsImages.Length; i++)
        {
            creditsImages[i].color = new Color(creditsImages[i].color.r, creditsImages[i].color.g, creditsImages[i].color.b, 0);
        }
    }

    private void Update()
    {
        //īƮ�� Ʈ���� 80% �̻� �����ϸ� �ڷ�ƾ�� ������
        if (cart.m_Position+(cart.m_Path.PathLength * 0.25f) >= cart.m_Path.PathLength && isSwitching == false)
        {
            StartCoroutine(SwitchToNextTrack());
        }
        //Debug.Log($"currentTrackIndex : {currentTrackIndex} , {creditsImages.Length - 2}");
    }

    //0. īƮ�� Ư�� ������ �����ϸ�
    //1. ĵ���� ���̵� ���ؼ� ��Ӱ� �ϱ�, ó������ �̹��� ���� �� �˾����� �ڷ�ƾ�� ���� �ȸ���°ǵ�
    //2. ��Ӱ� �� ���� �̹��� ����ϱ�
    //3. �̹����� ���̵� �ƿ����� n�ʵ��� ����ϰ� ���̵� ������ �ٽ� ����
    //4. �̹����� ���̵� ���� ������ Ʈ�� �ű�� ĵ������ ���̵� �ƿ�
    //5. ���� ������ �̹����� ��� ���̵�ƿ� ���� StartScene���� �̵� 

    IEnumerator SwitchToNextTrack()
    {
        //�ڷ�ƾ �ߺ� ���� ������ bool���� 
        isSwitching = true;
        
        //ȭ���� ��Ӱ� ��, �̷��� �����ϸ� �ڷ�ƾ ���������� �Ʒ� if�� �������� ����
        yield return StartCoroutine(fadeController.FadeIn());

        //���� Ʈ�� �ε������� �̹��� �迭�� ũ�� - 1���� ������,
        //�̹��� �迭�� ũ�Ⱑ 5�ϱ�, Index < (5-1)
        //0,1,2,3 �� 4���� �̹����� ������
        //if (currentTrackIndex < creditsImages.Length - 1)
        //{
        //    //ShowImage���� �̹��� �����ְ�, waitTime��ŭ ��ٸ�
        //    yield return StartCoroutine(fadeController.ShowImage(creditsImages[currentTrackIndex], waitTime));
        //    //ShowImage������ HideImage �����ؼ� �̹��� ������
        //    yield return StartCoroutine(fadeController.HideImage(creditsImages[currentTrackIndex]));
        //}
        
        //���� ���� �� �ε����� �̹��� �迭�� ũ�� - 2��,
        //Ʈ���� �ִ� �ε����� 3, �̹��� �迭�� ũ��� 5�ϱ� 3 == (5 - 2) ��, ������ Ʈ���̸�
        if (currentTrackIndex == creditsImages.Length - 2)
        {
            //Debug.Log("������ �̹��� ���");
            //������ �̹��� ��� �� ����ó��
            yield return StartCoroutine(fadeController.ShowImage(creditsImages[creditsImages.Length - 1], waitTime));
            yield return StartCoroutine(fadeController.HideImage(creditsImages[creditsImages.Length - 1]));

            //������ ���̴ϱ� �̹����� �����ְ� �ٽ� StartScene���� �Ѿ
            //Ȥ���� StartScene���� �Ѿ������ �ؾ��� �۾� ������ ���⼭ �����ϸ� ��
            UIManager.Instance.TransitionToLoadScene("GameStartScene");
            yield break;
        }

        //(n + 1) < 4, �� 3�� TrackIndex++ �� Ʈ�� ��ȯ 
        if (currentTrackIndex + 1 < tracks.Length)
        {
            //HideImage���� ������ Ʈ�� �ε��� ���� �� ���� ���� �ʱ�ȭ��
            currentTrackIndex++;
            //Debug.Log(currentTrackIndex);

            //switch (currentTrackIndex)
            //{
            //    //case 0 �� Start�� ����
            //    case 1:
            //        cart.m_Speed = cartSpeed[currentTrackIndex];
            //        break;
            //        case 2:
            //        cart.m_Speed = cartSpeed[currentTrackIndex];
            //        break;
            //        case 3:
            //            cart.m_Speed = cartSpeed[]
            //    default:
            //        cart.m_Speed = 20f;
            //        break;
            //}

            cart.m_Speed = cartSpeed[currentTrackIndex];
            cart.m_Path = tracks[currentTrackIndex];

            lookAtTarget.transform.position = lookAtPos[currentTrackIndex].position;
            cart.m_Position = 0f;
        }
        //���� ������ ���̵� �ƿ� �����ؼ� �ٽ� ȭ�� ��� �ٲ�
        yield return StartCoroutine(fadeController.FadeOut());

        isSwitching = false;
    }
}
