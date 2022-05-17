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

    public List<MoroQuestion> unansweredQuestions = new List<MoroQuestion>();

    [Header("References")]

    public GameObject eventPrefab;

    Transform parentTransform;

    bool isDone = false;

    System.Random rnd = new System.Random();

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

    public MoroElementHandler GetQuestion(MoroQuestion moro, Vector3 cursor)
    {
        MoroElementHandler element = Instantiate(eventPrefab, cursor, Quaternion.identity).GetComponent<MoroElementHandler>();
        element.moroQuestion = moro;
        element.transform.SetParent(parentTransform, false);
        return element;
    }

    /*
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
    */

    public void SetParentTransform(Transform pt)
    {
        parentTransform = pt;
    }

    public MoroQuestion GetNextQuestion()
    {
        MoroQuestion moro;

        moro = unansweredQuestions[0];

        unansweredQuestions.RemoveAt(0);

        if (unansweredQuestions.Count == 0)
        {
            isDone = true;
        }

        return moro;
    }

    void SetupUnanswered()
    {
        foreach (MoroQuestion item in moroQuestions)
        {
            unansweredQuestions.Add(item);
        }

        unansweredQuestions.Shuffle(rnd);
    }

    private void Start()
    {
        SetupUnanswered();
    }

    public bool OutOfQuestions()
    {
        return isDone;
    }



}


