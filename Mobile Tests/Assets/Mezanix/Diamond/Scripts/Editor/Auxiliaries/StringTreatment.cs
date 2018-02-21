using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

namespace Mezanix.Diamond
{
	public class StringTreatment 
	{
		public const string rArrow = " -> ";

		public const string gwm = "\"";

		public const string backup = "_backup";

		public const string GraphsBackupFolderName = "DiamondGraphsBackup";

		public const string selectedFolderMustBeValidAndInTheProjectAssets_Title = "Invalid Selected Folder";
		public const string selectedFolderMustBeValidAndInTheProjectAssets =
			"Please choose a valid folder, it must be valid and in the project's Assets folder";


		public static int GetMaxPhrasesLength (string [] message)
		{
			int [] phrasesLengths = new int[message.Length];

			for (int i = 0; i < message.Length; i++)
				phrasesLengths [i] = message [i].Length;

			return Mathf.Max (phrasesLengths);
		}

		public static bool CompareArrays (string [] s_0, string [] s_1)
		{
			bool r = true;

			if (s_0 == null || s_1 == null)
				return false;

			if (s_0.Length != s_1.Length)
				return false;

			for (int i = 0; i < s_0.Length; i++)
			{
				if (s_0 [i] != s_1 [i])
					return false;
			}

			return r;
		}


		public static string PrepareStringToInt (string s)
		{
			string r = "0";

			r = Regex.Replace (s, @"[a-zA-Z]", "");

			r = Regex.Replace (r, @"[&²é~""#'{(|è`_\ç^à@)=}+°>>,?;:/!*§ù%µ^¤$£.]", "");

			//r = Regex.Replace (r, @"-", "");


			if (r.Length > 1)
			{
				if (r [0] == '0')
					r = r.Substring (1);
			}

			return r;
		}
	


		public static string IntToString (int n)
		{
			if (n == 0)
				return "0";

			bool isNegative = false;

			if (n < 0)
			{
				n = -n;

				isNegative = true;
			}
			

			int pow = 0;

			for (;;)
			{
				pow++;

				if (ModuloOfTenPower (n, pow) == n)
					break;
			}
		

			string s = "";

			float [] f = new float[pow];

			for (int i = 0; i < pow; i++)
			{
				if (i > 0)
				{
					int subs = (int)f [i-1];

					n -= subs * (int)( Mathf.Pow (10f, (float)(i-1)) );
				}

				f [i] = ModuloOfTenPower (n, i+1) / Mathf.Pow (10f, (float)i);
			}

			for (int i = pow -1; i >= 0; i--)
				s += f [i].ToString ("0");


			string retVal = s;

			if (isNegative)
				retVal = "-" + s;

			return retVal;
		}

		static int ModuloOfTenPower (int nb, int p)
		{
			float pf = (float)p;

			float tenPowP = Mathf.Pow (10f, pf);

			int tenPowPInt = (int)tenPowP;


			return nb % tenPowPInt;
		}

		public static List <int> IntsFromStringSpaceSeparator (string s)
		{
			List <int> retVal = new List<int> ();

			string intS = "";

			for (int i = 0; i < s.Length; i++)
			{
				if (s [i] != ' ')
				{
					intS += s [i];
				}
				else
				{
					retVal.Add (int.Parse (intS));

					intS = "";
				}
			}

			return retVal;
		}


		static string [] notAllowedScriptNames = new string[]
		{
			"bool",


			"Shader",

			"shader",


			"Texture",

			"texture",


			"Texture2D",


			"texture2D",


			"int",

			"float",


			"Int",

			"Float",


			"string",

			"String",


			"vector2",

			"Vector2",


			"Vector3",

			"vector3",


			"Vector4",

			"vector4",


			"Camera",

			"Collider",

			"Collider2D",

			"NavMeshAgent",

			"Particle",

			"ParticleSystem",


			"Ray",
			"Ray2D",


			"Renderer",

			"Rigidbody",

			"Rigidbody2D",

			"Transform",


			"Material",

			"material",


			"Light",

			"light",
		};

		public static string ScriptName (string s)
		{
			string r = s;

			r = Regex.Replace (s, @"[ ]", "");

			r = Regex.Replace (r, @"[&²é~""#'{(|è`\ç^à@)=}+°>>,?;:/!*§ù%µ^¤$£.-]", "");

			List <char> rL = new List<char> ();

			for (int i = 0; i < r.Length; i++)
			{
				rL.Add (r [i]);
			}


			for (int i = -1; i < rL.Count; i++)
			{
				if (i == 0)
				{
					if (IsNumeric (rL [i]))
					{
						rL.Remove (rL [i]);

						i--;
					}
					else 
					{
						rL [i] = char.ToUpper (rL [i]);
					}
				}
			}


			r = new string (rL.ToArray ());

			for (int i = 0; i < notAllowedScriptNames.Length; i++)
			{
				if (r == notAllowedScriptNames [i])
				{
					Debug.Log ("A '_' was added to the name: '" + r + "'." +
						" Because the name: '" + r + 
						"' is not allowed as a script name by the user");

					r += "_";
					break;
				}
			}

			return r;
		}

