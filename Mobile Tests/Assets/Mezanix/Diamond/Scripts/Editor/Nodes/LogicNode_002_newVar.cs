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
	public class NewVar
	{
		const string prefix = "nv";
		const string sep = "_|_";

		public enum Type
		{
			Bool,

			Int,

			Float,
		}
		public Type type;

		public int index = 0;


		public bool boolVal;

		public int intVal;

		public float floatVal;


		public NewVar (Type setType, int setIndex)
		{
			type = setType;

			index = setIndex;

			id = prefix + sep +
				type.ToString () + sep +
				index.ToString ();
		}

		public string id = "";
	}

	//LogicNode_nv
	public partial class LogicNode : ScriptableObject 
	{
		public bool [] nv_publicInputs = new bool[nv_count];
		public Rect [] nv_inputsRects = new Rect[nv_count];

		public bool [] nv_activeInputs = new bool[nv_count];
		public bool [] nv_activeInputsFields = new bool[nv_count];

		public bool [] nv_dataFlowControlEnabled = new bool[nv_count];
		public bool [] nv_permittedInputs = new bool[nv_count];

		public string [] nv_inputsSources = new string[nv_count];
		public string [] nv_inputsSources_forPermition = new string[nv_count];



		void Nv_Init ()
		{
			Nv_InitInputsOutputs ();


			Nv_InitPublicInputs ();
			Nv_InitInputRects ();

			Nv_InitDataFlowControlEnabled ();

			Nv_InitPermittedInputs ();
			Nv_InitInputSources ();
		}

		void Nv_InitInputSources ()
		{
			nv_inputsSources = new string [nv_inputsIDs.Length];

			for (int i = 0; i < nv_inputsSources.Length; i++)
			{
				nv_inputsSources [i] = "";
			}


			nv_inputsSources_forPermition = new string [nv_inputsIDs.Length];

			for (int i = 0; i < nv_inputsSources_forPermition.Length; i++)
			{
				nv_inputsSources_forPermition [i] = "";
			}
		}
		void Nv_InitPublicInputs ()
		{
			nv_publicInputs = new bool[nv_inputsIDs.Length];

			for (int i = 0; i < nv_publicInputs.Length; i++)
			{
				nv_publicInputs [i] = false;
			}
		}

		void Nv_InitInputRects ()
		{
			nv_inputsRects = new Rect [nv_inputsIDs.Length];

			for (int i = 0; i < nv_inputsRects.Length; i++)
			{
				nv_inputsRects [i] = new Rect ();
			}
		}

		void Nv_DisactivateAllInputs ()
		{
			nv_activeInputs = new bool[nv_inputsIDs.Length];

			for (int i = 0; i < nv_activeInputs.Length; i++)
			{
				nv_activeInputs [i] = false;
			}


			nv_activeInputsFields = new bool[nv_inputsIDs.Length];

			for (int i = 0; i < nv_activeInputsFields.Length; i++)
			{
				nv_activeInputsFields [i] = false;
			}
		}

		void Nv_InitDataFlowControlEnabled ()
		{
			if (nv_dataFlowControlEnabled.Length == nv_inputsIDs.Length)
				return;

			nv_dataFlowControlEnabled = new bool[nv_inputsIDs.Length];

			for (int i = 0; i < nv_dataFlowControlEnabled.Length; i++)
			{
				nv_dataFlowControlEnabled [i] = false;
			}
		}

		void Nv_InitPermittedInputs ()
		{
			nv_permittedInputs = new bool[nv_inputsIDs.Length];

			for (int i = 0; i < nv_permittedInputs.Length; i++)
			{
				nv_permittedInputs [i] = true;
			}
		}


		#region varTypeAndID
		const int nv_count = 120;
		const int nv_perTypeCount = 10;
		 
		void Nv_InitInputsOutputs ()
		{
			nv_inOutID_inOutType = new InOutID_inOutType[]
			{
				new InOutID_inOutType (Enums.nv_int_0, VariableType.Int),
				new InOutID_inOutType (Enums.nv_int_1, VariableType.Int),
				new InOutID_inOutType (Enums.nv_int_2, VariableType.Int),
				new InOutID_inOutType (Enums.nv_int_3, VariableType.Int),
				new InOutID_inOutType (Enums.nv_int_4, VariableType.Int),
				new InOutID_inOutType (Enums.nv_int_5, VariableType.Int),
				new InOutID_inOutType (Enums.nv_int_6, VariableType.Int),
				new InOutID_inOutType (Enums.nv_int_7, VariableType.Int),
				new InOutID_inOutType (Enums.nv_int_8, VariableType.Int),
				new InOutID_inOutType (Enums.nv_int_9, VariableType.Int),

				new InOutID_inOutType (Enums.nv_float_0, VariableType.Float),
				new InOutID_inOutType (Enums.nv_float_1, VariableType.Float),
				new InOutID_inOutType (Enums.nv_float_2, VariableType.Float),
				new InOutID_inOutType (Enums.nv_float_3, VariableType.Float),
				new InOutID_inOutType (Enums.nv_float_4, VariableType.Float),
				new InOutID_inOutType (Enums.nv_float_5, VariableType.Float),
				new InOutID_inOutType (Enums.nv_float_6, VariableType.Float),
				new InOutID_inOutType (Enums.nv_float_7, VariableType.Float),
				new InOutID_inOutType (Enums.nv_float_8, VariableType.Float),
				new InOutID_inOutType (Enums.nv_float_9, VariableType.Float),

				new InOutID_inOutType (Enums.nv_Vector2_0, VariableType.Vector2),
				new InOutID_inOutType (Enums.nv_Vector2_1, VariableType.Vector2),
				new InOutID_inOutType (Enums.nv_Vector2_2, VariableType.Vector2),
				new InOutID_inOutType (Enums.nv_Vector2_3, VariableType.Vector2),
				new InOutID_inOutType (Enums.nv_Vector2_4, VariableType.Vector2),
				new InOutID_inOutType (Enums.nv_Vector2_5, VariableType.Vector2),
				new InOutID_inOutType (Enums.nv_Vector2_6, VariableType.Vector2),
				new InOutID_inOutType (Enums.nv_Vector2_7, VariableType.Vector2),
				new InOutID_inOutType (Enums.nv_Vector2_8, VariableType.Vector2),
				new InOutID_inOutType (Enums.nv_Vector2_9, VariableType.Vector2),

				new InOutID_inOutType (Enums.nv_Vector3_0, VariableType.Vector3),
				new InOutID_inOutType (Enums.nv_Vector3_1, VariableType.Vector3),
				new InOutID_inOutType (Enums.nv_Vector3_2, VariableType.Vector3),
				new InOutID_inOutType (Enums.nv_Vector3_3, VariableType.Vector3),
				new InOutID_inOutType (Enums.nv_Vector3_4, VariableType.Vector3),
				new InOutID_inOutType (Enums.nv_Vector3_5, VariableType.Vector3),
				new InOutID_inOutType (Enums.nv_Vector3_6, VariableType.Vector3),
				new InOutID_inOutType (Enums.nv_Vector3_7, VariableType.Vector3),
				new InOutID_inOutType (Enums.nv_Vector3_8, VariableType.Vector3),
				new InOutID_inOutType (Enums.nv_Vector3_9, VariableType.Vector3),

				new InOutID_inOutType (Enums.nv_Vector4_0, VariableType.Vector4),
				new InOutID_inOutType (Enums.nv_Vector4_1, VariableType.Vector4),
				new InOutID_inOutType (Enums.nv_Vector4_2, VariableType.Vector4),
				new InOutID_inOutType (Enums.nv_Vector4_3, VariableType.Vector4),
				new InOutID_inOutType (Enums.nv_Vector4_4, VariableType.Vector4),
				new InOutID_inOutType (Enums.nv_Vector4_5, VariableType.Vector4),
				new InOutID_inOutType (Enums.nv_Vector4_6, VariableType.Vector4),
				new InOutID_inOutType (Enums.nv_Vector4_7, VariableType.Vector4),
				new InOutID_inOutType (Enums.nv_Vector4_8, VariableType.Vector4),
				new InOutID_inOutType (Enums.nv_Vector4_9, VariableType.Vector4),

				new InOutID_inOutType (Enums.nv_Rect_0, VariableType.rect),
				new InOutID_inOutType (Enums.nv_Rect_1, VariableType.rect),
				new InOutID_inOutType (Enums.nv_Rect_2, VariableType.rect),
				new InOutID_inOutType (Enums.nv_Rect_3, VariableType.rect),
				new InOutID_inOutType (Enums.nv_Rect_4, VariableType.rect),
				new InOutID_inOutType (Enums.nv_Rect_5, VariableType.rect),
				new InOutID_inOutType (Enums.nv_Rect_6, VariableType.rect),
				new InOutID_inOutType (Enums.nv_Rect_7, VariableType.rect),
				new InOutID_inOutType (Enums.nv_Rect_8, VariableType.rect),
				new InOutID_inOutType (Enums.nv_Rect_9, VariableType.rect),

				new InOutID_inOutType (Enums.nv_Color_0, VariableType.Color),
				new InOutID_inOutType (Enums.nv_Color_1, VariableType.Color),
				new InOutID_inOutType (Enums.nv_Color_2, VariableType.Color),
				new InOutID_inOutType (Enums.nv_Color_3, VariableType.Color),
				new InOutID_inOutType (Enums.nv_Color_4, VariableType.Color),
				new InOutID_inOutType (Enums.nv_Color_5, VariableType.Color),
				new InOutID_inOutType (Enums.nv_Color_6, VariableType.Color),
				new InOutID_inOutType (Enums.nv_Color_7, VariableType.Color),
				new InOutID_inOutType (Enums.nv_Color_8, VariableType.Color),
				new InOutID_inOutType (Enums.nv_Color_9, VariableType.Color),

				new InOutID_inOutType (Enums.nv_String_0, VariableType.String),
				new InOutID_inOutType (Enums.nv_String_1, VariableType.String),
				new InOutID_inOutType (Enums.nv_String_2, VariableType.String),
				new InOutID_inOutType (Enums.nv_String_3, VariableType.String),
				new InOutID_inOutType (Enums.nv_String_4, VariableType.String),
				new InOutID_inOutType (Enums.nv_String_5, VariableType.String),
				new InOutID_inOutType (Enums.nv_String_6, VariableType.String),
				new InOutID_inOutType (Enums.nv_String_7, VariableType.String),
				new InOutID_inOutType (Enums.nv_String_8, VariableType.String),
				new InOutID_inOutType (Enums.nv_String_9, VariableType.String),

				new InOutID_inOutType (Enums.nv_Bool_0, VariableType.Bool),
				new InOutID_inOutType (Enums.nv_Bool_1, VariableType.Bool),
				new InOutID_inOutType (Enums.nv_Bool_2, VariableType.Bool),
				new InOutID_inOutType (Enums.nv_Bool_3, VariableType.Bool),
				new InOutID_inOutType (Enums.nv_Bool_4, VariableType.Bool),
				new InOutID_inOutType (Enums.nv_Bool_5, VariableType.Bool),
				new InOutID_inOutType (Enums.nv_Bool_6, VariableType.Bool),
				new InOutID_inOutType (Enums.nv_Bool_7, VariableType.Bool),
				new InOutID_inOutType (Enums.nv_Bool_8, VariableType.Bool),
				new InOutID_inOutType (Enums.nv_Bool_9, VariableType.Bool),

				new InOutID_inOutType (Enums.nv_Texture2D_0, VariableType.Texture2D),
				new InOutID_inOutType (Enums.nv_Texture2D_1, VariableType.Texture2D),
				new InOutID_inOutType (Enums.nv_Texture2D_2, VariableType.Texture2D),
				new InOutID_inOutType (Enums.nv_Texture2D_3, VariableType.Texture2D),
				new InOutID_inOutType (Enums.nv_Texture2D_4, VariableType.Texture2D),
				new InOutID_inOutType (Enums.nv_Texture2D_5, VariableType.Texture2D),
				new InOutID_inOutType (Enums.nv_Texture2D_6, VariableType.Texture2D),
				new InOutID_inOutType (Enums.nv_Texture2D_7, VariableType.Texture2D),
				new InOutID_inOutType (Enums.nv_Texture2D_8, VariableType.Texture2D),
				new InOutID_inOutType (Enums.nv_Texture2D_9, VariableType.Texture2D),

				new InOutID_inOutType (Enums.nv_Material_0, VariableType.Material),
				new InOutID_inOutType (Enums.nv_Material_1, VariableType.Material),
				new InOutID_inOutType (Enums.nv_Material_2, VariableType.Material),
				new InOutID_inOutType (Enums.nv_Material_3, VariableType.Material),
				new InOutID_inOutType (Enums.nv_Material_4, VariableType.Material),
				new InOutID_inOutType (Enums.nv_Material_5, VariableType.Material),
				new InOutID_inOutType (Enums.nv_Material_6, VariableType.Material),
				new InOutID_inOutType (Enums.nv_Material_7, VariableType.Material),
				new InOutID_inOutType (Enums.nv_Material_8, VariableType.Material),
				new InOutID_inOutType (Enums.nv_Material_9, VariableType.Material),

				new InOutID_inOutType (Enums.nv_Shader_0, VariableType.Shader),
				new InOutID_inOutType (Enums.nv_Shader_1, VariableType.Shader),
				new InOutID_inOutType (Enums.nv_Shader_2, VariableType.Shader),
				new InOutID_inOutType (Enums.nv_Shader_3, VariableType.Shader),
				new InOutID_inOutType (Enums.nv_Shader_4, VariableType.Shader),
				new InOutID_inOutType (Enums.nv_Shader_5, VariableType.Shader),
				new InOutID_inOutType (Enums.nv_Shader_6, VariableType.Shader),
				new InOutID_inOutType (Enums.nv_Shader_7, VariableType.Shader),
				new InOutID_inOutType (Enums.nv_Shader_8, VariableType.Shader),
				new InOutID_inOutType (Enums.nv_Shader_9, VariableType.Shader),
			};

			nv_inputsIDs = new string[nv_inOutID_inOutType.Length];

			nv_inputsTypes = new VariableType[nv_inOutID_inOutType.Length];

			for (int i = 0; i < nv_inOutID_inOutType.Length; i++)
			{
				nv_inputsIDs [i] = nv_inOutID_inOutType [i].id;

				nv_inputsTypes [i] = nv_inOutID_inOutType [i].type;
			}

			Nv_DisactivateAllInputs ();
		}


		InOutID_inOutType [] nv_inOutID_inOutType = new InOutID_inOutType[nv_count];

	
		public string [] nv_inputsIDs = new string[nv_count];
		public VariableType [] nv_inputsTypes = new VariableType[nv_count];



		public string [] nv_Shader_IDs = new string[]
		{
			Enums.nv_Shader_0,
			Enums.nv_Shader_1,
			Enums.nv_Shader_2,
			Enums.nv_Shader_3,
			Enums.nv_Shader_4,
			Enums.nv_Shader_5,
			Enums.nv_Shader_6,
			Enums.nv_Shader_7,
			Enums.nv_Shader_8,
			Enums.nv_Shader_9,
		};

		public Shader [] nv_Shader = new Shader[nv_perTypeCount];


		public string [] nv_Material_IDs = new string[]
		{
			Enums.nv_Material_0,
			Enums.nv_Material_1,
			Enums.nv_Material_2,
			Enums.nv_Material_3,
			Enums.nv_Material_4,
			Enums.nv_Material_5,
			Enums.nv_Material_6,
			Enums.nv_Material_7,
			Enums.nv_Material_8,
			Enums.nv_Material_9,
		};

		public Material [] nv_Material = new Material[nv_perTypeCount];


		public string [] nv_Texture2D_IDs = new string[]
		{
			Enums.nv_Texture2D_0,
			Enums.nv_Texture2D_1,
			Enums.nv_Texture2D_2,
			Enums.nv_Texture2D_3,
			Enums.nv_Texture2D_4,
			Enums.nv_Texture2D_5,
			Enums.nv_Texture2D_6,
			Enums.nv_Texture2D_7,
			Enums.nv_Texture2D_8,
			Enums.nv_Texture2D_9,
		};

		public Texture2D [] nv_Texture2D = new Texture2D[nv_perTypeCount];


		public string [] nv_Bool_IDs = new string[]
		{
			Enums.nv_Bool_0,
			Enums.nv_Bool_1,
			Enums.nv_Bool_2,
			Enums.nv_Bool_3,
			Enums.nv_Bool_4,
			Enums.nv_Bool_5,
			Enums.nv_Bool_6,
			Enums.nv_Bool_7,
			Enums.nv_Bool_8,
			Enums.nv_Bool_9,
		};

		public bool [] nv_Bool = new bool[nv_perTypeCount];


		public string [] nv_String_IDs = new string[]
		{
			Enums.nv_String_0,
			Enums.nv_String_1,
			Enums.nv_String_2,
			Enums.nv_String_3,
			Enums.nv_String_4,
			Enums.nv_String_5,
			Enums.nv_String_6,
			Enums.nv_String_7,
			Enums.nv_String_8,
			Enums.nv_String_9,
		};

		public string [] nv_String = new string[nv_perTypeCount];


		public string [] nv_Color_IDs = new string[]
		{
			Enums.nv_Color_0,
			Enums.nv_Color_1,
			Enums.nv_Color_2,
			Enums.nv_Color_3,
			Enums.nv_Color_4,
			Enums.nv_Color_5,
			Enums.nv_Color_6,
			Enums.nv_Color_7,
			Enums.nv_Color_8,
			Enums.nv_Color_9,
		};

		public Color [] nv_Color = new Color[nv_perTypeCount];


		public string [] nv_Rect_IDs = new string[]
		{
			Enums.nv_Rect_0,
			Enums.nv_Rect_1,
			Enums.nv_Rect_2,
			Enums.nv_Rect_3,
			Enums.nv_Rect_4,
			Enums.nv_Rect_5,
			Enums.nv_Rect_6,
			Enums.nv_Rect_7,
			Enums.nv_Rect_8,
			Enums.nv_Rect_9,
		};

		public Rect [] nv_Rect = new Rect[nv_perTypeCount];


		public string [] nv_int_IDs = new string[]
		{
			Enums.nv_int_0,
			Enums.nv_int_1,
			Enums.nv_int_2,
			Enums.nv_int_3,
			Enums.nv_int_4,
			Enums.nv_int_5,
			Enums.nv_int_6,
			Enums.nv_int_7,
			Enums.nv_int_8,
			Enums.nv_int_9,
		};

		public int [] nv_int = new int[nv_perTypeCount];


		public string [] nv_float_IDs = new string[]
		{
			Enums.nv_float_0,
			Enums.nv_float_1,
			Enums.nv_float_2,
			Enums.nv_float_3,
			Enums.nv_float_4,
			Enums.nv_float_5,
			Enums.nv_float_6,
			Enums.nv_float_7,
			Enums.nv_float_8,
			Enums.nv_float_9,
		};

		public float [] nv_float = new float[nv_perTypeCount];


		public string [] nv_Vector4_IDs = new string[]
		{
			Enums.nv_Vector4_0,
			Enums.nv_Vector4_1,
			Enums.nv_Vector4_2,
			Enums.nv_Vector4_3,
			Enums.nv_Vector4_4,
			Enums.nv_Vector4_5,
			Enums.nv_Vector4_6,
			Enums.nv_Vector4_7,
			Enums.nv_Vector4_8,
			Enums.nv_Vector4_9,
		};

		public Vector4 [] nv_Vector4 = new Vector4[nv_perTypeCount];


		public string [] nv_Vector2_IDs = new string[]
		{
			Enums.nv_Vector2_0,
			Enums.nv_Vector2_1,
			Enums.nv_Vector2_2,
			Enums.nv_Vector2_3,
			Enums.nv_Vector2_4,
			Enums.nv_Vector2_5,
			Enums.nv_Vector2_6,
			Enums.nv_Vector2_7,
			Enums.nv_Vector2_8,
			Enums.nv_Vector2_9,
		};

		public Vector2 [] nv_Vector2 = new Vector2[nv_perTypeCount];


		public string [] nv_Vector3_IDs = new string[]
		{
			Enums.nv_Vector3_0,
			Enums.nv_Vector3_1,
			Enums.nv_Vector3_2,
			Enums.nv_Vector3_3,
			Enums.nv_Vector3_4,
			Enums.nv_Vector3_5,
			Enums.nv_Vector3_6,
			Enums.nv_Vector3_7,
			Enums.nv_Vector3_8,
			Enums.nv_Vector3_9,
		};

		public Vector3 [] nv_Vector3 = new Vector3[nv_perTypeCount];
		#endregion varTypeAndID



		void Nv_DrawInputField (string inID, ref int val, string label)
		{
			DrawLogicNodeLabel (label, 0, 2);

			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = EditorGUI.IntField (suitRect, val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;
		}

		void Nv_DrawInputField (string inID, ref float val, string label)
		{
			DrawLogicNodeLabel (label, 0, 2);

			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = EditorGUI.FloatField (suitRect, val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;
		}

		void Nv_DrawInputField (string inID, ref Vector2 val, string label)
		{
			DrawLogicNodeLabel (label, 0, 2);

			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = EditorGUI.Vector2Field (suitRect, "", val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;
		}

		void Nv_DrawInputField (string inID, ref Vector3 val, string label, bool twoLines)
		{
			Rect suitRect = new Rect ();

			if ( ! twoLines)
			{
				DrawLogicNodeLabel (label, 0, 2);

				suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);
			}
			else
			{
				DrawLogicNodeLabel (label);

				suitRect = GetSuitableRect (FieldDrawType.label);
			}

			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = EditorGUI.Vector3Field (suitRect, "", val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;
		}

		void Nv_DrawInputField (string inID, ref Vector4 val, string label)
		{
			Rect suitRect = new Rect ();

			DrawLogicNodeLabel (label);

			suitRect = GetSuitableRect (FieldDrawType.label);


			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = EditorGUI.Vector4Field (suitRect, "", val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;
		}

		void Nv_DrawInputField (string inID, ref Rect val, string label)
		{
			DrawLogicNodeLabel (label, 0, 2);

			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = EditorGUI.RectField (suitRect, val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;

			suitRect = GetSuitableRect (FieldDrawType.variable);
		}

		void Nv_DrawInputField (string inID, ref Color val, string label)
		{
			DrawLogicNodeLabel (label, 0, 2);

			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = EditorGUI.ColorField (suitRect, "", val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;
		}

		void Nv_DrawInputField (string inID, ref string val, string label)
		{
			DrawLogicNodeLabel (label, 0, 2);

			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = EditorGUI.TextField (suitRect, "", val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;
		}

		void Nv_DrawInputField (string inID, ref bool val, string label)
		{
			DrawLogicNodeLabel (label, 0, 2);

			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = EditorGUI.Toggle (suitRect, val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;
		}


		void Nv_DrawInputField (string inID, ref Texture2D val, string label)
		{
			DrawLogicNodeLabel (label, 0, 2);

			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = 
							EditorGUI.ObjectField (suitRect, 
								val, typeof (Texture2D), true) 
							as Texture2D;

						//AssignAssetID  (ref texture2DValues [valueID], ref texture2DValuesOld [valueID], ref texture2DValuesAssetsID [valueID]);
						ScriptsCreatedByDiamond.IdentifiedObjectsActions.SetIdentifiedObject (
							FieldUniqueID (inID), val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;
		}

		void Nv_DrawInputField (string inID, ref Material val, string label)
		{
			DrawLogicNodeLabel (label, 0, 2);

			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = 
							EditorGUI.ObjectField (suitRect, 
								val, typeof (Material), true) 
							as Material;

						//AssignAssetID  (ref texture2DValues [valueID], ref texture2DValuesOld [valueID], ref texture2DValuesAssetsID [valueID]);
						ScriptsCreatedByDiamond.IdentifiedObjectsActions.SetIdentifiedObject (
							FieldUniqueID (inID), val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;
		}

		void Nv_DrawInputField (string inID, ref Shader val, string label)
		{
			DrawLogicNodeLabel (label, 0, 2);

			Rect suitRect = GetSuitableRect (FieldDrawType.label, 1, 2);

			int varIndex = Nv_IndexOfInID (inID);

			Nv_DrawInput (InOutGatePreCenter (suitRect), inID);

			if (maximized)
			{
				if (varIndex > -1 && varIndex < nv_publicInputs.Length)
				{
					if ( ! nv_publicInputs [varIndex])
					{
						val = 
							EditorGUI.ObjectField (suitRect, 
								val, typeof (Shader), true) 
							as Shader;

						//AssignAssetID  (ref texture2DValues [valueID], ref texture2DValuesOld [valueID], ref texture2DValuesAssetsID [valueID]);
						ScriptsCreatedByDiamond.IdentifiedObjectsActions.SetIdentifiedObject (
							FieldUniqueID (inID), val);
					}
					else if (nv_publicInputs [varIndex])
					{
						Nv_DrawPublicInputFieldLabel (varIndex, suitRect);
					}
				}
			}

			if (varIndex > -1 && varIndex < nv_activeInputsFields.Length)
				nv_activeInputsFields [varIndex] = true;
		}





		void Nv_DrawPublicInputFieldLabel (int varIndex, Rect suitRect)
		{
			EditorGUI.LabelField (suitRect,
				nv_inputsTypes [varIndex].ToString (), 
				Skins.guiSkin.GetStyle (Skins.logicNodeResult));
		}

		void Nv_DrawVariableTypeColor (Rect inOutRect, VariableType varType)
		{
			float reductFactor = 0.3f;

			Rect drawingRect = new Rect (
				rect.x + rect.width - (1f+reductFactor)*inOutRect.width, inOutRect.y + 
				reductFactor*inOutRect.height, 
				inOutRect.width*(1f-reductFactor), inOutRect.height*(1f-reductFactor));

			GUI.backgroundColor = Diamond.namesToSave.variableTypeColor [(int)varType];

			GUI.Box (drawingRect, "");

			GUI.backgroundColor = Color.white;
		}

		void Nv_DrawFloatingType (Rect inOutRect, VariableType varType)
		{
			if ( ! inOutRect.Contains (eGlobal.mousePosition))
				return;

			Vector2 pos = inOutRect.center;

			string inOutIDTypeName = varType.ToString ();

			float characterShifting = 2.5f*(float)inOutIDTypeName.Length;

			Vector2 shift = new Vector2 (40f, -35f);

			pos = pos + new Vector2 ((-shift.x) - characterShifting, shift.y);

			DrawFloatingMessage (pos, inOutIDTypeName);
		}

		void Nv_DrawLink (Vector2 start, Vector2 end, bool permittedInput, VariableType varType)
		{
			float linkTanCoef = LinkTanCoef (start, end);

			Vector2 startTan = start + new Vector2 (linkTanCoef*tanLength, 0f);

			Vector2 endTan = end + new Vector2 (-linkTanCoef*tanLength, 0f);

			Color variableTypeLinnkColor = Diamond.namesToSave.variableTypeColor [(int)varType];

			Color linkColorToDraw = permittedInput? variableTypeLinnkColor: 
				ColorModifierSimple.SemiDot (variableTypeLinnkColor, 
					new Vector4 (1f, 1f, 1f, notPermittedInputAlphaColor));
			
			Handles.DrawBezier (start, end, startTan, endTan, linkColorToDraw, null, linkWidth);
		}


		void Nv_DrawInput (Vector2 atPosition, string inID)
		{
			Rect inOutRect = InOutRect (true, atPosition);

			if (Nv_IndexOfInID (inID) > -1 && Nv_IndexOfInID (inID) < nv_publicInputs.Length)
			{
				if ( ! nv_publicInputs [Nv_IndexOfInID (inID)])
				{
					GUI.Box (inOutRect, "", Skins.guiSkin.GetStyle (GetInRectSkin (inID, -1)));

					Nv_AssignInRect (inID, inOutRect);

					Nv_ActiveThisInput (inID);

					Nv_DrawInputPermissionButton (inOutRect, Nv_IndexOfInID (inID));
				}
			}


			//
			Rect inputOptionsRect = 
				new Rect (inOutRect.x + 1f*inOutRect.width, 
					inOutRect.y, inOutRect.width, inOutRect.height);

			string inputOptionsRectSkin = Skins.asterix;
			if (Nv_IndexOfInID (inID) > -1 && Nv_IndexOfInID (inID) < nv_publicInputs.Length)
			if (nv_publicInputs [Nv_IndexOfInID (inID)])
				inputOptionsRectSkin = Skins.publicVariable;

			if (GUI.Button (inputOptionsRect, "", Skins.guiSkin.GetStyle (inputOptionsRectSkin)) && 
				eGlobal.button == 0)
			{
				Nv_OpenInputFieldOptionsMenu (inOutRect, inID);
			}

			//if (Nv_IndexOfInID (inID) > -1 && Nv_IndexOfInID (inID) < nv_publicInputs.Length)
			//{
			//	if ( ! nv_publicInputs [Nv_IndexOfInID (inID)])
			//	{
			//		GUI.Box (inOutRect, "", Skins.guiSkin.GetStyle (GetInRectSkin (inID, -1)));
			//
			//		Nv_AssignInRect (inID, inOutRect);
			//
			//		Nv_ActiveThisInput (inID);
			//
			//		Nv_DrawInputPermissionButton (inOutRect, Nv_IndexOfInID (inID));
			//	}
			//}



			Nv_DrawCurrentLink (inOutRect, inID);

			if ( ! string.IsNullOrEmpty (logic.inOutAdressCurrentToLink))
				Nv_AssignInputSource (inOutRect, inID);

			if ( ! string.IsNullOrEmpty (nv_inputsSources [Nv_IndexOfInID (inID)]))
				Nv_DrawEstablishedLinks (-1);

			Nv_DrawFloatingType (inOutRect, nv_inputsTypes [Nv_IndexOfInID (inID)]);

			Nv_DrawVariableTypeColor (inOutRect, nv_inputsTypes [Nv_IndexOfInID (inID)]);
		}

		void Nv_DrawEstablishedLinks (int currentInOutListIndex)
		{
			List <int> activeLinkedInputs = new List<int> ();

			for (int i = 0; i < nv_activeInputs.Length; i++)
			{
				if (nv_activeInputs [i])
				{
					if ( ! string.IsNullOrEmpty (nv_inputsSources [i]))
					{
						activeLinkedInputs.Add (i);
					}
				}
			}

			if (activeLinkedInputs.Count == 0)
				return;

			for (int i = 0; i < activeLinkedInputs.Count; i++)
			{
				if (nv_inputsSources [activeLinkedInputs [i]].Length < 1)
					continue;

				//if (nv_inputsSources [activeLinkedInputs [i]][0] == 'v')
				//{
				//	Nv_LinkInputWithProjectVariable (nv_inputsIDs [activeLinkedInputs [i]], 
				//		nv_inputsSources [activeLinkedInputs [i]],
				//		activeLinkedInputs [i]);
				//}
				//else
				//{
				if (IsAListOutSource (nv_inputsSources [activeLinkedInputs [i]]))
				{
					Nv_LinkInput (nv_inputsIDs [activeLinkedInputs [i]], nv_inputsSources [activeLinkedInputs [i]],
						activeLinkedInputs [i], currentInOutListIndex);
				}
				else if ( ! IsAListOutSource (nv_inputsSources [activeLinkedInputs [i]]))
				{
					Nv_LinkInput (nv_inputsIDs [activeLinkedInputs [i]], nv_inputsSources [activeLinkedInputs [i]],
						activeLinkedInputs [i]);
				}
				//}
			}
		}

		void Nv_LinkInput (string inID, string inSource, int inIndex)
		{
			if (string.IsNullOrEmpty (inSource))
				return;

			string sourceUID = InOutAdressCurrentToLinkToUniqueID (inSource);

			if (sourceUID.Length > 0)
			{
				//if (sourceUID [0] == 'v')
				//{
				//	if ( ! Diamond.projectVariables.ProjectVariableStillHereOnUniqueID (sourceUID))
				//	{
				//		nv_inputsSources [inIndex] = "";
				//
				//		return;
				//	}
				//}
				//else 
				if ( ! SourceLogicNodeStillHereOnUniqueID (sourceUID))
				{
					nv_inputsSources [inIndex] = "";

					return;
				}
			}



			LogicNode sourceLogicNode = GetLogicNodeOnUniqueID (sourceUID);


			string sourceNodeOutID = InOutAdressCurrentToLinkToInOutID (inSource);

			int sourceNodeOutIndex = IndexOfOutID (sourceNodeOutID);

			if (sourceNodeOutIndex < 0 || sourceNodeOutIndex > sourceLogicNode.activeOutputs.Length-1)
				return;

			if ( ! sourceLogicNode.activeOutputs [sourceNodeOutIndex])
				return;


			Nv_DrawLink (sourceLogicNode.outputsRects [sourceNodeOutIndex].center, 
				nv_inputsRects [inIndex].center, nv_permittedInputs [inIndex], nv_inputsTypes [inIndex]);

			Nv_LinkInputAction (inIndex, inID, sourceNodeOutID, sourceLogicNode);

		}
		void Nv_LinkInput (string inID, string inSource, int inIndex, int currentInOutListIndex)
		{
			if ( ! string.IsNullOrEmpty  (logic.inOutAdressCurrentToLink))
			{
				if (InOutAdressCurrentToLinkToUniqueID (logic.inOutAdressCurrentToLink) == inSource)
				{
					return;
				}
			}

			//
			if (string.IsNullOrEmpty (inSource))
				return;

			string sourceUID = InOutAdressCurrentToLinkToUniqueID (inSource);

			if (sourceUID.Length > 0)
			{
				if (sourceUID [0] == 'v')
				{
					if ( ! Diamond.projectVariables.ProjectVariableStillHereOnUniqueID (sourceUID))
					{
						nv_inputsSources [inIndex] = "";

						return;
					}
				}
				else if ( ! SourceLogicNodeStillHereOnUniqueID (sourceUID))
				{
					nv_inputsSources [inIndex] = "";

					return;
				}
			}
			//

			//string sourceUID = InOutAdressCurrentToLinkToUniqueID (inSource);
			//
			//if ( ! SourceLogicNodeStillHereOnUniqueID (sourceUID))
			//{
			//	nv_inputsSources [inIndex] = "";
			//
			//	return;
			//}

			LogicNode sourceLogicNode = GetLogicNodeOnUniqueID (sourceUID);


			string sourceNodeOutID = InOutAdressCurrentToLinkToInOutID (inSource, currentInOutListIndex);

			int sourceNodeOutIndex = IndexOfOutID (sourceNodeOutID);

			if (sourceNodeOutIndex < 0 || sourceNodeOutIndex > sourceLogicNode.activeOutputs.Length-1)
				return;

			if ( ! sourceLogicNode.activeOutputs [sourceNodeOutIndex])
				return;


			//DrawLink (sourceLogicNode.outputsRects [sourceNodeOutIndex].center, nv_inputsRects [inIndex].center);


			if (InoutAdressToLinkToListIndex (inSource) > -1 && 
				InoutAdressToLinkToListIndex (inSource) < sourceLogicNode.outputListCounts [sourceNodeOutIndex])
			{
				Nv_DrawLink (GetDecaledListRect (sourceLogicNode, sourceNodeOutIndex, inSource).center, 
					nv_inputsRects [inIndex].center, nv_permittedInputs [inIndex], nv_inputsTypes [inIndex]);
			}

			Nv_LinkInputAction (inIndex, inID, sourceNodeOutID, sourceLogicNode);

		}





		void Nv_LinkInputAction (int inIndex, string inID, string sourceNodeOutID, LogicNode sourceLogicNode)
		{	
			if ( ! logic.playing)
				return;

			if ( ! nv_permittedInputs [inIndex])
				return;

			int lstIndex;



			switch (nv_inputsTypes [inIndex])
			{

			case VariableType.rect:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);

				switch (sourceNodeOutID)
				{
				case Enums.rectList_ID:
					Nv_LinkInputAction_Rect (inID, lstIndex, sourceLogicNode);
					break;


				case Enums.rectValue_ID:
					Nv_LinkInputAction_Rect (inID, sourceLogicNode.rectValue);
					break;

				case Enums.rectValues_0_ID:
					Nv_LinkInputAction_Rect (inID, sourceLogicNode.rectValues [0]);
					break;

				case Enums.rectValues_1_ID:
					Nv_LinkInputAction_Rect (inID, sourceLogicNode.rectValues [1]);
					break;
				}

				//switch (inID)
				//{
				//case Enums.rectValues_0_ID:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.rectList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.rectListValue.Count)
				//		{
				//			rectValues [0] = sourceLogicNode.rectListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.rectValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.rectList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.rectListValue.Count)
				//		{
				//			rectValues [1] = sourceLogicNode.rectListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}

				//switch (inID)
				//{
				//case Enums.rectValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.rectValue_ID:
				//		LinkInput_rect_rectValues_0_ID_rectValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rectValues_0_ID:
				//		LinkInput_rect_rectValues_0_ID_rectValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rectValues_1_ID:
				//		LinkInput_rect_rectValues_0_ID_rectValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.rectValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.rectValue_ID:
				//		LinkInput_rect_rectValues_1_ID_rectValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rectValues_0_ID:
				//		LinkInput_rect_rectValues_1_ID_rectValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rectValues_1_ID:
				//		LinkInput_rect_rectValues_1_ID_rectValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				break;




			case VariableType.Vector4:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);

				switch (sourceNodeOutID)
				{
				case Enums.vector4List_ID:
					Nv_LinkInputAction_Vector4 (inID, lstIndex, sourceLogicNode);
					break;


				case Enums.vector4Value_ID:
					Nv_LinkInputAction_Vector4 (inID, sourceLogicNode.vector4Value);
					break;

				case Enums.vector4Values_0_ID:
					Nv_LinkInputAction_Vector4 (inID, sourceLogicNode.vector4Values [0]);
					break;

				case Enums.vector4Values_1_ID:
					Nv_LinkInputAction_Vector4 (inID, sourceLogicNode.vector4Values [1]);
					break;
				}


				//switch (inID)
				//{
				//case Enums.vector4Values_0_ID:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector4List_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.vector4ListValue.Count)
				//		{
				//			vector4Values [0] = sourceLogicNode.vector4ListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.vector4Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector4List_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.vector4ListValue.Count)
				//		{
				//			vector4Values [1] = sourceLogicNode.vector4ListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}

				//switch (inID)
				//{
				//case Enums.vector4Values_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector4Value_ID:
				//		LinkInput_Vector4_vector4Values_0_ID_vector4Value_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector4Values_0_ID:
				//		LinkInput_Vector4_vector4Values_0_ID_vector4Values_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector4Values_1_ID:
				//		LinkInput_Vector4_vector4Values_0_ID_vector4Values_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.vector4Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector4Value_ID:
				//		LinkInput_Vector4_vector4Values_1_ID_vector4Value_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector4Values_0_ID:
				//		LinkInput_Vector4_vector4Values_1_ID_vector4Values_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector4Values_1_ID:
				//		LinkInput_Vector4_vector4Values_1_ID_vector4Values_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				break;




			case VariableType.Shader:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);

				switch (sourceNodeOutID)
				{
				case Enums.shaderList_ID:
					Nv_LinkInputAction_Shader (inID, lstIndex, sourceLogicNode);
					break;


				case Enums.shaderValue_ID:
					Nv_LinkInputAction_Shader (inID, sourceLogicNode.shaderValue);
					break;

				case Enums.shaderValues_0_ID:
					Nv_LinkInputAction_Shader (inID, sourceLogicNode.shaderValues [0]);
					break;

				case Enums.shaderValues_1_ID:
					Nv_LinkInputAction_Shader (inID, sourceLogicNode.shaderValues [1]);
					break;
				}



				//switch (inID)
				//{
				//case Enums.shaderValues_0_ID:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.shaderList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.shaderListValue.Count)
				//		{
				//			shaderValues [0] = sourceLogicNode.shaderListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.shaderValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.shaderList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.shaderListValue.Count)
				//		{
				//			shaderValues [1] = sourceLogicNode.shaderListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}

				//switch (inID)
				//{
				//case Enums.shaderValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.shaderValue_ID:
				//		LinkInput_Shader_shaderValues_0_ID_shaderValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.shaderValues_0_ID:
				//		LinkInput_Shader_shaderValues_0_ID_shaderValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.shaderValues_1_ID:
				//		LinkInput_Shader_shaderValues_0_ID_shaderValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.shaderValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.shaderValue_ID:
				//		LinkInput_Shader_shaderValues_1_ID_shaderValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.shaderValues_0_ID:
				//		LinkInput_Shader_shaderValues_1_ID_shaderValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.shaderValues_1_ID:
				//		LinkInput_Shader_shaderValues_1_ID_shaderValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				break;


			case VariableType.Texture2D:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);

				switch (sourceNodeOutID)
				{
				case Enums.texture2DList_ID:
					Nv_LinkInputAction_Texture2D (inID, lstIndex, sourceLogicNode);
					break;


				case Enums.texture2DValue_ID:
					Nv_LinkInputAction_Texture2D (inID, sourceLogicNode.texture2DValue);
					break;

				case Enums.texture2DValues_0_ID:
					Nv_LinkInputAction_Texture2D (inID, sourceLogicNode.texture2DValues [0]);
					break;

				case Enums.texture2DValues_1_ID:
					Nv_LinkInputAction_Texture2D (inID, sourceLogicNode.texture2DValues [1]);
					break;
				}




				//switch (inID)
				//{
				//case Enums.texture2DValues_0_ID:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.texture2DList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.texture2DListValue.Count)
				//		{
				//			texture2DValues [0] = sourceLogicNode.texture2DListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.texture2DValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.texture2DList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.texture2DListValue.Count)
				//		{
				//			texture2DValues [1] = sourceLogicNode.texture2DListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}


				//switch (inID)
				//{
				//case Enums.texture2DValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.texture2DValue_ID:
				//		LinkInput_Texture2D_texture2DValues_0_ID_texture2DValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.texture2DValues_0_ID:
				//		LinkInput_Texture2D_texture2DValues_0_ID_texture2DValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.texture2DValues_1_ID:
				//		LinkInput_Texture2D_texture2DValues_0_ID_texture2DValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.texture2DValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.texture2DValue_ID:
				//		LinkInput_Texture2D_texture2DValues_1_ID_texture2DValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.texture2DValues_0_ID:
				//		LinkInput_Texture2D_texture2DValues_1_ID_texture2DValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.texture2DValues_1_ID:
				//		LinkInput_Texture2D_texture2DValues_1_ID_texture2DValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				break;



			case VariableType.Material:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);


				switch (sourceNodeOutID)
				{
				case Enums.materialList_ID:
					Nv_LinkInputAction_Material (inID, lstIndex, sourceLogicNode);
					break;


				case Enums.materialValue_ID:
					Nv_LinkInputAction_Material (inID, sourceLogicNode.materialValue);
					break;

				case Enums.materialValues_0_ID:
					Nv_LinkInputAction_Material (inID, sourceLogicNode.materialValues [0]);
					break;

				case Enums.materialValues_1_ID:
					Nv_LinkInputAction_Material (inID, sourceLogicNode.materialValues [1]);
					break;
				}




				//switch (inID)
				//{
				//case Enums.materialValues_0_ID:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.materialList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.materialsListValue.Count)
				//		{
				//			materialValues [0] = sourceLogicNode.materialsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.materialValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.materialList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.materialsListValue.Count)
				//		{
				//			materialValues [1] = sourceLogicNode.materialsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}


				//switch (inID)
				//{
				//case Enums.materialValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.materialValue_ID:
				//		LinkInput_Material_materialValues_0_materialValue ( sourceLogicNode);
				//		break;
				//
				//	case Enums.materialValues_0_ID:
				//		LinkInput_Material_materialValues_0_materialValues_0 ( sourceLogicNode);
				//		break;
				//
				//	case Enums.materialValues_1_ID:
				//		LinkInput_Material_materialValues_0_materialValues_1 ( sourceLogicNode);
				//		break;
				//
				//	}
				//	break;
				//
				//case Enums.materialValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.materialValue_ID:
				//		LinkInput_Material_materialValues_1_materialValue ( sourceLogicNode);
				//		break;
				//
				//	case Enums.materialValues_0_ID:
				//		LinkInput_Material_materialValues_1_materialValues_0 ( sourceLogicNode);
				//		break;
				//
				//	case Enums.materialValues_1_ID:
				//		LinkInput_Material_materialValues_1_materialValues_1 ( sourceLogicNode);
				//		break;
				//
				//	}
				//	break;
				//}

				break;



			case VariableType.Bool:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);

				switch (sourceNodeOutID)
				{
				case Enums.boolsList_ID:
					Nv_LinkInputAction_Bool (inID, lstIndex, sourceLogicNode);
					break;


				case Enums.boolValue_ID:
					Nv_LinkInputAction_Bool (inID, sourceLogicNode.boolValue);
					break;

				case Enums.boolValues_0_ID:
					Nv_LinkInputAction_Bool (inID, sourceLogicNode.boolValues [0]);
					break;

				case Enums.boolValues_1_ID:
					Nv_LinkInputAction_Bool (inID, sourceLogicNode.boolValues [1]);
					break;


				case Enums.m44ValueIsIdentity_ID:
					Nv_LinkInputAction_Bool (inID, sourceLogicNode.m44ValueIsIdentity);
					break;

				case Enums.m44ValueInvertible_ID:
					Nv_LinkInputAction_Bool (inID, sourceLogicNode.m44ValueInvertible);
					break;


				case Enums.OffMeshLinkData_activated_ID:
					Nv_LinkInputAction_Bool (inID, sourceLogicNode.OffMeshLinkData_activated);
					break;

				case Enums.OffMeshLinkData_valid_ID:
					Nv_LinkInputAction_Bool (inID, sourceLogicNode.OffMeshLinkData_valid);
					break;


				case Enums.NavMeshHit_hit_ID:
					Nv_LinkInputAction_Bool (inID, sourceLogicNode.NavMeshHit_hit);
					break;
				}



				//switch (inID)
				//{
				//case Enums.boolValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.boolValue_ID:
				//		LinkInput_Bool_boolValues_0_ID_boolValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boolValues_0_ID:
				//		LinkInput_Bool_boolValues_0_ID_boolValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boolValues_1_ID:
				//		LinkInput_Bool_boolValues_0_ID_boolValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.boolValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.boolValue_ID:
				//		LinkInput_Bool_boolValues_1_ID_boolValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boolValues_0_ID:
				//		LinkInput_Bool_boolValues_1_ID_boolValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boolValues_1_ID:
				//		LinkInput_Bool_boolValues_1_ID_boolValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.doIt_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.boolValue_ID:
				//		LinkInput_Bool_doIt_ID_boolValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boolValues_0_ID:
				//		LinkInput_Bool_doIt_ID_boolValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boolValues_1_ID:
				//		LinkInput_Bool_doIt_ID_boolValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.boolValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.m44ValueIsIdentity_ID:
				//		LinkInput_M44_boolValues_0_ID_m44ValueIsIdentity_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.m44ValueInvertible_ID:
				//		LinkInput_M44_boolValues_0_ID_m44ValueInvertible_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.boolValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.m44ValueIsIdentity_ID:
				//		LinkInput_M44_boolValues_1_ID_m44ValueIsIdentity_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.m44ValueInvertible_ID:
				//		LinkInput_M44_boolValues_1_ID_m44ValueInvertible_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.boolValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.OffMeshLinkData_activated_ID:
				//		LinkInput_OffMeshLink_boolValues_0_ID_OffMeshLinkData_activated_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.OffMeshLinkData_valid_ID:
				//		LinkInput_OffMeshLink_boolValues_0_ID_OffMeshLinkData_valid_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.boolValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.OffMeshLinkData_activated_ID:
				//		LinkInput_OffMeshLink_boolValues_1_ID_OffMeshLinkData_activated_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.OffMeshLinkData_valid_ID:
				//		LinkInput_OffMeshLink_boolValues_1_ID_OffMeshLinkData_valid_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.doIt_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.OffMeshLinkData_activated_ID:
				//		LinkInput_OffMeshLink_doIt_ID_OffMeshLinkData_activated_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.OffMeshLinkData_valid_ID:
				//		LinkInput_OffMeshLink_doIt_ID_OffMeshLinkData_valid_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.boolValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_hit_ID:
				//		LinkInput_navMeshHit_boolValues_0_ID_NavMeshHit_hit_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.boolValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_hit_ID:
				//		LinkInput_navMeshHit_boolValues_1_ID_NavMeshHit_hit_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.doIt_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_hit_ID:
				//		LinkInput_navMeshHit_doIt_ID_NavMeshHit_hit_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}


				//switch (inID)
				//{
				//case Enums.boolValues_0_ID:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.boolsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.boolsListValue.Count)
				//		{
				//			boolValues [0] = sourceLogicNode.boolsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.boolValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.boolsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.boolsListValue.Count)
				//		{
				//			boolValues [1] = sourceLogicNode.boolsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}

				break;


			case VariableType.Color:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);

				switch (sourceNodeOutID)
				{
				case Enums.colorsList_ID:
					Nv_LinkInputAction_Color (inID, lstIndex, sourceLogicNode);
					break;


				case Enums.colorValue_ID:
					Nv_LinkInputAction_Color (inID, sourceLogicNode.colorValue);
					break;

				case Enums.colorValues_0_ID:
					Nv_LinkInputAction_Color (inID, sourceLogicNode.colorValues [0]);
					break;

				case Enums.colorValues_1_ID:
					Nv_LinkInputAction_Color (inID, sourceLogicNode.colorValues [1]);
					break;
				}


				//switch (inID)
				//{
				//case Enums.colorValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.colorValue_ID:
				//		LinkInput_Color_colorValues_0_ID_colorValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.colorValues_0_ID:
				//		LinkInput_Color_colorValues_0_ID_colorValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.colorValues_1_ID:
				//		LinkInput_Color_colorValues_0_ID_colorValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.colorValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.colorValue_ID:
				//		LinkInput_Color_colorValues_1_ID_colorValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.colorValues_0_ID:
				//		LinkInput_Color_colorValues_1_ID_colorValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.colorValues_1_ID:
				//		LinkInput_Color_colorValues_1_ID_colorValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.colorValues_0_ID:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.colorsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.colorsListValue.Count)
				//		{
				//			colorValues [0] = sourceLogicNode.colorsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.colorValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.colorsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.colorsListValue.Count)
				//		{
				//			colorValues [1] = sourceLogicNode.colorsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}

				break;


			case VariableType.Float:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);
			
				switch (sourceNodeOutID)
				{
				case Enums.floatsList_ID:
					Nv_LinkInputAction_float (inID, lstIndex, sourceLogicNode);
					break;

				case Enums.floatValue_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.floatValue);
					break;

				case Enums.floatValues_0_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.floatValues [0]);
					break;

				case Enums.floatValues_1_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.floatValues [1]);
					break;

				case Enums.floatValues_2_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.floatValues [2]);
					break;

				case Enums.raycastHitDistance_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.raycastHitDistance);
					break;


				case Enums.m44Value_0_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [0]);
					break;

				case Enums.m44Value_1_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [1]);
					break;

				case Enums.m44Value_2_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [2]);
					break;

				case Enums.m44Value_3_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [3]);
					break;

				case Enums.m44Value_4_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [4]);
					break;

				case Enums.m44Value_5_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [5]);
					break;

				case Enums.m44Value_6_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [6]);
					break;

				case Enums.m44Value_7_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [7]);
					break;

				case Enums.m44Value_8_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [8]);
					break;

				case Enums.m44Value_9_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [9]);
					break;

				case Enums.m44Value_10_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [10]);
					break;

				case Enums.m44Value_11_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [11]);
					break;

				case Enums.m44Value_12_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [12]);
					break;

				case Enums.m44Value_13_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [13]);
					break;

				case Enums.m44Value_14_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [14]);
					break;

				case Enums.m44Value_15_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44Value [15]);
					break;


				case Enums.m44ValueDeterminant_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.m44ValueDeterminant);
					break;

				case Enums.NavMeshHit_distance_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.NavMeshHit_distance);
					break;


				case Enums.hit2D_distance_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.hit2D_distance);
					break;

				case Enums.hit2D_fraction_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.hit2D_fraction);
					break;


				case Enums.touch_altitudeAngle_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.touch_altitudeAngle);
					break;

				case Enums.touch_azimuthAngle_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.touch_azimuthAngle);
					break;

				case Enums.touch_deltaTime_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.touch_deltaTime);
					break;

				case Enums.touch_maximumPossiblePressure_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.touch_maximumPossiblePressure);
					break;

				case Enums.touch_pressure_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.touch_pressure);
					break;

				case Enums.touch_radius_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.touch_radius);
					break;

				case Enums.touch_radiusVariance_ID:
					Nv_LinkInputAction_float (inID, sourceLogicNode.touch_radiusVariance);
					break;
				}



				//switch (inID)
				//{
				//case Enums.floatValues_0_ID:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.floatsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.floatsListValue.Count)
				//		{
				//			floatValues [0] = sourceLogicNode.floatsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.floatValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.floatsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.floatsListValue.Count)
				//		{
				//			floatValues [1] = sourceLogicNode.floatsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.floatValues_2_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.floatsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.floatsListValue.Count)
				//		{
				//			floatValues [2] = sourceLogicNode.floatsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}


				//switch (inID)
				//{
				//case Enums.floatValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.floatValue_ID:
				//		LinkInput_Float_floatValues_0_ID_floatValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_0_ID:
				//		LinkInput_Float_floatValues_0_ID_floatValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_1_ID:
				//		LinkInput_Float_floatValues_0_ID_floatValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_2_ID:
				//		LinkInput_Float_floatValues_0_ID_floatValues_2_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.floatValue_ID:
				//		LinkInput_Float_floatValues_1_ID_floatValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_0_ID:
				//		LinkInput_Float_floatValues_1_ID_floatValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_1_ID:
				//		LinkInput_Float_floatValues_1_ID_floatValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_2_ID:
				//		LinkInput_Float_floatValues_1_ID_floatValues_2_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_2_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.floatValue_ID:
				//		LinkInput_Float_floatValues_2_ID_floatValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_0_ID:
				//		LinkInput_Float_floatValues_2_ID_floatValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_1_ID:
				//		LinkInput_Float_floatValues_2_ID_floatValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_2_ID:
				//		LinkInput_Float_floatValues_2_ID_floatValues_2_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.floatValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitDistance_ID:
				//		LinkInput_RaycastV3_floatValues_0_ID_raycastHitDistance_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitDistance_ID:
				//		LinkInput_RaycastV3_floatValues_1_ID_raycastHitDistance_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_2_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitDistance_ID:
				//		LinkInput_RaycastV3_floatValues_2_ID_raycastHitDistance_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

