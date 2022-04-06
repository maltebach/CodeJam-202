using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    public float shakeForceMultiplier;
    public Rigidbody2D[] shakingRigidbodies;
    
    public void ShakeRigidbodies(Vector3 deviceAcceleration)
    {
        foreach (var rigidbody in shakingRigidbodies)
        {
            rigidbody.AddForce(deviceAcceleration * shakeForceMultiplier, ForceMode2D.Impulse);


        }
    }


}
