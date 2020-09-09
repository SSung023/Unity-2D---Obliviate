using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    //공통된 변수
    public float speed;
    protected Vector3 vector;
    
    public int walkCount;
    protected int currentWalkCount;
    
    public BoxCollider2D boxCollider;
    public LayerMask layerMask; // 통과가 불가능한 레이어를 설정해 주는 역할
    public Animator animator;
    //====================================
}