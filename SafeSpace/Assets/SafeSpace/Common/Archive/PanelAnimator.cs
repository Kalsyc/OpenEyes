using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnimator : MonoBehaviour
{
    public GameObject animController;
    public GameObject meditateObj;
    Animator anim;

    public GameObject HideObject;
    // Start is called before the first frame update
    public void StartAnimation()
    {
        anim = animController.GetComponent<Animator>();
        HideObject.SetActive(false);
        // call any method after particular time
        Invoke("PanelAnimation", 1.0f);
    }

    public void PanelAnimation()
    {
        anim.SetBool("isPlaying", true);

        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Debug.Log("Switch!");
            meditateObj.SetActive(false);
            Invoke("SetAnimateFalse", 1.0f);
        }
    }

    public void SetAnimateFalse()
    {
        anim.SetBool("isPlaying", false);
    }

}
