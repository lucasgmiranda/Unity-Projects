using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Mezanix.GameDesign
{
	public class GameDesign 
	{
		public static int lateUpdateAdded = 0;

		public class CustomInspectorsStrings
		{
			public const string enumKeyQuickAccesHelp = "keys for: keyboard, mouse, joysticks." +
				"\nIn the above Input Key field: Press the first letter of your key name to quick access to it";
			public const string enumTowKeysQuickAccesHelp = "keys for: keyboard, mouse, joysticks." +
				"\nIn the above Positive/Negative Input Key fields: Press the first letter of your key name to quick access to it." +
				"\nUse Two keys when the action has to lower or upper the input value";

			public static string InputOfActionHelp(string actionName)
			{
				return actionName + " =>\n" +
						"   Select your own " + actionName + " keys (keyboard, mouse, joysticks)";
			}
			public static string InputOfActionHelp (string actionName, string desktopInput, string mobileInput,
				string joysticksInput)
			{
				return actionName + " =>\n" +
						"1. " + desktopInput + " on desktop\n" +
						"2. " + mobileInput + " on mobile\n" +
						"    (CrossPlatformInput Standard Asset)\n" +
						"3. " + joysticksInput + " with Joysticks";
			}

			public const string thisAxisNameIsNotSetup = "This Axis/Button Name is not setup";
			public const string inputManagerEditorUrl = "Edit > Project Settings > Input";
			public const string inputManagerGuidance = "Go To: " + inputManagerEditorUrl + "\nTo edit the Input Manager or have more information";
		}
		
	}
}