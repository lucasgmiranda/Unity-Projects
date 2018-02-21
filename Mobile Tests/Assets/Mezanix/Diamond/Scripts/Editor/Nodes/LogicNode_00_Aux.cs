using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// www.mezanix.com
/// </summary>
namespace Mezanix.Diamond
{
	/// <summary>
	/// LogicNode_00_Aux
	/// The building block of the Diamond Graph, here we have the logic executed.
	/// However, Other classes like Node, NodeState, and Logic, manage
	/// which logic and when this logic will be executed.
	/// 
	/// In the Logic Node things are executed,
	/// 	For workflow speed, the Logic Node is adaptive,
	/// 	no need to search in big lists of nodes, you create your Logic Node and you adapt it to your need.
	/// 	to do that, the Logic Node show you 3 enum buttons: Logic Type, Variable Type, and Compute Type.
	/// 	In Logic Type you choose what you want to do (define an input, compute or operation, time operation).
	/// 	In Variable Type you choose on which variabale you want to apply your Logic Type (bool, float, game object,
	/// 	transform, camera, rigidbody etc..).
	/// 	In Compute Type: according to your earlier choices (Logic Type and Variable Type), the Logic Node adapte the
	/// 	Compute Type enum and offer to you a list of operations corresponding to your choice.
	/// 	To explaine the Logic Node adaptivity, lets see some examples: 
	/// 	Example 1: In the Logic Type, choose "Compute Or Operation". In the Variable Type, choose "Camera".
	/// 	click on the Compute Type enum button and you will see a list of operations related to the camera
	/// 	like "Screen Point To Ray", .., "Get Far Clip Plan", .. "Set Field Of View" etc..
	/// 	Example 2: In the Logic Type, choose "Compute Or Operation". In the Variable Type, choose 
	/// 	"Component Transform".
	/// 	click on the Compute Type enum button and you will see a list of operations related to the transform
	/// 	like "Get Position", .. "Get Child Count", .. "Translate" etc..
	/// 	Example 3: In the Logic Type, choose "Time Operation". Click on the Time Type enum button and you will 
	/// 	see a list of variables or operations related to the time or the frames like "Delta Time", .. "TicTac One Frames", 
	/// 	.. "Tictac On Time", .. "Time Since Level Load" etc.. 
	/// 	Example 4: In the Logic Type, choose "Unity Input Class And Cross Platform". In the Compute Type choose
	/// 	"Get Axis". The Logic Node will invite you to enter your axis name and to choose if you want to use 
	/// 	cross platform inputs or not. If you have spelling errors in the axe name, the Logic Node will tell you
	/// 	that this axis is not defined in the Unity Input Manager. 
	/// 	P.S. To acess the Unity Input Manager, in th Unity editor go to top menu: Edit->Project Settings->Input
	/// 	P.S. Cross platform inputs runs only on generated scripts (no run in editor), in order to use the 
	/// 	cross platform inputs, import the Unity cross platform inputs standard asset.
	/// 	P.S In the Logic Type, instead of "Unity Input Class And Cross Platform", you can choose "Input", it's
	/// 	a simpler case of the first one. In it, you will choose essentially desktop inputs (keyboard and mouse).
	/// 	P.S In the Logic Type, you can use also "Mouse Input", the features of "Mouse Input" exists already in
	/// 	"Unity Input Class And Cross Platform", but you choose it if you want to have the mouse position 
	/// 	after freeing the cursor. Some First Person cameras may lock the cursor at the screen center,
	/// 	so sometimes you need to free it if you want to use the mouse position.
	/// </summary>

	public partial class LogicNode : ScriptableObject 
	{
		public static bool [] Aux_detectedPixels (Color id, ref Texture2D tex, float t)
		{
			Color[] colors = tex.GetPixels ();

			bool [] r = new bool[colors.Length];

			for (int i = 0; i < r.Length; i++)
			{
				r [i] = false;

				r [i] = Aux_InRange (id, colors [i], t);
			}

			return r;
		}
		public static bool [,] Aux_detectedPixels (Color[] id, ref Texture2D tex, float[] t)
		{
			if (id.Length != t.Length)
				return null;

			Color[] colors = tex.GetPixels ();

			bool [,] r = new bool[id.Length, colors.Length];

			for (int j = 0; j < r.GetLength (0); j++)
			{
				for (int i = 0; i < r.GetLength (1); i++)
				{
					r [j, i] = false;

					r [j, i] = Aux_InRange (id [j], colors [i], t [j]);
				}
			}

			return r;
		}
		public static bool [,] Aux_detectedPixels (Color[] id, ref Texture2D tex, float t)
		{
			Color[] colors = tex.GetPixels ();

			bool [,] r = new bool[id.Length, colors.Length];

			for (int j = 0; j < r.GetLength (0); j++)
			{
				for (int i = 0; i < r.GetLength (1); i++)
				{
					r [j, i] = false;

					r [j, i] = Aux_InRange (id [j], colors [i], t);
				}
			}

			return r;
		}

		public static bool Aux_InRange (Color id, Color c, float t)
		{
			
			return 
				Aux_InRange (c.r, id.r - t, id.r + t) &&
				Aux_InRange (c.g, id.g - t, id.g + t) &&
				Aux_InRange (c.b, id.b - t, id.b + t);
		}
		public static bool Aux_InRange (Color c, float min, float max)
		{
			bool _out = false;

			float r = c.r;

			float g = c.g;

			float b = c.b;

			_out = 
				Aux_InRange (r, min, max) && 
				Aux_InRange (g, min, max) && 
				Aux_InRange (b, min, max);

			return _out;
		}
		public static bool Aux_InRange (float v, float min, float max)
		{
			if (v >= min && v < max)
				return true;
			else
				return false;
		}
		public static bool Aux_InRange (int v, int min, int max)
		{
			if (v >= min && v < max)
				return true;
			else
				return false;
		}
	

	
		public static void Aux_WriteTextureToFile (ref Texture2D MSO, ref Texture2D HM, ref Texture2D NorM, string alternativeName_MSO, string alternativeName_HM, string alternativeName_NorM, string folderName)
		{
			if (MSO == null || HM == null || NorM == null)
				return;


			Aux_WriteTextureToFile (ref MSO, alternativeName_MSO, folderName);

			Aux_WriteTextureToFile (ref HM, alternativeName_HM, folderName);

			Aux_WriteTextureToFile (ref NorM, alternativeName_NorM, folderName);
		}
		public static void Aux_WriteTextureToFile (ref Texture2D MSO, ref Texture2D HM, ref Texture2D NorM, ref Texture2D alb, string alternativeName_MSO, string alternativeName_HM, string alternativeName_NorM, string alternativeName_alb, string folderName)
		{
			if (MSO == null || HM == null || NorM == null || alb == null)
				return;


			Aux_WriteTextureToFile (ref MSO, alternativeName_MSO, folderName);

			Aux_WriteTextureToFile (ref HM, alternativeName_HM, folderName);

			Aux_WriteTextureToFile (ref NorM, alternativeName_NorM, folderName);

			Aux_WriteTextureToFile (ref alb, alternativeName_alb, folderName, true);
		}
		public static string Aux_WriteTextureToFile (
			ref Texture2D MSO, ref Texture2D HM, ref Texture2D NorM, ref Texture2D alb, string alternativeName_MSO, string alternativeName_HM, string alternativeName_NorM, string alternativeName_alb, string folderName,
			bool returnAlbedoPath)
		{
			if (MSO == null || HM == null || NorM == null || alb == null)
				return "";


			Aux_WriteTextureToFile (ref MSO, alternativeName_MSO, folderName);

			Aux_WriteTextureToFile (ref HM, alternativeName_HM, folderName);

			Aux_WriteTextureToFile (ref NorM, alternativeName_NorM, folderName);

			return Aux_WriteTextureToFile (ref alb, alternativeName_alb, folderName, true);
		}

		public static void Aux_WriteTextureToFile (ref Texture2D tex, string alterName, string folderName)
		{
			TextureWriter.WriteTexture (tex, TextureEncodeType.png, 0, Aux_FileName_tex (ref tex, alterName),
				WriteTextureFormat.ARGB32, false, false, Vector2.zero, 
				new Vector2 ((float)tex.width, (float)tex.height), 
				folderName);
		}
		public static string Aux_WriteTextureToFile (ref Texture2D tex, string alterName, string folderName,
			bool returnPath)
		{
			return TextureWriter.WriteTexture (tex, TextureEncodeType.png, 0, Aux_FileName_tex (ref tex, alterName),
				WriteTextureFormat.ARGB32, false, false, Vector2.zero, 
				new Vector2 ((float)tex.width, (float)tex.height), 
				folderName);
		}

		public static string Aux_FileName_tex (ref Texture2D tex, string alterName)
		{
			string r = alterName;

			if ( ! string.IsNullOrEmpty (tex.name))
				r = tex.name;

			return r;
		}
	
	
		#region array

		public static int Aux_OnePixelIndex (int i, int j, int width)
		{
			return i + j*width;
		}

		public static float [] Aux_Divide (float [] val, float d)
		{
			float [] r = new float[val.Length];

			for (int i = 0; i < val.Length; i++)
			{
				r [i] = val [i] / d;
			}

			return r;
		}
		public static void Aux_Divide (ref float [] val, float d)
		{
			for (int i = 0; i < val.Length; i++)
			{
				val [i] /= d;
			}
		}

		public static void Aux_OldToNew (ref Texture2D [] old, ref Texture2D [] new_)
		{
			for (int i = 0; i < old.Length; i++)
			{
				if (i > new_.Length - 1)
					break;

				new_ [i] = old [i];
			}
		}
		public static void Aux_OldToNew (ref Color [] old, ref Color [] new_)
		{
			for (int i = 0; i < old.Length; i++)
			{
				if (i > new_.Length - 1)
					break;

				new_ [i] = old [i];
			}
		}
		public static void Aux_OldToNew (ref float [] old, ref float [] new_)
		{
			for (int i = 0; i < old.Length; i++)
			{
				if (i > new_.Length - 1)
					break;

				new_ [i] = old [i];
			}
		}
		public static void Aux_OldToNew (ref bool [] old, ref bool [] new_)
		{
			for (int i = 0; i < old.Length; i++)
			{
				if (i > new_.Length - 1)
					break;

				new_ [i] = old [i];
			}
		}
		#endregion array

		#region input_fields
		bool Aux_InputTextureField (int valueId, string label, string nullMessage)
		{
			DrawLogicNodeLabel (label, 0, 2);
			DrawTexture2DFieldInput (valueId, 1, 2);

			if (texture2DValues [valueId] == null)
			{
				DrawInNodeInfo (nullMessage);

				return false;
			}

			return true;
		}

