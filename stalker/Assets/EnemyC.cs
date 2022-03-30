using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum emEnemyState {
    Stand,
    Partol,
    Trace,
    Attack,
}
public class EnemyC : MonoBehaviour
{
    public static EnemyC instance;

    private emEnemyState e_State;
    private Animator m_Anim;
    private NavMeshAgent m_navMeshAgent;


    //巡逻点
    public Transform[] m_Warpoints;
    //巡逻点下标
    private int m_CurPointIndex;
    //存储巡逻点坐标
    private Vector3 m_V3Partol;

    //计时器
    private float m_StandTimer;
    //间隔时间
    public float m_InverseStandTimer;
    // Start is called before the first frame update
    //自动寻路的速度
    public float mMoveSpeed;


    // Update is called once per frame
    private void Awake()
    {
        //一开始为待机状态
        m_Anim = transform.Find("Fire Rock Monster").GetComponent<Animator>();
        m_navMeshAgent= transform.GetComponent<NavMeshAgent>();
        e_State = emEnemyState.Stand;
        m_CurPointIndex = 0;
        instance = this;
    }
    void Update()
    {
        switch (e_State)
        {
            case emEnemyState.Stand:
                Standing();
                break;
            case emEnemyState.Partol:
                Partoling();
                break;
            case emEnemyState.Trace:
                Traceing();
                break;
            case emEnemyState.Attack:
                Attacking();
                break;
            default:
                break;
        }
    }

    public void Change(emEnemyState State)
    {
        e_State = State;
    }
    private void Standing()
    {
        //待机动画
        m_Anim.SetBool("bWalk", false);
        m_StandTimer += Time.deltaTime;
        if (m_StandTimer >= m_InverseStandTimer)
        {
            m_StandTimer = 0;
            //切换到巡逻
            Change(emEnemyState.Partol);
        }
    }
    private void Partoling()
    {
        m_Anim.SetBool("bWalk", true);

        if (m_V3Partol == Vector3.zero)
        {
            if (m_CurPointIndex >= m_Warpoints.Length)
            {
                m_CurPointIndex = 0;
            }
            m_V3Partol = m_Warpoints[m_CurPointIndex].position;
            m_CurPointIndex++;

            //设置巡逻的速度
            m_navMeshAgent.speed = mMoveSpeed;
            //设置巡逻的路径点
            m_navMeshAgent.destination = m_V3Partol;
            //设置停止距离
            m_navMeshAgent.stoppingDistance = 0.8f;
        }

        //到达目的后切换到待机
        //向量距离，判断第一个向量到第二个向量的模长
        if (Vector3.Distance(transform.position,m_V3Partol)<=m_navMeshAgent.stoppingDistance)
        {
            m_V3Partol = Vector3.zero;
            e_State = emEnemyState.Stand;
        }
    }
    private void Traceing()
    {
        if (Vector3.Distance(transform.position, m_V3Partol) <= m_navMeshAgent.stoppingDistance)
        {
            m_V3Partol = Vector3.zero;
            Change(emEnemyState.Attack);
            return;
        }
        m_V3Partol = PlayerC.instance.transform.position;
        //设置巡逻的速度
        m_navMeshAgent.speed = mMoveSpeed;
        //设置巡逻的路径点
        m_navMeshAgent.destination = m_V3Partol;
        //设置停止距离
        m_navMeshAgent.stoppingDistance = 1.8f;
        m_Anim.SetBool("bWalk", true);
    }
    private void Attacking()
    {
        if (Vector3.Distance(transform.position, PlayerC.instance.transform.position) >= m_navMeshAgent.stoppingDistance)
        {
            m_Anim.SetBool("bAttack", false);
            Change(emEnemyState.Trace);
            return;
        }
        m_Anim.SetBool("bWalk", false);
        m_Anim.SetBool("bAttack", true);
        if (PlayerC.instance.playerHp <= 0||PlayerC.instance.WinKey==true)
        {
            m_Anim.SetBool("bAttack", false);
            Change(emEnemyState.Partol);
        }
    }
}
