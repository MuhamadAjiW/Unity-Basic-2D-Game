using UnityEngine;
using static ConfigurationManager;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    protected bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (ConfigurationManager.Instance != null && ConfigurationManager.Instance.controlsConfig != null)
            {
                Debug.Log("Player entered range of " + gameObject.name + ". Press " + ConfigurationManager.Instance.controlsConfig.Interact.ToString() + " to interact.");
            }
            else
            {
                Debug.LogWarning("ControlsConfig not loaded in ConfigurationManager. Cannot display interact key.");
            }
            // Optionally, show a UI prompt here
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left range of " + gameObject.name);
            // Optionally, hide the UI prompt here
        }
    }

    protected virtual void Update()
    {
        if (playerInRange && ConfigurationManager.Instance != null && ConfigurationManager.Instance.controlsConfig != null && Input.GetKeyDown(ConfigurationManager.Instance.controlsConfig.Interact))
        {
            Interact();
        }
    }

    public abstract void Interact();
}