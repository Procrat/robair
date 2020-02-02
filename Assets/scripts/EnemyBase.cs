using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected bool isRepaired;
    protected Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //animator = gameObject.GetComponent<Animator>();
    }

    public void RepairMe()
    {
        animator.SetTrigger("EnemyRepaired");
        isRepaired = true;
    }
}
