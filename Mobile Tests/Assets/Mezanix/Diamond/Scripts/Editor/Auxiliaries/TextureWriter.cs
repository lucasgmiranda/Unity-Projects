using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Mezanix.Diamond
{
	public enum TextureEncodeType
	{
		png,
		
		jpg,
	}

	public enum WriteTextureFormat
	{
		ARGB32,

		RGBA32,

		RGB24, 

		Alpha8,


		RHalf,

		RGHalf,

		RGBAHalf,


		RFloat,

		RGFloat,

		RGBAFloat
	}


	public class TextureWriter 
	{
		public const string projectFolder = @"Assets";

		//public const string auxiliariesFolder = @"Assets/Mezanix/Diamond/Scripts/Editor/Auxiliaries";

		public const string texturesFolderName = "TexturesCreatedByDiamond";

		//static string texturesFolderPath = "Generate Textures Here";

		public const string newUniColortextureFolderName = "NewUniColortexture";

		public const string screenshotsFolderName = "Screenshots";



		public static TextureFormat GetTextureFormat (WriteTextureFormat writeTextureFormat)
		{
			TextureFormat textureFormat = TextureFormat.ARGB32;

			switch (writeTextureFormat)
			{
			case WriteTextureFormat.Alpha8:
				textureFormat = TextureFormat.Alpha8;
				break;

			case WriteTextureFormat.ARGB32:
				textureFormat = TextureFormat.ARGB32;
				break;

			case WriteTextureFormat.RFloat:
				textureFormat = TextureFormat.RFloat;
				break;

			case WriteTextureFormat.RGB24:
				textureFormat = TextureFormat.RGB24;
				break;

			case WriteTextureFormat.RGBA32:
				textureFormat = TextureFormat.RGBA32;
				break;

			case WriteTextureFormat.RGBAFloat:
				textureFormat = TextureFormat.RGBAFloat;
				break;

			case WriteTextureFormat.RGBAHalf:
				textureFormat = TextureFormat.RGBAHalf;
				break;

			case WriteTextureFormat.RGFloat:
				textureFormat = TextureFormat.RGFloat;
				break;

			case WriteTextureFormat.RGHalf:
				textureFormat = TextureFormat.RGHalf;
				break;

			case WriteTextureFormat.RHalf:
				textureFormat = TextureFormat.RHalf;
				break;

			}

			return textureFormat;
		}

		public static string TextureEncodeTypeToFileExtention (TextureEncodeType tet)
		{
			string r = "png";

			switch (tet)
			{
			case TextureEncodeType.jpg:
				r = "jpg";
				break;

			case TextureEncodeType.png:
				r = "png";
				break;
			}

			return r;
		}


		public static string WriteTexture (Texture2D texture, TextureEncodeType textureEncodeType, int jpgQuality,
			string fileName, WriteTextureFormat writeTextureFormat, bool isMipmap, bool isLinear,
			Vector2 beginAt, Vector2 block, bool isUniColor)
		{
			TextureFormat textureFormat = GetTextureFormat (writeTextureFormat);
	


			int x = Mathf.CeilToInt ((Mathf.Clamp (beginAt.x, 0f, 100f) * ((float)texture.width/100f)));

			int y = Mathf.CeilToInt ((Mathf.Clamp (beginAt.y, 0f, 100f) * ((float)texture.height/100f)));

			int bW = Mathf.CeilToInt ((Mathf.Clamp (block.x, 0f, 100f) * ((float)texture.width/100f)));

			int bH = Mathf.CeilToInt ((Mathf.Clamp (block.y, 0f, 100f) * ((float)texture.height/100f)));


			bW = Mathf.Clamp (bW, 2, texture.width-x);

			bH = Mathf.Clamp (bH, 2, texture.height-y);

			Texture2D tex = new Texture2D (bW, bH, textureFormat, isMipmap, isLinear);

			tex.SetPixels (texture.GetPixels (x, y, bW, bH));

			tex.Apply ();

			byte [] bytes = null;



			string exten = TextureEncodeTypeToFileExtention (textureEncodeType);

			switch (textureEncodeType)
			{
			case TextureEncodeType.jpg:
				jpgQuality = Mathf.Clamp (jpgQuality, 0, 100);

				bytes = tex.EncodeToJPG (jpgQuality);
				break;

			case TextureEncodeType.png:
				bytes = tex.EncodeToPNG ();
				break;
			}

			if (string.IsNullOrEmpty (fileName))
			{
				fileName = texture.name;

				if (string.IsNullOrEmpty (fileName))
					fileName = "New_Texture";
			}

			string path = CreateTexturesFolder () + "/" + fileName + "." + exten;

			if (isUniColor)
				path = CreateNewFolder (newUniColortextureFolderName)
					+ "/" + fileName + "." + exten;

			File.WriteAllBytes (path, bytes);

			Auxiliaries.SaveAndRefreshAssetsForced ();

			Aux.Ass.PingObject <Texture2D> (path);

			return path;
		}

		public static string WriteTexture (Texture2D texture, TextureEncodeType textureEncodeType, int jpgQuality,
			string fileName, WriteTextureFormat writeTextureFormat, bool isMipmap, bool isLinear,
			Vector2 beginAt, Vector2 block, string testName)
		{
			TextureFormat textureFormat = GetTextureFormat (writeTextureFormat);



			int x = Mathf.CeilToInt ((Mathf.Clamp (beginAt.x, 0f, 100f) * ((float)texture.width/100f)));

			int y = Mathf.CeilToInt ((Mathf.Clamp (beginAt.y, 0f, 100f) * ((float)texture.height/100f)));

			int bW = Mathf.CeilToInt ((Mathf.Clamp (block.x, 0f, 100f) * ((float)texture.width/100f)));

			int bH = Mathf.CeilToInt ((Mathf.Clamp (block.y, 0f, 100f) * ((float)texture.height/100f)));


			bW = Mathf.Clamp (bW, 2, texture.width-x);

			bH = Mathf.Clamp (bH, 2, texture.height-y);

			Texture2D tex = new Texture2D (bW, bH, textureFormat, isMipmap, isLinear);

			tex.SetPixels (texture.GetPixels (x, y, bW, bH));

			tex.Apply ();

			byte [] bytes = null;



			string exten = TextureEncodeTypeToFileExtention (textureEncodeType);

			switch (textureEncodeType)
			{
			case TextureEncodeType.jpg:
				jpgQuality = Mathf.Clamp (jpgQuality, 0, 100);

				bytes = tex.EncodeToJPG (jpgQuality);
				break;

			case TextureEncodeType.png:
				bytes = tex.EncodeToPNG ();
				break;
			}

			if (string.IsNullOrEmpty (fileName))
			{
				fileName = texture.name;

				if (string.IsNullOrEmpty (fileName))
					fileName = "New_Texture";
			}


			string path = CreateNewFolder (testName) + "/" + fileName + "." + exten;

			File.WriteAllBytes (path, bytes);

			Auxiliaries.SaveAndRefreshAssetsForced ();

			return path;
		}

		public static string CreateTexturesFolder ()
		{
			//return Auxiliaries.CreateFolder (projectFolder, texturesFolderName);

			if ( ! AssetDatabase.IsValidFolder (Diamond.namesToSave.texturesGenerationFolderPath))
			{
				Debug.LogWarning ("Trying to write texture to an invalid folder");
				Debug.LogWarning ("So Diamond had created the texture at the Assets root");
				Debug.LogWarning ("Choose a valid folder by clicking on the asterix at the top right of the Diamond window");

				return projectFolder;
			}

			return Diamond.namesToSave.texturesGenerationFolderPath;
		}

		public static string CreateNewFolder (string folderName)
		{
			//string texturesFolderPath = Auxiliaries.CreateFolder (projectFolder, texturesFolderName);

			if ( ! AssetDatabase.IsValidFolder (Diamond.namesToSave.texturesGenerationFolderPath))
			{
				Debug.LogWarning ("Trying to write texture to an invalid folder");
				Debug.LogWarning ("So Diamond had created the texture at the Assets root");
				Debug.LogWarning ("Choose a valid folder by clicking on the asterix at the top right of the Diamond window");

				return projectFolder;
			}

			return Auxiliaries.CreateFolder (Diamond.namesToSave.texturesGenerationFolderPath, folderName);
		}
	}
}
