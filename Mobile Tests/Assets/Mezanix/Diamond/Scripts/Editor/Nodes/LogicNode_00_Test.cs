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
		void Test_Head ()
		{
			Test_Inputs ();
			if (logic.playing) Test ();
			Test_Outputs ();
		}
		void Test_Inputs ()
		{ 
			for (int i = 0; i < nv_perTypeCount; i++)
			{
				Nv_DrawInputField (nv_int_IDs [i], ref nv_int [i], "int " + i);
			}
			GetSuitableRect (FieldDrawType.label);
			for (int i = 0; i < nv_perTypeCount; i++)
			{				
				Nv_DrawInputField (nv_float_IDs [i], ref nv_float [i], "float " + i);
			}
			GetSuitableRect (FieldDrawType.label);
			for (int i = 0; i < nv_perTypeCount; i++)
			{				
				Nv_DrawInputField (nv_Vector2_IDs [i], ref nv_Vector2 [i], "Vect2 " + i);
			}
			GetSuitableRect (FieldDrawType.label);
			for (int i = 0; i < nv_perTypeCount; i++)
			{				
				Nv_DrawInputField (nv_Vector3_IDs [i], ref nv_Vector3 [i], "Vect3 " + i, false);
			}
			GetSuitableRect (FieldDrawType.label);
			for (int i = 0; i < nv_perTypeCount; i++)
			{				
				Nv_DrawInputField (nv_Vector4_IDs [i], ref nv_Vector4 [i], "Vect4 " + i);
			}
			GetSuitableRect (FieldDrawType.label);
			for (int i = 0; i < nv_perTypeCount; i++)
			{				
				Nv_DrawInputField (nv_Rect_IDs [i], ref nv_Rect [i], "Rect " + i);
			}
			GetSuitableRect (FieldDrawType.label);
			for (int i = 0; i < nv_perTypeCount; i++)
			{				
				Nv_DrawInputField (nv_Color_IDs [i], ref nv_Color [i], "Color " + i);
			}
			GetSuitableRect (FieldDrawType.label);
			for (int i = 0; i < nv_perTypeCount; i++)
			{				
				Nv_DrawInputField (nv_String_IDs [i], ref nv_String [i], "String " + i);
			}
			GetSuitableRect (FieldDrawType.label);
			for (int i = 0; i < nv_perTypeCount; i++)
			{				
				Nv_DrawInputField (nv_Bool_IDs [i], ref nv_Bool [i], "Bool " + i);
			}
			GetSuitableRect (FieldDrawType.label);
			for (int i = 0; i < nv_perTypeCount; i++)
			{				
				Nv_DrawInputField (nv_Texture2D_IDs [i], ref nv_Texture2D [i], "Texture2D " + i);
			}
			GetSuitableRect (FieldDrawType.label);
			for (int i = 0; i < nv_perTypeCount; i++)
			{				
				Nv_DrawInputField (nv_Material_IDs [i], ref nv_Material [i], "Material " + i);
			}
			GetSuitableRect (FieldDrawType.label);
			for (int i = 0; i < nv_perTypeCount; i++)
			{				
				Nv_DrawInputField (nv_Shader_IDs [i], ref nv_Shader [i], "Shader " + i);
			}
		}
		void Test ()
		{
		}
		void Test_Outputs ()
		{
		}
		string Test_Code (CsLogicNodeWhere csLogicNodeWhere)
		{
			return Nv_Script_all (csLogicNodeWhere);

			//return Nv_Script_id (csLogicNodeWhere);
		}

		string Nv_Script_all (CsLogicNodeWhere csLogicNodeWhere)
		{
			string r = "";

			r += Nv_Script (VariableType.Int, csLogicNodeWhere);

			r += Nv_Script (VariableType.Float, csLogicNodeWhere);

			r += Nv_Script (VariableType.Vector2, csLogicNodeWhere);

			r += Nv_Script (VariableType.Vector3, csLogicNodeWhere);

			r += Nv_Script (VariableType.Vector4, csLogicNodeWhere);

			r += Nv_Script (VariableType.rect, csLogicNodeWhere);

			r += Nv_Script (VariableType.Color, csLogicNodeWhere);

			r += Nv_Script (VariableType.String, csLogicNodeWhere);

			r += Nv_Script (VariableType.Bool, csLogicNodeWhere);


			r += Nv_Script (VariableType.Texture2D, csLogicNodeWhere, true);

			r += Nv_Script (VariableType.Material, csLogicNodeWhere, false);

			r += Nv_Script (VariableType.Shader, csLogicNodeWhere, false);
			return r;
		}

		string Nv_Script_id (CsLogicNodeWhere csLogicNodeWhere)
		{
			string r = "";

			r += Nv_Script (nv_int_IDs [8], VariableType.Int, csLogicNodeWhere);

			r += Nv_Script (nv_float_IDs [6], VariableType.Float, csLogicNodeWhere);

			r += Nv_Script (nv_Vector2_IDs [2], VariableType.Vector2, csLogicNodeWhere);

			r += Nv_Script (nv_Vector3_IDs [2], VariableType.Vector3, csLogicNodeWhere);

			r += Nv_Script (nv_Vector4_IDs [2], VariableType.Vector4, csLogicNodeWhere);

			r += Nv_Script (nv_Rect_IDs [2], VariableType.rect, csLogicNodeWhere);

			r += Nv_Script (nv_Color_IDs [2], VariableType.Color, csLogicNodeWhere);

			r += Nv_Script (nv_String_IDs [2], VariableType.String, csLogicNodeWhere);

			r += Nv_Script (nv_Bool_IDs [2], VariableType.Bool, csLogicNodeWhere);


			r += Nv_Script (nv_Texture2D_IDs [2], VariableType.Texture2D, csLogicNodeWhere, true);

			r += Nv_Script (nv_Material_IDs [2], VariableType.Material, csLogicNodeWhere, false);

			r += Nv_Script (nv_Shader_IDs [2], VariableType.Shader, csLogicNodeWhere, false);
			return r;
		}
	}
}