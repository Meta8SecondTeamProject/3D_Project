using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : SingletonManager<AudioPool>
{
	//Ǯ�� �̸� �����ص� ����� �ҽ��� ��
	[SerializeField] private int poolSize = 10;
	//���� �� ť�� ����� ����� �� ��� : �̰� �� ���� ����̿���� 
	private Queue<AudioSource> audioSourcePool = new Queue<AudioSource>();

	protected override void Awake()
	{
		base.Awake();
		InitializePool();
	}

	//Ǯ�ʱ�ȭ
	void InitializePool()
	{
		//�̸� �ʱ�ȭ�س��� Ǯ�����ŭ ������Ʈ�� �����ϰ� ���� ���� ����
		for (int i = 0; i < poolSize; i++)
		{
			GameObject obj = new GameObject("PooledSFX"); //������Ʈ �̸�
			obj.transform.SetParent(this.transform); //Ǯ ���� ������Ʈ�� �ڽ����� �ּ� ���� ���ϰ�
			AudioSource audioSource = obj.AddComponent<AudioSource>(); //����� �ҽ� ������Ʈ �����ϰ� ���� ����
			audioSource.spatialBlend = 1.0f; //3D ���� ����, 0.5�� �ϸ� 2.5D ���� ��
			audioSource.minDistance = 10f; //�Ҹ��� �ִ�� �鸮�� �ּ� �Ÿ�
			audioSource.maxDistance = 200f; //�Ҹ��� �鸮�� �ִ� �Ÿ�
			audioSource.rolloffMode = AudioRolloffMode.Linear; //�Ҹ� ���� ���? 
			audioSource.dopplerLevel = 0.0f; //���÷� ȿ�� ��Ȱ��ȭ, ��ġ �����ε� ���� ���߿� �Ǵ�
			obj.SetActive(false); //�����ϰ��� ��Ȱ��ȭ �س���
			audioSourcePool.Enqueue(audioSource); //������ ������Ʈ�� ť�� �������
		}
	}

	public AudioSource GetAudioSource()
	{
		//ť�� �ִ� ����� �ҽ��� ������ Ȱ��ȭ�ϴ� �޼���
		//�����ִ� ����� �ҽ��� 0���� ������ �����
		//Queue.Count�� List.Countó�� ũ�Ⱑ �ƴ� �����ִ� ����� ������ �򰥸��� �Ӹ�������
		if (audioSourcePool.Count > 0)
		{
			//Dequeue �� �� ����Ǹ� Count--�� 
			AudioSource source = audioSourcePool.Dequeue();
			//��Ȱ��ȭ���ִ� ������Ʈ�� Ȱ��ȭ ������
			source.gameObject.SetActive(true);
			return source;
		}

		//Ǯ�� �����ϸ� ���� ����
		GameObject obj = new GameObject("PooledSFX");
		AudioSource audioSource = obj.AddComponent<AudioSource>();
		audioSource.spatialBlend = 1.0f;
		audioSource.minDistance = 10f;
		audioSource.maxDistance = 200f;
		audioSource.rolloffMode = AudioRolloffMode.Linear;
		audioSource.dopplerLevel = 0.0f;
		return audioSource;
	}

	public void ReturnAudioSource(AudioSource source)
	{
		//�ٽ� ť�� �ִ� �޼���
		//AudioManager���� �ڷ�ƾ���� �����
		source.Stop();
		source.clip = null;
		source.gameObject.SetActive(false);
		audioSourcePool.Enqueue(source);
	}
}
