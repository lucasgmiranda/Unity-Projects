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
		public enum MaterialType
		{
			wood,

			pbrCooker,
		}
		public MaterialType materialType;
		public int materialType_length = 0;
		public string materialType_last;

		partial void LogicNode_materials ()
		{
			DrawMaterialTypeEnum ();
		}

		void AdaptOnMaterialType ()
		{
			switch (materialType)
			{
			case MaterialType.pbrCooker:
				rectWidthFactorDefaultFactor = 1f;
				LogicNode_PbrCooker ();
				GetPbrCooker ();
				break;

			case MaterialType.wood:
				rectWidthFactorDefaultFactor = 1f;
				LogicNode_wood ();
				GetWood ();
				break;
			}
		}
		partial void LogicNode_wood ();
		void GetWood ()
		{
			if ( ! ExtensionsHere.wood)
				DrawExtensionUrlButton ("Wood", ExtensionsHere.woodUrl);
		}

		partial void LogicNode_PbrCooker ();
		void GetPbrCooker ()
		{
			if ( ! ExtensionsHere.pbrCooker)
				DrawExtensionUrlButton ("PBR Cooker", ExtensionsHere.pbrCookerUrl);
		}

		void DrawMaterialTypeEnum ()
		{
			DrawLogicNodeLabel ("Type", 0, 2);
			materialType = (MaterialType)DrawEnum (materialType, ref materialType_length, ref materialType_last,
				typeof (MaterialType), FieldDrawType.label, 1, 2);
		
			AdaptOnMaterialType ();
		}
	}
}