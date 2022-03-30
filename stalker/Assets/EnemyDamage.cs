using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public void Damage(int Damage)
    {
        PlayerC.instance.playerHp -= Damage;
        PlayerC.instance.animator.SetTrigger("bHurt");
    }
}
