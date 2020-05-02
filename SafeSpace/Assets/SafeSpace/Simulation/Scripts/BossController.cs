using UnityEngine;

/// <summary>
/// Script to handle animations for Boss
/// </summary>
public class BossController : MonoBehaviour
{
    public Animator animator;
    public void SetTalkOne(bool v)
    {
        animator.SetBool("Angry", v);
    }

    public void SetTalkTwo(bool v)
    {
        animator.SetBool("Sass", v);
    }
}
