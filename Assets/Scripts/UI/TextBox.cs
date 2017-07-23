using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class TextBox : MonoBehaviour {
	public int characterCutoff;
	public TextAsset cutscene;
	[System.Serializable]
	public class Profile {

		public string name;
		public Sprite sprite;

	}

	public Profile[] profiles;

	private string[] targets;
	private int key = 0;
	private float scrollTime = .01f;

	private Image textbox;
	private Image portrait;
	private Text body;
	private bool scrolling = false;

	public void Start() {
		textbox = GetComponent<Image> ();
		portrait = GetComponentsInChildren<Image> ()[1];
		body = GetComponentInChildren<Text> ();
		EndProcess ();
	}

	public void Update() {
		if (Input.GetButtonDown ("Fire1")) {
			
			Tick ();
		}

	}

	public void OnGUI() {
		if (GUILayout.Button ("Start Cutscene")) {
			if (targets == null) {
				LoadCutscene ();
				Tick ();
			}
		}

		GUILayout.Label ("LMB to continue or scroll faster");

	}

	public void LoadCutscene() {
		StartProcess ();
		targets = (cutscene.text.Replace("\r", "").Replace("\n", "")).Split('$');
	}

	public void Tick() {
		if (!scrolling) {
			if (key >= targets.Length) {

				EndProcess ();
				key = 0;
				targets = null;

			}

			CompareToProfiles (targets [key]);
			StartCoroutine (FillText ());
		}
	}

	public void StartProcess() {
		textbox.enabled = true;
		portrait.enabled = true;
		body.enabled = true;
	}

	public void EndProcess() {
		textbox.enabled = false;
		portrait.enabled = false;
		body.enabled = false;
	}

	public Profile CompareToProfiles(string target) {


		for (int i = 0; i < profiles.Length; i++) {

			string name = profiles [i].name;

			if (target == name) {
				portrait.sprite = profiles [i].sprite;
				key++;
				return profiles [i];
			}
		}

		return null;

	}



	IEnumerator FillText() {

		body.text = "";


		for (int i = 1; i < targets[key].Length + 1; i++) {
			scrolling = true;

			int lap = Mathf.FloorToInt(i / characterCutoff) * characterCutoff;

			Debug.Log (lap + "-" + i);

			body.text = (lap > 0? "-" : "") + targets[key].Substring (lap, i - lap);

			yield return new WaitForSeconds (scrollTime * (Input.GetButton("Fire1")? 1.0f : 5.0f));

		}
		scrolling = false;

		key++;

		yield break;

	}

}
