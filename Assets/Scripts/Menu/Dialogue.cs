using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    #region UI
    [SerializeField]
    Canvas dialogueUI;
    [SerializeField]
    Image dialogueBox;
    [SerializeField]
    Text dialogueText;
    #endregion

    #region Dialogue
    [SerializeField]
    List<String> tutorialDialogue;
    [SerializeField]
    List<String> fallDialogue;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
