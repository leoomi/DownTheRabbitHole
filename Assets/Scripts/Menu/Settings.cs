using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{

    [SerializeField]
    UserSettings userSettings;
    [SerializeField]
    UnityEngine.UI.Slider volumeSFXSlider;
    [SerializeField]
    UnityEngine.UI.Slider volumeMusicSlider;
    AudioMixer SFXMixer;
    AudioMixer MusicMixer;
    GameObject master;

    // Should only be initialised once on seperate gameobject
    // UI should only be initialised once on same gameobject as the settings.
    [System.Serializable]
    public struct UserSettings
    {

        public int framerate;
        [Range(0,2)]
        public int vsync;
        public Resolution res;
        public float volumeMusic;
        public float volumeSFX;
        [Range(0, 4)]
        public int qualityLevel;
        public bool fullscreen;
        public AudioSpeakerMode audioMode;

        public UserSettings(int fr, int vs, Resolution r, float vm, float vsy, int ql, bool fs, AudioSpeakerMode asm)
        {
            framerate = fr;
            vsync = vs;
            res = r;
            volumeMusic = (1 > vm ? 1 : vm < 0 ? 0 : vm);
            volumeSFX = (1 > vsy ? 1 : vsy < 0 ? 0 : vsy);
            qualityLevel = ql;
            fullscreen = fs;
            audioMode = asm;
        }

    }

    // load me daddy
    protected void loadJSON()
    {
        // actually load json at some point.

        UserSettings userSettings = new UserSettings(
            120,
            0,
            Screen.resolutions[0],
            0.5f,
            0.7f,
            QualitySettings.GetQualityLevel(),
            false,
            AudioSpeakerMode.Stereo
        );
    }

    protected void applySettings()
    {
        // everything thats not audio settings
        Application.targetFrameRate = userSettings.framerate;
        QualitySettings.vSyncCount = userSettings.vsync;
        Screen.SetResolution(userSettings.res.width, userSettings.res.height, userSettings.fullscreen);
        QualitySettings.SetQualityLevel(userSettings.qualityLevel);
    }

    public void audioSettings()
    {
        // audio settings should get their own page?
        float volumeSFX = volumeSFXSlider.value;
        float volumeMusic = volumeMusicSlider.value;
        SFXMixer.SetFloat("SFX", volumeSFX);
        MusicMixer.SetFloat("MUSIC", volumeMusic);
        AudioSettings.speakerMode = userSettings.audioMode;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
