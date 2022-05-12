using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestionType
{
    Likert = 0,
    Swipe = 1,

}

[System.Serializable]
public class MoroQuestion
{
    public string title = "TODO: Set title";
    public QuestionType questionType = QuestionType.Likert;
    public FFMData ffm;
}

public class MoroQuestionManager : MonoBehaviour
{
    public static MoroQuestionManager instance;

    public List<MoroQuestion> moroQuestions = new List<MoroQuestion>();

    public List<MoroQuestion> answeredQuestions = new List<MoroQuestion>();

    [Header("References")]

    public GameObject eventPrefab;

    Transform parentTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public MoroElementHandler GetQuestion(int i, Vector3 cursor)
    {
        MoroElementHandler element = Instantiate(eventPrefab, cursor, Quaternion.identity).GetComponent<MoroElementHandler>();
        element.referenceIndex = i;
        element.transform.SetParent(parentTransform, false);
        return element;
    }

    public void AnswerQuestion(int i)
    {
        MoroQuestion question;
        question = moroQuestions[i];
        answeredQuestions.Add(question);
        moroQuestions.Remove(question);
        //TestManager.instance.Evaluate(question.ffm.openness, question.ffm.conscientiousness, question.ffm.extraversion, question.ffm.agreeableness, question.ffm.neuroticism);
    }

    public void ResetQuestions()
    {
        foreach (MoroQuestion question in answeredQuestions)
        {
            moroQuestions.Add(question);
            answeredQuestions.Remove(question);
        }
    }

    public void SetParentTransform(Transform pt)
    {
        parentTransform = pt;
    }
}