		public static void Aux_Input_Multi (string fname, ref bool val, LogicNode ln)
		{
			ln.DrawLogicNodeLabel (fname, 0, 2);
			val = EditorGUI.Toggle (ln.GetSuitableRect (FieldDrawType.label, 1, 2), val);
		}
		public static void Aux_Input_Multi (string fname, ref float val, float min, float max, LogicNode ln)
		{
			ln.DrawLogicNodeLabel (fname, 0, 2);
			val = EditorGUI.FloatField (ln.GetSuitableRect (FieldDrawType.label, 1, 2), val);
			val = Mathf.Clamp (val, min, max);
		}
		public static void Aux_Input_Multi (string fname, ref float [] val_old, ref float [] val, float min, float max, LogicNode ln)
		{
			int columns = val.Length + 1;

			ln.DrawLogicNodeLabel (fname, 0, columns);

			for (int i = 0; i < columns-1; i++)
			{
				val [i] = EditorGUI.FloatField (ln.GetSuitableRect (FieldDrawType.label, i+1, columns), val [i]);
				val [i] = Mathf.Clamp (val [i], min, max);
			}

			val_old = val;
		}
		public static void Aux_Input_Multi (string fname, ref Texture2D [] val_old, ref Texture2D [] val, LogicNode ln)
		{
			int columns = val.Length + 1;

			ln.DrawLogicNodeLabel (fname, 0, columns);

			for (int i = 0; i < columns-1; i++)
			{
				val [i] = 
					EditorGUI.ObjectField (ln.GetSuitableRect (FieldDrawType.label, i+1, columns), 
						val [i], typeof (Texture2D), true) 
					as Texture2D;
			}

			val_old = val;
		}
		public static void Aux_Input_Multi (string fname, ref Color [] val_old, ref Color [] val, LogicNode ln)
		{
			int columns = val.Length + 1;

			ln.DrawLogicNodeLabel (fname, 0, columns);

			for (int i = 0; i < columns-1; i++)
			{
				val [i] = EditorGUI.ColorField (ln.GetSuitableRect (FieldDrawType.label, i+1, columns), new GUIContent (""), val [i],
					false, false, false, new ColorPickerHDRConfig (0f, 0f, 0f, 0f));
			}

			val_old = val;
		}
		public static void Aux_Input_Multi (string fname, ref Color [] val_old, ref Color [] val, LogicNode ln, bool withAlphaAndPicker)
		{
			int columns = val.Length + 1;

			ln.DrawLogicNodeLabel (fname, 0, columns);

			for (int i = 0; i < columns-1; i++)
			{
				val [i] = EditorGUI.ColorField (ln.GetSuitableRect (FieldDrawType.label, i+1, columns), val [i]);
			}

			val_old = val;
		}
		public static void Aux_Input_Multi (string fname, ref bool [] val_old, ref bool [] val, LogicNode ln)
		{
			int columns = val.Length + 1;

			ln.DrawLogicNodeLabel (fname, 0, columns);

			for (int i = 0; i < columns-1; i++)
			{
				val [i] = EditorGUI.Toggle (ln.GetSuitableRect (FieldDrawType.label, i+1, columns), val [i]);
			}

			val_old = val;
		}

		public static void Aux_Label_Multi (string fname, string [] val, LogicNode ln)
		{
			int columns = val.Length + 1;

			ln.DrawLogicNodeLabel (fname, 0, columns);

			for (int i = 0; i < columns-1; i++)
			{
				ln.DrawLogicNodeLabel (val [i], i+1, columns);
			}
		}
		#endregion input_fields

		#region wood
		public static float [] Aux_Wood_values (Vector2 coordAnisoFreq, Vector2 coordOffset, float wood_g, 
			int octaves, bool woodify, float wood_g_persistence, float persistence, float lacunarity, 
			int seed, int width, int height)
		{
			float [] r = new float [width * height];

			float [,] r2 = new float [width, height];

			float maxNoiseHeight = float.MinValue;
			float minNoiseHeight = float.MaxValue;

			for (int i = 0; i < r2.GetLength (0); i++)
			{
				for (int j = 0; j < r2.GetLength (1); j++)
				{
					//int index = Aux_OnePixelIndex (i, j, r2.GetLength (0));
					int index = Aux_OnePixelIndex (j, i, r2.GetLength (1));

					//j_norm = Mathf.Cos (j_norm);
					//i_norm = Mathf.Cos (i_norm);

					float amplitude = 1f;
					float wood_g_curr = wood_g;

					float freq = 1f;

					Vector2 [] octaveOffsets = Aux_OctavesOffsets (octaves, coordOffset, seed);

					r [index] = 0f;
					for (int k = 0; k < octaves; k++)
					{
						float j_norm = ((float)j / (float)r2.GetLength (1));
						float i_norm = ((float)i / (float)r2.GetLength (0));

						float preOffset = -0.5f;
						j_norm += preOffset;
						i_norm += preOffset;

						j_norm *= coordAnisoFreq.y;
						i_norm *= coordAnisoFreq.x;

						j_norm += octaveOffsets [k].y;
						i_norm += octaveOffsets [k].x;


						i_norm *= freq;
						j_norm *= freq;

						float perlinValue = Mathf.PerlinNoise (i_norm, j_norm);

						//perlinValue *= 2f;

						//perlinValue += -1f;

						perlinValue *= amplitude;


						r [index] += perlinValue;

						if (woodify)
						{
							r [index] *= wood_g_curr;
							r [index] -= Mathf.Floor (r [index]);

							wood_g_curr *= wood_g_persistence;
						}

						amplitude *= persistence;
						freq *= lacunarity;
					}

					if (r [index] > maxNoiseHeight) 
					{
						maxNoiseHeight = r [index];
					} 
					else if (r [index] < minNoiseHeight) 
					{
						minNoiseHeight = r [index];
					}
				}
			}

			for (int j = 0; j < r.Length; j++)
			{
				r [j] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, r [j]);

				//r [j] = Sigmoid (r [j], sigmoidSpeedGlobal);
			}

			return r;
		} 
	
		public static float [,] Aux_Wood_values (Vector2 coordAnisoFreq, Vector2 coordOffset, float wood_g, 
			int octaves, bool woodify, float wood_g_persistence, float persistence, float lacunarity, 
			int seed, int width, int height, bool Out2D)
		{
			//float [] r = new float [width * height];

			float [,] r2 = new float [width, height];

			float maxNoiseHeight = float.MinValue;
			float minNoiseHeight = float.MaxValue;

			//Vector2 [] octaveOffsets = Aux_OctavesOffsets (width, height, seed, octaves, coordOffset);
			Vector2 [] octaveOffsets = Aux_OctavesOffsets (octaves, coordOffset, seed);

			// 
			for (int i = 0; i < r2.GetLength (0); i++)
			{
				for (int j = 0; j < r2.GetLength (1); j++)
				{

					//int index = Aux_OnePixelIndex (i, j, r2.GetLength (0));
					//int index = Aux_OnePixelIndex (j, i, r2.GetLength (1));

					//j_norm = Mathf.Cos (j_norm);
					//i_norm = Mathf.Cos (i_norm);

					float amplitude = 1f;
					float wood_g_curr = wood_g*1f;

					float freq = 1f;



					//r [index] = 0f;
					r2 [i, j] = 0f;
					for (int k = 0; k < octaves; k++)
					{
						float j_norm = ((float)j / (float)r2.GetLength (1));
						float i_norm = ((float)i / (float)r2.GetLength (0));

						//float preOffset = -0.5f;
						//j_norm += preOffset;
						//i_norm += preOffset;


					

						j_norm *= coordAnisoFreq.x;
						i_norm *= coordAnisoFreq.y;

						i_norm *= freq;
						j_norm *= freq;

						j_norm += octaveOffsets [k].y;
						i_norm += octaveOffsets [k].x;
					

		

						float perlinValue = Mathf.PerlinNoise (i_norm, j_norm);

						//perlinValue *= 2f;

						//perlinValue += -1f;

						perlinValue *= amplitude;


						//r [index] += perlinValue;
						r2 [i, j] += perlinValue;

						if (woodify)
						{
							//r [index] *= wood_g_curr;
							//r [index] -= Mathf.Floor (r [index]);

							//float r2_opp = 1f - r2 [i, j];
							//r2_opp *= wood_g_curr;
							//
							//float r2_inv = 1f/((r2 [i, j]==0f)?0.01f: r2 [i, j]);
							//r2_inv *= wood_g_curr;

							r2 [i, j] *= wood_g_curr;
									
							r2 [i, j] -= Mathf.Floor (r2 [i, j]);

							wood_g_curr *= wood_g_persistence;
							//wood_g_curr *= Mathf.Pow (r2 [i, j], 0.1f);

							//float r2_round = r2 [i, j] - Mathf.Round (r2 [i, j]);
							//float r2_floor = r2 [i, j] - Mathf.Floor (r2 [i, j]);
							//float r2_opp_floor = r2_opp - Mathf.Floor (r2_opp);
							//float r2_inv_floor = r2_inv - Mathf.Floor (r2_inv);
							//float r2_floor_param = r2 [i, j] - 1.5f*Mathf.Floor (r2 [i, j]);
							//float r2_sin = r2 [i, j] - Mathf.Sin (r2 [i, j]*1f);
							//float r2_sin_floor = Mathf.Sin (r2_floor);



							//r2 [i, j] = Mathf.Lerp (r2_floor, r2_round, 0f);
							//r2 [i, j] = r2_floor * r2_round;
							//r2 [i, j] = Mathf.Lerp (r2_floor + r2_round, r2_floor * r2_round, 0f);
							//r2 [i, j] = Mathf.Lerp (r2_floor, r2 [i, j], 0f);
							//r2 [i, j] = r2_floor + r2_opp_floor;
							//r2 [i, j] = r2_floor + r2_inv_floor;
							//r2 [i, j] = r2_floor;

							//r2 [i, j] = Aux_Smooth_middle_lerp (
							//	Aux_Smooth_middle_lerp (
							//		r2 [i, j], 0.1f), 0.35f) ;

							//r2 [i, j] = Aux_RoundSmooth_middle_lerp (
							//	Aux_Smooth_middle_lerp (
							//		r2 [i, j], 0.1f), 0.1f) ;


						}

						amplitude *= persistence;
						freq *= lacunarity;
					}

					//if (r [index] > maxNoiseHeight) 
					//{
					//	maxNoiseHeight = r [index];
					//} 
					//else if (r [index] < minNoiseHeight) 
					//{
					//	minNoiseHeight = r [index];
					//}

					if (r2 [i, j] > maxNoiseHeight) 
					{
						maxNoiseHeight = r2 [i, j];
					} 
					else if (r2 [i, j] < minNoiseHeight) 
					{
						minNoiseHeight = r2 [i, j];
					}
				}
			}

			Aux_InverseLerpHeights (ref r2, minNoiseHeight, maxNoiseHeight, 0f);

			//for (int j = 0; j < r.Length; j++)
			//{
			//	r [j] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, r [j]);
			//
			//	//r [j] = Sigmoid (r [j], sigmoidSpeedGlobal);
			//}

			//return r;
			return r2;
		} 

		public static Vector2 [] Aux_OctavesOffsets (int octaves, Vector2 offset, int seed)
		{
			Vector2 [] r = new Vector2 [octaves];

			System.Random prn = new System.Random (seed);

			for (int i = 0; i < r.Length; i++)
			{
				r [i] = new Vector2 (
					0.5f * (float)prn.Next (-100000, 100000) + offset.x,
					0.5f * (float)prn.Next (-100000, 100000) + offset.y);
			}

			return r;
		}

