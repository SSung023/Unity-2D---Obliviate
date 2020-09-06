using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound
{
    public string name; // 사운드의 이름

    public AudioClip clip; // 사운드 파일
    private AudioSource source; // 사운드 플레이어

    public float volumn;
    public bool loop;
    
    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = volumn;
        source.loop = loop;
    }

}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            //하이어라키 뷰에 추가될 객체의 이름..?
            GameObject soundObject = new GameObject("사운드 파일 이름: " + i + " = " + sounds[i].name );
            sounds[i].setSource(soundObject.AddComponent<AudioSource>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