//switch (inID)
//{
//case Enums.floatValues_0_ID:
//	switch (sourceNodeOutID)
//	{
//	case Enums.m44Value_0_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_0_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_1_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_1_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_2_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_2_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_3_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_3_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_4_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_4_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_5_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_5_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_6_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_6_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_7_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_7_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_8_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_8_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_9_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_9_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_10_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_10_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_11_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_11_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_12_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_12_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_13_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_13_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_14_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_14_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_15_ID:
//		LinkInput_M44_floatValues_0_ID_m44Value_15_ID ( sourceLogicNode);
//		break;
//
//	}
//
//	break;
//
//case Enums.floatValues_1_ID:
//	switch (sourceNodeOutID)
//	{
//	case Enums.m44Value_0_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_0_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_1_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_1_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_2_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_2_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_3_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_3_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_4_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_4_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_5_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_5_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_6_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_6_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_7_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_7_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_8_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_8_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_9_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_9_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_10_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_10_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_11_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_11_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_12_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_12_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_13_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_13_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_14_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_14_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_15_ID:
//		LinkInput_M44_floatValues_1_ID_m44Value_15_ID ( sourceLogicNode);
//		break;
//
//	}
//
//	break;
//
//case Enums.floatValues_2_ID:
//	switch (sourceNodeOutID)
//	{
//	case Enums.m44Value_0_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_0_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_1_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_1_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_2_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_2_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_3_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_3_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_4_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_4_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_5_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_5_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_6_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_6_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_7_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_7_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_8_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_8_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_9_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_9_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_10_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_10_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_11_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_11_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_12_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_12_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_13_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_13_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_14_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_14_ID ( sourceLogicNode);
//		break;
//
//	case Enums.m44Value_15_ID:
//		LinkInput_M44_floatValues_2_ID_m44Value_15_ID ( sourceLogicNode);
//		break;
//
//	}
//
//	break;
//
//}
//

				//switch (inID)
				//{
				//case Enums.floatValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.m44ValueDeterminant_ID:
				//		LinkInput_M44_floatValues_0_ID_m44ValueDeterminant_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.m44ValueDeterminant_ID:
				//		LinkInput_M44_floatValues_1_ID_m44ValueDeterminant_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_2_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.m44ValueDeterminant_ID:
				//		LinkInput_M44_floatValues_2_ID_m44ValueDeterminant_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.floatValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_distance_ID:
				//		LinkInput_navMeshHit_floatValues_0_ID_NavMeshHit_distance_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_distance_ID:
				//		LinkInput_navMeshHit_floatValues_1_ID_NavMeshHit_distance_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_2_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_distance_ID:
				//		LinkInput_navMeshHit_floatValues_2_ID_NavMeshHit_distance_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.floatValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.hit2D_distance_ID:
				//		LinkInput_hit2D_floatValues_0_ID_hit2D_distance_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.hit2D_fraction_ID:
				//		LinkInput_hit2D_floatValues_0_ID_hit2D_fraction_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.hit2D_distance_ID:
				//		LinkInput_hit2D_floatValues_1_ID_hit2D_distance_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.hit2D_fraction_ID:
				//		LinkInput_hit2D_floatValues_1_ID_hit2D_fraction_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_2_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.hit2D_distance_ID:
				//		LinkInput_hit2D_floatValues_2_ID_hit2D_distance_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.hit2D_fraction_ID:
				//		LinkInput_hit2D_floatValues_2_ID_hit2D_fraction_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}


				//switch (inID)
				//{
				//case Enums.floatValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_altitudeAngle_ID:
				//		LinkInput_touch_floatValues_0_ID_touch_altitudeAngle_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_azimuthAngle_ID:
				//		LinkInput_touch_floatValues_0_ID_touch_azimuthAngle_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_deltaTime_ID:
				//		LinkInput_touch_floatValues_0_ID_touch_deltaTime_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_maximumPossiblePressure_ID:
				//		LinkInput_touch_floatValues_0_ID_touch_maximumPossiblePressure_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_pressure_ID:
				//		LinkInput_touch_floatValues_0_ID_touch_pressure_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_radius_ID:
				//		LinkInput_touch_floatValues_0_ID_touch_radius_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_radiusVariance_ID:
				//		LinkInput_touch_floatValues_0_ID_touch_radiusVariance_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_altitudeAngle_ID:
				//		LinkInput_touch_floatValues_1_ID_touch_altitudeAngle_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_azimuthAngle_ID:
				//		LinkInput_touch_floatValues_1_ID_touch_azimuthAngle_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_deltaTime_ID:
				//		LinkInput_touch_floatValues_1_ID_touch_deltaTime_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_maximumPossiblePressure_ID:
				//		LinkInput_touch_floatValues_1_ID_touch_maximumPossiblePressure_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_pressure_ID:
				//		LinkInput_touch_floatValues_1_ID_touch_pressure_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_radius_ID:
				//		LinkInput_touch_floatValues_1_ID_touch_radius_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_radiusVariance_ID:
				//		LinkInput_touch_floatValues_1_ID_touch_radiusVariance_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.floatValues_2_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_altitudeAngle_ID:
				//		LinkInput_touch_floatValues_2_ID_touch_altitudeAngle_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_azimuthAngle_ID:
				//		LinkInput_touch_floatValues_2_ID_touch_azimuthAngle_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_deltaTime_ID:
				//		LinkInput_touch_floatValues_2_ID_touch_deltaTime_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_maximumPossiblePressure_ID:
				//		LinkInput_touch_floatValues_2_ID_touch_maximumPossiblePressure_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_pressure_ID:
				//		LinkInput_touch_floatValues_2_ID_touch_pressure_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_radius_ID:
				//		LinkInput_touch_floatValues_2_ID_touch_radius_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_radiusVariance_ID:
				//		LinkInput_touch_floatValues_2_ID_touch_radiusVariance_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				break;


			case VariableType.Int:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);

				switch (sourceNodeOutID)
				{
				case Enums.touch_fingerId_ID:
					Nv_LinkInputAction_int (inID, sourceLogicNode.touch_fingerId);
					break;

				case Enums.touch_tapCount_ID:
					Nv_LinkInputAction_int (inID, sourceLogicNode.touch_tapCount);
					break;

				case Enums.NavMeshHit_mask_ID:
					Nv_LinkInputAction_int (inID, sourceLogicNode.NavMeshHit_mask);
					break;


				case Enums.intValue_ID:
					Nv_LinkInputAction_int (inID, sourceLogicNode.intValue);
					break;

				case Enums.intValues_0_ID:
					Nv_LinkInputAction_int (inID, sourceLogicNode.intValues [0]);
					break;

				case Enums.intValues_1_ID:
					Nv_LinkInputAction_int (inID, sourceLogicNode.intValues [1]);
					break;

				case Enums.intValues_2_ID:
					Nv_LinkInputAction_int (inID, sourceLogicNode.intValues [2]);
					break;


				case Enums.raycastHitTriangleIndex_ID:
					Nv_LinkInputAction_int (inID, sourceLogicNode.raycastHitTriangleIndex);
					break;

				case Enums.intsList_ID:
					Nv_LinkInputAction_int (inID, lstIndex, sourceLogicNode);
					break;
				}
				//switch (inID)
				//{
				//case Enums.nv_int_0:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_fingerId_ID:						
				//		nv_int [0] = sourceLogicNode.touch_fingerId;
				//		break;
				//
				//	case Enums.touch_tapCount_ID:
				//		nv_int [0] = sourceLogicNode.touch_tapCount;
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.nv_int_1:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_fingerId_ID:						
				//		nv_int [1] = sourceLogicNode.touch_fingerId;
				//		break;
				//
				//	case Enums.touch_tapCount_ID:
				//		nv_int [1] = sourceLogicNode.touch_tapCount;
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.nv_int_2:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_fingerId_ID:						
				//		nv_int [2] = sourceLogicNode.touch_fingerId;
				//		break;
				//
				//	case Enums.touch_tapCount_ID:
				//		nv_int [2] = sourceLogicNode.touch_tapCount;
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}


				//switch (inID)
				//{
				//case Enums.nv_int_0:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.intsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.intsListValue.Count)
				//		{
				//			nv_int [0] = sourceLogicNode.intsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.nv_int_1:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.intsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.intsListValue.Count)
				//		{
				//			nv_int [1] = sourceLogicNode.intsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.nv_int_2:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.intsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.intsListValue.Count)
				//		{
				//			nv_int [2] = sourceLogicNode.intsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}

				//switch (inID)
				//{
				//case Enums.nv_int_0:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_mask_ID:						
				//		nv_int [0] = sourceLogicNode.NavMeshHit_mask; 
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.nv_int_1:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_mask_ID:
				//		nv_int [1] = sourceLogicNode.NavMeshHit_mask; 
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.nv_int_2:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_mask_ID:
				//		nv_int [2] = sourceLogicNode.NavMeshHit_mask; 
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}


				//switch (inID)
				//{
				//case Enums.intValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.intValue_ID:
				//		LinkInput_Int_intValues_0_ID_intValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.intValues_0_ID:
				//		LinkInput_Int_intValues_0_ID_intValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.intValues_1_ID:
				//		LinkInput_Int_intValues_0_ID_intValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.intValues_2_ID:
				//		LinkInput_Int_intValues_0_ID_intValues_2_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.intValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.intValue_ID:
				//		LinkInput_Int_intValues_1_ID_intValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.intValues_0_ID:
				//		LinkInput_Int_intValues_1_ID_intValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.intValues_1_ID:
				//		LinkInput_Int_intValues_1_ID_intValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.intValues_2_ID:
				//		LinkInput_Int_intValues_1_ID_intValues_2_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.intValues_2_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.intValue_ID:
				//		LinkInput_Int_intValues_2_ID_intValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.intValues_0_ID:
				//		LinkInput_Int_intValues_2_ID_intValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.intValues_1_ID:
				//		LinkInput_Int_intValues_2_ID_intValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.intValues_2_ID:
				//		LinkInput_Int_intValues_2_ID_intValues_2_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.intValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitTriangleIndex_ID:
				//		LinkInput_RaycastV3_intValues_0_ID_raycastHitTriangleIndex_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.intValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitTriangleIndex_ID:
				//		LinkInput_RaycastV3_intValues_1_ID_raycastHitTriangleIndex_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.intValues_2_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitTriangleIndex_ID:
				//		LinkInput_RaycastV3_intValues_2_ID_raycastHitTriangleIndex_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}
				//break;

				break;


			case VariableType.String:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);

				switch (sourceNodeOutID)
				{
				case Enums.stringsList_ID:
					Nv_LinkInputAction_String (inID, lstIndex, sourceLogicNode);
					break;


				case Enums.touch_phase_ID:
					Nv_LinkInputAction_String (inID, sourceLogicNode.touch_phase);
					break;

				case Enums.touch_type_ID:
					Nv_LinkInputAction_String (inID, sourceLogicNode.touch_type);
					break;


				case Enums.stringValue_ID:
					Nv_LinkInputAction_String (inID, sourceLogicNode.stringValue);
					break;

				case Enums.stringValues_0_ID:
					Nv_LinkInputAction_String (inID, sourceLogicNode.stringValues [0]);
					break;

				case Enums.stringValues_1_ID:
					Nv_LinkInputAction_String (inID, sourceLogicNode.stringValues [1]);
					break;
				}


				//switch (inID)
				//{
				//case Enums.stringValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_phase_ID:
				//		LinkInput_touch_stringValues_0_ID_touch_phase_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_type_ID:
				//		LinkInput_touch_stringValues_0_ID_touch_type_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.stringValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_phase_ID:
				//		LinkInput_touch_stringValues_1_ID_touch_phase_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_type_ID:
				//		LinkInput_touch_stringValues_1_ID_touch_type_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}



				//switch (inID)
				//{
				//case Enums.stringValues_0_ID:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.stringsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.stringsListValue.Count)
				//		{
				//			stringValues [0] = sourceLogicNode.stringsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.stringValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.stringsList_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.stringsListValue.Count)
				//		{
				//			stringValues [1] = sourceLogicNode.stringsListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}


				//switch (inID)
				//{
				//case Enums.stringValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.stringValue_ID:
				//		LinkInput_String_stringValues_0_ID_stringValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.stringValues_0_ID:
				//		LinkInput_String_stringValues_0_ID_stringValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.stringValues_1_ID:
				//		LinkInput_String_stringValues_0_ID_stringValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.stringValues_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.stringValue_ID:
				//		LinkInput_String_stringValues_1_ID_stringValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.stringValues_0_ID:
				//		LinkInput_String_stringValues_1_ID_stringValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.stringValues_1_ID:
				//		LinkInput_String_stringValues_1_ID_stringValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				break;


			case VariableType.Vector2:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);

				switch (sourceNodeOutID)
				{
				case Enums.vector2List_ID:
					Nv_LinkInputAction_Vector2 (inID, lstIndex, sourceLogicNode);
					break;


				case Enums.vector2Value_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.vector2Value);
					break;

				case Enums.vector2Values_0_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.vector2Values [0]);
					break;

				case Enums.vector2Values_1_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.vector2Values [1]);
					break;


				case Enums.ray2DOrigin_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.ray2DValueOrigin);
					break;

				case Enums.ray2DDirection_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.ray2DDirectionValueNotNormalized);
					break;


				case Enums.raycastHitLightmapCoord_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.raycastHitLightmapCoord);
					break;

				case Enums.raycastHitTextureCoord_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.raycastHittextureCoord);
					break;

				case Enums.raycastHitTextureCoord2_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.raycastHittextureCoord2);
					break;


				case Enums.hit2D_centroid_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.hit2D_centroid);
					break;

				case Enums.hit2D_normal_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.hit2D_normal);
					break;

				case Enums.hit2D_point_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.hit2D_point);
					break;


				case Enums.touch_position_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.touch_position);
					break;

				case Enums.touch_deltaPosition_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.touch_deltaPosition);
					break;

				case Enums.touch_rawPosition_ID:
					Nv_LinkInputAction_Vector2 (inID, sourceLogicNode.touch_rawPosition);
					break;
				}



				//switch (inID)
				//{
				//case Enums.vector2Values_0_ID:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector2List_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.vector2ListValue.Count)
				//		{
				//			vector2Values [0] = sourceLogicNode.vector2ListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.vector2Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector2List_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.vector2ListValue.Count)
				//		{
				//			vector2Values [1] = sourceLogicNode.vector2ListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}

				//switch (inID)
				//{
				//case Enums.floatValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.floatValue_ID:
				//		LinkInput_Vector2_floatValues_0_ID_floatValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_0_ID:
				//		LinkInput_Vector2_floatValues_0_ID_floatValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_1_ID:
				//		LinkInput_Vector2_floatValues_0_ID_floatValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_2_ID:
				//		LinkInput_Vector2_floatValues_0_ID_floatValues_2_ID ( sourceLogicNode);
				//		break;
				//	}
				//
				//	break;
				//
				//case Enums.vector2Values_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector2Value_ID:
				//		LinkInput_Vector2_vector2Values_0_ID_vector2Value_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector2Values_0_ID:
				//		LinkInput_Vector2_vector2Values_0_ID_vector2Values_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector2Values_1_ID:
				//		LinkInput_Vector2_vector2Values_0_ID_vector2Values_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.ray2DOrigin_ID:
				//		LinkInput_Vector2_vector2Values_0_ID_ray2DOrigin_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.ray2DDirection_ID:
				//		LinkInput_Vector2_vector2Values_0_ID_ray2DDirection_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.vector2Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector2Value_ID:
				//		LinkInput_Vector2_vector2Values_1_ID_vector2Value_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector2Values_0_ID:
				//		LinkInput_Vector2_vector2Values_1_ID_vector2Values_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector2Values_1_ID:
				//		LinkInput_Vector2_vector2Values_1_ID_vector2Values_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.ray2DOrigin_ID:
				//		LinkInput_Vector2_vector2Values_1_ID_ray2DOrigin_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.ray2DDirection_ID:
				//		LinkInput_Vector2_vector2Values_1_ID_ray2DDirection_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.ray2DOrigin_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector2Value_ID:
				//		LinkInput_Ray2D_ray2DOrigin_ID_vector2Value_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector2Values_0_ID:
				//		LinkInput_Ray2D_ray2DOrigin_ID_vector2Values_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector2Values_1_ID:
				//		LinkInput_Ray2D_ray2DOrigin_ID_vector2Values_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.ray2DOrigin_ID:
				//		LinkInput_Ray2D_ray2DOrigin_ID_ray2DOrigin_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.ray2DDirection_ID:
				//		LinkInput_Ray2D_ray2DOrigin_ID_ray2DDirection_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.ray2DDirection_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector2Value_ID:
				//		LinkInput_Ray2D_ray2DDirection_ID_vector2Value_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector2Values_0_ID:
				//		LinkInput_Ray2D_ray2DDirection_ID_vector2Values_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector2Values_1_ID:
				//		LinkInput_Ray2D_ray2DDirection_ID_vector2Values_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.ray2DOrigin_ID:
				//		LinkInput_Ray2D_ray2DDirection_ID_ray2DOrigin_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.ray2DDirection_ID:
				//		LinkInput_Ray2D_ray2DDirection_ID_ray2DDirection_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.vector2Values_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitLightmapCoord_ID:
				//		LinkInput_RaycastV3_vector2Values_0_ID_raycastHitLightmapCoord_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitTextureCoord_ID:
				//		LinkInput_RaycastV3_vector2Values_0_ID_raycastHitTextureCoord_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitTextureCoord2_ID:
				//		LinkInput_RaycastV3_vector2Values_0_ID_raycastHitTextureCoord2_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.vector2Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitLightmapCoord_ID:
				//		LinkInput_RaycastV3_vector2Values_1_ID_raycastHitLightmapCoord_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitTextureCoord_ID:
				//		LinkInput_RaycastV3_vector2Values_1_ID_raycastHitTextureCoord_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitTextureCoord2_ID:
				//		LinkInput_RaycastV3_vector2Values_1_ID_raycastHitTextureCoord2_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.ray2DOrigin_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitLightmapCoord_ID:
				//		LinkInput_RaycastV3_ray2DOrigin_ID_raycastHitLightmapCoord_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitTextureCoord_ID:
				//		LinkInput_RaycastV3_ray2DOrigin_ID_raycastHitTextureCoord_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitTextureCoord2_ID:
				//		LinkInput_RaycastV3_ray2DOrigin_ID_raycastHitTextureCoord2_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.ray2DDirection_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitLightmapCoord_ID:
				//		LinkInput_RaycastV3_ray2DDirection_ID_raycastHitLightmapCoord_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitTextureCoord_ID:
				//		LinkInput_RaycastV3_ray2DDirection_ID_raycastHitTextureCoord_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitTextureCoord2_ID:
				//		LinkInput_RaycastV3_ray2DDirection_ID_raycastHitTextureCoord2_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.vector2Values_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.hit2D_centroid_ID:
				//		LinkInput_hit2D_vector2Values_0_ID_hit2D_centroid_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.hit2D_normal_ID:
				//		LinkInput_hit2D_vector2Values_0_ID_hit2D_normal_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.hit2D_point_ID:
				//		LinkInput_hit2D_vector2Values_0_ID_hit2D_point_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.vector2Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.hit2D_centroid_ID:
				//		LinkInput_hit2D_vector2Values_1_ID_hit2D_centroid_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.hit2D_normal_ID:
				//		LinkInput_hit2D_vector2Values_1_ID_hit2D_normal_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.hit2D_point_ID:
				//		LinkInput_hit2D_vector2Values_1_ID_hit2D_point_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.vector2Values_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_position_ID:
				//		LinkInput_touch_vector2Values_0_ID_touch_position_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_deltaPosition_ID:
				//		LinkInput_touch_vector2Values_0_ID_touch_deltaPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_rawPosition_ID:
				//		LinkInput_touch_vector2Values_0_ID_touch_rawPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.vector2Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_position_ID:
				//		LinkInput_touch_vector2Values_1_ID_touch_position_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_deltaPosition_ID:
				//		LinkInput_touch_vector2Values_1_ID_touch_deltaPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_rawPosition_ID:
				//		LinkInput_touch_vector2Values_1_ID_touch_rawPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.ray2DOrigin_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_position_ID:
				//		LinkInput_touch_ray2DOrigin_ID_touch_position_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_deltaPosition_ID:
				//		LinkInput_touch_ray2DOrigin_ID_touch_deltaPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_rawPosition_ID:
				//		LinkInput_touch_ray2DOrigin_ID_touch_rawPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.ray2DDirection_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.touch_position_ID:
				//		LinkInput_touch_ray2DDirection_ID_touch_position_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_deltaPosition_ID:
				//		LinkInput_touch_ray2DDirection_ID_touch_deltaPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.touch_rawPosition_ID:
				//		LinkInput_touch_ray2DDirection_ID_touch_rawPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				break;



			case VariableType.Vector3:
				lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources [Nv_IndexOfInID (inID)]);

				switch (sourceNodeOutID)
				{
				case Enums.vector3List_ID:
					Nv_LinkInputAction_Vector3 (inID, lstIndex, sourceLogicNode);
					break;


				case Enums.NavMeshHit_normal_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.NavMeshHit_normal);
					break;

				case Enums.NavMeshHit_position_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.NavMeshHit_position);
					break;


				case Enums.vector3Value_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.vector3Value);
					break;

				case Enums.vector3Values_0_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.vector3Values [0]);
					break;

				case Enums.vector3Values_1_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.vector3Values [1]);
					break;

				case Enums.rayOrigin_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.rayValueOrigin);
					break;

				case Enums.rayDirection_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.rayDirectionValueNotNormalized);
					break;


				case Enums.boundsCenterValue_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.boundsCenterValue);
					break;

				case Enums.boundsExtentsValue_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.boundsExtentsValue);
					break;

				case Enums.boundsMaxValue_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.boundsMaxValue);
					break;

				case Enums.boundsMinValue_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.boundsMinValue);
					break;

				case Enums.boundsSizeValue_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.boundsSizeValue);
					break;


				case Enums.raycastHitBarycentricCoordinate_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.raycastHitBarycentricCoordinate);
					break;

				case Enums.raycastHitNormal_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.raycastHitNormal);
					break;

				case Enums.raycastHitPoint_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.raycastHitPoint);
					break;


				case Enums.OffMeshLinkData_startPosition_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.OffMeshLinkData_startPosition);
					break;

				case Enums.OffMeshLinkData_endPosition_ID:
					Nv_LinkInputAction_Vector3 (inID, sourceLogicNode.OffMeshLinkData_endPosition);
					break;
				}


				//switch (inID)
				//{
				//case Enums.vector3Values_0_ID:		
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector3List_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.vector3ListValue.Count)
				//		{
				//			vector3Values [0] = sourceLogicNode.vector3ListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//
				//case Enums.vector3Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector3List_ID:
				//		if (lstIndex >-1 && lstIndex < sourceLogicNode.vector3ListValue.Count)
				//		{
				//			vector3Values [1] = sourceLogicNode.vector3ListValue [lstIndex];
				//		}
				//		break;
				//	}
				//	break;
				//}

				//switch (inID)
				//{
				//case Enums.vector3Values_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_normal_ID:
				//		LinkInput_navMeshHit_vector3Values_0_ID_NavMeshHit_normal_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.NavMeshHit_position_ID:
				//		LinkInput_navMeshHit_vector3Values_0_ID_NavMeshHit_position_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.vector3Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_normal_ID:
				//		LinkInput_navMeshHit_vector3Values_1_ID_NavMeshHit_normal_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.NavMeshHit_position_ID:
				//		LinkInput_navMeshHit_vector3Values_1_ID_NavMeshHit_position_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.rayOrigin_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_normal_ID:
				//		LinkInput_navMeshHit_rayOrigin_ID_NavMeshHit_normal_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.NavMeshHit_position_ID:
				//		LinkInput_navMeshHit_rayOrigin_ID_NavMeshHit_position_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.rayDirection_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.NavMeshHit_normal_ID:
				//		LinkInput_navMeshHit_rayDirection_ID_NavMeshHit_normal_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.NavMeshHit_position_ID:
				//		LinkInput_navMeshHit_rayDirection_ID_NavMeshHit_position_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				//switch (inID)
				//{
				//case Enums.floatValues_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.floatValue_ID:
				//		LinkInput_Vector3_floatValues_0_ID_floatValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_0_ID:
				//		LinkInput_Vector3_floatValues_0_ID_floatValues_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_1_ID:
				//		LinkInput_Vector3_floatValues_0_ID_floatValues_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.floatValues_2_ID:
				//		LinkInput_Vector3_floatValues_0_ID_floatValues_2_ID ( sourceLogicNode);
				//		break;
				//	}
				//
				//	break;
				//
				//case Enums.vector3Values_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//
				//	case Enums.vector3Value_ID:
				//		LinkInput_Vector3_vector3Values_0_ID_vector3Value_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector3Values_0_ID:
				//		LinkInput_Vector3_vector3Values_0_ID_vector3Values_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector3Values_1_ID:
				//		LinkInput_Vector3_vector3Values_0_ID_vector3Values_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rayOrigin_ID:
				//		LinkInput_Vector3_vector3Values_0_ID_rayOrigin_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rayDirection_ID:
				//		LinkInput_Vector3_vector3Values_0_ID_rayDirection_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.vector3Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//
				//	case Enums.vector3Value_ID:
				//		LinkInput_Vector3_vector3Values_1_ID_vector3Value_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector3Values_0_ID:
				//		LinkInput_Vector3_vector3Values_1_ID_vector3Values_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector3Values_1_ID:
				//		LinkInput_Vector3_vector3Values_1_ID_vector3Values_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rayOrigin_ID:
				//		LinkInput_Vector3_vector3Values_1_ID_rayOrigin_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rayDirection_ID:
				//		LinkInput_Vector3_vector3Values_1_ID_rayDirection_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}


				//switch (inID)
				//{
				//case Enums.rayOrigin_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector3Value_ID:
				//		LinkInput_Ray_rayOrigin_ID_vector3Value_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector3Values_0_ID:
				//		LinkInput_Ray_rayOrigin_ID_vector3Values_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector3Values_1_ID:
				//		LinkInput_Ray_rayOrigin_ID_vector3Values_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rayOrigin_ID:
				//		LinkInput_Ray_rayOrigin_ID_rayOrigin_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rayDirection_ID:
				//		LinkInput_Ray_rayOrigin_ID_rayDirection_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.rayDirection_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.vector3Value_ID:
				//		LinkInput_Ray_rayDirection_ID_vector3Value_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector3Values_0_ID:
				//		LinkInput_Ray_rayDirection_ID_vector3Values_0_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.vector3Values_1_ID:
				//		LinkInput_Ray_rayDirection_ID_vector3Values_1_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rayOrigin_ID:
				//		LinkInput_Ray_rayDirection_ID_rayOrigin_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.rayDirection_ID:
				//		LinkInput_Ray_rayDirection_ID_rayDirection_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}


				//switch (inID)
				//{
				//case Enums.vector3Values_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.boundsCenterValue_ID:
				//		LinkInput_GameObject_vector3Values_0_ID_boundsCenterValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsExtentsValue_ID:
				//		LinkInput_GameObject_vector3Values_0_ID_boundsExtentsValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsMaxValue_ID:
				//		LinkInput_GameObject_vector3Values_0_ID_boundsMaxValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsMinValue_ID:
				//		LinkInput_GameObject_vector3Values_0_ID_boundsMinValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsSizeValue_ID:
				//		LinkInput_GameObject_vector3Values_0_ID_boundsSizeValue_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.vector3Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.boundsCenterValue_ID:
				//		LinkInput_GameObject_vector3Values_1_ID_boundsCenterValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsExtentsValue_ID:
				//		LinkInput_GameObject_vector3Values_1_ID_boundsExtentsValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsMaxValue_ID:
				//		LinkInput_GameObject_vector3Values_1_ID_boundsMaxValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsMinValue_ID:
				//		LinkInput_GameObject_vector3Values_1_ID_boundsMinValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsSizeValue_ID:
				//		LinkInput_GameObject_vector3Values_1_ID_boundsSizeValue_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.rayOrigin_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.boundsCenterValue_ID:
				//		LinkInput_GameObject_rayOrigin_ID_boundsCenterValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsExtentsValue_ID:
				//		LinkInput_GameObject_rayOrigin_ID_boundsExtentsValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsMaxValue_ID:
				//		LinkInput_GameObject_rayOrigin_ID_boundsMaxValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsMinValue_ID:
				//		LinkInput_GameObject_rayOrigin_ID_boundsMinValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsSizeValue_ID:
				//		LinkInput_GameObject_rayOrigin_ID_boundsSizeValue_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.rayDirection_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.boundsCenterValue_ID:
				//		LinkInput_GameObject_rayDirection_ID_boundsCenterValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsExtentsValue_ID:
				//		LinkInput_GameObject_rayDirection_ID_boundsExtentsValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsMaxValue_ID:
				//		LinkInput_GameObject_rayDirection_ID_boundsMaxValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsMinValue_ID:
				//		LinkInput_GameObject_rayDirection_ID_boundsMinValue_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.boundsSizeValue_ID:
				//		LinkInput_GameObject_rayDirection_ID_boundsSizeValue_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}


				//switch (inID)
				//{
				//case Enums.vector3Values_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitBarycentricCoordinate_ID:
				//		LinkInput_RaycastV3_vector3Values_0_ID_raycastHitBarycentricCoordinate_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitNormal_ID:
				//		LinkInput_RaycastV3_vector3Values_0_ID_raycastHitNormal_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitPoint_ID:
				//		LinkInput_RaycastV3_vector3Values_0_ID_raycastHitPoint_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.vector3Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitBarycentricCoordinate_ID:
				//		LinkInput_RaycastV3_vector3Values_1_ID_raycastHitBarycentricCoordinate_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitNormal_ID:
				//		LinkInput_RaycastV3_vector3Values_1_ID_raycastHitNormal_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitPoint_ID:
				//		LinkInput_RaycastV3_vector3Values_1_ID_raycastHitPoint_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.rayOrigin_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitBarycentricCoordinate_ID:
				//		LinkInput_RaycastV3_rayOrigin_ID_raycastHitBarycentricCoordinate_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitNormal_ID:
				//		LinkInput_RaycastV3_rayOrigin_ID_raycastHitNormal_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitPoint_ID:
				//		LinkInput_RaycastV3_rayOrigin_ID_raycastHitPoint_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.rayDirection_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.raycastHitBarycentricCoordinate_ID:
				//		LinkInput_RaycastV3_rayDirection_ID_raycastHitBarycentricCoordinate_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitNormal_ID:
				//		LinkInput_RaycastV3_rayDirection_ID_raycastHitNormal_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.raycastHitPoint_ID:
				//		LinkInput_RaycastV3_rayDirection_ID_raycastHitPoint_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}


				//switch (inID)
				//{
				//case Enums.vector3Values_0_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.OffMeshLinkData_startPosition_ID:
				//		LinkInput_OffMeshLink_vector3Values_0_ID_OffMeshLinkData_startPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.OffMeshLinkData_endPosition_ID:
				//		LinkInput_OffMeshLink_vector3Values_0_ID_OffMeshLinkData_endPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.vector3Values_1_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.OffMeshLinkData_startPosition_ID:
				//		LinkInput_OffMeshLink_vector3Values_1_ID_OffMeshLinkData_startPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.OffMeshLinkData_endPosition_ID:
				//		LinkInput_OffMeshLink_vector3Values_1_ID_OffMeshLinkData_endPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.rayOrigin_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.OffMeshLinkData_startPosition_ID:
				//		LinkInput_OffMeshLink_rayOrigin_ID_OffMeshLinkData_startPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.OffMeshLinkData_endPosition_ID:
				//		LinkInput_OffMeshLink_rayOrigin_ID_OffMeshLinkData_endPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//case Enums.rayDirection_ID:
				//	switch (sourceNodeOutID)
				//	{
				//	case Enums.OffMeshLinkData_startPosition_ID:
				//		LinkInput_OffMeshLink_rayDirection_ID_OffMeshLinkData_startPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	case Enums.OffMeshLinkData_endPosition_ID:
				//		LinkInput_OffMeshLink_rayDirection_ID_OffMeshLinkData_endPosition_ID ( sourceLogicNode);
				//		break;
				//
				//	}
				//
				//	break;
				//
				//}

				break;
			}
		}

		void Nv_LinkInputAction_String (string inID, string StringToSet)
		{
			for (int i = 0; i < nv_String_IDs.Length; i++)
			{
				if (inID == nv_String_IDs [i])
				{
					nv_String [i] = StringToSet;
				}
			}
		}
		void Nv_LinkInputAction_String (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_String_IDs.Length; i++)
			{
				if (inID == nv_String_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.stringsListValue.Count)
					{
						nv_String [i] = sourceLogicNode.stringsListValue [lstIndex];
					}
				}
			}
		}


		void Nv_LinkInputAction_float (string inID, float valToSet)
		{
			for (int i = 0; i < nv_float_IDs.Length; i++)
			{
				if (inID == nv_float_IDs [i])
				{
					nv_float [i] = valToSet;
				}
			}
		}
		void Nv_LinkInputAction_float (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_float_IDs.Length; i++)
			{
				if (inID == nv_float_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.floatsListValue.Count)
					{
						nv_float [i] = sourceLogicNode.floatsListValue [lstIndex];
					}
				}
			}
		}

		void Nv_LinkInputAction_int (string inID, int intToSet)
		{
			for (int i = 0; i < nv_int_IDs.Length; i++)
			{
				if (inID == nv_int_IDs [i])
				{
					nv_int [i] = intToSet;
				}
			}
		}
		void Nv_LinkInputAction_int (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_int_IDs.Length; i++)
			{
				if (inID == nv_int_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.intsListValue.Count)
					{
						nv_int [i] = sourceLogicNode.intsListValue [lstIndex];
					}
				}
			}
		}

		void Nv_LinkInputAction_Vector2 (string inID, Vector2 Vector2ToSet)
		{
			for (int i = 0; i < nv_Vector2_IDs.Length; i++)
			{
				if (inID == nv_Vector2_IDs [i])
				{
					nv_Vector2 [i] = Vector2ToSet;
				}
			}
		}
		void Nv_LinkInputAction_Vector2 (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_Vector2_IDs.Length; i++)
			{
				if (inID == nv_Vector2_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.vector2ListValue.Count)
					{
						nv_Vector2 [i] = sourceLogicNode.vector2ListValue [lstIndex];
					}
				}
			}

		}

		void Nv_LinkInputAction_Vector3 (string inID, Vector3 Vector3ToSet)
		{
			for (int i = 0; i < nv_Vector3_IDs.Length; i++)
			{
				if (inID == nv_Vector3_IDs [i])
				{
					nv_Vector3 [i] = Vector3ToSet;
				}
			}
		}
		void Nv_LinkInputAction_Vector3 (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_Vector3_IDs.Length; i++)
			{
				if (inID == nv_Vector3_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.vector3ListValue.Count)
					{
						nv_Vector3 [i] = sourceLogicNode.vector3ListValue [lstIndex];
					}
				}
			}

		}

		void Nv_LinkInputAction_Vector4 (string inID, Vector4 Vector4ToSet)
		{
			for (int i = 0; i < nv_Vector4_IDs.Length; i++)
			{
				if (inID == nv_Vector4_IDs [i])
				{
					nv_Vector4 [i] = Vector4ToSet;
				}
			}
		}
		void Nv_LinkInputAction_Vector4 (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_Vector4_IDs.Length; i++)
			{
				if (inID == nv_Vector4_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.vector4ListValue.Count)
					{
						nv_Vector4 [i] = sourceLogicNode.vector4ListValue [lstIndex];
					}
				}
			}

		}

		void Nv_LinkInputAction_Rect (string inID, Rect RectToSet)
		{
			for (int i = 0; i < nv_Rect_IDs.Length; i++)
			{
				if (inID == nv_Rect_IDs [i])
				{
					nv_Rect [i] = RectToSet;
				}
			}
		}
		void Nv_LinkInputAction_Rect (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_Rect_IDs.Length; i++)
			{
				if (inID == nv_Rect_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.rectListValue.Count)
					{
						nv_Rect [i] = sourceLogicNode.rectListValue [lstIndex];
					}
				}
			}

		}

		void Nv_LinkInputAction_Color (string inID, Color ColorToSet)
		{
			for (int i = 0; i < nv_Color_IDs.Length; i++)
			{
				if (inID == nv_Color_IDs [i])
				{
					nv_Color [i] = ColorToSet;
				}
			}
		}
		void Nv_LinkInputAction_Color (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_Color_IDs.Length; i++)
			{
				if (inID == nv_Color_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.colorsListValue.Count)
					{
						nv_Color [i] = sourceLogicNode.colorsListValue [lstIndex];
					}
				}
			}

		}

		void Nv_LinkInputAction_Bool (string inID, bool BoolToSet)
		{
			for (int i = 0; i < nv_Bool_IDs.Length; i++)
			{
				if (inID == nv_Bool_IDs [i])
				{
					nv_Bool [i] = BoolToSet;
				}
			}
		}
		void Nv_LinkInputAction_Bool (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_Bool_IDs.Length; i++)
			{
				if (inID == nv_Bool_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.boolsListValue.Count)
					{
						nv_Bool [i] = sourceLogicNode.boolsListValue [lstIndex];
					}
				}
			}

		}

		void Nv_LinkInputAction_Texture2D (string inID, Texture2D Texture2DToSet)
		{
			for (int i = 0; i < nv_Texture2D_IDs.Length; i++)
			{
				if (inID == nv_Texture2D_IDs [i])
				{
					nv_Texture2D [i] = Texture2DToSet;
				}
			}
		}
		void Nv_LinkInputAction_Texture2D (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_Texture2D_IDs.Length; i++)
			{
				if (inID == nv_Texture2D_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.texture2DListValue.Count)
					{
						nv_Texture2D [i] = sourceLogicNode.texture2DListValue [lstIndex];
					}
				}
			}

		}

		void Nv_LinkInputAction_Material (string inID, Material MaterialToSet)
		{
			for (int i = 0; i < nv_Material_IDs.Length; i++)
			{
				if (inID == nv_Material_IDs [i])
				{
					nv_Material [i] = MaterialToSet;
				}
			}
		}
		void Nv_LinkInputAction_Material (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_Material_IDs.Length; i++)
			{
				if (inID == nv_Material_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.materialsListValue.Count)
					{
						nv_Material [i] = sourceLogicNode.materialsListValue [lstIndex];
					}
				}
			}

		}

		void Nv_LinkInputAction_Shader (string inID, Shader ShaderToSet)
		{
			for (int i = 0; i < nv_Shader_IDs.Length; i++)
			{
				if (inID == nv_Shader_IDs [i])
				{
					nv_Shader [i] = ShaderToSet;
				}
			}
		}
		void Nv_LinkInputAction_Shader (string inID, int lstIndex, LogicNode sourceLogicNode)
		{
			for (int i = 0; i < nv_Shader_IDs.Length; i++)
			{
				if (inID == nv_Shader_IDs [i])
				{
					if (lstIndex >-1 && lstIndex < sourceLogicNode.shaderListValue.Count)
					{
						nv_Shader [i] = sourceLogicNode.shaderListValue [lstIndex];
					}
				}
			}

		}




		void Nv_AssignInputSource (Rect inOutRect, string inOutID)
		{
			if (string.IsNullOrEmpty (logic.inOutAdressCurrentToLink))
				return;
			
			string facingLogicNodeUniqueID = InOutAdressCurrentToLinkToUniqueID ();

			//LogicNode facingLogicNode = GetLogicNodeOnUniqueID (facingLogicNodeUniqueID);

			if (facingLogicNodeUniqueID == uniqueID)
				return;

			if (! Nv_IsInOutTypeSameWeak (InOutAdressCurrentToLinkToInOutID (-1), inOutID))
			{
				if ( ! Nv_IsInOutTypeSame (InOutAdressCurrentToLinkToInOutID (), inOutID))
					return;
			}

			if (InOutAdressCurrentToLinkToInOutDirection () == outAdressSignature)
			{
				if (inOutRect.Contains (eGlobal.mousePosition) ||
					inOutRect.Contains (logic.linkExtremity))
				{
					Nv_AssignInputSource (inOutID);
				}
			}

		}
		void Nv_AssignInputSource (string inOutID)
		{
			for (int i = 0; i < nv_inputsIDs.Length; i++)
			{
				if (inOutID == nv_inputsIDs [i])
				{
					nv_inputsSources [i] = logic.inOutAdressCurrentToLink;

					if (IsAListOutSource (logic.inOutAdressCurrentToLink))
					{
						SetOutputLinkedIndexToDestination ();
					}
					break;
				}
			}
		}


		bool Nv_IsInOutTypeSame (string outID, string inID)
		{
			bool retVal = false;


			VariableType varType_out = VariableType.Bool;

			VariableType varType_in = VariableType.Color;


			bool loopAllOuts = false;

			for (int i = 0; i < outputsIDs.Length; i++)
			{
				if (outputsIDs [i] == outID)
				{
					varType_out = outputsTypes [i];

					break;
				}

				if (i == outputsIDs.Length - 1)
					loopAllOuts = true;
			}


			bool loopAllInts = false;

			for (int i = 0; i < nv_inputsIDs.Length; i++)
			{
				if (nv_inputsIDs [i] == inID)
				{
					varType_in = nv_inputsTypes [i];

					break;
				}

				if (i == nv_inputsIDs.Length - 1)
					loopAllInts = true;
			}


			if (loopAllInts || loopAllOuts)
				return false;



			if (varType_in == varType_out)
				retVal = true;

			return retVal;
		}

		bool Nv_IsInOutTypeSameWeak (string outID, string inID)
		{
			bool retVal = false;


			VariableType varType_out = VariableType.Bool;

			VariableType varType_in = VariableType.Color;


			bool loopAllOuts = false;

			for (int i = 0; i < outputsIDs.Length; i++)
			{
				if (outputsIDs [i] == outID)
				{
					varType_out = outputsTypes [i];

					break;
				}

				if (i == outputsIDs.Length - 1)
					loopAllOuts = true;
			}


			bool loopAllInts = false;

			for (int i = 0; i < nv_inputsIDs.Length; i++)
			{
				if (nv_inputsIDs [i] == inID)
				{
					varType_in = nv_inputsTypes [i];

					break;
				}

				if (i == nv_inputsIDs.Length - 1)
					loopAllInts = true;
			}


			if (loopAllInts || loopAllOuts)
				return false;


			if (varType_in == varType_out)
				return false;




			switch (varType_out)
			{
			case VariableType.boolsList:
				retVal = WeakInOutPermission (
					varType_out, VariableType.boolsList, 
					outID, Enums.boolsList_ID, 
					varType_in, VariableType.Bool);
				break;

			case VariableType.colorsList:
				retVal = WeakInOutPermission (
					varType_out, VariableType.colorsList, 
					outID, Enums.colorsList_ID, 
					varType_in, VariableType.Color);
				break;

			case VariableType.floatsList:
				retVal = WeakInOutPermission (
					varType_out, VariableType.floatsList, 
					outID, Enums.floatsList_ID, 
					varType_in, VariableType.Float);
				break;

			case VariableType.GameObjectList:
				retVal = WeakInOutPermission (
					varType_out, VariableType.GameObjectList, 
					outID, Enums.gameObjectsList_ID, 
					varType_in, VariableType.GameObject);
				break;

			case VariableType.intsList:
				retVal = WeakInOutPermission (
					varType_out, VariableType.intsList, 
					outID, Enums.intsList_ID, 
					varType_in, VariableType.Int);
				break;

			case VariableType.materialsList:
				retVal = WeakInOutPermission (
					varType_out, VariableType.materialsList, 
					outID, Enums.materialList_ID, 
					varType_in, VariableType.Material);
				break;

			case VariableType.rectsList:
				retVal = WeakInOutPermission (
					varType_out, VariableType.rectsList, 
					outID, Enums.rectList_ID, 
					varType_in, VariableType.rect);
				break;

			case VariableType.shadersList:
				retVal = WeakInOutPermission (
					varType_out, VariableType.shadersList, 
					outID, Enums.shaderList_ID, 
					varType_in, VariableType.Shader);
				break;

			case VariableType.stringsList:
				retVal = WeakInOutPermission (
					varType_out, VariableType.stringsList, 
					outID, Enums.stringsList_ID, 
					varType_in, VariableType.String);
				break;

			case VariableType.texture2DList:
				retVal = WeakInOutPermission (
					varType_out, VariableType.texture2DList, 
					outID, Enums.texture2DList_ID, 
					varType_in, VariableType.Texture2D);
				break;

			case VariableType.vector2List:
				retVal = WeakInOutPermission (
					varType_out, VariableType.vector2List, 
					outID, Enums.vector2List_ID, 
					varType_in, VariableType.Vector2);
				break;

			case VariableType.vector3List:
				retVal = WeakInOutPermission (
					varType_out, VariableType.vector3List, 
					outID, Enums.vector3List_ID, 
					varType_in, VariableType.Vector3);
				break;

			case VariableType.vector4List:
				retVal = WeakInOutPermission (
					varType_out, VariableType.vector4List, 
					outID, Enums.vector4List_ID, 
					varType_in, VariableType.Vector4);
				break;
			}

			return retVal;
		}

		void Nv_DrawCurrentLink (Rect inOutRect, string inOutID)
		{
			if ( ! maximized)
				return;

			if ( ! string.IsNullOrEmpty (logic.inOutAdressCurrentToLink))
			{
				if (eGlobal.button == 0)
				{
					if (eGlobal.type == EventType.MouseDown)
					{
						logic.inOutAdressCurrentToLink = "";
					}
				}
			}


			if (Nv_IndexOfInID (inOutID) > -1 && Nv_IndexOfInID (inOutID) 
				< nv_publicInputs.Length)
			if (nv_publicInputs [Nv_IndexOfInID (inOutID)])
				return;


			if (inOutRect.Contains (eGlobal.mousePosition))
			{
				if (eGlobal.button == 0)
				{
					if (eGlobal.type == EventType.MouseDown)
					{
						logic.inOutAdressCurrentToLink = 
							uniqueID + 
							inOutAdressSeparator + inOutID + inOutAdressSeparator +
							inAdressSignature;

						eGlobal.type = EventType.Ignore;
					}
				}
			}



			if ( ! string.IsNullOrEmpty (logic.inOutAdressCurrentToLink))
			{
				if (Nv_IsThisInIDActive (InOutAdressCurrentToLinkToInOutID ()))
				{
					Vector2 linkExtrend = eGlobal.mousePosition;

					if (outIndexForHotKeyLinking != -1 
						|| inputIndexForHotKeyLinking !=-1)
					{
						linkExtrend = logic.linkExtremity;
					}

					DrawLink (Nv_InOutRectOfThisInOutID (
						InOutAdressCurrentToLinkToInOutID ()).center, 
						linkExtrend, true, true);
				}
			}
		}

		Rect Nv_InOutRectOfThisInOutID (string inOutID)
		{
			Rect retVal = new Rect ();

			if (string.IsNullOrEmpty (inOutID))
				return new Rect ();

			for (int i = 0; i < nv_inputsRects.Length; i++)
			{
				if (nv_inputsIDs [i] == inOutID)
				{
					retVal = nv_inputsRects [i];

					break;
				}
			}


			return retVal;
		}


		bool Nv_IsThisInIDActive (string inOutID)
		{
			bool retVal = false;

			if (string.IsNullOrEmpty (inOutID))
				return false;


			for (int i = 0; i < nv_inputsIDs.Length; i++)
			{
				if (nv_inputsIDs [i] == inOutID)
				{
					if (nv_activeInputs [i])
						retVal = true;

					break;
				}
			}


			return retVal;
		}

		void Nv_AssignInRect (string inID, Rect inRect)
		{
			for (int i = 0; i < nv_inputsIDs.Length; i++)
			{
				if (nv_inputsIDs [i] == inID)
				{
					nv_inputsRects [i] = inRect;

					break;
				}
			}
		}

		public int Nv_IndexOfInID (string inID)
		{
			int retVal = -1;

			for (int i = 0; i < nv_inputsIDs.Length; i++)
			{
				if (nv_inputsIDs [i] == inID)
				{
					retVal = i;

					break;
				}
			}

			return retVal;
		}

		void Nv_ActiveThisInput (string inputToActive)
		{ 
			for (int i = 0; i < nv_activeInputs.Length; i++)
			{
				if (nv_inputsIDs [i] == inputToActive)
				{
					nv_activeInputs [i] = true;
				}
			}
		}

		void Nv_DrawInputPermissionButton (Rect inOutRect, int indexOfInput)
		{
			if ( ! nv_dataFlowControlEnabled [indexOfInput])
			{
				nv_permittedInputs [indexOfInput] = true;

				return;
			}

			string skinPermission = nv_permittedInputs [indexOfInput]? Skins.gate: Skins.gateBloc;

			if ( ! string.IsNullOrEmpty (logic.inOutAdressCurrentToLink))
			{
				if (InOutAdressCurrentToLinkToInOutDirection () == outAdressSignature)
				{
					if (IsInOutTypeSame (InOutAdressCurrentToLinkToInOutID (), Enums.boolValues_0_ID) || 
						IsInOutTypeSameWeak (InOutAdressCurrentToLinkToInOutID (0), Enums.boolValues_0_ID) )
					{
						skinPermission = nv_permittedInputs [indexOfInput]? Skins.gateWaiting: Skins.gateBlocWaiting;
					}
				}
			}

			Rect inputPermissionRect = new Rect (inOutRect.position + new Vector2 (-inOutRect.width, 0f), 
				inOutRect.size);

			if (GUI.Button (inputPermissionRect, "", GetGuiStyle (skinPermission)) && eGlobal.button == 0)
			{
				GenericMenu menu = new GenericMenu ();

				string menuString = nv_permittedInputs [indexOfInput]? blockThisInput: allowThisInput;

				menu.AddItem (new GUIContent (menuString), false, Nv_AllowBlockInput, indexOfInput.ToString ());

				if ( ! string.IsNullOrEmpty (nv_inputsSources_forPermition [indexOfInput]))
					menu.AddItem (new GUIContent (breackTheLink), false, 
						Nv_BreakInputPermissionInputLink, indexOfInput.ToString ());

				menu.ShowAsContext ();
			}

			Nv_InputPermissionButton_AssignInput (inputPermissionRect, indexOfInput);

			Nv_InputPermissionButton_DrawLink (inputPermissionRect, indexOfInput);

			Nv_InputPermission_LinkAction (indexOfInput);
		}

		void Nv_InputPermission_LinkAction (int indexOfInput)
		{
			if (string.IsNullOrEmpty (nv_inputsSources_forPermition [indexOfInput]))
				return;

			LogicNode sourceLogicNode = logic.LogicNodeByUniqueID (
				InOutAdressCurrentToLinkToUniqueID (nv_inputsSources_forPermition [indexOfInput]));

			if (sourceLogicNode == null)
				return;

			if ( ! sourceLogicNode.activeOutputs [IndexOfOutID (
				InOutAdressCurrentToLinkToInOutID (nv_inputsSources_forPermition [indexOfInput]))])
			{
				return;
			}

			int lstIndex = sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources_forPermition [indexOfInput]);

			switch (InOutAdressCurrentToLinkToInOutID (nv_inputsSources_forPermition [indexOfInput]))
			{
			case Enums.boolValue_ID:
				nv_permittedInputs [indexOfInput] = sourceLogicNode.boolValue;
				break;

			case Enums.boolValues_0_ID:
				nv_permittedInputs [indexOfInput] = sourceLogicNode.boolValues [0];
				break;

			case Enums.boolValues_1_ID:
				nv_permittedInputs [indexOfInput] = sourceLogicNode.boolValues [1];
				break;

			case Enums.m44ValueIsIdentity_ID:
				nv_permittedInputs [indexOfInput] = sourceLogicNode.m44ValueIsIdentity;
				break;

			case Enums.m44ValueInvertible_ID:
				nv_permittedInputs [indexOfInput] = sourceLogicNode.m44ValueInvertible;
				break;


			case Enums.OffMeshLinkData_activated_ID:
				nv_permittedInputs [indexOfInput] = sourceLogicNode.OffMeshLinkData_activated;
				break;

			case Enums.OffMeshLinkData_valid_ID:
				nv_permittedInputs [indexOfInput] = sourceLogicNode.OffMeshLinkData_valid;
				break;

			case Enums.NavMeshHit_hit_ID:
				nv_permittedInputs [indexOfInput] = sourceLogicNode.NavMeshHit_hit;
				break;

			}

			switch (InOutAdressCurrentToLinkToInOutID (nv_inputsSources_forPermition [indexOfInput], -1))
			{
			case Enums.boolsList_ID:
				if (lstIndex >-1 && lstIndex < sourceLogicNode.boolsListValue.Count)
				{
					nv_permittedInputs [indexOfInput] = sourceLogicNode.boolsListValue [lstIndex];
				}
				break;
			}
		}



		void Nv_AllowBlockInput (object indexOfInput)
		{
			int i = int.Parse (indexOfInput.ToString ());

			nv_permittedInputs [i] = ! nv_permittedInputs [i];
		}

		void Nv_BreakInputPermissionInputLink (object indexOfInput)
		{
			int i = int.Parse (indexOfInput.ToString ());

			nv_inputsSources_forPermition [i] = "";
		}
	
		void Nv_InputPermissionButton_AssignInput (Rect inputPermissionRect, int indexOfInput)
		{
			if (string.IsNullOrEmpty (logic.inOutAdressCurrentToLink))
				return;

			if (InOutAdressCurrentToLinkToInOutDirection () == inAdressSignature)
				return;

			string sourceLogicNodeOutID = InOutAdressCurrentToLinkToInOutID ();

			//if ( ! Nv_IsInOutTypeSameWeak (sourceLogicNodeOutID, Enums.boolValues_0_ID))
			//	return;
			//Debug.Log (logic.inOutAdressCurrentToLink);
			if ( ! inputPermissionRect.Contains (eGlobal.mousePosition))
				return;
			

			LogicNode sourceLogicNode = logic.LogicNodeByUniqueID (InOutAdressCurrentToLinkToUniqueID ());

			if (sourceLogicNode == null)
				return;
			
			if ( ! sourceLogicNode.IsAListOutSource (logic.inOutAdressCurrentToLink))
			{
				if ( ! IsInOutTypeSame (sourceLogicNodeOutID, Enums.boolValues_0_ID))
					return;
				
				nv_inputsSources_forPermition [indexOfInput] = logic.inOutAdressCurrentToLink;

			}
			else if (sourceLogicNode.IsAListOutSource (logic.inOutAdressCurrentToLink))
			{
				if (sourceLogicNode.outputsTypes [IndexOfOutID (sourceLogicNodeOutID)] != VariableType.boolsList)
					return;

				nv_inputsSources_forPermition [indexOfInput] = logic.inOutAdressCurrentToLink;

				SetOutputLinkedIndexToDestination ();
			}
		}
	
		void Nv_InputPermissionButton_DrawLink (Rect inputPermissionRect, int indexOfInput)
		{
			if (string.IsNullOrEmpty (nv_inputsSources_forPermition [indexOfInput]))
				return;

			LogicNode sourceLogicNode = logic.LogicNodeByUniqueID (
				InOutAdressCurrentToLinkToUniqueID (nv_inputsSources_forPermition [indexOfInput]));

			if (sourceLogicNode == null)
			{
				nv_inputsSources_forPermition [indexOfInput] = "";

				return;
			}

			if ( ! sourceLogicNode.activeOutputs [IndexOfOutID (
				InOutAdressCurrentToLinkToInOutID (nv_inputsSources_forPermition [indexOfInput]))])
			{
				nv_inputsSources_forPermition [indexOfInput] = "";

				return;
			}


			Color inputPermissionLinkColor = new Color (linkColor.g, linkColor.g, linkColor.b, linkColor.a);

			Rect facingOutRect = sourceLogicNode.InOutRectOfThisInOutID (false, 
				InOutAdressCurrentToLinkToInOutID (nv_inputsSources_forPermition [indexOfInput]));

			if ( ! sourceLogicNode.IsAListOutSource (nv_inputsSources_forPermition [indexOfInput]))
			{
				DrawLink_PermissionButton (
					facingOutRect.center + gapPermissionLinkDraw * new Vector2 (0f, facingOutRect.height), 
					inputPermissionRect.center + gapPermissionLinkDraw * new Vector2 (0f, inputPermissionRect.height),
					inputPermissionLinkColor);
			}
			else if (sourceLogicNode.IsAListOutSource (nv_inputsSources_forPermition [indexOfInput]))
			{
				//int listIndexFinal = sourceLogicNode.InoutAdressCurrentToLinkToListIndex ();

				string sourceNodeOutID =
					sourceLogicNode.InOutAdressCurrentToLinkToInOutID (nv_inputsSources_forPermition [indexOfInput], 0);

				int sourceNodeOutIndex = sourceLogicNode.IndexOfOutID (sourceNodeOutID);

				if (sourceNodeOutIndex < 0 || sourceNodeOutIndex > sourceLogicNode.activeOutputs.Length-1)
					return;

				if ( ! sourceLogicNode.activeOutputs [sourceNodeOutIndex])
					return;

				if (sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources_forPermition [indexOfInput]) > -1 && 
					sourceLogicNode.InoutAdressToLinkToListIndex (nv_inputsSources_forPermition [indexOfInput]) < 
					sourceLogicNode.outputListCounts [sourceNodeOutIndex])
				{
					DrawLink_PermissionButton (
						sourceLogicNode.GetDecaledListRect (sourceLogicNode, sourceNodeOutIndex, 
							nv_inputsSources_forPermition [indexOfInput]).center
						+ gapPermissionLinkDraw * new Vector2 (0f, inputPermissionRect.height),
						inputPermissionRect.center + gapPermissionLinkDraw * new Vector2 (0f, 
							inputPermissionRect.height),
						inputPermissionLinkColor);
				}
			}
		}

		void Nv_OpenInputFieldOptionsMenu (Rect inOutRect, string inOutID)
		{
			variableFieldIDToMakePublicPrivate = inOutID;

			int inIndex = Nv_IndexOfInID (inOutID);

			if (inIndex > -1 && inIndex < nv_publicInputs.Length)
			{

				string makeThisVariblePubPrivMenu = 
					nv_publicInputs [Nv_IndexOfInID (inOutID)]? "Make this variable private":
					"Make this variable public";

				GenericMenu menu = new GenericMenu ();

				menu.AddItem (new GUIContent (makeThisVariblePubPrivMenu), false, Nv_MakeVariablePublicPrivate);




				if ( ! string.IsNullOrEmpty (nv_inputsSources [inIndex]))
					menu.AddItem (new GUIContent (breackTheLink), false, Nv_BreackTheLink, inOutID);

				menu.AddItem (new GUIContent (
					nv_dataFlowControlEnabled [inIndex]?"Disable Control of Data Flow":"Enable Control of Data Flow"),
					false, Nv_EnableDisableDataFlowControl, inOutID);

				menu.ShowAsContext ();
			}
		}

		void Nv_MakeVariablePublicPrivate ()
		{
			if (string.IsNullOrEmpty (variableFieldIDToMakePublicPrivate))
				return;

			////Debug.Log (variableFieldIDToMakePublicPrivate);

			nv_publicInputs [Nv_IndexOfInID (variableFieldIDToMakePublicPrivate)] = 
				! nv_publicInputs [Nv_IndexOfInID (variableFieldIDToMakePublicPrivate)];
		}

		void Nv_EnableDisableDataFlowControl (object so)
		{
			nv_dataFlowControlEnabled [Nv_IndexOfInID (so.ToString ())] = 
				! nv_dataFlowControlEnabled [Nv_IndexOfInID (so.ToString ())];
		}

		void Nv_BreackTheLink (object so)
		{
			nv_inputsSources [Nv_IndexOfInID (so.ToString ())] = "";
		}
	


		string Nv_Script (VariableType varType, CsLogicNodeWhere csLogicNodeWhere)
		{
			string [] nv_ids = new string[nv_perTypeCount];

			switch (varType)
			{
			case VariableType.Bool://1
				nv_ids = nv_Bool_IDs;
				break;

			case VariableType.Color://2
				nv_ids = nv_Color_IDs;
				break;

			case VariableType.Float://3
				nv_ids = nv_float_IDs;
				break;

			case VariableType.Int://4
				nv_ids = nv_int_IDs;
				break;

			case VariableType.Material://5
				nv_ids = nv_Material_IDs;
				break;

			case VariableType.rect://6
				nv_ids = nv_Rect_IDs;
				break;

			case VariableType.Shader://7
				nv_ids = nv_Shader_IDs;
				break;

			case VariableType.String://8
				nv_ids = nv_String_IDs;
				break;

			case VariableType.Texture2D://9
				nv_ids = nv_Texture2D_IDs;
				break;

			case VariableType.Vector2://10
				nv_ids = nv_Vector2_IDs;
				break;

			case VariableType.Vector3://11
				nv_ids = nv_Vector3_IDs;
				break;

			case VariableType.Vector4://12
				nv_ids = nv_Vector4_IDs;
				break;
			}

			string r = "";

			for (int i = 0; i < nv_ids.Length; i++)
			{
				if (csLogicNodeWhere == CsLogicNodeWhere.globalVariables)
				{
					if (i == 0)
						r += Nv_Script (nv_ids [i], varType, csLogicNodeWhere);
				}
				else if (csLogicNodeWhere == CsLogicNodeWhere.constructor)
				{
					r += Nv_Script (nv_ids [i], varType, csLogicNodeWhere);
				}
			}

			return r;
		}
		string Nv_Script (VariableType varType, CsLogicNodeWhere csLogicNodeWhere, bool withIdentifiedObjectsDecl)
		{
			string [] nv_ids = new string[nv_perTypeCount];

			switch (varType)
			{
			case VariableType.Bool://1
				nv_ids = nv_Bool_IDs;
				break;

			case VariableType.Color://2
				nv_ids = nv_Color_IDs;
				break;

			case VariableType.Float://3
				nv_ids = nv_float_IDs;
				break;

			case VariableType.Int://4
				nv_ids = nv_int_IDs;
				break;

			case VariableType.Material://5
				nv_ids = nv_Material_IDs;
				break;

			case VariableType.rect://6
				nv_ids = nv_Rect_IDs;
				break;

			case VariableType.Shader://7
				nv_ids = nv_Shader_IDs;
				break;

			case VariableType.String://8
				nv_ids = nv_String_IDs;
				break;

			case VariableType.Texture2D://9
				nv_ids = nv_Texture2D_IDs;
				break;

			case VariableType.Vector2://10
				nv_ids = nv_Vector2_IDs;
				break;

			case VariableType.Vector3://11
				nv_ids = nv_Vector3_IDs;
				break;

			case VariableType.Vector4://12
				nv_ids = nv_Vector4_IDs;
				break;
			}

			string r = "";

			for (int i = 0; i < nv_ids.Length; i++)
			{
				if (csLogicNodeWhere == CsLogicNodeWhere.globalVariables)
				{
					if (i == 0)
						r += Nv_Script (nv_ids [i], varType, csLogicNodeWhere, withIdentifiedObjectsDecl);
				}
				else if (csLogicNodeWhere == CsLogicNodeWhere.constructor)
				{
					r += Nv_Script (nv_ids [i], varType, csLogicNodeWhere, withIdentifiedObjectsDecl);
				}
			}

			return r;
		}

		string Nv_Script (string id, VariableType varType, CsLogicNodeWhere csLogicNodeWhere, bool withIdentifiedObjectsDecl)
		{
			string r = "";

			switch (csLogicNodeWhere)
			{
			case CsLogicNodeWhere.globalVariables:
				r = Nv_Script (id, varType, withIdentifiedObjectsDecl) [0];
				break;

			case CsLogicNodeWhere.constructor:
				r = Nv_Script (id, varType, withIdentifiedObjectsDecl) [1];
				break;
			}

			return r;
		}
		string Nv_Script (string id, VariableType varType, CsLogicNodeWhere csLogicNodeWhere)
		{
			string r = "";

			switch (csLogicNodeWhere)
			{
			case CsLogicNodeWhere.globalVariables:
				r = Nv_Script (id, varType) [0];
				break;

			case CsLogicNodeWhere.constructor:
				r = Nv_Script (id, varType) [1];
				break;
			}

			return r;
		}

		string [] Nv_Script (string id, VariableType varType, bool withIdentifiedObjectsDecl)
		{
			string [] r = new string[2];

			switch (varType)
			{
			case VariableType.Bool://1
				r = Nv_Script_bool (id);
				break;

			case VariableType.Color://2
				r = Nv_Script_color (id);
				break;

			case VariableType.Float://3
				r = Nv_Script_float (id);
				break;

			case VariableType.Int://4
				r = Nv_Script_int (id);
				break;

			case VariableType.Material://5
				r = Nv_Script_material (id, withIdentifiedObjectsDecl);
				break;

			case VariableType.rect://6
				r = Nv_Script_rect (id);
				break;

			case VariableType.Shader://7
				r = Nv_Script_shader (id, withIdentifiedObjectsDecl);
				break;

			case VariableType.String://8
				r = Nv_Script_string (id);
				break;

			case VariableType.Texture2D://9
				r = Nv_Script_texture2D (id, withIdentifiedObjectsDecl);
				break;

			case VariableType.Vector2://10
				r = Nv_Script_vector2 (id);
				break;

			case VariableType.Vector3://11
				r = Nv_Script_vector3 (id);
				break;

			case VariableType.Vector4://12
				r = Nv_Script_vector4 (id);
				break;
			}

			return r;
		}
		string [] Nv_Script (string id, VariableType varType)
		{
			string [] r = new string[2];

			switch (varType)
			{
			case VariableType.Bool://1
				r = Nv_Script_bool (id);
				break;

			case VariableType.Color://2
				r = Nv_Script_color (id);
				break;

			case VariableType.Float://3
				r = Nv_Script_float (id);
				break;

			case VariableType.Int://4
				r = Nv_Script_int (id);
				break;

			case VariableType.Material://5
				
				break;

			case VariableType.rect://6
				r = Nv_Script_rect (id);
				break;

			case VariableType.Shader://7
				
				break;

			case VariableType.String://8
				r = Nv_Script_string (id);
				break;

			case VariableType.Texture2D://9
				
				break;

			case VariableType.Vector2://10
				r = Nv_Script_vector2 (id);
				break;

			case VariableType.Vector3://11
				r = Nv_Script_vector3 (id);
				break;

			case VariableType.Vector4://12
				r = Nv_Script_vector4 (id);
				break;
			}

			return r;
		}


		string [] Nv_Script_vector4 (string id)
		{
			string [] r = new string[2];

			string varType_0 = "Vector4 [] ";
			string varType_1 = "Vector4 [";
			string varName = "nv_Vector4 ";


			for (int i = 0; i < nv_Vector4_IDs.Length; i++)
			{
				if (nv_Vector4_IDs [i] == id)
				{
					r [0] = "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";
					r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + Vector4ToScriptWrite (nv_Vector4 [i]) + ";\n";
				}
			}

			return r;
		}
		string [] Nv_Script_vector3 (string id)
		{
			string [] r = new string[2];

			string varType_0 = "Vector3 [] ";
			string varType_1 = "Vector3 [";
			string varName = "nv_Vector3 ";


			for (int i = 0; i < nv_Vector3_IDs.Length; i++)
			{
				if (nv_Vector3_IDs [i] == id)
				{
					r [0] = "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";
					r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + Vector3ToScriptWrite (nv_Vector3 [i]) + ";\n";
				}
			}

			return r;
		}
		string [] Nv_Script_vector2 (string id)
		{
			string [] r = new string[2];

			string varType_0 = "Vector2 [] ";
			string varType_1 = "Vector2 [";
			string varName = "nv_Vector2 ";


			for (int i = 0; i < nv_Vector2_IDs.Length; i++)
			{
				if (nv_Vector2_IDs [i] == id)
				{
					r [0] = "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";
					r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + Vector2ToScriptWrite (nv_Vector2 [i]) + ";\n";
				}
			}

			return r;
		}
		string [] Nv_Script_texture2D (string id, bool withIdentifiedObjectsDecl)
		{
			string [] r = new string[2];

			//

			//
			string varType_0 = "Texture2D [] ";
			string varType_1 = "Texture2D [";
			string varName = "nv_Texture2D ";


			if (withIdentifiedObjectsDecl)
				r [0] = ExprWs.Gv.identifiedObjects;
			else
				r [0] = "";
			for (int i = 0; i < nv_Texture2D_IDs.Length; i++)
			{
				if (nv_Texture2D_IDs [i] == id)
				{
					r [0] += "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";

					r [1] = Nv_ConstructorGetIdentifiedObject (new string [] {nv_Texture2D_IDs [i],}, VariableType.Texture2D);
					//r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + Vector2ToScriptWrite (nv_Vector2 [i]) + ";\n";
				}
			}


			return r;
		}

		string [] Nv_Script_material (string id, bool withIdentifiedObjectsDecl)
		{
			string [] r = new string[2];

			string varType_0 = "Material [] ";
			string varType_1 = "Material [";
			string varName = "nv_Material ";

			if (withIdentifiedObjectsDecl)
				r [0] = ExprWs.Gv.identifiedObjects;
			else
				r [0] = "";
			for (int i = 0; i < nv_Material_IDs.Length; i++)
			{
				if (nv_Material_IDs [i] == id)
				{
					r [0] += "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";

					r [1] = Nv_ConstructorGetIdentifiedObject (new string [] {nv_Material_IDs [i],}, VariableType.Material);
					//r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + Vector2ToScriptWrite (nv_Vector2 [i]) + ";\n";
				}
			}


			return r;
		}

		string [] Nv_Script_rect (string id)
		{
			string [] r = new string[2];

			string varType_0 = "Rect [] ";
			string varType_1 = "Rect [";
			string varName = "nv_Rect ";


			for (int i = 0; i < nv_Rect_IDs.Length; i++)
			{
				if (nv_Rect_IDs [i] == id)
				{
					r [0] = "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";
					r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + RectToScriptWrite (nv_Rect [i]) + ";\n";
				}
			}

			return r;
		}

		string [] Nv_Script_color (string id)
		{
			string [] r = new string[2];

			string varType_0 = "Color [] ";
			string varType_1 = "Color [";
			string varName = "nv_Color ";


			for (int i = 0; i < nv_Color_IDs.Length; i++)
			{
				if (nv_Color_IDs [i] == id)
				{
					r [0] = "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";
					r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + ColorToScriptWrite (nv_Color [i]) + ";\n";
				}
			}


			return r;
		}


		string [] Nv_Script_float (string id)
		{
			string [] r = new string[2];

			string varType_0 = "float [] ";
			string varType_1 = "float [";
			string varName = "nv_float ";


			for (int i = 0; i < nv_float_IDs.Length; i++)
			{
				if (nv_float_IDs [i] == id)
				{
					r [0] = "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";
					r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + nv_float [i] + "f" + ";\n";
				}
			}

			return r;
		}

		string [] Nv_Script_int (string id)
		{
			string [] r = new string[2];

			string varType_0 = "int [] ";
			string varType_1 = "int [";
			string varName = "nv_int ";


			for (int i = 0; i < nv_int_IDs.Length; i++)
			{
				if (nv_int_IDs [i] == id)
				{
					r [0] = "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";
					r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + nv_int [i] + ";\n";
				}
			}

			return r;
		}
		string [] Nv_Script_bool (string id)
		{
			string [] r = new string[2];

			string varType_0 = "bool [] ";
			string varType_1 = "bool [";
			string varName = "nv_Bool ";


			for (int i = 0; i < nv_Bool_IDs.Length; i++)
			{
				if (nv_Bool_IDs [i] == id)
				{
					r [0] = "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";
					r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + StringTreatment.FirstToLower (nv_Bool [i].ToString ()) + ";\n";
				}
			}


			return r;
		}
		string [] Nv_Script_string (string id)
		{
			string [] r = new string[2];

			string varType_0 = "String [] ";
			string varType_1 = "String [";
			string varName = "nv_String ";


			for (int i = 0; i < nv_String_IDs.Length; i++)
			{
				if (nv_String_IDs [i] == id)
				{
					r [0] = "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";
					r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + "\"" + nv_String [i] + "\"" + ";\n";
				}
			}

			return r;
		}
		string [] Nv_Script_shader (string id, bool withIdentifiedObjectsDecl)
		{
			string [] r = new string[2];

			string varType_0 = "Shader [] ";
			string varType_1 = "Shader [";
			string varName = "nv_Shader ";

			if (withIdentifiedObjectsDecl)
				r [0] = ExprWs.Gv.identifiedObjects;
			else
				r [0] = "";
			for (int i = 0; i < nv_Shader_IDs.Length; i++)
			{
				if (nv_Shader_IDs [i] == id)
				{
					r [0] += "\t\t" + "public " + varType_0 + varName + "= new " + varType_1 + nv_perTypeCount.ToString () + "];\n";

					r [1] = Nv_ConstructorGetIdentifiedObject (new string [] {nv_Shader_IDs [i],}, VariableType.Shader);
					//r [1] = "\t\t\t" + varName + " [" + i.ToString () + "] = " + Vector2ToScriptWrite (nv_Vector2 [i]) + ";\n";
				}
			}



			return r;
		}
	
	
	
		string Nv_ConstructorGetIdentifiedObject (string [] arrayInOutIDs, VariableType varType)
		{
			string r = "";
			r += ExprWs.ConstructorExpr.identifiedObjects;
			r += identifiedObjectsNotNull;
			for (int i = 0; i < arrayInOutIDs.Length; i++)
			{
				r += Nv_ConstructorGetIdentifiedObject (arrayInOutIDs [i], varType);
			}
			r += "\t\t\t}\n";
			r += "\n";

			return r;
		}

		string Nv_ConstructorGetIdentifiedObject (string inOutID, VariableType varType)
		{
			string r = "";

			string fieldUID = "";

			string varTypeString = Nv_VarTypeToObjectType (varType);

			string varName = "";

			string func = "identifiedObjects.GetIdentifiedObject (";


			fieldUID = FieldUniqueID (inOutID);

			varName = CsScriptWriter.Nv_InOutIdToVarName (StringTreatment.AfterThat(fieldUID, '.'), varType, this);

			r += tab4 + varName + " = (" + varTypeString + ")" + func + "\"" + fieldUID + "\"" + ");\n"; 

		
			return r;
		}

		string Nv_VarTypeToObjectType (VariableType varType)
		{
			string r = "";

			switch (varType)
			{
			case VariableType.Material:
				r = "Material";
				break;

			case VariableType.Texture2D:
				r = "Texture2D";
				break;

			case VariableType.Shader:
				r = "Shader";
				break;
			}

			return r;
		}
	}

}
