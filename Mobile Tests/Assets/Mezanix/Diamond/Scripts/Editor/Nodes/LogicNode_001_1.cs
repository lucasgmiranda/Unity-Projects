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
	/// LogicNode_001_1
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


	/// Access materiales in renderer component
	public partial class LogicNode : ScriptableObject 
	{
		#region componentRenderer
		List<Material> ArrayToList (Material [] arr)
		{
			List<Material> r = new List<Material> ();

			for (int i = 0; i < arr.Length; i++)
			{
				r.Add (arr [i]);
			}

			return r;
		}
		const string ArrayToList_Sript_Material = "\t\tList<Material> ArrayToList (Material [] arr)\n\t\t{\n\t\t\tList<Material> r = new List<Material> ();\n\n\t\t\tfor (int i = 0; i < arr.Length; i++)\n\t\t\t{\n\t\t\t\tr.Add (arr [i]);\n\t\t\t}\n\n\t\t\treturn r;\n\t\t}\n\n";

		void ApplyComputeRenderer ()
		{
			ComputeRenderer_InputFields();
			if (logic.playing) ComputeRenderer();
			ComputeRenderer_OutputFields ();
		}

		public int computeRendererType_length = 0;
		public string computeRendererType_last;
		void ComputeRenderer_InputFields ()
		{
			linkedOrAttachedTo = (IsGameObject_0_InputLinked () && gameObjectValues [0] == null) || 
				IsGameObject_0_AttachedTo () || 
				gameObjectValues [0] == null;

			DrawGameObjectFieldInput (0);

			if ( ! linkedOrAttachedTo)
			{
				if (gameObjectValues [0] == null)
				{
					DrawGameObject_0_isNullInfo ();

					computeRendererType = (ComputeRendererType)DrawEnumComputeType (computeRendererType, 
						ref computeRendererType_length, ref computeRendererType_last, typeof(ComputeRendererType));

					return;
				}
			}
			if (linkedOrAttachedTo)
			{
				DrawInNodeInfo (noRunInEditorWithAttachedToOrLinked);
			}
			if ( ! linkedOrAttachedTo)
			{
				renderer = gameObjectValues [0].GetComponent <Renderer>();

				if (renderer == null)
				{
					DrawInNodeInfo ("Add Renderer To GameObject");

					linkedOrAttachedTo = (IsGameObject_0_InputLinked () && gameObjectValues [0] == null) || 
						IsGameObject_0_AttachedTo () || 
						gameObjectValues [0] == null ||
						renderer == null;

					return;
				}

			}
			computeRendererType = (ComputeRendererType)DrawEnumComputeType (computeRendererType, 
				ref computeRendererType_length, ref computeRendererType_last, typeof(ComputeRendererType));

			switch (computeRendererType)
			{
			case ComputeRendererType.getSharedMaterial:

				break;

			case ComputeRendererType.setSharedMaterial:
				DrawMaterialFieldInput (0);
				break;

			case ComputeRendererType.getMaterial:

				break;

			case ComputeRendererType.setMaterial:
				DrawMaterialFieldInput (0);
				break;

			case ComputeRendererType.setProbeAnchor:
				DrawGameObjectFieldInput (1);				if (gameObjectValues [1] == null)
				{
					DrawInNodeInfo ("Fill in GameObject field");

					return;
				}
				break;

			case ComputeRendererType.setLightmapIndex:
				DrawIntInputField (0);
				break;

			case ComputeRendererType.setEnabled:
				DrawBoolInputField (0);
				break;

			case ComputeRendererType.getProbeAnchor:

				break;

			case ComputeRendererType.getLocalToWorldMatrix:

				break;

			case ComputeRendererType.getLightmapScaleOffset:

				break;

			case ComputeRendererType.getLightmapIndex:

				break;

			case ComputeRendererType.getIsVisible:

				break;

			case ComputeRendererType.getIsPartOfStaticBatch:

				break;

			case ComputeRendererType.getEnabled:

				break;

			case ComputeRendererType.getBounds:

				break;

			}
			DrawDoItButton();
		}
		void ComputeRenderer ()
		{
			if (linkedOrAttachedTo)
				return;			if ( ! doIT)
			{
				return;
			}
			if (IsGameObject_0_AttachedTo ())
			{
				return;
			}
			AssignGameObjectField (ref gameObjectValue, ref gameObjectValueOld, gameObjectValues [0]);

			switch (computeRendererType)
			{
			case  ComputeRendererType.getSharedMaterialsArray:
				materialsListValue = ArrayToList (renderer.sharedMaterials);
				break;

			case  ComputeRendererType.getMaterialsArray:
				materialsListValue = ArrayToList (renderer.materials);
				break;

			case ComputeRendererType.getSharedMaterial:
				materialValue = renderer.sharedMaterial;
				break;

			case ComputeRendererType.setSharedMaterial:
				renderer.sharedMaterial = materialValues [0];
				break;

			case ComputeRendererType.getMaterial:
				materialValue = renderer.material;
				break;

			case ComputeRendererType.setMaterial:
				renderer.material = materialValues [0];
				break;

			case ComputeRendererType.setProbeAnchor:
				renderer.probeAnchor = gameObjectValues [1].transform;
				break;

			case ComputeRendererType.setLightmapIndex:
				renderer.lightmapIndex = intValues [0];
				break;

			case ComputeRendererType.setEnabled:
				renderer.enabled = boolValues [0];
				break;

			case ComputeRendererType.getProbeAnchor:
				if (renderer.probeAnchor != null)
				{
					gameObjectValue = renderer.probeAnchor.gameObject;
				}
				break;

			case ComputeRendererType.getLocalToWorldMatrix:
				m44Value_entier = renderer.localToWorldMatrix;
				SetM44Value (m44Value_entier);
				break;

			case ComputeRendererType.getLightmapScaleOffset:
				vector4Value = renderer.lightmapScaleOffset;
				break;

			case ComputeRendererType.getLightmapIndex:
				intValue = renderer.lightmapIndex;
				break;

			case ComputeRendererType.getIsVisible:
				boolValue = renderer.isVisible;
				break;

			case ComputeRendererType.getIsPartOfStaticBatch:
				boolValue = renderer.isPartOfStaticBatch;
				break;

			case ComputeRendererType.getEnabled:
				boolValue = renderer.enabled;
				break;

			case ComputeRendererType.getBounds:
				boundsCenterValue = renderer.bounds.center;

				boundsExtentsValue = renderer.bounds.extents;

				boundsMaxValue = renderer.bounds.max;

				boundsMinValue = renderer.bounds.min;

				boundsSizeValue = renderer.bounds.size;
				break;

			}
		}
		void ComputeRenderer_OutputFields ()
		{

			string [] documentationMessage;

			switch (computeRendererType)
			{
			case  ComputeRendererType.getSharedMaterialsArray:
				DrawMaterialListResultField (false);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"All the shared materials of this object.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-sharedMaterials.html", 
					"");
				
				break;

			case ComputeRendererType.getMaterialsArray:
				DrawMaterialListResultField (false);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Returns all the instantiated materials of this object.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-materials.html", 
					"");
				break;

			case ComputeRendererType.getSharedMaterial:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"The shared material of this object.",
					"Modifying sharedMaterial will change ",
					"the appearance of all objects using this material.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-sharedMaterial.html", 
					"");
				break;

			case ComputeRendererType.setSharedMaterial:
				DrawGameObjectResultField (ObjectResultDrawChoice.itsName);

				documentationMessage = 
					new string[]
				{
					"",
					"The shared material of this object.",
					"Modifying sharedMaterial will change ",
					"the appearance of all objects using this material.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-sharedMaterial.html", 
					"");
				break;

			case ComputeRendererType.getMaterial:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"The first instantiated Material assigned to the renderer.",
					"Modifying material will change the material for this object only.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-material.html", 
					"");
				break;

			case ComputeRendererType.setMaterial:
				DrawGameObjectResultField (ObjectResultDrawChoice.itsName);

				documentationMessage = 
					new string[]
				{
					"",
					"The first instantiated Material assigned to the renderer.",
					"Modifying material will change the material for this object only.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-material.html", 
					"");
				break;

			case ComputeRendererType.setProbeAnchor:
				DrawGameObjectResultField (ObjectResultDrawChoice.itsName);

				documentationMessage = 
					new string[]
				{
					"",
					"If set, Renderer will use this Transform's position to find the light or reflection probe.",
					"Otherwise the center of Renderer's AABB will be used.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-probeAnchor.html", 
					"");
				break;

			case ComputeRendererType.setLightmapIndex:
				DrawGameObjectResultField (ObjectResultDrawChoice.itsName);

				documentationMessage = 
					new string[]
				{
					"",
					"The index of the baked lightmap applied to this renderer.",
					"The index refers to the LightmapSettings.lightmaps array.",
					"A value of -1 (0xFFFF) means no lightmap has been assigned, which is the default.",
					"A value of 0xFFFE is internally used for objects that have their scale in lightmap set to 0;",
					"they affect lightmaps, but don't have a lightmap assigned themselves.",
					"The index is 16 bits internally and can't be larger than 65533 (0xFFFE).",
					"Note: this property is only serialized when building the player.",
					"In all the other cases it's the responsibility of the Unity lightmapping system",
					"(or a custom script that brings external lightmapping data) to set it",
					"when the scene loads or playmode is entered.",
					"A lightmap is a texture atlas",
					"and multiple Renderers can use different portions of the same lightmap.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-lightmapIndex.html", 
					"");
				break;

			case ComputeRendererType.setEnabled:
				DrawGameObjectResultField (ObjectResultDrawChoice.itsName);

				documentationMessage = 
					new string[]
				{
					"",
					"Makes the rendered 3D object visible if enabled.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-enabled.html", 
					"");
				break;

			case ComputeRendererType.getProbeAnchor:
				DrawGameObjectResultField (ObjectResultDrawChoice.itsName);

				documentationMessage = 
					new string[]
				{
					"",
					"If set, Renderer will use this Transform's position to find the light or reflection probe.",
					"Otherwise the center of Renderer's AABB will be used.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-probeAnchor.html", 
					"");
				break;

			case ComputeRendererType.getLocalToWorldMatrix:
				DrawM44ResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"Matrix that transforms a point from local space into world space (Read Only).",
					"This property MUST be used instead of Transform.localToWorldMatrix,",
					"if you're setting shader parameters.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-localToWorldMatrix.html", 
					"");
				break;

			case ComputeRendererType.getLightmapScaleOffset:
				DrawVector4ResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"The UV scale & offset used for a lightmap.",
					"A lightmap is a texture atlas and multiple Renderers can use different portions of the same lightmap.",
					"The vector's x and y refer to UV scale, while z and w refer to UV offset.",
					"Note: this property is only serialized when building the player.",
					"In all the other cases it's the responsibility of the Unity lightmapping system",
					"(or a custom script that brings external lightmapping data) to set it when",
					"the scene loads or playmode is entered.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-lightmapScaleOffset.html", 
					"");
				break;

			case ComputeRendererType.getLightmapIndex:
				DrawIntResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"The index of the baked lightmap applied to this renderer.",
					"The index refers to the LightmapSettings.lightmaps array.",
					"A value of -1 (0xFFFF) means no lightmap has been assigned, which is the default.",
					"A value of 0xFFFE is internally used for objects that have their scale in lightmap set to 0;",
					"they affect lightmaps, but don't have a lightmap assigned themselves.",
					"The index is 16 bits internally and can't be larger than 65533 (0xFFFE).",
					"Note: this property is only serialized when building the player.",
					"In all the other cases it's the responsibility of the Unity lightmapping system",
					"(or a custom script that brings external lightmapping data) to set it",
					"when the scene loads or playmode is entered.",
					"A lightmap is a texture atlas",
					"and multiple Renderers can use different portions of the same lightmap.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-lightmapIndex.html", 
					"");
				break;

			case ComputeRendererType.getIsVisible:
				DrawBoolResultField ();

				documentationMessage = 
					new string[]
				{
					"",
					"Is this renderer visible in any camera? (Read Only)",
					"Note that the object is considered visible when it needs to be rendered in the scene.",
					"For example, it might not actually be visible by any camera but still need",
					"to be rendered for shadows. When running in the editor, the scene view cameras",
					"will also cause this value to be true.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-isVisible.html", 
					"");
				break;

			case ComputeRendererType.getIsPartOfStaticBatch:
				DrawBoolResultField ();

				documentationMessage = 
					new string[]
				{
					"",
					"Has this renderer been statically batched with any other renderers?",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-isPartOfStaticBatch.html", 
					"");
				break;

			case ComputeRendererType.getEnabled:
				DrawBoolResultField ();

				documentationMessage = 
					new string[]
				{
					"",
					"Makes the rendered 3D object visible if enabled.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-enabled.html", 
					"");
				break;

			case ComputeRendererType.getBounds:
				DrawBoundsResultField ();

				documentationMessage = 
					new string[]
				{
					"",
					"The bounding volume of the renderer (Read Only).",
					"This is the axis-aligned bounding box fully enclosing the object in world space.",
					"Using bounds is convenient to make rough approximations about the object's location",
					"and its extents. For example, the center property is often a more precise approximation",
					"to the center of the object than Transform.position, especially if the object is",
					"not symmetrical.",
					"Note that the Mesh.bounds property is similar but returns",
					"the bounds of the mesh in local space.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Renderer-bounds.html", 
					"");
				break;

			}
		}


		EnumInputComputeOutput [] EnumInputComputeOutput_Renderer ()
		{
			return new EnumInputComputeOutput[]
			{
				new EnumInputComputeOutput (
					ComputeRendererType.getSharedMaterialsArray.ToString (),
					"",
					"\t\t\t\tmaterialsListValue = ArrayToList (renderer.sharedMaterials);\n",
					"",
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.materialListValue,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ArrayToList_Sript_Material +
					ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"",
					},
					"",
					""),
				
				new EnumInputComputeOutput (
					ComputeRendererType.getMaterialsArray.ToString (),
					"",
					"\t\t\t\tmaterialsListValue = ArrayToList (renderer.materials);\n",
					"",
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.materialListValue,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ArrayToList_Sript_Material +
					ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"",
					},
					"",
					""),



				new EnumInputComputeOutput (
					ComputeRendererType.getSharedMaterial.ToString (),
					"",
					"\t\t\t\tmaterialValue = renderer.sharedMaterial;",
					InOutWs.OutWs.material_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.materialValue,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"The shared material of this object.",
						"Modifying sharedMaterial will change ",
						"the appearance of all objects using this material.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-sharedMaterial.html",
					""),

				new EnumInputComputeOutput (
					ComputeRendererType.setSharedMaterial.ToString (),
					InOutWs.InWs._material,
					"\t\t\t\trenderer.sharedMaterial = materialValues [0];",
					InOutWs.OutWs.gameObject_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.materialValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID, 
						Enums.materialValues_0_ID}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"The shared material of this object.",
						"Modifying sharedMaterial will change ",
						"the appearance of all objects using this material.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-sharedMaterial.html",
					""),
				
				new EnumInputComputeOutput (
					ComputeRendererType.getMaterial.ToString (),
					"",
					"\t\t\t\tmaterialValue = renderer.material;",
					InOutWs.OutWs.material_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.materialValue,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"The first instantiated Material assigned to the renderer.",
						"Modifying material will change the material for this object only.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-material.html",
					""),

				new EnumInputComputeOutput (
					ComputeRendererType.setMaterial.ToString (),
					InOutWs.InWs._material,
					"\t\t\t\trenderer.material = materialValues [0];",
					InOutWs.OutWs.gameObject_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.materialValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,
						Enums.materialValues_0_ID}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"The first instantiated Material assigned to the renderer.",
						"Modifying material will change the material for this object only.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-material.html",
					""),

				new EnumInputComputeOutput (
					ComputeRendererType.setProbeAnchor.ToString (),
					InOutWs.InWs._gameObject_1 + InOutWs.InWs._gameObject_1_check,
					"\t\t\t\trenderer.probeAnchor = gameObjectValues [1].transform;",
					InOutWs.OutWs.gameObject_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"If set, Renderer will use this Transform's position to find the light or reflection probe.",
						"Otherwise the center of Renderer's AABB will be used.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-probeAnchor.html",
					""),



				new EnumInputComputeOutput (
					ComputeRendererType.setLightmapIndex.ToString (),
					InOutWs.InWs._int,
					"\t\t\t\trenderer.lightmapIndex = intValues [0];",
					InOutWs.OutWs.gameObject_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.intValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}) +
					ExprWs.ConstructorExpr.IntValues (this),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"The index of the baked lightmap applied to this renderer.",
						"The index refers to the LightmapSettings.lightmaps array.",
						"A value of -1 (0xFFFF) means no lightmap has been assigned, which is the default.",
						"A value of 0xFFFE is internally used for objects that have their scale in lightmap set to 0;",
						"they affect lightmaps, but don't have a lightmap assigned themselves.",
						"The index is 16 bits internally and can't be larger than 65533 (0xFFFE).",
						"Note: this property is only serialized when building the player.",
						"In all the other cases it's the responsibility of the Unity lightmapping system",
						"(or a custom script that brings external lightmapping data) to set it",
						"when the scene loads or playmode is entered.",
						"A lightmap is a texture atlas",
						"and multiple Renderers can use different portions of the same lightmap.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-lightmapIndex.html",
					""),

				new EnumInputComputeOutput (
					ComputeRendererType.setEnabled.ToString (),
					InOutWs.InWs._bool,
					"\t\t\t\trenderer.enabled = boolValues [0];",
					InOutWs.OutWs.gameObject_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.boolValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}) +
					ExprWs.ConstructorExpr.BoolValues (this),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"Makes the rendered 3D object visible if enabled.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-enabled.html",
					""),



				new EnumInputComputeOutput (
					ComputeRendererType.getProbeAnchor.ToString (),
					"",
					"\t\t\t\tif (renderer.probeAnchor != null)\n\t\t\t\t{\n\t\t\t\t\tgameObjectValue = renderer.probeAnchor.gameObject;\n\t\t\t\t}",
					InOutWs.OutWs.gameObject_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"If set, Renderer will use this Transform's position to find the light or reflection probe.",
						"Otherwise the center of Renderer's AABB will be used.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-probeAnchor.html",
					""),

				new EnumInputComputeOutput (
					ComputeRendererType.getLocalToWorldMatrix.ToString (),
					"",
					CodeOfSetM44EntierAndValues ("renderer.localToWorldMatrix"),
					InOutWs.OutWs.m44_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.m44Value_entier + ExprWs.Gv.m44ValueAndProperties,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}),
					ExprWs.UMDecl.SetM44Value +
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"Matrix that transforms a point from local space into world space (Read Only).",
						"This property MUST be used instead of Transform.localToWorldMatrix,",
						"if you're setting shader parameters.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-localToWorldMatrix.html",
					""),

				new EnumInputComputeOutput (
					ComputeRendererType.getLightmapScaleOffset.ToString (),
					"",
					"\t\t\t\tvector4Value = renderer.lightmapScaleOffset;",
					InOutWs.OutWs.vector4_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.vector4Value,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"The UV scale & offset used for a lightmap.",
						"A lightmap is a texture atlas and multiple Renderers can use different portions of the same lightmap.",
						"The vector's x and y refer to UV scale, while z and w refer to UV offset.",
						"Note: this property is only serialized when building the player.",
						"In all the other cases it's the responsibility of the Unity lightmapping system",
						"(or a custom script that brings external lightmapping data) to set it when",
						"the scene loads or playmode is entered.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-lightmapScaleOffset.html",
					""),

				new EnumInputComputeOutput (
					ComputeRendererType.getLightmapIndex.ToString (),
					"",
					"\t\t\t\tintValue = renderer.lightmapIndex;",
					InOutWs.OutWs.int_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.intValue,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"The index of the baked lightmap applied to this renderer.",
						"The index refers to the LightmapSettings.lightmaps array.",
						"A value of -1 (0xFFFF) means no lightmap has been assigned, which is the default.",
						"A value of 0xFFFE is internally used for objects that have their scale in lightmap set to 0;",
						"they affect lightmaps, but don't have a lightmap assigned themselves.",
						"The index is 16 bits internally and can't be larger than 65533 (0xFFFE).",
						"Note: this property is only serialized when building the player.",
						"In all the other cases it's the responsibility of the Unity lightmapping system",
						"(or a custom script that brings external lightmapping data) to set it",
						"when the scene loads or playmode is entered.",
						"A lightmap is a texture atlas",
						"and multiple Renderers can use different portions of the same lightmap.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-lightmapIndex.html",
					""),

				new EnumInputComputeOutput (
					ComputeRendererType.getIsVisible.ToString (),
					"",
					"\t\t\t\tboolValue = renderer.isVisible;",
					InOutWs.OutWs.bool_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.boolValue,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"Is this renderer visible in any camera? (Read Only)",
						"Note that the object is considered visible when it needs to be rendered in the scene.",
						"For example, it might not actually be visible by any camera but still need",
						"to be rendered for shadows. When running in the editor, the scene view cameras",
						"will also cause this value to be true.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-isVisible.html",
					""),

				new EnumInputComputeOutput (
					ComputeRendererType.getIsPartOfStaticBatch.ToString (),
					"",
					"\t\t\t\tboolValue = renderer.isPartOfStaticBatch;",
					InOutWs.OutWs.bool_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.boolValue,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"Has this renderer been statically batched with any other renderers?",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-isPartOfStaticBatch.html",
					""),

				new EnumInputComputeOutput (
					ComputeRendererType.getEnabled.ToString (),
					"",
					"\t\t\t\tboolValue = renderer.enabled;",
					InOutWs.OutWs.bool_,
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.boolValue,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"Makes the rendered 3D object visible if enabled.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-enabled.html",
					""),

				new EnumInputComputeOutput (
					ComputeRendererType.getBounds.ToString (),
					"",
					"\t\t\t\tboundsCenterValue = renderer.bounds.center;\n\n\t\t\t\tboundsExtentsValue = renderer.bounds.extents;\n\n\t\t\t\tboundsMaxValue = renderer.bounds.max;\n\n\t\t\t\tboundsMinValue = renderer.bounds.min;\n\n\t\t\t\tboundsSizeValue = renderer.bounds.size;",
					"\t\t\t\tDrawBoundsResultField ();",
					ExprWs.Gv.renderer_doIt_identifiedObjects_gameObjectAll +
					ExprWs.Gv.bounds,
					ConstructorGetIdentifiedObject (new string [] {Enums.gameObjectValues_0_ID,}),
					ExprWs.UMDecl.gameObjectCheck + ExprWs.UMDecl.rendererCheck + ExprWs.UMDecl.computeRenderer,
					new string []
					{
						"The bounding volume of the renderer (Read Only).",
						"This is the axis-aligned bounding box fully enclosing the object in world space.",
						"Using bounds is convenient to make rough approximations about the object's location",
						"and its extents. For example, the center property is often a more precise approximation",
						"to the center of the object than Transform.position, especially if the object is",
						"not symmetrical.",
						"Note that the Mesh.bounds property is similar but returns",
						"the bounds of the mesh in local space.",
					},
					"https://docs.unity3d.com/ScriptReference/Renderer-bounds.html",
					""),
			};
		}
		#endregion componentRenderer
	
		#region variable_material
		void ApplyComputeMaterial ()
		{
			ComputeMaterial_InputFields ();
			if (logic.playing)ComputeMaterial ();
			ComputeMaterial_OutputFields ();
		}
		void ComputeMaterial_InputFields ()
		{
			linkedOrAttachedTo = materialValues [0] == null;

			if (computeMaterialType != ComputeMaterialType.get)
				DrawMaterialFieldInput (0);

			if (computeMaterialType != ComputeMaterialType.get)
			if ( ! linkedOrAttachedTo)
			{
				if (materialValues [0] == null)
				{
					DrawInNodeInfo ("Fill in the Material field");

					computeMaterialType = (ComputeMaterialType)DrawEnumComputeType (computeMaterialType,
						ref computeMaterialType_length, ref computeMaterialType_last, typeof (ComputeMaterialType));

					return;
				}

			}
			computeMaterialType = (ComputeMaterialType)DrawEnumComputeType (computeMaterialType,
				ref computeMaterialType_length, ref computeMaterialType_last, typeof (ComputeMaterialType));

			switch (computeMaterialType)
			{
			case ComputeMaterialType.getSetShaderPropertes:
				GetSetShaderProperties_Input ();
				break;
#if !UNITY_2018
			case ComputeMaterialType.ProceduralMaterialSetProceduralIntEnumAndRebuild:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				DrawLogicNodeLabel ("Int Value To set", 0, 2);
				DrawIntInputField (0, 1, 2);
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralIntEnum:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				DrawLogicNodeLabel ("Int Value To set", 0, 2);
				DrawIntInputField (0, 1, 2);
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralIntEnum:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				break;
#endif
			case ComputeMaterialType.listenToTransferredData:
				DrawInNodeInfo ("Default value: in the above material field");
				DrawStringListMenuToString_0 (
					"Data Name", MezanixDiamondMaterialNames (), noTransferredDataFound, 0, 2);
				DrawLogicNodeLabel (stringValues [0], 1, 2);
				DrawLogicNodeLabel ("Consume data?", 0, 2);
				DrawBoolInputField (0, 3, 4);

				break;

			case ComputeMaterialType.sendMeAsTransferredData:
				DrawLogicNodeLabel ("Data Name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				DrawInNodeInfo ("Play logic, name data, click doIT");

				break;

			case ComputeMaterialType.getName:

				break;

			case ComputeMaterialType.setName:
				DrawStringInputField (0, stringInputFieldForWhat.general);
				break;
#if !UNITY_2018
			case ComputeMaterialType.ProceduralMaterialSetProceduralVectorAndRebuild:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				DrawLogicNodeLabel ("Vector Value To set");
				DrawVector4InputField (0);
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralVector:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				DrawLogicNodeLabel ("Vector Value To set");
				DrawVector4InputField (0);
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralFloatAndRebuild:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				DrawLogicNodeLabel ("Value To set", 0, 2);
				DrawFloatInputField (0, 1, 2);
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralFloat:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				DrawLogicNodeLabel ("Value To set", 0, 2);
				DrawFloatInputField (0, 1, 2);
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralColorAndRebuild:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				DrawLogicNodeLabel ("Value To set", 0, 2);
				DrawColorInputField (0, 1, 2);
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralColor:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				DrawLogicNodeLabel ("Value To set", 0, 2);
				DrawColorInputField (0, 1, 2);
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralBooleanAndRebuild:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				DrawLogicNodeLabel ("Bool Value To set", 0, 2);
				DrawBoolInputField (0, 3, 4);
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralBoolean:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				DrawLogicNodeLabel ("Bool Value To set", 0, 2);
				DrawBoolInputField (0, 3, 4);
				break;

			case ComputeMaterialType.ProceduralMaterialRebuildTexturesImmediately:

				break;

			case ComputeMaterialType.ProceduralMaterialRebuildTextures:

				break;

			case ComputeMaterialType.ProceduralMaterialGetIsProcessing:

				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralVector:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralFloat:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralColor:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralBoolean:
				DrawLogicNodeLabel ("Property name", 0, 2);
				DrawStringInputField (0, stringInputFieldForWhat.general, 1, 2);
				break;

			case ComputeMaterialType.ProceduralMaterialSetAnimationUpdateRate:
				DrawIntInputField (0);
				break;

			case ComputeMaterialType.ProceduralMaterialGetAnimationUpdateRate:

				break;
#endif
			case ComputeMaterialType.get:
				DrawLogicNodeLabel ("Get from input", 0, 2);
				DrawBoolInputField (0, 3, 4);
				ForGet_material_DrawInputs ();
				break;

			case ComputeMaterialType.setShader:
				DrawShaderFieldInput (0);
				break;

			case ComputeMaterialType.setRenderQueue:
				DrawIntInputField (0);
				break;

			case ComputeMaterialType.setMainTextureTiling:
				DrawVector2InputField (0);
				break;

			case ComputeMaterialType.setMainTextureOffset:
				DrawVector2InputField (0);
				break;

			case ComputeMaterialType.setMainTexture:
				DrawTexture2DFieldInput (0);
				break;

			case ComputeMaterialType.setMainColor:
				DrawColorInputField (0);
				break;

			case ComputeMaterialType.setGlobalIlluminationFlags:
				DrawMaterialGlobalIlluminationFlagsEnum ();
				break;

			case ComputeMaterialType.getShader:

				break;

			case ComputeMaterialType.getRenderQueue:

				break;

			case ComputeMaterialType.getPassCount:

				break;

			case ComputeMaterialType.getMainTextureTiling:

				break;

			case ComputeMaterialType.getMainTextureOffset:

				break;

			case ComputeMaterialType.getMainTexture:

				break;

			case ComputeMaterialType.getMainColor:

				break;

			}
			if (computeMaterialType == ComputeMaterialType.get)
			{
				doIT = true;
			}
			else
			{
				DrawDoItButton();
			}
		}
		void ComputeMaterial ()
		{
			bool notDataTransfer = ! (computeMaterialType == ComputeMaterialType.sendMeAsTransferredData ||
				computeMaterialType == ComputeMaterialType.listenToTransferredData);
			if (notDataTransfer)
			if (linkedOrAttachedTo)
				return;			if ( ! doIT)
			{
				return;
			}
			switch (computeMaterialType)
			{
			case ComputeMaterialType.getSetShaderPropertes:
				GetSetShaderProperties ();
				break;
#if !UNITY_2018
				case ComputeMaterialType.ProceduralMaterialSetProceduralIntEnumAndRebuild:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.SetProceduralEnum (stringValues [0], intValues [0]);

						proceduralMaterial.RebuildTexturesImmediately ();
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralIntEnum:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.SetProceduralEnum (stringValues [0], intValues [0]);
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralIntEnum:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						intValue = proceduralMaterial.GetProceduralEnum (stringValues [0]);

						ProceduralPropertyDescription [] ppds = proceduralMaterial.GetProceduralPropertyDescriptions ();

						for (int i = 0; i < ppds.Length; i++)
						{
							if (ppds [i].type == ProceduralPropertyType.Enum)
							{
								if (ppds [i].name == stringValues [0])
								{
									if (intValue > -1)
									{
										stringValue = ppds [i].enumOptions [intValue];

										break;
									}
									else
									{
										stringValue = "";
									}
								}
								else
								{
									stringValue = "";
								}
							}
							else
							{
								stringValue = "";
							}
						}
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;
#endif
				case ComputeMaterialType.listenToTransferredData:
				materialValue = MezanixDiamondGetMaterial (stringValues [0]);
				if (boolValues [0])
				{
					MezanixDiamondRemoveMaterial (stringValues [0]);
				}
				break;

			case ComputeMaterialType.sendMeAsTransferredData:
				MezanixDiamondSetMaterial (stringValues [0]);

				break;

			case ComputeMaterialType.getName:
				if (materialValues [0] != null)
				{
					stringValue = materialValues [0].name;
				}

				break;

			case ComputeMaterialType.setName:
				if (materialValues [0] != null)
				{
					materialValues [0].name = stringValues [0];

					materialValue = materialValues [0];
				}

				break;
#if !UNITY_2018
				case ComputeMaterialType.ProceduralMaterialSetProceduralVectorAndRebuild:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.SetProceduralVector (stringValues [0], vector4Values [0]);

						proceduralMaterial.RebuildTexturesImmediately ();
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralVector:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.SetProceduralVector (stringValues [0], vector4Values [0]);
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralFloatAndRebuild:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.SetProceduralFloat (stringValues [0], floatValues [0]);

						proceduralMaterial.RebuildTexturesImmediately ();
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralFloat:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.SetProceduralFloat (stringValues [0], floatValues [0]);
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralColorAndRebuild:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.SetProceduralColor (stringValues [0], colorValues [0]);

						proceduralMaterial.RebuildTexturesImmediately ();
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralColor:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.SetProceduralColor (stringValues [0], colorValues [0]);
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralBooleanAndRebuild:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.SetProceduralBoolean (stringValues [0], boolValues [0]);

						proceduralMaterial.RebuildTexturesImmediately ();
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralBoolean:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.SetProceduralBoolean (stringValues [0], boolValues [0]);
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialRebuildTexturesImmediately:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.RebuildTexturesImmediately ();
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialRebuildTextures:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.RebuildTextures ();
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialGetIsProcessing:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						boolValue = proceduralMaterial.isProcessing;
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralVector:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						vector4Value = proceduralMaterial.GetProceduralVector (stringValues [0]);
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralFloat:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						floatValue = proceduralMaterial.GetProceduralFloat (stringValues [0]);
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralColor:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						colorValue = proceduralMaterial.GetProceduralColor (stringValues [0]);
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralBoolean:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						boolValue = proceduralMaterial.GetProceduralBoolean (stringValues [0]);
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialSetAnimationUpdateRate:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						proceduralMaterial.animationUpdateRate = intValues [0];
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;

			case ComputeMaterialType.ProceduralMaterialGetAnimationUpdateRate:
				if (materialValues [0] != null)
				{
					if (materialValues [0].GetType () == typeof (ProceduralMaterial))
					{
						ProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];

						intValue = proceduralMaterial.animationUpdateRate;
					}
					else
					{
						Debug.LogWarning ("The material: " + materialValues [0].name + " is not procedural");
					}
				}
				break;
#endif
				case ComputeMaterialType.get:
				ForGet_material_Compute ();
				break;

			case ComputeMaterialType.setShader:
				if (materialValues [0] != null)
				{
					materialValues [0].shader = shaderValues [0];

					materialValue = materialValues [0];
				}
				break;

			case ComputeMaterialType.setRenderQueue:
				if (materialValues [0] != null)
				{
					materialValues [0].renderQueue = intValues [0];

					materialValue = materialValues [0];
				}
				break;

			case ComputeMaterialType.setMainTextureTiling:
				if (materialValues [0] != null)
				{
					materialValues [0].mainTextureScale = vector2Values [0];

					materialValue = materialValues [0];
				}
				break;

			case ComputeMaterialType.setMainTextureOffset:
				if (materialValues [0] != null)
				{
					materialValues [0].mainTextureOffset = vector2Values [0];

					materialValue = materialValues [0];
				}
				break;

			case ComputeMaterialType.setMainTexture:
				if (materialValues [0] != null)
				{
					materialValues [0].mainTexture = texture2DValues [0];

					materialValue = materialValues [0];
				}
				break;

			case ComputeMaterialType.setMainColor:
				if (materialValues [0] != null)
				{
					materialValues [0].color = colorValues [0];

					materialValue = materialValues [0];
				}
				break;

			case ComputeMaterialType.setGlobalIlluminationFlags:
				if (materialValues [0] != null)
				{
					materialValues [0].globalIlluminationFlags = materialGlobalIlluminationFlags;

					materialValue = materialValues [0];
				}
				break;

			case ComputeMaterialType.getShader:
				if (materialValues [0] != null)
				{
					shaderValue = materialValues [0].shader;
				}
				break;

			case ComputeMaterialType.getRenderQueue:
				if (materialValues [0] != null)
				{
					intValue = materialValues [0].renderQueue;
				}
				break;

			case ComputeMaterialType.getPassCount:
				if (materialValues [0] != null)
				{
					intValue = materialValues [0].passCount;
				}
				break;

			case ComputeMaterialType.getMainTextureTiling:
				if (materialValues [0] != null)
				{
					vector2Value = materialValues [0].mainTextureScale;
				}
				break;

			case ComputeMaterialType.getMainTextureOffset:
				if (materialValues [0] != null)
				{
					vector2Value = materialValues [0].mainTextureOffset;
				}
				break;

			case ComputeMaterialType.getMainTexture:
				if (materialValues [0] != null)
				{
					texture2DValue = (Texture2D)materialValues [0].mainTexture;
				}
				break;

			case ComputeMaterialType.getMainColor:
				if (materialValues [0] != null)
				{
					colorValue = materialValues [0].color;
				}
				break;

			}
		}
		void ComputeMaterial_OutputFields ()
		{

			string [] documentationMessage;

			switch (computeMaterialType)
			{
			case ComputeMaterialType.getSetShaderPropertes:
				GetSetShaderProperties_Outputs ();
				break;
#if !UNITY_2018
			case ComputeMaterialType.ProceduralMaterialSetProceduralIntEnumAndRebuild:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set a named Procedural int (enum) property.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralEnum.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralIntEnum:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set a named Procedural int (enum) property.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralEnum.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralIntEnum:
				DrawLogicNodeLabel ("enum index", 0, 2);
				DrawIntResultField (true, 1, 2);

				DrawLogicNodeLabel ("enum option", 0, 2);
				DrawStringResultField (true, 1, 2);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Get a named Procedural enum (int) property",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.GetProceduralEnum.html", 
					"");
				break;
#endif
			case ComputeMaterialType.listenToTransferredData:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Listen to (read) a value by its name.",
					"If you want to read this value at this moment",
					"and no need to read it later, it's recommended",
					"to consume it.",
					"",
					"If the transferred data is not found,",
					"the default value will be used",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"", 
					"");
				break;

			case ComputeMaterialType.sendMeAsTransferredData:


				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Send a value by its name. The name can be used",
					"by another graph (script) to listen to (read) this value.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"", 
					"");
				break;

			case ComputeMaterialType.getName:
				DrawStringResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The name of the object (material).",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Object-name.html", 
					"");
				break;

			case ComputeMaterialType.setName:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The name of the object (material).",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Object-name.html", 
					"");
				break;
