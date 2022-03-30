using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Lasers
{
    Normal,
    Auto,
}
public class LaserEffect : MonoBehaviour
{
    public Lasers e_LasersType;
    private new MeshRenderer renderer;
    private new Collider collider;

    public float e_timeWait;
    private float e_time;


    private EnemyC enemy;

    private void Awake()
    {
        renderer = this.transform.GetComponent<MeshRenderer>();
        collider = this.transform.GetComponent<BoxCollider>();

        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyC>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (e_LasersType == Lasers.Normal)
        {
            return;
        }
        e_time += Time.deltaTime;
        if (e_time >= e_timeWait)
        {
            renderer.enabled = !renderer.enabled;
            collider.enabled = !collider.enabled;
            e_time = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GoWarn();
        }
        else
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
        else
        {
            return;
        }
    }

    private void GoWarn()
    {
        LightEffect.instance.mbWarn = true;
        MuiscManager.instance.mbWarn = true;
        enemy.Change(emEnemyState.Trace);
    }
}
