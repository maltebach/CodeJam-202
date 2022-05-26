using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This script evaluates any number of likert questions and swipes, as well as sets up the likert questions with the correct titles.
//This script is used to evaluate and set up likert and swipe questions outside of the feed.
public class LikertEval : MonoBehaviour
{
    //To evaluate likert questions we need to access the likert scale script to know what the user have selected for that question.
    public List<RealLikertScale> likerts = new List<RealLikertScale>();

    //And we also need to know the associated question. In this case we input specific and significant questions manually to ensure the user gets as unique a first set of suggestions as possible.
    public List<MoroQuestion> questions = new List<MoroQuestion>();

    //We also get a list of text objects, these are the titles of the likert questions.
    public List<TMP_Text> texts = new List<TMP_Text>();


    //Swipes is a bit more manual at the moment. They are baked into the scene.
    public List<SwipeEffect> swipes = new List<SwipeEffect>();

    //In order to evaluate a swipe card we need to assign it a "Strength" value 
    //(This is the same as the 1 to 7 range on likert questions. 1 means you get the opposite of the max results, 4 means this question has no bearing on your final score, and 7 means you get full marks. Everything in betweens are steps of equal size to bridge the gab between these 3 values)
    [Range(1,7)]
    public int swipeCardStrength = 7;

    //In start we call the setup method to put the correct titles for our likert questions in the scene.
    private void Start()
    {
        Setup();
    }

    //This method loops through all of our text objects and sets their text parameter to the correct title as defined in the moro question with the same list index.
    void Setup()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            texts[i].text = questions[i].title;
        }
    }

    //This method feeds all of the likert results to the test manager in order to evaluate their effect on your final test scores.
    public void Evaluate()
    {
        //This loop is for the likert questions.
        for (int i = 0; i < questions.Count; i++)
        {
            if(likerts[i].cursor != -1) //We do not wish to evaluate a question if the user has not chosen an option.
                TestManager.instance.Evaluate(questions[i].ffm, likerts[i].cursor); //This is where we need the list of likert scale objects, to access the cursor value (The users chosen option)
        }

        //This loop is for the swipe cards.
        for (int i = 0; i < swipes.Count; i++)
        {
            //Swipe effect script contains 2 FFMData objects, one for each direction you swipe. The script also contains a method to get the correct one based on what the user chose.
            TestManager.instance.Evaluate(swipes[i].GetFFM(), swipeCardStrength);
        }
    }
}
