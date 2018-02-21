using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptsCreatedByDiamond
{
	public class MezanixDiamondPhysics : MonoBehaviour 
	{
		#region movement
		void FixedUpdate ()
		{
			if (addForce && ! setVelocity)
				AddForce ();

			if ( ! addForce && setVelocity)
				SetVelocity ();
		}

		[HideInInspector]
		public Rigidbody rb;

		[HideInInspector]
		public Vector3 force;
		[HideInInspector]
		public ForceMode forceMode;
		[HideInInspector]
		public bool addForce = false;
		void AddForce ()
		{
			if (rb == null)
				return;

			rb.AddForce (force, forceMode);
		}

		[HideInInspector]
		public Vector3 velocity;
		[HideInInspector]
		public bool setVelocity = false;
		void SetVelocity ()
		{
			if (rb == null)
				return;

			if (setVelocity)
				rb.velocity = velocity;
			else
				rb.velocity = Vector3.zero;
		}
		#endregion movement

		#region contact
		public enum CollisionState
		{
			none,

			enter,

			stay,

			exit,
		}
		[HideInInspector]
		public CollisionState collisionState;


		public enum TriggerState
		{
			none,

			enter,

			stay,

			exit,
		}
		[HideInInspector]
		public TriggerState triggerState;

		[HideInInspector]
		public GameObject otherGo = null;

		[HideInInspector]
		public GameObject collisionGo = null;

		[HideInInspector]
		public string otherTag = "";

		[HideInInspector]
		public string collisionTag = "";


		[HideInInspector]
		public Vector3 collisionPoint;

		[HideInInspector]
		public Vector3 collisionNormal;

		[HideInInspector]
		public Vector3 collisionRelativeVelocity;

		[HideInInspector]
		public Vector3 collisionImpulse;

		[HideInInspector]
		public Vector3 collisionForce;

		void GetOtherTag (Collider other)
		{
			otherGo = other.gameObject;

			if (otherGo == null)
				return;

			otherTag = otherGo.tag;			
		}

		void GetCollisionInfo (Collision collision)
		{
			collisionGo = collision.gameObject;

			if (collisionGo == null)
				return;

			collisionTag = collisionGo.tag;


			if ( ! (collision.contacts.Length > 0))
				return;

			collisionPoint = collision.contacts [0].point;

			collisionNormal = collision.contacts [0].normal;

			collisionRelativeVelocity = collision.relativeVelocity;

			if (collisionState == CollisionState.enter)
				collisionImpulse = collision.impulse;

			collisionForce = collisionImpulse / Time.fixedDeltaTime;
		}

		void OnTriggerEnter (Collider other) 
		{
			triggerState = TriggerState.enter;

			GetOtherTag (other);
		}

		void OnTriggerStay (Collider other) 
		{
			triggerState = TriggerState.stay;

			GetOtherTag (other);
		}

		void OnTriggerExit (Collider other) 
		{
			triggerState = TriggerState.exit;

			GetOtherTag (other);
		}


		void OnTriggerEnter2D (Collider2D other) 
		{
		}

		void OnTriggerStay2D (Collider2D other) 
		{
		}

		void OnTriggerExit2D (Collider2D other)
		{
		}




		void OnCollisionEnter (Collision collision)
		{
			collisionState = CollisionState.enter;

			GetCollisionInfo (collision);
		}

		void OnCollisionStay (Collision collision) 
		{
			collisionState = CollisionState.stay;

			GetCollisionInfo (collision);
		}

		void OnCollisionExit (Collision collision) 
		{
			collisionState = CollisionState.exit;

			GetCollisionInfo (collision);
		}


		void OnCollisionEnter2D (Collision2D coll) 
		{
		}

		void OnCollisionStay2D (Collision2D coll) 
		{
		}

		void OnCollisionExit2D (Collision2D coll) 
		{
		}
		#endregion contact
	}
}
