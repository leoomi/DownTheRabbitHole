using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;
using UnityEngine.Audio;
using System.Linq;

public class Settings : MonoBehaviour
{

    [SerializeField]
    UserSettings userSettings;

    #region privates
    private bool cooldown = false;
    #endregion

    #region UI
    // main UI
    [SerializeField]
    Canvas canvas;
    //[SerializeField]
    //GameObject menuGO;
    [SerializeField]
    GameObject optionsGO;
    [SerializeField]
    GameObject audioSettingsGO;
    [SerializeField]
    GameObject qualitySettingsGO;
    // sliders
    [SerializeField]
    Slider volumeSFXSlider;
    [SerializeField]
    Slider volumeMusicSlider;
    [SerializeField]
    Slider FPSSlider;
    // buttons
    [SerializeField]
    Button FullscreenBtn; // TODO
    [SerializeField]
    Button ApplySettingsBtn; // apply ALSO brings you back cause im lazy
    [SerializeField]
    Button AudioBtn;
    [SerializeField]
    Button QualityBtn;
    [SerializeField]
    Button ResumeBtn;
    // dropdowns
    [SerializeField]
    Dropdown ResolutionDd;
    [SerializeField]
    Dropdown QualityDd;
    #endregion

    #region Audio
    [SerializeField]
    AudioMixer SFXMixer;
    [SerializeField]
    AudioMixer MusicMixer;
    #endregion

    // Should only be initialised once on seperate gameobject
    // UI should only be initialised once on same gameobject as the settings.
    // TODO key mapping?
    [System.Serializable]
    public struct UserSettings
    {

        [Range(30, 300)]
        public int framerate;
        [Range(0, 2)]
        public int vsync;
        public Resolution res;
        [Range(0.0001f, 1f)]
        public float volumeMusic;
        [Range(0.0001f, 1f)]
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
            volumeMusic = (1 > vm ? 1 : vm < 0 ? 0.0001f : vm);
            volumeSFX = (1 > vsy ? 1 : vsy < 0 ? 0.0001f : vsy);
            qualityLevel = ql;
            fullscreen = fs;
            audioMode = asm;
        }

    }

    /*IEnumerator Cooldown(float seconds=0.1f)
    {
        cooldown = true;
        yield return new WaitForSeconds(seconds);
        cooldown = false;
    }*/

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

        this.userSettings = userSettings;

    }

    protected void loadQualitySettingsOptions()
    {
        // QualitySettings
        string[] qo = QualitySettings.names;
        QualityDd.options = new List<Dropdown.OptionData>();
        foreach (string opt in qo)
        {
            QualityDd.options.Add(new Dropdown.OptionData(opt));
        }

        // Resolution
        Resolution[] ro = Screen.resolutions;
        ResolutionDd.options = new List<Dropdown.OptionData>();
        foreach (Resolution res in ro)
        {
            ResolutionDd.options.Add(new Dropdown.OptionData($"{res.width}x{res.height} {res.refreshRate}hz"));
        }
    }

    // TODO make all the sliders show their values on initialisation (done!) - Red
    public void getQualitySettingsFromUI()
    {
        // gets the menu's setting opt
        userSettings.framerate = (int)FPSSlider.value;
        userSettings.vsync = 0;
        userSettings.res = Screen.resolutions[ResolutionDd.value];
        userSettings.qualityLevel = QualityDd.value;
    }

    public void qualitySettingsApply()
    {
        // need to be able to set fullscreen mode
        // need to be able to set the display used

        // everything thats not audio settings
        Application.targetFrameRate = userSettings.framerate;
        QualitySettings.vSyncCount = userSettings.vsync;
        Screen.SetResolution(userSettings.res.width, userSettings.res.height, FullScreenMode.Windowed);//userSettings.fullscreen);
        QualitySettings.SetQualityLevel(userSettings.qualityLevel);

        // TODO dump to json?
        // do in different method that gets called on button press

        // then we go back
    }

    public void audioSettingsApply()
    {
        // audio settings should get their own page?
        float volumeSFX = volumeSFXSlider.value;
        float volumeMusic = volumeMusicSlider.value;
        print(SFXMixer);
        SFXMixer.SetFloat("musicVol", Mathf.Log10(volumeSFX) * 20);
        MusicMixer.SetFloat("musicVol", Mathf.Log10(volumeMusic) * 20);
        // AudioSettings.speakerMode = userSettings.audioMode;

        // TODO dump to json?
        // do in different method that gets called on button press

        // then we go back
    }

    // TODO add all the quality options and resolutions into the drop-down

    public void exitGame() { Application.Quit(); }

    public void qualityButtonMenuActivate()
    {
        //menuGO.SetActive(false);
        optionsGO.SetActive(false);
        audioSettingsGO.SetActive(false);
        qualitySettingsGO.SetActive(true);
    }

    public void audioButtonMenuActivate()
    {
        //menuGO.SetActive(false);
        optionsGO.SetActive(false);
        audioSettingsGO.SetActive(true);
        qualitySettingsGO.SetActive(false);
    }

    public void backButtonActivate()
    {
        //menuGO.SetActive(false);
        optionsGO.SetActive(true);
        audioSettingsGO.SetActive(false);
        qualitySettingsGO.SetActive(false);
    }

    public void resumeGame()
    {
        //menuGO.SetActive(false);
        optionsGO.SetActive(false);
        audioSettingsGO.SetActive(false);
        qualitySettingsGO.SetActive(false);
        canvas.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //AudioMixer MusicMixerGroup = Resources.Load<AudioMixer>("Resources/Music");
        //AudioMixer SFXMixerGroup = Resources.Load<AudioMixer>("Resources/SFX");
        loadQualitySettingsOptions();
        DontDestroyOnLoad(this);
        //DontDestroyOnLoad(this.gameObject);
        //DontDestroyOnLoad(canvas);
        // loadJSON();
        qualitySettingsApply();
        audioSettingsApply();

        // HAHA UNITY FUCK YOU THIS TOOK WAYYYYY TOO LONG TO FIX
        // IMAGINE NOT BEING ABLE TO PROPERLY PLAY TEST YOUR OWN GAME
        foreach (Display display in Display.displays) {
            print(display.active);
        }
        print(Display.displays.Length);
        Display.displays[0].Activate();
    }

    // Update is called once per frame
    void Update()
    {
        // need to check button press for ESC
        // TODO freeze movement? 
        if (Input.GetKeyDown(KeyCode.Escape)) // this works but Input.GetButtonDown("Menu") doesnt? Why, Unity?
        {
            optionsGO.SetActive(!optionsGO.gameObject.activeSelf);
            audioSettingsGO.SetActive(false);
            qualitySettingsGO.SetActive(false);
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
            //Time.timeScale = 1; // pauses game
            //StartCoroutine(Cooldown());
        }
    }
}