		public static Vector2 [] Aux_OctavesOffsets (int width, int height, int s, int n, Vector2 offs)
		{
			System.Random prng = new System.Random (s);

			Vector2[] octaveOffsets = new Vector2[n];

			for (int i = 0; i < octaveOffsets.Length; i++) 
			{
				float offsetX = 0.5f * (float)prng.Next (-width, width) + offs.x;

				float offsetY = 0.5f * (float)prng.Next (-height, height) + offs.y;

				octaveOffsets [i] = new Vector2 (offsetX, offsetY);
			}

			return octaveOffsets;
		}

		public static void Aux_InverseLerpHeights (ref float [,] heights, 
			float minNoiseHeight, float maxNoiseHeight)
		{
			for (int i = 0; i < heights.GetLength (0); i++)
			{
				for (int j = 0; j < heights.GetLength (1); j++)
				{
					heights [i, j] = Mathf.InverseLerp (
						minNoiseHeight, maxNoiseHeight, heights [i, j]);
				}
			}
		}
		public static void Aux_InverseLerpHeights (ref float [,] heights, 
			float minNoiseHeight, float maxNoiseHeight, float epsAboveZero)
		{
			for (int i = 0; i < heights.GetLength (0); i++)
			{
				for (int j = 0; j < heights.GetLength (1); j++)
				{
					heights [i, j] = Mathf.InverseLerp (
						minNoiseHeight, maxNoiseHeight, heights [i, j]) + epsAboveZero;
				}
			}


		}

		public static float [] Aux_Fibers_values_vertical (int fiberFreq, int width, int height)
		{
			float [] r = new float [width * height];

			float [,] r2 = new float [width, height];

			int pavStepCounter_i = UnityEngine.Random.Range (-1, r2.GetLength (0)-1);
			int pavStepCounter_j = UnityEngine.Random.Range (-1, r2.GetLength (1)-1);
			bool pavBlack = true;

			int pavStep_i =  r2.GetLength (0) / fiberFreq;
			int pavStep_j =  r2.GetLength (1) / fiberFreq;


			for (int i = 0; i < r2.GetLength (0); i++)
			{
				for (int j = 0; j < r2.GetLength (1); j++)
				{
					int index = Aux_OnePixelIndex (i, j, r2.GetLength (0));

					//float j_norm = ((float)j / (float)r2.GetLength (1));
					//float i_norm = ((float)i / (float)r2.GetLength (0));


					pavStepCounter_j++;
					if (pavStepCounter_j > pavStep_j)
					{
						pavStepCounter_j = UnityEngine.Random.Range (-1, r2.GetLength (1)-1);
						pavBlack = ! pavBlack;
					}

					r [index] = pavBlack? 0f : 1f;
				}
				pavStepCounter_j = UnityEngine.Random.Range (-1, r2.GetLength (1)-1);
				if (fiberFreq % 2 == 0)
				{
					pavBlack = ! pavBlack;
				}

				pavStepCounter_i++;
				if (pavStepCounter_i > pavStep_i)
				{
					pavStepCounter_i = UnityEngine.Random.Range (-1, r2.GetLength (0)-1);
					pavBlack = ! pavBlack;
				}
			}

			return r;
		}

		public static float [] Aux_Fibers_values (int fiberFreq, int width, int height)
		{
			float [] r = new float [width * height];

			float [,] r2 = new float [width, height];

			int pavStepCounter_i = UnityEngine.Random.Range (-1, r2.GetLength (0)-1);
			int pavStepCounter_j = UnityEngine.Random.Range (-1, r2.GetLength (1)-1);
			bool pavBlack = true;

			int pavStep_i =  r2.GetLength (0) / fiberFreq;
			int pavStep_j =  r2.GetLength (1) / fiberFreq;


			for (int j = 0; j < r2.GetLength (1); j++)
			{
				for (int i = 0; i < r2.GetLength (0); i++)
				{
					int index = Aux_OnePixelIndex (i, j, r2.GetLength (0));

					//float j_norm = ((float)j / (float)r2.GetLength (1));
					//float i_norm = ((float)i / (float)r2.GetLength (0));


					pavStepCounter_i++;
					if (pavStepCounter_i > pavStep_i)
					{
						pavStepCounter_i = UnityEngine.Random.Range (-1, r2.GetLength (0)-1);
						pavBlack = ! pavBlack;
					}

					r [index] = pavBlack? 0f : 1f;
				}
				pavStepCounter_i = UnityEngine.Random.Range (-1, r2.GetLength (0)-1);
				if (fiberFreq % 2 == 0)
				{
					pavBlack = ! pavBlack;
				}

				pavStepCounter_j++;
				if (pavStepCounter_j > pavStep_j)
				{
					pavStepCounter_j = UnityEngine.Random.Range (-1, r2.GetLength (1)-1);
					pavBlack = ! pavBlack;
				}
			}

			return r;
		}

		public static float [] Aux_Pavement_values (int width, int height, int pavFreq_x, int pavFreq_y, bool pavWithGap, int pavGap_x, int pavGap_y)
		{
			float [] r = new float [width * height];

			float [,] r2 = new float [width, height];


			//if (pavFreq_x % 2 != 0)
			//{
			//	pavFreq_x++;
			//}



			//if (pavGap_x % 2 != 0)
			//{
			//	pavGap_x++;
			//}
			//if (pavGap_y % 2 != 0)
			//{
			//	pavGap_y++;
			//}

			int pavStep_i_big =  r2.GetLength (0) / pavFreq_x;
			int pavStep_j_big =  r2.GetLength (1) / pavFreq_y;

			int pavStep_i =  pavStep_i_big;
			int pavStep_j =  pavStep_j_big;

			if (pavWithGap)
			{
				pavStep_i_big -= (pavGap_x/2);
				pavStep_j_big -= (pavGap_y/2);

				pavStep_i = pavStep_i_big;
				pavStep_j = pavStep_j_big;
			}

			int pavOffset = UnityEngine.Random.Range (-1, pavStep_i) + 7*pavGap_x;
			//if (pavOffset >= pavStep_i/2 - pavGap_x && pavOffset <= pavStep_i/2 + pavGap_x)
			//{
			//	pavOffset += ((pavOffset==pavStep_i/2)?pavStep_i/3:0);
			//}
			//Debug.Log (pavOffset);

			//int firstLinePavOffset = pavOffset;


			int pavStepCounter_i = pavOffset;
			int pavStepCounter_j = -1;
			bool pavBlack = false;
			bool firstLinePavBlack = true;

			for (int j = 0; j < r2.GetLength (1); j++)
			{
				firstLinePavBlack = pavBlack;

				for (int i = 0; i < r2.GetLength (0); i++)
				{
					int index = Aux_OnePixelIndex (i, j, r2.GetLength (0));

					//float j_norm = ((float)j / (float)r2.GetLength (1));
					//float i_norm = ((float)i / (float)r2.GetLength (0));

					pavStepCounter_i++;
					if (pavStepCounter_i > pavStep_i)
					{
						pavStepCounter_i = -1;

						if (pavWithGap)
						{
							if (pavStep_i == pavGap_x)
							{
								pavStep_i = pavStep_i_big;

								if (pavStep_j != pavGap_y)
								{
									pavBlack = false;
								}
							}
							else if (pavStep_i != pavGap_x)
							{
								pavStep_i = pavGap_x;

								if (pavStep_j != pavGap_y)
								{
									pavBlack = true;
								}
							}
						}
						else if ( ! pavWithGap)
						{
							pavBlack = ! pavBlack;
						}
					}

					if (pavWithGap)
					{
						if (j < pavGap_y)
						{
							pavBlack = true;
						}
					}

					r [index] = pavBlack? 0f : 1f;
				}
				pavBlack = firstLinePavBlack;



				pavStepCounter_j++;
				if (pavStepCounter_j > pavStep_j)
				{
					pavStepCounter_j = -1;

					pavOffset = UnityEngine.Random.Range (-1, pavStep_i) + 7*pavGap_x;
					//if (pavOffset >= pavStep_i/2 - pavGap_x && pavOffset <= pavStep_i/2 + pavGap_x)
					//{
					//	pavOffset += ((pavOffset==pavStep_i/2)?pavStep_i/3:0);
					//}
					//Debug.Log (pavOffset);



					//if (pavFreq_x % 2 != 0)
					//{
					//	pavBlack = firstLinePavBlack;
					//}

					if (pavWithGap)
					{
						//if (r2.GetLength (1) -1-pavGap_y - j < pavStep_j_big)
						//{
						//	pavOffset = firstLinePavOffset;
						//}

						if (pavStep_j == pavGap_y)
						{
							pavStep_j = pavStep_j_big;

							pavBlack = false;
						}
						else if (pavStep_j != pavGap_y)
						{
							pavStep_j = pavGap_y;

							pavBlack = true;
						}
					}
					else if ( ! pavWithGap)
					{
						pavBlack = ! pavBlack;
					}
				}

				pavStepCounter_i = pavOffset;
			}

			return r;
		}


		#endregion wood

		#region wood_turbulence
		public static float [] Aux_TurbulenceForWoodTexture (int width, int height, float turbSize, float turbStrength, float freq,
			Vector2 anisoFreq, bool filterResult, float resultCutoff, bool inverseCutoff)
		{
			float turb = 0f;

			float [] r = new float[width * height];

			float rV = 0f;

			for (int j = 0; j < height; j++)
			{
				for (int i = 0; i  < width; i++)
				{
					int index = Aux_OnePixelIndex (i, j, width);

					float j_Norm = (float)j / (float)height;
					float i_Norm = (float)i / (float)width;

					float j_normScaled = j_Norm * anisoFreq.y * freq;
					float i_normScaled = i_Norm * anisoFreq.x * freq;

					turb = Aux_Turbulence (i_normScaled, j_normScaled, turbSize) * turbStrength;

					rV = Mathf.Sin (turb);

					r [index] = rV;

					if (filterResult)
					{
						r [index] = (rV - resultCutoff) / (1f - resultCutoff);

						if (inverseCutoff)
							r [index] = (resultCutoff - rV) / (1f - resultCutoff);
					}
				}
			}

			return r;
		}

		public static Texture2D Aux_TurbulenceInTexture2D_wood (int texWidth, int texHeight, float turbSize, float turbStrength,
			float frequency, Vector2 anisoFrequency, Color color_0, Color color_1, bool filterResult, 
			float resultCutoff, bool inversCutoff)
		{
			Texture2D tex = new Texture2D (texWidth, texHeight);

			Color [] colors = new Color [tex.width * tex.height];

			float [] values = Aux_TurbulenceForWoodTexture (tex.width, tex.height, turbSize,
				turbStrength, frequency, anisoFrequency, filterResult, resultCutoff, inversCutoff);

			for (int j = 0; j < values.Length; j++)
			{
				colors [j] = Color.Lerp (color_0, color_1, values [j]);
			}

			tex.SetPixels (colors);
			tex.Apply ();

			return tex;
		}

		#endregion wood_turbulence

		#region Perlin

