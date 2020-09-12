using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    //공통된 변수
    public string characterName;
    
    public float speed;
    protected Vector3 vector;
    
    public int walkCount;
    protected int currentWalkCount;

    protected bool isNpcMove = true;
    private bool notCoroutine = false;
    
    public BoxCollider2D boxCollider;
    public LayerMask layerMask; // 통과가 불가능한 레이어를 설정해 주는 역할
    public Animator animator;
    
    public Queue<string> queue; //npc 강제 이동 오류를 고치기 위한 자료구조
    //====================================


    
    // RayCast를 이용하여 충돌을 감지하는 함수
    protected bool checkCollision()
    {
        //A지점 -> B지점 이동 시, 성공하면 hit = null, 방해물에 부딫혀 실패 시 hit = 방해물 return
        RaycastHit2D hit;

        Vector2 start = transform.position; // A지점, 캐릭터의 현재 위치 값
        Vector2 end = start + new Vector2(vector.x * speed * walkCount , vector.y * speed * walkCount); // B지점, 캐릭터가 이동하고자 하는 위치 값

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        //충돌이 일어난 경우 true 반환
        if (hit.transform != null)
            return true;

        return false;
    }
    
    //npc의 움직임을 구현하는 함수
    public void Move(string _direction, int _frequency = 5)
    {
        queue.Enqueue(_direction);
        if (!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_direction, _frequency));
        }

    }

    IEnumerator MoveCoroutine(string _str, int _frequency)
    {
        while (queue.Count != 0)
        {
            switch (_frequency)
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
            
            
            string direction = queue.Dequeue();
            
            isNpcMove = false;
            vector.Set(0,0, vector.z);

            // npc의 이동 방향을 받아서 
            switch (direction)
            {
                case "UP":
                    vector.y = 1f;
                    break;
                case "DOWN":
                    vector.y = -1f;
                    break;
                case "LEFT":
                    vector.x = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1f;
                    break;
            }
        
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            
            // npc와 충돌 방지 코드
            while (true)
            {
                bool checkCollisionFlag = checkCollision();
                if (checkCollisionFlag)
                {
                    animator.SetBool("Walking", false);
                    yield return new WaitForSeconds(1f);
                } 

                else
                {
                    break;
                }
            }

            
            animator.SetBool("Walking", true);
        
            boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);
        
            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * speed, vector.y * speed, 0);
                currentWalkCount++;

                if (currentWalkCount == 3)
                {
                    boxCollider.offset = Vector2.zero;
                }
                
                //대기하는 명령어
                yield return new WaitForSeconds(0.01f);
            }

            currentWalkCount = 0;

            //5로 설정한 경우에는 한 발로만 걷는 버그 발생
            // 따라서 5가 아닌 경우에만 walking을 false로 바꿔준다
            if (_frequency != 5)
            {
                animator.SetBool("Walking", false);
            }
        
        
            isNpcMove = true;
        }
        animator.SetBool("Walking", false);
        notCoroutine = false;
    }
    
    
    
    
}