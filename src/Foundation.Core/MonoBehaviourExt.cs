using System;
using System.Linq;
using UnityEngine;

namespace Foundation.Core
{
	public static class MonoBehaviourExt
	{
		private const int MaxParentLevels = 32;

		public static Transform GetTransformRoot(this MonoBehaviour gameObject)
		{
			Transform transform = gameObject.get_transform();
			for (int i = 0; i < 32; i++)
			{
				Transform transform2 = transform.get_transform();
				if (transform2.get_parent() == null)
				{
					return transform2;
				}
				transform = transform.get_parent();
			}
			return null;
		}

		public static T GetComponenetInParent<T>(this MonoBehaviour gameObject, bool ignoreSelf) where T : Component
		{
			Transform transform = gameObject.get_transform();
			if (ignoreSelf)
			{
				transform = transform.get_parent();
			}
			for (int i = 0; i < 32; i++)
			{
				if (transform == null)
				{
					return (T)((object)null);
				}
				T component = transform.GetComponent<T>();
				if (component != null)
				{
					return component;
				}
				if (transform.get_parent() == null)
				{
					return (T)((object)null);
				}
				transform = transform.get_parent();
			}
			return (T)((object)null);
		}

		public static T GetComponenetInParent<T>(this MonoBehaviour gameObject) where T : Component
		{
			Transform transform = gameObject.get_transform();
			for (int i = 0; i < 32; i++)
			{
				T component = transform.GetComponent<T>();
				if (component != null)
				{
					return component;
				}
				if (transform.get_parent() == null)
				{
					return (T)((object)null);
				}
				transform = transform.get_parent();
			}
			return (T)((object)null);
		}

		public static T GetComponenetInParent<T>(this GameObject gameObject) where T : Component
		{
			Transform transform = gameObject.get_transform();
			for (int i = 0; i < 32; i++)
			{
				T component = transform.GetComponent<T>();
				if (component != null)
				{
					return component;
				}
				if (transform.get_parent() == null)
				{
					return (T)((object)null);
				}
				transform = transform.get_parent();
			}
			return (T)((object)null);
		}

		public static GameObject[] FindChildrenByName(this MonoBehaviour gameObject, string name)
		{
			return Enumerable.ToArray<GameObject>(Enumerable.Select<Transform, GameObject>(Enumerable.Where<Transform>(gameObject.GetComponentsInChildren<Transform>(), (Transform o) => o.get_name() == name), (Transform o) => o.get_gameObject()));
		}

		public static T[] FindChildrenByName<T>(this MonoBehaviour gameObject, string name) where T : Component
		{
			return Enumerable.ToArray<T>(Enumerable.Select<Transform, T>(Enumerable.Where<Transform>(gameObject.GetComponentsInChildren<Transform>(), (Transform o) => o.get_name() == name), (Transform o) => o.GetComponent<T>()));
		}

		public static GameObject FindChildByName(this MonoBehaviour gameObject, string name)
		{
			return Enumerable.SingleOrDefault<GameObject>(gameObject.FindChildrenByName(name));
		}

		public static T FindChildByName<T>(this MonoBehaviour gameObject, string name) where T : Component
		{
			return Enumerable.SingleOrDefault<T>(gameObject.FindChildrenByName(name));
		}

		public static GameObject[] FindChildrenWithTag(this MonoBehaviour gameObject, string tag, bool includeInactive)
		{
			return Enumerable.ToArray<GameObject>(Enumerable.Select<Transform, GameObject>(Enumerable.Where<Transform>(gameObject.GetComponentsInChildren<Transform>(includeInactive), (Transform o) => o.CompareTag(tag)), (Transform o) => o.get_gameObject()));
		}

		public static T[] FindChildrenWithTag<T>(this MonoBehaviour gameObject, string tag, bool includeInactive) where T : Component
		{
			return Enumerable.ToArray<T>(Enumerable.Select<Transform, T>(Enumerable.Where<Transform>(gameObject.GetComponentsInChildren<Transform>(includeInactive), (Transform o) => o.CompareTag(tag)), (Transform o) => o.GetComponent<T>()));
		}

