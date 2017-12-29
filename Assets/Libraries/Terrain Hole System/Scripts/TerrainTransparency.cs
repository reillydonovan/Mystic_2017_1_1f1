using UnityEngine;
using Random = System.Random;

[ExecuteInEditMode] public class TerrainTransparency : MonoBehaviour
{
	public bool disableBasemap = true;
	public float alphaCutoff = .5f;
	public bool autoUpdateTransparencyMap = true;
	
	public Texture2D opacityMap;

	Terrain terrain;
	TerrainData tData;
	Material tMaterial;
	void Update()
	{
		terrain = GetComponent<Terrain>();
		tData = terrain ? terrain.terrainData : null;
		tMaterial = terrain ? terrain.materialTemplate : null;
		if (!terrain || !tData || !tMaterial)
			return;
		
		if(disableBasemap && !Application.isPlaying && GetComponent<Terrain>().basemapDistance != 1000000) // only reset on update in edit mode
			GetComponent<Terrain>().basemapDistance = 1000000;
		//if (tMaterial.HasProperty("_AlphaCutoff") && tMaterial.GetFloat("_AlphaCutoff") != alphaCutoff)
		{
			var alphaCutoff_final = Application.isPlaying ? alphaCutoff + .00001f : alphaCutoff; // forces property change on play-mode stop, to force material refresh, to fix that terrain would not display on play-mode stop
			tMaterial.SetFloat("_AlphaCutoff", alphaCutoff_final);
		}

		if (!opacityMap && autoUpdateTransparencyMap)
		{
			UpdateTransparencyMap();
			ApplyTransparencyMap();
		}
		else
			ApplyTransparencyMap();
	}

	public void UpdateTransparencyMap() {
		Debug.Log("Updating transparency map");
		var newTransparencyMapValues = new Color[tData.alphamapResolution, tData.alphamapResolution];
		for (var slotIndex = 0; slotIndex < tData.alphamapLayers; slotIndex++)
		{
			SplatPrototype slotTexture = tData.splatPrototypes[slotIndex];

			// found the transparent texture slot
			if (slotTexture.texture != null && slotTexture.texture.name == "Transparent")
			{
				float[,,] slotApplicationMapValues = tData.GetAlphamaps(0, 0, tData.alphamapResolution, tData.alphamapResolution);
				for (var a = 0; a < tData.alphamapResolution; a++)
					for (var b = 0; b < tData.alphamapResolution; b++)
					{
						float textureStrength = slotApplicationMapValues[a, b, slotIndex];
						var newColor = new Color(0, 0, 0, 1 - textureStrength);
						newTransparencyMapValues[b, a] = newColor;
					}
				break;
			}
		}

		bool opacityMapNeedsUpdating = !opacityMap;
		if (!opacityMapNeedsUpdating)
		{
			try
			{
				Color[] opacityMap_colors = opacityMap.GetPixels();
				if (opacityMap.width != tData.alphamapResolution || opacityMap.height != tData.alphamapResolution) // if line above passed (i.e. transparency-map was script-created), and size is outdated
					opacityMapNeedsUpdating = true;
				if (!opacityMapNeedsUpdating)
					for (var a = 0; a < tData.alphamapResolution; a++)
						for (var b = 0; b < tData.alphamapResolution; b++)
							if (opacityMap_colors[(a * tData.alphamapResolution) + b] != newTransparencyMapValues[b, a]) //opacityMap.GetPixel(b, a) != newTransparencyMapValues[b, a])
							{
								opacityMapNeedsUpdating = true;
								break;
							}
			}
			catch (UnityException ex)
			{
				if (!ex.Message.Contains("is not readable")) // (ignore 'is not readable' errors; when they occur, the needs-updating flag is left as: false)
					throw;
			}
		}

		if (opacityMapNeedsUpdating)
		{
			// if old transparency map was of a different resolution, destroy old transparency map
			if (opacityMap)
			{
				DestroyImmediate(opacityMap);
				opacityMap = null;
			}
			if (!opacityMap)
				opacityMap = new Texture2D(tData.alphamapResolution, tData.alphamapResolution);

			for (var a = 0; a < tData.alphamapResolution; a++)
				for (var b = 0; b < tData.alphamapResolution; b++)
					opacityMap.SetPixel(a, b, newTransparencyMapValues[a, b]);
			opacityMap.Apply();
		}
	}
	public void ApplyTransparencyMap()
	{
		// apply our opacity map (ensure our opacity map is connected to the shader)
		tMaterial.SetTexture("_OpacityMap", opacityMap);
	}
}