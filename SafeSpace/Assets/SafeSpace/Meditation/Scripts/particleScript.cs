using UnityEngine;

/// <summary>
/// Controls the particle circle
/// </summary>
public class ParticleScript : MonoBehaviour
{
    public Vector3 speed;

    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
}