#if !UNITY_2018
			case ComputeMaterialType.ProceduralMaterialSetProceduralVectorAndRebuild:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set a named Procedural vector property.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralVector.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralVector:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set a named Procedural vector property.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralVector.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralFloatAndRebuild:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set a named Procedural float property.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralFloat.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralFloat:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set a named Procedural float property.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralFloat.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralColorAndRebuild:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set a named Procedural color property.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralColor.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralColor:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set a named Procedural color property.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralColor.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralBooleanAndRebuild:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set a named Procedural boolean property.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralBoolean.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialSetProceduralBoolean:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set a named Procedural boolean property.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralBoolean.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialRebuildTexturesImmediately:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Triggers an immediate (synchronous) rebuild of this ProceduralMaterial's dirty textures.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.RebuildTexturesImmediately.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialRebuildTextures:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Triggers an asynchronous rebuild of this ProceduralMaterial's dirty textures.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.RebuildTextures.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialGetIsProcessing:
				DrawBoolResultField ();

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Check if the ProceduralTextures from this ProceduralMaterial",
					"are currently being rebuilt.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial-isProcessing.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralVector:
				DrawVector4ResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Get a named Procedural vector property",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.GetProceduralVector.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralFloat:
				DrawFloatResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Get a named Procedural float property",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.GetProceduralFloat.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralColor:
				DrawColorResultField ();

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Get a named Procedural color property",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.GetProceduralColor.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialGetProceduralBoolean:
				DrawBoolResultField ();

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Get a named Procedural boolean property",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.GetProceduralBoolean.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialSetAnimationUpdateRate:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set or get the update rate in millisecond of the animated substance.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial-animationUpdateRate.html", 
					"");
				break;

			case ComputeMaterialType.ProceduralMaterialGetAnimationUpdateRate:
				DrawIntResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Set or get the update rate in millisecond of the animated substance.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial-animationUpdateRate.html", 
					"");
				break;
