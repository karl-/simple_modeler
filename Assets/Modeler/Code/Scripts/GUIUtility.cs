using UnityEngine;
using System.Collections.Generic;

namespace Modeler
{
	public static class GUIUtility
	{
		private static GUISkin _skin = null;

		public static GUISkin skin
		{
			get
			{
				if(_skin == null)
				{
					_skin = (GUISkin) Resources.Load("Skin/Modeler", typeof(GUISkin));

					// Crappy workaround for retina displays - not scale-able at all
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
					_skin.font = (Font) Resources.Load("Font/FiraCode-Retina", typeof(Font));

					foreach(GUIStyle style in _skin.customStyles)
					{
						if(style.fontSize > 0)
							style.fontSize = (int) (_skin.font.fontSize * 1.5f);
					}
#endif
				}
				return _skin;
			}
		}

		private static Dictionary<string, GUIStyle> onStyles = new Dictionary<string, GUIStyle>();

		public static GUIStyle GetStyleOn(string name)
		{
			GUIStyle active = null;

			if(onStyles.TryGetValue(name, out active))
				return active;

			active = new GUIStyle(GUI.skin.GetStyle(name));
			active.normal = active.onNormal;
			onStyles.Add(name, active);

			return active;
		}

		/**
		 * Workaround for IMGUI scaling on retina display.
		 */
		public static float ScalePoint(float x)
		{
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
			return x * 2f;
#else
			return x;
#endif
		}

		public static Vector2 ScalePoint(Vector2 v)
		{
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
			return v * 2f;
#else
			return v;
#endif
		}
	}
}
