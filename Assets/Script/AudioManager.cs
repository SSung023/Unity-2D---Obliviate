using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
[System.Serializable]
public class Sound
{
    public string name; // 사운드의 이름

    public AudioClip clip; // 사운드 파일
    private AudioSource source; // 사운드 플레이어

    public float Volumn;
    public bool loop;
    
    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = Volumn;
        source.loop = loop;
    }

    public void SetVolumn()
    {
        source.volume = Volumn;
    }
    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void setLoop()
    {
        source.loop = true;
    }

    public void cancelLoop()
    {
        source.loop = false;
    }
}

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public Sound[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        //Use this for initialization
        for (int i = 0; i < sounds.Length; i++)
        {
            //하이어라키 뷰에 추가될 객체의 이름
            GameObject soundObject = new GameObject("사운드 파일 이름: " + i + " = " + sounds[i].name );
            sounds[i].setSource(soundObject.AddComponent<AudioSource>());
            
            //추가한 사운드들이 하이어라키 뷰에서 AudioManager 하위 객체로 들어가게끔 설정
            soundObject.transform.SetParent(this.transform);
        }
    }

    public void Play(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void Stop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }
    
    public void SetLoop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].setLoop();
                return;
            }
        }
    }
    public void CancelLoop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].cancelLoop();
                return;
            }
        }
    }


}
