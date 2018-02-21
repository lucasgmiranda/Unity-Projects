using UnityEngine;
using System.Collections;
using System;

namespace Mezanix.Diamond
{
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

		public static bool TicTac (ref float count, float period, bool init)
		{
			bool r = false;

			if (init)
				count = 0f;
			
			if (count >= period)
			{
			//	Debug.Log ("count = " + count.ToString ());
				//Debug.Log ("count = ");

				count = 0f;

				r = true;
			}

			count += Diamond.diamondDeltaTime;

			return r;
		}
	}
}
