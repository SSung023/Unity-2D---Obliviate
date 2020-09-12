using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 커스텀 클래스
[System.Serializable]
public class Dialog
{
    [TextArea(1,2)]
    public string[] sentence; //출력할 대화의 내용
    public Sprite[] sprites; //다이어로그 창에 보일 캐릭터의 스프라이트
    public Sprite[] dialogWindows; //다이어로그 창의 이름
    
    
    
}