		public static float Aux_Turbulence (float x, float y, float size)
		{
			float r = 0f;
			float v = 0f;

			float initSize = size;

			while (size >= 1f)
			{
				v += Mathf.PerlinNoise (x / size, y / size) * size;

				size *= 0.5f;
			}

			r = v / initSize;

			return r;
		}
		#endregion Perlin

		#region texture
		public static void Aux_WriteTextureDoubleTest (string name_0, string name_1,
			float v_0, float v_1, float [,] heights, string testName, Texture2D tex)
		{
			string textureName = "" +
				name_0 + StringTreatment.FloatToFileName (v_0.ToString ()) + "__" +
				name_1 + StringTreatment.FloatToFileName (v_1.ToString ());

			Aux_ValuesToGrayscaleTexture2D (ref heights, textureName, ref tex);

			TextureWriter.WriteTexture (tex, TextureEncodeType.png, 0, textureName,
				WriteTextureFormat.ARGB32, false, false, Vector2.zero, 
				new Vector2 ((float)tex.width, (float)tex.height), 
				testName);
		}

		public static void Aux_WriteTextureDoubleTest (string name_0, string name_1,
			float v_0, float v_1, float [] heights, string testName, Texture2D tex)
		{
			string textureName = "" +
				name_0 + StringTreatment.FloatToFileName (v_0.ToString ()) + "__" +
				name_1 + StringTreatment.FloatToFileName (v_1.ToString ());

			Aux_ValuesToGrayscaleTexture2D_noInverseLerp (ref heights, textureName, ref tex);

			TextureWriter.WriteTexture (tex, TextureEncodeType.png, 0, textureName,
				WriteTextureFormat.ARGB32, false, false, Vector2.zero, 
				new Vector2 ((float)tex.width, (float)tex.height), 
				testName);
		}

		//toDel
		public static void Aux_ValuesToGrayscaleTexture2D (ref float [,] values, string namemap, 
			ref Texture2D tex)
		{
			tex = new Texture2D (values.GetLength (0), values.GetLength (1));

			Color [] colors = new Color [values.GetLength (0) * values.GetLength (1)];

			values = Aux_InverseLerp (ref values);

			for (int i = 0; i < values.GetLength (0); i++)
			{
				for (int j = 0; j < values.GetLength (1); j++)
				{
					colors [i * values.GetLength (1) + j] = Color.Lerp (
						Color.black, Color.white, values [i, j]);
				}
			}

			tex.SetPixels (colors);

			tex.Apply ();

			tex.name = namemap;
		}

		public static void Aux_ValuesToGrayscaleTexture2D (ref float [] values, string namemap, ref Texture2D tex)
		{
			//tex = new Texture2D (values.GetLength (0), values.GetLength (1));

			Color [] colors = new Color [values.Length];

			values = Aux_InverseLerp (ref values);

			for (int i = 0; i < values.Length; i++)
			{
				colors [i] = Color.Lerp (Color.black, Color.white, values [i]);
			}

			tex.SetPixels (colors);

			tex.Apply ();

			tex.name = namemap;
		}
		public static void Aux_ValuesToGrayscaleTexture2D_noInverseLerp (ref float [] values, string namemap, ref Texture2D tex)
		{
			//tex = new Texture2D (values.GetLength (0), values.GetLength (1));

			Color [] colors = new Color [values.Length];

			//values = InverseLerp (ref values);

			for (int i = 0; i < values.Length; i++)
			{
				colors [i] = Color.Lerp (Color.black, Color.white, values [i]);
			}

			tex.SetPixels (colors);

			tex.Apply ();

			tex.name = namemap;
		}
		public static void Aux_ValuesToGrayscaleTexture2D_noInverseLerp (ref float [,] values,
			string namemap, ref Texture2D tex)
		{
			//tex = new Texture2D (values.GetLength (0), values.GetLength (1));

			Color [] colors = new Color [values.GetLength (0) * values.GetLength (1)];

			//values = InverseLerp (ref values);

			for (int i = 0; i < values.GetLength (0); i++)
			{
				for (int j = 0; j < values.GetLength (1); j++)
				{
					colors [i * values.GetLength (1) + j] = 
						Color.Lerp (Color.black, Color.white, values [i, j]);
				}
			}

			tex.SetPixels (colors);

			tex.Apply ();

			tex.name = namemap;
		}


		public static void Aux_ValuesToGrayscaleTexture2D_noInverseLerp_blur (ref float [,] values,
			string namemap, ref Texture2D tex)
		{
			//tex = new Texture2D (values.GetLength (0), values.GetLength (1));

			Color [] colors = new Color [values.GetLength (0) * values.GetLength (1)];

			//values = InverseLerp (ref values);

			for (int i = 0; i < values.GetLength (0); i++)
			{
				for (int j = 0; j < values.GetLength (1); j++)
				{
					colors [i * values.GetLength (1) + j] = 
						Color.Lerp (Color.black, Color.white, values [i, j]);
				}
			}

			tex.SetPixels (colors);

			//tex = Aux_Blur (tex, 2);
			//Aux_FastBlur (ref tex, (int)((4f/1024f)*(float)Math.Max (tex.width, tex.height)), 2);

			Color c_Blur = Color.black;
			int blurPixelCount = 0;
			int blurRad = Mathf.FloorToInt((1.5f/512f)*(float)Math.Max (tex.width, tex.height));
			if (Math.Max (tex.width, tex.height) > 2047)
			{
				blurRad = Mathf.FloorToInt((4f/2048f)*(float)Math.Max (tex.width, tex.height));
			}
			blurRad = Mathf.Clamp (blurRad, 2, 10);
			int blurPass = (int)(512f/(float)Math.Max (tex.width, tex.height));
			blurPass = Mathf.Clamp (blurPass, 1, 10);

			Aux_FastBlurMezanix (ref tex, blurRad, blurPass, ref c_Blur, ref blurPixelCount);

			tex.Apply ();

			tex.name = namemap;
		}
	
		public static float Aux_Texture2DSumValues (ref Texture2D tex)
		{
			float r = 0f;

			float [] rValues = Aux_ColorToValueValue ( tex.GetPixels () );

			for (int i = 0; i < rValues.Length; i++)
			{
				r += rValues [i];
			}

			return r;
		}

		public static void Aux_ColorToValue (ref Texture2D tex)
		{
			tex.SetPixels ( Aux_ColorToValue (tex.GetPixels ()) );
			tex.Apply ();
		}

		public static void Aux_ColorToValue (ref Texture2D inp, ref Texture2D outp)
		{
			outp = new Texture2D (inp.width, inp.height);

			outp.SetPixels ( Aux_ColorToValue (inp.GetPixels ()) );
			outp.Apply ();
		}

		public static float [] Aux_ColorToValueValue (Color [] c)
		{
			float [] r = new float [c.Length];

			for (int i = 0; i < r.Length; i++)
			{
				r [i] = Aux_ColorToValueValue (c [i]);
			}

			return r;
		}

		public static Color [] Aux_ColorToValue (Color [] c)
		{
			Color [] r = new Color [c.Length];

			for (int i = 0; i < r.Length; i++)
			{
				r [i] = Aux_ColorToValue (c [i]);
			}

			return r;
		}

		public static Color Aux_ColorToValue (Color c)
		{
			return Aux_ValueToGRayscalColor (ColorsArithmetic.RGBToHSV (c) [2]);
		}

		public static float Aux_ColorToValueValue (Color c)
		{
			return ColorsArithmetic.RGBToHSV (c) [2];
		}

		public static Color [] Aux_ValueToGRayscalColor (float [] v)
		{
			Color [] r = new Color [v.Length];

			for (int i = 0; i < r.Length; i++)
			{
				r [i] = Aux_ValueToGRayscalColor (v [i]);
			}

			return r;
		}

		public static Color Aux_ValueToGRayscalColor (float v)
		{
			return Color.Lerp (Color.black, Color.white, v);
		}


		public static Texture2D Aux_ColorValueAbsoluteDifference (ref Texture2D tex_0, ref Texture2D tex_1)
		{
			Texture2D r = new Texture2D (Mathf.Min (tex_0.width, tex_1.width), Mathf.Min (tex_0.height, tex_1.height));

			float [] rValues = Aux_ColorValueAbsoluteDifference (tex_0.GetPixels (), tex_1.GetPixels ());

			r.SetPixels ( Aux_ValueToGRayscalColor (rValues) );
			r.Apply ();

			return r;
		}

		public static float [] Aux_ColorValueAbsoluteDifference (Color [] c_0, Color [] c_1)
		{
			float [] r = new float [Mathf.Min (c_0.Length, c_1.Length)];

			for (int i = 0; i < r.Length; i++)
			{
				r [i] = Aux_ColorValueAbsoluteDifference (c_0 [i], c_1 [i]);
			}

			return r;
		}

		public static float Aux_ColorValueAbsoluteDifference (Color c_0, Color c_1)
		{
			return Mathf.Abs (ColorsArithmetic.RGBToHSV (c_0)[2] - ColorsArithmetic.RGBToHSV (c_1)[2]);
		}


		public static Texture2D Aux_CombineTexturesPixels (ref Texture2D tex_0, ref Texture2D tex_1, float t)
		{
			Texture2D r = new Texture2D (Mathf.Min (tex_0.width, tex_1.width), Mathf.Min (tex_0.height, tex_1.height));

			Color [] colores = r.GetPixels ();
			Color [] colores_0 = tex_0.GetPixels ();
			Color [] colores_1 = tex_1.GetPixels ();

			for (int i = 0; i < colores.Length; i++)
			{
				if (Aux_InRange (i, 0, colores_0.Length) &&
					Aux_InRange (i, 0, colores_1.Length))
				{
					colores [i] = Color.Lerp (colores_0 [i], colores_1 [i], t);
				}
				else if (Aux_InRange (i, 0, colores_0.Length))
				{
					colores [i] = colores_0 [i];
				}
				else if (Aux_InRange (i, 0, colores_1.Length))
				{
					colores [i] = colores_1 [i];
				}
			}

			r.SetPixels (colores);
			r.Apply ();

			return r;
		}

		public static Texture2D Aux_CombineNormalsTexturesPixels (ref Texture2D tex_0, ref Texture2D tex_1, float t)
		{
			Texture2D r = new Texture2D (Mathf.Min (tex_0.width, tex_1.width), Mathf.Min (tex_0.height, tex_1.height));

			Color [] colores = r.GetPixels ();
			Color [] colores_0 = tex_0.GetPixels ();
			Color [] colores_1 = tex_1.GetPixels ();

			for (int i = 0; i < colores.Length; i++)
			{
				if (Aux_InRange (i, 0, colores_0.Length) &&
					Aux_InRange (i, 0, colores_1.Length))
				{
					colores [i] = Aux_CombineNormals (colores_0 [i], colores_1 [i], t);
				}
				else if (Aux_InRange (i, 0, colores_0.Length))
				{
					colores [i] = colores_0 [i];
				}
				else if (Aux_InRange (i, 0, colores_1.Length))
				{
					colores [i] = colores_1 [i];
				}
			}

			r.SetPixels (colores);
			r.Apply ();

			return r;
		}

		public static Color Aux_CombineNormals (Color c_0, Color c_1, float t)
		{
			Color c;

			c = new Color (
				(1f-t)*c_0.r + t*c_1.r, 
				(1f-t)*c_0.g + t*c_1.g, 1f, 1f);

			return c;
		}

