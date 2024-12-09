using System.Collections;
using UnityEngine;
using UniRan = UnityEngine.Random;

public class FliesMovement : MonoBehaviour
{
	[Range(0, 1)]
	public float upSpeed;
	[Range(0, 1)]
	public float YAmplitude;
	[Range(0, 1)]
	public float rotateSpeed;

	public Transform rotateTarget;

	private int rand;
	private float dir;

	public AudioClip idleClip;

	private void Awake()
	{
		rand = UniRan.Range(0, 2);
	}

	//NOTE : 사운드 추가
    private void Start()
    {
        StartCoroutine(PlayIdleClipCoroutine());
    }

    private IEnumerator PlayIdleClipCoroutine()
    {
        while (gameObject.activeSelf == true)
        {
			yield return new WaitForSeconds(idleClip.length + Random.Range(3f,5f));                                                        
            AudioManager.Instance.PlaySFX(idleClip, transform.position, this.transform);
        }
    }

    private void Update()
	{
		Movement();
		
	}
	private void Movement()
	{
		if (Time.timeScale == 0) return;
		if (rand == 0)
		{
			dir = Mathf.Sin(Time.time * upSpeed) * YAmplitude * 0.1f;
		}
		if (rand == 1)
		{
			dir = Mathf.Cos(Time.time * upSpeed) * YAmplitude * 0.1f;
		}
		transform.position = new Vector3(transform.position.x, transform.position.y + dir, transform.position.z);
		transform.RotateAround(rotateTarget.position, Vector3.down, rotateSpeed);
	}
}
