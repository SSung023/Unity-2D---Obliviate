using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    static public MovingObject instance; //자기 자신을 값으로 갖는 변수 선언
    
    public string currentMapName; // transfer script에 있는 transferMapName 변수의 값을 저장
    public string beforeSceneName; //전에 갔었던 map이 어디인지 저장하는 변수
    
    private BoxCollider2D boxCollider;
    public LayerMask layerMask; // 통과가 불가능한 레이어를 설정해 주는 역할


    public string walkSound_1;
    public string walkSound_2;
    public string walkSound_3;

    private AudioManager theAudio;
    
    public float speed;
    private Vector3 vector;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;

    private Animator animator;

    IEnumerator MoveCoroutine()
    {

        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
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

            //A지점 -> B지점 이동 시, 성공하면 hit = null, 방해물에 부딫혀 실패 시 hit = 방해물 return
            RaycastHit2D hit;

            Vector2 start = transform.position; // A지점, 캐릭터의 현재 위치 값
            Vector2 end = start + new Vector2(vector.x * speed * walkCount , vector.y * speed * walkCount); // B지점, 캐릭터가 이동하고자 하는 위치 값

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null)
                break;

            // 상태 전이 
            animator.SetBool("Walking", true);
            
            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
            
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
            
                //대기하는 명령어
                yield return new WaitForSeconds(0.01f);
            }

            currentWalkCount = 0;

        }
        animator.SetBool("Walking", false);
        canMove = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        // 만약 instance가 null이라면(첫 실행의 경우 instace는 null)
        if (instance == null)
        {
            //다른 scene으로 전환할 때, 해당 Object를 파괴하지 말라는 함수
            DontDestroyOnLoad(this.gameObject);
            boxCollider = GetComponent<BoxCollider2D>();
            //audioSource 변수가 Player에 추가되어있는 AudioSource 컴포넌트 컨트롤 가능
            animator = GetComponent<Animator>();
            theAudio = FindObjectOfType<AudioManager>();
            
            instance = this; //자기 자신을 대입
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //theAudio.Play(walkSound_1);
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                theAudio.Play(walkSound_1);
                canMove = false;
                StartCoroutine(MoveCoroutine());
                
            }
        }

    }
}