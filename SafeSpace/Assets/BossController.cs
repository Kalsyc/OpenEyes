using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Animator animator;
    public void SetTalkOne(bool v)
    {
        animator.SetBool("Angry", v);
    }

    public void SetTalkTwo(bool v)
    {
        animator.SetBool("Sassy", v);
    }
}
