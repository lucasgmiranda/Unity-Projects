using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

/// <summary>
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{
	/// <summary>
	/// Diamond support.
	/// Simple window, it is about support.
	/// Opened and initialized when the user goes to the Unity top menu:
	/// Tools/Mezanix/Diamond Support
	/// A button called "Mezanix Support" invite you to the mezanix contact web page
	/// to ask questions, send suggestions, and thank me for this very good work :)
	/// </summary>
	public class DiamondSupport : EditorWindow
	{
		static DiamondSupport diamondSupport;

		[MenuItem ("Tools/Mezanix/Diamond Support")]
		static void Init ()
		{
			diamondSupport = EditorWindow.GetWindow <DiamondSupport> ();

			diamondSupport.titleContent = new GUIContent ("Diamond Support");

			diamondSupport.position = new Rect (new Vector2 (300f, 150f), 
				new Vector2 (370f, 120f));

			diamondSupport.Show ();


			Skins.GetGuiSkin (); 
		}

		void ReInit ()
		{
			diamondSupport = EditorWindow.GetWindow <DiamondSupport> ();

			diamondSupport.titleContent = new GUIContent ("Diamond Support");

			diamondSupport.Show ();



			Skins.GetGuiSkin (); 

		}

		//string message = "";
		public void OnGUI ()
		{	
			if (diamondSupport == null)
				ReInit ();

			//Event e = Event.current;

			GUI.Box (new Rect (Vector2.zero, diamondSupport.position.size),
				"", Skins.guiSkin.GetStyle (Skins.view));

			EditorGUI.LabelField (new Rect(new Vector2 (10f, 0f), new Vector2 (300f, 40f)),
				"Any suggestion, bug or error, let me know it, please.\nmezanix",
				Skins.guiSkin.GetStyle (Skins.logicSignature));

			//message = EditorGUI.TextArea (
			//	new Rect(new Vector2 (10f, 50f), 
			//		new Vector2 (diamondSupport.position.width - 20f, 
			//			diamondSupport.position.height - 50f - 40f)),
			//	message);

			if (GUI.Button (new Rect (
				new Vector2 (10f, diamondSupport.position.height - 30f), 
				new Vector2 (95f, 17f)),
				"Mezanix Support", Skins.guiSkin.GetStyle (Skins.LittleNamedRectsCenterDark)))
			{
				Application.OpenURL ("http://mezanix.com/contact-page/");
			}

			Repaint ();
		}
	}
}
 