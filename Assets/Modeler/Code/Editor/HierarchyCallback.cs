using UnityEditor;
using UnityEngine;

namespace Modeler
{
	[InitializeOnLoad]
	public class HierarchyCallback
	{
		static Texture2D sublimeIcon = null;

		static HierarchyCallback()
		{
			sublimeIcon = ResourceUtility.Load<Texture2D>("Icon/Sublime");
			EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
		}

		static void OnProjectWindowItemGUI(string id, Rect rect)
		{
			string path = AssetDatabase.GUIDToAssetPath(id);

			if(sublimeIcon == null || !path.EndsWith(".sublime-project"))
				return;

			rect.x += rect.width - 24;

			GUI.Label(rect, sublimeIcon);
		}
	}
}
