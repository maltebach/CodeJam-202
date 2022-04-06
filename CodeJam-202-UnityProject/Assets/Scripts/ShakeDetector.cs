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
    public int shakeFinishMin;
    public int shakeFinishMax;

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
        // Hvis vi ryster telefonen og der er g�et mere tid end ventetiden OG shakeCount er mindre end den endelige m�ngde af ryst inden 
        if (shakeCount < shakeFinish && Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold 
            && Time.unscaledTime >= timeSinceLastShake + minShakeInterval)
        {
            physicsController.ShakeRigidbodies(Input.acceleration);
            timeSinceLastShake = Time.unscaledTime;
            shakeCount++;
        }
        //N�r vi er oppe p� den endelige m�ngde af ryst, resetter vi shakeCount og s�tter en ny shakeFinish v�rdi
        else if (shakeCount == shakeFinish)
        {
            Debug.Log("Tillykke kammerat, her er dit event:");
            shakeCount = 0;
            shakeFinish = Random.Range(shakeFinishMin, shakeFinishMax);
        }
    }
}
