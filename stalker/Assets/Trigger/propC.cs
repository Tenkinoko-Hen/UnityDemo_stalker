using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propC : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Lightings;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for(int i = 0; i < Lightings.Length; i++)
            {
                Lightings[i].SetActive(false);
            }
        }   
    }
}
