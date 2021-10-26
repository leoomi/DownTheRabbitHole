using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler : MonoBehaviour
{

    public static AudioHandler instance = null;

    [SerializeField]
    private AudioMixer musicMixer;
    //[SerializeField]
    private AudioSource musicSource;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(this);
        musicSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(musicSource);
    }

    private static IEnumerator Fade(AudioMixer mixer, string param, float duration=1f, float targetVolume = 0.0f)
    {
        float ctime = 0;
        float cvol;

        mixer.GetFloat(param, out cvol);

        cvol = Mathf.Pow(10, cvol / 20);
        float tval = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (ctime < duration)
        {
            ctime += Time.deltaTime;
            float nvol = Mathf.Lerp(cvol, tval, ctime / duration);
            mixer.SetFloat(param, Mathf.Log10(nvol) * 20);
            yield return null;
        }

        yield break;
    }

    public void PlaySFX(AudioClip clip, Vector3 location)
    {
        AudioSource.PlayClipAtPoint(clip, location);
    }

    public void PlaySFX(AudioSource source)
    {
        source.Play();
    }

    /*public void PlayMusic(AudioClip clip, bool fadein=false)
    {
        //musicMixer.
        musicSource.clip = clip;
        StartCoroutine(Fade(musicMixer, "SFXVolume", targetVolume:(fadein ? 1 : 0)));
    }*/

}
