using UnityEngine;

public class StoryTrigger : InteractableObject
{
    [SerializeField] private string storyEventPath; // e.g., "chapter1.EventA"
    [SerializeField] private bool triggerOnce = true;

    private bool hasTriggered = false;

    public override void Interact()
    {
        if (triggerOnce && hasTriggered)
        {
            Debug.Log("Story Trigger '" + storyEventPath + "' has already been activated.");
            return;
        }

        if (StoryManager.Instance != null)
        {
            StoryManager.Instance.SetEventState(storyEventPath, true);
            Debug.Log($"Story Event '{storyEventPath}' triggered to TRUE.");
            hasTriggered = true;
        }
        else
        {
            Debug.LogWarning("StoryManager instance not found. Cannot trigger story event.");
        }
    }
}