using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerC : MonoBehaviour
{

    private Animator autoDoor;

    // Start is called before the first frame update
    void Start()
    {
        autoDoor = this.transform.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        autoDoor.SetBool("bOpen", true);
    }
    private void OnTriggerExit(Collider other)
    {
        autoDoor.SetBool("bOpen", false);

    }
}
