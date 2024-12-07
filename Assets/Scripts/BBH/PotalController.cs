using UnityEngine;

public class PotalController : MonoBehaviour
{
    [SerializeField]
    private GameObject potal;

    public BossKilled boss;
    private bool cheak;

    private void Start()
    {
        BossClearCheak();
        potal.SetActive(cheak);
        PotalControllerDestroy();
    }

    private void Update()
    {
        BossClearCheak();
        if (cheak)
        {
            potal.SetActive(cheak);
            PotalControllerDestroy();
        }
    }

    private void BossClearCheak()
    {
        switch (boss)
        {
            case BossKilled.killedBossBird:
                cheak = DataManager.Instance.data.isKilledBossBird;
                break;
            case BossKilled.killedBossFish:
                cheak = DataManager.Instance.data.isKilledBossFish;
                break;
            case BossKilled.None:
                cheak = true;
                break;
            default:
                Debug.LogError("PotalController / BossClearCheak / default error");
                break;
        }
    }

    private void PotalControllerDestroy()
    {
        if (potal.activeSelf)
        {
            Destroy(this);
        }
    }
}
