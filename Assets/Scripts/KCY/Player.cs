using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

	public Frog_Move frogMove;
	public Frog_Look frogLook;
	public Frog_Action frogAction;
	private Rigidbody rb;
	private bool damageAble;
	public GameObject bloodExplosion;
	public Frog_Body_Change bodyChange;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		frogLook = GetComponent<Frog_Look>();
		frogMove = GetComponent<Frog_Move>();
		frogAction = GetComponent<Frog_Action>();
		bodyChange = GetComponentInChildren<Frog_Body_Change>();
	}

	private IEnumerator Start()
	{
		while (true)
		{
			damageAble = true;
			yield return new WaitWhile(() => damageAble);
			yield return new WaitForSeconds(3);
			bloodExplosion.SetActive(false);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Enemy"))
		{
			if (Knockback(collision, out Rigidbody otherRb))
			{
				otherRb.AddForce(Vector3.back * 20f, ForceMode.Impulse);

			}
			if (damageAble)
			{
				Debug.Log("¾Æ¾ß");
				TakeDamage();
			}
		}
	}

	public void TakeDamage()
	{
		damageAble = false;
		DataManager.Instance.data.HP--;
		bloodExplosion.SetActive(true);
		rb.AddForce(Vector3.up * 250, ForceMode.Impulse);
		bodyChange.BodyChange();
		if (DataManager.Instance.data.HP <= 0)
			OffAction();
	}

	private bool Knockback(Collision collision, out Rigidbody otherRb)
	{
		otherRb = collision.collider.GetComponentInParent<Rigidbody>();
		return true;
	}

	private void OffAction()
	{
		frogMove.enabled = false;
		frogLook.enabled = false;
		frogAction.enabled = false;
	}
}
