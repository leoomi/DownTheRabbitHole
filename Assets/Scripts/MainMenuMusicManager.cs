using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource introSource;
    [SerializeField]
    private AudioSource loopSource;
    // Start is called before the first frame update
    void Start()
    {
        loopSource.PlayDelayed(introSource.clip.length);
    }
}
