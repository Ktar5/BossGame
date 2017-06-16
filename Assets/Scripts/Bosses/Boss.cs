using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour {

    private Dictionary<int, Meatbag> meatbags;
    private Behavior behavior;

    public Boss(int health, int maxHealth) {
        
    }

    public void ApplyStage(Stage stage) {
        this.meatbags = stage.meatbags;
        this.behavior = stage.behavior;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

}
