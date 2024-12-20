using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistanceCulling : SingletonManager<DistanceCulling>
{
	[SerializeField, Header("컬링할 거리")]
	private float cullingDis = 400f;

	private Transform player;
	//컬링할 오브젝트를 리스트로 관리, 태그로는 못함
	[TextArea(1, 2)]
	public string explanation = "컬링할 오브젝트 태그를 Culling으로 바꿔주세요.\n눈에 띄지않는 작은 오브젝트가 적합합니다.";
	public List<GameObject> cullingObj = new List<GameObject>();

	[ContextMenu("Set")]
	public void SetPlayerAndTarget()
	{
		//플레이어를 매번 생성하는 로직이므로 이렇게 초기화안하면 null뜰거같음
		//아마? 뜰?거임
		GameObject playerObj = GameObject.FindWithTag("Player");

		//TODO : (확인부탁드립니다.)조건 추가해서 플레이어가 생성되지 않는 씬은 플레이어를 찾아오지 않도록 수정
		//플레이어를 못찾아오면 오브젝트를 찾아오도록 수정
		if (playerObj != null)
		{
			player = playerObj.transform;
		}
		else if (SceneManager.GetActiveScene().name != "GameStartScene"
			&& SceneManager.GetActiveScene().name != "LoadingScene"
			&& SceneManager.GetActiveScene().name != "GameEndingScene" && playerObj == null)
		{
			playerObj = FindAnyObjectByType<Player>().gameObject;
			Debug.LogWarning("플레이어를 찾을수 없음 (Distance Culling)");
		}
		//씬이 로드되면 GameObject를 모두 찾아옴
		GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>();

		//그 중에서 Culling태그가 붙은 오브젝트만 리스트에 넣음
		foreach (GameObject obj in objects)
		{
			if (obj.CompareTag("Culling"))
			{
				cullingObj.Add(obj);
			}
		}
		StartCoroutine(DistanceCullingCoroutine());
	}

	[ContextMenu("Reset")]
	public void ResetPlayerAndTarget()
	{
		player = null;
		cullingObj.Clear();
	}

	//플레이어와 Culling 태그가 붙은 오브젝트를 찾아와서 플레이어와의 거리에 따라 비활성화 처리함
	public IEnumerator DistanceCullingCoroutine()
	{
		while (player != null)
		{
			yield return null;

			//null 체크
			//if (player == null)
			//    return;

			//태그로 찾는건 좋으나, 매 프레임마다 Find를 호출하는건 성능에 좋지도 않고,
			//Find는 비활성화된 오브젝트를 찾지 못하므로 다시 활성화되게 하려면 다른 로직이 필요함
			foreach (GameObject obj in cullingObj)//GameObject.FindGameObjectsWithTag("Culling"))
			{
				float distance = Vector3.Distance(player.transform.position, obj.transform.position);

				//Debug.Log($"거리 : {distance}");
				//Debug.Log($"플레이어 이름 : {player.name}");
				//Debug.Log($"오브젝트 이름 : {obj.name}");

				obj.SetActive(distance <= cullingDis);
			}
		}
	}
}