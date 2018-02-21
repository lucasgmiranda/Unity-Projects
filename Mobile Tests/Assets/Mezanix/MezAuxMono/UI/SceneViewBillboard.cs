using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mezanix
{
	[ExecuteInEditMode]
	public class SceneViewBillboard : MonoBehaviour
	{
		#if UNITY_EDITOR
		public List <AuxMono.Tex.TexturePosDepth> texPosDep = new List <AuxMono.Tex.TexturePosDepth> ();
		int texPosDepCountOld = -1;

		Camera lastActiveSceneViewCamera;

		Material material;
		const int resolution = 1024;
		Texture2D mainTexture;

		const float halfDiagTarget = 0.2f;
		const float halfDiagStep = 0.1f;
		const float halfDiagTol = 0.005f;

		//const float halfSquareMin = 0.1f;
		//const float halfSquareMax = 1f;
		const float halfSquareTarget = 0.1f;
		readonly Vector2 halfSquareStep_0 = new Vector2 (0f, 0.001f);
		readonly Vector2 halfSquareStep_1 = new Vector2 (0.6f, 1f);
		readonly Vector2 halfSquareStep_2 = new Vector2 (1f, 40f);
		readonly Vector2 halfSquareStep_3 = new Vector2 (5f, 50f);
		//const float halfSquareStepSpeed = 6f;
		const float halfSquareTol = 0.01f;

		void Start ()
		{
			AuxMono.Cam.GetLastActiveSceneViewCamera (ref lastActiveSceneViewCamera);
		}

		void LateUpdate ()
		{
			if (! enabled)
				return;

			if (AuxMono.Cam.GetLastActiveSceneViewCamera (ref lastActiveSceneViewCamera))
			{
				transform.up = lastActiveSceneViewCamera.transform.up;
				transform.forward = -lastActiveSceneViewCamera.transform.forward;
				ToHalfSquare ();
				if (material != null)
				{					
					//material.SetVector ("_TrPos", new Vector4 (
					//	transform.position.x, transform.position.y, transform.position.z, 1f));
					//Debug.Log (material.GetFloat ("_InClipSize"));
				}
				//transform.localScale = new Vector3 (
				//	1f/(transform.parent.lossyScale.x==0f?1f:transform.parent.lossyScale.x),
				//	1f/(transform.parent.lossyScale.y==0f?1f:transform.parent.lossyScale.y),
				//	1f/(transform.parent.lossyScale.z==0f?1f:transform.parent.lossyScale.z));
			}

			DrawTextures ();
			//UnityEditor.SceneView.RepaintAll ();
		}

		void OnEnable ()
		{
			if (GameDesign.GameDesign.lateUpdateAdded < 1)
			{
				UnityEditor.EditorApplication.update += LateUpdate;
				GameDesign.GameDesign.lateUpdateAdded++;
			}
		}
		void OnDisable ()
		{
			UnityEditor.EditorApplication.update -= LateUpdate;
			if (GameDesign.GameDesign.lateUpdateAdded > 0)
			{
				GameDesign.GameDesign.lateUpdateAdded--;
			}
		}

		public void Init (Shader unlitShader, Transform tr)
		{
			MeshFilter mf = gameObject.AddComponent<MeshFilter>();
			if (mf == null)
				return;
			mf.mesh = AuxMono.Meshm.VerticalPlan (2, AuxMono.Meshm.HowInverseUv.x, true);

			MeshRenderer mr = gameObject.AddComponent <MeshRenderer> ();
			if (mr == null)
				return;			
			material = new Material (unlitShader);
			mr.material = material;

			transform.position = tr.position;
			//transform.parent = tr;

			TopRight ();
		}
			
		public void AddTexture (AuxMono.Tex.TexturePosDepth trd)
		{
			texPosDep.Add (trd);
		}

		void DrawTextures ()
		{
			if (material == null)
				return;

			if (texPosDep.Count < 1)
				return;

			if (texPosDepCountOld == texPosDep.Count)
				return;
			//Debug.Log ("tes");
			texPosDep.Sort ((a, b) => a.depth.CompareTo (b.depth));

			DrawPixels ();

			texPosDepCountOld = texPosDep.Count;

			material.mainTexture = (Texture)mainTexture;
		}
		void DrawPixels ()
		{
			mainTexture = new Texture2D (resolution, resolution);
			Color [] pixels = mainTexture.GetPixels ();
			for (int i = 0; i < pixels.Length; i++)
				pixels [i] = new Color (0f, 0f, 0f, 0f);
			for (int i = 0; i < texPosDep.Count; i++)
			{
				texPosDep [i].InTexturePos (mainTexture.width, mainTexture.height);
				for (int x = texPosDep [i].Xint; x < texPosDep [i].Xint + texPosDep [i].Wint; x++)
				{
					for (int y = texPosDep [i].Yint; y < texPosDep [i].Yint + texPosDep [i].Hint; y++)
					{
						int index = x + y*mainTexture.width;
						if (AuxMono.Mathm.InRange (index, 0, mainTexture.width*mainTexture.height))
							pixels [index] += texPosDep [i].GetPixel (x, y);
					}
				}
			}
			mainTexture.SetPixels (pixels);
			mainTexture.Apply ();
		}

		#region ToViewport
		float ToLastActiveSceneViewCamera ()
		{
			return transform.position.z - lastActiveSceneViewCamera.transform.position.z;
		}

		//void ToHalfDiag ()
		//{
		//	float err = TopRightViewportDistance () / halfDiagTarget;
		//
		//	if (err >= 1f - halfDiagTol && err <= 1f + halfDiagTol)
		//		return;
		//
		//	if (err > 1f + halfDiagTol)
		//	{
		//		transform.localScale -= Vector3.one * halfDiagStep;
		//	}
		//	else if (err < 1f - halfDiagTol)
		//	{
		//		transform.localScale += Vector3.one * halfDiagStep;
		//	}
		//}

		void ToHalfSquare ()
		{
			float rightViewportDistance = RightViewportDistance ();

			//if ( ! AuxMono.Mathm.InRange (rightViewportDistance, halfSquareMin, halfSquareMax))
			//{
				//transform.localScale = oldLocalScale;
			//	return;
			//}
			//if ( ! AuxMono.Mathm.InRange (ToLastActiveSceneViewCamera (), 2f, 100f))
			//	return;

			float err = rightViewportDistance - halfSquareTarget;
			float errAbs = Mathf.Abs (err);

			if (err >= -halfSquareTol && err <= halfSquareTol)
				return;

			float step = AuxMono.Mathm.Segment3 (errAbs,
				halfSquareStep_0, halfSquareStep_1, halfSquareStep_2, halfSquareStep_3);
			
			if (err < -halfSquareTol)
			{
				transform.localScale += Vector3.one * step * 0.5f;
			}
			else if (err > halfSquareTol)
			{
				transform.localScale -= Vector3.one * step * 0.5f;
			}

			if (transform.localScale.y < 0f)
			{
				transform.localScale *= -1f;
			}
			//oldLocalScale = transform.localScale;
		}
		//Vector3 oldLocalScale = Vector3.one*0.01f;
			
		Vector3 Right ()
		{
			return transform.position + transform.right*transform.localScale.x;
		}

		Vector3 TopRight ()
		{
			return transform.position + transform.right*transform.localScale.x + transform.up*transform.localScale.y;
		}

		Vector3 TransformViewport ()
		{
			return lastActiveSceneViewCamera.WorldToViewportPoint (transform.position);
		}

		Vector3 TopRghtViewport ()
		{
			return lastActiveSceneViewCamera.WorldToViewportPoint (TopRight ());
		}

		float TopRightViewportDistance ()
		{
			return Vector3.Distance (TopRghtViewport (), TransformViewport ());
		}

		Vector3 RightViewport ()
		{
			return lastActiveSceneViewCamera.WorldToViewportPoint (Right ());
		}

		float RightViewportDistance ()
		{
			return Vector3.Distance (RightViewport (), TransformViewport ());
		}

		float RightDistance ()
		{
			return Vector3.Distance (Right (), transform.position);
		}
		#endregion ToViewport

		#endif
	}
}
