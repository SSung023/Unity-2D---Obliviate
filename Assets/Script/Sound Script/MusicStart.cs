using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStart : MonoBehaviour
{
    BgmManager bgm;

    public int playMusicTrack;
    
    // Start is called before the first frame update
    void Start()
    {
        bgm = FindObjectOfType<BgmManager>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bgm.Play(playMusicTrack);
        this.gameObject.SetActive(false);
    }
}
