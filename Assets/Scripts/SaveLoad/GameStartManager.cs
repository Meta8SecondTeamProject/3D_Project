using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//StartScene에서 new게임을 했을시
//불러올 값들을 지정.
//데이터 매니저가 가지고 있고
//public class GameStartManager : MonoBehaviour
//{
//    public void NewGame()
//    {
//        ResetData(); //초기 데이터를 저장
//        //ContinueGame(); //초기 데이터를 불러오기

//        GameSaveData loadedData = SaveManager.LoadGame();

//        if (loadedData != null)
//        {
//            PlayerManager.Instance.LoadPlayerState(loadedData); //상태 복원
//            LoadingSceneController.LoadScene(loadedData.currentSceneName); //로딩 씬을 통해 씬 전환
//        }
//    }

//    public void ContinueGame()
//    {
//        //JSON파일 읽기
//        GameSaveData loadedData = SaveManager.LoadGame();

//        if (loadedData != null)
//        {
//            //읽어들인 파일을 기반으로 플레이어 복원 및 씬 이동
//            PlayerManager.Instance.LoadPlayerState(loadedData);
//            //SceneManager.LoadScene(loadedData.currentSceneName);

//            LoadingSceneController.LoadScene(loadedData.currentSceneName);
//            //PlayerManager.Instance.TransitionToScene(loadedData.currentSceneName);
//        }
//        else
//        {
//            Debug.Log("404 NOT FOUND, NO SAVED DATA");
//        }
//    }

//    private void ResetData()
//    {
//        GameSaveData resetData = new GameSaveData //생성자 호출, 중괄호 잘 봐야됨
//        {
//            playerPos = new Vector3(0, 70, 0), //초기 위치
//            health = 100, //초기 체력
//            ammo = 50,    //초기 탄약
//            currentSceneName = "BBH_Scene" //초기 씬 
//        };
//        SaveManager.SaveGame(resetData); //초기화 한 값을 JSON으로 변환 및 저장

//        Debug.Log("세이브 데이터 초기화됨");
//    }
//}
