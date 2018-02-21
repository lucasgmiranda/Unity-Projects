using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Mezanix.Diamond
{
	public partial class LogicNode : ScriptableObject 
	{
		partial void LogicNode_gameDesign ()
		{
			//DrawLogicNodeLabel ("Do", 0, 2);
			//if (GUI.Button (GetSuitableRect (FieldDrawType.label, 1, 2), ""))
			//{
			//	GenericMenu menu = new GenericMenu ();
			//
			//	menu.AddItem (new GUIContent ("Fps/levelDesign/cdfcd"), false, ttt);
			//
			//	menu.AddItem (new GUIContent ("Fps/controller"), false, ttt);
			//
			//	menu.AddItem (new GUIContent ("Platformer/levelDesign"), false, ttt);
			//
			//	menu.AddItem (new GUIContent ("Platformer/controller"), false, ttt);
			//
			//	menu.ShowAsContext ();	
			//}
		
			DrawGameTypeEnum ();
			AdaptOnGameTypeEnum ();
		}

		#region assetsPaths
		const string lightSourceIconFolder = "Assets/Mezanix/Diamond/Resources/GameDesignGui/Textures/LightMaze";
		const string lightSourceIconPath = lightSourceIconFolder + "/LightSource.png";

		const string spritesDefaultMaterialOriginFolder = "Assets/Mezanix/GameDesign/Materials";
		const string spritesDefaultMaterialOriginName = "SpritesDefault.mat";
		const string spritesDefaultMaterialOriginPath = spritesDefaultMaterialOriginFolder + "/" + spritesDefaultMaterialOriginName;


		#endregion assetsPaths

		public GameDesign.TagsLayersIdentifiers.GameType gameType;
		public int gameType_length = 0;
		public string gameType_last;
		void DrawGameTypeEnum ()
		{
			DrawLogicNodeLabel ("Game Type", 0, 2);
			gameType = (GameDesign.TagsLayersIdentifiers.GameType)DrawEnum (gameType,
				ref gameType_length, ref gameType_last,
				typeof (GameDesign.TagsLayersIdentifiers.GameType), FieldDrawType.label, 1, 2);
		}
		void AdaptOnGameTypeEnum ()
		{
			switch (gameType)
			{
			case GameDesign.TagsLayersIdentifiers.GameType.lightMaze:
				LogicNode_GaDeLightMaz ();
				GetGaDeLightMaze ();
				break;

			case GameDesign.TagsLayersIdentifiers.GameType.shootEmUp:
				break;

			case GameDesign.TagsLayersIdentifiers.GameType._2dPlatformer:
				LogicNode_GaDe2DPlat ();
				GetGaDe2DPlat ();
				break;
			}
		}


		partial void LogicNode_GaDe2DPlat ();
		void GetGaDe2DPlat ()
		{
			if ( ! ExtensionsHere.GaDe2DPlat)
				DrawExtensionUrlButton ("Game Design 2D Platformer", ExtensionsHere.GaDe2DPlatUrl);
		}


		partial void LogicNode_GaDeLightMaz ();
		void GetGaDeLightMaze ()
		{
			if ( ! ExtensionsHere.GaDeLightMaz)
				DrawExtensionUrlButton ("Game Design Light Maze", ExtensionsHere.GaDeLightMazUrl);
		}
		static GameObject GaDeLightMazParent;
		const string GaDeLightMazParent_Name = "Light_Maze_Parent";
		void GaDeLightMazParent_Find ()
		{
			if (GaDeLightMazParent != null)
			{
				GaDeLightMazParent_Enable ();
				return;
			}
			GameDesign.TagsLayersIdentifiers [] taggedGos = GameObject.FindObjectsOfType <GameDesign.TagsLayersIdentifiers> ();
			for (int i = 0; i < taggedGos.Length; i++)
			{
				if (taggedGos [i].gameType == GameDesign.TagsLayersIdentifiers.GameType.lightMaze &&
					taggedGos [i].layer == GameDesign.TagsLayersIdentifiers.Layer.parent)
				{
					GaDeLightMazParent = taggedGos [i].gameObject;
					break;
				}
			}
			if (GaDeLightMazParent == null)
			{
				GaDeLightMazParent_Create ();
			}
		}
		void GaDeLightMazParent_Enable ()
		{
			GaDeLightMazParent.SetActive (true);
			GameDesign.TagsLayersIdentifiers taggedGo = GaDeLightMazParent.GetComponent <GameDesign.TagsLayersIdentifiers> ();
			if (taggedGo != null)
			{
				taggedGo.enabled = true;
				GaDeLightMazParent_Set (taggedGo);
			}
		}
		void GaDeLightMazParent_Create ()
		{
			GaDeLightMazParent = new GameObject (GaDeLightMazParent_Name);
			if (GaDeLightMazParent == null)
				return;
			Auxiliaries.MakeActiveSceneDirty ();

			GameDesign.TagsLayersIdentifiers taggedGo = GaDeLightMazParent.AddComponent <GameDesign.TagsLayersIdentifiers> ();
			if (taggedGo != null)
			{
				GaDeLightMazParent_Set(taggedGo);
			}

			GaDeLightMazParent.AddComponent<GameDesign.ParentGameObject>();
		}
		void GaDeLightMazParent_Set (GameDesign.TagsLayersIdentifiers taggedGo)
		{
			taggedGo.gameType = GameDesign.TagsLayersIdentifiers.GameType.lightMaze;
			taggedGo.layer = GameDesign.TagsLayersIdentifiers.Layer.parent;
			taggedGo.roleTags = GameDesign.TagsLayersIdentifiers.RoleTags.game;
		}




		public enum GameDesignType
		{
			gameManagement,
			addSmartGameObject,
			editSmartGameObject,
		}
		public GameDesignType gameDesignType;
		public int gameDesignType_length = 0;
		public string gameDesignType_last;
		void DrawGameDesignTypeEnum ()
		{
			//DrawLogicNodeLabel ("Do");
			gameDesignType = (GameDesignType)DrawEnum (gameDesignType,
				ref gameDesignType_length, ref gameDesignType_last,
				typeof (GameDesignType), "Do", Skins.logicNodeLabel);
		}
		void AdaptOnGameDesignTypeEnum ()
		{
			switch (gameDesignType)
			{
			case GameDesignType.addSmartGameObject:
				break;

			case GameDesignType.editSmartGameObject:
				break;

			case GameDesignType.gameManagement:
				break;
			}
		}

		#region AddSmartGameObject


		void AddSmartGameObject ()
		{
		}
		#endregion AddSmartGameObject

		#region EditSmartGameObject
		void EditSmartGameObject ()
		{
		}
		#endregion EditSmartGameObject

		#region GameManager
		void GameManager ()
		{
		}
		#endregion GameManager

	}
}