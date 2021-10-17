using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerController.isSpawned)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
    }
}
