using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class XUtility
{
	public const float NavMeshStepFixDistance = 10f;

	public const float NavMeshFixDistance = 500f;

	private static float UpDistance = 30f;

	private static float LayerHeight = 45f;

	private static RaycastHit[] raycastHits;

	private static int highestPointIndex = -1;

	private static float currentHighestValue = -3.40282347E+38f;

	private static List<Vector3> mRoughPathPoint = new List<Vector3>();

	private static Queue<Transform> tempTransformQ;

	private static Dictionary<string, float> totalTimes = new Dictionary<string, float>();

	private static Dictionary<string, List<int>> effectsTime = new Dictionary<string, List<int>>();

	public static void PrintList<T>(List<T> list)
	{
		for (int i = 0; i < list.get_Count(); i++)
		{
			T t = list.get_Item(i);
			Debuger.Info("PrintList   " + t.ToString(), new object[0]);
		}
	}

	public static void ListExchange<T>(List<T> list, int index1, int index2)
	{
		if (index1 < 0)
		{
			index1 = 0;
		}
		if (index2 < 0)
		{
			index2 = 0;
		}
		T t = list.get_Item(index1);
		T t2 = list.get_Item(index2);
		list.RemoveAt(index1);
		if (index1 >= list.get_Count())
		{
			list.Add(t2);
		}
		else
		{
			list.Insert(index1, t2);
		}
		list.RemoveAt(index2);
		if (index2 >= list.get_Count())
		{
			list.Add(t);
		}
		else
		{
			list.Insert(index2, t);
		}
	}

	public static string Md5Sum(string strToEncrypt)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(strToEncrypt);
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, '0');
		}
		return text.PadLeft(32, '0');
	}

	public static float GetDistanceNoY(Vector3 start, Vector3 end)
	{
		end.y = start.y;
		return Vector3.Distance(start, end);
	}

	public static float DistanceNoY(Vector3 v1, Vector3 v2)
	{
		return Vector3.Distance(v1, new Vector3(v2.x, v1.y, v2.z));
	}

	public static bool IsINRect(Vector3 point, Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
	{
		float x = point.x;
		float z = point.z;
		float x2 = v0.x;
		float z2 = v0.z;
		float x3 = v1.x;
		float z3 = v1.z;
		float x4 = v2.x;
		float z4 = v2.z;
		float x5 = v3.x;
		float z5 = v3.z;
		return XUtility.Multiply(x, z, x2, z2, x3, z3) * XUtility.Multiply(x, z, x5, z5, x4, z4) <= 0f && XUtility.Multiply(x, z, x5, z5, x2, z2) * XUtility.Multiply(x, z, x4, z4, x3, z3) <= 0f;
	}

	public static bool IsInSector(Transform self, Transform target, float angle, float radius)
	{
		if (Vector3.Distance(self.get_position(), target.get_position()) > radius)
		{
			return false;
		}
		Vector3 vector = target.get_position() - self.get_position();
		Quaternion quaternion = Quaternion.LookRotation(vector);
		float angle2 = target.get_rotation().get_eulerAngles().y - angle / 2f;
		float angle3 = target.get_rotation().get_eulerAngles().y + angle / 2f;
		float y = quaternion.get_eulerAngles().y;
		return XUtility.CheckWithAngle(y, angle2, angle3);
	}

	private static bool CheckWithAngle(float angle, float angle1, float angle2)
	{
		if (angle1 <= 0f)
		{
			angle1 += 360f;
			if (angle2 >= 360f)
			{
				angle2 -= 360f;
				if (angle1 < angle)
				{
					return true;
				}
				if (angle < angle2)
				{
					return true;
				}
			}
			else if (angle > angle1 || angle < angle2)
			{
				return true;
			}
		}
		else if (angle2 >= 360f)
		{
			angle2 -= 360f;
			if (angle > angle1 || angle <= angle2)
			{
				return true;
			}
		}
		else if (angle > angle1 && angle < angle2)
		{
			return true;
		}
		return false;
	}

	public static void IgnoreCollision(Transform a, Transform b)
	{
		Physics.IgnoreCollision(a.GetComponent<Collider>(), b.GetComponent<Collider>());
	}

	public static bool GetTerrainPoint(float x, float z, float curHeight, out Vector3 result)
	{
		XUtility.raycastHits = Physics.RaycastAll(new Vector3(x, curHeight + XUtility.UpDistance, z), -Vector3.get_up(), XUtility.LayerHeight);
		XUtility.highestPointIndex = -1;
		XUtility.currentHighestValue = -2.14748365E+09f;
		for (int i = 0; i < XUtility.raycastHits.Length; i++)
		{
			if (XUtility.raycastHits[i].get_collider().get_gameObject().get_layer() == LayerMask.NameToLayer("Terrian"))
			{
				if (XUtility.raycastHits[i].get_point().y > XUtility.currentHighestValue)
				{
					XUtility.currentHighestValue = XUtility.raycastHits[i].get_point().y;
					XUtility.highestPointIndex = i;
				}
			}
		}
		if (XUtility.highestPointIndex == -1)
		{
			result = new Vector3(x, curHeight, z);
			return false;
		}
		result = XUtility.raycastHits[XUtility.highestPointIndex].get_point();
		return true;
	}

	public static Vector3 GetTerrainPoint(float x, float z, float curHeight)
	{
		XUtility.raycastHits = Physics.RaycastAll(new Vector3(x, curHeight + XUtility.UpDistance, z), -Vector3.get_up(), XUtility.LayerHeight);
		XUtility.highestPointIndex = -1;
		XUtility.currentHighestValue = -2.14748365E+09f;
		for (int i = 0; i < XUtility.raycastHits.Length; i++)
		{
			if (XUtility.raycastHits[i].get_collider().get_gameObject().get_layer() == LayerMask.NameToLayer("Terrian"))
			{
				if (XUtility.raycastHits[i].get_point().y > XUtility.currentHighestValue)
				{
					XUtility.currentHighestValue = XUtility.raycastHits[i].get_point().y;
					XUtility.highestPointIndex = i;
				}
			}
		}
		if (XUtility.highestPointIndex != -1)
		{
			return XUtility.raycastHits[XUtility.highestPointIndex].get_point();
		}
		return new Vector3(x, curHeight, z);
	}

	public static bool GetRoughPathPoint(float desX, float desZ, float accuracy, out List<Vector3> pathPoint)
	{
		XUtility.mRoughPathPoint.Clear();
		pathPoint = XUtility.mRoughPathPoint;
		Vector3 position;
		if (!XUtility.GetTerrainPoint(desX, desZ, 0f, out position))
		{
			return false;
		}
		NavMeshHit navMeshHit;
		if (!NavMesh.SamplePosition(position, ref navMeshHit, 500f, -1))
		{
			return false;
		}
		position = navMeshHit.get_position();
		Vector3 position2 = EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position();
		if (!NavMesh.SamplePosition(EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position(), ref navMeshHit, 500f, -1))
		{
			return false;
		}
		Vector3 position3 = navMeshHit.get_position();
		NavMeshPath navMeshPath = new NavMeshPath();
		if (!NavMesh.CalculatePath(position2, position, -1, navMeshPath))
		{
			return false;
		}
		if (navMeshPath.get_corners().Length < 2)
		{
			return false;
		}
		pathPoint.AddRange(navMeshPath.get_corners());
		if (XUtility.DistanceNoY(position3, navMeshPath.get_corners()[0]) > accuracy)
		{
			pathPoint.Insert(0, position3);
		}
		if (XUtility.DistanceNoY(position2, position3) > accuracy)
		{
			pathPoint.Insert(0, position2);
		}
		return true;
	}

	public static void SetTwoCircle(this Transform trans, Vector3 center, float ρ, float θ, Vector2 camToRole, Vector3 camToLookPoint)
	{
		trans.SetSelfPositionByPlane(center.GetVectorNoY() - camToRole.get_normalized() * ρ * Mathf.Cos(θ), center.y + ρ * Mathf.Sin(θ));
		trans.set_forward(camToLookPoint.AssignYZero().get_normalized() * Mathf.Cos(θ) + Vector3.get_down() * Mathf.Sin(θ));
	}

	public static Vector3 GetCenterPoint(List<Vector3> points)
	{
		Vector3 vector = Vector3.get_zero();
		for (int i = 0; i < points.get_Count(); i++)
		{
			vector += points.get_Item(i);
		}
		return vector / (float)points.get_Count();
	}

	public static float GetCameraRotation(float playerAngle, float distanceToCenter)
	{
		float num = 1f;
		return num * playerAngle * 2f * Mathf.Atan(distanceToCenter) / 3.14159274f;
	}

	private static float Multiply(float p1x, float p1y, float p2x, float p2y, float p0x, float p0y)
	{
		return (p1x - p0x) * (p2y - p0y) - (p2x - p0x) * (p1y - p0y);
	}

	public static string u3dToAbs(string assetPath)
	{
		return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Application.get_dataPath()), assetPath)).Replace("\\", "/");
	}

	public static string TranslatePath(string assetPath)
	{
		return PathUtil.absToU3d(XUtility.u3dToAbs(assetPath));
	}

	public static float GetHitRadius(Transform trans)
	{
		if (trans == null)
		{
			return 0f;
		}
		if (trans.GetComponent<CharacterController>() != null)
		{
			return trans.GetComponent<CharacterController>().get_radius() * trans.get_localScale().x;
		}
		return 0f;
	}

	public static float GetTriggerRadius(Transform trans)
	{
		if (trans == null)
		{
			return 0f;
		}
		if (trans.GetComponent<CapsuleCollider>() != null)
		{
			return trans.GetComponent<CapsuleCollider>().get_radius() * trans.get_localScale().x;
		}
		return 0f;
	}

	public static float GetTop(Transform trans, string name)
	{
		if (trans == null)
		{
			return 0f;
		}
		Transform transform = XUtility.RecursiveFindTransform(trans, name);
		if (transform != null)
		{
			return transform.get_position().y - trans.get_position().y;
		}
		if (trans.GetComponent<CharacterController>() != null)
		{
			return trans.GetComponent<CharacterController>().get_height() * 0.5f * trans.get_localScale().y;
		}
		return 0f;
	}

	public static Transform RecursiveFindTransform(Transform root, string name)
	{
		Transform transform = root.FindChild(name);
		if (transform == null)
		{
			for (int i = 0; i < root.get_childCount(); i++)
			{
				transform = XUtility.RecursiveFindTransform(root.GetChild(i), name);
				if (transform != null)
				{
					return transform;
				}
			}
		}
		return transform;
	}

	public static GameObject RecursiveFindGameObject(GameObject root, string name)
	{
		if (root == null)
		{
			return null;
		}
		return XUtility.RecursiveFindGameObject(root.get_transform(), name);
	}

	public static GameObject RecursiveFindGameObject(Transform root, string name)
	{
		Transform transform = XUtility.RecursiveFindTransform(root.get_transform(), name);
		if (transform != null)
		{
			return transform.get_gameObject();
		}
		return null;
	}

	public static List<GameObject> RecursiveFindGameObjects(GameObject root, string[] names)
	{
		if (root == null)
		{
			return null;
		}
		return XUtility.RecursiveFindGameObjects(root.get_transform(), names);
	}

	public static List<GameObject> RecursiveFindGameObjects(Transform root, string[] names)
	{
		if (root == null)
		{
			return null;
		}
		if (names == null || names.Length == 0)
		{
			return null;
		}
		TempVar.listGameObject.Clear();
		for (int i = 0; i < names.Length; i++)
		{
			GameObject gameObject = XUtility.RecursiveFindGameObject(root, names[i]);
			if (gameObject != null)
			{
				TempVar.listGameObject.Add(gameObject);
			}
		}
		return TempVar.listGameObject;
	}

	public static T AddMissingComponent<T>(this GameObject go) where T : Component
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		return t;
	}

	public static T AddUniqueComponent<T>(this GameObject go) where T : Component
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		return t;
	}

	public static void Destroy(this Component script)
	{
		Object.Destroy(script);
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

	private static string GetPath()
	{
		return Application.get_streamingAssetsPath() + "/data/";
	}

	public static bool ToProtoData<T>(this List<T> list)
	{
		string[] array = typeof(T).ToString().Split(new char[]
		{
			'.'
		});
		string text = array[array.Length - 1];
		string text2 = XUtility.GetPath() + text + ".data";
		if (File.Exists(text2))
		{
			File.Delete(text2);
		}
		using (Stream stream = File.OpenWrite(text2))
		{
			Serializer.Serialize<List<T>>(stream, list);
			stream.Close();
		}
		return true;
	}

	public static string Pack<T>(this IEnumerable<T> list, string seperator = " ")
	{
		return string.Join(seperator, Enumerable.ToArray<string>(Enumerable.Select<T, string>(list, (T x) => x.ToString())));
	}

	public static string PackDictionary<T, U>(this Dictionary<T, U> dict, string seperator = " ")
	{
		return string.Join(seperator, Enumerable.ToArray<string>(Enumerable.Select<KeyValuePair<T, U>, string>(Enumerable.ToList<KeyValuePair<T, U>>(dict), (KeyValuePair<T, U> x) => string.Format("({0} , {1})", x.get_Key(), x.get_Value()))));
	}

	public static bool HasAction(this Animator ac, string actName)
	{
		return ac.HasState(0, Animator.StringToHash(actName));
	}

	public static float GetTotalTime(RuntimeAnimatorController controller, string actName)
	{
		string text = controller.get_name() + "-" + actName;
		if (!XUtility.totalTimes.ContainsKey(text))
		{
			for (int i = 0; i < controller.get_animationClips().Length; i++)
			{
				AnimationClip animationClip = controller.get_animationClips()[i];
				if (animationClip.get_name().Equals(actName))
				{
					XUtility.totalTimes.Add(text, animationClip.get_length());
				}
			}
		}
		return XUtility.totalTimes.get_Item(text);
	}

	public static List<int> GetActionEffects(RuntimeAnimatorController controller, string actName)
	{
		if (controller == null)
		{
			Debug.LogError("controller为空");
			return new List<int>();
		}
		string text = controller.get_name() + "-" + actName;
		if (!XUtility.effectsTime.ContainsKey(text))
		{
			XUtility.effectsTime.Add(text, new List<int>());
			for (int i = 0; i < controller.get_animationClips().Length; i++)
			{
				AnimationClip animationClip = controller.get_animationClips()[i];
				if (animationClip.get_name().Equals(actName))
				{
					for (int j = 0; j < animationClip.get_events().Length; j++)
					{
						AnimationEvent animationEvent = animationClip.get_events()[j];
						if (animationEvent.get_functionName().Equals("Effect"))
						{
							XUtility.effectsTime.get_Item(text).Add(animationEvent.get_intParameter());
						}
					}
				}
			}
		}
		return XUtility.effectsTime.get_Item(text);
	}

	public static bool StartsWith(string s1, string s2)
	{
		if (s2.get_Length() > s1.get_Length())
		{
			return false;
		}
		for (int i = 0; i < s2.get_Length(); i++)
		{
			if (s1.get_Chars(i) != s2.get_Chars(i))
			{
				return false;
			}
		}
		return true;
	}

	public static bool EndsWith(string s1, string s2)
	{
		if (s2.get_Length() > s1.get_Length())
		{
			return false;
		}
		for (int i = 0; i < s2.get_Length(); i++)
		{
			if (s1.get_Chars(s1.get_Length() - 1 - i) != s2.get_Chars(s2.get_Length() - 1 - i))
			{
				return false;
			}
		}
		return true;
	}

	public static Vector2 RotationAngle(Vector2 position, float angle)
	{
		Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.get_up());
		Vector3 vector = quaternion * position.GetVectorWithY();
		return new Vector2(vector.x, vector.z);
	}

	public static Color ReturnColorFromString(string color, int alpha = 255)
	{
		string text = color.Substring(0, 2);
		string text2 = color.Substring(2, 2);
		string text3 = color.Substring(4, 2);
		byte b = Convert.ToByte(text, 16);
		byte b2 = Convert.ToByte(text2, 16);
		byte b3 = Convert.ToByte(text3, 16);
		return new Color32(b, b2, b3, 255);
	}

	public static string GetConfigTxt(string fileName, string suffix = ".txt")
	{
		fileName = fileName.ToLower();
		string text = Path.Combine(AppConst.ResourcePath, fileName + suffix);
		if (File.Exists(text))
		{
			return File.OpenText(text).ReadToEnd();
		}
		return Encoding.get_UTF8().GetString(NativeCallManager.getFromAssets(fileName + suffix));
	}
}
