using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Invokes first event upon start of simulation
/// </summary>
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
