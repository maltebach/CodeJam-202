using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickTest : MonoBehaviour
{
    public bool click;
    public GameObject UIHider;
    public AudioClip buttonSound;

    private SetChildrenToActive setChildrenToActive;


    public void Start()
    {
        setChildrenToActive = UIHider.GetComponent<SetChildrenToActive>();
    }

    public void ClickToLoadScene()
    {
        SoundManager.Instance.PlaySound(buttonSound);        
        GameManager.Instance.LoadNextScene();
        setChildrenToActive.TurnOffUI();
    }

}
