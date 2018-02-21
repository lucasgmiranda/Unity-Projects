using UnityEngine;
using UnityEditor;
using System.Collections;


namespace Mezanix.Diamond
{
	public class Skins  
	{
		public static GUISkin guiSkin;

		public static void GetGuiSkin ()
		{
			guiSkin = Resources.Load ("GUI_Read_Only/Diamond") as GUISkin;

			//if (Diamond.testing)
				//guiSkin = Resources.Load ("GUI_Read_Only/Diamond 1") as GUISkin;


			if (guiSkin == null)
				Debug.LogError ("Failed to load Diamond Skin");

		}

		public static readonly Rect nodeRect = new Rect (10f, 20f, 146f, 46f);

		public static float separatorThickness = 2f;

		public static void DrawSeparator (Rect rect)
		{
			GUI.Box (rect, "", guiSkin.GetStyle (separator));
		}

		#region logic node sizes

		public static readonly Rect logicNodeRect = new Rect (10f, 20f, 200f, 146f);

		public static readonly Vector2 logicNodeRectStep = new Vector2 (265f, 17.5f);

		public static readonly Rect logicNodeRectMini = new Rect (10f, 20f, logicNodeRect.width, 2f*logicNodeRectStep.y);

		#endregion logic node sizes



		#region GUI Styles Names

		public static readonly string node = "node";

		public static readonly string nodeSelected = "node selected";

		public static readonly string up = "up";

		public static readonly string down = "down";

		public static readonly string add = "add";

		public static readonly string view = "view";

		public static readonly string diamondLogo = "diamondLogo";

		public static readonly string viewProjectVariables = "viewProjectVariables";

		public static readonly string alwaysDoItExpression = "alwaysDoItExpression";


		public static readonly string buttonQuestionMark = "buttonQuestionMark";

		public static readonly string playButton = "playButton";

		public static readonly string pauseButton = "pauseButton";

		//
		public static readonly string nodeStateName = "nodeStateName";

		public static readonly string redDot = "redDot";

		public static readonly string x_small = "x_xmall";

		public static readonly string newFile = "newFile";

		public static readonly string loadFile = "loadFile";

		public static readonly string saveFile = "saveFile";

		public static readonly string saveFileAndScene = "saveFileAndScene";

		public static readonly string floatMessage = "floatMessage";

		public static readonly string button = "button";

		public static readonly string in_out = "in_out";

		public static readonly string set = "set";

		public static readonly string get = "get";

		public static readonly string magnet = "magnet";

		public static readonly string magnetInvert = "magnet_invert";

		public static readonly string gateWaiting = "gateWaiting";

		public static readonly string gateBlocWaiting = "gateBlocWaiting";

		public static readonly string gate = "gate";

		public static readonly string gateBloc = "gateBloc";

		public static readonly string setInactive = "setInactive";

		public static readonly string asterix = "asterix";

		public static readonly string getInactive = "getInactive";
		//
		public static readonly string publicVariable = "publicVariable";

		public static readonly string x = "x";

		public static readonly string cut = "cut";

		public static readonly string in_out_selected = "in_out_selected";

		public static readonly string label = "label";

		public static readonly string dotesOptions = "dotes options";

		public static readonly string logicInside = "logic inside";

		public static readonly string back = "back";

		public static readonly string forward = "forward";

		public static readonly string questionMark = "questionMark";
		//
		public static readonly string logicSignature = "logicSignature";

		public static readonly string alphaBg = "alphaBg";


		public static readonly string hueBar = "hueBar";

		public static readonly string RedBar = "RedBar";

		public static readonly string GreenBar = "GreenBar";

		public static readonly string BlueBar = "BlueBar";

		public static readonly string nodeBG = "nodeBG";

		public static readonly string GrayScaleBar = "GrayScaleBar";


		public static readonly string InNodeMessageInfo = "InNodeMessageInfo";

		//
		public static readonly string LittleNamedRectsCenterDark = "LittleNamedRectsCenterDark";


		public static readonly string LittleNamedRectsCenter = "LittleNamedRectsCenter";

		public static readonly string LittleNamedRects = "LittleNamedRects";

		//public static readonly string logicNodeSelected = "logic node selected";

		public static readonly string logicNodeName = "logic node name";

		public static readonly string projectVariableName = "projectVariableName";
		//
		public static readonly string filterLabel = "filterLabel";

		public static readonly string logicNodeLabel = "logic node label";

		public static readonly string logicNodeResult = "logic node result";

		public static readonly string logicNodeIntermidiateResult = "logicNodeIntermidiateResult";

		public static readonly string graphLabel = "graph label";

		public static readonly string leftUpMessageInfo = "Left Up Message Info";

		public static readonly string leftDownMessageInfo = "Left Down Message Info";
		//
		public static readonly string logicNodeLabelLeft = "logicNodeLabelLeft";

		public static readonly string documentationMessage = "documentationMessage";


		public static readonly string ViewToolBar = "ViewToolBar";

		public static readonly string ViewLogic = "ViewLogic";

		//
		public static readonly string asterix_small = "asterix_xmall";

		public static readonly string separator = "separator";

		#endregion GUI Styles Names
	

		#region GUI Colors Hex

		public static readonly string viewColor = "66848f"; 

		#endregion GUI Colors Hex
	}
}
