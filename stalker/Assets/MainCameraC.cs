using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraC : MonoBehaviour
{
    private PlayerC m_Player;
    private Vector3 m_Camera;
    private float m_magnitude;

    public float m_MoveSpeed=2f;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerC>();
        m_Camera = this.transform.position - m_Player.transform.position;
        //Vector3.magnitude 返回向量的长度，也就是点P(x,y,z)到原点(0,0,0)的距离。 最常用的是用来返回物体的移动速度 
        m_magnitude = m_Camera.magnitude;
        
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.position = m_Player.transform.position + m_Camera;
        CalePoint();
        //始终朝向玩家
        Vector3 dir = m_Player.transform.position - this.transform.position;

        Quaternion targetQ = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQ, Time.deltaTime * m_MoveSpeed);

    }

    void CalePoint()
    {
        Vector3 standPoint = m_Player.transform.position+m_Camera;
        Vector3 absovePoint = m_Player.transform.position + Vector3.up * m_magnitude;

        Vector3[] camPoints = new Vector3[5];
        camPoints[0] = standPoint;
        //线性插值 Vector3.Lerp(起始点，终点，两点平均数)
        camPoints[1] = Vector3.Lerp(standPoint, absovePoint, 0.25f);
        camPoints[2] = Vector3.Lerp(standPoint, absovePoint, 0.5f);
        camPoints[3] = Vector3.Lerp(standPoint, absovePoint, 0.75f);
        camPoints[4] = absovePoint;
        //向量变量要赋初值
        Vector3 newPoint = Vector3.zero ;
        for(int i = 0; i < camPoints.Length; i++)
        {                      //从哪里开始打  打的方向是 玩家位置-参考点等于参考点指向玩家的向量
            if(Physics.Raycast(camPoints[i], m_Player.transform.position - camPoints[i],out RaycastHit hit , m_magnitude,1<<8))
            {

                if (hit.transform.tag != "Player")
                    continue;

                newPoint = camPoints[i];
                break;


            }
        }
        this.transform.position = Vector3.Lerp(transform.position,newPoint,Time.deltaTime* m_MoveSpeed);



    }
}
