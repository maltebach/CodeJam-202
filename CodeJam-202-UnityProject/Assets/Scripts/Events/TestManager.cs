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


public class TestManager : MonoBehaviour, IDataPersistence
{
    public static TestManager instance;

    public float openness;

    public float conscientiousness;

    public float extraversion;

    public float agreeableness;

    public float neuroticism;

    public float likertStrengthFactor = 5;

    public float curveAdjustment;

    public float peakAdjustment = 1.2f;

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

    public void LoadData(AppData data)
    {
        this.openness = data.openness;
        this.conscientiousness = data.conscientiousness;
        this.extraversion = data.extraversion;
        this.agreeableness = data.agreeableness;
        this.neuroticism = data.neuroticism;
    }

    public void SaveData(ref AppData data)
    {
        data.openness = this.openness;
        data.conscientiousness = this.conscientiousness;
        data.extraversion = this.extraversion;
        data.agreeableness = this.agreeableness;
        data.neuroticism = this.neuroticism;
    }

    public int GetFilteredIndex()
    {
        int i = -1;

        //placeholder
        i = Random.Range(0, MoroEventManager.instance.moroEvents.Count);

        return i;
    }

    /// <summary>
    /// This function evaluates how much your personality trait score gets adjusted when answering a Likert question.
    /// </summary>
    /// <param name="ffm"></param>
    /// <param name="likert"></param>

    public void Evaluate(FFMData ffm, int likert)
    {
        // To avoid weird input errors, we clamp it to the scale values
        Mathf.Clamp(likert, 1, 7);

        //If likert is 4, we can compeltely ignore the result as a likert of 4 represents a neutral answer to a likert question.
        //If we don't do this, we would multiply the traits by 0 in the following switch statements.
        if(likert == 4)
        {
            return;
        }

        // Adjust ffm values by our strength factor. This determines how big an effect each question has on the final user test scores. This assumes all ffm scores are assigned as a number between -1 and 1.
        ffm.openness = ffm.openness * likertStrengthFactor;
        ffm.conscientiousness = ffm.conscientiousness * likertStrengthFactor;
        ffm.extraversion = ffm.extraversion * likertStrengthFactor;
        ffm.agreeableness = ffm.agreeableness * likertStrengthFactor;
        ffm.neuroticism = ffm.neuroticism * likertStrengthFactor;

        // For every trait, we check if it has a correlation, and if it has
        // we run a switch case, adjusting the correlation coeffienct depeding on the Likert scale answer
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

        // we again check if there is a correlation with every personality trait, and for the once with a correlation
        // we us a gaussian function to adjust the factor further, depending on how close to the center (50) the persons personality score is
        if (ffm.openness != 0)
        {
            adjustmentFactor = GaussianCalculation(openness);

            ffm.openness *= adjustmentFactor;

            openness += ffm.openness;

            openness = Mathf.Clamp(openness, 0, 100);
        }
        if (ffm.conscientiousness != 0)
        {
            adjustmentFactor = GaussianCalculation(conscientiousness);

            ffm.conscientiousness *= adjustmentFactor;

            conscientiousness += ffm.conscientiousness;

            conscientiousness = Mathf.Clamp(conscientiousness, 0, 100);
        }
        if (ffm.extraversion != 0)
        {
            adjustmentFactor = GaussianCalculation(extraversion);

            ffm.extraversion *= adjustmentFactor;

            extraversion += ffm.extraversion;

            extraversion = Mathf.Clamp(extraversion, 0, 100);
        }
        if (ffm.agreeableness != 0)
        {
            adjustmentFactor = GaussianCalculation(agreeableness);

            ffm.agreeableness *= adjustmentFactor;

            agreeableness += ffm.agreeableness;

            agreeableness = Mathf.Clamp(agreeableness, 0, 100);
        }
        if (ffm.neuroticism != 0)
        {
            adjustmentFactor = GaussianCalculation(neuroticism);

            ffm.neuroticism *= adjustmentFactor;

            neuroticism += ffm.neuroticism;

            neuroticism = Mathf.Clamp(neuroticism, 0, 100);
        } 
    }

    /// <summary>
    /// Using a gaussian function, this method adjust the correlation coefficient of a ffmTrait
    /// </summary>
    /// <param name="ffmTrait"></param>
    /// <returns></returns>
    private float GaussianCalculation(float ffmTrait)
    {
        
        float traitSubtractedByMean = ffmTrait - mean;
        float ffmAdjustmentMetric = peakAdjustment * Mathf.Exp(-((Mathf.Pow(traitSubtractedByMean,2f)) / (2 * Mathf.Pow(curveAdjustment,2f))));
        //Debug.Log(ffmAdjustmentMetric);
        return ffmAdjustmentMetric;
    }


}
