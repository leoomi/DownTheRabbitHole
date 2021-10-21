using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    // TODO add the canvas here
    //[SerializeField]
    //private GameObject levelFloor;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject menuPrefab;

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
            changeCamTarget(player);
        }
    }

    public void changeCamTarget(GameObject player)
    {
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        GameObject camController = GameObject.FindGameObjectWithTag("GameController");
        if (camController.GetComponent<CameraFollow>() is null) camController.AddComponent<CameraFollow>();
        camController.GetComponent<CameraFollow>().target = player.transform;
        camController.GetComponent<CameraFollow>().cam = cam;
        camController.GetComponent<CameraFollow>().height = -5f;
        // cam.GetComponent<CameraFollow>().gameFloor = levelFloor;
        //camController.GetComponent<CameraFollow>().target = player.transform;
        camController.GetComponent<CameraFollow>().setCameraTarget();
        // print(GameObject.Find("Camera").GetComponent<CameraFollow>().target);
    }

    private void OnLevelWasLoaded(int level)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        changeCamTarget(player);
    }

}