		//toDel
		public static Texture2D Aux_InvertTexturesPixelsValue (ref Texture2D tex_0)
		{
			Texture2D r = new Texture2D (tex_0.width, tex_0.height);

			Color [] colores = r.GetPixels ();
			Color [] colores_0 = tex_0.GetPixels ();

			for (int i = 0; i < colores.Length; i++)
			{
				colores [i] = Color.HSVToRGB (
					ColorsArithmetic.RGBToHSV (colores_0 [i])[0],
					ColorsArithmetic.RGBToHSV (colores_0 [i])[1],
					1f - ColorsArithmetic.RGBToHSV (colores_0 [i])[2] );
			}

			r.SetPixels (colores);
			r.Apply ();

			return r;
		}

		public static Texture2D Aux_AddTexturesPixels (ref Texture2D tex_0, ref Texture2D tex_1, bool alphaMultiplicatif)
		{
			if ( ! alphaMultiplicatif)
			{
				return Aux_AddTexturesPixels (ref tex_0, ref tex_1);
			}
			else if (alphaMultiplicatif)
			{
				return Aux_AddTexturesPixels_AlphaMultiplicatif (ref tex_0, ref tex_1);
			}
			return Aux_AddTexturesPixels (ref tex_0, ref tex_1);
		}

		public static Texture2D Aux_AddTexturesPixels (ref Texture2D tex_0, ref Texture2D tex_1)
		{
			Texture2D r = new Texture2D (Mathf.Min (tex_0.width, tex_1.width), Mathf.Min (tex_0.height, tex_1.height));

			Color [] colores = r.GetPixels ();
			Color [] colores_0 = tex_0.GetPixels ();
			Color [] colores_1 = tex_1.GetPixels ();

			for (int i = 0; i < colores.Length; i++)
			{
				if (Aux_InRange (i, 0, colores_0.Length) &&
					Aux_InRange (i, 0, colores_1.Length))
				{
					colores [i] = colores_0 [i] + colores_1 [i];
				}
				else if (Aux_InRange (i, 0, colores_0.Length))
				{
					colores [i] = colores_0 [i];
				}
				else if (Aux_InRange (i, 0, colores_1.Length))
				{
					colores [i] = colores_1 [i];
				}
			}

			r.SetPixels (colores);
			r.Apply ();

			return r;
		}

		public static Texture2D Aux_AddTexturesPixels_AlphaMultiplicatif (ref Texture2D tex_0, ref Texture2D tex_1)
		{
			Texture2D r = new Texture2D (Mathf.Min (tex_0.width, tex_1.width), Mathf.Min (tex_0.height, tex_1.height));

			Color [] colores = r.GetPixels ();
			Color [] colores_0 = tex_0.GetPixels ();
			Color [] colores_1 = tex_1.GetPixels ();

			for (int i = 0; i < colores.Length; i++)
			{
				if (Aux_InRange (i, 0, colores_0.Length) &&
					Aux_InRange (i, 0, colores_1.Length))
				{
					colores [i] = new Color (
						colores_0 [i].r + colores_1 [i].r,
						colores_0 [i].g + colores_1 [i].g,
						colores_0 [i].b + colores_1 [i].b,
						colores_0 [i].a*colores_1 [i].a);
				}
				else if (Aux_InRange (i, 0, colores_0.Length))
				{
					colores [i] = colores_0 [i];
				}
				else if (Aux_InRange (i, 0, colores_1.Length))
				{
					colores [i] = colores_1 [i];
				}
			}

			r.SetPixels (colores);
			r.Apply ();

			return r;
		}


		public static void Aux_WriteTextureToFile (ref Texture2D tex, string alternativeName,
			LogicNode ln)
		{
			if (tex == null)
				return;

			string fileName = alternativeName;

			if ( ! string.IsNullOrEmpty (tex.name))
				fileName = tex.name;

			if (GUI.Button (ln.GetSuitableRect (FieldDrawType.label), "Write To File"))
			{
				TextureWriter.WriteTexture (tex, TextureEncodeType.png, 0, fileName,
					WriteTextureFormat.ARGB32, false, false, Vector2.zero, 
					new Vector2 ((float)tex.width, (float)tex.height), 
					false);
			}
		}

		//Slow
		public static Texture2D Aux_Blur (Texture2D image, int blurSize)
		{
			Texture2D blurred = new Texture2D(image.width, image.height);

			// look at every pixel in the blur rectangle
			for (int xx = 0; xx < image.width; xx++)
			{
				for (int yy = 0; yy < image.height; yy++)
				{
					float avgR = 0, avgG = 0, avgB = 0, avgA = 0;
					int blurPixelCount = 0;

					// average the color of the red, green and blue for each pixel in the
					// blur size while making sure you don't go outside the image bounds
					for (int x = xx; (x < xx + blurSize || x < image.width); x++)
					{
						for (int y = yy; (y < yy + blurSize || y < image.height); y++)
						{
							Color pixel = image.GetPixel(x, y);

							avgR += pixel.r;
							avgG += pixel.g;
							avgB += pixel.b;
							avgA += pixel.a;

							blurPixelCount++;
						}
					}

					avgR = avgR / blurPixelCount;
					avgG = avgG / blurPixelCount;
					avgB = avgB / blurPixelCount;
					avgA = avgA / blurPixelCount;

					// now that we know the average for the blur size, set each pixel to that color
					for (int x = xx; x < xx + blurSize || x < image.width; x++)
						for (int y = yy; y < yy + blurSize || y < image.height; y++)
							blurred.SetPixel(x, y, new Color(avgR, avgG, avgB, avgA));
				}
			}
			blurred.Apply();
			return blurred;
		}


		static float aux_FastBlur_avg_R = 0f;
		static float aux_FastBlur_avg_G = 0f;
		static float aux_FastBlur_avg_B = 0f;
		static float aux_FastBlur_avg_A = 0f;
		static int aux_FastBlur_blurPixelCount = 0;
		public static void Aux_FastBlur (ref Texture2D tex, int rad, int iter)
		{
			for (int i = 0; i < iter; i++)
			{
				Aux_FastBlur_BlurImage (ref tex, rad, true);
				Aux_FastBlur_BlurImage (ref tex, rad, false);
			}
		}
		public static void Aux_FastBlur_BlurImage (ref Texture2D tex, int blurSize, bool horz)
		{
			int w = tex.width;
			int h = tex.height;
		
			int xx;
			int yy;
			int x;
			int y;
		
			if (horz)
			{
				for (yy = 0; yy < h; yy++)
				{
					for (xx = 0; xx < w; xx++)
					{
						Aux_FastBlur_ResetPixel ();
		
						for (x = xx; (x < xx + blurSize && x < w); x++)
						{
							Aux_FastBlur_AddPixel (tex.GetPixel (x, yy));
						}
						for (x = xx; (x > xx - blurSize && x > 0); x--)
						{
							Aux_FastBlur_AddPixel (tex.GetPixel (x, yy));
						}
						Aux_FastBlur_ComputePixel ();
		
						for (x = xx; x < xx + blurSize && x < w; x++)
						{
							tex.SetPixel (x, yy, new Color (
								aux_FastBlur_avg_R, aux_FastBlur_avg_G, aux_FastBlur_avg_B, 1f));
						}
					}
				}
			}
			else
			{
				for (xx = 0; xx < w; xx++)
				{
					for (yy = 0; yy < h; yy++)
					{
						Aux_FastBlur_ResetPixel ();
		
						for (y = yy; (y < yy + blurSize && y < h); y++)
						{
							Aux_FastBlur_AddPixel (tex.GetPixel (xx, y));
						}
						for (y = yy; (y > yy - blurSize && y > 0); y--)
						{
							Aux_FastBlur_AddPixel (tex.GetPixel (xx, y));
						}
						Aux_FastBlur_ComputePixel ();
		
						for (y = yy; y < yy + blurSize && y < h; y++)
						{
							tex.SetPixel (xx, y, new Color (
								aux_FastBlur_avg_R, aux_FastBlur_avg_G, aux_FastBlur_avg_B, 1f));
						}
					}
				}
			}
		
			tex.Apply ();
		}
		public static void Aux_FastBlur_AddPixel (Color c)
		{
			aux_FastBlur_avg_A += c.a;
			aux_FastBlur_avg_B += c.b;
			aux_FastBlur_avg_G += c.g;
			aux_FastBlur_avg_R += c.r;
		
			aux_FastBlur_blurPixelCount++;
		}
		public static void Aux_FastBlur_ResetPixel ()
		{
			aux_FastBlur_avg_A = 0f;
			aux_FastBlur_avg_B = 0f;
			aux_FastBlur_avg_G = 0f;
			aux_FastBlur_avg_R = 0f;
		
			aux_FastBlur_blurPixelCount = 0;
		}
		public static void Aux_FastBlur_ComputePixel ()
		{
			aux_FastBlur_avg_R /= (float)aux_FastBlur_blurPixelCount;
			aux_FastBlur_avg_B /= (float)aux_FastBlur_blurPixelCount;
			aux_FastBlur_avg_G /= (float)aux_FastBlur_blurPixelCount;
		}


		//toDel
		public static void Aux_FastBlurMezanix (ref Texture2D tex, int rad, int iter, 
			ref Color c, ref int blurPixelCount)
		{
			for (int i = 0; i < iter; i++)
			{
				Aux_FastBlurMezanix_BlurImage (ref tex, rad, true, ref c, ref blurPixelCount);
				Aux_FastBlurMezanix_BlurImage (ref tex, rad, false, ref c, ref blurPixelCount);
			}
		}
		public static void Aux_FastBlurMezanix_BlurImage (ref Texture2D tex, int blurSize, bool horz,
			ref Color c, ref int blurPixelCount)
		{
			int w = tex.width;
			int h = tex.height;

			int xx;
			int yy;
			int x;
			int y;

			if (horz)
			{
				for (yy = 0; yy < h; yy++)
				{
					for (xx = 0; xx < w; xx++)
					{
						Aux_FastBlurMezanix_ResetPixel (ref c, ref blurPixelCount);

						for (x = xx; (x < xx + blurSize && x < w); x++)
						{
							Aux_FastBlurMezanix_AddPixel (tex.GetPixel (x, yy), ref c, ref blurPixelCount);
						}
						for (x = xx; (x > xx - blurSize && x > 0); x--)
						{
							Aux_FastBlurMezanix_AddPixel (tex.GetPixel (x, yy), ref c, ref blurPixelCount);
						}
						Aux_FastBlurMezanix_ComputePixel (ref c, ref blurPixelCount);

						for (x = xx; x < xx + blurSize && x < w; x++)
						{
							tex.SetPixel (x, yy, new Color (c.r, c.g, c.b, 1f));
						}
					}
				}
			}
			else
			{
				for (xx = 0; xx < w; xx++)
				{
					for (yy = 0; yy < h; yy++)
					{
						Aux_FastBlurMezanix_ResetPixel (ref c, ref blurPixelCount);

						for (y = yy; (y < yy + blurSize && y < h); y++)
						{
							Aux_FastBlurMezanix_AddPixel (tex.GetPixel (xx, y), ref c, ref blurPixelCount);
						}
						for (y = yy; (y > yy - blurSize && y > 0); y--)
						{
							Aux_FastBlurMezanix_AddPixel (tex.GetPixel (xx, y), ref c, ref blurPixelCount);
						}
						Aux_FastBlurMezanix_ComputePixel (ref c, ref blurPixelCount);

						for (y = yy; y < yy + blurSize && y < h; y++)
						{
							tex.SetPixel (xx, y, new Color (c.r, c.g, c.b, 1f));
						}
					}
				}
			}

			tex.Apply ();
		}
		public static void Aux_FastBlurMezanix_AddPixel (Color cS, ref Color c, ref int blurPixelCount)
		{
			c += cS;

			blurPixelCount++;
		}
		public static void Aux_FastBlurMezanix_ResetPixel (ref Color c, ref int blurPixelCount)
		{
			c = new Color (0f, 0f, 0f, 0f);
			blurPixelCount = 0;
		}
		public static void Aux_FastBlurMezanix_ComputePixel (ref Color c, ref int blurPixelCount)
		{
			c = new Color (
				c.r / (float)blurPixelCount,
				c.g / (float)blurPixelCount,
				c.b / (float)blurPixelCount, 1f);
		}
		#endregion texture

