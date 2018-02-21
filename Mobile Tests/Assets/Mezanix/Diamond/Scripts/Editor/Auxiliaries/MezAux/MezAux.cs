using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mezanix
{
	public partial class Aux 
	{
		#region assets
		#if UNITY_EDITOR
		public class Ass
		{
			public static bool SaveAndRefreshAssets ()
			{
				bool r = false;

				try
				{
					AssetDatabase.SaveAssets ();

					AssetDatabase.Refresh ();
				}
				catch
				{
					return r;
				}

				r = true;
				return r;
			}

			public static void PingObject <T> (string path)
			{
				UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath (path, typeof (T));

				if (obj != null)
					EditorGUIUtility.PingObject (obj);
			}	
		}
		#endif
		#endregion assets

		public class GizmosMez
		{
			public enum DrawTextureStyle
			{
				free,
				centered,
			}

			public static void DrawGUITexture (Rect rect, Texture tex, DrawTextureStyle style, Transform tr)
			{
				switch (style)
				{
				case DrawTextureStyle.centered:
					Gizmos.DrawGUITexture (Rectan.CenteredToTransform (tr), tex);
					break;

				case DrawTextureStyle.free:
					Gizmos.DrawGUITexture (rect, tex);
					break;
				}
			}
		}

		public class ArrGeneric
		{
			public static List<Material> ArrayToList (Material [] arr)
			{
				List<Material> r = new List<Material> ();

				for (int i = 0; i < arr.Length; i++)
				{
					r.Add (arr [i]);
				}

				return r;
			}
		}

		public class BitOper
		{
			public static int Reverse (int i, int size)
			{
				int j = i;

				int sum = 0;

				int w = 1;

				int m = size / 2;

				while (m != 0)
				{
					j = ((i&m) > m-1)? 1: 0;

					sum += j*w;

					w *= 2;

					m /= 2;
				}
				return sum;
			}

			public static float [] ButterflyLookupTable (float [] bflut, int size, int passes)
			{
				bflut = new float[size * passes * 4];

				for (int i = 0; i < passes; i++)
				{
					int blocks = (int) Mathf.Pow (2f, (float)(passes-1-i));
					int inputs = (int) Mathf.Pow (2f, (float)i);

					for (int j = 0; j < blocks; j++)
					{
						for (int k = 0; k < inputs; k++)
						{
							int i1, i2, j1, j2;
							if (i == 0)
							{
								i1 = j*inputs*2 + k;
								i2 = j*inputs*2 + inputs + k;

								j1 = Reverse (i1, size);
								j2 = Reverse (i2, size);
							}
							else
							{
								i1 = j*inputs*2 + k;
								i2 = j*inputs*2 + inputs + k;

								j1 = i1;
								j2 = i2;
							}

							float wr = Mathf.Cos (2f*Mathf.PI*(float)(k*blocks)/(float)size);
							float wi = Mathf.Sin (2f*Mathf.PI*(float)(k*blocks)/(float)size);

							int offset1 = 4*(i1 + i*size);
							bflut [offset1 + 0] = j1;
							bflut [offset1 + 1] = j2;
							bflut [offset1 + 2] = wr;
							bflut [offset1 + 3] = wi;


							int offset2 = 4*(i2 + i*size);
							bflut [offset2 + 0] = j1;
							bflut [offset2 + 1] = j2;
							bflut [offset2 + 2] = -wr;
							bflut [offset2 + 3] = -wi;
						}
					}
				}

				return bflut;
			}
		}


		public class Cam
		{
			#if UNITY_EDITOR
			public static bool GetLastActiveSceneViewCamera (ref Camera cam)
			{
				if (cam != null)
					return true;
				
				if (UnityEditor.SceneView.lastActiveSceneView == null)
					return false;

				cam = UnityEditor.SceneView.lastActiveSceneView.camera;
				if (cam == null)
					return false;

				return true;
			}
			#endif

			public static Rect ClipPlanRect (Camera c, float z)
			{
				Rect r = new Rect (Vector2.zero, Vector2.one);

				if (c == null)
					return r;

				z = Mathf.Max (z, c.nearClipPlane);

				Vector2 topLeft = c.ViewportToWorldPoint (new Vector3 (0f, 1f, z));
				Vector2 bottomRight = c.ViewportToWorldPoint (new Vector3 (1f, 0f, z));
				Vector2 rectSize = new Vector2 (
					Mathf.Abs (bottomRight.x - topLeft.x),
					Mathf.Abs (bottomRight.y - topLeft.y));

				r = new Rect (topLeft, rectSize);

				return r;
			}

			public static Vector3 [] ClipPlanBounds (Camera c, float z)
			{
				if (c == null)
					return new Vector3[]
					{
						new Vector3 (0f, 1f, 0),
						new Vector3 (1f, 0f, 0),
					};

				z = Mathf.Max (z, c.nearClipPlane);

				Vector3 topLeft = c.ViewportToWorldPoint (new Vector3 (0f, 1f, z));
				Vector3 bottomRight = c.ViewportToWorldPoint (new Vector3 (1f, 0f, z));

				return new Vector3[]
				{
					topLeft,
					bottomRight,
				};
			}
		}

		public class Col
		{
			public static float [] RGBToHSV (Color c)
			{
				float [] retVal = new float[3];

				Color.RGBToHSV (c, out retVal [0], out retVal [1], out retVal [2]);

				return retVal;
			}
		}

		public class Complex
		{
			public Complex (float setRe, float setIm)
			{
				re = setRe;
				im = setIm;
			}

			float re = 0;
			float im = 0;

			public static Complex Add (Complex c0, Complex c1)
			{
				return new Complex (c0.re + c1.re, c0.im + c1.im);
			}

			public static Complex Sub (Complex c0, Complex c1)
			{
				return new Complex (c0.re - c1.re, c0.im - c1.im);
			}

			public static Complex Mult (Complex c0, Complex c1)
			{
				return new Complex (c0.re*c1.re - c0.im*c1.im, c0.re*c1.im + c0.im*c1.re);
			}

			public static Complex Mult (Complex c, float x)
			{
				return new Complex (x*c.re, x*c.im);
			}

			public static bool Eql (Complex c0, Complex c1)
			{
				return c0.re == c1.re && c0.im == c1.im;
			}

			public static Complex Conj (Complex c)
			{
				return new Complex (c.re, -c.im);
			}

			public static Complex Sqrt (float x)
			{
				if (x >= 0f)
				{
					return new Complex (Mathf.Sqrt (x), 0f);
				}
				else
				{
					return new Complex (0f, Mathf.Sqrt (Mathf.Abs (x)));
				}
			}

			public override string ToString ()
			{
				return this.re.ToString () + " + i*" + this.im.ToString ();
			}
		}


		public static class Deb
		{
			public static void DrawFunc (float [] v, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3,
				Mathm.MathFuncSegment3 func, Color c)
			{
				float [] r = Mathm.FuncSegment3Compute (v, p0, p1, p2, p3, func);

				for (int i = 0; i < r.Length-1; i++)
				{
					Debug.DrawLine (
						new Vector3 (v [i], r [i], 0f),
						new Vector3 (v [i+1], r [i+1], 0f), c, Time.deltaTime);
				}
			}
			public static void DrawFunc (float [] v, Vector2 min, Vector2 max,
				Mathm.MathFuncSegment func, Color c)
			{
				float [] r = Mathm.FuncSegmentCompute (v, min, max, func);

				for (int i = 0; i < r.Length-1; i++)
				{
					Debug.DrawLine (
						new Vector3 (v [i], r [i], 0f),
						new Vector3 (v [i+1], r [i+1], 0f), c, Time.deltaTime);
				}
			}
			public static void DrawFunc (float [] v, float speed, Vector2 min, Vector2 max,
				Mathm.MathFuncExpSegment func, Color c)
			{
				float [] r = Mathm.FuncExpSegmentCompute (v, speed, min, max, func);

				for (int i = 0; i < r.Length-1; i++)
				{
					Debug.DrawLine (
						new Vector3 (v [i], r [i], 0f),
						new Vector3 (v [i+1], r [i+1], 0f), c, Time.deltaTime);
				}
			}
			public static void DrawFunc (float [] v, float speed, float x_0, float x_1,
				Mathm.MathFuncHyperbol func, Color c)
			{
				float [] r = Mathm.FuncHyperbolCompute (v, speed, x_0, x_1, func);

				for (int i = 0; i < r.Length-1; i++)
				{
					Debug.DrawLine (
						new Vector3 (v [i], r [i], 0f),
						new Vector3 (v [i+1], r [i+1], 0f), c, Time.deltaTime);
				}
			}
			public static void DrawFunc (float [] v, float speed, Vector2 min, Vector2 max, bool inv,
				Mathm.MathFuncSigmoid func, Color c)
			{
				float [] r = Mathm.FuncSigmoidCompute (v, speed, min, max, inv, func);

				for (int i = 0; i < r.Length-1; i++)
				{
					Debug.DrawLine (
						new Vector3 (v [i], r [i], 0f),
						new Vector3 (v [i+1], r [i+1], 0f), c, Time.deltaTime);
				}
			}
			public static void DrawFunc (float [] v, Mathm.MathFunc func, Color c)
			{
				float [] r = Mathm.FuncCompute (v, func);

				for (int i = 0; i < r.Length-1; i++)
				{
					Debug.DrawLine (
						new Vector3 (v [i], r [i], 0f),
						new Vector3 (v [i+1], r [i+1], 0f), c, Time.deltaTime);
				}
			}
			public static void DrawFunc (float [] v, float speed, float a,
				Mathm.MathFuncAToMinusAExp func, Color c)
			{
				float [] r = Mathm.FuncAToMinusAExpCompute (v, speed, a, func);

				for (int i = 0; i < r.Length-1; i++)
				{
					Debug.DrawLine (
						new Vector3 (v [i], r [i], 0f),
						new Vector3 (v [i+1], r [i+1], 0f), c, Time.deltaTime);
				}
			}


			public static void DrawSquare (Vector3 pos, float size, Color c)
			{
				float sizeHalf = 0.5f*size;
				Vector3 [] p = new Vector3[]
				{
					new Vector3 (-1f, -1f, 0f)*sizeHalf + pos,
					new Vector3 (1f, -1f, 0f)*sizeHalf + pos,
					new Vector3 (1f, 1f, 0f)*sizeHalf + pos,
					new Vector3 (-1f, 1f, 0f)*sizeHalf + pos,
				};

				for (int i = 0; i < p.Length-1; i++)
				{
					Debug.DrawLine (p [i], p [i+1], c, Time.deltaTime);
				}
			}
		}


		public class Fourrier
		{
			public static void Init (ref int N, ref int size, ref int passes, ref float [] bflut)
			{
				N = Mathf.Max (16, N);

				size = Mathf.NextPowerOfTwo (N);

				passes = (int)( Mathf.Log ((float)size) / Mathf.Log (2.0f) );

				bflut = BitOper.ButterflyLookupTable (bflut, size, passes);
			}

			public static Vector4 FFT (Vector2 w, Vector4 input1, Vector4 input2) 
			{
				input1.x += w.x*input2.x - w.y*input2.y;
				input1.y += w.y*input2.x + w.x*input2.y;
				input1.z += w.x*input2.z - w.y*input2.w;
				input1.w += w.y*input2.z + w.x*input2.w;

				return input1;
			}

			public static Vector2 FFT (Vector2 w, Vector2 input1, Vector2 input2) 
			{
				input1.x += w.x*input2.x - w.y*input2.y;
				input1.y += w.y*input2.x + w.x*input2.y;

				return input1;
			}

			public static int FFTAction (int startIdx, Vector2 [,] data, int passes, int size, 
				float [] butterflyLookupTable)
			{
				int x; int y; int i;
				int idx = 0; int idx1; int bftIdx;
				int X; int Y;
				Vector2 w;

				int j = startIdx;

				for (i = 0; i < passes; i++, j++) 
				{
					idx = j%2;
					idx1 = (j+1)%2;

					for(x = 0; x < size; x++)
					{
						for(y = 0; y < size; y++)
						{
							bftIdx = 4*(x + i*size);

							X = (int)butterflyLookupTable [bftIdx + 0];
							Y = (int)butterflyLookupTable [bftIdx + 1];
							w.x = butterflyLookupTable [bftIdx + 2];
							w.y = butterflyLookupTable [bftIdx + 3];

							data[idx, x + y*size] = FFT (w, data[idx1, X + y*size], data[idx1, Y + y*size]);
						}
					}
				}

				for (i = 0; i < passes; i++, j++) 
				{
					idx = j%2;
					idx1 = (j+1)%2;

					for(x = 0; x < size; x++)
					{
						for(y = 0; y < size; y++)
						{
							bftIdx = 4*(y + i*size);

							X = (int)butterflyLookupTable [bftIdx + 0];
							Y = (int)butterflyLookupTable [bftIdx + 1];
							w.x = butterflyLookupTable [bftIdx + 2];
							w.y = butterflyLookupTable [bftIdx + 3];

							data[idx, x + y*size] = FFT (w, data[idx1, x + X*size], data[idx1, x + Y*size]);
						}
					}
				}

				return idx;
			}

			public static int FFTAction (int startIdx, Vector4 [,] data, int passes, int size, 
				float [] butterflyLookupTable)
			{
				int x; int y; int i;
				int idx = 0; int idx1; int bftIdx;
				int X; int Y;
				Vector2 w;

				int j = startIdx;

				for (i = 0; i < passes; i++, j++) 
				{
					idx = j%2;
					idx1 = (j+1)%2;

					for(x = 0; x < size; x++)
					{
						for(y = 0; y < size; y++)
						{
							bftIdx = 4*(x + i*size);

							X = (int)butterflyLookupTable [bftIdx + 0];
							Y = (int)butterflyLookupTable [bftIdx + 1];
							w.x = butterflyLookupTable [bftIdx + 2];
							w.y = butterflyLookupTable [bftIdx + 3];

							data [idx, x + y*size] = FFT (w, data[idx1, X + y*size], data[idx1, Y + y*size]);
						}
					}
				}

				for (i = 0; i < passes; i++, j++) 
				{
					idx = j%2;
					idx1 = (j+1)%2;

					for(x = 0; x < size; x++)
					{
						for(y = 0; y < size; y++)
						{
							bftIdx = 4*(y + i*size);

							X = (int)butterflyLookupTable [bftIdx + 0];
							Y = (int)butterflyLookupTable [bftIdx + 1];
							w.x = butterflyLookupTable [bftIdx + 2];
							w.y = butterflyLookupTable [bftIdx + 3];

							data [idx, x + y*size] = FFT (w, data[idx1, x + X*size], data[idx1, x + Y*size]);
						}
					}
				}

				return idx;
			}

		}


		public class Inp
		{
			public static bool MouseMoved (ref Vector2 lastPos, Event e, float eps)
			{
				eps = Mathf.Max (0.1f, eps);

				float d = (e.mousePosition - lastPos).magnitude;

				if (d > eps)
				{
					lastPos = e.mousePosition;

					return true;
				}

				return false;
			}
		}


		public class Liq
		{
			
		}


		public class Mathm
		{
			public class Norms
			{

			}

			#region tooSmall
			public const int eps5 = 5;
			public const int eps10 = 10;
			public const int eps20 = 20;
			public const int eps40 = 40;

			public static float Eps (int n)
			{
				n = Mathf.Max (1, n);

				return Mathf.Pow (10f, -1f * (float)n);
			}

			public static float TendTo (float v, float l, int n)
			{
				float d = v-l;

				float dAbs = Mathf.Abs (d);

				float eps = Eps (n);

				if (d > 0f)
				{
					if (dAbs < eps)
					{
						v = l + eps;
					}
				}
				else if (d < 0f)
				{
					if (dAbs < eps)
					{
						v = l - eps;
					}
				}
				else if (d == 0f)
				{
					v = l + eps;
				}

				return v;
			}
			#endregion tooSmall

			#region funcCompute
			public delegate float MathFunc (float v);
			public delegate float MathFuncSegment3 (float v, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3);
			public delegate float MathFuncSegment (float v, Vector2 min, Vector2 max);
			public delegate float MathFuncExpSegment (float v, float speed, Vector2 min, Vector2 max);
			public delegate float MathFuncSigmoid (float v, float speed, Vector2 min, Vector2 max, bool inv);
			public delegate float MathFuncHyperbol (float v, float speed, float x_0, float x_1);
			public delegate float MathFuncAToMinusAExp (float v, float speed, float a);

			public static float [] FuncSegment3Compute (float [] v, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3,
				MathFuncSegment3 fun)
			{
				float [] r = new float [v.Length];

				for (int i = 0; i < r.Length; i++)
				{
					r [i] = fun (v [i], p0, p1, p2, p3);
				}

				return r;
			}
			public static float [] FuncSegmentCompute (float [] v, Vector2 min, Vector2 max,
				MathFuncSegment fun)
			{
				float [] r = new float [v.Length];

				for (int i = 0; i < r.Length; i++)
				{
					r [i] = fun (v [i], min, max);
				}

				return r;
			}

			public static float [] FuncExpSegmentCompute (float [] v, float speed, Vector2 min, Vector2 max,
				MathFuncExpSegment fun)
			{
				float [] r = new float [v.Length];

				for (int i = 0; i < r.Length; i++)
				{
					r [i] = fun (v [i], speed, min, max);
				}

				return r;
			}
			public static float [] FuncHyperbolCompute (float [] v, float speed, float x_0, float x_1,
				MathFuncHyperbol fun)
			{
				float [] r = new float [v.Length];

				for (int i = 0; i < r.Length; i++)
				{
					r [i] = fun (v [i], speed, x_0, x_1);
				}

				return r;
			}
			public static float [] FuncSigmoidCompute (float [] v, float speed, Vector2 min, Vector2 max, bool inv,
				MathFuncSigmoid fun)
			{
				float [] r = new float [v.Length];

				for (int i = 0; i < r.Length; i++)
				{
					r [i] = fun (v [i], speed, min, max, inv);
				}

				return r;
			}
			public static float [] FuncCompute (float [] v, MathFunc fun)
			{
				float [] r = new float [v.Length];

				for (int i = 0; i < r.Length; i++)
				{
					r [i] = fun (v [i]);
				}

				return r;
			}
			public static float [] FuncAToMinusAExpCompute (float [] v, float speed, float a,
				MathFuncAToMinusAExp fun)
			{
				float [] r = new float [v.Length];

				for (int i = 0; i < r.Length; i++)
				{
					r [i] = fun (v [i], speed, a);
				}

				return r;
			}
			#endregion funcCompute

			#region funcAxis
			public static float [] Ddd (float min, float max, int n)
			{
				float l = max-min;
				float nF = (float)n;

				List<float> v = new List<float>();
				for (int i = 0; i < n; i++)
				{
					float iF = (float)i; 
					v.Add ( min + iF*(l/nF) );
				}

				return v.ToArray ();
			}
			#endregion funcAxis
			public static float Segment3 (float v, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
			{
				float r = p0.y;

				if (v > p3.x)
				{
					r = p3.y;
				}

				else if (v > p2.x)
				{
					r = Segment (v, p2, p3);
				}
				else if (v > p1.x)
				{
					r = Segment (v, p1, p2);
				}
				else if (v > p0.x)
				{
					r = Segment (v, p0, p1);
				}

				else
				{
					r = p0.y;
				}

				return r;
			}
			public static float Segment (float v, Vector2 min, Vector2 max)
			{
				v = Mathf.Clamp (v, min.x+Eps (eps10), max.x-Eps (eps10));
				float x_x0 = v - min.x;
				float y_0 = min.y;
				float z = (max.y - min.y) / (max.x - min.x);

				return x_x0*z + y_0;
			}
			public static float ExpSegment (float v, float speed, Vector2 min, Vector2 max)
			{
				speed = Mathf.Abs (speed);

				v = Mathf.Clamp (v, min.x+Eps (eps10), max.x-Eps (eps10));
				float y0_y1 = min.y - max.y;
				float y_1 = max.y;
				float z = Hyperbol (v, speed, min.x, max.x);

				return y0_y1*Mathf.Exp (z) + y_1;
			}


			public static float TwoVerticalAsym (float v, float speed, float x_0, float x_1)
			{
				v = TendTo (v, x_0, eps10);
				v = TendTo (v, x_1, eps10);

				return speed / ( (v-x_0) * (v-x_1) );
			}
			public static float Hyperbol (float v, float speed, float x_0, float x_1)
			{
				v = TendTo (v, x_1, eps10);

				return speed * ( (v-x_0) / (v-x_1) );
			}

			public static float ArcTanH (float v)
			{
				v = Mathf.Clamp (v, -1f+Eps (10), 1f-Eps (10));

				return 0.5f*Mathf.Log ((1f+v)/(1f-v));
			}

			public static float Sigmoid (float v)
			{
				return 1f / (1f + Mathf.Exp ((-1f)*v));
			}
			public static float Sigmoid (float v, float speed)
			{
				return 1f / (1f + Mathf.Exp ((-1f)*speed*v));
			}
			public static float Sigmoid (float v, float speed, Vector2 min, Vector2 max, bool inv)
			{
				float n = 2f*Mathf.InverseLerp (min.x, max.x, v) - 1f;
				float t = ArcTanH (n);

				if (inv)
				{
					t = Mathf.Clamp (t, 0f+Eps (10), 1f-Eps (10));

					return (-1f/speed==0f?1f:speed) * Mathf.Log ( (1f-t)/t );
				}
				else
				{
					return 1f / (1f + Mathf.Exp ((-1f)*t*speed));
				}
			}

			public static float AToMinusAExp (float v, float speed, float a)
			{
				v = Mathf.Max (0f, v);
				float exp = 2f*Mathf.Exp (-speed*v);

				return a*(exp - 1f);
			}

			public static float Sharpen (float v, float ints)
			{
				float r = Mathf.Clamp (v, 0f, 1f);

				ints *= 0.5f;

				float from_05 = r - 0.5f;
				float signFrom_05 = Mathf.Sign (from_05);

				r += ints*signFrom_05;
				r = Mathf.Clamp (r, 0f, 1f);

				return r;
			}

			public static float [,] Smooth2D (float [,] inp, int passes)
			{
				float [,] r = inp;

				for (int p = 0; p < passes; p++)
				{
					for (int i = 0; i < r.GetLength (0); i++)
					{
						for (int j = 0; j < r.GetLength (1) - 1; j++)
						{
							r [i, j] = 0.5f*(inp [i, j] + inp [i, j+1]);
						}
					}

					for (int j = 0; j < r.GetLength (1); j++)
					{
						for (int i = 0; i < r.GetLength (0) - 1; i++)
						{
							r [i, j] = 0.5f*(inp [i, j] + inp [i+1, j]);
						}
					}
				}

				return r;
			}

			public static int TwoDimIndex (int i, int j, int w)
			{
				return i + j*w;
			}

			public static float [,] InverseLerp (ref float [,] values)
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


			public static bool InRange (int v, int min, int max)
			{
				return v >= min && v < max;
			}

			public static bool InRange (float v, float min, float max)
			{
				return v >= min && v < max;
			}
		
		
			public static float Angle (Vector2 frome, Vector2 to, bool deg)
			{
				return Angle (to, deg) - Angle (frome, deg);
			}

			public static float Angle (Vector2 v, bool deg)
			{
				float r = 0f;

				float h = v.magnitude;
				float halfPi = Mathf.PI*0.5f;

				if (v.x > 0f && v.y > 0f)
				{
					r = Mathf.Acos (v.x/h);
				}
				else if (v.x < 0f && v.y > 0f)
				{
					r = Mathf.Acos (v.y/h) + halfPi;
				}
				else if (v.x < 0f && v.y < 0f)
				{
					r = Mathf.Acos (-v.x/h) + 2f*halfPi;
				}
				else if (v.x > 0f && v.y < 0f)
				{
					r = Mathf.Acos (-v.y/h) + 3f*halfPi;
				}
				else if (v.x == 0f && v.y == 0f)
				{
					r = 0f;
				}
				else if (v.x == 0f)
				{
					if (v.y > 0f)
						r = halfPi;
					else
						r = 3f*halfPi;
				}
				else if (v.y == 0f)
				{
					if (v.x > 0f)
						r = 0f;
					else
						r = 2f*halfPi;
				}


				if (deg)
					r *= Mathf.Rad2Deg; 

				return r;
			}
		}

		public class Meca
		{
			public static Vector3 EulerInteg (Vector3 f, float dt, Vector3 s0)
			{
				return s0 + f*dt;
			}
		}

		public class Meshm
		{
			public static Mesh HorizontalPlan (int size) 
			{

				Vector3[] vertices = new Vector3[size*size];
				Vector2[] texcoords = new Vector2[size*size];
				Vector3[] normals = new Vector3[size*size];
				int[] indices = new int[size*size*6];

				for(int x = 0; x < size; x++)
				{
					for(int y = 0; y < size; y++)
					{
						Vector2 uv = new Vector3( (float)x / (float)(size-1), (float)y / (float)(size-1) );
						Vector3 pos = new Vector3(x, 0.0f, y);
						Vector3 norm = new Vector3(0.0f, 1.0f, 0.0f);

						texcoords[x+y*size] = uv;
						vertices[x+y*size] = pos;
						normals[x+y*size] = norm;
					}
				}

				int num = 0;
				for(int x = 0; x < size-1; x++)
				{
					for(int y = 0; y < size-1; y++)
					{
						indices[num++] = x + y * size;
						indices[num++] = x + (y+1) * size;
						indices[num++] = (x+1) + y * size;

						indices[num++] = x + (y+1) * size;
						indices[num++] = (x+1) + (y+1) * size;
						indices[num++] = (x+1) + y * size;
					}
				}

				Mesh mesh = new Mesh();

				mesh.vertices = vertices;
				mesh.uv = texcoords;
				mesh.triangles = indices;
				mesh.normals = normals;

				return mesh;
			}

			public enum HowInverseUv
			{
				none,
				x,
				y,
				both,
			}
			public static Mesh VerticalPlan (int size, HowInverseUv hiUv, bool centered)
			{
				size = Mathf.Max (size, 2);

				Vector3[] vertices = new Vector3[size*size];
				Vector2[] texcoords = new Vector2[size*size];
				Vector3[] normals = new Vector3[size*size];
				int[] indices = new int[size*size*6];

				for(int x = 0; x < size; x++)
				{
					for(int y = 0; y < size; y++)
					{
						float sizeM1F = (float)(size-1);

						float xF = (float)x;
						float yF = (float)y;
						float xUvF = (float)x;
						float yUvF = (float)y;
						float xPosF = (float)x;
						float yPosF = (float)y;
						Vector3 norm = new Vector3(0.0f, 0.0f, 1.0f);
						switch (hiUv)
						{
						case HowInverseUv.both:
							xUvF = sizeM1F - xF;
							yUvF = sizeM1F - yF;
							break;

						case HowInverseUv.none:
							break;

						case HowInverseUv.x:
							xUvF = sizeM1F - xF;
							break;

						case HowInverseUv.y:
							yUvF = sizeM1F - yF;
							break;
						}
						Vector2 uv = new Vector3( xUvF/sizeM1F, yUvF/sizeM1F );
						if (centered)
						{
							xPosF = xF - 0.5f*sizeM1F;
							yPosF = yF - 0.5f*sizeM1F;
						}
						Vector3 pos = new Vector3(xPosF, yPosF, 0f);

						texcoords[x+y*size] = uv;
						vertices[x+y*size] = pos;
						normals[x+y*size] = norm;
					}
				}

				int num = 0;
				for(int x = 0; x < size-1; x++)
				{
					for(int y = 0; y < size-1; y++)
					{
						indices[num++] = x + y * size;
						indices[num++] = x + (y+1) * size;
						indices[num++] = (x+1) + y * size;

						indices[num++] = x + (y+1) * size;
						indices[num++] = (x+1) + (y+1) * size;
						indices[num++] = (x+1) + y * size;
					}
				}

				Mesh mesh = new Mesh();

				mesh.vertices = vertices;
				mesh.uv = texcoords;
				mesh.triangles = indices;
				mesh.normals = normals;

				return mesh;
			}

		}


		public class Ocean
		{
			public static void WavesFFT (float t, int N, float length, 
				Vector2 [,] heightBuffer, Vector4 [,] displacementBuffer,
				Vector4 [,] slopeBuffer, Vector3 [] position,
				Vector3 [] vertices, Vector3 [] normals,
				int passes, int size, float [] butterflyLookupTable,
				float [] dispersionTable, Vector2 [] spectrum, Vector2 [] spectrum_conj )
			{
				float kx, kz, len, lambda = -1.0f;

				int index, index1;

				int Nplus1 = N + 1;
			
				for (int m_prime = 0; m_prime < N; m_prime++) 
				{
					kz = Mathf.PI * (2.0f * m_prime - N) / length;
			
					for (int n_prime = 0; n_prime < N; n_prime++) 
					{
						kx = Mathf.PI*(2 * n_prime - N) / length;
						len = Mathf.Sqrt(kx * kx + kz * kz);
						index = m_prime * N + n_prime;
			
						Vector2 c = InitSpectrum (t, n_prime, m_prime, dispersionTable, Nplus1,
							spectrum, spectrum_conj);
			
						heightBuffer[1,index].x = c.x;
						heightBuffer[1,index].y = c.y;
			
						slopeBuffer[1,index].x = -c.y*kx;
						slopeBuffer[1,index].y = c.x*kx;
			
						slopeBuffer[1,index].z = -c.y*kz;
						slopeBuffer[1,index].w = c.x*kz;
			
						if (len < 0.000001f) 
						{
							displacementBuffer[1,index].x = 0.0f;
							displacementBuffer[1,index].y = 0.0f;
							displacementBuffer[1,index].z = 0.0f;
							displacementBuffer[1,index].w = 0.0f;
						} 
						else 
						{
							displacementBuffer[1,index].x = -c.y * -(kx/len);
							displacementBuffer[1,index].y = c.x * -(kx/len);
							displacementBuffer[1,index].z = -c.y * -(kz/len);
							displacementBuffer[1,index].w = c.x * -(kz/len);
						}	
					}
				}
			
				Fourrier.FFTAction (0, heightBuffer, passes, size, butterflyLookupTable);
				Fourrier.FFTAction (0, slopeBuffer, passes, size, butterflyLookupTable);
				Fourrier.FFTAction (0, displacementBuffer, passes, size, butterflyLookupTable);
			
			
				int sign;
				float[] signs = new float[]{ 1.0f, -1.0f };
				Vector3 n;
			
				for (int m_prime = 0; m_prime < N; m_prime++) 
				{
					for (int n_prime = 0; n_prime < N; n_prime++) 
					{
						index  = m_prime * N + n_prime;			// index into buffers
						index1 = m_prime * Nplus1 + n_prime;	// index into vertices
			
						sign = (int)signs[(n_prime + m_prime) & 1];
			
						// height
						vertices[index1].y = heightBuffer[1, index].x * sign;
			
						// displacement
						vertices[index1].x = position[index1].x + displacementBuffer[1, index].x * lambda * sign;
						vertices[index1].z = position[index1].z + displacementBuffer[1, index].z * lambda * sign;
			
						// normal
						n = new Vector3(-slopeBuffer[1, index].x * sign, 1.0f, -slopeBuffer[1, index].z * sign);
						n.Normalize();
			
						normals[index1].x =  n.x;
						normals[index1].y =  n.y;
						normals[index1].z =  n.z;
			
						// for tiling
						if (n_prime == 0 && m_prime == 0) 
						{
							vertices[index1 + N + Nplus1 * N].y = heightBuffer[1, index].x * sign;
			
							vertices[index1 + N + Nplus1 * N].x = position[index1 + N + Nplus1 * N].x + displacementBuffer[1, index].x * lambda * sign;
							vertices[index1 + N + Nplus1 * N].z = position[index1 + N + Nplus1 * N].z + displacementBuffer[1, index].z * lambda * sign;
			
							normals[index1 + N + Nplus1 * N].x =  n.x;
							normals[index1 + N + Nplus1 * N].y =  n.y;
							normals[index1 + N + Nplus1 * N].z =  n.z;
						}
						if (n_prime == 0) 
						{
							vertices[index1 + N].y = heightBuffer[1, index].x * sign;
			
							vertices[index1 + N].x = position[index1 + N].x + displacementBuffer[1, index].x * lambda * sign;
							vertices[index1 + N].z = position[index1 + N].z + displacementBuffer[1, index].z * lambda * sign;
			
							normals[index1 + N].x =  n.x;
							normals[index1 + N].y =  n.y;
							normals[index1 + N].z =  n.z;
						}
						if (m_prime == 0) 
						{
							vertices[index1 + Nplus1 * N].y = heightBuffer[1, index].x * sign;
			
							vertices[index1 + Nplus1 * N].x = position[index1 + Nplus1 * N].x + displacementBuffer[1, index].x * lambda * sign;
							vertices[index1 + Nplus1 * N].z = position[index1 + Nplus1 * N].z + displacementBuffer[1, index].z * lambda * sign;
			
							normals[index1 + Nplus1 * N].x =  n.x;
							normals[index1 + Nplus1 * N].y =  n.y;
							normals[index1 + Nplus1 * N].z =  n.z;
						}
					}
				}
			}



			public static Vector2 InitSpectrum (float t, int n_prime, int m_prime, 
				float [] dispersionTable, int Nplus1, Vector2 [] spectrum, Vector2 [] spectrum_conj) 
			{
				int index = m_prime * Nplus1 + n_prime;

				float omegat = dispersionTable [index] * t;

				float cos = Mathf.Cos(omegat);
				float sin = Mathf.Sin(omegat);

				float c0a = spectrum [index].x*cos - spectrum [index].y*sin;
				float c0b = spectrum [index].x*sin + spectrum [index].y*cos;

				float c1a = spectrum_conj [index].x*cos - spectrum_conj [index].y*-sin;
				float c1b = spectrum_conj [index].x*-sin + spectrum_conj [index].y*cos;

				return new Vector2 (c0a+c1a, c0b+c1b);
			}

			public static float PhillipsSpectrum (int n_prime, int m_prime, int N, float length, 
				Vector2 windDirection, Vector2 windSpeed, float waveAmp, float g) 
			{
				Vector2 k = 
					new Vector2(Mathf.PI * (2 * n_prime - N) / length, Mathf.PI * (2 * m_prime - N) / length);

				float k_length  = k.magnitude;

				if (k_length < 0.000001f) 
					return 0f;


				float k_length2 = k_length  * k_length;
				float k_length4 = k_length2 * k_length2;

				k.Normalize();

				float k_dot_w = Vector2.Dot(k, windDirection);
				float k_dot_w2 = k_dot_w * k_dot_w * k_dot_w * k_dot_w * k_dot_w * k_dot_w;


				float w_length  = windSpeed.magnitude;
				float L = w_length * w_length / g;
				float L2 = L * L;


				float damping = 0.001f;
				float l2 = L2 * damping * damping;


				return waveAmp * 
					Mathf.Exp (-1.0f / (k_length2*L2)) / k_length4*k_dot_w2*Mathf.Exp(-k_length2*l2);
			}
		
			public static Vector2 PhillipsSpectrumVector2 (int n_prime, int m_prime, int N, float length, 
				Vector2 windDirection, Vector2 windSpeed, float waveAmp, float g) 
			{
				Vector2 r = Ran.GaussRand ();

				return r * Mathf.Sqrt (PhillipsSpectrum (
					n_prime, m_prime, N, length, 
					windDirection, windSpeed, waveAmp, g) / 2.0f);
			}

			public static float Dispersion(int n_prime, int m_prime, float length, int N, float g) 
			{
				float w_0 = 2.0f * Mathf.PI / 200.0f;

				float kx = Mathf.PI * (2 * n_prime - N) / length;
				float kz = Mathf.PI * (2 * m_prime - N) / length;

				return Mathf.Floor(Mathf.Sqrt(g * Mathf.Sqrt(kx * kx + kz * kz)) / w_0) * w_0;
			}
		}


		public class Ran
		{
			public static Vector3 Rand (Vector3 min, Vector3 max)
			{
				return new Vector3 (
					UnityEngine.Random.Range (min.x, max.x),
					UnityEngine.Random.Range (min.y, max.y),
					UnityEngine.Random.Range (min.z, max.z));
			}

			public static float UniformRandom (int randMax)
			{
				return (float)UnityEngine.Random.Range (0, randMax) / (float)randMax;
			}

			public static Complex GaussRandComplex ()
			{
				float x1;
				float x2;
				float w;

				do
				{
					x1 = 2f*UnityEngine.Random.value - 1f;
					x2 = 2f*UnityEngine.Random.value - 1f;
					w = x1*x1 + x2*x2;
				}
				while (w >= 1f);

				w = Mathf.Sqrt ( (-2f*Mathf.Log (w)) / w);

				return new Complex (
					float.IsNaN (x1*w)? 0.75f: x1*w,
					float.IsNaN (x2*w)? 0.75f: x2*w);

				//return new Complex (x1*w, x2*w);
			}

			public static Vector2 GaussRand ()
			{
				float x1;
				float x2;
				float w;

				do
				{
					x1 = 2f*UnityEngine.Random.value - 1f;
					x2 = 2f*UnityEngine.Random.value - 1f;
					w = x1*x1 + x2*x2;
				}
				while (w >= 1f);

				w = Mathf.Sqrt ( (-2f*Mathf.Log (w)) / w);

				return new Vector2 (
					float.IsNaN (x1*w)? 0.75f: x1*w,
					float.IsNaN (x2*w)? 0.75f: x2*w);
			}
		}

		public class Rectan
		{
			public static Rect CenteredToTransform (Transform tr)
			{
				return new Rect (
					new Vector2 (tr.position.x, tr.position.y) +
					new Vector2 (-tr.localScale.x*0.5f, - tr.localScale.y*0.5f),
					new Vector2 (tr.localScale.x, tr.localScale.y));
			}


			public static Rect AddMargin (Rect container, float margin, bool yUpward)
			{
				Rect ret = new Rect (Vector2.zero, Vector2.one);

				if (container.size.x <= 0f || container.size.y <= 0f)
				{
					return ret;
				}

				ret = new Rect (container.position + new Vector2 (margin, margin*(yUpward?-1f:1f)),
					container.size - 2f*Vector2.one*margin);

				return ret;
			}

			public static Vector2 AbsolutePosition (Rect container, Vector2 normalizedPosition)
			{
				return container.position + new Vector2 (
					container.width * normalizedPosition.x,
					container.height * normalizedPosition.y);
			}

			public static Vector2 NormalizedPosition (Rect container, Vector2 absolutePosition)
			{
				Vector2 relativePosition = absolutePosition - container.position;

				return new Vector2 (
					relativePosition.x / container.width, 
					relativePosition.y / container.height);
			}

			public static Vector2 FromRectToRect (Vector2 position, Rect from, Rect to)
			{
				Vector2 relativePosition = position - from.position;

				Vector2 relativeRectPosition = from.position- to.position;

				return relativeRectPosition + relativePosition;
			}

			public static Rect Zoom (Rect initRect, float speed)
			{
				return new Rect (initRect.position + Vector2.one*speed, 
					initRect.size - 2f*Vector2.one*speed);
			}

			public static Rect Zoom (Rect initRect, float speed, bool multiplicative)
			{
				Vector2 newSize = initRect.size*speed;

				return new Rect (initRect.position + 0.5f *(initRect.size-newSize), 
					newSize);
			}
		}

		public class Rend
		{

		}


		public class Terr
		{
			public static float [,] GetInterpolatedHeights (ref TerrainData td, float [,] heights)
			{
				float [,] interpHeights = heights;

				int condens = 20;

				for (int i = 0; i < heights.GetLength (0)*condens; i++)
				{
					for (int j = 0; j < heights.GetLength (1)*condens; j++)
					{
						float normI = Mathf.InverseLerp (0f, (float)(heights.GetLength (0)*condens), (float)i);
						float normJ = Mathf.InverseLerp (0f, (float)(heights.GetLength (1)*condens), (float)j);

						int iIndex = Mathf.Clamp (i/condens, 0, heights.GetLength (0));
						int jIndex = Mathf.Clamp (j/condens, 0, heights.GetLength (1));

						interpHeights [iIndex, jIndex] = td.GetInterpolatedHeight (normJ, normI)/(td.size.y==0f?1f:td.size.y);
					}
				}

				return interpHeights;
			}


			public static float Exposure (ref Terrain terrain, float normI, float normJ, Vector3 dir)
			{
				return Mathf.InverseLerp (-1f, 1f, Vector3.Dot (SlopeV3 (ref terrain, normI, normJ).normalized, dir.normalized));
			} 


			public static Vector3 SlopeV3 (ref Terrain terrain, float normI, float normJ)
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

				return new Vector3 (dhOdx, -1f, dhOdY);

			} 


			public static float Slope (ref Terrain terrain, float normI, float normJ)
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

		}

		public class Tex
		{
			public class TexturePosDepth
			{
				public TexturePosDepth (Texture2D setTexture, float setX, float setY, float setW, float setH)
				{
					texture = setTexture;

					X = setX;
					Y = setY;
					W = setW;
					H = setH;
				}
				public TexturePosDepth (Texture2D setTexture, float setX, float setY)
				{
					texture = setTexture;

					X = setX;
					Y = setY;
				}

				public Texture2D texture;
				public int depth;

				public float X
				{
					get {return x;}
					set	{x = Mathf.Clamp (value, 0f, 100f);}
				}
				float x;
				public int Xint
				{
					get {return xInt;}
				}
				int xInt;

				public float Y
				{
					get {return y;}
					set {y = Mathf.Clamp (value, 0f, 100f);}
				}
				float y;
				public int Yint
				{
					get {return yInt;}
				}
				int yInt;

				public float W
				{
					get {return w;}
					set {w = Mathf.Clamp (value, 2f, 100f);}
				}
				float w;
				public int Wint
				{
					get {return wInt;}
				}
				int wInt;

				public float H
				{
					get {return h;}
					set {h = Mathf.Clamp (value, 2f, 100f);}
				}
				float h;
				public int Hint
				{
					get {return hInt;}
				}
				int hInt;


				public void InTexturePos (int wBig, int hBig)
				{
					xInt = Mathf.Clamp (Mathf.RoundToInt ((x/100f)*(float)wBig), 0, wBig);
					yInt = Mathf.Clamp (Mathf.RoundToInt ((y/100f)*(float)hBig), 0, hBig);

					wInt = texture.width;
					hInt = texture.height;
				}

				void InTextureSize (int wBig, int hBig)
				{
					wInt = Mathf.Clamp (Mathf.RoundToInt ((w/100f)*(float)wBig), 2, wBig);
					hInt = Mathf.Clamp (Mathf.RoundToInt ((h/100f)*(float)hBig), 2, hBig);
				}

				public void InTexturePosSize (int wBig, int hBig)
				{
					InTexturePos (wBig, hBig);
					InTextureSize (wBig, hBig);
				}

				public Color GetPixel (int xBig, int yBig)
				{
					int xInsideTexture = xBig - xInt;
					int yInsideTexture = yBig - yInt;

					return texture.GetPixel (xInsideTexture, yInsideTexture);
				}
			}

			#region pixelsDetection
			public static float [,] DetectedPixelsF (Color[] id, ref Texture2D tex, float farSpeed)
			{
				Color[] colors = tex.GetPixels ();

				float [,] r = new float [id.Length, colors.Length];

				for (int j = 0; j < r.GetLength (0); j++)
				{
					for (int i = 0; i < r.GetLength (1); i++)
					{
						Color c0 = id [j];
						Color c1 = colors [i];

						float d = Vector3.Distance (
							new Vector3 (c0.r, c0.g, c0.b),
							new Vector3 (c1.r, c1.g, c1.b));

						//float pR = Mathf.Exp (-farSpeed*Mathf.Abs (c0.r-c1.r));
						//float pG = Mathf.Exp (-farSpeed*Mathf.Abs (c0.g-c1.g));
						//float pB = Mathf.Exp (-farSpeed*Mathf.Abs (c0.b-c1.b));
					
						//r [j, i] = Vector3.Magnitude (new Vector3 (pR, pG, pB));
						r [j, i] = Mathf.Exp (-farSpeed*d);
					}
				}

				return r;
			}

			public static bool [,] DetectedPixels (Color[] id, ref Texture2D tex, float t)
			{
				Color[] colors = tex.GetPixels ();

				bool [,] r = new bool[id.Length, colors.Length];

				for (int j = 0; j < r.GetLength (0); j++)
				{
					for (int i = 0; i < r.GetLength (1); i++)
					{
						r [j, i] = false;

						r [j, i] = InRange (id [j], colors [i], t);
					}
				}

				return r;
			}

			public static bool InRange (Color id, Color c, float t)
			{

				return 
					Mathm.InRange (c.r, id.r - t, id.r + t) &&
					Mathm.InRange (c.g, id.g - t, id.g + t) &&
					Mathm.InRange (c.b, id.b - t, id.b + t);
			}
			#endregion pixelsDetection

			public static Texture2D InvertTexturesPixelsValue (ref Texture2D tex_0)
			{
				Texture2D r = new Texture2D (tex_0.width, tex_0.height);

				Color [] colores = r.GetPixels ();
				Color [] colores_0 = tex_0.GetPixels ();

				for (int i = 0; i < colores.Length; i++)
				{
					colores [i] = Color.HSVToRGB (
						Col.RGBToHSV (colores_0 [i])[0],
						Col.RGBToHSV (colores_0 [i])[1],
						1f - Col.RGBToHSV (colores_0 [i])[2] );
				}

				r.SetPixels (colores);
				r.Apply ();

				return r;
			}

			#region transform
			public enum MirrorTexture2D_Direction
			{
				rotate90,

				rotateMinus90,

				mirrorXAndY,

				mirrorY,

				mirrorX,
			}

			public static Texture2D MirrorAndRotate (Texture2D tex, MirrorTexture2D_Direction mtd, bool texToTex)
			{
				List <Color> rL = new List<Color> ();

				for (int x = 0; x < tex.width; x++)
				{
					for (int y = 0; y < tex.height; y++)
					{
						switch (mtd)
						{
						case MirrorTexture2D_Direction.rotate90:
							rL.Add (tex.GetPixel (tex.width-1 - x, y));
							break;

						case MirrorTexture2D_Direction.rotateMinus90:
							rL.Add (tex.GetPixel (x, tex.height-1 - y));
							break;

						case MirrorTexture2D_Direction.mirrorXAndY:
							rL.Add (tex.GetPixel (tex.width-1 - x, tex.height-1 - y));
							break;
						}

					}
				}

				for (int y = 0; y < tex.height; y++)
				{
					for (int x = 0; x < tex.width; x++)
					{
						switch (mtd)
						{
						case MirrorTexture2D_Direction.mirrorY: 
							rL.Add (tex.GetPixel (x, tex.height-1 - y));
							break;

						case MirrorTexture2D_Direction.mirrorX: 
							rL.Add (tex.GetPixel (tex.width-1 - x, y));
							break;
						}
					}
				}

				Texture2D r = new Texture2D (tex.width, tex.height);
				r.SetPixels (rL.ToArray ());
				r.Apply ();

				return r;
			}
			public static Color [] MirrorAndRotate (Texture2D tex, MirrorTexture2D_Direction mtd)
			{
				List <Color> rL = new List<Color> ();

				for (int x = 0; x < tex.width; x++)
				{
					for (int y = 0; y < tex.height; y++)
					{
						switch (mtd)
						{
						case MirrorTexture2D_Direction.rotate90:
							rL.Add (tex.GetPixel (tex.width-1 - x, y));
							break;

						case MirrorTexture2D_Direction.rotateMinus90:
							rL.Add (tex.GetPixel (x, tex.height-1 - y));
							break;

						case MirrorTexture2D_Direction.mirrorXAndY:
							rL.Add (tex.GetPixel (tex.width-1 - x, tex.height-1 - y));
							break;
						}

					}
				}

				for (int y = 0; y < tex.height; y++)
				{
					for (int x = 0; x < tex.width; x++)
					{
						switch (mtd)
						{
						case MirrorTexture2D_Direction.mirrorY: 
							rL.Add (tex.GetPixel (x, tex.height-1 - y));
							break;

						case MirrorTexture2D_Direction.mirrorX: 
							rL.Add (tex.GetPixel (tex.width-1 - x, y));
							break;
						}
					}
				}


				return rL.ToArray ();
			}

			#endregion transform

			#region gradient
			//public static Color [] Gradient (bool linear, float speed, int width, int height)
			//{
			//	Color [] c = new Color[width*height];
			//
			//	float widthF = (float)width;
			//
			//	Vector2 min = Vector2.zero;
			//	Vector2 max = new Vector2 (widthF, 1f);
			//
			//	for (int i = 0; i < width; i++)
			//	{
			//		for (int j = 0; j < height; j++)
			//		{
			//			float jF = (float)j;
			//
			//			int ind = Mathm.TwoDimIndex (i, j, width);
			//
			//			float v = jF;
			//			float r = linear? Mathm.Segment (v, min, max): Mathm.ExpSegment (v, speed, min, max);
			//
			//			c [ind] = Color.HSVToRGB (0f, 0f, r);
			//		}
			//	}
			//
			//	return c;
			//}
			public static Color [] GradientConcat (bool linear, int number, float speed, int size)
			{
				List<Color> c = new List<Color>();

				for (int i = 0; i < number; i++)
				{
					Color [] cTr = Gradient (linear, number, speed, size);

					for (int j = 0; j < cTr.Length; j++)
					{
						c.Add (cTr [j]);
					}
				}

				Color [] cA = new Color[size*size];
				for (int i = 0; i < cA.Length; i++)
					cA [i] = Color.black;

				for (int i = 0; i < c.Count; i++)
				{
					if (Mathm.InRange (i, 0, cA.Length))
					{
						cA [i] = c [i];
					}
				}

				return cA;
			}
			public static Color [] Gradient (bool linear, int number, float speed, int size)
			{
				float numberF = (float)number;
				float sizeF = (float)size;
				float widthF = sizeF/numberF;
				float heightF = sizeF;

				int width = Mathf.CeilToInt (widthF);
				int height = Mathf.CeilToInt (heightF);

				Color [] c = new Color[width*height];

				Vector2 min = Vector2.zero;
				Vector2 max = new Vector2 (widthF, 1f);

				for (int j = 0; j < height; j++)
				{
					for (int i = 0; i < width; i++)
					{
						float iF = (float)i;

						int ind = Mathm.TwoDimIndex (j, i, height);

						float v = iF;
						float r = linear? Mathm.Segment (v, min, max): Mathm.ExpSegment (v, speed, min, max);

						c [ind] = Color.HSVToRGB (0f, 0f, r);
					}
				}

				return c;
			}
			public static Texture2D Gradient (bool linear, float speed, int size)
			{
				Texture2D tex = new Texture2D (size, size);

				Color [] c = tex.GetPixels ();

				float sizeF = (float)size;
				Vector2 min = Vector2.zero;
				Vector2 max = new Vector2 (sizeF, 1f);

				for (int i = 0; i < tex.width; i++)
				{
					for (int j = 0; j < tex.height; j++)
					{
						float jF = (float)j;

						int ind = Mathm.TwoDimIndex (i, j, tex.width);

						float v = jF;
						float r = linear? Mathm.Segment (v, min, max): Mathm.ExpSegment (v, speed, min, max);

						c [ind] = Color.HSVToRGB (0f, 0f, r);
					}
				}

				tex.SetPixels (c);
				tex.Apply ();

				return tex;
			}

			public static Texture2D Gradient (bool linear, bool multiple, int number, float speed, bool horiz, int size)
			{
				Texture2D tex = new Texture2D (size, size);

				if ( ! multiple)
				{
					tex = Gradient (linear, speed, size);
				}
				else
				{
					tex.SetPixels (GradientConcat (linear, number, speed, size));
					tex.Apply ();
				}

				if ( ! horiz)
					tex = MirrorAndRotate (tex, MirrorTexture2D_Direction.rotate90, true);

				return tex;
			}
			#endregion gradient

			#region Sharpen
			public static Texture2D SharpenValue (Texture2D tex_0, float ints)
			{
				Texture2D r = new Texture2D (tex_0.width, tex_0.height);

				Color [] colores = r.GetPixels ();
				Color [] colores_0 = tex_0.GetPixels ();

				for (int i = 0; i < colores.Length; i++)
				{
					colores [i] = Color.HSVToRGB (
						Col.RGBToHSV (colores_0 [i])[0],
						Col.RGBToHSV (colores_0 [i])[1],
						Mathm.Sharpen (Col.RGBToHSV (colores_0 [i])[2], ints) );
				}

				r.SetPixels (colores);
				r.Apply ();

				return r;
			}
			#endregion Sharpen

			#region blurMezanixFast
			public static void FastBlurMezanix (ref Texture2D tex, int rad, int iter,
				ref Color c, ref int blurPixelCount)
			{
				for (int i = 0; i < iter; i++)
				{
					FastBlurMezanix_BlurImage (ref tex, rad, true, ref c, ref blurPixelCount);
					FastBlurMezanix_BlurImage (ref tex, rad, false, ref c, ref blurPixelCount);
				}
			}
			static void FastBlurMezanix_BlurImage (ref Texture2D tex, int blurSize, bool horz,
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
							FastBlurMezanix_ResetPixel (ref c, ref blurPixelCount);

							for (x = xx; (x < xx + blurSize && x < w); x++)
							{
								FastBlurMezanix_AddPixel (tex.GetPixel (x, yy), ref c, ref blurPixelCount);
							}
							for (x = xx; (x > xx - blurSize && x > 0); x--)
							{
								FastBlurMezanix_AddPixel (tex.GetPixel (x, yy), ref c, ref blurPixelCount);
							}
							FastBlurMezanix_ComputePixel (ref c, ref blurPixelCount);

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
							FastBlurMezanix_ResetPixel (ref c, ref blurPixelCount);

							for (y = yy; (y < yy + blurSize && y < h); y++)
							{
								FastBlurMezanix_AddPixel (tex.GetPixel (xx, y), ref c, ref blurPixelCount);
							}
							for (y = yy; (y > yy - blurSize && y > 0); y--)
							{
								FastBlurMezanix_AddPixel (tex.GetPixel (xx, y), ref c, ref blurPixelCount);
							}
							FastBlurMezanix_ComputePixel (ref c, ref blurPixelCount);

							for (y = yy; y < yy + blurSize && y < h; y++)
							{
								tex.SetPixel (xx, y, new Color (c.r, c.g, c.b, 1f));
							}
						}
					}
				}

				tex.Apply ();
			}
			static void FastBlurMezanix_AddPixel (Color cS, ref Color c, ref int blurPixelCount)
			{
				c += cS;

				blurPixelCount++;
			}
			static void FastBlurMezanix_ResetPixel (ref Color c, ref int blurPixelCount)
			{
				c = new Color (0f, 0f, 0f, 0f);
				blurPixelCount = 0;
			}
			static void FastBlurMezanix_ComputePixel (ref Color c, ref int blurPixelCount)
			{
				c = new Color (
					c.r / (float)blurPixelCount,
					c.g / (float)blurPixelCount,
					c.b / (float)blurPixelCount, 1f);
			}
			#endregion blurMezanixFast



			public static Texture2D Fresnel (int size, float index)
			{
				Texture2D fresnelTex = new Texture2D(size, 1, TextureFormat.Alpha8, false);
				fresnelTex.filterMode = FilterMode.Bilinear;
				fresnelTex.wrapMode = TextureWrapMode.Clamp;
				fresnelTex.anisoLevel = 0;

				for(int x = 0; x < 512; x++)
				{
					float fresnel = 0.0f;
					float costhetai = (float)x/511f;
					float thetai = Mathf.Acos(costhetai);
					float sinthetat = Mathf.Sin(thetai)/index;
					float thetat = Mathf.Asin(sinthetat);

					if(thetai == 0.0f)
					{
						fresnel = (index - 1.0f)/(index + 1.0f);
						fresnel = fresnel * fresnel;
					}
					else
					{
						float fs = Mathf.Sin(thetat - thetai) / Mathf.Sin(thetat + thetai);
						float ts = Mathf.Tan(thetat - thetai) / Mathf.Tan(thetat + thetai);
						fresnel = 0.5f * ( fs*fs + ts*ts );
					}

					fresnelTex.SetPixel(x, 0, new Color(fresnel,fresnel,fresnel,fresnel));
				}

				fresnelTex.Apply();

				return fresnelTex;
			}


			public static Texture2D UniColor (Color col, int width, int height)
			{
				width = Mathf.Max (2, width);
				height = Mathf.Max (2, height);

				Texture2D ret = new Texture2D (width, height, TextureFormat.ARGB32, false, true);

				Color [] cols = ret.GetPixels ();

				for (int i = 0; i < cols.Length; i++)
				{
					cols [i] = col;
				}

				ret.SetPixels (cols);
				ret.Apply ();

				return ret;
			}

			public static void SetTexture (GameObject go, Texture2D setTex)
			{
				if (setTex == null || go == null)
					return;

				Renderer rend = go.GetComponent <Renderer> ();
				if (rend == null)
					return;

				Material mat = rend.material;
				if (mat == null)
					return;

				mat.mainTexture = setTex;
			}

			public static Texture2D ToMs (Texture2D metallic, Texture2D smooth)
			{
				if (metallic == null || smooth == null)
					return null;

				if (metallic.width != smooth.width || metallic.height != smooth.height)
					return null;

				Texture2D r = new Texture2D (metallic.width, metallic.height, TextureFormat.ARGB32, false, true);

				Color [] metallic_c = metallic.GetPixels ();
				Color [] smooth_c = smooth.GetPixels ();

				Color [] r_c = r.GetPixels ();


				float [] metallic_v = new float [metallic_c.Length];
				for (int i = 0; i < metallic_c.Length; i++)
				{
					metallic_v [i] = Col.RGBToHSV (metallic_c [i])[2];
				}

				float [] smooth_v = new float [smooth_c.Length];
				for (int i = 0; i < smooth_c.Length; i++)
				{
					smooth_v [i] = Col.RGBToHSV (smooth_c [i])[2];
				}

				for (int i = 0; i < r_c.Length; i++)
				{
					r_c [i] = new Color (metallic_v [i], 0f, 0f, smooth_v [i]);
				}

				r.SetPixels (r_c);
				r.Apply ();

				return r;
			}
			public static void ToMs (Texture2D metallic, Texture2D smooth, string path, bool ping)
			{
				#if UNITY_EDITOR
				Texture2D r = ToMs (metallic, smooth);


				ToFilePng (r, path, ping);
				#endif
			}	

			public static Texture2D ToMso (Texture2D metallic, Texture2D smooth, Texture2D ao)
			{
				if (metallic == null || smooth == null || ao == null)
					return null;

				if (metallic.width != smooth.width || metallic.height != smooth.height ||
					metallic.width != ao.width || metallic.height != ao.height ||
					smooth.width != ao.width || smooth.height != ao.height)
					return null;

				Texture2D r = new Texture2D (metallic.width, metallic.height, TextureFormat.ARGB32, false, true);

				Color [] metallic_c = metallic.GetPixels ();
				Color [] smooth_c = smooth.GetPixels ();
				Color [] ao_c = ao.GetPixels ();

				Color [] r_c = r.GetPixels ();


				float [] metallic_v = new float [metallic_c.Length];
				for (int i = 0; i < metallic_c.Length; i++)
				{
					metallic_v [i] = Col.RGBToHSV (metallic_c [i])[2];
				}

				float [] smooth_v = new float [smooth_c.Length];
				for (int i = 0; i < smooth_c.Length; i++)
				{
					smooth_v [i] = Col.RGBToHSV (smooth_c [i])[2];
				}

				float [] ao_v = new float [ao_c.Length];
				for (int i = 0; i < ao_c.Length; i++)
				{
					ao_v [i] = Col.RGBToHSV (ao_c [i])[2];
				}

				for (int i = 0; i < r_c.Length; i++)
				{
					r_c [i] = new Color (metallic_v [i], ao_v [i], 0f, smooth_v [i]);
				}

				r.SetPixels (r_c);
				r.Apply ();

				return r;
			}
			public static void ToMso (Texture2D metallic, Texture2D smooth, Texture2D ao, string path, bool ping)
			{
				#if UNITY_EDITOR
				Texture2D r = ToMso (metallic, smooth, ao);


				ToFilePng (r, path, ping);
				#endif
			}	

			public static void ValuesToGrayscaleTexture2D (ref float [,] values, string namemap, 
				ref Texture2D tex)
			{
				tex = new Texture2D (values.GetLength (0), values.GetLength (1));

				Color [] colors = new Color [values.GetLength (0) * values.GetLength (1)];

				values = Mathm.InverseLerp (ref values);

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
			public static void ValuesToGrayscaleTexture2D_blur (ref float [,] values, string namemap, 
				ref Texture2D tex)
			{
				tex = new Texture2D (values.GetLength (0), values.GetLength (1));

				Color [] colors = new Color [values.GetLength (0) * values.GetLength (1)];

				values = Mathm.InverseLerp (ref values);

				for (int i = 0; i < values.GetLength (0); i++)
				{
					for (int j = 0; j < values.GetLength (1); j++)
					{
						colors [i * values.GetLength (1) + j] = Color.Lerp (
							Color.black, Color.white, values [i, j]);
					}
				}

				tex.SetPixels (colors);

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

				FastBlurMezanix (ref tex, blurRad, blurPass, ref c_Blur, ref blurPixelCount);

				tex.Apply ();

				tex.name = namemap;
			}
			public static void ValuesToGrayscaleTexture2D_blur (ref float [,] values, string namemap, 
				ref Texture2D tex, int blurRad, int blurPass)
			{
				tex = new Texture2D (values.GetLength (0), values.GetLength (1));

				Color [] colors = new Color [values.GetLength (0) * values.GetLength (1)];

				values = Mathm.InverseLerp (ref values);

				for (int i = 0; i < values.GetLength (0); i++)
				{
					for (int j = 0; j < values.GetLength (1); j++)
					{
						colors [i * values.GetLength (1) + j] = Color.Lerp (
							Color.black, Color.white, values [i, j]);
					}
				}

				tex.SetPixels (colors);

				Color c_Blur = Color.black;
				int blurPixelCount = 0;

				FastBlurMezanix (ref tex, blurRad, blurPass, ref c_Blur, ref blurPixelCount);

				tex.Apply ();

				tex.name = namemap;
			}

			public static float GetPixelValue (Texture2D tex, int texI, int texJ)
			{
				float r = 0.5f;

				r = Col.RGBToHSV (tex.GetPixel (texI, texJ)) [2];

				return Mathf.Clamp (r, 0f, 1f);
			}
			public static float [,] GetPixelValue (Texture2D tex)
			{
				int width = tex.width;
				int height = tex.height;

				float [,] r = new float[width, height];

				for (int i = 0; i < width; i++)
				{
					for (int j = 0; j < width; j++)
					{
						r [i, j] = Col.RGBToHSV (tex.GetPixel (i, j)) [2];

						r [i, j] = Mathf.Clamp (r [i, j], 0f, 1f);
					}
				}

				return r;
			}


			#region write
			public static void ToFilePng (Texture2D tex)
			{
				string path = Application.dataPath + "/Tex_texture.png";

				Texture2D texToWrite = new Texture2D (tex.width, tex.height, TextureFormat.ARGB32, false, true);

				texToWrite.SetPixels (tex.GetPixels ());
				texToWrite.Apply ();

				byte [] bytes = texToWrite.EncodeToJPG ();

				File.WriteAllBytes (path, bytes);
			}
			public static void ToFilePng (Texture2D tex, string path)
			{
				Texture2D texToWrite = new Texture2D (tex.width, tex.height, TextureFormat.ARGB32, false, true);

				texToWrite.SetPixels (tex.GetPixels ());
				texToWrite.Apply ();

				byte [] bytes = texToWrite.EncodeToJPG ();

				File.WriteAllBytes (path, bytes);
			}

			#if UNITY_EDITOR
			public static void ToFilePng (Texture2D tex, string path, bool ping)
			{
				Texture2D texToWrite = new Texture2D (tex.width, tex.height, TextureFormat.ARGB32, false, true);

				texToWrite.SetPixels (tex.GetPixels ());
				texToWrite.Apply ();

				byte [] bytes = texToWrite.EncodeToPNG ();


				File.WriteAllBytes (path, bytes);


				if (Ass.SaveAndRefreshAssets ())
				{
					if (ping)
					{
						Texture2D texObj = AssetDatabase.LoadAssetAtPath <Texture2D> (path);

						if (texObj != null)
						{
							EditorGUIUtility.PingObject (texObj);
						}
					}
				}
			}
			#endif

			#endregion write
		}


		public class Vec
		{
			public static Vector3 Center (Vector3 [] v)
			{
				Vector3 r = Vector3.zero;

				for (int i = 0; i < v.Length; i++)
				{
					r += v [i];
				}

				r *= 1f/( (float)(v.Length==0?1:v.Length) );

				return r;
			}
		}

		public class DatesTimesAndFrequences 
		{
			public static string DateTimeNow ()
			{
				string dateTimeNow = DateTime.Now.ToString ();

				dateTimeNow = dateTimeNow.Replace ('/', '_');

				dateTimeNow = dateTimeNow.Replace (':', '_');

				dateTimeNow = dateTimeNow.Replace (' ', '_');


				dateTimeNow = dateTimeNow.Replace ('.', '_');

				dateTimeNow = dateTimeNow.Replace ('-', '_');

				dateTimeNow = dateTimeNow.Replace (',', '_');

				dateTimeNow = dateTimeNow.Replace (';', '_');

				dateTimeNow = dateTimeNow.Replace ('\\', '_');


				dateTimeNow += "_" + 
					_4RandomDigitsToString ();

				return dateTimeNow;
			}

			public static string _4RandomDigitsToString ()
			{
				return
					UnityEngine.Random.Range (0, 9).ToString () + 
					UnityEngine.Random.Range (0, 9).ToString () + 
					UnityEngine.Random.Range (0, 9).ToString ()	+ 
					UnityEngine.Random.Range (0, 9).ToString ();
			}

			public static bool TicTac (ref int count, int period, bool init)
			{
				bool r = false;

				if (init)
					count = 0;

				if (count >= period)
				{
					//Debug.Log ("count = " + count.ToString ());

					count = 0;

					r = true;
				}

				count++;

				return r;
			}
		}
	}
}
