using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 이벤트 관련 이동 명령 함수들을 모아 놓은 클래스
public class OrderManager : MonoBehaviour
{

    private PlayerManager thePlayer; // 이벤트 도중에 플레이어의 키 입력 처리 방지를 위한 변수
    private List<MovingObject> characters; // npc의 수는 유동적이기 때문에 List 사용 Add(), Remove(), Clear()
    
    
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    //이 함수를 통해서 character list를 채워넣는다
    public void preLoadCharacter()
    {
        characters = toList();//toList에서 반환한 List를 characters로 전달
    }

    public List<MovingObject> toList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>(); //MovingObject가 포함된 모든 오브젝트를 찾아서 배열에 적용

        for (int i = 0; i < temp.Length; i++)
        {
            tempList.Add(temp[i]);
        }
        return tempList;
    }

    public void Move(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName) //일치할 경우 MovingObject의 Move함수 실행
            {
                characters[i].Move(_dir);
            }
        }
    }

    public void Turn(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName) //일치할 경우 MovingObject의 Move함수 실행
            {
                characters[i].animator.SetFloat("DirX", 0f);
                characters[i].animator.SetFloat("DirY", 0f);
                
                switch (_dir)
                {
                    case "UP":
                        characters[i].animator.SetFloat("DirY", 1f);
                        break;
                    case "DOWN":
                        characters[i].animator.SetFloat("DirY", -1f);
                        break;
                    case "LEFT":
                        characters[i].animator.SetFloat("DirX", -1f);
                        break;
                    case "RIGHT":
                        characters[i].animator.SetFloat("DirX", 1f);
                        break;
                }

            }
        }
    }

    public void setInvisible(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName) //일치할 경우 MovingObject의 Move함수 실행
            {
                characters[i].gameObject.SetActive(false);
            }
        }
    }

    public void setVisible(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName) //일치할 경우 MovingObject의 Move함수 실행
            {
                characters[i].gameObject.SetActive(true);
            }
        }
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