		#region math
		//toDel
		public static float [,] Aux_InverseLerp (ref float [,] values)
		{
			float min = float.MaxValue;
			float max = float.MinValue;

			for (int j = 0; j < values.GetLength (1); j++)
			{
				for (int i = 0; i < values.GetLength (0); i++)
				{
					if (values [i, j] > max)
						max = values [i, j];

					if (values [i, j] < min)
						min = values [i, j];
				}
			}

			for (int j = 0; j < values.GetLength (1); j++)
			{
				for (int i = 0; i < values.GetLength (0); i++)
				{
					values [i, j] = Mathf.InverseLerp (min, max, values [i, j]);
				}
			}

			return values;
		}

		public static float [] Aux_InverseLerp (ref float [] values)
		{
			float min = float.MaxValue;
			float max = float.MinValue;

			for (int j = 0; j < values.Length; j++)
			{
				if (values [j] > max)
					max = values [j];
			}

			for (int j = 0; j < values.Length; j++)
			{
				values [j] = Mathf.InverseLerp (min, max, values [j]);
			}

			return values;
		}

		public static void Aux_IncrementWithLimit (ref float v, float incr, float limit, bool up)
		{
			if (up)
			{
				if (v < limit)
					v += incr;

				v = Mathf.Min (v, limit);
			}
			else
			{
				if (v > limit)
					v -= incr;

				v = Mathf.Max (v, limit);
			}
		}
	
		public static void Aux_StockMinMax (float value, ref float min, ref float max)
		{
			if (value < min)
				min = value;

			if (value > max)
				max = value;
		}

		public static Vector2 [,] Aux_DoubleCrossTestValues (Vector3 v_0, Vector3 v_1)
		{
			int n_0 = Mathf.CeilToInt ((v_0.y - v_0.x) / (v_0.z==0f?1f:v_0.z));
			int n_1 = Mathf.CeilToInt ((v_1.y - v_1.x) / (v_1.z==0f?1f:v_1.z));

			Vector2 [,] r = new Vector2 [n_0, n_1];

			float r_0 = v_0.x;
			float r_1 = v_1.x;

			for (int i = 0; i < n_0; i++)
			{
				for (int j = 0; j < n_1; j++)
				{
					r [i, j] = new Vector2 (r_0, r_1);

					r_1 += v_1.z;
				}

				r_1 = v_1.x;

				r_0 += v_0.z;
			}

			return r;
		}


		public class Aux_Minimizer
		{
			public void Sort (ref Aux_MinimizerTest [] minimizerTests)
			{
				for (int i = 0; i < minimizerTests.Length-1; i++)
				{
					for (int j = i+1; j < minimizerTests.Length; j++)
					{
						if (minimizerTests [i].rate > minimizerTests [j].rate)
						{
							Aux_MinimizerTest tmp = minimizerTests [i];
							minimizerTests [i] = minimizerTests [j];
							minimizerTests [j] = tmp;
						}
					}
				}
			}
		}

		public class Aux_MinimizerTest
		{
			public int id = -1;
			public float rate = 0f;
			public float valueM = 0f;

			public float [] coefs = new float[1];
			public string [] coefsNames = new string[1];

			public Aux_MinimizerTest (int setID, float setValueM, float [] setCoefs, string [] setCoefsNames)
			{
				id = setID;

				valueM = (setValueM < 0f || setValueM > 1f)? LogicNode.Aux_Sigmoid (setValueM, 1f) : setValueM;

				rate = valueM;

				coefs = setCoefs;

				coefsNames = setCoefsNames;
			}
		}


		public static float Aux_Sigmoid (float x, float speed)
		{
			return 1f / ( 1f + Mathf.Exp ( (-1f)*x*speed ) );
		}

		public static int Aux_TestVectorToCount (Vector3 v)
		{
			return Mathf.CeilToInt ( (v.y - v.x) / v.z );
		}

		public static float [] Aux_AbsoluteDifference (ref float [] v_0, ref float [] v_1)
		{
			float [] r = new float [] {-1f};

			if (v_0.Length != v_1.Length)
				return r;

			r = new float [v_0.Length];

			for (int i = 0; i < v_0.Length; i++)
			{
				r [i] = Mathf.Abs ( v_0 [i] - v_1 [i] );
			}

			return r;
		}

		public static float Aux_SumArray (ref float [] v)
		{
			float r = 0f;

			for (int i = 0; i < v.Length; i++)
			{
				r += v [i];
			}

			return r;
		}


		public static float [] Aux_ForwardSlopesArray (ref float [] v)
		{
			float [] r = new float[v.Length];

			for (int i = 0; i < v.Length-1; i++)
			{
				r [i] = v [i+1] - v [i];
			}
			r [v.Length-1] = v [0] - v [v.Length-1];

			return r;
		}

		public enum Aux_RoundSmoothPlace
		{
			floor,

			middle,

			ceil,
		}

		public static float Aux_RoundSmooth (float v, float smoothnesss, Aux_RoundSmoothPlace place)
		{
			float middle = 0.5f * (Mathf.Floor (v) + Mathf.Ceil (v));

			float ruptureMin = middle - smoothnesss;

			float ruptureMax = middle + smoothnesss;

			switch (place)
			{
			case Aux_RoundSmoothPlace.ceil:
				smoothnesss = Mathf.Clamp (smoothnesss, 0f, 1f);

				ruptureMin = Mathf.Ceil (v) - smoothnesss;

				if (v >= ruptureMin)
				{
					return v;
				}
				break;

			case Aux_RoundSmoothPlace.floor:
				smoothnesss = Mathf.Clamp (smoothnesss, 0f, 1f);

				ruptureMax = Mathf.Floor (v) + smoothnesss;

				if (v <= ruptureMax)
				{
					return v;
				}
				break;

			case Aux_RoundSmoothPlace.middle:
				smoothnesss = Mathf.Clamp (smoothnesss, 0f, 0.5f);

				ruptureMin = middle - smoothnesss;

				ruptureMax = middle + smoothnesss;

				if (Aux_InRange (ruptureMin, ruptureMax, v))
				{
					return v;
				}
				break;
			}

			return Mathf.Round (v);
		}

		public static float Aux_RoundSmooth_middle_lerp (float v, float smoothnesss)
		{
			float floor = Mathf.Floor (v);

			float ceil = Mathf.Ceil (v);

			float middle = 0.5f * (floor + ceil);

			smoothnesss = Mathf.Clamp (smoothnesss, 0f, 0.5f);

			float ruptureMin = middle - smoothnesss;

			float ruptureMax = middle + smoothnesss;

			if (Aux_InRange (v, ruptureMin, ruptureMax))
			{
				return Mathf.Lerp ( floor, ceil, Mathf.InverseLerp (ruptureMin, ruptureMax, v) );
			}
			//else if (v > ruptureMax)
			//{
			//	return ceil;
			//}
			//else if (v < ruptureMin)
			//{
			//	return floor;
			//}

			return Mathf.Round (v);
		}

		public static float Aux_MiddleSmooth_middle_lerp (float v, float smoothnesss)
		{
			float floor = Mathf.Floor (v);

			float ceil = Mathf.Ceil (v);

			float middle = 0.5f * (floor + ceil);

			smoothnesss = Mathf.Clamp (smoothnesss, 0f, 0.5f);

			float ruptureMin = middle - smoothnesss;

			float ruptureMax = middle + smoothnesss;

			if (Aux_InRange (v, ruptureMin, ruptureMax))
			{
				return Mathf.Lerp ( floor, ceil, Mathf.InverseLerp (ruptureMin, ruptureMax, v) );
			}
			//else if (v > ruptureMax)
			//{
			//	return ceil;
			//}
			//else if (v < ruptureMin)
			//{
			//	return floor;
			//}

			return middle;
		}

		public static float Aux_FloorSmooth_middle_lerp (float v, float smoothnesss)
		{
			float floor = Mathf.Floor (v);

			float ceil = Mathf.Ceil (v);

			float middle = 0.5f * (floor + ceil);

			smoothnesss = Mathf.Clamp (smoothnesss, 0f, 0.5f);

			float ruptureMin = middle - smoothnesss;

			float ruptureMax = middle + smoothnesss;

			if (Aux_InRange (v, ruptureMin, ruptureMax))
			{
				return Mathf.Lerp ( floor, ceil, Mathf.InverseLerp (ruptureMin, ruptureMax, v) );
			}
			//else if (v > ruptureMax)
			//{
			//	return ceil;
			//}
			//else if (v < ruptureMin)
			//{
			//	return floor;
			//}

			return floor;
		}

		public static float Aux_CeilSmooth_middle_lerp (float v, float smoothnesss)
		{
			float floor = Mathf.Floor (v);

			float ceil = Mathf.Ceil (v);

			float middle = 0.5f * (floor + ceil);

			smoothnesss = Mathf.Clamp (smoothnesss, 0f, 0.5f);

			float ruptureMin = middle - smoothnesss;

			float ruptureMax = middle + smoothnesss;

			if (Aux_InRange (v, ruptureMin, ruptureMax))
			{
				return Mathf.Lerp ( floor, ceil, Mathf.InverseLerp (ruptureMin, ruptureMax, v) );
			}
			//else if (v > ruptureMax)
			//{
			//	return ceil;
			//}
			//else if (v < ruptureMin)
			//{
			//	return floor;
			//}

			return ceil;
		}


		public static float Aux_Smooth_middle_lerp (float v, float smoothnesss)
		{
			float floor = Mathf.Floor (v);

			float ceil = Mathf.Ceil (v);

			float middle = 0.5f * (floor + ceil);

			smoothnesss = Mathf.Clamp (smoothnesss, 0f, 0.5f);

			float ruptureMin = middle - smoothnesss;

			float ruptureMax = middle + smoothnesss;

			if (Aux_InRange (v, ruptureMin, ruptureMax))
			{
				return Mathf.Lerp ( floor, ceil, Mathf.InverseLerp (ruptureMin, ruptureMax, v) );
			}
			//else if (v > ruptureMax)
			//{
			//	return ceil;
			//}
			//else if (v < ruptureMin)
			//{
			//	return floor;
			//}

			return v;
		}

