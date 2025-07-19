
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static ConfigurationManager;

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
        
        GameObject gameObject = parent == null? GameObject.Instantiate(prefabObject) : GameObject.Instantiate(prefabObject, parent);
        if(position != null) gameObject.transform.position = position.Value;
        if(rotation != null) gameObject.transform.rotation = rotation.Value;
        if(scale != null) gameObject.transform.localScale = scale.Value;
        
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null) renderer.sortingOrder = sortingOrder;
        
        gameObject.name = name;

        return gameObject;
    }

    public static GameObject Generate(
        GameObject targetObject,
        Transform parent = null,
        Vector2? position = null,
        Vector2? scaleModifier = null,
        Quaternion? rotation = null,
        int sortingOrder = 0,
        string name = "attackObject"
        )
    {
        GameObject gameObject = parent == null? GameObject.Instantiate(targetObject) : GameObject.Instantiate(targetObject, parent);
        if(position != null) gameObject.transform.position = position.Value;
        if(rotation != null) gameObject.transform.rotation = rotation.Value;
        if(scaleModifier != null) gameObject.transform.localScale *= scaleModifier.Value;
        
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null) renderer.sortingOrder = sortingOrder;
        
        gameObject.name = name;

        return gameObject;
    }

    public static GameObject GenerateAttackObject(
        string prefabPath,
        float? damage = null,
        Direction? knockbackDirection = null,
        float? knockbackPower = null,
        bool isPlayer = false,
        bool isEnemy = false,
        Transform parent = null,
        Vector2? position = null,
        Vector2? scaleModifier = null,
        Quaternion? rotation = null,
        int sortingOrder = 0,
        string name = "attackObject"
        )
    {
        GameObject prefabObject = Resources.Load<GameObject>(prefabPath);
        if (!prefabObject.TryGetComponent<DamagingObject>(out var damagingObject)) Debug.LogError("Tried instantiating attackObject on non attack object: " + prefabPath + ", try using Generate instead");

        if(damage != null){
            damagingObject.Damage = damage.Value;
        }

        if(isPlayer){
            prefabObject.layer = LayerMask.NameToLayer("PlayerHitbox");
            damagingObject.Damage *= ConfigurationManager.Instance.playerConfig.GLOBAL_DAMAGE_MULTIPLIER;
        }
        else if(isEnemy){
            prefabObject.layer = LayerMask.NameToLayer("EnemyHitbox");
            damagingObject.Damage *= ConfigurationManager.Instance.enemyConfig.GLOBAL_DAMAGE_MULTIPLIER;
        }

        if(knockbackPower != null){
            damagingObject.KnockbackPower = knockbackPower.Value;
        }

        if(knockbackDirection != null){
            damagingObject.KnockbackDirection = knockbackDirection.Value;
        }

        return Generate(prefabObject, parent, position, scaleModifier, rotation, sortingOrder, name);
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