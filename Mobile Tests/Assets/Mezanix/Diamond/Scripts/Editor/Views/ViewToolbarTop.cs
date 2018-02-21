using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

/// <summary>
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{
	/// <summary>
	/// View toolbar top.
	/// The top view inside the Diamond window, holding the global menu and its buttons
	/// </summary>
	[Serializable]
	public class ViewToolbarTop : View 
	{
		Vector2 mousePosition;

		bool newGraphMenuOpened = false;

		bool preferencesOpened = false;

		Event eGlobal;

		Rect rectGlobal;

		Graph graphGlobal;


		public ViewToolbarTop (string setTitle) : base (setTitle)
		{
			
		}


		GUIStyle GetGuiStyle (string styleName)
		{
			return Skins.guiSkin.GetStyle (styleName);
		}


		delegate void ButtonFunction ();

		void SaveGraphAndScene ()
		{
			Auxiliaries.SaveAndRefreshAssetsForced ();
			Auxiliaries.SaveActiveScene ();
		}

		void SaveGraph ()
		{
			Auxiliaries.SaveAndRefreshAssetsForced ();
		}

		void OpenGraphMenu ()
		{
			newGraphMenuOpened = true;
		}

		void LoadGraph ()
		{
			Auxiliaries.LoadGraph ();
		}

		void OpenPrefereneces ()
		{
			scriptsGenerationFolderPath_old = Diamond.namesToSave.scriptsGenerationFolderPath;

			texturesGenerationFolderPath_old = Diamond.namesToSave.texturesGenerationFolderPath;

			preferencesOpened = true;
		}

		void GoToDocumentation ()
		{
			Application.OpenURL ("http://mezanix.com/portfolio/diamond-documentation/");
		}

		void CloseDiamond ()
		{
			Diamond.close = true;
		}

		void DrawButton (Vector2 buttonPosition, ButtonFunction buttonFunction, string guiStyleName, string floatingMessag)
		{
			Rect buttonRect = new Rect (buttonPosition, buttonsSize);

			bool antiPosition = guiStyleName == Skins.x_small || guiStyleName == Skins.asterix_small || 
				guiStyleName == Skins.buttonQuestionMark;

			string buttonText = (guiStyleName == Skins.buttonQuestionMark)? "?  ": "";

			if (antiPosition)
			{
				GUI.Box (new Rect (buttonRect.position + new Vector2 (-Skins.separatorThickness, 0f),
					new Vector2 (Skins.separatorThickness, buttonsSize.y)),
					"", Skins.guiSkin.GetStyle (Skins.separator));
				
			}

			if (GUI.Button (buttonRect, buttonText, GetGuiStyle (guiStyleName)))
			{
				buttonFunction ();
			}
			if ( ! antiPosition)
			{
			GUI.Box (new Rect (buttonRect.position + new Vector2 (buttonsSize.x, 0f),
				new Vector2 (Skins.separatorThickness, buttonsSize.y)),
				"", Skins.guiSkin.GetStyle (Skins.separator));
			}


			Vector2 floatingShift = new Vector2 ((antiPosition? -1f: 1f)*40f, 0f);

			if (buttonRect.Contains (eGlobal.mousePosition))
				LogicNode.DrawFloatingMessage (eGlobal.mousePosition + floatingShift, floatingMessag);


			if (guiStyleName == Skins.saveFile)
			{
				if (Diamond.changesOccured)
				{
					GUI.Box (RectOperations.RatioToAbsolute (buttonRect, new Rect (0.5f, 0.5f, 0.52f, 0.52f)), "", 
						GetGuiStyle (Skins.redDot));
				}
			}

			if (guiStyleName == Skins.saveFileAndScene)
			{
				if ((Diamond.changesOccured && UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene ().isDirty) ||
					UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene ().isDirty)
				{
					GUI.Box (RectOperations.RatioToAbsolute (buttonRect, new Rect (0.5f, 0.5f, 0.52f, 0.52f)), "", 
						GetGuiStyle (Skins.redDot));
				}
			}
		}


		Vector2 buttonsSize;
		void DrawButtons ()
		{
			buttonsSize = Vector2.one * rectGlobal.height;

			int buttonPositionId = -1;

			buttonPositionId++;
			Vector2 currentPos = new Vector2 (rectGlobal.position.x + buttonsSize.x*(float)buttonPositionId, rectGlobal.position.y) ;
			DrawButton (currentPos, OpenGraphMenu, Skins.newFile, "New Graph");

			buttonPositionId++;
			currentPos = new Vector2 (rectGlobal.position.x + buttonsSize.x*(float)buttonPositionId, rectGlobal.position.y) ;
			DrawButton (currentPos, LoadGraph, Skins.loadFile, "Load Graph");

			if (graphGlobal != null)
			{
				buttonPositionId++;
				currentPos = new Vector2 (rectGlobal.position.x + buttonsSize.x*(float)buttonPositionId, rectGlobal.position.y) ;
				DrawButton (currentPos, SaveGraph, Skins.saveFile, "Save Graph");

				buttonPositionId++;
				currentPos = new Vector2 (rectGlobal.position.x + buttonsSize.x*(float)buttonPositionId, rectGlobal.position.y) ;
				DrawButton (currentPos, SaveGraphAndScene, Skins.saveFileAndScene, "Save Graph and Scene");
			}


			int buttonAntiPositionId = 0;

			buttonAntiPositionId++;
			Vector2 currentAntiPos = new Vector2 (rectGlobal.position.x + rectGlobal.width - buttonsSize.x*(float)buttonAntiPositionId,
				rectGlobal.position.y);
			DrawButton (currentAntiPos, CloseDiamond, Skins.x_small, "Close Diamond");

			buttonAntiPositionId++;
			currentAntiPos = new Vector2 (rectGlobal.position.x + rectGlobal.width - buttonsSize.x*(float)buttonAntiPositionId,
				rectGlobal.position.y);
			DrawButton (currentAntiPos,	OpenPrefereneces, Skins.asterix_small, "Preferences");

			buttonAntiPositionId++;
			currentAntiPos = new Vector2 (rectGlobal.position.x + rectGlobal.width - buttonsSize.x*(float)buttonAntiPositionId,
				rectGlobal.position.y);
			DrawButton (currentAntiPos,	GoToDocumentation, Skins.buttonQuestionMark, "Documentation & Tutorials");
		}


		public override void ViewUpdate (Event e, Graph graph, Rect rect)
		{
			base.ViewUpdate (e, graph, rect);

			eGlobal = e;

			rectGlobal = rect;

			graphGlobal = graph;

			GUI.Box (rect, title, Skins.guiSkin.GetStyle (Skins.view));

			Skins.DrawSeparator (
				new Rect (rect.x, rect.y + rect.height, rect.width, 
					Skins.separatorThickness));
			
			DrawButtons ();



		

			DrawNewGraphMenu (rect, e);

			DrawPreferencesMenu (rect, e);
		}


		string newGraphName = "";
		float [] newGraphMenu_fieldsIndex = new float[]{0f, 0f};
		Rect SuitRect (float [] fieldsIndex, Rect rect, float [] fStep)
		{
			return new Rect (
				rect.x + fieldsIndex [0]*fStep [0],
				rect.y + rect.height + fieldsIndex [1]*fStep [1],
				fStep [0], fStep [1]);
		}
		void DrawNewGraphMenu (Rect rect, Event e)
		{
			if ( ! newGraphMenuOpened)
			{
				return;
			}


			float [] fieldStep = new float[]{rect.height*8f, rect.height};

			Rect bgRect = new Rect (
				rect.x, 
				rect.y + rect.height,
				newGraphMenu_fieldsIndex [0]*fieldStep [0],
				newGraphMenu_fieldsIndex [1]*fieldStep [1]);

			GUI.Box (bgRect, "", Skins.guiSkin.GetStyle (Skins.node));



			newGraphMenu_fieldsIndex = new float[]{0f, 0f};
			EditorGUI.LabelField (SuitRect (newGraphMenu_fieldsIndex, rect, fieldStep),
				"New Graph Name", Skins.guiSkin.GetStyle (Skins.logicNodeName));
			newGraphMenu_fieldsIndex [0] += 1f;
			newGraphMenu_fieldsIndex [1] += 1f;

			newGraphMenu_fieldsIndex [0] -= 1f;
			Rect rectFieldTmp = SuitRect (newGraphMenu_fieldsIndex, rect, fieldStep);
			Rect rectFieldTmpNorm = new Rect (0.1f, 0f, 0.8f, 0.6f);
			Rect rectFieldTmpRes = new Rect (
				rectFieldTmp.x + rectFieldTmp.width*rectFieldTmpNorm.x,
				rectFieldTmp.y + rectFieldTmp.height*rectFieldTmpNorm.y,
				rectFieldTmp.width*rectFieldTmpNorm.width,
				rectFieldTmp.height*rectFieldTmpNorm.height);
			newGraphName = EditorGUI.TextField (rectFieldTmpRes,
				newGraphName);
			newGraphMenu_fieldsIndex [0] += 1f;
			newGraphMenu_fieldsIndex [1] += 1f;

			newGraphMenu_fieldsIndex [0] -= 1f;
			rectFieldTmp = SuitRect (newGraphMenu_fieldsIndex, rect, fieldStep);
			rectFieldTmpNorm = new Rect (0.1f, 0f, 0.35f, 0.6f);
			rectFieldTmpRes = new Rect (
				rectFieldTmp.x + rectFieldTmp.width*rectFieldTmpNorm.x,
				rectFieldTmp.y + rectFieldTmp.height*rectFieldTmpNorm.y,
				rectFieldTmp.width*rectFieldTmpNorm.width,
				rectFieldTmp.height*rectFieldTmpNorm.height);
			if (GUI.Button (rectFieldTmpRes, "Create Graph", 
				Skins.guiSkin.GetStyle (Skins.LittleNamedRectsCenterDark)) ||
				MouseKeysEvents.KeyIsUp (KeyCode.Return, e))
			{
				newGraphName = StringTreatment.ScriptName (newGraphName);

				if ( ! string.IsNullOrEmpty (newGraphName))
				{
					if (ClassesNamesManager.CheckNewName (newGraphName))
					{
						if (Auxiliaries.CreateGraph (newGraphName, GraphType.MonoBehaviour))
						{
							ClassesNamesManager.AddNewName (newGraphName);

							newGraphMenuOpened = false;
						}
						else //Auxiliaries.CreateGraph (newGraphName, GraphType.MonoBehaviour) Failed to create Graph
						{
							EditorUtility.DisplayDialog ("Graph Creation", "Failed to create Graph or canceled by the user", "Ok");
						}
					}
				}
				else if (string.IsNullOrEmpty (newGraphName))
				{
					EditorUtility.DisplayDialog ("Invalid Name", Enums.invalidNameDialog, "Ok");
				}

				newGraphMenuOpened = false;
			}
			newGraphMenu_fieldsIndex [0] += 1f;

			newGraphMenu_fieldsIndex [0] -= 1f;
			rectFieldTmp = SuitRect (newGraphMenu_fieldsIndex, rect, fieldStep);
			rectFieldTmpNorm = new Rect (0.55f, 0f, 0.35f, 0.6f);
			rectFieldTmpRes = new Rect (
				rectFieldTmp.x + rectFieldTmp.width*rectFieldTmpNorm.x,
				rectFieldTmp.y + rectFieldTmp.height*rectFieldTmpNorm.y,
				rectFieldTmp.width*rectFieldTmpNorm.width,
				rectFieldTmp.height*rectFieldTmpNorm.height);
			if (GUI.Button (rectFieldTmpRes, "Cancel", 
				Skins.guiSkin.GetStyle (Skins.LittleNamedRectsCenterDark)) ||
				MouseKeysEvents.ControlCommandAltKey (KeyCode.B, e))
			{
				newGraphMenuOpened = false;
			}
			newGraphMenu_fieldsIndex [0] += 1f;
			newGraphMenu_fieldsIndex [1] += 1f;
		}
	

		float antiposGap = 38f;
		float [] preferenceshMenu_fieldsIndex = new float[]{0f, 0f};
		Rect SuitRect_antiPos (float [] fieldsIndex, Rect rect, float [] fStep)
		{
			return new Rect (
				rect.x + rect.width - fieldsIndex [0]*fStep [0] - antiposGap,
				rect.y + rect.height + fieldsIndex [1]*fStep [1],
				fStep [0], fStep [1]);
		}
		Rect SuitRect_antiPos (float [] fieldsIndex, Rect rect, float [] fStep, Rect inRatio)
		{
			Rect preRect = new Rect (
				rect.x + rect.width - fieldsIndex [0]*fStep [0] - antiposGap,
				rect.y + rect.height + fieldsIndex [1]*fStep [1],
				fStep [0], fStep [1]);

			return RectOperations.RatioToAbsolute (preRect, inRatio);
		}

		string scriptsGenerationFolderPath_old;
		string texturesGenerationFolderPath_old;
		void DrawPreferencesMenu (Rect rect, Event e)
		{
			if ( ! preferencesOpened)
				return;

			float [] fieldStep = new float[]{rect.height*8f, rect.height};

			Rect bgRect = new Rect (
				rect.x + rect.width - preferenceshMenu_fieldsIndex [0]*fieldStep [0] - antiposGap,
				rect.y + rect.height,
				preferenceshMenu_fieldsIndex [0]*fieldStep [0],
				preferenceshMenu_fieldsIndex [1]*fieldStep [1]);

			GUI.Box (bgRect, "", Skins.guiSkin.GetStyle (Skins.node));


			preferenceshMenu_fieldsIndex = new float[]{1f, 0f};
			preferenceshMenu_fieldsIndex [0] += 1f;
			EditorGUI.LabelField (SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep),
				"\tPreferences", Skins.guiSkin.GetStyle (Skins.projectVariableName));
			preferenceshMenu_fieldsIndex [1] += 1f;



			//preferenceshMenu_fieldsIndex [0] -= 1f;
			Rect rectFieldTmp_NoInRatio = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep);
			Rect rectFieldTmp = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep, new Rect (0.2f, 0f, 0.8f, 1f));
			EditorGUI.LabelField (rectFieldTmp, "Scripts generation Folder:", GetGuiStyle (Skins.logicNodeLabelLeft));
			preferenceshMenu_fieldsIndex [0] -= 1f;
			//preferenceshMenu_fieldsIndex [1] += 1f;


			//preferenceshMenu_fieldsIndex [0] += 1f;
			rectFieldTmp_NoInRatio = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep);
			rectFieldTmp = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep, new Rect (0f, 0.25f, 0.9f, 0.65f));
			EditorGUI.TextField (rectFieldTmp, Diamond.namesToSave.scriptsGenerationFolderPath);

			Rect actionRect = RectOperations.RatioToAbsolute (rectFieldTmp_NoInRatio, new Rect (0.9f, 0f, 0.1f, 1f));
			if (GUI.Button (actionRect, "", GetGuiStyle (Skins.forward)))
			{
				Diamond.namesToSave.scriptsGenerationFolderPath = GetAPreferenceFolderPath ("Where to generate Scripts?");

				ScriptGenerationFolderPathFromNamesToSave ();
			}
			preferenceshMenu_fieldsIndex [0] += 1f;
			preferenceshMenu_fieldsIndex [1] += 1f;



			//preferenceshMenu_fieldsIndex [0] -= 1f;
			rectFieldTmp_NoInRatio = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep);
			rectFieldTmp = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep, new Rect (0.2f, 0f, 0.8f, 1f));
			EditorGUI.LabelField (rectFieldTmp, "Textures generation Folder:", GetGuiStyle (Skins.logicNodeLabelLeft));
			preferenceshMenu_fieldsIndex [0] -= 1f;
			//preferenceshMenu_fieldsIndex [1] += 1f;


			//preferenceshMenu_fieldsIndex [0] += 1f;
			rectFieldTmp_NoInRatio = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep);
			rectFieldTmp = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep, new Rect (0f, 0.25f, 0.9f, 0.65f));
			EditorGUI.TextField (rectFieldTmp,	Diamond.namesToSave.texturesGenerationFolderPath);

			actionRect = RectOperations.RatioToAbsolute (rectFieldTmp_NoInRatio, new Rect (0.9f, 0f, 0.1f, 1f));
			if (GUI.Button (actionRect, "", GetGuiStyle (Skins.forward)))
				Diamond.namesToSave.texturesGenerationFolderPath = GetAPreferenceFolderPath ("Where to generate Textures?");
			preferenceshMenu_fieldsIndex [0] += 1f;
			preferenceshMenu_fieldsIndex [1] += 1f;




			//preferenceshMenu_fieldsIndex [0] -= 1f;
			rectFieldTmp_NoInRatio = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep);
			rectFieldTmp = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep, new Rect (0.1f, 0.25f, 0.8f, 0.65f));
			if (GUI.Button (rectFieldTmp, "Done", GetGuiStyle (Skins.LittleNamedRectsCenterDark)))
			{
				Auxiliaries.SaveAndRefreshAssetsForced ();

				preferencesOpened = false;
			}
			preferenceshMenu_fieldsIndex [0] -= 1f;
			//preferenceshMenu_fieldsIndex [1] += 1f;

			//preferenceshMenu_fieldsIndex [0] += 1f;
			rectFieldTmp_NoInRatio = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep);
			rectFieldTmp = SuitRect_antiPos (preferenceshMenu_fieldsIndex, rect, fieldStep, new Rect (0f, 0.25f, 0.9f, 0.65f));
			if (GUI.Button (rectFieldTmp, "Cancel", GetGuiStyle (Skins.LittleNamedRectsCenterDark)))
			{
				Diamond.namesToSave.scriptsGenerationFolderPath = scriptsGenerationFolderPath_old;

				ScriptGenerationFolderPathFromNamesToSave ();

				Diamond.namesToSave.texturesGenerationFolderPath = texturesGenerationFolderPath_old;


				Auxiliaries.SaveAndRefreshAssetsForced ();

				preferencesOpened = false;
			}

			//actionRect = RectOperations.RatioToAbsolute (rectFieldTmp_NoInRatio, new Rect (0.9f, 0f, 0.1f, 1f));
			//if (GUI.Button (actionRect, "", GetGuiStyle (Skins.forward)))
			//	TextureWriter.texturesFolderPath = GetAPreferenceFolderPath ("Where to generate Textures?");
			preferenceshMenu_fieldsIndex [0] += 1f;
			preferenceshMenu_fieldsIndex [1] += 1f;


		}

		string GetAPreferenceFolderPath (string menuTitle)
		{
			string r = Auxiliaries.OpenChooseFolderMenu (menuTitle);

			if (string.IsNullOrEmpty (r))
			{
				r = @"Assets";

				EditorUtility.DisplayDialog (StringTreatment.selectedFolderMustBeValidAndInTheProjectAssets_Title,
					StringTreatment.selectedFolderMustBeValidAndInTheProjectAssets, "Ok");
			}

			return r;
		}
	

		void ScriptGenerationFolderPathFromNamesToSave ()
		{
			if (graphGlobal == null)
				return;

			graphGlobal.ScriptGenerationFolderPathFromNamesToSave ();
		}
	}
}
