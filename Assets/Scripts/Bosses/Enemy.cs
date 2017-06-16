using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public bool drawHealth = true;
	public int MaxHP = 100;
	public float recoilSeconds;

	private int HP;
	private bool recoiling;
	private Texture2D red;
	public void Start() {
		HP = MaxHP;
		red = new Texture2D (1, 1);
		Color r = Color.red;
		r.a = .5f;
		red.SetPixel (0, 0, r);
		red.Apply ();

	}

	IEnumerator Recoil() {
		recoiling = true;
		yield return new WaitForSeconds (recoilSeconds);
		recoiling = false;
	}

	public void OperateHP(int amount) {HP += amount;}

	public void Damage(int amount) {
		if (!recoiling) {
			StartCoroutine (Recoil ());
			OperateHP (-amount);
		}
	}

	public float GetHealthPercent() {
		return ((HP * 1.0f) / (MaxHP * 1.0f));
	}

	public bool isRecoiling() {
		return recoiling;
	}

	void OnGUI() {

		int width = (int) ((Screen.width * .8f) * GetHealthPercent());
		int height = (int) (Screen.height * .1f);
		int x = (int) (Screen.width * .1f);
		int y = (int) (Screen.height * .8f);

		Rect healthRect = new Rect (x, y, width, height);

		GUI.DrawTexture (healthRect, red);

	}

	public void Update() {
	}


}
