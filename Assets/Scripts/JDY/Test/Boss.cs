using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent, RequireComponent(typeof(Rigidbody))]
public class Boss : MonoBehaviour
{
    public float bossHp;
    public float bossMaxHp;
    public float hpAmount { get { return bossHp / bossMaxHp; } }
    public Image hpBar;

    protected Rigidbody rb;
    public Player target;
    public float speed;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    protected virtual void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.player != null)
            target = GameManager.Instance.player;
        StartCoroutine(BossPattern());
    }

    private void OnEnable()
    {

        if (GameManager.Instance.player != null)
        {
            target = GameManager.Instance.player;
        }
    }

    public virtual void Hit()
    {
        bossHp--;
        hpBar.fillAmount = hpAmount;
        if (bossHp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
    }

    protected virtual IEnumerator BossPattern()
    {
        yield return null;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Hit();
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            rb.useGravity = false;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.useGravity = true;
        }
    }
}
