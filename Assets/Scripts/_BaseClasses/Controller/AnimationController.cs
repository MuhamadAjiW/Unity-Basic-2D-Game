using UnityEngine;

public class AnimationController{
    public static void Mirror(Transform transform){
        Vector2 mirrored = transform.localScale;
        mirrored.x = -mirrored.x;
        transform.localScale = mirrored;
    }
}