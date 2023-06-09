using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo : MonoBehaviour
{
    public Animator anim;
    public LogicaEnemigo enemigo;
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("Player"))
        {
            Debug.Log("entre");
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("attack", true);
            enemigo.atacando = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
