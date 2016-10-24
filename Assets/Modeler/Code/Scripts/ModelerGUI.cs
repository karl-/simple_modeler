using UnityEngine;

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
	}
}
