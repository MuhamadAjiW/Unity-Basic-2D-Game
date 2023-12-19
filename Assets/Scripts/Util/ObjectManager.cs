
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public static class ObjectManager{
    public static GameObject Generate(
        string prefabPath,
        Transform parent = null,
        Vector2? position = null,
        Vector2? scale = null,
        Quaternion? rotation = null,
        int sortingOrder = 0,
        string name = "attackObject")
    {
        GameObject prefabObject = Resources.Load<GameObject>(prefabPath);

        if(prefabObject == null) Debug.LogError("Prefab not found: " + prefabPath);
        
        GameObject attackObject = parent == null? GameObject.Instantiate(prefabObject) : GameObject.Instantiate(prefabObject, parent);
        if(position != null) attackObject.transform.position = position.Value;
        if(rotation != null) attackObject.transform.rotation = rotation.Value;
        if(scale != null) attackObject.transform.localScale = scale.Value;
        
        Renderer renderer = attackObject.GetComponent<Renderer>();
        if (renderer != null) renderer.sortingOrder = sortingOrder;
        
        attackObject.name = name;

        return attackObject;
    }

    public static void Destroy(GameObject gameObject, float delay = 0){
        if(gameObject != null){
            GameController.instance.StartCoroutine(DestroyWithDelay(gameObject, delay));
        } else{
            Debug.LogError("Game Object is null");
        }
    }

    private static IEnumerator DestroyWithDelay(GameObject gameObject, float delay){
        yield return new WaitForSeconds(delay);
        GameObject.Destroy(gameObject);
    }
}