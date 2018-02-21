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
	/// Project variables.
	/// class that manages the list of the project variables and their appearance in the Diamond window.
	/// </summary>
	public class ProjectVariables : ScriptableObject 
	{
		#region filter
		public bool showFilter = false;


		public bool showAll = true;

		public bool hideAll = false;



		public bool showBool = true;

		public bool showFloat = true;

		public bool showInt = true;

		public bool showString = true;


		public bool showVector2 = true;

		public bool showVector3 = true;

		public bool showVector4 = true;

		public bool showGameObject = true;
		#endregion filter


		public const string myName = "ProjectVariables";


		public List<ProjectVariable> projectVariables;

		public List<ProjectVariable> projectVariablesToShow;


		public List<ProjectVariable> projectVariables_bool;

		public List<ProjectVariable> projectVariables_float;

		public List<ProjectVariable> projectVariables_int;

		public List<ProjectVariable> projectVariables_string;


		public List<ProjectVariable> projectVariables_vector2;

		public List<ProjectVariable> projectVariables_vector3;

		public List<ProjectVariable> projectVariables_vector4;

		public List<ProjectVariable> projectVariables_gameObject;


		public void Init ()
		{
			projectVariables = new List<ProjectVariable> ();

			projectVariablesToShow = new List<ProjectVariable> ();


			projectVariables_bool = new List<ProjectVariable> ();

			projectVariables_float = new List<ProjectVariable> ();

			projectVariables_int = new List<ProjectVariable> ();

			projectVariables_string = new List<ProjectVariable> ();


			projectVariables_vector2 = new List<ProjectVariable> ();

			projectVariables_vector3 = new List<ProjectVariable> ();

			projectVariables_vector4 = new List<ProjectVariable> ();

			projectVariables_gameObject = new List<ProjectVariable> ();
		}

		public void UpdateProjectVariables ()
		{
			ComposeProjectVariablesToShow ();
		}


		public void AddProjectVariable (ProjectVariable pVar)
		{
			projectVariables.Add (pVar);

			switch (pVar.variableTypeForProject)
			{
			case VariableTypeForProject.Bool:
				projectVariables_bool.Add (pVar);
				break;

			case VariableTypeForProject.Float:
				projectVariables_float.Add (pVar);
				break;

			case VariableTypeForProject.Int:
				projectVariables_int.Add (pVar);
				break;

			case VariableTypeForProject.String:
				projectVariables_string.Add (pVar);
				break;


			case VariableTypeForProject.Vector2:
				projectVariables_vector2.Add (pVar);
				break;

			case VariableTypeForProject.Vector3:
				projectVariables_vector3.Add (pVar);
				break;

			case VariableTypeForProject.Vector4:
				projectVariables_vector4.Add (pVar);
				break;

			case VariableTypeForProject.GameObject:
				projectVariables_gameObject.Add (pVar);
				break;
			}
		}

		public void RemoveProjectVariable (ProjectVariable pVar)
		{
			ProjectVariable pVarTmp = pVar;

			projectVariables.Remove (pVar);

			RemoveProjectVariableFromOthers (pVar);

			UnityEngine.Object.DestroyImmediate (pVarTmp, true);
		}

		void RemoveProjectVariableFromOthers (ProjectVariable pVar)
		{
			switch (pVar.variableTypeForProject)
			{
			case VariableTypeForProject.Bool:
				projectVariables_bool.Remove (pVar);
				break;

			case VariableTypeForProject.Float:
				projectVariables_float.Remove (pVar);
				break;

			case VariableTypeForProject.Int:
				projectVariables_int.Remove (pVar);
				break;

			case VariableTypeForProject.String:
				projectVariables_string.Remove (pVar);
				break;


			case VariableTypeForProject.Vector2:
				projectVariables_vector2.Remove (pVar);
				break;

			case VariableTypeForProject.Vector3:
				projectVariables_vector3.Remove (pVar);
				break;

			case VariableTypeForProject.Vector4:
				projectVariables_vector4.Remove (pVar);
				break;

			case VariableTypeForProject.GameObject:
				projectVariables_gameObject.Remove (pVar);
				break;
			}
		}


		void ComposeProjectVariablesToShow ()
		{
			projectVariablesToShow = new List<ProjectVariable>();

			if (hideAll)
				return;

			if (showBool)
			{
				AddToProjectVariablesToShow (projectVariables_bool);
			}

			if (showFloat)
			{
				AddToProjectVariablesToShow (projectVariables_float);
			}

			if (showInt)
			{
				AddToProjectVariablesToShow (projectVariables_int);
			}

			if (showString)
			{
				AddToProjectVariablesToShow (projectVariables_string);
			}


			if (showVector2)
			{
				AddToProjectVariablesToShow (projectVariables_vector2);
			}

			if (showVector3)
			{
				AddToProjectVariablesToShow (projectVariables_vector3);
			}

			if (showVector4)
			{
				AddToProjectVariablesToShow (projectVariables_vector4);
			}

			if (showGameObject)
			{
				AddToProjectVariablesToShow (projectVariables_gameObject);
			}
		}

		void AddToProjectVariablesToShow (List<ProjectVariable> pVarsToAdd)
		{
			for (int i = 0; i < pVarsToAdd.Count; i++)
				projectVariablesToShow.Add (pVarsToAdd [i]);
		}


		public void ActiveAllFilters ()
		{
			hideAll = false;


			showBool = true;

			showFloat = true;

			showInt = true;

			showString = true;


			showVector2 = true;

			showVector3 = true;

			showVector4 = true;

			showGameObject = true;
		}
	
		public void DiactiveAllFilters ()
		{
			showBool = false;

			showFloat = false;

			showInt = false;

			showString = false;


			showVector2 = false;

			showVector3 = false;

			showVector4 = false;

			showGameObject = false;
		}

		public void DiactiveHideAll ()
		{
			if (showBool || showFloat || showInt || showString ||
				showVector2 || showVector3 || showVector4 || showGameObject)
			{
				hideAll = false;
			}
		}
	

		public bool ProjectVariableStillHereOnUniqueID (string uID)
		{
			bool r = false;

			for (int i = 0; i < projectVariables.Count; i++)
			{
				if (projectVariables [i].uniqueID == uID)
				{
					r = true;

					break;
				}
			}

			return r;
		}

		public ProjectVariable ProjectVariableOnUniqueID (string uID)
		{
			ProjectVariable r = null;

			if (string.IsNullOrEmpty (uID))
				return null;

			for (int i = 0; i < projectVariables.Count; i++)
			{
				if (projectVariables [i].uniqueID == uID)
				{
					r = projectVariables [i];

					break;
				}
			}

			return r;
		}
	
		public int ProjectVariableIndexOnUniqueID (string uID)
		{
			int r = -1;

			for (int i = 0; i < projectVariables.Count; i++)
			{
				if (projectVariables [i].uniqueID == uID)
				{
					r = i;

					break;
				}
			}

			return r;
		}
	}     
}

