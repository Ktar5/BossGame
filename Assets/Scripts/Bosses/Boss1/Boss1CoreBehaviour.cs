using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1CoreBehaviour : MonoBehaviour {

	Boss1HandBehaviour[] hands;

	public GameObject player;

	public enum AttackMode {
		SLAM,
		SPIRAL,
		CLAP,
		PUSH
	}

	public AttackMode attackMode;

	//ATTACK VARS
	public int phase = 1;

	//SPIRAL
	float spiral_mainspeed = .004f;
	float spiral_sin = -1f;
	float spiral_speed = .02f;
	[Range(0,1)]
	public float spiral_time = 0;
	[Range(1,2)]
	bool spiral_moving = true; 

	public void Start() {
		hands = GetComponentsInChildren<Boss1HandBehaviour> ();
	}

	public void FixedUpdate() {

		switch (attackMode) {
		case AttackMode.SPIRAL:
		default:
			Spiral (1);
			break;
		}

	}

	public bool Slam() {
		return false;
	}

	public bool Spiral(float distance) {
		float speed = Mathf.Lerp (spiral_speed * 3, spiral_speed * 1, spiral_time);
		float magnitude = Mathf.Lerp (1, 6, spiral_time);

		if (spiral_moving) {
			Vector3 away = (player.transform.position - transform.position).normalized * -1.0f;

			transform.position += away * spiral_mainspeed;

			transform.position = new Vector3 (
				Mathf.Clamp (transform.position.x, -6f, 6f),
				Mathf.Clamp (transform.position.y, -3f, 3f),
				0);

		}

		for (int i = 0; i < hands.Length; i++) {

			Boss1HandBehaviour hand = hands [i];

			Transform t = hand.gameObject.transform;

			float offset = i * ((2 * Mathf.PI) / hands.Length);

				t.localPosition = Vector3.Lerp (t.localPosition, 
					new Vector3 (
					Mathf.Sin (spiral_sin + offset) * distance * magnitude,
					Mathf.Cos (spiral_sin + offset) * distance * magnitude,
						0),
					.2f);
			

			Vector3 locEulers = new Vector3(0,0,
				(spiral_moving? Mathf.Atan2 (t.localPosition.y, t.localPosition.x)*Mathf.Rad2Deg :
					i * (360/hands.Length)));

			//t.localEulerAngles = Vector3.Lerp (t.localEulerAngles, locEulers, .08f);
			t.localRotation = Quaternion.Lerp(t.localRotation, Quaternion.Euler(locEulers), .08f);

			if (spiral_moving) {
				spiral_sin += speed / hands.Length;
			}
		}


		spiral_time += 0.002f;

		if (spiral_time > 1) {
			spiral_time = -.2f;
			spiral_moving = true;
			return true;
		} else if (spiral_time > .8) {
			spiral_moving = false;
			return false;
		} else {
			return false;
		}

	}

	public bool Clap() {
		return false;
	}

	public bool Push(float distance) {

		return false;

	}

}