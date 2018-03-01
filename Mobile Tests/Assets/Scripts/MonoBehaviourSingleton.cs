using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Inst
	{
		get
		{
			if (inst == null)
			{
				GameObject coreGameObject = new GameObject(typeof(T).Name);

				inst = coreGameObject.AddComponent<T>();
			}

			return inst;
		}
	}

	private static T inst;

	protected virtual void Awake()
	{
		if (inst == null)
			inst = GetComponent<T>();
		else
			DestroyImmediate(this);
	}
}
