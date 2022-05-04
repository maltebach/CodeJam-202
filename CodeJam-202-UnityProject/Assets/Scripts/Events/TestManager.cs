using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager instance;

    public float openness = 0;
    public float conscientiousness = 0;
    public float extraversion = 0;
    public float agreeableness = 0;
    public float neuroticism = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public int GetFilteredIndex()
    {
        int i = -1;

        //placeholder
        i = Random.Range(0, MoroEventManager.instance.moroEvents.Count);

        return i;
    }

    public void ChangeOpenness(float amount)
    {
        openness += amount;
    }

    public void ChangeConsientiousness(float amount)
    {
        conscientiousness += amount;
    }

    public void ChangeExtraversion(float amount)
    {
        extraversion += amount;
    }

    public void ChangeAgreeableness(float amount)
    {
        agreeableness += amount;
    }

    public void ChangeNeuroticism(float amount)
    {
        neuroticism += amount;
    }

    public void Evaluate(float o, float c, float e, float a, float n)
    {
        ChangeOpenness(o);
        ChangeConsientiousness(c);
        ChangeExtraversion(e);
        ChangeAgreeableness(a);
        ChangeNeuroticism(n);
    }
}
