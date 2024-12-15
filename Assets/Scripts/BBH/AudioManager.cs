using System;
using System.Collections;
using System.Net.Http;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonManager<AudioManager>
{
    //�ϳ��� ����� �ҽ��� ������� ȿ���� ��� ����� ���� ������, ������� ���߰� ȿ������ ����Ǵ� �����
    //����� �ҽ��� ����Ŀ ���� �����̶� �� �� ���� ����ǰ� �Ϸ��� ���� ��ߵ�
    public AudioSource SFX;
    public AudioSource BGM;
    public AudioClip UIAudioClip;
    //�� �ε����� �´� BGM�Ҵ�
    //����� �ҽ��� �̱������� �ϰ�, ����� Ŭ���� ������ ���� �ִ°� �´°Ű���� �ѵ� ��?��ڴ�
    [Header("���� ����, �� �ε����� �°� �������ֽñ� �ٶ��ϴ�"), Tooltip("0 : GameStartScene\n1 : GameEndScene\n2 : LoadingScene\n3 : BBH_Scene\n4 : JDY_Scene\n5 : KCY_Scene")]
    public AudioClip[] backgroundMusic;
    public AudioPool pool;
    public float SFXVolume = 1f;
    public float BGMVolume = 1f;
    private void Start()
    { 
        SFXVolume = 1f;
        BGMVolume = 1f;
        BGM.loop = true;
        //PlayBGM(backgroundMusic[0]);
        BGMChange(SceneManager.GetActiveScene().buildIndex);
    }

    public void Update()
    {
        #region �ӽ� ����
        //SFX.volume = SFXVolume;
        //BGM.volume = BGMVolume;
        #endregion
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
        //���ڸ� �̸� �ʱ�ȭ �س����� ȣ���� �� ���� �������൵ �˾Ƽ� ��,
        //2D ���� ����� �޼���. �ٸ� Ŭ�� ������̿��� ���ÿ� ��� ������
        //2D ���� �̹Ƿ� ����� �����ʶ� ����� ��ü, �÷��̾�� �ַ� ����� ����
        if (clip != null)
        {
            //SFX.clip = clip;
            //SFX.volume = SFXVolume * volume;
            SFX.PlayOneShot(clip, volume * SFX.volume);
        }
    }

    //DEPRECATED : ���� Destroy��� ��Ȱ��
    public void PlaySFX2(AudioClip clip, Vector3 pos, Transform parent = null, float volume = 1f, float maxDis = 150)
    {

        if (clip != null)
        {
            //�� ������� �߾��µ� �Ҹ��� �ʹ� ����
            // AudioSource sfx = AudioSource.PlayClipAtPoint(clip, pos, volume);

            // ���ο� GameObject�� AudioSource�� �߰��ϰ� ����ؾߵ�
            GameObject tempObj = new GameObject("TempSFX3D");
            tempObj.transform.position = pos;

            if (parent != null)
            {
                //Debug.Log("�θ� ����");
                tempObj.transform.parent = parent;
            }

            AudioSource tempAudioSource = tempObj.AddComponent<AudioSource>();

            tempAudioSource.clip = clip;
            tempAudioSource.spatialBlend = 1.0f; //3D ����� ����
            tempAudioSource.minDistance = 10f; //�ּ� �Ÿ� 
            tempAudioSource.maxDistance = maxDis; //�ִ� �Ÿ� 
            tempAudioSource.rolloffMode = AudioRolloffMode.Linear;
            tempAudioSource.dopplerLevel = 0.0f; // Doppler ȿ�� ����
            //HACK : �ӽù���
            //tempAudioSource.volume = SFX.volume * volume;
            //tempAudioSource.volume = volume;


            //tempAudioSource.Play();
            tempAudioSource.PlayOneShot(clip, SFX.volume * volume);
            Destroy(tempObj, clip.length); //Ŭ�� ���̸�ŭ ��� �� ������Ʈ ����

            //�ٸ� �̷��� GC�� ���� ȣ��Ǽ� �޸𸮰� ȿ���� �ٴ��� ��ԵǸ�
            //���� ���� �� �޼��带 ȣ���� ��ü���� ���°� �ƴ�, ȣ���� ������ ��ġ���� ����ǰ� ��
            //��, �����̴� ��ü���� �����ϸ� �������
            //HACK : �ϴ� �̷��� �ϰ� �ٸ��� �ϸ鼭 ���Ŀ� ������Ʈ Ǯ������ �����丵 �ʿ�
            //(�Ϸ��) ������ ������Ʈ�� �θ� �޼��带 ȣ���� ��ü�� �ڽ����� �����ϸ� ���尡 ��� ���󰥰Ű���
        }
    }

    public void PlaySFX(AudioClip clip, Vector3 pos, Transform parent = null, float volume = 1f)
    {
        //3D ����, ����� �����ʿ��� �Ÿ�, ���⿡ ���� �Ҹ��� �޶���
        if (clip != null)
        {
            //Ǯ���� AudioSource ��������
            AudioSource tempAudioSource = AudioPool.Instance.GetAudioSource();
            tempAudioSource.transform.position = pos;

            //��?�� �Ź� ���ӿ�����Ʈ �����Ҳ��� ������Ʈ Ǯ�� ���ϴ°���
            //GameObject tempObj = tempAudioSource.gameObject;
            //tempObj.transform.position = pos;

            if (parent != null)
            {
                Debug.Log("�θ� ����");
                tempAudioSource.transform.SetParent(parent);
                //tempObj.transform.parent = parent;
            }
            else
            {
                //tempObj.transform.parent = null;
            }

            tempAudioSource.clip = clip;
            //HACK : �ӽù���
            //tempAudioSource.volume = volume;
            tempAudioSource.volume = tempAudioSource.volume * volume;

            tempAudioSource.Play();

            //����� �ٽ� Ǯ�� �־���
            StartCoroutine(ReturnToPool(tempAudioSource, clip.length));
        }
    }

    private IEnumerator ReturnToPool(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);


        if (source == null || source.gameObject == null)
        {
            Debug.LogWarning("����� �ҽ� �Ǵ� ���� ������Ʈ�� �ı��� ���¿��� ������");
        }


        AudioPool.Instance.ReturnAudioSource(source);
    }

    //���� �� �ε����� ���ڷ� �޾Ƽ� BGM�� �ٲ���
    //�̰� ������ �ν�����â���� �Ҵ� �߸��ϸ� ������ BGM�� ����Ǵ� ���ǰ� �ʿ���
    public void BGMChange(int sceneBuildIndex)
    {
        //����ó�� ���� �߰�
        if (0 <= sceneBuildIndex && sceneBuildIndex < backgroundMusic.Length)
        {
            Debug.Log($"���� �� �ε��� : {sceneBuildIndex}");
            // Debug.Log($"���� �� �ε��� : {sceneBuildIndex}");
            if(sceneBuildIndex == 5)
                PlayBGM(backgroundMusic[sceneBuildIndex], 0.4f);
            else
                PlayBGM(backgroundMusic[sceneBuildIndex]);
        }
        else
        {
            Debug.LogWarning($"�߸��� �� �ε����� ������ : {sceneBuildIndex}");
        }
    }

    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        //�̹� ������� BGM�� �ִٸ� ���߰� ���
        if (BGM.isPlaying)
        {
            BGM.Stop();
        }
        BGM.clip = clip;
        //HACK : �ӽ� ����
        //BGM.volume = volume;
        BGM.volume = BGM.volume * volume;
        BGM.Play();
    }

    public void SetVolume(float BGMvalue, float SFXVolume)
    {
        BGM.volume = BGMvalue;
        SFX.volume = SFXVolume;
    }
}
