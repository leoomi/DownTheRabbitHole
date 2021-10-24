using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class GameButton : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private UnityEvent onButtonPressEvent;
    [SerializeField]
    private UnityEvent onButtonReleasedEvent;
    [SerializeField]
    private bool permanentPress = false;
    [SerializeField]
    private List<string> tags = new List<string> { "Player", "Box" };
    [SerializeField]
    private AudioClip buttonPressSFXU;
    [SerializeField]
    private AudioClip buttonPressSFXD;

    private bool pressed;
    private List<GameObject> pressers = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsValidPresser(other))
        {
            return;
        }

        pressers.Add(other.gameObject);
        if (pressers.Count > 1)
        {
            return;
        }

        if (onButtonPressEvent.GetPersistentEventCount() > 0)
        {
            onButtonPressEvent?.Invoke();
        }

        // TODO: Temporary visuals, replace with actual sprites
        sprite.color = Color.cyan;
        pressed = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (permanentPress)
        {
            return;
        }

        if (!IsValidPresser(other))
        {
            return;
        }

        pressers.Remove(other.gameObject);

        if (pressers.Count > 0)
        {
            return;
        }

        if (onButtonPressEvent.GetPersistentEventCount() > 0)
        {
             onButtonReleasedEvent?.Invoke();
        }
        // TODO: Temporary visuals, replace with actual sprites
        sprite.color = Color.red;
        pressed = false;
    }

    public bool isPressed() { return pressed; }

    private bool IsValidPresser(Collider2D other)
    {
        return tags.Any(t => t == other.tag);
    }

    public void toggleSFX()
    {
        GetComponent<AudioSource>().clip = (pressed is false ? buttonPressSFXD : buttonPressSFXU);
        GetComponent<AudioSource>().pitch = Random.Range(0.95f, 1.05f);
        AudioHandler.instance.PlaySFX(GetComponent<AudioSource>());
    }
}
