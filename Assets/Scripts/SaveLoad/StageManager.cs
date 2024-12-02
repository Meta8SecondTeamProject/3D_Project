using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//매 씬마다 시작 위치를 지정해주는 친구
//데이터매니저가 게임매니저한테 현재씬을 같이 보내고
//시작위치를 게임매니저가 생성하게끔.
//public class StageManager : MonoBehaviour
//{
//    public Vector3 spawnPos; //플레이어 초기 위치 설정

//    private void Start()
//    {

//        //저장된 위치가 있는지 확인
//        GameSaveData saveData = SaveManager.LoadGame();

//        //세이브 데이터가 존재하고, 세이브 데이터의 씬 이름과 현재 씬 이름이 일치한다면
//        if (saveData != null && saveData.currentSceneName == SceneManager.GetActiveScene().name)
//        {
//            //세이브 데이터의 저장된 포지션에 플레이어를 생성함
//            PlayerManager.Instance.CreatePlayer(saveData.playerPos);
//            Debug.Log($"세이브 데이터에서 불러온 위치로 이동시킴 : {saveData.playerPos}");
//        }
//        else
//        {
//            //그거 아니면 미리 정해둔 위치에 생성
//            PlayerManager.Instance.CreatePlayer(spawnPos);
//            Debug.Log($"기본 위치로 이동시킴 : {spawnPos}");
//        }
//    }
//}

