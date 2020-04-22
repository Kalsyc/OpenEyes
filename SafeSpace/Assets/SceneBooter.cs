using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneBooter : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent bootEvent;
    public GameObject toInstantiate;
    void Start()
    {
        bootEvent.Invoke();
    }
}
