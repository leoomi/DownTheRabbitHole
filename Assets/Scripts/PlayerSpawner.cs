using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    // Bug where you can sometimes spawn on this instead of the x, y, z when switching levels by going DOWN a level. - Red
    // This behaves fine when going up.
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerController.isSpawned)
        {
            GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            print(player);
            // this isnt assigning
            GameObject.Find("Camera").GetComponent<CameraFollow>().target = player.transform;
            print(GameObject.Find("Camera").GetComponent<CameraFollow>().target);
        }
    }
}