		public static string FileName (string s)
		{
			string r = s;



			r = Regex.Replace (r, @"[&²é~""#'{(|è`\ç^à@)=}+°>>,?;:/!*§ù%µ^¤$£.-]", "");

			List <char> rL = new List<char> ();

			for (int i = 0; i < r.Length; i++)
			{
				rL.Add (r [i]);
			}



			r = new string (rL.ToArray ());

			return r;
		}

		static bool IsNumeric (char s)
		{
			bool r = false;

			if (s == '0' ||
				s == '1' ||
				s == '2' ||
				s == '3' ||
				s == '4' ||
				s == '5' ||
				s == '6' ||
				s == '7' ||
				s == '8' ||
				s == '9')
			{
				r = true;
			}

			return r;
		}
	
		public static string FirstToLower (string s)
		{
			string r = s;
			
			if (s.Length == 0)
				return "";

			char [] c = s.ToCharArray ();

			c [0] = char.ToLower (c [0]);

			r = new string (c);

			return r;
		}

		public static string AfterSlash (string s)
		{
			char [] c = s.ToCharArray ();

			List <char> cR = new List<char> ();

			for (int i = c.Length-1; i > -1; i--)
			{
				if (c [i] == '/' || c [i] == '\\')
					break;

				cR.Add (c [i]);
			}

			List <char> crI = new List<char> ();

			for (int i = cR.Count-1; i > -1; i--)
			{
				crI.Add (cR [i]);
			}

			char [] criA = crI.ToArray ();

			return new string (criA);
		}
	
		public static string BeforeLastSlash (string s, bool withSlash)
		{
			char [] c = s.ToCharArray ();

			List <char> cR = new List<char> ();

			int lastSlashIndex = -1;

			for (int i = c.Length-1; i > -1; i--)
			{
				if (c [i] == '/' || c [i] == '\\')
				{
					lastSlashIndex = i;

					break;
				}
			}

			int toCountTO = lastSlashIndex;

			if (withSlash)
			{
				toCountTO = lastSlashIndex + 1;
			}

			if (toCountTO < 0 || toCountTO > c.Length)
				return "";

			for (int i = 0; i < lastSlashIndex; i++)
			{
				cR.Add (c [i]);
			}

			return new string (cR.ToArray ());
		}


		public static string BeforeThat (string s, char that)
		{
			char [] c = s.ToCharArray ();

			List <char> cR = new List<char> ();

			for (int i = 0; i < c.Length; i++)
			{
				if (c [i] == that)
					break;

				cR.Add (c [i]);
			}

			return new string (cR.ToArray ());
		}


		public static string AfterThat (string s, char that)
		{
			char [] c = s.ToCharArray ();

			List <char> cR = new List<char> ();


			for (int i = c.Length-1; i > -1; i--)
			{
				if (c [i] == that)
					break;

				cR.Add (c [i]);

				if (i == 0)
					cR = new List<char> ();
			}

			List <char> crI = new List<char> ();

			for (int i = cR.Count-1; i > -1; i--)
			{
				crI.Add (cR [i]);
			}

			char [] criA = crI.ToArray ();

			return new string (criA);
		}

		public static bool IsEndWith_List (string s)
		{
			char [] c = s.ToCharArray ();

			if (c.Length < 4)
				return false;

			List <char> cR = new List<char> ();

			for (int i = c.Length-1; i > c.Length-5; i--)
			{
				cR.Add (c [i]);
			}

			List <char> crI = new List<char> ();

			for (int i = cR.Count-1; i > -1; i--)
			{
				crI.Add (cR [i]);
			}

			char [] criA = crI.ToArray ();

			return new string (criA) == "List";
		}


		public static string WithoutExtention (string s)
		{
			char [] c = s.ToCharArray ();

			List <char> cR = new List<char> ();

			for (int i = 0; i < c.Length; i++)
			{
				if (c [i] == '.')
					break;

				cR.Add (c [i]);
			}

			return new string (cR.ToArray ());
		}
	
		public static string WithoutTabs (string s)
		{
			char [] c = s.ToCharArray ();

			List <char> cR = new List<char> ();

			for (int i = 0; i < c.Length; i++)
			{
				cR.Add (c [i]);
			}

			for (int i = -1; i < cR.Count; i++)
			{
				if (i == -1)
					continue;
				
				if (cR [i] == '\\')
				{
					if (i < cR.Count-1)
					{
						if (cR [i+1] == 't')
						{
							cR.RemoveAt (i);

							cR.RemoveAt (i);

							i--;
						}
					}
				}
			}

			return new string (cR.ToArray ());
		}
	

