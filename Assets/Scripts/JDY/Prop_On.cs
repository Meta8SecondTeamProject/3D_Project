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
                text.text = $"죽이다 {data}\n 새끼";
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
