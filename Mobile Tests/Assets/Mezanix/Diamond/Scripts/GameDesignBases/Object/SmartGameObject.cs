using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mezanix.GameDesign
{
	[ExecuteInEditMode]
	public class SmartGameObject : MonoBehaviour 
	{
		#if UNITY_EDITOR
		#region sceneViewBillboard_declaration
		SceneViewBillboard sceneViewBillboard = null;
		const string sceneViewBillboardGoName = "SceneViewBillboard";

		Shader unlitShader;
		const string unlitShaderFolder = "Assets/Mezanix/MezAuxMono/Shaders";
		const string unlitShaderPath = unlitShaderFolder + "/UnlitShaderTransparentQuadSize.shader";

		public AuxMono.Tex.TexturePosDepth [] texturePosDepths = new AuxMono.Tex.TexturePosDepth [0];
		#endregion sceneViewBillboard_declaration


		#region gameObject_declaration
		public GameObject scriptsHolder = null;
		public const string scriptsHolderName = "Scripts_Holder";

		public GameObject graphicsAndPhysicsHolder = null;
		public const string graphicsAndPhysicsHolderName = "GraphicsAndPhysics_Holder";
		//public TagsLayersIdentifiers.GameType gameObjectGameType;
		//public TagsLayersIdentifiers.Layer gameObjectLayer;
		#endregion gameObject_declaration

		//[Header ("Texture 0")]
		//public Texture2D texture_0;
		//[Range (0f, 100f)]
		//public float x_0 = 0f;
		//[Range (0f, 100f)]
		//public float y_0 = 0f;
		//
		//[Header ("Texture 1")]
		//public Texture2D texture_1;
		//[Range (0f, 100f)]
		//public float x_1 = 30f;
		//[Range (0f, 100f)]
		//public float y_1 = 30f;
		//
		//[Header ("Texture 2")]
		//public Texture2D texture_2;
		//[Range (0f, 100f)]
		//public float x_2 = 30f;
		//[Range (0f, 100f)]
		//public float y_2 = 30f;
#endif

		void LateUpdate ()
		{
			#if UNITY_EDITOR
			//UpdateInEditor ();
			#endif
		}

		#if UNITY_EDITOR
		void UpdateInEditor ()
		{
			GetSceneViewBillboard ();
			//GetGameObject ();
		}

		#region sceneViewBillboard
		public void GetSceneViewBillboard ()
		{
			if (IsSceneViewBillboardNotNull ())
				return;
			
			GameObject go = new GameObject (sceneViewBillboardGoName);
			if (go == null)
				return;
			go.transform.parent = transform;
			sceneViewBillboard = go.AddComponent <SceneViewBillboard> ();
			if (sceneViewBillboard == null)
				return;
			
			
			if ( ! GetUnlitShader ())
				return;
			SceneViewBillboardSetTextures ();
			sceneViewBillboard.Init (unlitShader, transform);



			TagsLayersIdentifiers taggedGo = go.AddComponent <TagsLayersIdentifiers> ();
			if (taggedGo == null)
				return;
			taggedGo.gameType = TagsLayersIdentifiers.GameType.lightMaze;
			taggedGo.layer = TagsLayersIdentifiers.Layer.none;
			taggedGo.roleTags = TagsLayersIdentifiers.RoleTags.unityEditor;
		}
		bool IsSceneViewBillboardNotNull ()
		{
			//if (sceneViewBillboard != null)
			//	return true;

			Transform tr = transform.Find (sceneViewBillboardGoName);
			if (tr == null)
				return false;

			SceneViewBillboard svbb = tr.GetComponent <SceneViewBillboard> ();
			if (svbb == null)
				return false;

			return true;
		}

		bool GetUnlitShader ()
		{
			if (unlitShader != null)
				return true;

			if ( ! UnityEditor.AssetDatabase.IsValidFolder (unlitShaderFolder))
				return false;

			unlitShader = (Shader)UnityEditor.AssetDatabase.LoadAssetAtPath <Shader> (unlitShaderPath);
			if (unlitShader == null)
				return false;

			return true;
		}

		void SceneViewBillboardSetTextures ()
		{
			//sceneViewBillboard.AddTexture (new AuxMono.Tex.TexturePosDepth (texture_0, x_0, y_0));
			//sceneViewBillboard.AddTexture (new AuxMono.Tex.TexturePosDepth (texture_1, x_1, y_1));
			//sceneViewBillboard.AddTexture (new AuxMono.Tex.TexturePosDepth (texture_2, x_2, y_2));

			for (int i = 0; i < texturePosDepths.Length; i++)
			{
				if (texturePosDepths [i] != null)
					sceneViewBillboard.AddTexture (texturePosDepths [i]);
			}
		}
		#endregion sceneViewBillboard


		#region gameObject
		//void GetGameObject ()
		//{
		//	if (IsGameObjectNotNull ())
		//		return;
		//
		//	GameObject go = new GameObject (gameObjectName);
		//	if (go == null)
		//		return;
		//	go.transform.parent = transform;
		//
		//
		//	TagsLayersIdentifiers taggedGo = go.AddComponent <TagsLayersIdentifiers> ();
		//	if (taggedGo == null)
		//		return;
		//	taggedGo.gameType = gameObjectGameType;
		//	taggedGo.layer = gameObjectLayer;
		//	taggedGo.roleTags = TagsLayersIdentifiers.RoleTags.game;
		//}
		//bool IsGameObjectNotNull ()
		//{
		//	Transform tr = transform.Find (gameObjectName);
		//	if (tr == null)
		//		return false;
		//
		//	TagsLayersIdentifiers taggedGo = tr.gameObject.GetComponent <TagsLayersIdentifiers> ();
		//	if (taggedGo == null)
		//		return false;
		//
		//	return true;
		//}


		#endregion gameObject
		#endif
	}
}