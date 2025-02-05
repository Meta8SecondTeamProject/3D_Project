using System.Collections;
using UnityEngine;


public class Toad_Boss : Boss
{
    #region reference variable
    //hp에 따라 적용할 몸체
    [SerializeField, Tooltip("바디들 0번 : 기본, 1번 : 사망")]
    private GameObject[] bodys;
    //점프할 높이
    [SerializeField, Tooltip("점프할 높이")]
    private float jumpHeight = 30f;
    //바라볼 시간
    [SerializeField, Tooltip("플레이어를 바라볼 시간")]
    private float lookTime = 5f;
    //점프할 시간
    [SerializeField, Tooltip("공중에 머무를 시간")]
    private float jumpDuration = 1f;

    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip toadDeathClip;
    [SerializeField] private AudioClip bossBGM;
    #endregion

    #region local variable
    //배지어 곡선을 이용할 포인터들
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 controlPoint;

    private bool isJumping;
    private Collider coll;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        coll = GetComponent<Collider>();
    }

    protected override void Start()
    {
        AudioManager.Instance.PlayBGM(bossBGM, 0.5f);
        BodyChange();
        isJumping = false;
        base.Start();
    }

    private void BodyChange()
    {
        if (bodys == null) return;

        bodys[0].SetActive(bossHp > 0);
        bodys[1].SetActive(bossHp <= 0);
    }

    protected override IEnumerator BossPattern()
    {
        while (bossHp > 0)
        {
            StartCoroutine(LookAtPlayer());
            yield return new WaitUntil(() => isJumping);
            StartCoroutine(JumpToPlayer());
            yield return new WaitWhile(() => isJumping);
        }
    }

    private IEnumerator JumpToPlayer()
    {
        float jumpProgress = 0f;
        PointInitialization();
        rb.velocity = Vector3.zero;
        float jumpTime = Time.time + jumpDuration;

        while (jumpTime >= Time.time)
        {
            jumpProgress += Time.deltaTime / jumpDuration;
            //배지어 곡선
            Vector3 position = Mathf.Pow(1 - jumpProgress, 2) * startPoint + 2 * (1 - jumpProgress) * jumpProgress * controlPoint + Mathf.Pow(jumpProgress, 2) * endPoint;

            Vector3 velocity = (position - rb.position) / Time.fixedDeltaTime;

            rb.velocity = velocity;

            Vector3 distance = (position - transform.position).normalized;
            if (distance.sqrMagnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(distance);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            }
            yield return new WaitForFixedUpdate();
        }

        isJumping = false;
    }

    //포인터 초기화
    private void PointInitialization()
    {
        startPoint = transform.position;

        if (target == null)
        {
            target = GameManager.Instance?.player;
        }

        endPoint = target.transform.position;

        controlPoint = (startPoint + endPoint) / 2 + (Vector3.up * jumpHeight);
    }

    private IEnumerator LookAtPlayer()
    {
        if (target == null)
        {
            Debug.LogError("Toad_Test / LookAtPlayer / target == null");
            target = GameManager.Instance?.player;
        }

        float delay = Time.time + lookTime;
        while (delay >= Time.time)
        {
            Vector3 distance = (target.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(distance.x, 0f, distance.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            yield return null;
        }
        isJumping = true;
    }

    public override void Die()
    {

        coll.isTrigger = true;
        AudioManager.Instance.BGM.Stop();
        AudioManager.Instance.PlaySFX(toadDeathClip, 0.75f);
        BodyChange();
    }
}