		//public static bool Aux_InRange (float min, float max, float v)
		//{
		//	if (v >= min && v <= max)
		//	{
		//		return true;
		//	}
		//
		//	return false;
		//}


		public static void Aux_ComputeSlopes (ref float [,] heights, LogicNode ln)
		{
			float criteria = 0f;

			float minSlope = float.MaxValue;

			float maxSlope = float.MinValue;

			float [,] slopes = new float [heights.GetLength (0), heights.GetLength (1)];

			for (int j = 0; j < heights.GetLength (1) -1; j++)
			{
				for (int i = 0; i < heights.GetLength (0) -1; i++)
				{
					int step = 1;

					int x = i;
					int x_1 = i + step;

					int dx = x_1 -x;

					int y = j;
					int y_1 = j + step;

					int dy = y_1 - y;


					float dhX = heights [x_1, y] - heights [x, y];
					float dhY = heights [x, y_1] - heights [x, y];

					//if (dhX != 0f)
					//	Debug.Log ((float)dx);

					float dhOdx = dhX / (float)dx;

					float dhOdY = dhY / (float)dy;


					//criteria = 
					//	terrainData.GetSteepness(
					//		Mathf.RoundToInt(normJ),
					//		Mathf.RoundToInt(normI) );

					criteria = new Vector3 (dhOdx, 1f, dhOdY).magnitude;


					slopes [i, j] = criteria;

					if (criteria > maxSlope)
						maxSlope = criteria;

					if (criteria < minSlope)
						minSlope = criteria;

				}
			}

			for (int j = 0; j < heights.GetLength (1); j++)
			{
				for (int i = 0; i < heights.GetLength (0); i++)
				{
					if (i == heights.GetLength (0) - 1 || j == heights.GetLength (1) -1)
					{
						slopes [i, j] = 0f;
					}
					else
					{
						slopes [i, j] = Mathf.InverseLerp (minSlope, maxSlope, slopes [i, j]);	
					}
				}
			}


			Aux_ValuesToGrayscaleTexture2D (ref slopes, "slopesmap", ref ln.texture2DValue);
		}

		#endregion math

		#region Gaussian_blur
		public static void Aux_GaussBlur_4 (ref Texture2D tex, float r)
		{
			Color [] source = tex.GetPixels ();
			Color [] dest = new Color [source.Length];

			Aux_GaussBlur_4 (source, dest, r, tex.width, tex.height);

			tex.SetPixels (dest);
			tex.Apply ();
		}

		public static void Aux_GaussBlur_4 (Color [] source, Color [] dest, float r, int w, int h)
		{
			float [] dest_r = new float [dest.Length];
			float [] dest_g = new float [dest.Length];
			float [] dest_b = new float [dest.Length];
			float [] dest_a = new float [dest.Length];

			float [] source_r = new float [source.Length];
			float [] source_g = new float [source.Length];
			float [] source_b = new float [source.Length];
			float [] source_a = new float [source.Length];

			for (int i = 0; i < source.Length; i++)
			{
				source_r [i] = source [i].r;
				source_g [i] = source [i].g;
				source_b [i] = source [i].b;
				source_a [i] = source [i].a;
			}

			Aux_GaussBlur_4 (source_r, dest_r, r, w, h);
			Aux_GaussBlur_4 (source_g, dest_g, r, w, h);
			Aux_GaussBlur_4 (source_b, dest_b, r, w, h);
			Aux_GaussBlur_4 (source_a, dest_a, r, w, h);

			for (int i = 0; i < dest.Length; i++)
			{
				dest [i] = new Color (dest_r [i], dest_g [i], dest_b [i], dest_a [i]);
			}
		}

		public static void Aux_GaussBlur_4 (float [] source, float [] dest, float r, int w, int h)
		{
			var bxs = Aux_GaussianBlur_Boxes (r, 3);
			Aux_boxBlur_4 (source, dest, w, h, (bxs[0] - 1) / 2);
			Aux_boxBlur_4 (dest, source, w, h, (bxs[1] - 1) / 2);
			Aux_boxBlur_4 (source, dest, w, h, (bxs[2] - 1) / 2);
		}

		public static int [] Aux_GaussianBlur_Boxes (float sigma, int n)
		{
			float wIdeal = Mathf.Sqrt((12f * sigma * sigma / (float)n) + 1f);

			int wl = Mathf.FloorToInt (wIdeal);
			wl = (wl % 2 == 0)? wl-1: wl;
			int wu = wl + 2;

			float mIdeal = (12f * sigma * sigma - (float)n * (float)wl * (float)wl - 4f * (float)n * (float)wl - 3f * (float)n) / (-4f * (float)wl - 4f);
			int m = Mathf.RoundToInt (mIdeal);

			List <int> boxes = new List<int> ();

			for (int i = 0; i < n; i++)
			{
				boxes.Add (i < m ? wl : wu);
			}

			return boxes.ToArray ();
		}

		public static void Aux_boxBlur_4 (float[] source, float[] dest, int w, int h, int r)
		{
			for (var i = 0; i < source.Length; i++) 
			{
				dest[i] = source[i];
			}

			Aux_boxBlurH_4 (dest, source, w, h, r);
			Aux_boxBlurT_4 (source, dest, w, h, r);
		}

		public static void Aux_boxBlurH_4 (float[] source, float[] dest, int w, int h, int r)
		{
			float iar = 1f / ((float)r + (float)r + 1f);
			for(int i = 0; i < h; i++)
			{
				int ti = i * w;
				int li = ti;
				int ri = ti + r;

				float fv = source [ti];
				float lv = source [ti + w - 1];

				float val = ((float)r + 1f) * fv;

				for (int j = 0; j < r; j++) 
				{
					val += source [ti + j];
				}

				for (int j = 0; j <= r; j++)
				{
					val += source [ri++] - fv;
					dest [ti++] = Mathf.RoundToInt ((float)val * iar);
				}
				for (int j = r + 1; j < w - r; j++)
				{
					val += source [ri++] - dest[li++];
					dest [ti++] = Mathf.RoundToInt ((float)val * iar);
				}
				for (int j = w - r; j < w; j++)
				{
					val += lv - source [li++];
					dest [ti++] = Mathf.RoundToInt ((float)val * iar);
				}
			}
		}

		public static void Aux_boxBlurT_4 (float[] source, float[] dest, int w, int h, int r)
		{
			float iar = 1f / ((float)r + (float)r + 1f);
			for (int i = 0; i < w; i++)
			{
				int ti = i;
				int li = ti;
				int ri = ti + r * w;

				float fv = source [ti];
				float lv = source [ti + w * (h - 1)];

				float val = ((float)r + 1) * fv;

				for (int j = 0; j < r; j++) 
				{
					val += source [ti + j * w];
				}


				for (int j = 0; j <= r; j++)
				{
					val += source [ri] - fv;
					dest [ti] = Mathf.RoundToInt ((float)val * iar);
					ri += w;
					ti += w;
				}
				for (int j = r + 1; j < h - r; j++)
				{
					val += source [ri] - source [li];
					dest [ti] = Mathf.RoundToInt ((float)val * iar);
					li += w;
					ri += w;
					ti += w;
				}
				for (int j = h - r; j < h; j++)
				{
					val += lv - source [li];
					dest [ti] = Mathf.RoundToInt ((float)val * iar);
					li += w;
					ti += w;
				}
			}
		}
		#endregion 

		#region outputAuxiliaries
		public static float [] Aux_texture2DViewerFillGapFactor (int nb, float fillRatio)
		{
			return new float [] {
				(1f/(float)nb) * fillRatio,
				(1f/(float)nb) * (1f - fillRatio) };
		}

