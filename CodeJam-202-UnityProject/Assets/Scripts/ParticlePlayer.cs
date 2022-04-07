using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public ParticleSystem shaker;
    public ShakeDetector shakeDetector;

    // Start is called before the first frame update
    void Start()
    {
        shakeDetector = GetComponent<ShakeDetector>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shakeDetector.shaking)
        {
            Instantiate(shaker, shaker.transform.position, shaker.transform.rotation);
            shaker.Play();
            shaker.Stop();
        }
    }
}
