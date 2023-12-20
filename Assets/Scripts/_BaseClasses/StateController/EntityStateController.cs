using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EntityStateController{
    protected MonoBehaviour entity;
    protected int state;
    protected Direction heading = Direction.RIGHT;

    public EntityStateController(MonoBehaviour entity){
        this.entity = entity;
    }

    public int State => state;
    public Direction Heading => heading;
    public abstract int UpdateState();
}
