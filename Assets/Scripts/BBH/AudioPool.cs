using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : SingletonManager<AudioPool>
{
	//풀에 미리 생성해둘 오디오 소스의 수
	[SerializeField] private int poolSize = 10;
	//누가 왜 큐를 썼는지 물어보면 할 대답 : 이게 젤 쉬운 방법이였어요 
	private Queue<AudioSource> audioSourcePool = new Queue<AudioSource>();

	protected override void Awake()
	{
		base.Awake();
		InitializePool();
	}

	//풀초기화
	void InitializePool()
	{
		//미리 초기화해놓은 풀사이즈만큼 오브젝트를 생성하고 사운드 관련 설정
		for (int i = 0; i < poolSize; i++)
		{
			GameObject obj = new GameObject("PooledSFX"); //오브젝트 이름
			obj.transform.SetParent(this.transform); //풀 관리 오브젝트의 자식으로 둬서 보기 편하게
			AudioSource audioSource = obj.AddComponent<AudioSource>(); //오디오 소스 컴포넌트 부착하고 관련 설정
			audioSource.spatialBlend = 1.0f; //3D 관련 설정, 0.5로 하면 2.5D 사운드 됨
			audioSource.minDistance = 10f; //소리가 최대로 들리는 최소 거리
			audioSource.maxDistance = 200f; //소리가 들리는 최대 거리
			audioSource.rolloffMode = AudioRolloffMode.Linear; //소리 감쇠 방식? 
			audioSource.dopplerLevel = 0.0f; //도플러 효과 비활성화, 피치 관련인데 들어보고 나중에 판단
			obj.SetActive(false); //생성하고나면 비활성화 해놓고
			audioSourcePool.Enqueue(audioSource); //생성된 오브젝트들 큐에 집어넣음
		}
	}

	public AudioSource GetAudioSource()
	{
		//큐에 있는 오디오 소스를 꺼내서 활성화하는 메서드
		//남아있는 오디오 소스가 0보다 많으면 실행됨
		//Queue.Count는 List.Count처럼 크기가 아닌 남아있는 요소의 개수라서 헷갈리면 머리아파짐
		if (audioSourcePool.Count > 0)
		{
			//Dequeue 한 번 실행되면 Count--됨 
			AudioSource source = audioSourcePool.Dequeue();
			//비활성화되있는 오브젝트를 활성화 시켜줌
			source.gameObject.SetActive(true);
			return source;
		}

		//풀이 부족하면 새로 생성
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
		//다시 큐로 넣는 메서드
		//AudioManager에서 코루틴으로 실행됨
		source.Stop();
		source.clip = null;
		source.gameObject.SetActive(false);
		audioSourcePool.Enqueue(source);
	}
}
