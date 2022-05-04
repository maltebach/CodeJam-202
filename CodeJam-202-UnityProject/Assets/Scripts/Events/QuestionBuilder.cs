using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionBuilder : MoroElement
{
    public TMP_Text titleText;
    public QuestionType questionType;

    MoroQuestion question;
    int questionIndex;
    public override void BuildElement(int index)
    {
        question = QuestionManager.instance.moroQuestions[index];
        questionType = question.questionType;
        questionIndex = index;

        if(titleText != null)
        {
            titleText.text = question.title;
        }

        switch (questionType)
        {
            case QuestionType.Likert:

                break;
            case QuestionType.Swipe:

                break;
            default:
                Debug.LogError("QuestionType not implemented!");
                break;
        }
    }
}
