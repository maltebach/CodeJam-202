using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a class that just hold FFM relevant values.
/// </summary>
[System.Serializable]
public class FFMData
{
    public float openness;
    public float conscientiousness;
    public float extraversion;
    public float agreeableness;
    public float neuroticism;
}


public class TestManager : MonoBehaviour
{
    public static TestManager instance;

    public float openness;

    public float conscientiousness;

    public float extraversion;

    public float agreeableness;

    public float neuroticism;

    public float standardDeviation;

    public float standardDeviationSquaredPI;

    public float mean = 50f;

    private float adjustmentFactor;

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

    private void Start()
    {
        standardDeviationSquaredPI = standardDeviation * Mathf.Sqrt(2 * Mathf.PI);
    }


    public int GetFilteredIndex()
    {
        int i = -1;

        //placeholder
        i = Random.Range(0, MoroEventManager.instance.moroEvents.Count);

        return i;
    }

    public void Evaluate(FFMData ffm, int likert)
    {

        Mathf.Clamp(likert, 1, 7);

        if (ffm.openness != 0)
        {
            switch (likert)
            {
                case 1:
                    ffm.openness *= (-1);

                    break;

                case 2:
                    ffm.openness *= (-0.66f);

                    break;

                case 3:
                    ffm.openness *= (-0.33f);

                    break;
                case 4:

                    return;

                case 5:
                    ffm.openness *= (0.33f);

                    break;
                case 6:
                    ffm.openness *= (0.66f);

                    break;
                case 7:
                    ffm.openness *= (1);

                    break;
                default:
                    break;
            }
        }

        if (ffm.conscientiousness != 0)
        {
            switch (likert)
            {
                case 1:
                    ffm.conscientiousness *= (-1);

                    break;

                case 2:
                    ffm.conscientiousness *= (-0.66f);

                    break;

                case 3:
                    ffm.conscientiousness *= (-0.33f);

                    break;
                case 4:

                    return;

                case 5:
                    ffm.conscientiousness *= (0.33f);

                    break;
                case 6:
                    ffm.conscientiousness *= (0.66f);

                    break;
                case 7:
                    ffm.conscientiousness *= (1);

                    break;
                default:
                    break;
            }
        }

        if (ffm.extraversion != 0)
        {
            switch (likert)
            {
                case 1:
                    ffm.extraversion *= (-1);

                    break;

                case 2:
                    ffm.extraversion *= (-0.66f);

                    break;

                case 3:
                    ffm.extraversion *= (-0.33f);

                    break;
                case 4:

                    return;

                case 5:
                    ffm.extraversion *= (0.33f);

                    break;
                case 6:
                    ffm.extraversion *= (0.66f);

                    break;
                case 7:
                    ffm.extraversion *= (1);

                    break;
                default:
                    break;
            }
        }

        if (ffm.agreeableness != 0)
        {
            switch (likert)
            {
                case 1:
                    ffm.agreeableness *= (-1);

                    break;

                case 2:
                    ffm.agreeableness *= (-0.66f);

                    break;

                case 3:
                    ffm.agreeableness *= (-0.33f);

                    break;
                case 4:

                    return;

                case 5:
                    ffm.agreeableness *= (0.33f);

                    break;
                case 6:
                    ffm.agreeableness *= (0.66f);

                    break;
                case 7:
                    ffm.agreeableness *= (1);

                    break;
                default:
                    break;
            }
        }

        if (ffm.neuroticism != 0)
        {
            switch (likert)
            {
                case 1:
                    ffm.neuroticism *= (-1);

                    break;

                case 2:
                    ffm.neuroticism *= (-0.66f);

                    break;

                case 3:
                    ffm.neuroticism *= (-0.33f);

                    break;
                case 4:

                    return;

                case 5:
                    ffm.neuroticism *= (0.33f);

                    break;
                case 6:
                    ffm.neuroticism *= (0.66f);

                    break;
                case 7:
                    ffm.neuroticism *= (1);

                    break;
                default:
                    break;
            }
        }

        //List<float> ffmTraits = new List<float>();
        //ffmTraits.Add(ffm.openness);
        //ffmTraits.Add(ffm.conscientiousness);
        //ffmTraits.Add(ffm.extraversion);
        //ffmTraits.Add(ffm.agreeableness);
        //ffmTraits.Add(ffm.neuroticism);

        //foreach (float trait in ffmTraits)
        //{
        //    if (trait != 0)
        //    {
        //        float adjustmentFactor = GaussianCalculation(trait);

        //        return trait *= adjustmentFactor;


        //    }




        //}

        if (ffm.openness != 0)
        {
            adjustmentFactor = GaussianCalculation(ffm.openness);

            ffm.openness *= adjustmentFactor;

            openness *= ffm.openness;

            Debug.Log(openness + adjustmentFactor);
        }
        if (ffm.conscientiousness != 0)
        {
            adjustmentFactor = GaussianCalculation(ffm.conscientiousness);

            ffm.conscientiousness *= adjustmentFactor;

            conscientiousness *= ffm.conscientiousness;

            Debug.Log(conscientiousness + adjustmentFactor);
        }
        if (ffm.extraversion != 0)
        {
            adjustmentFactor = GaussianCalculation(ffm.extraversion);

            ffm.extraversion *= adjustmentFactor;

            extraversion *= ffm.extraversion;

            Debug.Log(extraversion + adjustmentFactor);
        }
        if (ffm.agreeableness != 0)
        {
            adjustmentFactor = GaussianCalculation(ffm.agreeableness);

            ffm.agreeableness *= adjustmentFactor;

            agreeableness *= ffm.agreeableness;

            Debug.Log(agreeableness + adjustmentFactor);
        }
        if (ffm.neuroticism != 0)
        {
            adjustmentFactor = GaussianCalculation(ffm.neuroticism);

            ffm.neuroticism *= adjustmentFactor;

            neuroticism *= ffm.neuroticism;

            Debug.Log(neuroticism + adjustmentFactor);
        } 
    }


    public float GaussianCalculation(float ffmTrait)
    {
        
        float traitSubtractedByMean = ffmTrait - mean;
        float ffmAdjustmentMetric = (1 / standardDeviationSquaredPI) * Mathf.Exp(-((Mathf.Pow(traitSubtractedByMean,2f)) / 2 * Mathf.Pow(standardDeviation,2f)));

        return ffmAdjustmentMetric;
    }


}
