using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mezanix.GameDesign
{
	[ExecuteInEditMode]
	public class TagsLayersIdentifiers : MonoBehaviour 
	{
		public const string frameIconFolder = "Assets/Mezanix/Diamond/Resources/GameDesignGui/Textures/GameDesign";
		public const string frameIconPath = frameIconFolder + "/Frame.png";
		

		public enum RoleTags
		{
			unityEditor,
			game,
		};
		public RoleTags roleTags;

		public enum GameType
		{
			lightMaze,
			shootEmUp,
			_2dPlatformer,
		};
		public GameType gameType;

		public enum Layer
		{
			none,

			parent,

			player,

			enemy,
			enemyAi,

			challenge,
			economy,

			environement,
			environementStatic,
			environementDynamic,
			environementAi,

			bullet,
		}
		public Layer layer;
		public const string playerIconFolder = "Assets/Mezanix/Diamond/Resources/GameDesignGui/Textures/GameDesign/Layers";
		public const string playerIconPath = playerIconFolder + "/playerIcon.png";
		


		public string Id
		{
			get {return id;}
		}
		string id = "";
		public void CreateId ()
		{
			if ( ! string.IsNullOrEmpty (id))
				return;
			
			id = AuxMono.DatesTimesAndFrequences.DateTimeNow ();
		}

		// Use this for initialization
		void Start () 
		{
			
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}
	}
}
