using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    [System.Serializable]
    public struct UserSettings
    {

        public int framerate;
        [Range(0,2)]
        public int vsync;
        public Resolution res;
        [Range(0, 1)]
        public float volumeMusic;
        [Range(0, 1)]
        public float volumeSFX;
        [Range(0, 4)]
        public int qualityLevel;
        public bool fullscreen;
        public AudioSpeakerMode audioMode;

        public UserSettings(int fr, int vs, Resolution r, float vm, float vsy, int ql, bool fs, AudioSpeakerMode asm)
        {
            this.framerate = fr;
            this.vsync = vs;
            this.res = r;
            this.volumeMusic = (1 > vm ? 1 : vm < 0 ? 0 : vm);
            this.volumeSFX = (1 > vsy ? 1 : vsy < 0 ? 0 : vsy);
            this.qualityLevel = ql;
            this.fullscreen = fs;
            this.audioMode = asm;
        }

    }

    protected void loadJSON(UserSettings userSettings)
    {
        userSettings.framerate = 0;
        userSettings.vsync = 0;
    }

    protected void applySettings(UserSettings userSettings)
    {
        Application.targetFrameRate = userSettings.framerate;
        QualitySettings.vSyncCount = userSettings.vsync;
        Screen.SetResolution(userSettings.res.width, userSettings.res.height, userSettings.fullscreen);
        QualitySettings.SetQualityLevel(userSettings.qualityLevel);

        //figure out volume
        AudioSettings.speakerMode = userSettings.audioMode;


    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        UserSettings userSettings = new UserSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
