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
    public int shakeFinish;
    public int shakeFinishMin;
    public int shakeFinishMax;
    public ParticleSystem shaker;

    [SerializeField] private AudioClip shakeSound;
    [SerializeField] private AudioClip finishSound;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;
    public bool shaking = false;
    private int shakeCount;

    //private void OnEnable()
    //{
    //    // All sensors start out disabled so they have to manually be enabled first.
    //    InputSystem.EnableDevice(Accelerometer.current);
    //}

    //private void OnDisable()
    //{
    //    InputSystem.DisableDevice(Accelerometer.current);
    //}



    // Start is called before the first frame update
    void Start()
    {
        //if (Accelerometer.current != null)
        //{
        //    InputSystem.EnableDevice(Accelerometer.current);
        //}
        
        //Sætter mængden af ryst der skal til før den stopper til et tilfældigt tal inden for et bestemt omfang
        shakeFinish = Random.Range(shakeFinishMin, shakeFinishMax);
        sqrShakeDetectionThreshold = Mathf.Pow(shakeDetectionThreshold, 2);

        Vibration.Init();
    }

    // Update is called once per frame
    void Update()
    {
        //Læser accelerometerets værdi i dette frame
        //Vector3 acceleration = Accelerometer.current.acceleration.ReadValue();

        // Hvis vi ryster telefonen og der er gået mere tid end ventetiden OG shakeCount er mindre end den endelige mængde af ryst inden 
        if (shakeCount < shakeFinish && Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
            && Time.unscaledTime >= timeSinceLastShake + minShakeInterval)
        {
            timeSinceLastShake = Time.unscaledTime;
            shakeCount++;
            //Debug.Log("Shake" + Input.acceleration.sqrMagnitude);
            StartCoroutine(ShakeBag());
        }
        //Når vi er oppe på den endelige mængde af ryst, resetter vi shakeCount og sætter en ny shakeFinish værdi
        else if (shakeCount == shakeFinish)
        {
            //Vibration.Cancel();
            Vibration.Vibrate(10);
            Debug.Log("Tillykke kammerat, her er dit event:");
            SoundManager.Instance.StopSound();
            SoundManager.Instance.PlaySound(finishSound);
            GameManager.Instance.LoadNextScene();

        }
        if (shaking)
        {
            //Når vi er igang med at ryste, vibrerer telefonen og posen på skærmen går op og ned via en lerp
            //Handheld.Vibrate();
            Vibration.Vibrate(1000);
            Vector3 newPos = new Vector3(0,Input.acceleration.y * shakeWeightPercentile,0);
            StartCoroutine(LerpPosition(newPos, waitForSeconds));
            //Debug.Log(Input.acceleration.y * shakeWeightPercentile);

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
        SoundManager.Instance.PlaySound(shakeSound);
        yield return new WaitForSeconds(waitForSeconds);

        shaking = false;
        transform.position = originalPos;

    }

    //Hjælper på at gøre rystebevægelsen mindre hakket ved at lerpe bevægelsen til target postion og tilbage til start positionen
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration/2)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        while (time > duration /2 && time < duration)
        {
            transform.position = Vector3.Lerp(targetPosition, startPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = startPosition;
    }



}
