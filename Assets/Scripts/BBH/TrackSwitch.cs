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

        //이상한 실수 방지용으로 이미지 알파값 조절해서 전부 투명하게 바꿔줌
        for (int i = 0; i < creditsImages.Length; i++)
        {
            creditsImages[i].color = new Color(creditsImages[i].color.r, creditsImages[i].color.g, creditsImages[i].color.b, 0);
        }
    }

    private void Update()
    {
        //카트가 트랙의 80% 이상 도달하면 코루틴을 시작함
        if (cart.m_Position+(cart.m_Path.PathLength * 0.25f) >= cart.m_Path.PathLength && isSwitching == false)
        {
            StartCoroutine(SwitchToNextTrack());
        }
        //Debug.Log($"currentTrackIndex : {currentTrackIndex} , {creditsImages.Length - 2}");
    }

    //0. 카트가 특정 지점에 도달하면
    //1. 캔버스 페이드 인해서 어둡게 하기, 처음부터 이미지 넣을 줄 알았으면 코루틴도 따로 안만드는건데
    //2. 어둡게 한 동안 이미지 출력하기
    //3. 이미지를 페이드 아웃으로 n초동안 출력하고 페이드 인으로 다시 숨김
    //4. 이미지의 페이드 인이 끝나면 트랙 옮기고 캔버스도 페이드 아웃
    //5. 만약 마지막 이미지일 경우 페이드아웃 없이 StartScene으로 이동 

    IEnumerator SwitchToNextTrack()
    {
        //코루틴 중복 실행 방지용 bool변수 
        isSwitching = true;
        
        //화면을 어둡게 함, 이렇게 실행하면 코루틴 끝날때까지 아래 if문 실행하지 않음
        yield return StartCoroutine(fadeController.FadeIn());

        //현재 트랙 인덱스값이 이미지 배열의 크기 - 1보다 작으면,
        //이미지 배열의 크기가 5니까, Index < (5-1)
        //0,1,2,3 총 4번의 이미지를 보여줌
        //if (currentTrackIndex < creditsImages.Length - 1)
        //{
        //    //ShowImage에서 이미지 보여주고, waitTime만큼 기다림
        //    yield return StartCoroutine(fadeController.ShowImage(creditsImages[currentTrackIndex], waitTime));
        //    //ShowImage끝나면 HideImage 실행해서 이미지 숨겨줌
        //    yield return StartCoroutine(fadeController.HideImage(creditsImages[currentTrackIndex]));
        //}
        
        //만약 현재 씬 인덱스가 이미지 배열의 크기 - 2면,
        //트랙의 최대 인덱스는 3, 이미지 배열의 크기는 5니까 3 == (5 - 2) 즉, 마지막 트랙이면
        if (currentTrackIndex == creditsImages.Length - 2)
        {
            //Debug.Log("마지막 이미지 출력");
            //마지막 이미지 출력 및 숨김처리
            yield return StartCoroutine(fadeController.ShowImage(creditsImages[creditsImages.Length - 1], waitTime));
            yield return StartCoroutine(fadeController.HideImage(creditsImages[creditsImages.Length - 1]));

            //마지막 씬이니까 이미지만 보여주고 다시 StartScene으로 넘어감
            //혹여나 StartScene으로 넘어가기전에 해야할 작업 있으면 여기서 실행하면 됨
            UIManager.Instance.TransitionToLoadScene("GameStartScene");
            yield break;
        }

        //(n + 1) < 4, 총 3번 TrackIndex++ 및 트랙 전환 
        if (currentTrackIndex + 1 < tracks.Length)
        {
            //HideImage까지 끝나면 트랙 인덱스 증가 및 각종 설정 초기화함
            currentTrackIndex++;
            //Debug.Log(currentTrackIndex);

            //switch (currentTrackIndex)
            //{
            //    //case 0 은 Start때 설정
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
        //설정 끝내고 페이드 아웃 실행해서 다시 화면 밝게 바꿈
        yield return StartCoroutine(fadeController.FadeOut());

        isSwitching = false;
    }
}
