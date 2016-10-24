using UnityEngine;
using System.Collections.Generic;

namespace Modeler
{
	public static class ModelerGUI
	{
		private static GUISkin _skin = null;

		public static GUISkin skin
		{
			get
			{
				if(_skin == null)
					_skin = (GUISkin) Resources.Load("Skin/Modeler", typeof(GUISkin));
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
	}
}
