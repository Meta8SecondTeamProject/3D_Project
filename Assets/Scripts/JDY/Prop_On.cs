using System.Collections;
using TMPro;
using UnityEngine;


public enum EnemyType { Bird, Fish }

public class Prop_On : MonoBehaviour
{
    private const float movePos = 33f;
    private Vector3 arrivePos;
    [SerializeField]
    private TextMeshPro[] texts;
    private int data;

    [SerializeField]
    private EnemyType enemyType;
    public GameObject prefab;
    public Transform spawnPos;

    private void Start()
    {
        arrivePos = transform.position + (Vector3.up * movePos);
        data = DataManager.Instance.birdKillCount;
        foreach (var text in texts)
        {
            text.text = $"죽이다 {data}\n 새끼";
        }
    }

    private void DataSetting()
    {

    }

    //임시
    //DataManager에서 값이 변경 될 때마다 함수를 호출하는 방식이 좋아 보이는데...
    private void Update()
    {
        if (DataManager.Instance.birdKillCount != data)
        {
            data = DataManager.Instance.birdKillCount;
            foreach (var text in texts)
            {
                text.text = $"죽이다 {data}\n 새끼";
                //text.text = $"killd {data}\n birds/dolphins";
            }
        }

        if (data <= 0)
        {
            foreach (var text in texts)
            {
                text.text = null;
            }

            Instantiate(prefab).transform.position = spawnPos.position;
            Destroy(this);
        }
    }

    public IEnumerator PropOn()
    {
        while (Vector3.Distance(transform.position, arrivePos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, arrivePos, 0.5f * Time.unscaledDeltaTime);
            yield return null;
        }
        transform.position = arrivePos;
    }

    //작동하는지 확인용 추후 삭제 예정
    [ContextMenu("콘덴서 만들어요")]
    public void Test()
    {
        while (Vector3.Distance(transform.position, arrivePos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, arrivePos, 0.5f * Time.unscaledDeltaTime);
        }
        transform.position = arrivePos;
    }
}
