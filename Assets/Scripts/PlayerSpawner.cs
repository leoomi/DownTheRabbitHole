using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{

    // TODO add the canvas here
    //[SerializeField]
    //private GameObject levelFloor;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject menuPrefab;
    [SerializeField]
    LevelSettings[] levelSettings;
    
    [System.Serializable]
    public struct LevelSettings
    {
        [Header("Apply Camera Settings")]
        [SerializeField]
        public bool activateSettings;
        [Header("Camera Settings")]
        [SerializeField]
        public bool followPlayer;
        [SerializeField]
        public Vector3 fixedLocation;
        [SerializeField]
        public float height;
        [SerializeField]
        public float rbound, lbound, tbound, bbound;
        [SerializeField]
        public float scaleFactor;
        //[Header("Other settings")]
    }

    // Bug where you can sometimes spawn on this instead of the x, y, z when switching levels by going DOWN a level. - Red
    // This behaves fine when going up.
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance == null)
        {
            GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            GameObject menu = Instantiate(menuPrefab, Vector3.zero, Quaternion.identity);
            // TODO this isnt assigning
            //Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            changeCamTarget(player, SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void changeCamTarget(GameObject player, int level)
    {
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        GameObject camController = GameObject.FindGameObjectWithTag("GameController");
        if (camController.TryGetComponent(out CameraFollow _) is false) camController.AddComponent<CameraFollow>();
        CameraFollow camFollow = camController.GetComponent<CameraFollow>();
        camFollow.target = player.transform;
        camFollow.cam = cam;
        
        // because I need to make sure that the height is actually appropriate if no settings are found.
        camFollow.height = (levelSettings.Length -1 >= level ? (levelSettings[level].activateSettings ? -levelSettings[level].height : -5f) : -5f); 
        // each level can have camera settings applied here
        if (levelSettings.Length -1 >= level) {
            LevelSettings currentLevel = levelSettings[level];
            if (currentLevel.activateSettings)
            {
                
                camFollow.fixedLocation = currentLevel.fixedLocation;
                camFollow.rbound = currentLevel.rbound;
                camFollow.lbound = currentLevel.lbound;
                camFollow.tbound = currentLevel.tbound;
                camFollow.bbound = currentLevel.bbound;
                
                camFollow.toggleFollow = currentLevel.followPlayer;
            }
        }

        // cam.GetComponent<CameraFollow>().gameFloor = levelFloor;
        //camController.GetComponent<CameraFollow>().target = player.transform;
        camFollow.setCameraTarget();
        // print(GameObject.Find("Camera").GetComponent<CameraFollow>().target);
    }

    private void OnLevelWasLoaded(int level)
    {
        // toggle for each levl
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        changeCamTarget(player, level);
    }

}
