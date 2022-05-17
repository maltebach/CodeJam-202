using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//https://www.youtube.com/watch?v=9gdBzbqnvPk
public class SwipeEffect : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //Used to evaluate this swipe card, added by Pil
    public FFMData ffmLeft;
    public FFMData ffmRight;

    public bool finalCard = false; //bool used later for checking if the final card is swiped


    private Vector3 _initialPosition;
    private float _distanceMoved;
    private bool _swipeLeft;
    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition = new Vector2(transform.localPosition.x + eventData.delta.x, transform.localPosition.y); //changes x of the card value when swiping

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _initialPosition = transform.localPosition; //stores the value for the original position of the swipe card
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _distanceMoved = Mathf.Abs(transform.localPosition.x - _initialPosition.x); //how far the card has been swiped
        if (_distanceMoved < 0.2 * Screen.width)//checks if the the card has been swiped more than 0.4 of the screen size
        {
            transform.localPosition = _initialPosition; //if not, resets the card position
        }
        else
        {
            if (transform.localPosition.x > _initialPosition.x)//checks if the card is being swiped left
            {
                _swipeLeft = true;
            }
            else //or right
            {
                _swipeLeft = false;
            }

            StartCoroutine(MovedCard());
        }
    }

    private IEnumerator MovedCard() //animates the card moving out of the screen and dissapearing
    {
        float time = 0;
        while(GetComponent<Image>().color != new Color(1, 1, 1, 0))
        {
            time += Time.deltaTime; 
            if (_swipeLeft)
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x, transform.localPosition.x + Screen.width, 1 * time), transform.localPosition.y, 0);
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x, transform.localPosition.x - Screen.width, 1 * time), transform.localPosition.y, 0);
            }
            GetComponent<Image>().color = new Color(1, 1, 1, Mathf.SmoothStep(1, 0, 4*time));
            yield return null;
        }
        if(finalCard)
        {
            QuestionManager.instance.NextPage();
        }

    
        Destroy(gameObject);
    }



    public FFMData GetFFM()
    {
        if(_swipeLeft)
        {
            return ffmLeft;
        }
        else
        {
            return ffmRight;
        }
    }
}
