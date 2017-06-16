using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int MaxHP = 100;
	private int HP;

	public void Start() {
		HP = MaxHP;
	}

	public void OperateHP(int amount) {HP += amount;}
	public void Damage(int amount) {OperateHP (-amount);}


	public void Update() {
		Debug.Log (gameObject.name + " has " + HP + "/" + MaxHP + " health.");
	}

}
