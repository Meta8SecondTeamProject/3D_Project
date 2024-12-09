using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonManager<AudioManager>
{
    //하나의 오디오 소스로 배경음과 효과음 모두 재생할 수는 있으나, 배경음이 멈추고 효과음이 재생되는 방식임
    //오디오 소스는 스피커 같은 역할이라서 둘 다 같이 재생되게 하려면 따로 써야됨
    public AudioSource SFX;
    public AudioSource BGM;

    //씬 인덱스에 맞는 BGM할당
    //재생할 소스만 싱글톤으로 하고, 재생할 클립은 씬마다 따로 있는게 맞는거같기는 한데 몰?루겠다
    [Header("툴팁 있음, 씬 인덱스에 맞게 연결해주시길 바랍니다"), Tooltip("0 : GameStartScene\n1 : GameEndScene\n2 : LoadingScene\n3 : BBH_Scene\n4 : JDY_Scene\n5 : KCY_Scene")]
    public AudioClip[] backgroundMusic;

    private void Start()
    { 
        BGM.loop = true;
        //PlayBGM(backgroundMusic[0]);
        BGMChange(SceneManager.GetActiveScene().buildIndex);
    }

    public void Update()
    {
        if (!Application.isFocused)
        {
            BGM.Pause();
        }
        else
        {
            BGM.UnPause();
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        //인자를 미리 초기화 해놓으면 호출할 때 따로 안적어줘도 알아서 들어감,
        //2D 사운드 재생용 메서드. 다른 클립 재생중이여도 동시에 재생 가능함
        //2D 사운드 이므로 오디오 리스너랑 가까운 객체, 플레이어에서 주로 사용할 예정
        if (clip != null)
        {
            //SFX.clip = clip;
            SFX.PlayOneShot(clip, volume);
        }
    }

    //DEPRECATED : 기존 Destroy방식 멸망함
    //public void PlaySFX(AudioClip clip, Vector3 pos, Transform parent = null, float volume = 1f)
    //{

    //    if (clip != null)
    //    {
    //        //이 방법으로 했었는데 소리가 너무 작음
    //        // AudioSource sfx = AudioSource.PlayClipAtPoint(clip, pos, volume);

    //        // 새로운 GameObject에 AudioSource를 추가하고 재생해야됨
    //        GameObject tempObj = new GameObject("TempSFX3D");
    //        tempObj.transform.position = pos;

    //        if (parent != null)
    //        {
    //            Debug.Log("부모 설정");
    //            tempObj.transform.parent = parent;
    //        }

    //        AudioSource tempAudioSource = tempObj.AddComponent<AudioSource>();

    //        tempAudioSource.clip = clip;
    //        tempAudioSource.spatialBlend = 1.0f; //3D 사운드로 설정
    //        tempAudioSource.minDistance = 10f; //최소 거리 
    //        tempAudioSource.maxDistance = 200f; //최대 거리 
    //        tempAudioSource.rolloffMode = AudioRolloffMode.Linear;
    //        tempAudioSource.dopplerLevel = 0.0f; // Doppler 효과 제거
    //        tempAudioSource.volume = volume;

    //        tempAudioSource.Play();
    //        Destroy(tempObj, clip.length); //클립 길이만큼 재생 후 오브젝트 삭제

    //        //다만 이러면 GC가 자주 호출되서 메모리가 효율이 바닥을 기게되며
    //        //사운드 또한 이 메서드를 호출한 객체에서 나는게 아닌, 호출한 순간의 위치에서 재생되게 됨
    //        //즉, 움직이는 객체에게 적용하면 어색해짐
    //        //HACK : 일단 이렇게 하고 다른일 하면서 추후에 오브젝트 풀링으로 리팩토링 필요
    //        //(완료됨) 생성된 오브젝트의 부모를 메서드를 호출한 객체의 자식으로 설정하면 사운드가 계속 따라갈거같음
    //    }
    //}

    public void PlaySFX(AudioClip clip, Vector3 pos, Transform parent = null, float volume = 1f)
    {
        //3D 사운드, 오디오 리스너와의 거리, 방향에 따라 소리가 달라짐
        if (clip != null)
        {
            //풀에서 AudioSource 가져오기
            AudioSource tempAudioSource = AudioPool.Instance.GetAudioSource();
            tempAudioSource.transform.position = pos;

            //굳?이 매번 게임오브젝트 생성할꺼면 오브젝트 풀링 웨하는거임
            //GameObject tempObj = tempAudioSource.gameObject;
            //tempObj.transform.position = pos;

            if (parent != null)
            {
                Debug.Log("부모 설정");
                tempAudioSource.transform.SetParent(parent);
                //tempObj.transform.parent = parent;
            }
            else
            {
                //tempObj.transform.parent = null;
            }

            tempAudioSource.clip = clip;
            tempAudioSource.volume = volume;
            tempAudioSource.Play();

            //재생후 다시 풀에 넣어줌
            StartCoroutine(ReturnToPool(tempAudioSource, clip.length));
        }
    }

    private IEnumerator ReturnToPool(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioPool.Instance.ReturnAudioSource(source);
    }

    //현재 씬 인덱스를 인자로 받아서 BGM을 바꿔줌
    //이거 때문에 인스펙터창에서 할당 잘못하면 엉뚱한 BGM이 재생되니 주의가 필요함
    public void BGMChange(int sceneBuildIndex)
    {
        //예외처리 로직 추가
        if (0 <= sceneBuildIndex && sceneBuildIndex < backgroundMusic.Length)
        {
           // Debug.Log($"현재 씬 인덱스 : {sceneBuildIndex}");
            PlayBGM(backgroundMusic[sceneBuildIndex]);
        }
        else
        {
            Debug.LogWarning($"잘못된 씬 인덱스에 접근중 : {sceneBuildIndex}");
        }
    }

    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        //이미 재생중인 BGM이 있다면 멈추고 재생
        if (BGM.isPlaying)
        {
            BGM.Stop();
        }
        BGM.clip = clip;
        BGM.volume = volume;
        BGM.Play();
    }
}
