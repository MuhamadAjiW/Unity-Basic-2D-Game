using UnityEngine;

public class FollowObject : MonoBehaviour {
    public Transform objectToFollow;

    void Update(){
        this.transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, this.transform.position.z);
    }
}