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

    // TODO make this a list of objects each
    // TODO tie in w audio
    #region Dialogue
    [SerializeField]
    List<DialogueText> tutorialDialogue;
    [SerializeField]
    List<DialogueText> fallDialogue;
    #endregion

    [Serializable]
    private struct DialogueText
    {
        [SerializeField]
        public string dialogue;
        [SerializeField]
        public AudioClip dialogueAudio;
        [SerializeField]
        public float timeToRead;
    }
    /*
    public void PlayDialogue(int i, bool fail=true)
    {
        AudioSource.Play();
    }*/

    public void PlayDialogue(string s, bool fail = true)
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // mouse click or space to further dialogue
    }
}
