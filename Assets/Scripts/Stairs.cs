using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    [SerializeField]
    private bool goesUp = true;

    private bool isActive = false;

    void Start()
    {
        StartCoroutine(ActivationCoroutine());
    }

    private IEnumerator ActivationCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Workaround to avoid player to change the scene when falling/going up to a stairs object
        if (!isActive)
        {
            return;
        }

        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        var destinationIndex = currentIndex + (goesUp ? 1 : - 1);
        Debug.Log(currentIndex);

        SceneManager.LoadScene(destinationIndex);
    }
}
