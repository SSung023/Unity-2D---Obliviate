using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    
    static public PlayerManager instance; //자기 자신을 값으로 갖는 변수 선언
    static private bool isCollied = false; //OnTriggerEnter2D 함수에서 한번만 실행되게 설정하는 변수
    
    public string currentMapName; // transfer script에 있는 transferMapName 변수의 값을 저장
    public string beforeSceneName; //전에 갔었던 map이 어디인지 저장하는 변수
    
    public string walkSound_1;
    public string walkSound_2;
    public string walkSound_3;

    private AudioManager theAudio;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    private bool canMove = true;

    public bool canNotMove = false; //true일 때 움직일 수 없음, false일 때 움직일 수 있음
    //=============================================================


    IEnumerator MoveCoroutine()
    {

        while ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && !canNotMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            //부모 클래스인 MovingObject에서 충돌 확인하는 코드를 불러와 사용
            bool checkCollisionFlag = base.checkCollision();
            if(checkCollisionFlag) //충돌이 일어난 경우 break
                break;

            // 상태 전이 
            animator.SetBool("Walking", true);
            
            //theAudio.Play(walkSound_1);
            boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);
            
            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x *(speed + applyRunSpeed), vector.y * (speed + applyRunSpeed), 0);
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }

                currentWalkCount++;
                
                //npc와 player가 서로 끼는 현상을 방지하는 코드
                if (currentWalkCount == 3)
                {
                    boxCollider.offset = Vector2.zero;
                }
                // if (vector.x != 0)
                // {
                //     transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                // }
                // else if (vector.y != 0)
                // {
                //     transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                // }
                //
                // if (applyRunFlag)
                // {
                //     currentWalkCount++;
                // }
                // currentWalkCount++;
            
                //대기하는 명령어
                yield return new WaitForSeconds(0.01f);
            }

            currentWalkCount = 0;

        }
        animator.SetBool("Walking", false);
        canMove = true;

    }

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
    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>(); //MovingObject에서 선언한 queue 초기화
        
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        
        //audioSource 변수가 Player에 추가되어있는 AudioSource 컴포넌트 컨트롤 가능
        theAudio = FindObjectOfType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !canNotMove)
        {
            //player가 걷는 소리 재생
            theAudio.Play(walkSound_1);
            
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                //theAudio.Play(walkSound_1);
                canMove = false;
                StartCoroutine(MoveCoroutine());
                
            }
        }

    }

    static public bool getIsCollied()
    {
        return isCollied;
    }

    static public void setIsCollied(bool set)
    {
        isCollied = set;
    }

    public string getSoundName()
    {
        return walkSound_1;
    }
    
}
