using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionBuilder : MoroElement
{
    public TMP_Text titleText;    //Public reference to TMP text field.

    public QuestionType questionType; //Currently unused. But may be used to differentiate between different types of questions. (IE. Likert and swipe)

    MoroQuestion question; //Private reference to a specific question. Used to keep code cleaner.
    int questionIndex; //Internal reference to the index of the specific question this object represents to.


    public override void BuildElement(int index)
    {
        question = MoroQuestionManager.instance.moroQuestions[index]; //Gets the specific question that the index refers to.
        questionType = question.questionType; //Currently unused.
        questionIndex = index; //Will be used later for evaluating the question.

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
}
