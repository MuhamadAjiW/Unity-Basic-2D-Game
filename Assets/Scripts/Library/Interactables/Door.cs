using UnityEngine;

public class Door : InteractableObject
{
    [SerializeField] private bool isOpen = false;
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closedPosition;
    [SerializeField] private float moveSpeed = 2f;

    private void Start()
    {
        // Set initial position based on isOpen
        transform.position = isOpen ? openPosition : closedPosition;
    }

    public override void Interact()
    {
        isOpen = !isOpen;
        Debug.Log("Door interacted! Is now " + (isOpen ? "open" : "closed"));
        StopAllCoroutines(); // Stop any ongoing movement
        StartCoroutine(MoveDoor());
    }

    private System.Collections.IEnumerator MoveDoor()
    {
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
    }
}