using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickTest : MonoBehaviour
{
    public bool click;

    public GameObject UIHider;

    private SetChildrenToActive setChildrenToActive;

    public void Start()
    {
        setChildrenToActive = UIHider.GetComponent<SetChildrenToActive>();
    }

    public void ClickToLoadScene()
    {
        Debug.Log("click");
        setChildrenToActive.TurnOffUI();
        GameManager.Instance.LoadNextScene();
    }

}