		public static string SubtractWeak (string l, string s)
		{
			if (string.IsNullOrEmpty (s))
			{
				return "";
			}

			int index = s.Length;

			if (index < 0 || index > l.Length -1)
			{
				return "";
			}

			char [] lc = l.ToCharArray ();

			List <char> rcL = new List<char> ();


			for (int i = index; i < l.Length; i++)
			{
				rcL.Add (lc [i]); 
			}

			return new string (rcL.ToArray ());
		}

		public static string SubtractWeakFromEnd (string l, string s)
		{
			if (string.IsNullOrEmpty (s))
			{
				return "";
			}

			int index = s.Length;

			if (index < 0 || index > l.Length -1)
			{
				return "";
			}

			char [] lc = l.ToCharArray ();

			List <char> rcL = new List<char> ();


			for (int i = 0; i < l.Length-index; i++)
			{
				rcL.Add (lc [i]); 
			}

			return new string (rcL.ToArray ());
		}


		public static string AfterThisIndex (string l, int index)
		{
			if (index < 0 || index > l.Length -1)
			{
				return "";
			}

			char [] lc = l.ToCharArray ();

			List <char> rcL = new List<char> ();


			for (int i = index; i < l.Length; i++)
			{

				rcL.Add (lc [i]); 
			}

			return new string (rcL.ToArray ());
		}
	
		public static string DeleteOneTabOfEachNewLine (string s)
		{
			string [] sLines = StringToStringArray (s, '\n');

			for (int i = 0; i < sLines.Length; i++)
			{
				if (sLines [i].Length > 0)
				{
					if (sLines [i][0] == '\t')
					{
						sLines [i] = AfterThisIndex (sLines [i], 1);
					}
				}
			}

			string r = "";

			for (int i = 0; i < sLines.Length; i++)
			{
				r += sLines [i] + "\n";
			}

			return r;
		}


		public static string [] StringToStringArray (string s, char separator)
		{
			char [] sChar = s.ToCharArray ();

			List<string> listOfStrings = new List<string>();

			List<char> listChar = new List<char>();

			for (int i = 0; i < sChar.Length; i++)
			{
				if (sChar [i] == separator || i == sChar.Length - 1)
				{
					if (i < sChar.Length - 1)
					{
						listOfStrings.Add (new string(listChar.ToArray ()));

						listChar = new List<char>();
					}
					else if (i == sChar.Length - 1)
					{
						listChar.Add (sChar [i]);

						listOfStrings.Add (new string(listChar.ToArray ()));

						listChar = new List<char>();
					}

					continue;
				}

				listChar.Add (sChar [i]);
			}

			return listOfStrings.ToArray ();
		}

		public static string StringArrayToString (string[] sA)
		{
			string messageFinale = "";

			for (int i = 0; i < sA.Length; i++)
			{
				messageFinale += sA [i];
			}

			return messageFinale;
		}


		public static string StringArrayToString (string[] sA, bool addReturn)
		{
			string messageFinale = "";

			if (addReturn)
			{
				for (int i = 0; i < sA.Length; i++)
				{
					messageFinale += AddReturn (sA [i]);
				}
			}
			else if ( ! addReturn)
			{
				messageFinale = StringArrayToString (sA);
			}

			return messageFinale;
		}

		public static string StringArrayToScriptInitialization (string[] sA, int tabs,
			bool addEmptyAtFirst)
		{
			string r = "";

			string tabsOut = "";

			string tabsIn = "";

			for (int i = 0; i < tabs; i++)
			{
				tabsOut += "\t";
			}
			tabsIn = tabsOut + "\t";

			r += tabsOut + "new string[]\n";
			r += tabsOut + "{\n";

			if (addEmptyAtFirst)
			{
				r += tabsIn + gwm + gwm + ",\n";
			}


			if (sA == null)
			{
				return "";
			}
			for (int i = 0; i < sA.Length; i++)
			{
				r += tabsIn + gwm + sA [i] + gwm + ",\n";
			}

			r += tabsOut + "}";

			return r;
		}


		public static string AddReturn (string s)
		{
			return s + "\n";
		}

		public static string [] AddReturn (string [] s)
		{
			string [] r = s;

			for (int i = 0; i < r.Length; i++)
			{
				AddReturn (r [i]);
			}

			return r;
		}
	

		public static string FloatToFileName (string s)
		{
			char [] c = s.ToCharArray ();

			for (int i = 0; i < c.Length; i++)
			{
				if (c [i] == '.')
				{
					c [i] = '_';
				}
			}

			return new string (c);
		}
	}
}
