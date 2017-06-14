using UnityEngine;
using UnityEditor;
public class CustomAssetImporter : AssetPostprocessor {

	private void OnPreprocessTexture() {
		TextureImporter texImporter = assetImporter as TextureImporter;

		texImporter.alphaIsTransparency = true;
		texImporter.compressionQuality = 100;
		texImporter.filterMode = FilterMode.Point;
		texImporter.mipmapEnabled = false;
		texImporter.spritePixelsPerUnit = 32;

	}

	private void OnPostprocessTexture() {}

}
