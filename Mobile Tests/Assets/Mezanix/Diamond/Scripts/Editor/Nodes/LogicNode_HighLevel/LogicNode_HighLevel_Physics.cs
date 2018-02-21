using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// LogicNode_HighLevel_Physics
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{	
	public partial class LogicNode : ScriptableObject 
	{
		string [] GetCodeFrom_HighLevelNode ()
		{
			string [] r = new string[5];

			switch (highLevelNode)
			{
			case "Physics":
				switch (physicsType)
				{
				case "Move":
					switch (move)
					{
					case "Set Velocity":
						r = SetVelocity_GetCodeFrom ();
						break;

					case "Add Force":
						r = AddForce_GetCodeFrom ();
						break;
					}
					break;

				case "Contact":
					switch (contact)
					{
					case "Trigger":
						r = Trigger_GetCodeFrom ();
						break;

					case "Collision":
						r = Collision_GetCodeFrom ();
						break;
					}
					break;
				}
				break;

			case "UI":
				switch (UI)
				{
				case "Text":
					switch (Text)
					{
					case "Show":
						r = ShowText_GetCodeFrom ();
						break;

					case "Get":
						r = GetText_GetCodeFrom ();
						break;
					}
					break;

				case "Button":
					switch (Button)
					{
					case "On Click":
						r = OnClick_GetCodeFrom ();
						break;
					}
					break;

				case "Image":
					break;
				}
				break;

			case "Inputs":
				switch (Inputs)
				{
				case "Mouse":
					r = MouseInput_GetCodeFrom ();
					break;
				
				case "Keyboard":
					r = KeyboardInput_GetCodeFrom ();
					break;

				case "Axis":
					r = AxisInput_GetCodeFrom ();
					break;
				}
				break;
			}

			return r;
		}



		#region head
		void CallPartialHighLevelNodes ()
		{
			DrawHighLevelNodesStringEnum ();

			AdaptOnLogicHighLevelNodes ();
		}

		void DrawHighLevelNodesStringEnum ()
		{
			DrawLogicNodeLabel ("Node", 0, 2);
			Draw_highLevelNode_ListMenu (highLevelNodes, 1, 2);
		}

		void AdaptOnLogicHighLevelNodes ()
		{
			switch (highLevelNode)
			{
			case "Physics":
				Draw_physicsType_ListMenu ();
				break;

			case "UI":
				Draw_UI_ListMenu ();
				break;

			case "Inputs":
				Draw_Inputs_ListMenu ();
				break;
			}
		}
		#endregion head


		#region contact_StringEnum
		Collider coll = null;
		const string coll_toCode = "\t\tCollider coll = null;\n";
		bool GetColl ()
		{
			if (coll == null)
			{
				if (gameObjectValues [0] == null)
					return false;

				coll = gameObjectValues [0].GetComponent <Collider>();

				if (coll == null)
					return false;

				return true;
			}

			return true;
		}
		const string getColl_toCode = "\t\tbool GetColl ()\n\t\t{\n\t\t\tif (coll == null)\n\t\t\t{\n\t\t\t\tif (gameObjectValues [0] == null)\n\t\t\t\t\treturn false;\n\n\t\t\t\tcoll = gameObjectValues [0].GetComponent <Collider>();\n\n\t\t\t\tif (coll == null)\n\t\t\t\t\treturn false;\n\n\t\t\t\treturn true;\n\t\t\t}\n\n\t\t\treturn true;\n\t\t}\n";

		float countForCollisionExitToNone = 0f;
		const string countForCollisionExitToNone_toCode = "\t\tfloat countForCollisionExitToNone = 0f;\n";

		void InitBoolListValueForCollision ()
		{
			boolsListValue = new List<bool> ();
			for (int i = 0; i < 4; i++)
				boolsListValue.Add (false);
		}
		const string InitBoolListValueForCollision_toCode = "\t\tvoid InitBoolListValueForCollision ()\n\t\t{\n\t\t\tboolsListValue = new List<bool> ();\n\t\t\tfor (int i = 0; i < 4; i++)\n\t\t\t\tboolsListValue.Add (false);\n\t\t}\n";

		void IniVector3ListValueForCollision ()
		{
			vector3ListValue = new List<Vector3> ();
			for (int i = 0; i < 5; i++)
				vector3ListValue.Add (Vector3.zero);
		}
		const string InitVector3ListValueForCollision_toCode = "\t\tvoid IniVector3ListValueForCollision ()\n\t\t{\n\t\t\tvector3ListValue = new List<Vector3> ();\n\t\t\tfor (int i = 0; i < 5; i++)\n\t\t\t\tvector3ListValue.Add (Vector3.zero);\n\t\t}\n";

		bool showCollisionState = false;

		bool showCollisionInfo = false;

		void Collision_Head ()
		{
			Collision_Input ();
			if (logic.playing) Collision ();
			Collision_Output ();
		}
		void Collision_Input ()
		{
			DrawLogicNodeLabel ("Gameobject", 0, 2);
			DrawGameObjectFieldInput (0, 1, 2);
			if (gameObjectValues [0] == null)
			{
				DrawInNodeInfo ("Fill In Gameobject field");
				return;
			}
			coll = gameObjectValues [0].GetComponent <Collider> ();
			if (coll == null)
			{
				DrawInNodeInfo ("Add Collider component to Gameobject");
				return;
			}

			DrawLogicNodeLabel ("Exit delay", 0, 2);
			DrawFloatInputField (0, 1, 2);
			floatValues [0] = Mathf.Max (0.02f, floatValues [0]);

			DrawLogicNodeLabel ("Compare tag", 0, 2);
			DrawTagField (0, 1, 2);

			InitBoolListValueForCollision ();

			IniVector3ListValueForCollision ();

			DrawDoItButton ();
		}
		void Collision ()
		{
			if ( ! doIT)
				return;

			if ( ! GetColl ())
				return;

			if ( ! GetMezanixDiamondPhysics ())
				return;


			if (mezanixDiamondPhysics.collisionState == 
				ScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.exit)
			{
				countForCollisionExitToNone += Time.deltaTime;

				if (countForCollisionExitToNone > floatValues [0])
				{
					countForCollisionExitToNone = 0f;

					mezanixDiamondPhysics.collisionState = 
						ScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.none;
				}
			}
			else
			{
				countForCollisionExitToNone = 0f;
			}
			switch (mezanixDiamondPhysics.collisionState)
			{
			case ScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.none:
				boolsListValue [0] = true;
				boolsListValue [1] = false;
				boolsListValue [2] = false;
				boolsListValue [3] = false;
				break;

			case ScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.enter:
				boolsListValue [0] = false;
				boolsListValue [1] = true;
				boolsListValue [2] = false;
				boolsListValue [3] = false;
				break;

			case ScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.stay:
				boolsListValue [0] = false;
				boolsListValue [1] = false;
				boolsListValue [2] = true;
				boolsListValue [3] = false;
				break;

			case ScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.exit:
				boolsListValue [0] = false;
				boolsListValue [1] = false;
				boolsListValue [2] = false;
				boolsListValue [3] = true;
				break;
			}


			gameObjectValue = mezanixDiamondPhysics.collisionGo;
			stringValue = mezanixDiamondPhysics.collisionTag;
			boolValue = stringValue == stringValues [0];

			vector3ListValue [0] = mezanixDiamondPhysics.collisionPoint;
			vector3ListValue [1] = mezanixDiamondPhysics.collisionNormal;
			vector3ListValue [2] = mezanixDiamondPhysics.collisionRelativeVelocity;
			vector3ListValue [3] = mezanixDiamondPhysics.collisionImpulse;
			vector3ListValue [4] = mezanixDiamondPhysics.collisionForce;
		}
		const string Collision_toCode = "\t\tvoid Collision ()\n\t\t{\n\t\t\tif ( ! doIT)\n\t\t\t\treturn;\n\n\t\t\tif ( ! GetColl ())\n\t\t\t\treturn;\n\n\t\t\tif ( ! GetMezanixDiamondPhysics ())\n\t\t\t\treturn;\n\n\n\t\t\tif (mezanixDiamondPhysics.collisionState == \n\t\t\t\tScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.exit)\n\t\t\t{\n\t\t\t\tcountForCollisionExitToNone += Time.deltaTime;\n\n\t\t\t\tif (countForCollisionExitToNone > floatValues [0])\n\t\t\t\t{\n\t\t\t\t\tcountForCollisionExitToNone = 0f;\n\n\t\t\t\t\tmezanixDiamondPhysics.collisionState = \n\t\t\t\t\t\tScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.none;\n\t\t\t\t}\n\t\t\t}\n\t\t\telse\n\t\t\t{\n\t\t\t\tcountForCollisionExitToNone = 0f;\n\t\t\t}\n\t\t\tswitch (mezanixDiamondPhysics.collisionState)\n\t\t\t{\n\t\t\tcase ScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.none:\n\t\t\t\tboolsListValue [0] = true;\n\t\t\t\tboolsListValue [1] = false;\n\t\t\t\tboolsListValue [2] = false;\n\t\t\t\tboolsListValue [3] = false;\n\t\t\t\tbreak;\n\n\t\t\tcase ScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.enter:\n\t\t\t\tboolsListValue [0] = false;\n\t\t\t\tboolsListValue [1] = true;\n\t\t\t\tboolsListValue [2] = false;\n\t\t\t\tboolsListValue [3] = false;\n\t\t\t\tbreak;\n\n\t\t\tcase ScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.stay:\n\t\t\t\tboolsListValue [0] = false;\n\t\t\t\tboolsListValue [1] = false;\n\t\t\t\tboolsListValue [2] = true;\n\t\t\t\tboolsListValue [3] = false;\n\t\t\t\tbreak;\n\n\t\t\tcase ScriptsCreatedByDiamond.MezanixDiamondPhysics.CollisionState.exit:\n\t\t\t\tboolsListValue [0] = false;\n\t\t\t\tboolsListValue [1] = false;\n\t\t\t\tboolsListValue [2] = false;\n\t\t\t\tboolsListValue [3] = true;\n\t\t\t\tbreak;\n\t\t\t}\n\n\n\t\t\tgameObjectValue = mezanixDiamondPhysics.collisionGo;\n\t\t\tstringValue = mezanixDiamondPhysics.collisionTag;\n\t\t\tboolValue = stringValue == stringValues [0];\n\n\t\t\tvector3ListValue [0] = mezanixDiamondPhysics.collisionPoint;\n\t\t\tvector3ListValue [1] = mezanixDiamondPhysics.collisionNormal;\n\t\t\tvector3ListValue [2] = mezanixDiamondPhysics.collisionRelativeVelocity;\n\t\t\tvector3ListValue [3] = mezanixDiamondPhysics.collisionImpulse;\n\t\t\tvector3ListValue [4] = mezanixDiamondPhysics.collisionForce;\n\t\t}\n";
		void Collision_Output ()
		{
			DrawLogicNodeLabel ("Collide gameobject", 0, 2);
			DrawGameObjectResultField (ObjectResultDrawChoice.itsName, 1, 2);

			DrawLogicNodeLabel ("it's tag", 0, 2);
			DrawStringResultField (true, 1, 2);

			DrawLogicNodeLabel ("is it the compare tag?", 0, 2);
			DrawBoolResultField (1, 2);

			DrawLogicNodeLabel ("Use Collision State", 0, 2);
			showCollisionState = EditorGUI.Toggle (GetSuitableRect (FieldDrawType.label, 3, 4), showCollisionState);

			if (showCollisionState)
				DrawBoolListResultField (1, 2, new string[]{"none", "Enter", "Stay", "Exit"}, true, false);


			DrawLogicNodeLabel ("Use Collision Info", 0, 2);
			showCollisionInfo = EditorGUI.Toggle (GetSuitableRect (FieldDrawType.label, 3, 4), showCollisionInfo);

			if (showCollisionInfo)
				DrawVector3ListResultField (1, 2, new string []{"Point", "Normal", "Rel Velocity", "Impulse", 
					"Coll Force"}, true, false);

			string [] documentationMessage = 
				new string[]
			{				
				"  \tIF ENTERED IN COLLISION",
				"  \t--------------------------------------------",
				"",
				"  For 2 objects which might collide, be sure that:",
				"  at least one objects has a non-kinematic rigidbody component.",
				"  ",
				"  Exit delay = duration for which the collision state",
				"  \t\tstill Exit before turning to none",
				"",
				"  Compare to tag = compare to the found objects tag",
				"",
				"",
				"  Collision State:",
				"  Enter = just entered in collision",
				"  Stay = still in contact",
				"  Exit = just exited from the collision",
				"",
				"  Collision Info:",
				"  Point = first point of collision contact",
				"  Normal = first normal of collision contact",
				"  Rel Velocity = relative velocity of 2 collided objects",
				"  Impulse = impulse of 2 collided objects to prevent penetration",
				"  Coll Force = Collision Force = Impulse / fixedDeltaTime of last frame",
			};

			DrawDocumentationBoxUpRight (documentationMessage);
			DrawDocumentationUrlButtons (documentationMessage, 
				"https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html", 
				"");
		}
		string [] Collision_GetCodeFrom ()
		{
			string [] r = new string[5];

			r [0] = ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.gameObjectValues + 
				ExprWs.Gv.floatValues + ExprWs.Gv.stringValues + countForCollisionExitToNone_toCode +
				coll_toCode + mezanixDiamondPhysics_toCode +
				ExprWs.Gv.gameObjectValue + ExprWs.Gv.stringValue + ExprWs.Gv.boolValue +
				ExprWs.Gv.boolsListValue + ExprWs.Gv.vector3ListValue;
			
			r [1] = ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}) +
				ExprWs.ConstructorExpr.FloattValues (this) + ExprWs.ConstructorExpr.StringValues (this) +
				"\t\t\tInitBoolListValueForCollision ();\n" + 
				"\t\t\tIniVector3ListValueForCollision ();\n";

			r [2] = "\t\t\tCollision ();\n";

			r [3] = Collision_toCode + getMezanixDiamondPhysics_toCode + getColl_toCode +
				InitBoolListValueForCollision_toCode + InitVector3ListValueForCollision_toCode;
			r [4] = "";

			return r;
		}



		float countForTriggerExitToNone = 0f;
		const string countForTriggerExitToNone_toCode = "\t\tfloat countForTriggerExitToNone = 0f;\n";

		void InitBoolListValueForTrigger ()
		{
			boolsListValue = new List<bool> ();
			for (int i = 0; i < 4; i++)
				boolsListValue.Add (false);
		}
		const string InitBoolListValueForTrigger_toCode = "\t\tvoid InitBoolListValueForTrigger ()\n\t\t{\n\t\t\tboolsListValue = new List<bool> ();\n\t\t\tfor (int i = 0; i < 4; i++)\n\t\t\t\tboolsListValue.Add (false);\n\t\t}\n";

		bool showTriggerState = false;

		void Trigger_Head ()
		{
			Trigger_Input ();
			if (logic.playing) Trigger ();
			Trigger_Output ();
		}
		void Trigger_Input ()
		{
			DrawLogicNodeLabel ("Gameobject", 0, 2);
			DrawGameObjectFieldInput (0, 1, 2);
			if (gameObjectValues [0] == null)
			{
				DrawInNodeInfo ("Fill In Gameobject field");
				return;
			}
			coll = gameObjectValues [0].GetComponent <Collider> ();
			if (coll == null)
			{
				DrawInNodeInfo ("Add Collider component to Gameobject");
				return;
			}

			DrawLogicNodeLabel ("Exit delay", 0, 2);
			DrawFloatInputField (0, 1, 2);
			floatValues [0] = Mathf.Max (0.02f, floatValues [0]);

			DrawLogicNodeLabel ("Compare tag", 0, 2);
			DrawTagField (0, 1, 2);

			InitBoolListValueForTrigger ();

			DrawDoItButton ();
		}
		void Trigger ()
		{
			if ( ! doIT)
				return;

			if ( ! GetColl ())
				return;
			
			if ( ! GetMezanixDiamondPhysics ())
				return;


			if (mezanixDiamondPhysics.triggerState == ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.exit)
			{
				countForTriggerExitToNone += Time.deltaTime;

				if (countForTriggerExitToNone > floatValues [0])
				{
					countForTriggerExitToNone = 0f;

					mezanixDiamondPhysics.triggerState = ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.none;
				}
			}
			else
			{
				countForTriggerExitToNone = 0f;
			}
			switch (mezanixDiamondPhysics.triggerState)
			{
			case ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.none:
				boolsListValue [0] = true;
				boolsListValue [1] = false;
				boolsListValue [2] = false;
				boolsListValue [3] = false;
				break;

			case ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.enter:
				boolsListValue [0] = false;
				boolsListValue [1] = true;
				boolsListValue [2] = false;
				boolsListValue [3] = false;
				break;

			case ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.stay:
				boolsListValue [0] = false;
				boolsListValue [1] = false;
				boolsListValue [2] = true;
				boolsListValue [3] = false;
				break;

			case ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.exit:
				boolsListValue [0] = false;
				boolsListValue [1] = false;
				boolsListValue [2] = false;
				boolsListValue [3] = true;
				break;
			}
		

			gameObjectValue = mezanixDiamondPhysics.otherGo;
			stringValue = mezanixDiamondPhysics.otherTag;
			boolValue = stringValue == stringValues [0];
		}
		const string Trigger_toCode = "\t\tvoid Trigger ()\n\t\t{\n\t\t\tif ( ! doIT)\n\t\t\t\treturn;\n\n\t\t\tif ( ! GetColl ())\n\t\t\t\treturn;\n\t\t\t\n\t\t\tif ( ! GetMezanixDiamondPhysics ())\n\t\t\t\treturn;\n\n\n\t\t\tif (mezanixDiamondPhysics.triggerState == ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.exit)\n\t\t\t{\n\t\t\t\tcountForTriggerExitToNone += Time.deltaTime;\n\n\t\t\t\tif (countForTriggerExitToNone > floatValues [0])\n\t\t\t\t{\n\t\t\t\t\tcountForTriggerExitToNone = 0f;\n\n\t\t\t\t\tmezanixDiamondPhysics.triggerState = ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.none;\n\t\t\t\t}\n\t\t\t}\n\t\t\telse\n\t\t\t{\n\t\t\t\tcountForTriggerExitToNone = 0f;\n\t\t\t}\n\t\t\tswitch (mezanixDiamondPhysics.triggerState)\n\t\t\t{\n\t\t\tcase ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.none:\n\t\t\t\tboolsListValue [0] = true;\n\t\t\t\tboolsListValue [1] = false;\n\t\t\t\tboolsListValue [2] = false;\n\t\t\t\tboolsListValue [3] = false;\n\t\t\t\tbreak;\n\n\t\t\tcase ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.enter:\n\t\t\t\tboolsListValue [0] = false;\n\t\t\t\tboolsListValue [1] = true;\n\t\t\t\tboolsListValue [2] = false;\n\t\t\t\tboolsListValue [3] = false;\n\t\t\t\tbreak;\n\n\t\t\tcase ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.stay:\n\t\t\t\tboolsListValue [0] = false;\n\t\t\t\tboolsListValue [1] = false;\n\t\t\t\tboolsListValue [2] = true;\n\t\t\t\tboolsListValue [3] = false;\n\t\t\t\tbreak;\n\n\t\t\tcase ScriptsCreatedByDiamond.MezanixDiamondPhysics.TriggerState.exit:\n\t\t\t\tboolsListValue [0] = false;\n\t\t\t\tboolsListValue [1] = false;\n\t\t\t\tboolsListValue [2] = false;\n\t\t\t\tboolsListValue [3] = true;\n\t\t\t\tbreak;\n\t\t\t}\n\t\t\n\n\t\t\tgameObjectValue = mezanixDiamondPhysics.otherGo;\n\t\t\tstringValue = mezanixDiamondPhysics.otherTag;\n\t\t\tboolValue = stringValue == stringValues [0];\n\t\t}\n";
		void Trigger_Output ()
		{
			DrawLogicNodeLabel ("Found gameobject", 0, 2);
			DrawGameObjectResultField (ObjectResultDrawChoice.itsName, 1, 2);

			DrawLogicNodeLabel ("it's tag", 0, 2);
			DrawStringResultField (true, 1, 2);

			DrawLogicNodeLabel ("is it the compare tag?", 0, 2);
			DrawBoolResultField (1, 2);


			DrawLogicNodeLabel ("Use Trigger State?", 0, 2);
			showTriggerState = EditorGUI.Toggle (GetSuitableRect (FieldDrawType.label, 3, 4), showTriggerState);

			if (showTriggerState)
				DrawBoolListResultField (1, 2, new string[]{"none", "Enter", "Stay", "Exit"}, true, false);
		
			string [] documentationMessage = 
				new string[]
			{				
				"  \tIF ENTERED IN A TRIGGER ",
				"  \t--------------------------------------------",
				"",
				"  For 2 objects which might collide, be sure that:",
				"  at least one of the colliders has 'is trigger' = true",
				"  at least one objects has a rigidbody component.",
				"  ",
				"  Exit delay = duration for which the trigger state",
				"  \t\tstill Exit before turning to none",
				"",
				"  Compare to tag = compare to the found objects tag",
				"",
				"",
				"  Trigger State:",
				"  Enter = just entered into the tirgger",
				"  Stay = still in the trigger",
				"  Exit = just exited from the trigger",
			};

			DrawDocumentationBoxUpRight (documentationMessage);
			DrawDocumentationUrlButtons (documentationMessage, 
				"https://docs.unity3d.com/ScriptReference/Collider.OnTriggerEnter.html", 
				"");
		}
		string [] Trigger_GetCodeFrom ()
		{
			string [] r = new string[5];

			r [0] = ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.gameObjectValues + 
				ExprWs.Gv.floatValues + ExprWs.Gv.stringValues + countForTriggerExitToNone_toCode +
				coll_toCode + mezanixDiamondPhysics_toCode +
				ExprWs.Gv.gameObjectValue + ExprWs.Gv.stringValue + ExprWs.Gv.boolValue +
				ExprWs.Gv.boolsListValue;

			r [1] = ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}) +
				ExprWs.ConstructorExpr.FloattValues (this) + ExprWs.ConstructorExpr.StringValues (this) +
				"\t\t\tInitBoolListValueForTrigger ();\n";

			r [2] = "\t\t\tTrigger ();\n";

			r [3] = Trigger_toCode + getMezanixDiamondPhysics_toCode + getColl_toCode +
				InitBoolListValueForTrigger_toCode;
			r [4] = "";

			return r;
		}

		/// <summary>
		/// Adapts the oncontact.
		/// </summary>
		void AdaptOncontact ()
		{
			switch (contact)
			{
			case "Trigger":
				Trigger_Head ();
				break;

			case "Collision":
				Collision_Head ();
				break;
			}
		}

		string [] contacts = new string[1];
		public string contact = "Trigger";


		void Init_contacts ()
		{
			contacts = new string[]
			{
				"Trigger",

				"Collision",
			};
		}
		void SetThisTo_contact (object s)
		{
			contact = s.ToString ();
		}
		void Draw_contact_ListMenu ()
		{
			Init_contacts ();
			DrawLogicNodeLabel ("Do", 0, 2);


			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			if (GUI.Button (suitRect, contact))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < contacts.Length; i++)
				{
					if (string.IsNullOrEmpty (contacts [i]))
						continue;

					menu.AddItem (new GUIContent (contacts [i]), false, 
						SetThisTo_contact, contacts [i]);
				}

				menu.ShowAsContext ();
			}

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					contact);

			AdaptOncontact ();
		}
		#endregion contactStringEnum


		#region move_StringEnum
		Rigidbody rb = null;
		const string rb_toCode = "\t\tRigidbody rb = null;\n";
		bool GetRb ()
		{
			if (rb == null)
			{
				if (gameObjectValues [0] == null)
					return false;

				rb = gameObjectValues [0].GetComponent <Rigidbody> ();

				if (rb == null)
					return false;

				return true;
			}

			return true;
		}
		const string getRb_toCode = "\t\tbool GetRb ()\n\t\t{\n\t\t\tif (rb == null)\n\t\t\t{\n\t\t\t\tif (gameObjectValues [0] == null)\n\t\t\t\t\treturn false;\n\n\t\t\t\trb = gameObjectValues [0].GetComponent <Rigidbody> ();\n\n\t\t\t\tif (rb == null)\n\t\t\t\t\treturn false;\n\n\t\t\t\treturn true;\n\t\t\t}\n\n\t\t\treturn true;\n\t\t}\n";

		ScriptsCreatedByDiamond.MezanixDiamondPhysics mezanixDiamondPhysics = null;
		const string mezanixDiamondPhysics_toCode = "\t\tMezanixDiamondPhysics mezanixDiamondPhysics = null;\n\n";
		bool GetMezanixDiamondPhysics ()
		{
			if (mezanixDiamondPhysics == null)
			{
				if (gameObjectValues [0] == null)
					return false;

				mezanixDiamondPhysics = 
					gameObjectValues [0].GetComponent <ScriptsCreatedByDiamond.MezanixDiamondPhysics>();

				if (mezanixDiamondPhysics == null)
				{
					mezanixDiamondPhysics = 
						gameObjectValues [0].AddComponent <ScriptsCreatedByDiamond.MezanixDiamondPhysics>();

					if (mezanixDiamondPhysics == null)
						return false;

					return true;
				}

				return true;
			}

			return true;
		}
		const string getMezanixDiamondPhysics_toCode = "\t\tbool GetMezanixDiamondPhysics ()\n\t\t{\n\t\t\tif (mezanixDiamondPhysics == null)\n\t\t\t{\n\t\t\t\tif (gameObjectValues [0] == null)\n\t\t\t\t\treturn false;\n\n\t\t\t\tmezanixDiamondPhysics = \n\t\t\t\t\tgameObjectValues [0].GetComponent <ScriptsCreatedByDiamond.MezanixDiamondPhysics>();\n\n\t\t\t\tif (mezanixDiamondPhysics == null)\n\t\t\t\t{\n\t\t\t\t\tmezanixDiamondPhysics = \n\t\t\t\t\t\tgameObjectValues [0].AddComponent <ScriptsCreatedByDiamond.MezanixDiamondPhysics>();\n\n\t\t\t\t\tif (mezanixDiamondPhysics == null)\n\t\t\t\t\t\treturn false;\n\n\t\t\t\t\treturn true;\n\t\t\t\t}\n\n\t\t\t\treturn true;\n\t\t\t}\n\n\t\t\treturn true;\n\t\t}\n";

		float countForFixedDeltaTime = 0f;
		bool CountForFixedDeltaTime ()
		{
			countForFixedDeltaTime += Time.deltaTime;

			if (countForFixedDeltaTime >= Time.fixedDeltaTime)
			{
				countForFixedDeltaTime = 0f;

				return true;
			}

			return false;
		}
		const string CountForFixedDeltaTime_toCode = "\t\tfloat countForFixedDeltaTime = 0f;\n\t\tbool CountForFixedDeltaTime ()\n\t\t{\n\t\t\tcountForFixedDeltaTime += Time.deltaTime;\n\n\t\t\tif (countForFixedDeltaTime >= Time.fixedDeltaTime)\n\t\t\t{\n\t\t\t\tcountForFixedDeltaTime = 0f;\n\n\t\t\t\treturn true;\n\t\t\t}\n\n\t\t\treturn false;\n\t\t}\n";

		void SetVelocity_Head ()
		{
			SetVelocity_Input ();
			if (logic.playing) SetVelocity ();
			SetVelocity_Output ();
		}
		void SetVelocity_Input ()
		{
			DrawLogicNodeLabel ("Gameobject", 0, 2);
			DrawGameObjectFieldInput (0, 1, 2);
			if (gameObjectValues [0] == null)
			{
				DrawInNodeInfo ("Fill In Gameobject field");
				return;
			}
			rb = gameObjectValues [0].GetComponent <Rigidbody> ();
			if (rb == null)
			{
				DrawInNodeInfo ("Add Rigidbody component to Gameobject");
				return;
			}

			DrawLogicNodeLabel ("Velocity");
			DrawVector3InputField (0);

			DrawDoItButton ();
		}
		void SetVelocity ()
		{
			if ( ! doIT)
			{
				if (mezanixDiamondPhysics != null)
					mezanixDiamondPhysics.setVelocity = false;

				return;
			}

			if ( ! GetRb ())
				return;

			if ( ! GetMezanixDiamondPhysics ())
				return;

			mezanixDiamondPhysics.rb = rb;
			mezanixDiamondPhysics.velocity = vector3Values [0];
			mezanixDiamondPhysics.setVelocity = true;

			gameObjectValue = gameObjectValues [0];
		}
		const string SetVelocity_toCode = "\t\tvoid SetVelocity ()\n\t\t{\n\t\t\tif ( ! doIT)\n\t\t\t{\n\t\t\t\tif (mezanixDiamondPhysics != null)\n\t\t\t\t\tmezanixDiamondPhysics.setVelocity = false;\n\n\t\t\t\treturn;\n\t\t\t}\n\n\t\t\tif ( ! GetRb ())\n\t\t\t\treturn;\n\n\t\t\tif ( ! GetMezanixDiamondPhysics ())\n\t\t\t\treturn;\n\n\t\t\tmezanixDiamondPhysics.rb = rb;\n\t\t\tmezanixDiamondPhysics.velocity = vector3Values [0];\n\t\t\tmezanixDiamondPhysics.setVelocity = true;\n\n\t\t\tgameObjectValue = gameObjectValues [0];\n\t\t}\n";
		void SetVelocity_Output ()
		{			
			DrawGameObjectResultField (ObjectResultDrawChoice.itsName);
		}
		string [] SetVelocity_GetCodeFrom ()
		{
			string [] r = new string[5];

			r [0] = ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.gameObjectValues + 
				rb_toCode + ExprWs.Gv.vector3Values + 
				mezanixDiamondPhysics_toCode;

			r [1] = ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}) +
				ExprWs.ConstructorExpr.Vector3Values (this);

			r [2] = "\t\t\tSetVelocity ();\n";

			r [3] = SetVelocity_toCode + getMezanixDiamondPhysics_toCode + getRb_toCode;
			r [4] = "";


			return r;
		}

		void AddForce_Head ()
		{
			AddForce_Input ();
			if (logic.playing) AddForce ();
			AddForce_Output ();
		}
		void AddForce_Input ()
		{
			DrawLogicNodeLabel ("Gameobject", 0, 2);
			DrawGameObjectFieldInput (0, 1, 2);
			if (gameObjectValues [0] == null)
			{
				DrawInNodeInfo ("Fill In Gameobject field");
				return;
			}
			rb = gameObjectValues [0].GetComponent <Rigidbody> ();
			if (rb == null)
			{
				DrawInNodeInfo ("Add Rigidbody component to Gameobject");
				return;
			}

			DrawLogicNodeLabel ("Force Mode", 0, 2);
			forceMode = (ForceMode)DrawEnum (forceMode, FieldDrawType.label, 1, 2);

			DrawLogicNodeLabel ("Force");
			DrawVector3InputField (0);

			DrawDoItButton ();
		}
		void AddForce ()
		{
			if ( ! doIT)
			{
				if (mezanixDiamondPhysics != null)
					mezanixDiamondPhysics.addForce = false;

				return;
			}

			if ( ! GetRb ())
				return;

			if ( ! GetMezanixDiamondPhysics ())
				return;

			mezanixDiamondPhysics.rb = rb;
			mezanixDiamondPhysics.force = vector3Values [0];
			mezanixDiamondPhysics.forceMode = forceMode;
			mezanixDiamondPhysics.addForce = true;

			gameObjectValue = gameObjectValues [0];
		}
		const string AddForce_toCode = "\t\tvoid AddForce ()\n\t\t{\n\t\t\tif ( ! doIT)\n\t\t\t{\n\t\t\t\tif (mezanixDiamondPhysics != null)\n\t\t\t\t\tmezanixDiamondPhysics.addForce = false;\n\n\t\t\t\treturn;\n\t\t\t}\n\n\t\t\tif ( ! GetRb ())\n\t\t\t\treturn;\n\n\t\t\tif ( ! GetMezanixDiamondPhysics ())\n\t\t\t\treturn;\n\n\t\t\tmezanixDiamondPhysics.rb = rb;\n\t\t\tmezanixDiamondPhysics.force = vector3Values [0];\n\t\t\tmezanixDiamondPhysics.forceMode = forceMode;\n\t\t\tmezanixDiamondPhysics.addForce = true;\n\n\t\t\tgameObjectValue = gameObjectValues [0];\n\t\t}\n";
		void AddForce_Output ()
		{			
			DrawGameObjectResultField (ObjectResultDrawChoice.itsName);
		}
		string [] AddForce_GetCodeFrom ()
		{
			string [] r = new string[5];

			r [0] = ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.gameObjectValues + 
				rb_toCode + ExprWs.Gv.vector3Values + ExprWs.Gv.forceMode + 
				mezanixDiamondPhysics_toCode;

			r [1] = ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}) +
				ExprWs.ConstructorExpr.Vector3Values (this) + ExprWs.ConstructorExpr.ForceMode (this);

			r [2] = "\t\t\tAddForce ();\n";

			r [3] = AddForce_toCode + getMezanixDiamondPhysics_toCode + getRb_toCode;
			r [4] = "";


			return r;
		}

		/// <summary>
		/// Adapts the on move.
		/// </summary>
		void AdaptOnMove ()
		{
			switch (move)
			{
			case "Set Velocity":
				SetVelocity_Head ();
				break;

			case "Add Force":
				AddForce_Head ();
				break;
			}
		}

		string [] moves = new string[1];
		public string move = "Set Velocity";


		void Init_moves ()
		{
			moves = new string[]
			{
				"Set Velocity",

				"Add Force",
			};
		}
		void SetThisTo_move (object s)
		{
			move = s.ToString ();
		}
		void Draw_move_ListMenu ()
		{
			Init_moves ();
			DrawLogicNodeLabel ("Move", 0, 2);

			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			if (GUI.Button (suitRect, move))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < moves.Length; i++)
				{
					if (string.IsNullOrEmpty (moves [i]))
						continue;

					menu.AddItem (new GUIContent (moves [i]), false, 
						SetThisTo_move, moves [i]);
				}

				menu.ShowAsContext ();
			}

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					move);

			AdaptOnMove ();
		}
		#endregion moveStringEnum


		#region physicsType_StringEnum
		void AdaptOnphysicsType ()
		{
			switch (physicsType)
			{
			case "Move":
				Draw_move_ListMenu ();
				break;

			case "Contact":
				Draw_contact_ListMenu ();
				break;
			}
		}

		string [] physicsTypes = new string[1];
		public string physicsType = "Move";


		void Init_physicsTypes ()
		{
			physicsTypes = new string[]
			{
				"Move",

				"Contact",
			};
		}
		void SetThisTo_physicsType (object s)
		{
			physicsType = s.ToString ();
		}
		void Draw_physicsType_ListMenu ()
		{
			Init_physicsTypes ();
			DrawLogicNodeLabel ("Do", 0, 2);


			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			if (GUI.Button (suitRect, physicsType))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < physicsTypes.Length; i++)
				{
					if (string.IsNullOrEmpty (physicsTypes [i]))
						continue;

					menu.AddItem (new GUIContent (physicsTypes [i]), false, 
						SetThisTo_physicsType, physicsTypes [i]);
				}

				menu.ShowAsContext ();
			}

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					physicsType);

			AdaptOnphysicsType ();
		}
		#endregion physicsTypeStringEnum


		#region highLevelNode_StringEnum
		string [] highLevelNodes = new string[1];
		public string highLevelNode = "Physics";


		void Init_highLevelNodes ()
		{
			highLevelNodes = new string[]
			{
				"UI",

				"Physics",

				"Inputs",
			};
		}
		void SetThisTo_highLevelNode (object s)
		{
			highLevelNode = s.ToString ();
		}
		void Draw_highLevelNode_ListMenu (string [] menuNames, int column, int totalColumns)
		{
			Init_highLevelNodes ();

			Rect suitRect = GetSuitableRect (FieldDrawType.label, column, totalColumns);

			if (GUI.Button (suitRect, highLevelNode))
			{
				GenericMenu menu = new GenericMenu ();

				for (int i = 0; i < menuNames.Length; i++)
				{
					if (string.IsNullOrEmpty (menuNames [i]))
						continue;

					menu.AddItem (new GUIContent (menuNames [i]), false, 
						SetThisTo_highLevelNode, menuNames [i]);
				}

				menu.ShowAsContext ();
			}

			if (suitRect.Contains (eGlobal.mousePosition))
				DrawFloatingMessage (
					new Rect (eGlobal.mousePosition + new Vector2 (15f, -10f), Vector2.one), 
					highLevelNode);
		}
		#endregion highLevelNodeStringEnum

	}
}
