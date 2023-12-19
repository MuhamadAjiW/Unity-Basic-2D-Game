using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EntityStateController{
    protected MonoBehaviour entity;
    protected int state;
    protected int heading = HeadingDirection.RIGHT;

    public EntityStateController(MonoBehaviour entity){
        this.entity = entity;
    }

    public int State{
        get { return state; }
    }

    public int Heading{
        get { return heading; }
    }
    public abstract int UpdateState();
}
