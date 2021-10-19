using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    // TODO add the canvas here
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
            // GameObject.Find("Camera").GetComponent<CameraFollow>().target = player.transform;
            // print(GameObject.Find("Camera").GetComponent<CameraFollow>().target);
        }
    }
}
