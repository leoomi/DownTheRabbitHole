using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MulitpleTriggersDoor : MonoBehaviour
{
    [SerializeField]
    private int triggersToActivate = 2;
    [SerializeField]
    private bool closeOnActivation = false;

    private int currentTriggers = 0;

    public void AddTrigger()
    {
        currentTriggers += 1;

        if (currentTriggers >= triggersToActivate)
        {
            Activate();
        }
    }

    public void RemoveTrigger()
    {
        currentTriggers -= 1;

        if (currentTriggers < triggersToActivate)
        {
            Deactivate();
        }
    }

    private void Activate()
    {
        if (closeOnActivation)
        {
            Close();
            return;
        }
        Open();
    }

    private void Deactivate()
    {
        if (closeOnActivation)
        {
            Open();
            return;
        }
        Close();
    }

    private void Open()
    {
        gameObject.SetActive(false);
    }

    private void Close()
    {
        gameObject.SetActive(true);
    }
}
