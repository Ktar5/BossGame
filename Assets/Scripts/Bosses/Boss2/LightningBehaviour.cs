using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBehaviour : MonoBehaviour {

	public GameObject strike;
	private InvertCameraEffect cam;
	private float timeToNextStrike = 0;

	void Start() {

		cam = Camera.main.GetComponent<InvertCameraEffect> ();
		strike.SetActive (false);
		cam.enabled = false;
	}

	IEnumerator Strike() {


		strike.SetActive (true);
		strike.transform.position = new Vector3 (
			transform.position.x + Random.Range (-6, 6),
			strike.transform.position.y,
			strike.transform.position.z);

		yield return new WaitForSeconds(.1f);

		cam.enabled = true;

		yield return new WaitForSeconds (.1f);

		cam.enabled = false;

		yield return new WaitForSeconds (.1f);

		cam.enabled = true;

		yield return new WaitForSeconds (.1f);

		cam.enabled = false;
		strike.SetActive (false);

	}

	void Update() {

		timeToNextStrike -= Time.deltaTime;

		if (timeToNextStrike <= 0) {
			StartCoroutine (Strike ());
			timeToNextStrike = Random.Range (2, 15);
		}

	}
}
