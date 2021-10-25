using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> audioClips;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Animator animator;

    private int currentIndex = 0;

    public void Start()
    {
        // TODO Pause background music
    }

    public void PlayClipAndSetNext()
    {
        if (currentIndex >= audioClips.Count)
        {
            return;
        }

        audioSource.clip = audioClips[currentIndex];
        audioSource.Play();
        currentIndex += 1;

        if (currentIndex == audioClips.Count)
        {
            RollCredits();
        }
    }

    public void RollCredits()
    {
        animator.SetTrigger("Roll");
    }
}
