﻿using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Modeler.Editor
{
	public class IconPreProcessor : AssetPostprocessor
	{
		/**
		 *	Automatically set the importer settings for icons.
		 */
		public void OnPreprocessTexture()
		{
			if( assetPath.IndexOf("GUI/") < 0 )
				return;

			TextureImporter ti = (TextureImporter) assetImporter;

#if !UNITY_5_5
			ti.textureType = TextureImporterType.Advanced;
			ti.textureFormat = TextureImporterFormat.AutomaticTruecolor;
			ti.linearTexture = true;
#elif UNITY_5_5
			ti.sRGBTexture = false;
#endif
			ti.npotScale = TextureImporterNPOTScale.None;
			ti.filterMode = FilterMode.Point;
			ti.wrapMode = TextureWrapMode.Clamp;
			ti.mipmapEnabled = false;
			ti.maxTextureSize = 64;
		}
	}
}
