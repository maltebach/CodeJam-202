using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionBuilder : MoroElement
{
    public TMP_Text titleText;    //Public reference to TMP text field.

    public QuestionType questionType; //Currently unused. But may be used to differentiate between different types of questions. (IE. Likert and swipe)

    MoroQuestion question; //Private reference to a specific question. Used to keep code cleaner.
    //int questionIndex; //Internal reference to the index of the specific question this object represents to.

    public override void BuildElement()
    {
        questionType = question.questionType; //Currently unused.

        //Checks to see if the text field exists before trying to edit it.
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

    //Returns true of questionType is equal to QuestionType.Likert
    public override bool IsLikert()
    {
        if (questionType == QuestionType.Likert)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void AttachElement(MoroElementHandler handler)
    {
        question = handler.moroQuestion;
    }
}
