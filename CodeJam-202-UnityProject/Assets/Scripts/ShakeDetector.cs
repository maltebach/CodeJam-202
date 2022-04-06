using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(PhysicsController))]
public class ShakeDetector : MonoBehaviour
{
    //Based on https://www.youtube.com/watch?v=CPGZZUjTMhU
    public float shakeDetectionThreshold;
    public float minShakeInterval;
    public int shakeFinish;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;
    public int shakeCount;

    private PhysicsController physicsController;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sqrShakeDetectionThreshold = Mathf.Pow(sqrShakeDetectionThreshold, 2);
        physicsController = GetComponent<PhysicsController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeCount < shakeFinish && Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold 
            && Time.unscaledTime >= timeSinceLastShake + minShakeInterval)
        {
            physicsController.ShakeRigidbodies(Input.acceleration);
            timeSinceLastShake = Time.unscaledTime;
            shakeCount++;
        }
        else if (shakeCount == shakeFinish)
        {
            Debug.Log("Tillykke kammerat, her er dit event:");
        }
    }
}
