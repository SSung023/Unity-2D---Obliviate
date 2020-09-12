using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogManager : MonoBehaviour
{

    public static DialogManager instance; // 싱글턴 화
    #region Singleton
    private void Awake()
    {
        // 만약 instance가 null이라면(첫 실행의 경우 instace는 null)
        if (instance == null)
        {
            //다른 scene으로 전환할 때, 해당 Object를 파괴하지 말라는 함수
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    public Text text; //Hierarchy view의 text를 교체하기 위한 변수
    
    //spriteRenderer는 sprite 이미지를 render해주는 컴포넌트 && sprite - audioClip, spriteRenderer - AudioSource와 같은 느낌
    public SpriteRenderer rendererSprite;
    public SpriteRenderer rendererDialogueWindow;

    // 배열은 고정적이기 때문에, List를 이용
    private List<string> listSentence; //대화 내용
    private List<Sprite> listSprite; // 캐릭터
    private List<Sprite> listDialogWindow; //대화 창

    private int count; //대화가 얼마나 진행됐는지 확인하기 위한 변수

    //대화 이벤트 시, 실행되는 애니메이션(다이어로그 창 나타나는 모션)
    public Animator animationSprite;
    public Animator animationDialogueWindow;


    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        text.text = "";
        listSentence = new List<string>();
        listSprite = new List<Sprite>();
        listDialogWindow = new List<Sprite>();
    }

    public void showDialogue(Dialog dialogue)
    {
        initDialogue(dialogue);
        
        animationSprite.SetBool("isAppear", true);
        animationDialogueWindow.SetBool("isAppear", true);

        StartCoroutine(startDialogueCoroutine()); //대화가 시작되는 코루틴

    }
    //dialog의 배열에 있던 내용물을 list에 대입
    void initDialogue(Dialog dialogue)
    {
        //리스트에 들어있는 갯수만큼 반복
        for (int i = 0; i < dialogue.sentence.Length; i++)
        {
            listSentence.Add(dialogue.sentence[i]);
            listSprite.Add(dialogue.sprites[i]);
            listDialogWindow.Add(dialogue.dialogWindows[i]);
        }
    }

    IEnumerator startDialogueCoroutine()
    {
        if (count > 0)
        {
            //dialogue의 대화창 변경
            if (listDialogWindow[count] != listDialogWindow[count - 1]) // 말하는 캐릭터가 바뀌는 경우
            {
                animationSprite.SetBool("isChange", true);
                animationDialogueWindow.SetBool("isAppear", false);
                yield return new WaitForSeconds(0.2f);
            
                rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogWindow[count];
                rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
            
                animationDialogueWindow.SetBool("isAppear", true);
                animationSprite.SetBool("isChange", false);
            }
            else //같은 캐릭터가 다른 스프라이트로 바뀌는 경우
            {
                //현재 sprite와 전 sprite가 일치하지 않는다면 change 실행
                if (listSprite[count] != listSprite[count - 1])
                {
                    animationSprite.SetBool("isChange", true);
                    yield return new WaitForSeconds(0.1f);
                    //rendererSprite.sprite = listSprite[count]; 또한 가능
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
            
                    animationSprite.SetBool("isChange", false);
                }
                else //대화창 & 스프라이트가 동일할 경우
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        else //count == 0인 경우
        {
            rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogWindow[count];
            rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
        }
        
        
        // 대화 내용(i번째 문장)의 총 길이만큼 반복
        for (int i = 0; i < listSentence[count].Length; i++)
        {
            text.text += listSentence[count][i]; //한 문장의 맨 앞 글자부터 한 글자씩 차례대로 출력
            yield return new WaitForSeconds(0.01f); // text 출력이 한 글자씩 이루어지도록 특정 시간 대기
        }
        
        
    }

    // 다이어로그의 변수를 모두 초기화 && List들 초기화 && 애니메이션 초기화
    public void exitDialogue()
    {
        count = 0;
        text.text = "";
        
        listSentence.Clear();
        listSprite.Clear();
        listDialogWindow.Clear();
        
        animationSprite.SetBool("isAppear", false);
        animationDialogueWindow.SetBool("isAppear", false);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            count++;
            text.text = "";
            
            if (count == listSentence.Count - 1) //문장의 모든 글자를 출력한 경우
            {
                StopAllCoroutines();
                exitDialogue();
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(startDialogueCoroutine());
            }
        }
    }
}
