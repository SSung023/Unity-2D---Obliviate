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
    [Tooltip("1 = 천천히, 2 = 조금 천천히, 3 = 보통, 4 = 조금 빠르게, 5 = 빠르게")]
    public int frequency; //npc가 움직일 방향으로 얼마나 빠른 속도로 움직일 것인가 
}

public class NPCmanager : MovingObject
{
    [SerializeField]
    public NPCMove npc;
    
    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        //StartCoroutine(MoveCoroutine());
        
        // if(npc.isNpcMove)
        //     setMove();
        // else
        // {
        //     setNotMove();
        // }
    }

    public void setMove()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void setNotMove()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveCoroutine()
    {
        if (npc.direction.Length != 0)
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {
                //npc가 해당하는 시간만큼 대기하고 다시 움직이게 함
                switch (npc.frequency)
                {
                    case 1:
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4: 
                        yield return new WaitForSeconds(1f);
                        break;    
                    case 5:
                        break;

                }
                
                yield return new WaitUntil(() => isNpcMove); //isNpcMove가 true가 될 때까지 대기
                //실질적인 이동 구간
                base.Move(npc.direction[i], npc.frequency);
                
                // i를 -1로 설정 함으로써 for문이 계속 실행되도록 설정
                if (i == npc.direction.Length - 1)
                {
                    i = -1;
                }
                
                
            }
        }
    }
}
