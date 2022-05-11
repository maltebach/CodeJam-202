using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class LikertScaleList
{
    public LikertScale [] Likert;
}

public class CSVlikert : MonoBehaviour
{
    public TextAsset textAssetData;

    public List<MoroQuestion> likertScales = new List<MoroQuestion>();
    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int collumns = 7; //Det ville være fedt hvis programmet selv kunne finde ud af hvor mange collumns den skal springe over, så den ville være dynamisk istedet for statisk.
        int tableSize = (data.Length / collumns) - 1;


        for (int i = 0; i < tableSize; i++)
        {
            MoroQuestion moro = new MoroQuestion();
            FFMData ffm = new FFMData();
            moro.ffm = ffm;
            int j = 0;

            moro.title = (data[(i + 1) * collumns + j]);
            j++;

            moro.ffm.openness = float.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.ffm.conscientiousness = float.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.ffm.extraversion = float.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.ffm.agreeableness = float.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.ffm.neuroticism = float.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.questionType = (QuestionType)int.Parse(data[(i + 1) * collumns + j]);

            likertScales.Add(moro);

        }
    }
}