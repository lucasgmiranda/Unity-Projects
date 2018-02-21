using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dont destroy on load.
/// You have a opened scene A and you want to load a new scene B.
/// You have a gameobject G in the scene A.
/// 
/// If you want to keep your gameobject G in the newly loaded scene B,
/// attache this script to your gameobject G.
/// </summary>
/// 
namespace ScriptsCreatedByDiamond
{
	public class DontDestroyOnLoad : MonoBehaviour
	{

		// Use this for initialization
		void Start()
		{
			DontDestroyOnLoad(gameObject);
		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}
