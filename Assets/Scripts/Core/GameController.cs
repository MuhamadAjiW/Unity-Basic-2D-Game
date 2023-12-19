using UnityEngine;

public class GameController : MonoBehaviour {
    static public GameController instance;

    private void Awake(){
        if(instance == null) instance = this;
    }
}