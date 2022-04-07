using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
public class ShakeDetector : MonoBehaviour
{
    //Based on https://www.youtube.com/watch?v=CPGZZUjTMhU
    public float shakeDetectionThreshold;
    public float minShakeInterval;
    public float waitForSeconds;
    public float shakeWeightPercentile;
    //public float shakePower;
    //public Vector3 currentVelocity;
    //public float maxMoveSpeed = 10;
    //public float smoothTime = 0.3f;
    public int shakeFinish;
    public int shakeFinishMin;
    public int shakeFinishMax;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;
    private bool shaking = false;
    private int shakeCount;

/*    private void OnEnable()
    {
        // All sensors start out disabled so they have to manually be enabled first.
        InputSystem.EnableDevice(Accelerometer.current);
    }

    private void OnDisable()
    {
        InputSystem.DisableDevice(Accelerometer.current);
    }*/



    // Start is called before the first frame update
    void Start()
    {

        shakeFinish = Random.Range(shakeFinishMin, shakeFinishMax);
        sqrShakeDetectionThreshold = Mathf.Pow(shakeDetectionThreshold, 2);
    }

    // Update is called once per frame
    void Update()
    {
        //L�ser accelerometerets v�rdi i dette frame
        //Vector3 acceleration = Accelerometer.current.acceleration.ReadValue();


        // Hvis vi ryster telefonen og der er g�et mere tid end ventetiden OG shakeCount er mindre end den endelige m�ngde af ryst inden 
        if (shakeCount < shakeFinish && Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
            && Time.unscaledTime >= timeSinceLastShake + minShakeInterval)
        {
            timeSinceLastShake = Time.unscaledTime;
            shakeCount++;
            Debug.Log("Shake" + Input.acceleration.sqrMagnitude);
            StartCoroutine(ShakeBag());
        }
        //N�r vi er oppe p� den endelige m�ngde af ryst, resetter vi shakeCount og s�tter en ny shakeFinish v�rdi
        else if (shakeCount == shakeFinish)
        {
            Debug.Log("Tillykke kammerat, her er dit event:");
            shakeCount = 0;
            shakeFinish = Random.Range(shakeFinishMin, shakeFinishMax);

            //INDS�T SIGNAL TIL EVENTMANAGER HER


        }
        if (shaking)
        {
            //Vector3 newPos = new Vector3(0, shakePower * maxMoveSpeed, 0);
            //newPos.y = transform.position.y;
            //transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothTime, maxMoveSpeed);
            //newPos = new Vector3(0, -(shakePower * maxMoveSpeed), 0);
            //newPos.y = transform.position.y;
            //transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothTime, maxMoveSpeed);
            
            Handheld.Vibrate();
            Vector3 newPos = new Vector3(0,Input.acceleration.y * shakeWeightPercentile,0);
            transform.position = newPos;


        }
    }

    //Coroutine der aktiverer shakeren, venter i en lille rums tid og stopper shaker funktionen
    IEnumerator ShakeBag()
    {
        Vector3 originalPos = transform.position;

        if(shaking == false)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(waitForSeconds);

        shaking = false;
        transform.position = originalPos;

    }


}
