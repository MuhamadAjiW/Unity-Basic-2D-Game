using UnityEngine;

public class DamagingObject{
    private GameObject gameObject;

    public DamagingObject(Transform parent, float damage, string name = "DamagingObject"){
        gameObject = new GameObject();
        gameObject.transform.parent = parent;
        gameObject.AddComponent<Collider2D>();
    }
}