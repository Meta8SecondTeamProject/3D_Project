using UnityEngine;


public class Boss_Spawn : Interaction
{
    private bool isBoos;
    [SerializeField] private GameObject spawnPos;
    [SerializeField] private GameObject birdSpawner;
    [SerializeField] private GameObject fishSpawner;

    private void Awake()
    {
        birdSpawner.SetActive(false);
        fishSpawner.SetActive(false);
    }

    protected override void Start()
    {
        //상속받은 Start 아무동작안하도록 초기화.
        isNonInteractive = false;
    }

    private void OnEnable()
    {
        isBoos = true;
    }

    public override void InteractionEvent()
    {
        if (isBoos)
        {
            isBoos = false;
            birdSpawner.SetActive(true);
            fishSpawner.SetActive(true);
            Instantiate(interactionEffectObject).transform.position = spawnPos.transform.position;
            UIManager.Instance.ChangeInteractionText(str = "The King Is Comming...");
        }
    }

    private void OnDisable()
    {
        isBoos = false;
    }
}
