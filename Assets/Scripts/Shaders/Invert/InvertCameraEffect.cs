using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InvertCameraEffect : MonoBehaviour {
	
	public Material mat;

	void OnRenderImage(RenderTexture source, RenderTexture destination) {
		if (mat != null) {
			Graphics.Blit (source, destination, mat);
		}
	}

}
