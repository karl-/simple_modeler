using UnityEngine;

namespace Modeler
{
	/**
	 * A singleton implementation for MonoBehaviours.
	 * First written for GILES: https://github.com/procore3d/giles
	 */
	public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		/// The actual instance of this type.
		private static MonoBehaviour _instance;

		/// Override to maintain an instance of this object across level loads.
		public virtual bool dontDestroyOnLoad { get { return false; } }

		/**
		 * Called when an instance is initialized due to no previous instance found.  Use this to
		 * initialize any resources this singleton requires (eg, if this is a gui item or prefab,
		 * build out the hierarchy in here or instantiate stuff).
		 */
		protected virtual void OnInitialized() {}

		/**
		 * Get an instance to this MonoBehaviour.  Always returns a valid object.
		 * \sa nullableInstance
		 */
		public static T instance
		{
			get
			{
				if(_instance == null)
				{
					// first search the scene for an instance
					T[] scene = FindObjectsOfType<T>();

					if(scene != null && scene.Length > 0)
					{
						_instance = scene[0];

						for(int i = 1; i < scene.Length; i++)
						{
							Object.Destroy(scene[i]);
						}
					}
					else
					{
						GameObject go = new GameObject();
						string type_name = typeof(T).ToString();
						int i = type_name.LastIndexOf('.') + 1;
						go.name = (i > 0 ? type_name.Substring(i) : type_name) + " Singleton";
						T inst = go.AddComponent<T>();
						MonoBehaviourSingleton<T> cast = inst as MonoBehaviourSingleton<T>;
						if(cast != null) cast.OnInitialized();
						_instance = (MonoBehaviour) inst;
					}

					if(((MonoBehaviourSingleton<T>)_instance).dontDestroyOnLoad)
						Object.DontDestroyOnLoad(_instance.gameObject);
				}

				return (T) _instance;
			}
		}

		/**
		 * Return the instance if it has been initialized, null otherwise.
		 */
		public static T nullableInstance
		{
			get { return (T) _instance; }
		}

		/**
		 * If overriding, be sure to call base.Awake().
		 */
		protected virtual void Awake()
		{
			if(_instance == null)
			{
				_instance = this;

				if(((MonoBehaviourSingleton<T>)_instance).dontDestroyOnLoad)
					Object.DontDestroyOnLoad(_instance.gameObject);
			}
			else
			{
				Object.Destroy(this.gameObject);
			}
		}
	}
}
