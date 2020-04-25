using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BootEvent : MonoBehaviour
{
    public UnityEvent toInvoke;
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(Delay());
        
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        toInvoke.Invoke();
    }

}
