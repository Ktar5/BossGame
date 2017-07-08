using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapImporter : EditorWindow {

	int tileWidth = 32;
	int tileHeight = 32;
	Material mat;

	[MenuItem("Window/MapImporter")]
	static void Init() {
		MapImporter mapImporter = (MapImporter)EditorWindow.GetWindow (typeof(MapImporter));
		mapImporter.Show ();
	}

	void OnGUI() {
		GUILayout.Label("Map Importer", EditorStyles.boldLabel);
		mat = (Material) EditorGUILayout.ObjectField ("Material", mat, typeof(Material), false);
		Texture tex = mat.mainTexture;
		int mapWidth = tex.width / tileWidth;
		int mapHeight = tex.height / tileHeight;

		GUILayout.Label (tex);
		GUILayout.Label ("World space dimensions: " + mapWidth + " X " + mapHeight);


		if (GUILayout.Button ("Create")) {
			

			GameObject map = GameObject.CreatePrimitive (PrimitiveType.Quad);
			map.name = "MAP";
			map.transform.position = Vector3.zero;
			map.transform.localScale = new Vector3 (mapWidth, mapHeight, 1);
			map.GetComponent<MeshRenderer> ().material = mat;

		}

	}

}