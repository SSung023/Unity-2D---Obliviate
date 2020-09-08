using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    static public BgmManager instance;

    public AudioClip[] clips; // 배경 음악들
    private AudioSource source;
    
    //코루틴 yield문에서 계속 new 실행을 줄이기 위한 변수
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    
    
    // 다른 Scene으로 가도 파괴 되지 않게 설정
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
    }
    
    //BGM 관련 함수 구현
    public void Play(int _musicTrack)
    {
        source.volume = 1.0f;
        // 플레이 하고자 하는 사운드를 불러온다
        source.clip = clips[_musicTrack];
        source.Play();
    }

    public void SetVolume(float _volume)
    {
        source.volume = _volume;
    }
    
    //일시정지
    public void Pause()
    {
        source.Pause();
    }
    // 일시정지 해제
    public void UnPause()
    {
        source.UnPause();
    }
    
    public void Stop()
    {
        source.Stop();
    }
    public void FadeOutMusic()
    {
        StopAllCoroutines(); //FadeIn과 FadeOut이 동시에 실행되면 안되기 때문에 모든 Coroutine을 멈춘다
        StartCoroutine(FadeOutMusicCoroutine());
    }
    IEnumerator FadeOutMusicCoroutine()
    {
        // 볼륨을 1부터 0까지 서서히 줄인다
        for (float i = 1.0f; i >= 0f; i -= 0.01f)
        {
            source.volume = i;

            yield return waitTime;
        }
    }

    
    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }
    IEnumerator FadeInMusicCoroutine()
    {
        // 볼륨을 1부터 0까지 서서히 줄인다
        for (float i = 0f; i <= 1.0f; i += 0.01f)
        {
            source.volume = i;

            yield return waitTime;
        }
    }
    
}
