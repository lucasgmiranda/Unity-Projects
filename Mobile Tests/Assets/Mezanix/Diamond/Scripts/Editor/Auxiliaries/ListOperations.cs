using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Mezanix.Diamond
{
	public class ListOperations 
	{
		public static void Permute (ref List <LogicNode> l, int i0, int i1)
		{
			if ( ( ! InRange (l, i0)) || ( ! InRange (l, i1) ) )
			{
				return;
			}

			LogicNode tmp = l [i0];

			l.RemoveAt (i0);

			l.Insert (i1, tmp);
		}
		static bool InRange (List <LogicNode> l, int i)
		{
			bool r = false;

			if (i > -1 && i < l.Count)
			{
				r = true;
			}

			return r;
		}


		public static void Permute (ref List <Logic> l, int i0, int i1)
		{
			if ( ( ! InRange (l, i0)) || ( ! InRange (l, i1) ) )
			{
				return;
			}

			Logic logicTmp = l [i0];

			l.RemoveAt (i0);

			l.Insert (i1, logicTmp);
		}
		static bool InRange (List <Logic> l, int i)
		{
			bool r = false;

			if (i > -1 && i < l.Count)
			{
				r = true;
			}

			return r;
		}

		public static void Permute (ref List <string> l, int i0, int i1)
		{
			if ( ( ! InRange (l, i0)) || ( ! InRange (l, i1) ) )
			{
				return;
			}

			string logicTmp = l [i0];

			l.RemoveAt (i0);

			l.Insert (i1, logicTmp);
		}
		static bool InRange (List <string> l, int i)
		{
			bool r = false;

			if (i > -1 && i < l.Count)
			{
				r = true;
			}

			return r;
		}


		public static void ListValuesToAnother (ref List <GameObject> to, List <GameObject> frome)
		{
			to = new List<GameObject> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <Rect> to, List <Rect> frome)
		{
			to = new List<Rect> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <Vector4> to, List <Vector4> frome)
		{
			to = new List<Vector4> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <Shader> to, List <Shader> frome)
		{
			to = new List<Shader> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <Texture2D> to, List <Texture2D> frome)
		{
			to = new List<Texture2D> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <Material> to, List <Material> frome)
		{
			to = new List<Material> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <Vector2> to, List <Vector2> frome)
		{
			to = new List<Vector2> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <Vector3> to, List <Vector3> frome)
		{
			to = new List<Vector3> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <string> to, List <string> frome)
		{
			to = new List<string> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <int> to, List <int> frome)
		{
			to = new List<int> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <float> to, List <float> frome)
		{
			to = new List<float> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <bool> to, List <bool> frome)
		{
			to = new List<bool> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void ListValuesToAnother (ref List <Color> to, List <Color> frome)
		{
			to = new List<Color> ();

			for (int i = 0; i < frome.Count; i++)
			{
				to.Add (frome [i]);
			}
		}

		public static void Merge (ref List <Rect> to, List <Rect> from_0, List <Rect> from_1)
		{
			to = new List<Rect> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <Vector4> to, List <Vector4> from_0, List <Vector4> from_1)
		{
			to = new List<Vector4> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <Shader> to, List <Shader> from_0, List <Shader> from_1)
		{
			to = new List<Shader> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <Texture2D> to, List <Texture2D> from_0, List <Texture2D> from_1)
		{
			to = new List<Texture2D> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <Material> to, List <Material> from_0, List <Material> from_1)
		{
			to = new List<Material> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <string> to, List <string> from_0, List <string> from_1)
		{
			to = new List<string> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <Vector3> to, List <Vector3> from_0, List <Vector3> from_1)
		{
			to = new List<Vector3> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <Vector2> to, List <Vector2> from_0, List <Vector2> from_1)
		{
			to = new List<Vector2> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <GameObject> to, List <GameObject> from_0, List <GameObject> from_1)
		{
			to = new List<GameObject> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <int> to, List <int> from_0, List <int> from_1)
		{
			to = new List<int> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <float> to, List <float> from_0, List <float> from_1)
		{
			to = new List<float> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <Color> to, List <Color> from_0, List <Color> from_1)
		{
			to = new List<Color> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}

		public static void Merge (ref List <bool> to, List <bool> from_0, List <bool> from_1)
		{
			to = new List<bool> ();

			for (int i = 0; i < from_0.Count; i++)
			{
				to.Add (from_0 [i]);
			}

			for (int i = 0; i < from_1.Count; i++)
			{
				to.Add (from_1 [i]);
			}
		}
	}

}
