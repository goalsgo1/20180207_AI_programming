using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankAi : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private Ray ray;
    private RaycastHit hit;
    private float maxDistanceToCheck = 6.0f;
    private float currentDistance; //현재 거리
    private Vector3 checkDirection; //체크해야할 좌표값

    public Transform pointA; 
    public Transform pointB;
    public NavMeshAgent navMeshAgent;

    private int currentTarget; //현재 타겟
    private float distanceFromTarget; //타겟과의 거리
    private Transform[] waypoints = null; //배열로선언한 이유: 여러개 쓰려고

    private void Awake() // 스타트보다 먼저 초기화 될때 사용된다.
    {
        player = GameObject.FindWithTag("Player"); //플레이어가 누군지 찾아준다
        animator = gameObject.GetComponent<Animator>(); //
        pointA = GameObject.Find("p1").transform; 
        pointB = GameObject.Find("p2").transform; 
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>(); 
        waypoints = new Transform[2]            //여기부터
        {
            pointA,
            pointB
        };                                      //여기랑

        //waypoints = new Transform[2];         //여기부터
        //waypoints[1] = pointA;
        //waypoints[2] = pointB;                //여기랑 같은 말

        currentTarget = 0;
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }

    private void FixedUpdate()
    {
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        animator.SetFloat("distanceFromPlayer", currentDistance);

        checkDirection = player.transform.position - transform.position;
        ray = new Ray(transform.position, checkDirection);
        if(Physics.Raycast(ray,out hit, maxDistanceToCheck))
        {
            if(hit.collider.gameObject == player)
            {
                animator.SetBool("isPlayerVisible", true);
            }
            else
            {
                animator.SetBool("isPlayerVisible", false);
            }
        }
        else
        {
            animator.SetBool("isPlayerVisible", false);
        }

        distanceFromTarget = Vector3.Distance(waypoints[currentTarget].position, transform.position);
        animator.SetFloat("distanceFromWaypoint", distanceFromTarget);
    }

    public void SetNextPoint() 
    {
        switch (currentTarget)
        {
            case 0:
                currentTarget = 1; //현재 타겟이 1인 경우
                break;
            case 1:
                currentTarget = 0; //현재 타겟이 2인 경우
                break;
        }
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }
}
