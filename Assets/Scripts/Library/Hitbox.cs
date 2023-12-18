using UnityEngine;

public class Hitbox : MonoBehaviour{
    void OnTriggerEnter2D(){
        Debug.Log("Collision in hitbox of " + transform.parent.name);
    }
}