using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffect : MonoBehaviour
{

    public bool mbWarn;
    public Light mNormal;
    public Light mWan;

    private float m_time;
    public float m_timeWait;
    public static LightEffect instance;

    private void Awake()
    {
        instance = this;
        mbWarn = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mbWarn)
        {
            mWan.intensity = 1.5f;
            m_time += Time.deltaTime;
            if (m_time >= m_timeWait)
            {
                mWan.enabled = !mWan.enabled;
                m_time = 0;
            }
        }
    }
}
