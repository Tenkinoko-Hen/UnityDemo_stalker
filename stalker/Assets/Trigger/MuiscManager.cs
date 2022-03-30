using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuiscManager : MonoBehaviour
{
    public static MuiscManager instance;
    public AudioSource mNormal;
    public AudioSource mWarn;
    public bool mbWarn=false;

    public AudioSource[] mPropMegaphones;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        if (mbWarn)
        {

            if (mNormal.isPlaying)
            {
                mNormal.Stop();
            }
            if (!mWarn.isPlaying)
            {
                Debug.Log("BGM1");
                mWarn.Play();
            }
            for (int i = 0; i < mPropMegaphones.Length; i++)
            {
                if(!mPropMegaphones[i].isPlaying)
                mPropMegaphones[i].Play();
            }
        }
       
    }
}
