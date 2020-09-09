using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//npc의 움직임을 담당하는 커스텀 클래스
[System.Serializable]
public class NPCMove
{
    [Tooltip("isNpcMove를 체크하면 NPC가 움직임")]
    public bool isNpcMove;

    public string[] direction; //npc가 움직일 방향 설정
    
    [Range(1, 5)] //inspector 창에 스크롤
    public int frequency; //npc가 움직일 방향으로 얼마나 빠른 속도로 움직일 것인가 
}

public class NPCmanager : MonoBehaviour
{
    [SerializeField]
    public NPCMove npc;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
