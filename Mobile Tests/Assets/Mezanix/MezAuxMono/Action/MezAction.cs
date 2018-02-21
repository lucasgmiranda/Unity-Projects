using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mezanix
{
	public class MezAction
	{
		#region triggerType
		public enum TriggerType
		{
			always,
			userInput,
			proximity,
			contact,
			rayCast,
			eventListening,
		}

		public enum ContactType
		{
			triggerCollider,
			collision,
		}
		public enum TriggerMethod
		{
			OnTriggerEnter,
			OnTriggerStay,
			OnTriggerExit,
		}
		public enum CollisionMethod
		{
			OnCollisionEnter,
			OnCollisionStay,
			OnCollisionExit,
		}

		public enum RayCastType
		{
			seen,
			seeing,
		}
		#endregion triggerType

		#region actionType
		public enum ActionCategory
		{
			transformAndMovement,
			physics,
			gameObject,
			rendering,
			light,
			scene,
			_event,
		}

		public enum TransformActionType
		{
			translate,
			rotate,
			scale,
		}

		public enum PhysicsActionType
		{
			addForce,
			addTorque,
		}

		public enum GameObjectActionType
		{
			disable,
			enable,
			destroy,
		}

		public enum RenderingActionType
		{
			disable,
			enable,
			setColor,
		}

		public enum LightActionType
		{
			disable,
			enable,
			setColor,
			setIntensity,
		}

		public enum SceneActionType
		{
			load,
		}

		public enum EventActionType
		{
			send,
			listen,
		}
		#endregion actionType
	}
}