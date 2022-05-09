using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestionType
{
    Likert,
    Swipe,

}

[System.Serializable]
public class MoroQuestion
{
    public string title = "TODO: Set title";
    public QuestionType questionType = QuestionType.Likert;
    public float openness = 0;
    public float conscientiousness = 0;
    public float extraversion = 0;
    public float agreeableness = 0;
    public float neuroticism = 0;
}

public class MoroQuestionManager : MonoBehaviour
{
    public static MoroQuestionManager instance;

    public List<MoroQuestion> moroQuestions = new List<MoroQuestion>();

    public List<MoroQuestion> answeredQuestions = new List<MoroQuestion>();

    [Header("References")]

    public GameObject eventPrefab;

    public Transform parentTransform;

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
        TestManager.instance.Evaluate(question.openness, question.conscientiousness, question.extraversion, question.agreeableness, question.neuroticism);
    }

    public void ResetQuestions()
    {
        foreach (MoroQuestion question in answeredQuestions)
        {
            moroQuestions.Add(question);
            answeredQuestions.Remove(question);
        }
    }
}