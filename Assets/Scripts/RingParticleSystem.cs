using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingParticleSystem : MonoBehaviour
{
    [Range(-1.0f,1.0f)]
    public float XRotation;
    [Range(-1.0f,1.0f)]
    public float YRotation;
    [Range(-1.0f,1.0f)]
    public float ZRotation;

    public float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        var rotation = new Vector3(XRotation, YRotation, ZRotation) * RotationSpeed;
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * Quaternion.Euler(rotation));
    }
}