#endif
			case ComputeMaterialType.get:
				ForGet_material_DrawOutputs ();

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Get this",
					"",
					"If you choose 'Get from input'",
					"you can get from the input list that",
					"is turnable to public, so it can appear",
					"in the inspector of the game object holding",
					"the genrated script",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"", 
					"");
				break;

			case ComputeMaterialType.setShader:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The shader used by the material.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-shader.html", 
					"");
				break;

			case ComputeMaterialType.setRenderQueue:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Render queue of this material.",
					"By default materials use render queue of the shader it uses.",
					"You can override the render queue used using this variable. Note that if a shader on the material is changed,",
					"the render queue resets to that of the shader itself.",
					"Render queue value should be in [0..5000] range to work properly; or -1 to use the render queue from the shader.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-renderQueue.html", 
					"");
				break;

			case ComputeMaterialType.setMainTextureTiling:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The texture scale of the main texture.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-mainTextureScale.html", 
					"");
				break;

			case ComputeMaterialType.setMainTextureOffset:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The texture offset of the main texture.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-mainTextureOffset.html", 
					"");
				break;

			case ComputeMaterialType.setMainTexture:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The material's texture.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-mainTexture.html", 
					"");
				break;

			case ComputeMaterialType.setMainColor:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The main material's color.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-color.html", 
					"");
				break;

			case ComputeMaterialType.setGlobalIlluminationFlags:
				DrawMaterialResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"How the material interacts with lightmaps and lightprobes.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/MaterialGlobalIlluminationFlags.html", 
					"");
				break;

			case ComputeMaterialType.getShader:
				DrawShaderResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The shader used by the material.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-shader.html", 
					"");
				break;

			case ComputeMaterialType.getRenderQueue:
				DrawIntResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"Render queue of this material.",
					"By default materials use render queue of the shader it uses.",
					"You can override the render queue used using this variable. Note that if a shader on the material is changed,",
					"the render queue resets to that of the shader itself.",
					"Render queue value should be in [0..5000] range to work properly; or -1 to use the render queue from the shader.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-renderQueue.html", 
					"");
				break;

			case ComputeMaterialType.getPassCount:
				DrawIntResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"How many passes are in this material",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-passCount.html", 
					"");
				break;

			case ComputeMaterialType.getMainTextureTiling:
				DrawVector2ResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The texture scale of the main texture.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-mainTextureScale.html", 
					"");
				break;

			case ComputeMaterialType.getMainTextureOffset:
				DrawVector2ResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The texture offset of the main texture.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-mainTextureOffset.html", 
					"");
				break;

			case ComputeMaterialType.getMainTexture:
				DrawTexture2DResultField (true);

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The material's texture.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-mainTexture.html", 
					"");
				break;

			case ComputeMaterialType.getMainColor:
				DrawColorResultField ();

				documentationMessage = 
					new string[]
				{
					"",
					"",
					"The main material's color.",
				};

				DrawDocumentationBoxUpRight (documentationMessage);
				DrawDocumentationUrlButtons (documentationMessage, 
					"https://docs.unity3d.com/ScriptReference/Material-color.html", 
					"");
				break;

			}
		}


		EnumInputComputeOutput [] EnumInputComputeOutput_Material ()
		{
			return new EnumInputComputeOutput[]
			{
				#if !UNITY_2018
				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialSetProceduralIntEnumAndRebuild.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawLogicNodeLabel (\"Vector Value To set\");" + ret +
					InOutWs.InWs._vector4,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.SetProceduralEnum (stringValues [0], intValues [0]);\n\n\t\t\t\t\t\tproceduralMaterial.RebuildTexturesImmediately ();\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}\n",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.stringValues +
					ExprWs.Gv.intValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.IntValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"",
					},
					"",
					""),
