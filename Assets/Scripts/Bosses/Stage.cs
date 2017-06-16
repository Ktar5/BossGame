using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage {

    public Behavior behavior;
    public Dictionary<int, Meatbag> meatbags;

    protected abstract void StartStage();

    protected abstract void EndStage();

}

public abstract class Behavior {
    
}