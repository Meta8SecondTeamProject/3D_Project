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

    [SerializeField]
    private GameObject spawner;

    private void Start()
    {
        spawner.SetActive(false);
        arrivePos = transform.position + (Vector3.up * movePos);
        DataSetting();
        TextSetting(true);
    }

    private void DataSetting()
    {
        switch (enemyType)
        {
            case EnemyType.Bird:
                data = DataManager.Instance.birdKillCount;
                break;
            case EnemyType.Fish:
                data = DataManager.Instance.fishKillCount;
                break;
            default:
                Debug.LogError("Prop_On / DataSetting / enemyType missing");
                break;
        }
    }

    private void TextSetting(bool booool)
    {
        if (booool)
        {
            foreach (var text in texts)
            {
                text.text = $"Eliminate {data}\n enemies";
            }
        }
        else
        {
            foreach (var text in texts)
            {
                text.text = null;
            }
        }
    }

    //임시
    //TODO : DataManager에서 변수를 추가하여 함수를 호출하는 방식이 좋아보임
    private void Update()
    {
        DataSetting();
        TextSetting(true);

        if (data <= 0)
        {
            TextSetting(false);
            Instantiate(prefab).transform.position = spawnPos.position;
            spawner.SetActive(false);
            EnemysToPool();
            Destroy(this);
        }
    }

    public IEnumerator PropOn()
    {
        spawner.SetActive(true);
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

    private void EnemysToPool()
    {
        switch (enemyType)
        {
            case EnemyType.Bird:
                GameManager.Instance.EnemyToPool(2);
                break;
            case EnemyType.Fish:
                GameManager.Instance.EnemyToPool(1);
                break;
            default:
                Debug.LogError("Prop_On / EnemysToPool / enemyType Error");
                break;
        }
    }
}