#endif
				new EnumInputComputeOutput (
					ComputeMaterialType.getSetShaderPropertes.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawLogicNodeLabel (\"Vector Value To set\");" + ret +
					InOutWs.InWs._vector4,
					"\t\t\t\tif (materialValues [0] == null)\n\t\t\t\t\treturn;\n\n\t\t\t\tswitch (getSetShaderPropertiesEnum)\n\t\t\t\t{\n\t\t\t\tcase GetSetShaderPropertiesEnum.GetColor:\n\t\t\t\t\tcolorValue = materialValues [0].GetColor (stringValues [0]);\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.GetFloat:\n\t\t\t\t\tfloatValue = materialValues [0].GetFloat (stringValues [0]);\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.GetInt:\n\t\t\t\t\tintValue = materialValues [0].GetInt (stringValues [0]);\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.GetTexture:\n\t\t\t\t\ttexture2DValue = (Texture2D)materialValues [0].GetTexture (stringValues [0]);\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.GetTextureOffset:\n\t\t\t\t\tvector2Value = materialValues [0].GetTextureOffset (stringValues [0]);\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.GetTextureScale:\n\t\t\t\t\tvector2Value = materialValues [0].GetTextureScale (stringValues [0]);\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.GetVector:\n\t\t\t\t\tvector4Value = materialValues [0].GetVector (stringValues [0]);\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.HasProperty:\n\t\t\t\t\tboolValue = materialValues [0].HasProperty (stringValues [0]);\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.SetColor:\n\t\t\t\t\tmaterialValues [0].SetColor (stringValues [0], colorValues [0]);\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.SetFloat:\n\t\t\t\t\tmaterialValues [0].SetFloat (stringValues [0], floatValues [0]);\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.SetInt:\n\t\t\t\t\tmaterialValues [0].SetInt (stringValues [0], intValues [0]);\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.SetTexture:\n\t\t\t\t\tmaterialValues [0].SetTexture (stringValues [0], texture2DValues [0]);\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.SetTextureOffset:\n\t\t\t\t\tmaterialValues [0].SetTextureOffset (stringValues [0], vector2Values [0]);\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.SetTextureScale:\n\t\t\t\t\tmaterialValues [0].SetTextureScale (stringValues [0], vector2Values [0]);\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t\tbreak;\n\n\t\t\t\tcase GetSetShaderPropertiesEnum.SetVector:\n\t\t\t\t\tmaterialValues [0].SetVector (stringValues [0], vector4Values [0]);\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t\tbreak;\n\t\t\t\t}\n",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.stringValues +
					ExprWs.Gv.intAll + ExprWs.Gv.colorAll + ExprWs.Gv.floatAll + ExprWs.Gv.boolValue +
					ExprWs.Gv.texture2DAll + ExprWs.Gv.vector4All + ExprWs.Gv.vector2All +
					"\t\tpublic enum GetSetShaderPropertiesEnum\n\t\t{\n\t\t\tHasProperty,\n\n\t\t\tGetColor,\n\n\t\t\tGetFloat,\n\n\t\t\tGetInt,\n\n\t\t\tGetTexture,\n\n\t\t\tGetTextureOffset,\n\n\t\t\tGetTextureScale,\n\n\t\t\tGetVector,\n\n\t\t\tSetColor,\n\n\t\t\tSetFloat,\n\n\t\t\tSetInt,\n\n\t\t\tSetTexture,\n\n\t\t\tSetTextureOffset,\n\n\t\t\tSetTextureScale,\n\n\t\t\tSetVector,\n\t\t}\n\t\tpublic GetSetShaderPropertiesEnum getSetShaderPropertiesEnum;\n",
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.IntValues (this) +
					ExprWs.ConstructorExpr.ColorValues (this) + ExprWs.ConstructorExpr.FloattValues (this) +
					ConstructorGetIdentifiedObject (new string [] {Enums.texture2DValues_0_ID,}) + 
					ExprWs.ConstructorExpr.Vector4Values (this) + ExprWs.ConstructorExpr.Vector2Values (this) +
					"\t\t\tgetSetShaderPropertiesEnum = GetSetShaderPropertiesEnum." + getSetShaderPropertiesEnum.ToString () + ";\n",
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"",
					},
					"",
					""),
