using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PixelPerfectRender : MonoBehaviour {

	public Material mat;
	public int nativeSizeX = 480;
	public int nativeSizeY = 270;

	public enum ScaleMode
	{
		STRETCH,
		FIT
	}

	public ScaleMode scaleMode;

	[HideInInspector]
	[SerializeField]
	private Camera mainCam;
	private Camera renderCam;

	private string globalTextureName = "GlobalPixelPerfectTex";

	void Start() {
		GenerateRT ();
	}

	void GenerateRT() {

		mainCam = transform.parent.gameObject.GetComponent<Camera> ();
		renderCam = GetComponent<Camera> ();

		if (mainCam.targetTexture != null) {
			RenderTexture temp = mainCam.targetTexture;
			mainCam.targetTexture = null;
			DestroyImmediate (temp);
		}

		if (scaleMode == ScaleMode.FIT) {

			//Reset RenderCam
			renderCam.rect = new Rect (0.0f, 0.0f, 1.0f, 1.0f);

			float nativeAspect = .1f * Mathf.Round ((nativeSizeX * 10.0f) / (nativeSizeY * 1.0f));
			float targetAspect = .1f * Mathf.Round ((renderCam.pixelWidth * 10.0f) / (renderCam.pixelHeight * 1.0f));

			Debug.Log ("the native aspect is " + nativeAspect);
			Debug.Log ("the target aspect is " + targetAspect);

			if (targetAspect > nativeAspect) {
				//Pillarbox
				Debug.Log ("Target aspect is greater! Pillarbox required.");
				float inset = 1f - (nativeAspect / targetAspect);
				Rect r = new Rect (inset / 2f, 0.0f, 1f - inset, 1.0f);
				Debug.Log (r);
				renderCam.rect = r;

			} else if (targetAspect < nativeAspect) {
				//Letterbox	
				Debug.Log ("Target aspect is lesser! Letterbox required.");
				float inset = 1f - (targetAspect / nativeAspect);
				renderCam.rect = new Rect (0.0f, inset / 2f, 1.0f, 1f - inset);
		
			} else {
				//Equal aspect
				Debug.Log ("Target aspect is same!");
				Rect r = new Rect (0.0f, 0.0f, 1.0f, 1.0f);
				Debug.Log (r);
				renderCam.rect = r;

			}
		}

		mainCam.targetTexture = new RenderTexture (nativeSizeX, nativeSizeY, 16);
		mainCam.targetTexture.filterMode = FilterMode.Point;

		Shader.SetGlobalTexture (globalTextureName, mainCam.targetTexture);

		mat.SetTexture("_MainTex", Shader.GetGlobalTexture(globalTextureName));

	}


	void OnRenderImage(RenderTexture src, RenderTexture dst) {

		Graphics.Blit (null, dst, mat);

	}


}
