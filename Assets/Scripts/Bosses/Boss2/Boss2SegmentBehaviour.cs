using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2SegmentBehaviour : MonoBehaviour {


	public GameObject segment;
	public Transform butt;
	public Transform tail;
	public const int count = 12;
	private float sin = 0;
	private GameObject[] segments;

	public void Start() {

		segments = new GameObject[count];

		for (int i = 0; i < count; i++) {

			segments [i] = Instantiate (segment, transform.position, Quaternion.identity);
			segments [i].GetComponent<SpriteRenderer> ().sortingOrder = -2;
			segments [i].transform.SetParent (transform);
			segments [i].name = "segment_" + i;

		}

	}

	public void Update() {
		UpdatePositions ();
	}

	public void UpdatePositions() {

		sin += .045f;
		if (sin >= Mathf.PI * 2f) {
			sin = 0f;
		}


		for (int i = 0; i < count; i++) {
			Vector3 pos = Vector3.Lerp (butt.position, tail.position, (1.0f * i) / (1.0f * count));
			/*
			pos += new Vector3 (.5f+
				Mathf.Sin(sin + (middle - Mathf.Abs(i - middle))) * 
				(middle - Mathf.Abs(i - middle))*.03f, 
				-.5f, 0.0f);
			*/
			segments [i].transform.position = pos;

		}

	}


}
