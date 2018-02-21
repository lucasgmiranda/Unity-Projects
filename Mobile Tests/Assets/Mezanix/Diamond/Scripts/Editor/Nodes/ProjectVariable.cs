using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{
	/// <summary>
	/// Project variable.
	/// A project variable is a static variable in your project.
	/// A variable related to your whole project.
	/// It can be useful to communicate between different logics.
	/// </summary>
	public class ProjectVariable : ScriptableObject
	{
		#region global variables
		ProjectVariables projectVariables = null;

		public static Logic logic = null;

		static int globalIndex = 0;

		public static int cycle = 3;

		public string uniqueID = "";

		public Rect rect = new Rect ();

		public Rect getRect = new Rect();

		Rect setRect = new Rect();

		Rect setRectOptions = new Rect ();

		Rect getRectToggle = new Rect();

		Rect setRectToggle = new Rect();

		public bool setPermitted = true;

		public string setPermissionInputSource = "";

		Rect nameRect = new Rect ();

		Rect fieldRect = new Rect ();

		Rect removeRect = new Rect (); 


		bool renaming = false;

		string myNameTmp = "";
		public string myName = "";

		public bool getState = false;

		public bool setState = false;

		//public bool getLinked = false;

		public string setSource = "";

		public bool setCalling = false;

		public bool getSending = false;

		public VariableTypeForProject variableTypeForProject;


		public bool boolValue = false;

		public float floatValue = 1f;

		public int intValue = 0;

		public string stringValue = "";


		public Vector2 vector2Value = new Vector2 ();

		public Vector3 vector3Value = new Vector3 ();

		public Vector4 vector4Value = new Vector4 ();

		public GameObject gameObjectValue = null;
		#endregion global variables

		#region constructor
		float PositonIndex ()
		{
			int indexInList = projectVariables.projectVariablesToShow.IndexOf (this);

			int r = globalIndex + indexInList + 1;

			cycle = Mathf.Max (projectVariables.projectVariablesToShow.Count, 1);

			r = r % cycle + 1;

			return (float)r;
		}

		void CreateRect (Vector2 panelPosition, bool withGui)
		{
			rect = new Rect (
				new Vector2 (panelPosition.x, panelPosition.y + 52f*PositonIndex ()), 
				new Vector2 (250f, 50f));

			if (withGui)
				GUI.Box (rect, "", GetGuiStyle (Skins.node));



			nameRect = new Rect(Vector2.zero, new Vector2 (0.75f, 0.33f));

			fieldRect = nameRect;

			getRectToggle = new Rect(Vector2.zero, new Vector2 (0.065f, nameRect.height));

			setRectToggle = getRectToggle;

			setRect = getRectToggle;

			setRectOptions = setRect;

			getRect = getRectToggle;

			Vector2 gaps = new Vector2 (
				(1f - (nameRect.width + getRectToggle.width + setRectOptions.width + getRect.width)) / 5f,
				(1f - (nameRect.height + fieldRect.height)) / 3f);


			nameRect.position = gaps;

			getRectToggle.position = nameRect.position + new Vector2 (nameRect.width + gaps.x, 0f);

			getRect.position = getRectToggle.position + new Vector2 (
				getRectToggle.width + gaps.x + setRectOptions.width + gaps.x, 0f);

			fieldRect.position = nameRect.position + new Vector2 (0f, nameRect.height + gaps.y);


			setRectToggle.position = fieldRect.position + new Vector2 (fieldRect.width + gaps.x, 0f);

			setRectOptions.position = setRectToggle.position + new Vector2 (
				setRectToggle.width + gaps.x, 0f);

			setRect.position = setRectToggle.position + new Vector2 (
				setRectToggle.width + gaps.x + setRectOptions.width + gaps.x, 0f);


			nameRect = RectOperations.RatioToAbsolute (rect, nameRect);

			getRectToggle = RectOperations.RatioToAbsolute (rect, getRectToggle);

			getRect = RectOperations.RatioToAbsolute (rect, getRect);

			fieldRect = RectOperations.RatioToAbsolute (rect, fieldRect);

			setRectToggle = RectOperations.RatioToAbsolute (rect, setRectToggle);

			setRect = RectOperations.RatioToAbsolute (rect, setRect);

			setRectOptions = RectOperations.RatioToAbsolute (rect, setRectOptions);

			removeRect = new Rect (rect.position + new Vector2 (rect.width, 0f), setRect.size*0.8f);
		}

		void GetProjectVariables (bool simple)
		{
			if (simple)
			if (projectVariables != null)
				return;

			projectVariables = Diamond.projectVariables;

			if ( ! simple)
			{
				if (projectVariables != null)
				{
					projectVariables.AddProjectVariable (this);

					AssetDatabase.AddObjectToAsset (this, projectVariables);

					Auxiliaries.SaveAndRefreshAssetsForced ();
				}
			}
		}

		void ConstructorCommon (string setName, bool setGetState, bool setSetState, Vector2 panelPosition,
			Logic setLogic)
		{
			uniqueID = "v" + DatesTimesAndFrequences.DateTimeNow ();

			SetNewName (setName);

			getState = setGetState;

			setState = setSetState;

			GetProjectVariables (false);

			logic = setLogic;

			CreateRect (panelPosition, false);
		}

		public void Init (string setName, bool setGetState, bool setSetState, bool setValue, 
			Vector2 panelPosition, Logic setLogic)
		{
			variableTypeForProject = VariableTypeForProject.Bool;

			boolValue = setValue;

			ConstructorCommon (setName, setGetState, setSetState, panelPosition, setLogic);
		}

		public void Init (string setName, bool setGetState, bool setSetState, float setValue, 
			Vector2 panelPosition, Logic setLogic)
		{
			variableTypeForProject = VariableTypeForProject.Float;

			floatValue = setValue;

			ConstructorCommon (setName, setGetState, setSetState, panelPosition, setLogic);
		}

		public void Init (string setName, bool setGetState, bool setSetState, int setValue,
			Vector2 panelPosition, Logic setLogic)
		{
			variableTypeForProject = VariableTypeForProject.Int;

			intValue = setValue;

			ConstructorCommon (setName, setGetState, setSetState, panelPosition, setLogic);
		}

		public void Init (string setName, bool setGetState, bool setSetState, string setValue, 
			Vector2 panelPosition, Logic setLogic)
		{
			variableTypeForProject = VariableTypeForProject.String;

			stringValue = setValue;

			ConstructorCommon (setName, setGetState, setSetState, panelPosition, setLogic);
		}


		public void Init (string setName, bool setGetState, bool setSetState, Vector2 setValue, 
			Vector2 panelPosition, Logic setLogic)
		{
			variableTypeForProject = VariableTypeForProject.Vector2;

			vector2Value = setValue;

			ConstructorCommon (setName, setGetState, setSetState, panelPosition, setLogic);
		}

		public void Init (string setName, bool setGetState, bool setSetState, Vector3 setValue, 
			Vector2 panelPosition, Logic setLogic)
		{
			variableTypeForProject = VariableTypeForProject.Vector3;

			vector3Value = setValue;

			ConstructorCommon (setName, setGetState, setSetState, panelPosition, setLogic);
		}

		public void Init (string setName, bool setGetState, bool setSetState, Vector4 setValue,
			Vector2 panelPosition, Logic setLogic)
		{
			variableTypeForProject = VariableTypeForProject.Vector4;

			vector4Value = setValue;

			ConstructorCommon (setName, setGetState, setSetState, panelPosition, setLogic);
		}

		public void Init (string setName, bool setGetState, bool setSetState, 
			VariableTypeForProject setVariableTypeForProject,
			Vector2 panelPosition, Logic setLogic)
		{
			variableTypeForProject = setVariableTypeForProject;

			gameObjectValue = null;

			ConstructorCommon (setName, setGetState, setSetState, panelPosition, setLogic);
		}
		#endregion constructor
	
		#region static

		#endregion static


		#region update
		public static void ChangeGlobalIndex (bool up)
		{
			if (up)
			{
				globalIndex--;

				//globalIndex = Mathf.Max (globalIndex, 0);

				if (globalIndex < 0)
					globalIndex = Diamond.projectVariables.projectVariables.Count-1;

				return;
			}

			globalIndex++;
		}

		Event eGlobal;
		public void UpdateProjectVariable (Event e, Rect projectVariablesRect)
		{
			//ScriptsCreatedByDiamond.ProjectVariables.projectVariables.Add (new ScriptsCreatedByDiamond.ProjectVariable ());

			eGlobal = e;

			GetProjectVariables (true);

			DrawElements (projectVariablesRect);



			OnGetClick ();

			OnSetClick ();

			DiactivateSendingCalling ();

			OnSetRectContains ();

			OnGetRectContains ();


			DrawCurrentLink ();

			DrawEstablishedLinks ();
		}

		void DrawElements (Rect projectVariablesRect)
		{		
			CreateRect (projectVariablesRect.position, true);


			DrawNameField ();

			DrawField ();


			DrawGetToggle ();

			DrawSetToggle ();

			DrawSetOptions ();

			DrawGet ();

			DrawSet ();


			DrawOptionsButton ();
		}

		void DrawOptionsButton ()
		{
			if (GUI.Button (removeRect, "", GetGuiStyle (Skins.asterix)) && eGlobal.button == 0)
			{
				GenericMenu menu = new GenericMenu ();

				menu.AddItem (new GUIContent ("Rename"), false,
					Rename);

				menu.AddItem (new GUIContent ("Remove"), false,
					Remove);

				menu.ShowAsContext ();
			}
		}

		void Rename ()
		{
			myNameTmp = myName;

			renaming = true;
		}

		void Remove ()
		{
			if (EditorUtility.DisplayDialog ("Delete Project Variable",
				"Are you sure you want to delete this Project Variable? \n" +
				"\n" +
				"It is an irreversible action !",
				"Yes", "no"))
			{
				if (renaming)
					Diamond.namesToSave.projectVariablesNames.Remove (myNameTmp);
				else
					Diamond.namesToSave.projectVariablesNames.Remove (myName);

				projectVariables.RemoveProjectVariable (this);

				Auxiliaries.SaveAndRefreshAssetsForced ();
			}
		}

		void DrawGet ()
		{
			if (getState)
			{
				GUI.Box (getRect, "", GetGuiStyle (Skins.get));
			}
			else
			{
				GUI.Box (getRect, "", GetGuiStyle (Skins.getInactive));
			}
		}

		void DrawSet ()
		{
			if (setState)
			{
				GUI.Box (setRect, "", GetGuiStyle (Skins.set));

				DrawSetPermissionButton (setRect);
			}
			else
			{
				GUI.Box (setRect, "", GetGuiStyle (Skins.setInactive));
			}
		}

		const string blockThisInput = "Block this input";

		const string allowThisInput = "Allow this input";

		const string breackTheLink = "Break the link";

		void DrawSetPermissionButton (Rect setRect)
		{
			Rect setPermissionRect = new Rect (setRect.position + new Vector2 (setRect.width, 0f), setRect.size);

			string skinPermission = setPermitted? Skins.in_out: Skins.cut;

			if (GUI.Button (setPermissionRect, "", GetGuiStyle (skinPermission)) && eGlobal.button == 0)
			{
				GenericMenu menu = new GenericMenu ();

				string menuString = setPermitted? blockThisInput: allowThisInput;

				menu.AddItem (new GUIContent (menuString), false, AllowBlockInput);

				if ( ! string.IsNullOrEmpty (setPermissionInputSource))
					menu.AddItem (new GUIContent (breackTheLink), false, BreakInputPermissionInputLink);

				menu.ShowAsContext ();
			}
	

			SetPermissionButton_AssignInput (setPermissionRect);

			SetPermissionButton_DrawLink (setPermissionRect);

			SetPermission_LinkAction ();
		}

		void SetPermission_LinkAction ()
		{
			if (string.IsNullOrEmpty (setPermissionInputSource))
				return;
			
			LogicNode sourceLogicNode = InOutAdressToLinkToLogicNode (setPermissionInputSource);

			if (sourceLogicNode == null)
				return;

			if ( ! sourceLogicNode.activeOutputs [sourceLogicNode.IndexOfOutID (
				sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setPermissionInputSource))])
			{
				return;
			}

			int lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (setPermissionInputSource);

			switch (sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setPermissionInputSource))
			{
			case Enums.boolValue_ID:
				setPermitted = sourceLogicNode.boolValue;
				break;

			case Enums.boolValues_0_ID:
				setPermitted = sourceLogicNode.boolValues [0];
				break;

			case Enums.boolValues_1_ID:
				setPermitted = sourceLogicNode.boolValues [1];
				break;

			case Enums.m44ValueIsIdentity_ID:
				setPermitted = sourceLogicNode.m44ValueIsIdentity;
				break;

			case Enums.m44ValueInvertible_ID:
				setPermitted = sourceLogicNode.m44ValueInvertible;
				break;


			case Enums.OffMeshLinkData_activated_ID:
				setPermitted= sourceLogicNode.OffMeshLinkData_activated;
				break;

			case Enums.OffMeshLinkData_valid_ID:
				setPermitted = sourceLogicNode.OffMeshLinkData_valid;
				break;

			case Enums.NavMeshHit_hit_ID:
				setPermitted = sourceLogicNode.NavMeshHit_hit;
				break;

			}

			switch (sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setPermissionInputSource, -1))
			{
			case Enums.boolsList_ID:
				if (lstIndex >-1 && lstIndex < sourceLogicNode.boolsListValue.Count)
				{
					setPermitted = sourceLogicNode.boolsListValue [lstIndex];
				}
				break;
			}
		}

		void SetPermissionButton_DrawLink (Rect setPermissionRect)
		{
			if (string.IsNullOrEmpty (setPermissionInputSource))
				return;
			
			LogicNode sourceLogicNode = InOutAdressToLinkToLogicNode (setPermissionInputSource);

			if (sourceLogicNode == null)
			{
				setPermissionInputSource = "";

				return;
			}

			if ( ! sourceLogicNode.activeOutputs [sourceLogicNode.IndexOfOutID (
				sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setPermissionInputSource))])
			{
				setPermissionInputSource = "";

				return;
			}
		
			int facingLogicNodeVariableIndex = FacingLogicNodeVariableIndex (
				sourceLogicNode, setPermissionInputSource);

			if (facingLogicNodeVariableIndex == -1)
				return;

			if ( ! sourceLogicNode.activeOutputs [facingLogicNodeVariableIndex])
				return;



			if ( ! sourceLogicNode.IsAListOutSource (setPermissionInputSource))
			{
				DrawLink_ForSetPermission (
					setPermissionRect.center + gapPermissionLinkDraw * new Vector2 (0f, setPermissionRect.height), 
					sourceLogicNode.outputsRects [facingLogicNodeVariableIndex].center + 
					gapPermissionLinkDraw * new Vector2 (0f, sourceLogicNode.outputsRects [facingLogicNodeVariableIndex].height));
			}
			else if (sourceLogicNode.IsAListOutSource (setPermissionInputSource))
			{
				//int listIndexFinal = sourceLogicNode.InoutAdressCurrentToLinkToListIndex ();

				string sourceNodeOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setPermissionInputSource, 0);

				int sourceNodeOutIndex = sourceLogicNode.IndexOfOutID (sourceNodeOutID);

				if (sourceNodeOutIndex < 0 || sourceNodeOutIndex > sourceLogicNode.activeOutputs.Length-1)
					return;

				if ( ! sourceLogicNode.activeOutputs [sourceNodeOutIndex])
					return;

				if (sourceLogicNode.InoutAdressToLinkToListIndex (setPermissionInputSource) > -1 && 
					sourceLogicNode.InoutAdressToLinkToListIndex (setPermissionInputSource) < sourceLogicNode.outputListCounts [sourceNodeOutIndex])
				{
					DrawLink_ForSetPermission (						 
						setPermissionRect.center + gapPermissionLinkDraw * new Vector2 (0f, setPermissionRect.height),
						sourceLogicNode.GetDecaledListRect (sourceLogicNode, sourceNodeOutIndex, setPermissionInputSource).center +
						gapPermissionLinkDraw * new Vector2 (0f, 
							sourceLogicNode.GetDecaledListRect (sourceLogicNode, sourceNodeOutIndex, setPermissionInputSource).height));
				}
			}
			
		}

		const float gapPermissionLinkDraw = 0.25f;

		void SetPermissionButton_AssignInput (Rect setPermissionRect)
		{
			if ( ! setState)
				return;

			bool contains = false;

			if (setPermissionRect.Contains (eGlobal.mousePosition))
			{
				contains = true;
			}

			if ( ! contains)
				return;

			if (string.IsNullOrEmpty (logic.inOutAdressCurrentToLink))
				return;

			if (IsAProjectVariableAdress (logic.inOutAdressCurrentToLink))
				return;

			if ( ! InOutAdressToLinkToIsOutput (logic.inOutAdressCurrentToLink))
				return;

			LogicNode sourceLogicNode = InOutAdressToLinkToLogicNode (logic.inOutAdressCurrentToLink);

			if (sourceLogicNode == null)
				return;

			if ( ! sourceLogicNode.IsAListOutSource (logic.inOutAdressCurrentToLink))
			{
				if (FacingLogicNodeVariableType (sourceLogicNode, logic.inOutAdressCurrentToLink) != VariableType.Bool)
					return;

				setPermissionInputSource = logic.inOutAdressCurrentToLink;

			}
			else if (sourceLogicNode.IsAListOutSource (logic.inOutAdressCurrentToLink))
			{
				if (FacingLogicNodeVariableType (sourceLogicNode, logic.inOutAdressCurrentToLink) != VariableType.boolsList)
					return;

				setPermissionInputSource = logic.inOutAdressCurrentToLink;

				sourceLogicNode.SetOutputLinkedIndexToDestination ();
			}
		}

		void AllowBlockInput()
		{
			setPermitted = ! setPermitted;
		}

		void BreakInputPermissionInputLink ()
		{
			setPermissionInputSource = "";
		}



		void DrawGetToggle ()
		{
			getState = EditorGUI.Toggle (getRectToggle, getState);
		}

		void DrawSetOptions ()
		{
			if (GUI.Button (setRectOptions, "", GetGuiStyle (Skins.in_out)) && eGlobal.button == 0)
			{
				GenericMenu menu = new GenericMenu ();

				if ( ! string.IsNullOrEmpty (setSource))
					menu.AddItem (new GUIContent (breackTheLink), false, BreakSetLink);

				menu.ShowAsContext ();
			}
		}

		void BreakSetLink ()
		{
			setSource = "";
		}

		void DrawSetToggle ()
		{
			setState = EditorGUI.Toggle (setRectToggle, setState);
		}

		void DrawField ()
		{
			switch (variableTypeForProject)
			{
			case VariableTypeForProject.Bool:
				boolValue = EditorGUI.Toggle (fieldRect, boolValue);
				break;

			case VariableTypeForProject.Float:
				floatValue = EditorGUI.FloatField (fieldRect, floatValue);
				break;

			case VariableTypeForProject.Int:
				intValue = EditorGUI.IntField (fieldRect, intValue);
				break;

			case VariableTypeForProject.String:
				stringValue = EditorGUI.TextField (fieldRect, stringValue);
				break;


			case VariableTypeForProject.Vector2:
				vector2Value = EditorGUI.Vector2Field (fieldRect, "", vector2Value);
				break;

			case VariableTypeForProject.Vector3:
				vector3Value = EditorGUI.Vector3Field (fieldRect, "", vector3Value);
				break;

			case VariableTypeForProject.Vector4:
				vector4Value = EditorGUI.Vector4Field (fieldRect, "", vector4Value);
				break;

			case VariableTypeForProject.GameObject:
				//EditorGUI.ObjectField (fieldRect, gameObjectValue, typeof (GameObject), true);

				if (gameObjectValue == null)
				{
					EditorGUI.LabelField (fieldRect, "none game object", GetGuiStyle (Skins.logicNodeResult));
				}
				else
				{
					EditorGUI.LabelField (fieldRect, gameObjectValue.name, GetGuiStyle (Skins.logicNodeResult));
				}
				break;
			}
		}

		void DrawNameField ()
		{
			if (renaming)
			{
				DrawNameField_Renaming ();

				return;
			}

			EditorGUI.LabelField (nameRect, myName, GetGuiStyle (Skins.projectVariableName));
		}

		void DrawNameField_Renaming ()
		{
			myName = EditorGUI.TextField (nameRect, myName);

			if (eGlobal.keyCode == KeyCode.Return)
			{
				if (eGlobal.type == EventType.KeyUp)
				{
					SetNewName (myName);
				}
			}
		}

		void SetNewName (string newName)
		{
			newName = StringTreatment.ScriptName (newName);

			if (myNameTmp == newName)
			{
				renaming = false;
				return;
			}
			
			if (Diamond.namesToSave.projectVariablesNames.Contains (newName))
			{
				EditorUtility.DisplayDialog ("Names", "" + newName + " already exist as a project variable name, " +
					"choose an another name.", "Ok"); 

				return;
			}

			Diamond.namesToSave.projectVariablesNames.Remove (myNameTmp);
			myNameTmp = "";

			myName = newName;

			Diamond.namesToSave.projectVariablesNames.Add (newName);

			renaming = false;

			Auxiliaries.SaveAndRefreshAssetsForced ();
		}
		#endregion update

		#region links
		const char sep = LogicNode.inOutAdressSeparator;
		const string outSig = LogicNode.outAdressSignature;
		const string inSig = LogicNode.inAdressSignature;

		void SetInOutAdressCurrentToLink (bool isAnInput)
		{
			string inOutSig = isAnInput? inSig: outSig;

			logic.inOutAdressCurrentToLink = uniqueID + sep + variableTypeForProject.ToString () + sep + inOutSig;
		}


		void OnGetClick ()
		{
			if ( ! getState)
				return;

			if (getRect.Contains (eGlobal.mousePosition))
			{
				if (eGlobal.button == 0)
				{
					if (eGlobal.type == EventType.MouseDown)
					{
						SetInOutAdressCurrentToLink (false);

						eGlobal.type = EventType.Ignore;

						getSending = true;

						setCalling = false;
					}
				}
			}
		}

		void OnSetClick ()
		{
			if ( ! setState)
				return;

			if (setRect.Contains (eGlobal.mousePosition))
			{
				if (eGlobal.button == 0)
				{
					if (eGlobal.type == EventType.MouseDown)
					{
						SetInOutAdressCurrentToLink (true);

						eGlobal.type = EventType.Ignore;

						setCalling = true;

						getSending = false;
					}
				}
			}
		}


		void OnSetRectContains ()
		{
			if ( ! setState)
				return;

			if (string.IsNullOrEmpty (logic.inOutAdressCurrentToLink))
				return;

			bool contains = false;

			if (setRect.Contains (eGlobal.mousePosition))
			{
				contains = true;
			}

			if ( ! contains)
				return;



			if (IsAProjectVariableAdress (logic.inOutAdressCurrentToLink))
				return;

			if ( ! InOutAdressToLinkToIsOutput (logic.inOutAdressCurrentToLink))
				return;

			LogicNode sourceLogicNode = InOutAdressToLinkToLogicNode (logic.inOutAdressCurrentToLink);

			if ( ! sourceLogicNode.IsAListOutSource (logic.inOutAdressCurrentToLink))
			{
				if (FacingLogicNodeVariableType (sourceLogicNode, logic.inOutAdressCurrentToLink) != 
					CorrespondingVariableType ())
					return;

				setSource = logic.inOutAdressCurrentToLink;

			}
			else if (sourceLogicNode.IsAListOutSource (logic.inOutAdressCurrentToLink))
			{
				if (FacingLogicNodeVariableType (sourceLogicNode, logic.inOutAdressCurrentToLink) != 
					CorrespondingVariableType_ToTypeList ())
					return;

				setSource = logic.inOutAdressCurrentToLink;

				sourceLogicNode.SetOutputLinkedIndexToDestination ();
			}
		}

		void OnGetRectContains ()
		{
			if ( ! getState)
				return;

			if (string.IsNullOrEmpty (logic.inOutAdressCurrentToLink))
				return;

			bool contains = false;

			if (getRect.Contains (eGlobal.mousePosition))
			{
				contains = true;
			}

			if ( ! contains)
				return;

			if (IsAProjectVariableAdress (logic.inOutAdressCurrentToLink))
				return;

			if (InOutAdressToLinkToIsOutput (logic.inOutAdressCurrentToLink))
				return;

			LogicNode sourceLogicNode = InOutAdressToLinkToLogicNode (logic.inOutAdressCurrentToLink);

			if (FacingLogicNodeVariableType (sourceLogicNode, logic.inOutAdressCurrentToLink) != 
				CorrespondingVariableType ())
				return;

			int facingLogicNodeVariableIndex = FacingLogicNodeVariableIndex (
				sourceLogicNode, logic.inOutAdressCurrentToLink);

			if (facingLogicNodeVariableIndex == -1)
				return;

			sourceLogicNode.inputsSources [facingLogicNodeVariableIndex] = 
				uniqueID +
				sep +
				variableTypeForProject.ToString () +
				sep +
				outSig;
		}


		void DiactivateSendingCallingForced ()
		{
			setCalling = false;

			getSending = false;
		}

		void DiactivateSendingCalling ()
		{
			if (eGlobal.button == 0)
			{
				if (eGlobal.type == EventType.MouseDown)
				{
					setCalling = false;

					getSending = false;
				}
			}
		}


		string InOutAdressToLinkToUniqueID (string s)
		{
			return StringTreatment.BeforeThat (s, sep);
		}

		bool InOutAdressToLinkToVariableTypeMatch (string s, VariableType vt)
		{
			string withoutUniqueID = StringTreatment.SubtractWeak (
				s, InOutAdressToLinkToUniqueID (s) + ".");

			string typeString = StringTreatment.BeforeThat (withoutUniqueID, sep);

			return typeString == vt.ToString ();
		}

		public VariableType CorrespondingVariableType ()
		{
			VariableType r = VariableType.boolsList;

			switch (variableTypeForProject)
			{
			case VariableTypeForProject.Bool:
				r = VariableType.Bool;
				break;

			case VariableTypeForProject.Float:
				r = VariableType.Float;
				break;

			case VariableTypeForProject.Int:
				r = VariableType.Int;
				break;

			case VariableTypeForProject.String:
				r = VariableType.String;
				break;

			case VariableTypeForProject.Vector2:
				r = VariableType.Vector2;
				break;

			case VariableTypeForProject.Vector3:
				r = VariableType.Vector3;
				break;

			case VariableTypeForProject.Vector4:
				r = VariableType.Vector4;
				break;

			case VariableTypeForProject.GameObject:
				r = VariableType.GameObject;
				break;
			}

			return r;
		}

		public VariableType CorrespondingVariableType_ToTypeList ()
		{
			VariableType r = VariableType.boolsList;

			switch (variableTypeForProject)
			{
			case VariableTypeForProject.Bool:
				r = VariableType.boolsList;
				break;

			case VariableTypeForProject.Float:
				r = VariableType.floatsList;
				break;

			case VariableTypeForProject.Int:
				r = VariableType.intsList;
				break;

			case VariableTypeForProject.String:
				r = VariableType.stringsList;
				break;

			case VariableTypeForProject.Vector2:
				r = VariableType.vector2List;
				break;

			case VariableTypeForProject.Vector3:
				r = VariableType.vector3List;
				break;

			case VariableTypeForProject.Vector4:
				r = VariableType.vector4List;
				break;

			case VariableTypeForProject.GameObject:
				r = VariableType.GameObjectList;
				break;
			}

			return r;
		}

		bool InOutAdressToLinkToIsOutput (string s)
		{
			string withoutUniqueID = StringTreatment.SubtractWeak (
				s, InOutAdressToLinkToUniqueID (s) + ".");

			string inOutSigString = StringTreatment.AfterThat (withoutUniqueID, sep);

			return inOutSigString == outSig;
		}
	
		bool IsAProjectVariableAdress (string s)
		{
			if (string.IsNullOrEmpty (s))
				return false;

			return s [0] == 'v';
		}

		LogicNode InOutAdressToLinkToLogicNode (string s)
		{
			string uID = InOutAdressToLinkToUniqueID (s);

			LogicNode r = null;

			for (int i = 0; i < logic.nodes.Count; i++)
			{
				if (logic.nodes [i] == null)
					continue;
				
				if (uID == logic.nodes [i].uniqueID)
				{
					r = logic.nodes [i];

					break;
				}
			}

			return r;
		}

		VariableType FacingLogicNodeVariableType (LogicNode ln, string s)
		{
			VariableType r = VariableType.boolsList;

			string ln_InOutID = ln.InOutAdressCurrentToLinkToInOutID (s);

			bool  ln_Var_isOutput = InOutAdressToLinkToIsOutput (s);

			int ln_var_index = -1;

			if (ln_Var_isOutput)
			{
				ln_var_index = ln.IndexOfOutID (ln_InOutID);

				if (ln_var_index > -1 && ln_var_index < ln.outputsTypes.Length)
				{
					r = ln.outputsTypes [ln_var_index];
				}
			}
			else
			{
				ln_var_index = ln.IndexOfInID (ln_InOutID);

				if (ln_var_index > -1 && ln_var_index < ln.inputsTypes.Length)
				{
					r = ln.inputsTypes [ln_var_index];
				}
			}

			return r;
		}

		int FacingLogicNodeVariableIndex (LogicNode ln, string s)
		{
			if (ln == null)
				return -1;

			string ln_InOutID = ln.InOutAdressCurrentToLinkToInOutID (s);

			bool  ln_Var_isOutput = InOutAdressToLinkToIsOutput (s);

			int ln_var_index = -1;

			if (ln_Var_isOutput)
			{
				ln_var_index = ln.IndexOfOutID (ln_InOutID);
			}
			else
			{
				ln_var_index = ln.IndexOfInID (ln_InOutID);
			}

			return ln_var_index;
		}


		void DrawCurrentLink ()
		{
			if (setCalling)
			{
				DrawLink (setRect.center, eGlobal.mousePosition, true);
			}

			if (getSending)
			{
				DrawLink (getRect.center, eGlobal.mousePosition, false);
			}
		}

		void DrawEstablishedLinks ()
		{
			if ( ! setState)
				return;

			if (string.IsNullOrEmpty(setSource))
				return;

			LogicNode sourceLogicNode = InOutAdressToLinkToLogicNode (setSource);

			if (sourceLogicNode == null)
				return;

			int facingLogicNodeVariableIndex = FacingLogicNodeVariableIndex (
				sourceLogicNode, setSource);

			if (facingLogicNodeVariableIndex == -1)
				return;

			if ( ! sourceLogicNode.activeOutputs [facingLogicNodeVariableIndex])
				return;



			if ( ! sourceLogicNode.IsAListOutSource (setSource))
			{
				if (CorrespondingVariableType () != sourceLogicNode.outputsTypes [facingLogicNodeVariableIndex])
					return;

				DrawLink (setRect.center, sourceLogicNode.outputsRects [facingLogicNodeVariableIndex].center, true);
			}
			else if (sourceLogicNode.IsAListOutSource (setSource))
			{
				if (CorrespondingVariableType_ToTypeList () != sourceLogicNode.outputsTypes [facingLogicNodeVariableIndex])
					return;

				//int listIndexFinal = sourceLogicNode.InoutAdressCurrentToLinkToListIndex ();

				string sourceNodeOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setSource, 0);

				int sourceNodeOutIndex = sourceLogicNode.IndexOfOutID (sourceNodeOutID);

				if (sourceNodeOutIndex < 0 || sourceNodeOutIndex > sourceLogicNode.activeOutputs.Length-1)
					return;

				if ( ! sourceLogicNode.activeOutputs [sourceNodeOutIndex])
					return;

				if (sourceLogicNode.InoutAdressToLinkToListIndex (setSource) > -1 && 
					sourceLogicNode.InoutAdressToLinkToListIndex (setSource) < sourceLogicNode.outputListCounts [sourceNodeOutIndex])
				{
					DrawLink (sourceLogicNode.GetDecaledListRect (sourceLogicNode, sourceNodeOutIndex, setSource).center, 
						setRect.center, true);
				}
			}
			LinkInputAction (sourceLogicNode);
		}

		void DrawLink_ForSetPermission (Vector2 start, Vector2 end)
		{
			if ( ! Showing ())
				return;

			//ColorsArithmetic.RGB_255_To_Normalized (31f, 208f, 235f, 1f);
			//Color linkColor = ColorsArithmetic.RGB_255_To_Normalized (25f, 250f, 235f, 1f);
			Color linkColor = LogicNode.ColorsArithmetic.RGB_255_To_Normalized (255f, 250f, 235f, 1f);

			float tanLength = -100f * 1.2f;

			float linkWidth = 5f;

			Vector2 startTan = Vector2.one;

			Vector2 endTan = Vector2.one;


			startTan = start + new Vector2 (-tanLength, 0f);

			endTan = end + new Vector2 (-tanLength, 0f);

			Handles.DrawBezier (start, end, startTan, endTan, linkColor, null, linkWidth);
		}

		void DrawLink (Vector2 start, Vector2 end, bool forSet)
		{
			if ( ! Showing ())
				return;

			//ColorsArithmetic.RGB_255_To_Normalized (31f, 208f, 235f, 1f);
			Color linkColor = LogicNode.ColorsArithmetic.RGB_255_To_Normalized (25f, 250f, 235f, 1f);

			if ( ! setPermitted)
				linkColor = LogicNode.ColorsArithmetic.RGB_255_To_Normalized (25f, 250f, 235f, 0.5f);

			float tanLength = -100f;

			float linkWidth = 5f;

			Vector2 startTan = Vector2.one;

			Vector2 endTan = Vector2.one;

			if (forSet)
			{
				startTan = start + new Vector2 (-tanLength, 0f);

				endTan = end + new Vector2 (-tanLength, 0f);

				Handles.DrawBezier (start, end, startTan, endTan, linkColor, null, linkWidth);
			}
			else
			{
				tanLength = 30f;

				startTan = start + new Vector2 (tanLength, 0f);

				endTan = end + new Vector2 (-tanLength, 0f);

				Handles.DrawBezier (start, end, startTan, endTan, linkColor, null, linkWidth);
			}
		}

		void LinkInputAction (LogicNode sourceLogicNode)
		{
			if ( ! setPermitted)
				return;

			string ln_InOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setSource);

			int lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (setSource);

			switch (variableTypeForProject)
			{
			case VariableTypeForProject.Bool:
				switch (ln_InOutID)
				{
				case Enums.boolValue_ID:
					boolValue = sourceLogicNode.boolValue;
					break;

				case Enums.boolValues_0_ID:
					boolValue = sourceLogicNode.boolValues [0];
					break;

				case Enums.boolValues_1_ID:
					boolValue = sourceLogicNode.boolValues [1];
					break;


				case Enums.m44ValueIsIdentity_ID:
					boolValue = sourceLogicNode.m44ValueIsIdentity;
					break;

				case Enums.m44ValueInvertible_ID:
					boolValue = sourceLogicNode.m44ValueInvertible;
					break;


				case Enums.OffMeshLinkData_activated_ID:
					boolValue = sourceLogicNode.OffMeshLinkData_activated;
					break;

				case Enums.OffMeshLinkData_valid_ID:
					boolValue = sourceLogicNode.OffMeshLinkData_valid;
					break;

				case Enums.NavMeshHit_hit_ID:
					boolValue = sourceLogicNode.NavMeshHit_hit;
					break;
				}

				ln_InOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setSource, 0);
				switch (ln_InOutID)
				{
				case Enums.boolsList_ID:
					if (lstIndex >-1 && lstIndex < sourceLogicNode.boolsListValue.Count)
					{
						boolValue = sourceLogicNode.boolsListValue [lstIndex];
					}
					break;
				}
				break;

			case VariableTypeForProject.Float:
				switch (ln_InOutID)
				{
				case Enums.touch_altitudeAngle_ID:
					floatValue = sourceLogicNode.touch_altitudeAngle;
					break;

				case Enums.touch_azimuthAngle_ID:
					floatValue = sourceLogicNode.touch_azimuthAngle;
					break;

				case Enums.touch_deltaTime_ID:
					floatValue = sourceLogicNode.touch_deltaTime;
					break;


				case Enums.touch_maximumPossiblePressure_ID:
					floatValue = sourceLogicNode.touch_maximumPossiblePressure;
					break;

				case Enums.touch_pressure_ID:
					floatValue = sourceLogicNode.touch_pressure;
					break;

				case Enums.touch_radius_ID:
					floatValue = sourceLogicNode.touch_radius;
					break;

				case Enums.touch_radiusVariance_ID:
					floatValue = sourceLogicNode.touch_radiusVariance;
					break;
				}

				switch (ln_InOutID)
				{
				case Enums.floatValue_ID:
					floatValue = sourceLogicNode.floatValue;
					break;

				case Enums.floatValues_0_ID:
					floatValue = sourceLogicNode.floatValues [0];
					break;

				case Enums.floatValues_1_ID:
					floatValue = sourceLogicNode.floatValues [1];
					break;

				case Enums.floatValues_2_ID:
					floatValue = sourceLogicNode.floatValues [2];
					break;


				case Enums.raycastHitDistance_ID:
					floatValue = sourceLogicNode.raycastHitDistance;
					break;

				case Enums.m44Value_0_ID:
					floatValue = sourceLogicNode.m44Value [0];
					break;

				case Enums.m44Value_1_ID:
					floatValue = sourceLogicNode.m44Value [1];
					break;

				case Enums.m44Value_2_ID:
					floatValue = sourceLogicNode.m44Value [2];
					break;

				case Enums.m44Value_3_ID:
					floatValue = sourceLogicNode.m44Value [3];
					break;

				case Enums.m44Value_4_ID:
					floatValue = sourceLogicNode.m44Value [4];
					break;

				case Enums.m44Value_5_ID:
					floatValue = sourceLogicNode.m44Value [5];
					break;

				case Enums.m44Value_6_ID:
					floatValue = sourceLogicNode.m44Value [6];
					break;

				case Enums.m44Value_7_ID:
					floatValue = sourceLogicNode.m44Value [7];
					break;

				case Enums.m44Value_8_ID:
					floatValue = sourceLogicNode.m44Value [8];
					break;

				case Enums.m44Value_9_ID:
					floatValue = sourceLogicNode.m44Value [9];
					break;

				case Enums.m44Value_10_ID:
					floatValue = sourceLogicNode.m44Value [10];
					break;

				case Enums.m44Value_11_ID:
					floatValue = sourceLogicNode.m44Value [11];
					break;

				case Enums.m44Value_12_ID:
					floatValue = sourceLogicNode.m44Value [12];
					break;

				case Enums.m44Value_13_ID:
					floatValue = sourceLogicNode.m44Value [13];
					break;

				case Enums.m44Value_14_ID:
					floatValue = sourceLogicNode.m44Value [14];
					break;

				case Enums.m44Value_15_ID:
					floatValue = sourceLogicNode.m44Value [15];
					break;

				case Enums.m44ValueDeterminant_ID:
					floatValue = sourceLogicNode.m44ValueDeterminant;
					break;

				case Enums.NavMeshHit_distance_ID:
					floatValue = sourceLogicNode.NavMeshHit_distance;
					break;

				case Enums.hit2D_distance_ID:
					floatValue = sourceLogicNode.hit2D_distance;
					break;

				case Enums.hit2D_fraction_ID:
					floatValue = sourceLogicNode.hit2D_fraction;
					break;
				}

				ln_InOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setSource, 0);
				switch (ln_InOutID)
				{
				case Enums.floatsList_ID:
					if (lstIndex >-1 && lstIndex < sourceLogicNode.floatsListValue.Count)
					{
						floatValue = sourceLogicNode.floatsListValue [lstIndex];
					}
					break;
				}
				break;

			case VariableTypeForProject.Int:
				switch (ln_InOutID)
				{
				case Enums.touch_fingerId_ID:
					intValue = sourceLogicNode.touch_fingerId;
					break;

				case Enums.touch_tapCount_ID:
					intValue = sourceLogicNode.touch_tapCount;
					break;
				}

				switch (ln_InOutID)
				{
				case Enums.intValue_ID:
					intValue = sourceLogicNode.intValue;
					break;

				case Enums.intValues_0_ID:
					intValue = sourceLogicNode.intValues [0];
					break;

				case Enums.intValues_1_ID:
					intValue = sourceLogicNode.intValues [1];
					break;

				case Enums.intValues_2_ID:
					intValue = sourceLogicNode.intValues [2];
					break;

				case Enums.raycastHitTriangleIndex_ID:
					intValue = sourceLogicNode.raycastHitTriangleIndex;
					break;
				}

				ln_InOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setSource, 0);
				switch (ln_InOutID)
				{
				case Enums.intsList_ID:
					if (lstIndex >-1 && lstIndex < sourceLogicNode.intsListValue.Count)
					{
						intValue = sourceLogicNode.intsListValue [lstIndex];
					}
					break;
				}
				break;

			case VariableTypeForProject.String:
				switch (ln_InOutID)
				{
				case Enums.touch_type_ID:
					stringValue = sourceLogicNode.touch_type.ToString ();
					break;

				case Enums.touch_phase_ID:
					stringValue = sourceLogicNode.touch_phase.ToString ();
					break;
				}

				switch (ln_InOutID)
				{
				case Enums.stringValue_ID:
					stringValue = sourceLogicNode.stringValue;
					break;

				case Enums.stringValues_0_ID:
					stringValue = sourceLogicNode.stringValues [0];
					break;

				case Enums.stringValues_1_ID:
					stringValue = sourceLogicNode.stringValues [1];
					break;
				}

				ln_InOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setSource, 0);
				switch (ln_InOutID)
				{
				case Enums.stringsList_ID:
					if (lstIndex >-1 && lstIndex < sourceLogicNode.stringsListValue.Count)
					{
						stringValue = sourceLogicNode.stringsListValue [lstIndex];
					}
					break;
				}
				break;


			case VariableTypeForProject.Vector2:
				switch (ln_InOutID)
				{
				case Enums.vector2Value_ID:
					vector2Value = sourceLogicNode.vector2Value;
					break;

				case Enums.vector2Values_0_ID:
					vector2Value = sourceLogicNode.vector2Values [0];
					break;

				case Enums.vector2Values_1_ID:
					vector2Value = sourceLogicNode.vector2Values [1];
					break;

				case Enums.raycastHitLightmapCoord_ID:
					vector2Value = sourceLogicNode.raycastHitLightmapCoord;
					break;

				case Enums.raycastHitTextureCoord_ID:
					vector2Value = sourceLogicNode.raycastHittextureCoord;
					break;

				case Enums.raycastHitTextureCoord2_ID:
					vector2Value = sourceLogicNode.raycastHittextureCoord2;
					break;

				case Enums.hit2D_centroid_ID:
					vector2Value = sourceLogicNode.hit2D_centroid;
					break;

				case Enums.hit2D_normal_ID:
					vector2Value = sourceLogicNode.hit2D_normal;
					break;

				case Enums.hit2D_point_ID:
					vector2Value = sourceLogicNode.hit2D_point;
					break;
				}

				ln_InOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setSource, 0);
				switch (ln_InOutID)
				{
				case Enums.vector2List_ID:
					if (lstIndex >-1 && lstIndex < sourceLogicNode.vector2ListValue.Count)
					{
						vector2Value = sourceLogicNode.vector2ListValue [lstIndex];
					}
					break;
				}
				break;

			case VariableTypeForProject.Vector3:
				switch (ln_InOutID)
				{
				case Enums.vector3Value_ID:
					vector3Value = sourceLogicNode.vector3Value;
					break;

				case Enums.vector3Values_0_ID:
					vector3Value = sourceLogicNode.vector3Values [0];
					break;

				case Enums.vector3Values_1_ID:
					vector3Value = sourceLogicNode.vector3Values [1];
					break;


				case Enums.boundsCenterValue_ID:
					vector3Value = sourceLogicNode.boundsCenterValue;
					break;

				case Enums.boundsExtentsValue_ID:
					vector3Value = sourceLogicNode.boundsExtentsValue;
					break;

				case Enums.boundsMaxValue_ID:
					vector3Value = sourceLogicNode.boundsMaxValue;
					break;

				case Enums.boundsMinValue_ID:
					vector3Value = sourceLogicNode.boundsMinValue;
					break;

				case Enums.boundsSizeValue_ID:
					vector3Value = sourceLogicNode.boundsSizeValue;
					break;


				case Enums.raycastHitBarycentricCoordinate_ID:
					vector3Value = sourceLogicNode.raycastHitBarycentricCoordinate;
					break;

				case Enums.raycastHitNormal_ID:
					vector3Value = sourceLogicNode.raycastHitNormal;
					break;

				case Enums.raycastHitPoint_ID:
					vector3Value = sourceLogicNode.raycastHitPoint;
					break;


				case Enums.OffMeshLinkData_startPosition_ID:
					vector3Value = sourceLogicNode.OffMeshLinkData_startPosition;
					break;

				case Enums.OffMeshLinkData_endPosition_ID:
					vector3Value = sourceLogicNode.OffMeshLinkData_endPosition;
					break;


				case Enums.NavMeshHit_normal_ID:
					vector3Value = sourceLogicNode.NavMeshHit_normal;
					break;

				case Enums.NavMeshHit_position_ID:
					vector3Value = sourceLogicNode.NavMeshHit_position;
					break;
				}

				ln_InOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setSource, 0);
				switch (ln_InOutID)
				{
				case Enums.vector3List_ID:
					if (lstIndex >-1 && lstIndex < sourceLogicNode.vector3ListValue.Count)
					{
						vector3Value = sourceLogicNode.vector3ListValue [lstIndex];
					}
					break;
				}
				break;

			case VariableTypeForProject.Vector4:
				switch (ln_InOutID)
				{
				case Enums.vector4Value_ID:
					vector4Value = sourceLogicNode.vector4Value;
					break;

				case Enums.vector4Values_0_ID:
					vector4Value = sourceLogicNode.vector4Values [0];
					break;

				case Enums.vector4Values_1_ID:
					vector4Value = sourceLogicNode.vector4Values [1];
					break;				
				}

				ln_InOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setSource, 0);
				switch (ln_InOutID)
				{
				case Enums.vector4List_ID:
					if (lstIndex >-1 && lstIndex < sourceLogicNode.vector4ListValue.Count)
					{
						vector4Value = sourceLogicNode.vector4ListValue [lstIndex];
					}
					break;
				}
				break;

			case VariableTypeForProject.GameObject:
				switch (ln_InOutID)
				{
				case Enums.gameObjectValues_0_ID:
					gameObjectValue = sourceLogicNode.gameObjectValues [0];
					break;

				case Enums.gameObjectValues_1_ID:
					gameObjectValue = sourceLogicNode.gameObjectValues [1];
					break;

				case Enums.gameObjectValue_ID:
					gameObjectValue = sourceLogicNode.gameObjectValue;
					break;

				case Enums.hit2D_gameObject_ID:
					gameObjectValue = sourceLogicNode.hit2D_gameObject;
					break;

				case Enums.raycastHitGameObject_ID:
					gameObjectValue = sourceLogicNode.raycastHitGameObject;
					break;
				}

				ln_InOutID = sourceLogicNode.InOutAdressCurrentToLinkToInOutID (setSource, 0);
				switch (ln_InOutID)
				{
				case Enums.gameObjectsList_ID:
					if (lstIndex >-1 && lstIndex < sourceLogicNode.gameObjectsListValue.Count)
					{
						gameObjectValue = sourceLogicNode.gameObjectsListValue [lstIndex];
					}
				break;
				}
				break;
			}
		}

		public string TypeToVarName ()
		{
			string r = "";

			switch (variableTypeForProject)
			{
			case VariableTypeForProject.Bool:
				r = "boolValue";
				break;

			case VariableTypeForProject.Float:
				r = "floatValue";
				break;

			case VariableTypeForProject.Int:
				r = "intValue";
				break;

			case VariableTypeForProject.String:
				r = "stringValue";
				break;


			case VariableTypeForProject.Vector2:
				r = "vector2Value";
				break;

			case VariableTypeForProject.Vector3:
				r = "vector3Value";
				break;

			case VariableTypeForProject.Vector4:
				r = "vector4Value";
				break;

			case VariableTypeForProject.GameObject:
				r = "gameObjectValue";
				break;
			}

			return r;
		}

		public string TypeToVarDeclaration ()
		{
			string r = "";

			switch (variableTypeForProject)
			{
			case VariableTypeForProject.Bool:
				r = "bool";
				break;

			case VariableTypeForProject.Float:
				r = "float";
				break;

			case VariableTypeForProject.Int:
				r = "int";
				break;

			case VariableTypeForProject.String:
				r = "string";
				break;


			case VariableTypeForProject.Vector2:
				r = "Vector2";
				break;

			case VariableTypeForProject.Vector3:
				r = "Vector3";
				break;

			case VariableTypeForProject.Vector4:
				r = "Vector4";
				break;

			case VariableTypeForProject.GameObject:
				r = "GameObject";
				break;
			}

			return r;
		}

		public bool Showing ()
		{
			return Diamond.projectVariables.projectVariablesToShow.Contains (this);
		}
		#endregion links

		#region auxiliaries
		GUIStyle GetGuiStyle (string styleName)
		{
			return Skins.guiSkin.GetStyle (styleName);
		}
		#endregion auxiliaries
	}
}