#if !UNITY_2018
				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialSetProceduralIntEnum.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawLogicNodeLabel (\"Vector Value To set\");" + ret +
					InOutWs.InWs._vector4,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.SetProceduralEnum (stringValues [0], intValues [0]);\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}\n",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.stringValues +
					ExprWs.Gv.intValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.IntValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"",
					},
					"",
					""),
#endif

				new EnumInputComputeOutput (
					ComputeMaterialType.listenToTransferredData.ToString (),
					"\t\t\t\tDrawInNodeInfo (\"Default value: in the above material field\");\n" +
					"\t\t\t\tDrawStringListMenuToString_0 (\"Data Name\", " +
					"MezanixDiamondMaterialNames (), noTransferredDataFound);\n" +
					"\t\t\t\tDrawLogicNodeLabel (stringValues [0]);\n" +
					"\t\t\t\tDrawLogicNodeLabel (\"Consume the data?\");\n" +
					InOutWs.InWs._bool + ret,
					"\t\t\t\t" + "materialValue = MezanixDiamondGetMaterial (stringValues [0]);\n" +
					"\t\t\t\tif (boolValues [0])\n\t\t\t\t{\n" +
					"\t\t\t\t\t" + "MezanixDiamondRemoveMaterial (stringValues [0]);\n\t\t\t\t}",
					//"\t\t\t\tDrawInNodeInfo (workOnlyOnGeneratedScripts);\n
					InOutWs.OutWs.material_,
					ExprWs.Gv.stringValues + ExprWs.Gv.boolValues + ExprWs.Gv.doIt +
					ExprWs.Gv.mddtGameObjectHolder + ExprWs.Gv.materialAll +
					ExprWs.Gv.identifiedObjects,
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.BoolValues (this) + 
					ExprWs.ConstructorExpr.mddtGameObjectHolder + 
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.getmddt +
					ExprWs.UMDecl.MezanixDiamondGetMaterial + 
					ExprWs.UMDecl.MezanixDiamondRemoveMaterial +
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Listen to (read) a value by its name.",
						"If you want to read this value at this moment",
						"and no need to read it later, it's recommended",
						"to consume it.",
						"",
						"If the transferred data is not found,",
						"the default value will be used",
					},
					"",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.sendMeAsTransferredData.ToString (),
					"DrawLogicNodeLabel (\"Data Name\");\n" +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawInNodeInfo (\"Play logic, name data, click doIT\");\n",
					"\t\t\t\tMezanixDiamondSetMaterial (stringValues [0]);\n", 
					//"\t\t\t\tDrawInNodeInfo (workOnlyOnGeneratedScripts);\n" + 
					"",
					ExprWs.Gv.stringValues + ExprWs.Gv.materialValues + ExprWs.Gv.doIt +
					ExprWs.Gv.mddtGameObjectHolder + ExprWs.Gv.identifiedObjects,
					ExprWs.ConstructorExpr.StringValues (this) + 
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.mddtGameObjectHolder,
					ExprWs.UMDecl.getmddt +
					ExprWs.UMDecl.MezanixDiamondSetMaterial + ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Send a value by its name. The name can be used",
						"by another graph (script) to listen to (read) this value.",
					},
					"",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.getName.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tstringValue = materialValues [0].name;\n\t\t\t\t}\n",
					InOutWs.OutWs.string_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + 
					ExprWs.Gv.stringValue,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The name of the object (material).",
					},
					"https://docs.unity3d.com/ScriptReference/Object-name.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.setName.ToString (),
					InOutWs.InWs._string,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tmaterialValues [0].name = stringValues [0];\n\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t}\n",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + 
					ExprWs.Gv.stringValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The name of the object (material).",
					},
					"https://docs.unity3d.com/ScriptReference/Object-name.html",
					""),
				#if !UNITY_2018
				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialSetProceduralVectorAndRebuild.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawLogicNodeLabel (\"Vector Value To set\");" + ret +
					InOutWs.InWs._vector4,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.SetProceduralVector (stringValues [0], vector4Values [0]);\n\n\t\t\t\t\t\tproceduralMaterial.RebuildTexturesImmediately ();\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.stringValues +
					ExprWs.Gv.vector4Values,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.Vector4Values (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Set a named Procedural vector property.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralVector.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialSetProceduralVector.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawLogicNodeLabel (\"Vector Value To set\");" + ret +
					InOutWs.InWs._vector4,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.SetProceduralVector (stringValues [0], vector4Values [0]);\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.stringValues +
					ExprWs.Gv.vector4Values,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.Vector4Values (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Set a named Procedural vector property.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralVector.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialSetProceduralFloatAndRebuild.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawLogicNodeLabel (\"Float Value To set\");" + ret +
					InOutWs.InWs._float,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.SetProceduralFloat (stringValues [0], floatValues [0]);\n\n\t\t\t\t\t\tproceduralMaterial.RebuildTexturesImmediately ();\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.stringValues +
					ExprWs.Gv.floatValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.FloattValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Set a named Procedural float property.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralFloat.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialSetProceduralFloat.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawLogicNodeLabel (\"Float Value To set\");" + ret +
					InOutWs.InWs._float,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.SetProceduralFloat (stringValues [0], floatValues [0]);\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.stringValues +
					ExprWs.Gv.floatValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.FloattValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Set a named Procedural float property.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralFloat.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialSetProceduralColorAndRebuild.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawLogicNodeLabel (\"Color Value To set\");" + ret +
					InOutWs.InWs._color,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.SetProceduralColor (stringValues [0], colorValues [0]);\n\n\t\t\t\t\t\tproceduralMaterial.RebuildTexturesImmediately ();\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.stringValues +
					ExprWs.Gv.colorValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.ColorValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Set a named Procedural color property.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralColor.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialSetProceduralColor.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawLogicNodeLabel (\"Color Value To set\");" + ret +
					InOutWs.InWs._color,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.SetProceduralColor (stringValues [0], colorValues [0]);\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.stringValues +
					ExprWs.Gv.colorValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.ColorValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Set a named Procedural color property.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralColor.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialSetProceduralBooleanAndRebuild.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawLogicNodeLabel (\"Bool Value To set\");" + ret +
					InOutWs.InWs._bool,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.SetProceduralBoolean (stringValues [0], boolValues [0]);\n\n\t\t\t\t\t\tproceduralMaterial.RebuildTexturesImmediately ();\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.stringValues +
					ExprWs.Gv.boolValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.BoolValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Set a named Procedural boolean property.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralBoolean.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialSetProceduralBoolean.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string + ret +
					"\t\t\t\tDrawLogicNodeLabel (\"Bool Value To set\");" + ret +
					InOutWs.InWs._bool,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.SetProceduralBoolean (stringValues [0], boolValues [0]);\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.stringValues +
					ExprWs.Gv.boolValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.StringValues (this) + ExprWs.ConstructorExpr.BoolValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Set a named Procedural boolean property.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.SetProceduralBoolean.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialRebuildTexturesImmediately.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.RebuildTexturesImmediately ();\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Triggers an immediate (synchronous) rebuild of this ProceduralMaterial's dirty textures.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.RebuildTexturesImmediately.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialRebuildTextures.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.RebuildTextures ();\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Triggers an asynchronous rebuild of this ProceduralMaterial's dirty textures.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.RebuildTextures.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialGetIsProcessing.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tboolValue = proceduralMaterial.isProcessing;\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.bool_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.boolValue,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Check if the ProceduralTextures from this ProceduralMaterial",
						"are currently being rebuilt.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial-isProcessing.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialGetProceduralIntEnum.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tintValue = proceduralMaterial.GetProceduralEnum (stringValues [0]);\n\n\t\t\t\t\t\tProceduralPropertyDescription [] ppds = proceduralMaterial.GetProceduralPropertyDescriptions ();\n\n\t\t\t\t\t\tfor (int i = 0; i < ppds.Length; i++)\n\t\t\t\t\t\t{\n\t\t\t\t\t\t\tif (ppds [i].type == ProceduralPropertyType.Enum)\n\t\t\t\t\t\t\t{\n\t\t\t\t\t\t\t\tif (ppds [i].name == stringValues [0])\n\t\t\t\t\t\t\t\t{\n\t\t\t\t\t\t\t\t\tif (intValue > -1)\n\t\t\t\t\t\t\t\t\t{\n\t\t\t\t\t\t\t\t\t\tstringValue = ppds [i].enumOptions [intValue];\n\n\t\t\t\t\t\t\t\t\t\tbreak;\n\t\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t\t\telse\n\t\t\t\t\t\t\t\t\t{\n\t\t\t\t\t\t\t\t\t\tstringValue = \"\";\n\t\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t\telse\n\t\t\t\t\t\t\t\t{\n\t\t\t\t\t\t\t\t\tstringValue = \"\";\n\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\telse\n\t\t\t\t\t\t\t{\n\t\t\t\t\t\t\t\tstringValue = \"\";\n\t\t\t\t\t\t\t}\n\t\t\t\t\t\t}\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}\n",
					InOutWs.OutWs.vector4_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.intValue +
					ExprWs.Gv.stringAll,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) + 
					ExprWs.ConstructorExpr.StringValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"",
					},
					"",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialGetProceduralVector.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tvector4Value = proceduralMaterial.GetProceduralVector (stringValues [0]);\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.vector4_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.vector4Value +
					ExprWs.Gv.stringValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) + 
					ExprWs.ConstructorExpr.StringValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Get a named Procedural vector property",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.GetProceduralVector.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialGetProceduralFloat.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tfloatValue = proceduralMaterial.GetProceduralFloat (stringValues [0]);\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.float_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.floatValue +
					ExprWs.Gv.stringValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) + 
					ExprWs.ConstructorExpr.StringValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Get a named Procedural float property",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.GetProceduralFloat.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialGetProceduralColor.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tcolorValue = proceduralMaterial.GetProceduralColor (stringValues [0]);\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.color_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.colorValue +
					ExprWs.Gv.stringValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) + 
					ExprWs.ConstructorExpr.StringValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Get a named Procedural color property",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.GetProceduralColor.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialGetProceduralBoolean.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Property name\");" + ret +
					InOutWs.InWs._string,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tboolValue = proceduralMaterial.GetProceduralBoolean (stringValues [0]);\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.bool_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.boolValue +
					ExprWs.Gv.stringValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) + 
					ExprWs.ConstructorExpr.StringValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Get a named Procedural boolean property",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial.GetProceduralBoolean.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialSetAnimationUpdateRate.ToString (),
					InOutWs.InWs._int,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tproceduralMaterial.animationUpdateRate = intValues [0];\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.intValues,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.IntValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Set or get the update rate in millisecond of the animated substance.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial-animationUpdateRate.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.ProceduralMaterialGetAnimationUpdateRate.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tif (materialValues [0].GetType () == typeof (ProceduralMaterial))\n\t\t\t\t\t{\n\t\t\t\t\t\tProceduralMaterial proceduralMaterial = (ProceduralMaterial)materialValues [0];\n\n\t\t\t\t\t\tintValue = proceduralMaterial.animationUpdateRate;\n\t\t\t\t\t}\n\t\t\t\t\telse\n\t\t\t\t\t{\n\t\t\t\t\t\tDebug.LogWarning (\"The material: \" + materialValues [0].name + \" is not procedural\");\n\t\t\t\t\t}\n\t\t\t\t}",
					InOutWs.OutWs.int_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.intValue,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Set or get the update rate in millisecond of the animated substance.",
					},
					"https://docs.unity3d.com/ScriptReference/ProceduralMaterial-animationUpdateRate.html",
					""),
#endif
				new EnumInputComputeOutput (
					ComputeMaterialType.get.ToString (),
					"\t\t\t\tDrawLogicNodeLabel (\"Get from input\");" + "\n" +
					InOutWs.InWs._bool + "\n" +
					"\t\t\t\tForGet_material_DrawInputs ();",
					"\t\t\t\tForGet_material_Compute ();",
					"\t\t\t\tForGet_material_DrawOutputs ();",
					ExprWs.Gv.materialAll + 
					ExprWs.Gv.boolValues +
					ExprWs.Gv.identifiedObjects,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValue_ID,}) +
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ExprWs.ConstructorExpr.BoolValues (this),
					ExprWs.UMDecl.ForGet_material_Compute +
					ExprWs.UMDecl.computeMaterial_NoDoIt,
					new string []
					{
						"",
						"Get this",
						"",
						"If you choose 'Get from input'",
						"you can get from the input list that",
						"is turnable to public, so it can appear",
						"in the inspector of the game object holding",
						"the genrated script",
					},
					"",
					""),


				new EnumInputComputeOutput (
					ComputeMaterialType.setShader.ToString (),
					InOutWs.InWs._shader,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tmaterialValues [0].shader = shaderValues [0];\n\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.shaderValues,
					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) +
					ConstructorGetIdentifiedObject (new string [] {Enums.shaderValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The shader used by the material.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-shader.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.setRenderQueue.ToString (),
					InOutWs.InWs._int,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tmaterialValues [0].renderQueue = intValues [0];\n\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.intValues,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) + 
					ExprWs.ConstructorExpr.IntValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Render queue of this material.",
						"By default materials use render queue of the shader it uses.",
						"You can override the render queue used using this variable. Note that if a shader on the material is changed,",
						"the render queue resets to that of the shader itself.",
						"Render queue value should be in [0..5000] range to work properly; or -1 to use the render queue from the shader.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-renderQueue.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.setMainTextureTiling.ToString (),
					InOutWs.InWs._vector2,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tmaterialValues [0].mainTextureScale = vector2Values [0];\n\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.vector2Values,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) + 
					ExprWs.ConstructorExpr.Vector2Values (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The texture scale of the main texture.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-mainTextureScale.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.setMainTextureOffset.ToString (),
					InOutWs.InWs._vector2,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tmaterialValues [0].mainTextureOffset = vector2Values [0];\n\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.vector2Values,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) + 
					ExprWs.ConstructorExpr.Vector2Values (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The texture offset of the main texture.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-mainTextureOffset.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.setMainTexture.ToString (),
					InOutWs.InWs._texture2D,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tmaterialValues [0].mainTexture = texture2DValues [0];\n\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.texture2DValues,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) + 
					ConstructorGetIdentifiedObject (new string [] {Enums.texture2DValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The material's texture.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-mainTexture.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.setMainColor.ToString (),
					InOutWs.InWs._color,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tmaterialValues [0].color = colorValues [0];\n\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.colorValues,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) + 
					ExprWs.ConstructorExpr.ColorValues (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The main material's color.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-color.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.setGlobalIlluminationFlags.ToString (),
					InOutWs.InWs._materialGlobalIlluminationFlags,
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tmaterialValues [0].globalIlluminationFlags = materialGlobalIlluminationFlags;\n\n\t\t\t\t\tmaterialValue = materialValues [0];\n\t\t\t\t}",
					InOutWs.OutWs.material_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialAll + ExprWs.Gv.materialGlobalIlluminationFlags,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}) + 
					ExprWs.ConstructorExpr.MaterialGlobalIlluminationFlags (this),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"How the material interacts with lightmaps and lightprobes.",
					},
					"https://docs.unity3d.com/ScriptReference/MaterialGlobalIlluminationFlags.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.getShader.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tshaderValue = materialValues [0].shader;\n\t\t\t\t}",
					InOutWs.OutWs.shader_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.shaderValue,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The shader used by the material.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-shader.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.getRenderQueue.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tintValue = materialValues [0].renderQueue;\n\t\t\t\t}",
					InOutWs.OutWs.int_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.intValue,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"Render queue of this material.",
						"By default materials use render queue of the shader it uses.",
						"You can override the render queue used using this variable. Note that if a shader on the material is changed,",
						"the render queue resets to that of the shader itself.",
						"Render queue value should be in [0..5000] range to work properly; or -1 to use the render queue from the shader.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-renderQueue.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.getPassCount.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tintValue = materialValues [0].passCount;\n\t\t\t\t}",
					InOutWs.OutWs.int_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.intValue,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"How many passes are in this material",
					},
					"https://docs.unity3d.com/ScriptReference/Material-passCount.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.getMainTextureTiling.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tvector2Value = materialValues [0].mainTextureScale;\n\t\t\t\t}",
					InOutWs.OutWs.vector2_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.vector2Value,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The texture scale of the main texture.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-mainTextureScale.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.getMainTextureOffset.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tvector2Value = materialValues [0].mainTextureOffset;\n\t\t\t\t}",
					InOutWs.OutWs.vector2_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.vector2Value,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The texture offset of the main texture.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-mainTextureOffset.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.getMainTexture.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\ttexture2DValue = (Texture2D)materialValues [0].mainTexture;\n\t\t\t\t}",
					InOutWs.OutWs.texture2D_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.texture2DValue,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The material's texture.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-mainTexture.html",
					""),

				new EnumInputComputeOutput (
					ComputeMaterialType.getMainColor.ToString (),
					"",
					"\t\t\t\tif (materialValues [0] != null)\n\t\t\t\t{\n\t\t\t\t\tcolorValue = materialValues [0].color;\n\t\t\t\t}",
					InOutWs.OutWs.color_,
					ExprWs.Gv.doIt + ExprWs.Gv.identifiedObjects + ExprWs.Gv.materialValues + ExprWs.Gv.colorValue,

					ConstructorGetIdentifiedObject (new string [] {Enums.materialValues_0_ID,}),
					ExprWs.UMDecl.computeMaterial,
					new string []
					{
						"",
						"The main material's color.",
					},
					"https://docs.unity3d.com/ScriptReference/Material-color.html",
					""),
			};
		}
#endregion variable_material

	}
}