		public float Aux_texture2DViewer_drawingSize = 0.6f;
		public static void Aux_DrawScalableTexture2DViewer (ref Texture2D tex, LogicNode ln)
		{
			float texture2DViewer_incrSize = 0.3f;
			float texture2DViewer_maxSize = 3f;
			float texture2DViewer_minSize = 0.2f;

			Vector2 texture2DViewerSize = ln.Aux_texture2DViewer_drawingSize *
				Vector2.one*Skins.logicNodeRectStep.x * Aux_texture2DViewerFillGapFactor (1, 0.9f) [0];
			//float texture2DViewerGap = texture2DViewerSize.y * texture2DViewerFillGapFactor (1, 0.9f) [1];

			Rect drawingRect = new Rect (ln.rect.position + ln.rect.size + 
				new Vector2 (20f, -texture2DViewerSize.y), 
				texture2DViewerSize);

			if (drawingRect.Contains (ln.eGlobal.mousePosition))
			{
				if (ln.eGlobal.type == EventType.ScrollWheel)
				{
					if (ln.eGlobal.delta.y < 0f)
					{
						Aux_IncrementWithLimit (ref ln.Aux_texture2DViewer_drawingSize,
							texture2DViewer_incrSize, texture2DViewer_maxSize, true);
					}
					else if (ln.eGlobal.delta.y > 0f)
					{
						Aux_IncrementWithLimit (ref ln.Aux_texture2DViewer_drawingSize,
							texture2DViewer_incrSize, texture2DViewer_minSize, false);
					}
				}
			}


			ln.DrawTexture2DViewer (tex, drawingRect);
		}
		public static Rect Aux_DrawScalableTexture2DViewer (ref Texture2D tex, LogicNode ln, bool outDrawingRect)
		{
			float texture2DViewer_incrSize = 0.3f;
			float texture2DViewer_maxSize = 3f;
			float texture2DViewer_minSize = 0.2f;

			Vector2 texture2DViewerSize = ln.Aux_texture2DViewer_drawingSize *
				Vector2.one*Skins.logicNodeRectStep.x * Aux_texture2DViewerFillGapFactor (1, 0.9f) [0];
			//float texture2DViewerGap = texture2DViewerSize.y * texture2DViewerFillGapFactor (1, 0.9f) [1];

			Rect drawingRect = new Rect (ln.rect.position + ln.rect.size + 
				new Vector2 (20f, -texture2DViewerSize.y), 
				texture2DViewerSize);

			if (drawingRect.Contains (ln.eGlobal.mousePosition))
			{
				if (ln.eGlobal.type == EventType.ScrollWheel)
				{
					if (ln.eGlobal.delta.y < 0f)
					{
						Aux_IncrementWithLimit (ref ln.Aux_texture2DViewer_drawingSize,
							texture2DViewer_incrSize, texture2DViewer_maxSize, true);
					}
					else if (ln.eGlobal.delta.y > 0f)
					{
						Aux_IncrementWithLimit (ref ln.Aux_texture2DViewer_drawingSize,
							texture2DViewer_incrSize, texture2DViewer_minSize, false);
					}
				}
			}


			ln.DrawTexture2DViewer (tex, drawingRect);

			return drawingRect;
		}
		public static void Aux_DrawScalableTexture2D_0_pickable_Viewer (ref Texture2D tex, LogicNode ln, int pickabales,
			ref bool opened, float height, ref Color pickedColor)
		{
			Rect OpenedDrawingRect = Aux_TextureViewerButton (ref opened, ln, height);
			if ( ! opened)
			{
				Rect infoDrawingRect = new Rect (OpenedDrawingRect.position + new Vector2 (20f, 0f),
					new Vector2 (50f, OpenedDrawingRect.height));
				EditorGUI.LabelField (infoDrawingRect, "Click \nTriangle \nTo Pick ID", GetGuiStyle (Skins.logicNodeLabelLeft));

				return;
			}

			Rect viewerDrawingRect = Aux_DrawScalableTexture2DViewer (ref tex, ln, true);

			float gap = 10f;
			float gapV = 15f;
			Rect drawingRect = new Rect (viewerDrawingRect.x, viewerDrawingRect.y + viewerDrawingRect.height + gap,
				5.5f*gap, gap);
			EditorGUI.LabelField (drawingRect, "Choose ID", GetGuiStyle (Skins.logicNodeLabelLeft));
			Rect drawingRectSuppInfo = new Rect (drawingRect.position + new Vector2 (0f, gapV), drawingRect.size);
			EditorGUI.LabelField (drawingRectSuppInfo, "Pick choosen ID", GetGuiStyle (Skins.logicNodeLabelLeft));
			Rect drawingRectSuppInfo_1 = new Rect (drawingRectSuppInfo.position + new Vector2 (0f, gapV), drawingRectSuppInfo.size);
			EditorGUI.LabelField (drawingRectSuppInfo_1, "in the image", GetGuiStyle (Skins.logicNodeLabelLeft));

			Rect drawingRectPickMenu = new Rect (drawingRect.x + drawingRect.width + gap, drawingRect.y - 0.2f*gap,
				3f*gap, 1.4f*gap);
			pickabales = Mathf.Max (0, pickabales);
			if (GUI.Button (drawingRectPickMenu, ln.texture2D_0_pickId.ToString ()) &&
				ln.eGlobal.button == 0)
			{
				GenericMenu menu = new GenericMenu ();
				for (int i = 0; i < pickabales; i++)
				{
					menu.AddItem (new GUIContent ((i+1).ToString ()), false, ln.SetTexture2D_0_PickID, i+1);
				}
				menu.ShowAsContext ();
			}

			float texWidhtF = (float)tex.width;
			float texToRect = viewerDrawingRect.width / texWidhtF;
			Vector2 fromRectNorm = -Vector2.one;

			if (viewerDrawingRect.Contains (ln.eGlobal.mousePosition))
			{
				Vector2 mp = ln.eGlobal.mousePosition;
				if (texToRect <= 1f)
				{
					fromRectNorm = (mp - viewerDrawingRect.position) / viewerDrawingRect.width;

					Vector2 inTexNorm = new Vector2 (fromRectNorm.x, 1f-fromRectNorm.y); 

					if (ln.eGlobal.type == EventType.MouseUp)
					if (ln.eGlobal.button == 0)
					{
						pickedColor = tex.GetPixel (
							Mathf.RoundToInt (inTexNorm.x*tex.width),
							Mathf.RoundToInt (inTexNorm.y*tex.height));
						EditorUtility.SetDirty (ln.logic.node.graph);
					}
				}
				else
				{
					fromRectNorm = (mp - viewerDrawingRect.position) / viewerDrawingRect.width;
					Vector2 fromRectToTexNorm = Vector2.one*(0.5f*(viewerDrawingRect.width-texWidhtF)/viewerDrawingRect.width);

					Vector2 inTexNorm = new Vector2 (
						texToRect*(fromRectNorm.x-fromRectToTexNorm.x),
						1f-texToRect*(fromRectNorm.y-fromRectToTexNorm.y) );

					if (ln.eGlobal.type == EventType.MouseUp)
					if (ln.eGlobal.button == 0)
					{
						pickedColor = tex.GetPixel (
							Mathf.RoundToInt (inTexNorm.x*tex.width),
							Mathf.RoundToInt (inTexNorm.y*tex.height));
						EditorUtility.SetDirty (ln.logic.node.graph);
					}
				}
			}
		}
		public void SetTexture2D_0_PickID (object o)
		{
			texture2D_0_pickId = int.Parse (o.ToString ());
		}
		int texture2D_0_pickId = -1;

		public static void Aux_DrawScalableTexture2DViewer (ref Texture2D tex, LogicNode ln, 
			ref bool opened, string [] documentationMessage, string [] urls, string [] labels)
		{
			Aux_TextureViewerButton (ref opened, ln);
			if (opened)
			{
				Aux_DrawScalableTexture2DViewer (ref tex, ln);
			}

			ln.DrawTexture2DResultField (true);
			Aux_WriteTextureToFile (ref tex, "NewTexture", ln);

			ln.DrawDocumentationBoxUpRight (documentationMessage);
			ln.DrawDocumentationUrlButtons (documentationMessage, urls, labels);
		}			


		public bool textureViewerOpened = false;
		public static void Aux_TextureViewerButton (ref bool opened, LogicNode ln)
		{
			Rect drawingRect = new Rect (
				new Vector2 (ln.rect.x + ln.rect.width, ln.rect.y + ln.rect.height*0.25f), Vector2.one*17f);

			GUIStyle drawingStyle = opened? GetGuiStyle (Skins.set): GetGuiStyle (Skins.playButton);

			if (GUI.Button (drawingRect, "", drawingStyle))
			{
				opened = ! opened;
			}
		}
		public static Rect Aux_TextureViewerButton (ref bool opened, LogicNode ln, float height)
		{
			Rect drawingRect = new Rect (
				new Vector2 (ln.rect.x + ln.rect.width, height), Vector2.one*17f);

			GUIStyle drawingStyle = opened? GetGuiStyle (Skins.set): GetGuiStyle (Skins.playButton);

			if (GUI.Button (drawingRect, "", drawingStyle))
			{
				opened = ! opened;
			}

			return drawingRect;
		}
		#endregion outputAuxiliaries
	

		#region terra
		//toDel
		public static float Aux_DiamondTerrain_Slope (ref Terrain terrain, float normI, float normJ)
		{
			int step = 1;

			int x = Mathf.Clamp (
				Mathf.RoundToInt (normJ * (float)terrain.terrainData.heightmapHeight), 
				0, terrain.terrainData.heightmapHeight);

			int x_1 = Mathf.Clamp (
				Mathf.RoundToInt (normJ * (float)terrain.terrainData.heightmapHeight) + step,
				0, terrain.terrainData.heightmapHeight);

			int dx = x_1 -x;


			int y = Mathf.Clamp (
				Mathf.RoundToInt (normI * (float)terrain.terrainData.heightmapWidth), 
				0, terrain.terrainData.heightmapWidth);

			int y_1 = Mathf.Clamp (
				Mathf.RoundToInt (normI * (float)terrain.terrainData.heightmapWidth) + step, 
				0, terrain.terrainData.heightmapWidth);

			int dy = y_1 - y;


			float dhX = terrain.terrainData.GetHeight (x_1, y) - terrain.terrainData.GetHeight (x, y);
			float dhY = terrain.terrainData.GetHeight (x, y_1) - terrain.terrainData.GetHeight (x, y);

			float dhOdx = dhX / (float)dx;

			float dhOdY = dhY / (float)dy;


			//criteria = 
			//	terrain.terrainData.GetSteepness(
			//		Mathf.RoundToInt(normJ),
			//		Mathf.RoundToInt(normI) );

			return new Vector3 (dhOdx, 1f, dhOdY).magnitude;

		} 
		#endregion terra
	
	
		#region zooming
		public static Vector2 Aux_RectsCenter (Rect [] rects, Vector2 defaultCenter)
		{
			Vector2 r = defaultCenter;

			float rectsNb = 1f;

			for (int i = 0; i < rects.Length; i++)
			{
				rectsNb += 1f;
				r += rects [i].center;
			}

			r /= rectsNb;

			return r;
		}

		public static Rect [] Aux_LogicToNodesRects (Logic l, Rect defaultRect)
		{
			Rect [] r;

			if (l.nodes.Count == 0)
			{
				r = new Rect[]{defaultRect};

				return r;
			}
			r = new Rect[l.nodes.Count];

			for (int i = 0; i < r.Length; i++)
			{
				r [i] = l.nodes [i].rect;
			}

			return r;
		}
			
		#endregion zooming	
	
		#region DrawUI
		Rect Aux_SuitRectSecondColumn ()
		{
			return GetSuitableRect (FieldDrawType.label, 1, 2);
		}

		void DrawDocumentationUrlButtons (string [] message, string [] urls, string [] labels)
		{
			if (minimizeDocumentationBoxUpRight)
			{
				return;
			}

			Rect [] drawingRects = DocUrlRect (message, urls);

			for (int i = 0; i < urls.Length; i++)
			{
				if ( ! string.IsNullOrEmpty (urls [i]))
				{
					if (GUI.Button (drawingRects [i], labels [i], 
						Skins.guiSkin.GetStyle (Skins.LittleNamedRectsCenterDark)) && eGlobal.button == 0)
					{
						Application.OpenURL (urls [i]);
					}
				}
			}
		}

		void DrawLogicNodeUrlButton (string label, string url)
		{
			if (GUI.Button (GetSuitableRect (FieldDrawType.label), label) && eGlobal.button == 0)
			{
				Application.OpenURL (url);
			}
		}
		void DrawLogicNodeUrlButton (string label, string url, int column, int totalColumns)
		{
			if (GUI.Button (GetSuitableRect (FieldDrawType.label, column, totalColumns), label) &&
				eGlobal.button == 0)
			{
				Application.OpenURL (url);
			}
		}

		void DrawExtensionUrlButton (string extName, string url)
		{
			DrawInNodeInfo (extName + " is not installed");

			if (string.IsNullOrEmpty (url))
				return;

			DrawLogicNodeUrlButton ("Get It", url);
		}

		Rect [] DocUrlRect (string [] message, string [] urls)
		{
			Rect mbr = GetMessageBoxRectRighttUp (message, documentationCharSize);
			float docUrlHeight = 20f;
			float gapHeight = 0.12f*docUrlHeight;
			Rect docUrlRect = new Rect (
				mbr.x, mbr.y + mbr.height + 5f, 140f, docUrlHeight);

			Rect [] r = new Rect[urls.Length];
			int notNulCount = -1;
			for (int i = 0; i < urls.Length; i++)
			{
				if ( ! string.IsNullOrEmpty (urls [i]))
					notNulCount++;

				float nncF = (float)notNulCount;
				float nncF_1 = nncF - 1f;

				r [i] = new Rect (
					docUrlRect.x, docUrlRect.y + nncF*docUrlRect.height + nncF_1*gapHeight,
					docUrlRect.width, docUrlRect.height);
			}

			return r;
		}

		Rect GetSuitableRectReadOnly (FieldDrawType fieldDrawType)
		{
			return GetSuitableRect (fieldDrawType, 
				new Vector2 (fieldIDGlobal [0], fieldIDGlobal [1]),
				new Vector2 (fieldsCountGlobal [0], fieldsCountGlobal [1]));
		}
		#endregion DrawUI
	}
}