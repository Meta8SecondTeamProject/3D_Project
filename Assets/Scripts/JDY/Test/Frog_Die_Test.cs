using System.Collections;
using UnityEngine;

public class Frog_Die_Test : MonoBehaviour
{
	private Vector3[] pos;
	public Rigidbody[] rigidbodys;
	public float explosionForce = 5000f;
	public float explosionRadius = 5f;
	public Vector3 explosionOffset = Vector3.up;

	public float delay = 5f;

	private int remainingObjects;

	public AudioClip frogDeathClip;

	private float tempTime;

	private void OnEnable()
	{
		tempTime = 0;
		if (rigidbodys == null || rigidbodys.Length == 0)
		{
			Debug.LogError("Frog_Die_Test / OnEnable: No rigidbodies assigned.");
			return;
		}
		pos = new Vector3[rigidbodys.Length];
		if (rigidbodys.Length != pos.Length)
		{
			Debug.LogError("Frog_Die_Test / OnEnable");
			return;
		}
		for (int i = 0; i < rigidbodys.Length; i++)
		{
			pos[i] = rigidbodys[i].position;
		}
		TriggerDeathEffect();
		AudioManager.Instance.PlaySFX(frogDeathClip, transform.position);
	}

	private void OnDisable()
	{
		if (rigidbodys.Length != pos.Length)
		{
			Debug.LogError("Frog_Die_Test / OnDisable: Rigidbodies and positions mismatch.");
		}
		for (int i = 0; i < rigidbodys.Length; i++)
		{
			rigidbodys[i].position = pos[i];

		}
	}


	[ContextMenu("Test/사지분해")]
	public void TriggerDeathEffect()
	{
		if (rigidbodys == null || rigidbodys.Length == 0)
		{
			Debug.LogError("Frog_Die_Test / TriggerDeathEffect: No rigidbodies assigned.");
			return;
		}

		remainingObjects = rigidbodys.Length;

		Vector3 explosionCenter = transform.position + explosionOffset;

		foreach (Rigidbody rigidbody in rigidbodys)
		{
			if (rigidbody == null) continue;

			rigidbody.isKinematic = false;

			rigidbody.useGravity = true;

			rigidbody.AddExplosionForce(explosionForce, explosionCenter, explosionRadius);
			Debug.Log("TruggerDeathEffects");

			StartCoroutine(DisableObject(rigidbody.gameObject));
		}

	}

	private IEnumerator DisableObject(GameObject obj)
	{
		Debug.Log($"TempTime : {tempTime}");
		yield return new WaitForSeconds(tempTime += 0.3f);
		obj.SetActive(false);
		remainingObjects--;
		yield return null;
		if (remainingObjects <= 0 && CompareTag("Frog"))
		{
			Debug.Log("RetryGame");
			DataManager.Instance.RetryGame();
		}
		else if (remainingObjects <= 0 && CompareTag("Enemy"))
		{
			Debug.Log("Ending");
			DataManager.Instance.EndGame();
		}
	}

}