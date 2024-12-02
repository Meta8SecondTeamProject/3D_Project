using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //파일 입출력에 사용


//Json을 통해 데이터들을 세이브/로드하는 친구
//데이터매니저가 가지고 있지 않고 따로 돌게...
public class SaveManager : MonoBehaviour
{
    //저장 경로
    //정확히는 C:\Users\사용자\AppData\LocalLow\DefaultCompany\3D_Project에 savefile.json으로 생성됨
    private static string SavePath => Application.persistentDataPath + "/savefile.json";
    //SaveManager의 인스턴스를 매번 생성하는것 보다 static으로 선언하고 사용하는게 더 적합함

    //private static string SavePath { get { return Application.persistentDataPath + "/savefile.json"; } }
    //람다식으로 사용했지만 이렇게 써도 똑같음
    //반환 값만 정의하는 간단한 로직에서는 =>가 더 직관적이고 가독성이 좋다고 함

    //데이터를 저장하는 메서드
    public static void SaveGame(Data data)
    {
        string json = JsonUtility.ToJson(data, true); //GameSaveData 객체를 JSON 문자열로 변환
        //두 번째 인자로 오는 bool값은 들여쓰기 여부를 나타냄, true로 하면 사람이 보기 좋은 형태로 변환되지만 그만큼 용량이 좀 늘어남
        //false로 하면 들여쓰기 없이 압축된 형태의 JSON 문자열로 변환하니까 파일크기가 작고 저장 공간을 절약할 수 있음
        File.WriteAllText(SavePath, json); //변환된 JSON을 파일로 저장
        Debug.Log("GameSaved :" + SavePath);
    }

    //데이터를 불러오는 메서드
    public static Data LoadGame()
    {
        if (File.Exists(SavePath)) //파일이 존재하는지 확인
        {
            string json = File.ReadAllText(SavePath); //파일에서 JSON읽기
            Debug.Log("Loaded JSON: " + json);
            Data data = JsonUtility.FromJson<Data>(json); //JSON문자열을 GameSaveData객체로 변환
            //Debug.Log("Game Loaded" + SavePath);
            return data;
        }
        else
        {
            #region 없으면 새로 만들어서 사용하는것도 가능하다는 예제
            //예제였는데 실제로 쓰는 로직이 되버렸어요
            Debug.Log("파일이 없으므로 새로 생성합니다.");
            string defaultJson = "{\r\n    \"currentSceneName\": \"BBH_Scene\",\r\n    \"HP\": 2,\r\n    \"currentHP\": 2,\r\n    \"ammo\": 4,\r\n    \"currentAmmo\": 4,\r\n    \"maxAmmo\": 16,\r\n    \"money\": 0,\r\n    \"isHat\": false,\r\n    \"isAmmoBelt\": false,\r\n    \"isDoubleJump\": false,\r\n    \"isClear\": false,\r\n    \"isHardClear\": false\r\n}";
            File.WriteAllText(SavePath, defaultJson);
            Data data = JsonUtility.FromJson<Data>(defaultJson);
            return data;
            #endregion
            //Debug.Log("404 NOT FOUND");
            //return null; //파일이 없으면 null 반환

        }
    }
}
