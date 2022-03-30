using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LightEffect.instance.mbWarn = true;
            MuiscManager.instance.mbWarn = true;
            EnemyC.instance.Change(emEnemyState.Trace);

        }

    }
}