		public static GameObject FindChildWithTag(this MonoBehaviour gameObject, string tag, bool includeInactive)
		{
			return Enumerable.SingleOrDefault<GameObject>(gameObject.FindChildrenWithTag(tag, includeInactive));
		}

		public static T FindChildWithTag<T>(this MonoBehaviour gameObject, string tag, bool includeInactive) where T : Component
		{
			return Enumerable.SingleOrDefault<T>(gameObject.FindChildrenWithTag(tag, includeInactive));
		}

		public static GameObject[] FindChildrenWithTag(this MonoBehaviour gameObject, string tag)
		{
			return Enumerable.ToArray<GameObject>(Enumerable.Select<Transform, GameObject>(Enumerable.Where<Transform>(gameObject.GetComponentsInChildren<Transform>(), (Transform o) => o.CompareTag(tag)), (Transform o) => o.get_gameObject()));
		}

		public static T[] FindChildrenWithTag<T>(this MonoBehaviour gameObject, string tag) where T : Component
		{
			return Enumerable.ToArray<T>(Enumerable.Select<Transform, T>(Enumerable.Where<Transform>(gameObject.GetComponentsInChildren<Transform>(), (Transform o) => o.CompareTag(tag)), (Transform o) => o.GetComponent<T>()));
		}

		public static GameObject FindChildWithTag(this MonoBehaviour gameObject, string tag)
		{
			return Enumerable.SingleOrDefault<GameObject>(gameObject.FindChildrenWithTag(tag));
		}

		public static T FindChildWithTag<T>(this MonoBehaviour gameObject, string tag) where T : Component
		{
			return Enumerable.SingleOrDefault<T>(gameObject.FindChildrenWithTag(tag));
		}

		public static GameObject InstantiateChild(this MonoBehaviour mono, GameObject prefab)
		{
			return mono.InstantiateChild(prefab, Vector3.get_zero(), Quaternion.get_identity());
		}

		public static GameObject InstantiateChild(this MonoBehaviour mono, GameObject prefab, Vector3 localposition, Quaternion localrotation)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(prefab);
			if (gameObject != null)
			{
				gameObject.get_transform().set_parent(mono.get_transform());
				gameObject.get_transform().set_localPosition(localposition);
				gameObject.get_transform().set_localRotation(localrotation);
			}
			return gameObject;
		}

		public static T[] GetInterfaces<T>(this GameObject gObj)
		{
			if (!typeof(T).get_IsInterface())
			{
				throw new SystemException("Specified type is not an interface!");
			}
			MonoBehaviour[] components = gObj.GetComponents<MonoBehaviour>();
			return Enumerable.ToArray<T>(Enumerable.Select<MonoBehaviour, T>(Enumerable.Where<MonoBehaviour>(components, (MonoBehaviour a) => Enumerable.Any<Type>(a.GetType().GetInterfaces(), (Type k) => k == typeof(T))), (MonoBehaviour a) => (T)((object)a)));
		}

		public static T GetInterface<T>(this GameObject gObj)
		{
			if (!typeof(T).get_IsInterface())
			{
				throw new SystemException("Specified type is not an interface!");
			}
			return Enumerable.FirstOrDefault<T>(gObj.GetInterfaces<T>());
		}

		public static T GetInterfaceInChildren<T>(this MonoBehaviour gObj)
		{
			if (!typeof(T).get_IsInterface())
			{
				throw new SystemException("Specified type is not an interface!");
			}
			return Enumerable.FirstOrDefault<T>(gObj.GetInterfacesInChildren<T>());
		}

		public static T[] GetInterfacesInChildren<T>(this MonoBehaviour gObj)
		{
			if (!typeof(T).get_IsInterface())
			{
				throw new SystemException("Specified type is not an interface!");
			}
			MonoBehaviour[] componentsInChildren = gObj.GetComponentsInChildren<MonoBehaviour>(true);
			return Enumerable.ToArray<T>(Enumerable.Select<MonoBehaviour, T>(Enumerable.Where<MonoBehaviour>(componentsInChildren, (MonoBehaviour a) => Enumerable.Any<Type>(a.GetType().GetInterfaces(), (Type k) => k == typeof(T))), (MonoBehaviour a) => (T)((object)a)));
		}
	}
}
