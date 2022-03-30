using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyContraller : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        PlayerC.instance.WinKey = true;
        //后面可以加数字，表示延迟几秒销毁
        Destroy(this.transform.gameObject);

        


    }
}
