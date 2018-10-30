using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    public float HP = 100.0f;
    public float MoveSpeed = 2.0f;
    public float DetectRange = 2.0f;
    public float AttackRange = 1.0f;
    public enum StartStatus { Patrol, Attack };
    StartStatus startStatus = StartStatus.Patrol;
    public Transform[] WayPoints;
    int cur = 0;
    public GameObject Player;

    private enum Status { Patrol, Attack };
    private Status currentStatus;
    private float Dis;
    private int index;
    private bool dead = false;
    // Use this for initialization
    void Start()
    {
        if (startStatus == StartStatus.Patrol)
            Patrol();
        else if (startStatus == StartStatus.Attack)
            Attack();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (currentStatus == Status.Patrol)
            Patrol();
        else if (currentStatus == Status.Attack)
            Attack();

        if (HP <= 0 && !dead)
            // ... call the death function.
            Dead();
    }

    //巡逻状态
    void Patrol()
    {
        print("Patrol");
        currentStatus = Status.Patrol;
        if (!patrolToAttack())
        {
            //walk among points or idle
            walkAround();
        }
        else
            Attack();
    }

    //攻击状态
    void Attack()
    {
        currentStatus = Status.Attack;
        if (attackToPartrol())
            Patrol();
        else if (playerInAttackRange())
        {
            attackPerparing();
            attackActing();
            attackPadding();
        }
        else
        {
            approachPlayer();
        }

    }

    //死亡状态
    void Dead()
    {

    }

    //巡逻-》攻击 转换条件
    bool patrolToAttack()
    {
        // 玩家进入攻击范围 or 收到攻击
        bool res = detectPlayer();
        return res;
    }

    //攻击-》巡逻 转换条件
    bool attackToPartrol()
    {
        // 
        return false;
    }

    //侦察到玩家？
    bool detectPlayer()
    {
        //Dis = Vector3.Distance(transform.position, Player.transform.position);
        float Dis = 3.0f;
        if (Dis <= DetectRange)
            return true;
        else
            return false;
    }

    //玩家在攻击范围内？
    bool playerInAttackRange()
    {
        //        Dis = Vector3.Distance(transform.position, Player.transform.position);
        float Dis = 3.0f;
        print("playerdistance");
        print(Dis);
        if (Dis <= AttackRange)
            return true;
        else
            return false;
    }

    //在路经点间巡逻
    void walkAround()
    {
        print("walkaround");
        // Set the enemy's velocity to moveSpeed in the x direction.
        if (Vector3.Distance(transform.position, WayPoints[index].position) > 2)
        {
            Vector2 p = Vector2.MoveTowards(transform.position,
                                            WayPoints[index].position,
                                            MoveSpeed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        // Waypoint reached, select next one
        else index = (index + 1) % WayPoints.Length;

    }

    void idle()
    {

    }

    //接近玩家
    void approachPlayer()
    {
        if (transform.position != Player.transform.position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, Player.transform.position, MoveSpeed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
    }

    //准备攻击
    void attackPerparing()
    {

    }

    //攻击动作
    void attackActing()
    {

    }

    //攻击间隔
    void attackPadding()
    {

    }


}
