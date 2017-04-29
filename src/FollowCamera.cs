using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class FollowCamera : CameraBase
{
	private delegate bool SetPointBDelegate(ref Vector3 pointB);

	public static FollowCamera instance;

	private PointBType pointBTypeCurr;

	private PointBType pointBTypeLast;

	public bool isLogMoveY;

	public bool isLogYaw;

	public bool isLogPitch;

	public bool isLogUpdate;

	public bool isLogAddBoss;

	private Vector3 defaultRoleCameraRelative;

	private float roleBodyNodeHeightLast;

	private Transform boss;

	private Vector3 bossBornCameraPosition;

	private bool isBossBorn;

	private Vector3 pointB;

	private bool isMove;

	private bool isRotate;

	private bool isPathfinding;

	private bool isLooking;

	private camera cameraInfo = new camera();

	private float cameraAngleMin;

	private float cameraAngleMax;

	private List<FollowCamera.SetPointBDelegate> setPointBDelegatesCustom;

	private List<FollowCamera.SetPointBDelegate> setPointBDelegatesDefault;

	private Action SetCameraPositionCallback;

	private float timeReverseCounter;

	private Vector3 defaultPointB;

	private static string[] LAYER_NPC = new string[]
	{
		"Default",
		"NPC"
	};

	private static string[] LAYER_PLAYER = new string[]
	{
		"CityPlayer"
	};

	private Transform body_y_node;

	private Transform camerapoint2;

	private Dictionary<Transform, int> bosses = new Dictionary<Transform, int>();

	private bool isTeleporting;

	private float cameraHeightOffsetLast;

	private float jumpIncrementPerSkill;

	private float jumpIncrementAllSkill;

	private float cameraHeightNew;

	private float cameraHeightFixed;

	private float camreaPosYNew;

	private float cameraPosYIncrement;

	private float correctionIncrement;

	private float correction;

	private float currentVelocityArea;

	private List<long> ids = new List<long>();

	public bool isPetSkill;

	public Vector3 fightCameraPosition;

	public Quaternion fightCameraRotation;

	public int bornFxId;

	public float smoothingMoveSpeed;

	public float smoothingRotateAngle;

	private float currentVelocity;

	public float defaultCameraToNpcDist = 3f;

	public float offsetY = 1.2f;

	private Vector3 lookTgPoint;

	private Vector3 lookTgForward;

	private Vector3 lastCamrePoint;

	private Vector3 lastCamerForward;

	private Transform roleBodyNode;

	private void OnDrawGizmos()
	{
		if (this.role == null)
		{
			return;
		}
		Gizmos.set_color(Color.get_white());
		Gizmos.DrawRay(base.get_transform().get_position(), base.get_transform().get_forward() * 2.14748365E+09f);
		Gizmos.set_color(Color.get_yellow());
		Gizmos.DrawRay(base.get_transform().get_position().AssignYZero(), Vector3.get_up() * 2.14748365E+09f);
		Gizmos.set_color(Color.get_blue());
		Gizmos.DrawLine(base.get_transform().get_position(), this.GetRolePositionWithHeight());
		Gizmos.DrawRay(this.role.get_position(), Vector3.get_up() * 2.14748365E+09f);
		Gizmos.set_color(Color.get_green());
		Gizmos.DrawLine(base.get_transform().get_position(), this.GetPointB());
		Gizmos.DrawRay(this.GetPointB().AssignYZero(), Vector3.get_up() * 2.14748365E+09f);
	}

	private void Awake()
	{
		this.Init();
		this.InitListeners();
	}

	private void OnDestroy()
	{
	}

	protected override void Init()
	{
		FollowCamera.instance = this;
		this.role = base.GetPlayerRole();
		List<FollowCamera.SetPointBDelegate> list = new List<FollowCamera.SetPointBDelegate>();
		list.Add(new FollowCamera.SetPointBDelegate(this.SetPointBBoss));
		list.Add(new FollowCamera.SetPointBDelegate(this.SetPointBMap));
		list.Add(new FollowCamera.SetPointBDelegate(this.SetPointBArea));
		list.Add(new FollowCamera.SetPointBDelegate(this.SetPointBRole));
		this.setPointBDelegatesDefault = list;
		this.setPointBDelegatesCustom = new List<FollowCamera.SetPointBDelegate>(this.setPointBDelegatesDefault);
		this.InitCameraArgs();
		this.InitCamera();
		base.SetRolePositionLast();
		this.Refresh();
		base.SetRolePositionLast();
	}

	public void SetPointBPriority(int index)
	{
		this.setPointBDelegatesCustom = new List<FollowCamera.SetPointBDelegate>(this.setPointBDelegatesDefault);
		FollowCamera.SetPointBDelegate setPointBDelegate = this.setPointBDelegatesCustom.get_Item(index);
		this.setPointBDelegatesCustom.RemoveAt(index);
		this.setPointBDelegatesCustom.Insert(0, setPointBDelegate);
	}

	public void SetCameraPosition(float distance, float height)
	{
		this.defaultRoleCameraDistance = distance;
		this.defaultCameraHeight = height;
	}

	public void ResetCameraAsync(uint timeMS, Action callback)
	{
		this.OnResetCamera();
		this.SetCameraPositionCallback = callback;
		this.timeReverseCounter = timeMS;
	}

	protected override void InitCameraArgs()
	{
		Scene scene = DataReader<Scene>.Get(MySceneManager.Instance.CurSceneID);
		if (scene == null || scene.CameraPreset.get_Count() != 4)
		{
			Debug.LogError(string.Format("相机参数CameraPreset设置错误[sceneID : {0}]", MySceneManager.Instance.CurSceneID));
			return;
		}
		this.defaultRoleCameraDistance = scene.CameraDistance;
		this.defaultRoleHeight = scene.cameraAngle;
		this.defaultCameraHeight = scene.CameraHeight;
		string[] array = scene.LockLookPoint.Split(new char[]
		{
			';'
		});
		float num = float.Parse(array[0]);
		float num2 = float.Parse(array[1]);
		float num3 = float.Parse(array[2]);
		this.defaultPointB = new Vector3(num, num2, num3);
	}

	protected override void InitCamera()
	{
		Vector3 normalized = (this.role.get_position() - this.defaultPointB).AssignYZero().get_normalized();
		Vector3 vector = normalized * this.defaultRoleCameraDistance;
		Vector3 vector2 = new Vector3(0f, this.defaultCameraHeight);
		base.get_transform().set_position(this.role.get_position() + vector + vector2);
		base.get_transform().LookAt(this.GetRolePositionWithHeight());
		Debug.Log(string.Concat(new object[]
		{
			"InitCamera role=",
			this.role.get_position(),
			" position=",
			base.get_transform().get_position(),
			" rotation=",
			base.get_transform().get_rotation()
		}));
		this.defaultRoleCameraRelative = base.get_transform().get_position() - this.role.get_position();
		this.FindNodeOfBody();
		SoundManager.Instance.SetAddListenerPos(this.role.get_position());
	}

	private void SetProperties(camera aCamera)
	{
		this.cameraInfo = aCamera;
	}

	protected override void InitListeners()
	{
		base.InitListeners();
		EventDispatcher.AddListener<int, Transform>(CameraEvent.MonsterBorn, new Callback<int, Transform>(this.OnActorBorn));
		EventDispatcher.AddListener<int, Transform>(CameraEvent.PlayerBorn, new Callback<int, Transform>(this.OnActorBorn));
		EventDispatcher.AddListener<Transform>(CameraEvent.MonsterDie, new Callback<Transform>(this.OnActorDie));
		EventDispatcher.AddListener<Transform>(CameraEvent.PlayerDie, new Callback<Transform>(this.OnActorDie));
		EventDispatcher.AddListener(CameraEvent.SelfRebuild, new Callback(this.OnSelfRebuild));
		EventDispatcher.AddListener<bool>(CameraEvent.Pathfinding, new Callback<bool>(this.OnPathfinding));
		EventDispatcher.AddListener<int, Transform>(CameraEvent.LookAtNpc, new Callback<int, Transform>(this.OnLookAtNpc));
		EventDispatcher.AddListener("InputManager.SelectModel", new Callback(this.SelectModel));
	}

	public override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int, Transform>(CameraEvent.MonsterBorn, new Callback<int, Transform>(this.OnActorBorn));
		EventDispatcher.RemoveListener<int, Transform>(CameraEvent.PlayerBorn, new Callback<int, Transform>(this.OnActorBorn));
		EventDispatcher.RemoveListener<Transform>(CameraEvent.MonsterDie, new Callback<Transform>(this.OnActorDie));
		EventDispatcher.RemoveListener<Transform>(CameraEvent.PlayerDie, new Callback<Transform>(this.OnActorDie));
		EventDispatcher.RemoveListener(CameraEvent.SelfRebuild, new Callback(this.OnSelfRebuild));
		EventDispatcher.RemoveListener<bool>(CameraEvent.Pathfinding, new Callback<bool>(this.OnPathfinding));
		EventDispatcher.RemoveListener<int, Transform>(CameraEvent.LookAtNpc, new Callback<int, Transform>(this.OnLookAtNpc));
		EventDispatcher.RemoveListener("InputManager.SelectModel", new Callback(this.SelectModel));
	}

	private bool TryCastRay(float pickDistance, int layer, ref Transform hit)
	{
		if (CamerasMgr.CameraMain == null)
		{
			return false;
		}
		RaycastHit[] array = Physics.RaycastAll(CamerasMgr.CameraMain.ScreenPointToRay(InputManager.SelectTouchPositionBaseBottomL), 50f, layer);
		for (int i = 0; i < array.Length; i++)
		{
			if (!(array[i].get_collider() is CharacterController))
			{
				hit = array[i].get_transform().get_parent();
				if (!(hit.GetComponentInChildren<Animator>() == null))
				{
					if (Vector2.Distance(hit.get_position().GetVectorNoY(), EntityWorld.Instance.ActSelf.FixTransform.get_position().GetVectorNoY()) <= pickDistance)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private void SelectModel()
	{
		Transform transform = null;
		Actor actor = null;
		if (this.TryCastRay(BillboardManager.GetDistanceOfAVC(31), LayerMask.GetMask(FollowCamera.LAYER_NPC), ref transform))
		{
			if (transform != null && transform.GetComponent<Actor>() != null)
			{
				actor = transform.GetComponent<Actor>();
			}
			if (actor != null)
			{
				EventDispatcher.Broadcast<Actor>(CameraEvent.SelectModel, actor);
				Debug.LogError(actor);
			}
		}
		else if (this.TryCastRay(BillboardManager.GetDistanceOfAVC(21), LayerMask.GetMask(FollowCamera.LAYER_PLAYER), ref transform))
		{
			if (transform != null && transform.GetComponent<Actor>() != null)
			{
				actor = transform.GetComponent<Actor>();
			}
			if (actor != null)
			{
				EventDispatcher.Broadcast<Actor>(CameraEvent.SelectModel, actor);
				Debug.LogError(actor);
			}
		}
	}

	private void OnActorBorn(int typeId, Transform actor)
	{
		if (this.AddBoss(actor))
		{
			this.SetBoss(this.GetMaxPriorityBoss());
		}
	}

	private void OnActorDie(Transform actor)
	{
		this.RemoveBoss(actor);
		this.SetBoss(this.GetMaxPriorityBoss());
	}

	private void OnSelfRebuild()
	{
		base.set_enabled(false);
		base.set_enabled(true);
		this.OnTraSelfChanged();
		this.roleBodyNode = null;
		this.FindNodeOfBody();
	}

	private void OnPathfinding(bool value)
	{
		this.isPathfinding = value;
		this.currentVelocity = 0f;
	}

	private void OnLookAtNpc(int npcId, Transform trans)
	{
		if (npcId > 0 && trans != null)
		{
			Vector3 vector = trans.get_position() + new Vector3(0f, 1.5f, 0f);
			Vector3 vector2 = vector - this.role.get_position();
			float num = (vector2.get_magnitude() - this.defaultCameraToNpcDist) / vector2.get_magnitude();
			this.lookTgPoint = num * vector2 + this.role.get_position() + new Vector3(0f, this.offsetY, 0f);
			this.lookTgForward = (vector - this.lookTgPoint).get_normalized();
			this.lastCamrePoint = base.get_transform().get_position();
			this.lastCamerForward = base.get_transform().get_forward();
			this.isLooking = true;
		}
		else
		{
			this.lookTgPoint = this.lastCamrePoint;
			this.lookTgForward = this.lastCamerForward;
		}
	}

	public void OnResetCamera()
	{
		this.boss = null;
		this.bosses.Clear();
		CameraGlobal.cameraType = CameraType.Follow;
		Scene scene = DataReader<Scene>.Get(MySceneManager.Instance.CurSceneID);
		this.SetCameraPosition(scene.CameraDistance, scene.CameraHeight);
		this.cameraAngleMin = (float)scene.cameraDefaultAngle;
		this.cameraAngleMax = (float)scene.cameraDefaultAngle;
	}

	protected override void OnBossBornEnd(Transform newBoss)
	{
		base.OnBossBornEnd(newBoss);
		Vector3 normalized = (this.role.get_position() - newBoss.get_position()).AssignYZero().get_normalized();
		Vector3 vector = normalized * this.defaultRoleCameraDistance;
		Vector3 vector2 = new Vector3(0f, this.defaultCameraHeight);
		base.get_transform().set_position(this.role.get_position() + vector + vector2);
		base.get_transform().LookAt(this.GetBossPositionWithHeight(newBoss));
	}

	private Transform GetMaxPriorityBoss()
	{
		if (this.bosses.get_Count() == 0)
		{
			return null;
		}
		if (EntityWorld.Instance.ActSelf.FixTransform == null)
		{
			return null;
		}
		List<KeyValuePair<Transform, int>> list = new List<KeyValuePair<Transform, int>>();
		using (Dictionary<Transform, int>.Enumerator enumerator = this.bosses.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<Transform, int> current = enumerator.get_Current();
				list.Add(current);
			}
		}
		list.Sort(delegate(KeyValuePair<Transform, int> a, KeyValuePair<Transform, int> b)
		{
			if (a.get_Value() != b.get_Value())
			{
				return a.get_Value().CompareTo(b.get_Value());
			}
			return -Vector3.Distance(EntityWorld.Instance.ActSelf.FixTransform.get_position(), a.get_Key().get_position()).CompareTo(Vector3.Distance(EntityWorld.Instance.ActSelf.FixTransform.get_position(), b.get_Key().get_position()));
		});
		KeyValuePair<Transform, int> keyValuePair = list.get_Item(list.get_Count() - 1);
		if (this.boss != null && this.bosses.ContainsKey(this.boss))
		{
			return (keyValuePair.get_Value() <= this.bosses.get_Item(this.boss)) ? this.boss : keyValuePair.get_Key();
		}
		return keyValuePair.get_Key();
	}

	private int GetPriority(Transform boss)
	{
		ActorParent component = boss.GetComponent<ActorParent>();
		int typeID = component.GetEntity().TypeID;
		int result = -1;
		if (component as ActorMonster)
		{
			Monster monster = DataReader<Monster>.Get(typeID);
			result = monster.cameraLock;
		}
		else if (component as ActorPlayer)
		{
			RoleCreate roleCreate = DataReader<RoleCreate>.Get(typeID);
			result = roleCreate.cameraLock;
		}
		else
		{
			Debug.Log("<color=red>Error:</color>GetPriority=NEITHER Monster NOR Player " + component);
		}
		return result;
	}

	private bool AddBoss(Transform actor)
	{
		int priority = this.GetPriority(actor);
		if (priority > 0)
		{
			if (!this.bosses.ContainsKey(actor))
			{
				this.bosses.Add(actor, priority);
			}
			else
			{
				this.bosses.set_Item(actor, priority);
			}
			return true;
		}
		return false;
	}

	public void OnSwitchCamera()
	{
		if (this.bosses.get_Count() == 0)
		{
			return;
		}
		List<Transform> list = new List<Transform>();
		using (Dictionary<Transform, int>.Enumerator enumerator = this.bosses.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<Transform, int> current = enumerator.get_Current();
				if (current.get_Key() != null)
				{
					list.Add(current.get_Key());
				}
			}
		}
		list.Sort((Transform a, Transform b) => Vector3.Distance(EntityWorld.Instance.ActSelf.get_transform().get_position(), a.get_position()).CompareTo(Vector3.Distance(EntityWorld.Instance.ActSelf.get_transform().get_position(), b.get_position())));
		int num = list.IndexOf(this.boss) + 1;
		if (num >= list.get_Count())
		{
			num = 0;
		}
		Transform transform = list.get_Item(num);
		this.SetBoss(transform);
	}

	private void RemoveBoss(Transform actor)
	{
		this.bosses.Remove(actor);
	}

	private void SetBoss(Transform newBoss)
	{
		if (newBoss == this.boss)
		{
			return;
		}
		if (this.boss && this.boss.GetComponent<ActorParent>() != null)
		{
			this.boss.GetComponent<ActorParent>().OnSelected(false);
		}
		this.boss = newBoss;
		if (this.boss && this.boss.GetComponent<ActorParent>() != null)
		{
			this.boss.GetComponent<ActorParent>().OnSelected(true);
			EntityWorld.Instance.LockOnTarget = this.boss.GetComponent<ActorParent>().GetEntity();
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(this.boss.GetComponent<ActorParent>().GetEntity().FixModelID);
			this.SetCameraPosition(avatarModel.CameraDistance, avatarModel.CameraHeight);
			this.FindNodeRecursive(this.boss, ref this.body_y_node, "body_y_node");
			this.FindNodeRecursive(this.boss, ref this.camerapoint2, "camerapoint2");
		}
	}

	protected override void OnTraSelfChanged()
	{
		base.OnTraSelfChanged();
		CameraGlobal.cameraType = CameraType.Follow;
	}

	private Vector3 GetProject(Vector3 vector, Vector3 onNormal)
	{
		return onNormal * Vector3.Dot(vector, onNormal) / Vector3.SqrMagnitude(onNormal);
	}

	private Vector3 GetRolePointB()
	{
		return this.role.get_position() + this.role.get_forward() * this.cameraInfo.pointApointBDistance + Vector3.get_up() * this.cameraInfo.pointBPosY;
	}

	private bool SetPointBRole(ref Vector3 pointB)
	{
		pointB = this.GetRolePointB();
		this.SetProperties(DataReader<camera>.Get("1_0"));
		this.pointBTypeCurr = PointBType.role;
		return true;
	}

	private bool SetPointBMap(ref Vector3 pointB)
	{
		List<float> mapPointB = CameraGlobal.GetMapPointB();
		if (mapPointB != null)
		{
			pointB = new Vector3(mapPointB.get_Item(0), mapPointB.get_Item(1), mapPointB.get_Item(2));
			camera camera = DataReader<camera>.Get("3_0");
			this.SetProperties(camera);
			this.cameraAngleMin = camera.cameraAngleMin;
			this.cameraAngleMax = camera.cameraAngleMax;
			this.pointBTypeCurr = PointBType.map;
			return true;
		}
		return false;
	}

	private bool SetPointBArea(ref Vector3 pointB)
	{
		if (CameraGlobal.isAreaPointBActive)
		{
			pointB = CameraGlobal.areaPointB;
			camera camera = DataReader<camera>.Get("4_0");
			this.SetProperties(camera);
			this.cameraAngleMin = camera.cameraAngleMin;
			this.cameraAngleMax = camera.cameraAngleMax;
			this.pointBTypeCurr = PointBType.area;
			return true;
		}
		return false;
	}

	private bool SetPointBBoss(ref Vector3 pointB)
	{
		if (this.boss != null)
		{
			pointB = this.GetBossPositionWithHeight();
			int num = this.bosses.get_Item(this.boss);
			camera camera = DataReader<camera>.Get("2_" + num);
			if (camera == null)
			{
				camera = DataReader<camera>.Get("2_0");
			}
			this.SetProperties(camera);
			this.cameraAngleMin = camera.cameraAngleMin;
			this.cameraAngleMax = camera.cameraAngleMax;
			this.pointBTypeCurr = PointBType.boss;
			return true;
		}
		return false;
	}

	private void SetPointB()
	{
		for (int i = 0; i < this.setPointBDelegatesCustom.get_Count(); i++)
		{
			if (this.setPointBDelegatesCustom.get_Item(i)(ref this.pointB))
			{
				return;
			}
		}
	}

	private Vector3 ToVector3(List<float> floats)
	{
		return new Vector3(floats.get_Item(0), floats.get_Item(1), floats.get_Item(2));
	}

	private Vector3 GetPointB()
	{
		return this.pointB;
	}

	private Vector3 GetRolePositionWithHeight()
	{
		return this.role.get_position() + new Vector3(0f, this.defaultRoleHeight);
	}

	private float GetBossHeight(Transform boss)
	{
		ActorParent component = boss.GetComponent<ActorParent>();
		if (component)
		{
			return component.CameraPointHeight;
		}
		return 0f;
	}

	private float GetBossHeight()
	{
		return this.GetBossHeight(this.boss);
	}

	private Vector3 GetBossPositionWithHeight()
	{
		return this.GetBossPositionWithHeight(this.boss);
	}

	private Vector3 GetBossPositionWithHeight(Transform boss)
	{
		return boss.get_position() + new Vector3(0f, this.GetBossHeight(boss));
	}

	private void MoveZ()
	{
		if (this.role == null)
		{
			return;
		}
		Vector3 vector = base.get_transform().get_forward().AssignYZero();
		Vector3 vector2 = Vector3.Project(this.rolePositionLast.AssignYZero(), vector);
		Vector3 vector3 = Vector3.Project(this.role.get_position().AssignYZero(), vector);
		Transform expr_52 = base.get_transform();
		expr_52.set_position(expr_52.get_position() + (vector3 - vector2));
		float magnitude = (base.get_transform().get_position() - this.role.get_position()).AssignYZero().get_magnitude();
		float num = Mathf.Max(0f, this.defaultRoleCameraDistance) - magnitude;
		float num2 = Mathf.Min(magnitude, 1f * Time.get_deltaTime());
		Transform expr_C0 = base.get_transform();
		expr_C0.set_position(expr_C0.get_position() + num * -vector * num2);
	}

	public Vector3 TeleportingStart()
	{
		this.isTeleporting = true;
		Debug.Log("TeleportingStart=" + this.role.get_position());
		return base.get_transform().get_position() - this.role.get_position();
	}

	public void TeleportingEnd(Vector3 relativeDisplacement)
	{
		Debug.Log("TeleportingEnd=" + this.role.get_position());
		base.get_transform().set_position(this.role.get_position() + relativeDisplacement);
		this.isTeleporting = false;
		base.SetRolePositionLast();
	}

	private float GetCameraHeightOffset()
	{
		return 0f;
	}

	private bool IsSkill()
	{
		return EntityWorld.Instance.ActSelf.CurActionStatus != "run" && EntityWorld.Instance.ActSelf.CurActionStatus != "idle";
	}

	private bool IsRoleJumpFollow()
	{
		return !(EntityWorld.Instance.ActSelf == null) && EntityWorld.Instance.ActSelf.IsJumpFollow && this.IsSkill();
	}

	private void MoveY()
	{
		if (this.role == null)
		{
			return;
		}
		if (!this.IsRoleJumpFollow())
		{
			this.cameraHeightNew = this.defaultCameraHeight + this.GetCameraHeightOffset();
			this.cameraHeightFixed = Mathf.Clamp(this.cameraHeightNew, this.cameraInfo.cameraPosYMin, this.cameraInfo.cameraPosYMax);
			this.camreaPosYNew = this.role.get_position().y + this.cameraHeightFixed;
			this.cameraPosYIncrement = this.camreaPosYNew - base.get_transform().get_position().y;
			if (Mathf.Abs(this.cameraPosYIncrement) > this.cameraInfo.cameraPosYCorrection * Time.get_deltaTime())
			{
				this.correctionIncrement = this.cameraInfo.cameraPosYCorrection * Time.get_deltaTime();
			}
			else
			{
				this.correctionIncrement = Mathf.Abs(this.cameraPosYIncrement);
			}
			this.correction = Mathf.Sign(this.cameraPosYIncrement) * this.correctionIncrement;
			if (this.isLogMoveY)
			{
				Debug.Log(string.Concat(new object[]
				{
					"correction=",
					this.correction,
					" cameraPosYCorrection=",
					this.cameraInfo.cameraPosYCorrection * Time.get_deltaTime(),
					" jumpIncrementPerSkill=",
					this.jumpIncrementPerSkill,
					" jumpIncrementAllSkill=",
					this.jumpIncrementAllSkill
				}));
			}
			this.jumpIncrementPerSkill = 0f;
		}
		else
		{
			float num = this.roleBodyNode.get_position().y - this.roleBodyNodeHeightLast;
			float num2 = this.GetCameraHeightOffset() - this.cameraHeightOffsetLast;
			this.cameraHeightNew = base.get_transform().get_position().y + num2 + num - this.role.get_position().y;
			this.cameraHeightFixed = Mathf.Max(0f, this.cameraHeightNew);
			this.camreaPosYNew = this.role.get_position().y + this.cameraHeightFixed;
			this.cameraPosYIncrement = this.camreaPosYNew - base.get_transform().get_position().y;
			this.correctionIncrement = Mathf.Abs(this.cameraPosYIncrement);
			this.correction = Mathf.Sign(this.cameraPosYIncrement) * this.correctionIncrement;
			this.jumpIncrementPerSkill += this.correction;
			this.jumpIncrementAllSkill += this.correction;
			if (this.isLogMoveY)
			{
				Debug.Log("correction=" + this.correction);
			}
		}
		Transform expr_2BA = base.get_transform();
		expr_2BA.set_position(expr_2BA.get_position() + new Vector3(0f, this.correction));
	}

	private void MoveX()
	{
		if (!this.isMove)
		{
			return;
		}
		Vector3 t = this.role.get_position() - base.get_transform().get_position();
		float num = Vector3.Angle(base.get_transform().get_forward().AssignYZero(), t.AssignYZero());
		Vector3 vector = Vector3.Cross(base.get_transform().get_forward().AssignYZero(), t.AssignYZero());
		float num2 = Vector3.Dot((this.role.get_position() - this.rolePositionLast).AssignYZero(), base.get_transform().get_right());
		Vector3 vector2 = Vector3.Project(this.rolePositionLast.AssignYZero(), base.get_transform().get_right().AssignYZero());
		Vector3 vector3 = Vector3.Project(this.role.get_position().AssignYZero(), base.get_transform().get_right().AssignYZero());
		Vector3 vector4 = vector3 - vector2;
		float num3;
		if (num > this.cameraInfo.moveSmallAngle && vector.y < 0f && num2 > 0f)
		{
			num3 = -vector4.get_magnitude() * 0.5f;
		}
		else if (num > this.cameraInfo.moveSmallAngle && vector.y > 0f && num2 < 0f)
		{
			num3 = -vector4.get_magnitude() * 0.5f;
		}
		else if (num > this.cameraInfo.moveLargeAngle)
		{
			num3 = Mathf.Min(this.cameraInfo.moveMediumCorrection * Time.get_deltaTime(), vector4.get_magnitude()) + this.cameraInfo.moveLargeCorrection * Time.get_deltaTime();
		}
		else if (vector4.get_magnitude() > 0f)
		{
			num3 = Mathf.Min(this.cameraInfo.moveMediumCorrection * Time.get_deltaTime(), vector4.get_magnitude());
		}
		else
		{
			num3 = this.cameraInfo.moveSmallCorrection * Time.get_deltaTime();
		}
		Transform expr_1F7 = base.get_transform();
		expr_1F7.set_position(expr_1F7.get_position() + Mathf.Sign(vector.y) * base.get_transform().get_right() * num3);
	}

	private void Yaw()
	{
		this.YawNoSmoothDamp();
	}

	private void YawSmoothDamp()
	{
		if (base.IsRoleStand())
		{
			this.currentVelocityArea = 0f;
			return;
		}
		Vector3 vector = this.GetPointB() - base.get_transform().get_position();
		float y = base.get_transform().get_eulerAngles().y;
		float y2 = Quaternion.LookRotation(vector).get_eulerAngles().y;
		float num = Mathf.SmoothDampAngle(y, y2, ref this.currentVelocityArea, 1f);
		Quaternion quaternion = Quaternion.Euler(0f, num - base.get_transform().get_eulerAngles().y, 0f);
		Vector3 vector2 = quaternion * (base.get_transform().get_position() - this.role.get_position());
		base.get_transform().set_position(this.role.get_position() + vector2);
		base.get_transform().set_forward(quaternion * base.get_transform().get_forward());
	}

	private void YawNoSmoothDamp()
	{
		if (!this.isRotate)
		{
			return;
		}
		Vector3 vector = this.GetPointB() - base.get_transform().get_position();
		float num = Vector3.Angle(base.get_transform().get_forward().AssignYZero(), vector.AssignYZero());
		Vector3 vector2 = Vector3.Cross(base.get_transform().get_forward().AssignYZero(), vector);
		float num2 = 0f;
		string text = string.Empty;
		if (base.IsRoleStand())
		{
			float num3 = this.cameraInfo.rotateSmallCorrection * Time.get_deltaTime();
			float num4 = num;
			num2 = Mathf.Min(num3, num4);
			text = "=站立";
		}
		else if (num > this.cameraInfo.rotateLargeAngle)
		{
			float num5 = this.cameraInfo.rotateLargeCorrection * Time.get_deltaTime();
			float num6 = num - this.cameraInfo.rotateLargeAngle + 1f * Time.get_deltaTime();
			num2 = Mathf.Min(num5, num6);
			text = ">大角";
		}
		else if (num > this.cameraInfo.rotateMediumAngle)
		{
			float num7 = this.cameraInfo.rotateMediumCorrection * Time.get_deltaTime();
			float num8 = num - this.cameraInfo.rotateMediumAngle;
			num2 = Mathf.Min(num7, num8);
			text = ">中角";
		}
		else if (num > this.cameraInfo.rotateSmallAngle)
		{
			text = ">小角";
			num2 = this.cameraInfo.rotateMediumCorrection * Time.get_deltaTime();
		}
		else
		{
			text = "=小角";
		}
		float num9 = Mathf.Sign(vector2.y) * num2;
		Quaternion quaternion = Quaternion.AngleAxis(num9, Vector3.get_up());
		Vector3 vector3 = quaternion * (base.get_transform().get_position() - this.role.get_position());
		if (this.isLogYaw)
		{
			Debug.Log(string.Concat(new object[]
			{
				text,
				" angle=",
				num9,
				" forwardBossAngle=",
				num,
				" rotateLargeAngle=",
				this.cameraInfo.rotateLargeAngle,
				" rotateMediumAngle=",
				this.cameraInfo.rotateMediumAngle,
				" rotateSmallAngle=",
				this.cameraInfo.rotateSmallAngle
			}));
		}
		base.get_transform().set_position(this.role.get_position() + vector3);
		Transform expr_26A = base.get_transform();
		expr_26A.set_rotation(expr_26A.get_rotation() * quaternion);
	}

	private void Pitch()
	{
		if (EntityWorld.Instance.ActSelf == null)
		{
			return;
		}
		float num = Vector3.Angle(Vector3.get_down(), base.get_transform().get_forward());
		Vector3 vector = this.GetPointB() - base.get_transform().get_position();
		float num2 = Vector3.Angle(Vector3.get_down(), vector);
		float num3 = Mathf.Clamp(num2, this.cameraAngleMin, this.cameraAngleMax);
		float num4 = num3 - num;
		float num5 = float.Parse(DataReader<GlobalParams>.Get("camera_switch_speed").value);
		if (!EntityWorld.Instance.ActSelf.IsJumpFollow && Mathf.Abs(num4) > num5 * Time.get_deltaTime())
		{
			if (this.boss)
			{
				ActorParent component = this.boss.GetComponent<ActorParent>();
				if (component && !component.IsJumpFollow)
				{
					float num6 = Mathf.Sign(num4);
					float num7 = Mathf.Abs(num4);
					num4 = num6 * Mathf.Min(num5, num7) * Time.get_deltaTime();
				}
			}
			else
			{
				float num8 = Mathf.Sign(num4);
				float num9 = Mathf.Abs(num4);
				num4 = num8 * Mathf.Min(num5, num5) * Time.get_deltaTime();
			}
		}
		if (this.isLogPitch)
		{
			Debug.Log("pitchAngle=" + num4);
		}
		Quaternion quaternion = Quaternion.AngleAxis(-num4, base.get_transform().get_right());
		base.get_transform().set_forward(quaternion * base.get_transform().get_forward());
		if (this.SetCameraPositionCallback != null)
		{
			this.timeReverseCounter -= Time.get_deltaTime() * 1000f;
			if (this.cameraPosYIncrement == 0f && Mathf.Abs(num3 - num) <= num5 * Time.get_deltaTime() && this.timeReverseCounter < 0f)
			{
				this.SetCameraPositionCallback.Invoke();
				this.SetCameraPositionCallback = null;
				this.timeReverseCounter = 0f;
			}
		}
	}

	private void CheckMoveX()
	{
		if (this.role == null)
		{
			return;
		}
		Vector3 t = this.role.get_position() - base.get_transform().get_position();
		float num = Vector3.Angle(base.get_transform().get_forward().AssignYZero(), t.AssignYZero());
		if (num > this.cameraInfo.moveMediumAngle)
		{
			this.isMove = true;
		}
		if (num <= this.cameraInfo.moveSmallAngle)
		{
			this.isMove = false;
		}
	}

	private void CheckYaw()
	{
		Vector3 t = this.GetPointB() - base.get_transform().get_position();
		float num = Vector3.Angle(base.get_transform().get_forward().AssignYZero(), t.AssignYZero());
		if (num > this.cameraInfo.rotateMediumAngle)
		{
			this.isRotate = true;
		}
		if (num <= this.cameraInfo.rotateSmallAngle)
		{
			this.isRotate = false;
		}
	}

	private void CheckTransformation()
	{
		this.CheckMoveX();
		this.CheckYaw();
	}

	protected override void Refresh()
	{
		this.SetPointB();
		if (this.pointBTypeLast != this.pointBTypeCurr)
		{
			this.pointBTypeLast = this.pointBTypeCurr;
		}
		this.MoveZ();
		this.MoveY();
		this.CheckTransformation();
		this.MoveX();
		if (this.cameraInfo.canDrag == 1 && ControlStickCamera.IsDraging)
		{
			this.DragToRotate();
		}
		else
		{
			this.Yaw();
		}
		this.Pitch();
	}

	private void BossBorn()
	{
		Vector3 vector = this.bossBornCameraPosition;
		Vector3 vector2 = (this.role.get_position() - this.newBoss.get_position()).AssignYZero();
		Vector3 vector3 = vector2.get_normalized() * this.defaultRoleCameraRelative.AssignYZero().get_magnitude();
		Vector3 vector4 = new Vector3(0f, this.defaultRoleCameraRelative.y);
		Vector3 vector5 = this.role.get_position() + vector3 + vector4;
		float num = vector2.get_magnitude() * Time.get_deltaTime() * 2f;
		base.get_transform().set_position(Vector3.MoveTowards(vector, vector5, num));
		Vector3 forward = this.GetBossPositionWithHeight(this.newBoss) - vector5;
		base.get_transform().set_forward(forward);
		if (base.get_transform().get_position() == vector5)
		{
			this.isBossBorn = false;
		}
		this.bossBornCameraPosition = base.get_transform().get_position();
	}

	private void SetMaterialsColor(Renderer _renderer, float Transpa)
	{
		int num = _renderer.get_sharedMaterials().Length;
		for (int i = 0; i < num; i++)
		{
			Color color = _renderer.get_materials()[i].get_color();
			color.a = Transpa;
			_renderer.get_materials()[i].SetColor(ShaderPIDManager._Color, color);
		}
	}

	private void SphereCastAll()
	{
		for (int i = 0; i < this.ids.get_Count(); i++)
		{
			EntityParent entityByID = EntityWorld.Instance.GetEntityByID(this.ids.get_Item(i));
			if (entityByID != null)
			{
				ShaderEffectUtils.SetFadeRightNow(entityByID.alphaControls, false);
			}
		}
		Ray ray = new Ray(base.get_transform().get_position(), base.get_transform().get_forward());
		float num = Vector3.Distance(this.GetRolePositionWithHeight(), base.get_transform().get_position());
		RaycastHit[] array = Physics.SphereCastAll(ray, 0.5f, num);
		string text = string.Empty;
		RaycastHit[] array2 = array;
		for (int j = 0; j < array2.Length; j++)
		{
			RaycastHit raycastHit = array2[j];
			ActorParent component = raycastHit.get_collider().get_gameObject().GetComponent<ActorParent>();
			if (!(component == null))
			{
				if (component is ActorMonster || component is ActorCityPlayer)
				{
					text = text + raycastHit.get_collider().get_gameObject().get_name() + ", ";
					ShaderEffectUtils.SetFadeRightNow(component.GetEntity().alphaControls, true);
					this.ids.Add(component.GetEntity().ID);
				}
			}
		}
	}

	private void PetSkill()
	{
		if (CamerasMgr.MainCameraRoot.get_position() == EntityWorld.Instance.TraSelf.get_position() + this.fightCameraPosition && CamerasMgr.MainCameraRoot.get_rotation() == this.fightCameraRotation)
		{
			this.isPetSkill = false;
			PetManager.Instance.DeleteScreenFXOfBattle(this.bornFxId);
			Utils.PauseAI(false);
			return;
		}
		CamerasMgr.MainCameraRoot.set_position(Vector3.MoveTowards(CamerasMgr.MainCameraRoot.get_position(), EntityWorld.Instance.TraSelf.get_position() + this.fightCameraPosition, this.smoothingMoveSpeed * Time.get_deltaTime()));
		CamerasMgr.MainCameraRoot.set_rotation(Quaternion.RotateTowards(CamerasMgr.MainCameraRoot.get_rotation(), this.fightCameraRotation, this.smoothingRotateAngle * Time.get_deltaTime()));
	}

	private void DragToRotate()
	{
		float num = ControlStickCamera.Direction2Camera.x * this.cameraInfo.dragSpeed;
		float num2 = num * Time.get_deltaTime();
		Quaternion quaternion = Quaternion.AngleAxis(num2, Vector3.get_up());
		Vector3 vector = quaternion * (base.get_transform().get_position() - this.role.get_position());
		base.get_transform().set_position(this.role.get_position() + vector);
		base.get_transform().set_forward(quaternion * base.get_transform().get_forward());
	}

	private void Pathfinding()
	{
		Vector3 vector = this.role.get_position() - this.rolePositionLast;
		Transform expr_1D = base.get_transform();
		expr_1D.set_position(expr_1D.get_position() + vector);
		Vector3 forward = base.get_transform().get_forward();
		Vector3 forward2 = this.role.get_forward();
		float num = Vector3.Angle(forward.AssignYZero(), forward2.AssignYZero());
		Vector3 vector2 = Vector3.Cross(forward.AssignYZero(), forward2.AssignYZero());
		float num2 = Mathf.SmoothDampAngle(base.get_transform().get_eulerAngles().y, this.role.get_eulerAngles().y, ref this.currentVelocity, 1f);
		Quaternion quaternion = Quaternion.Euler(0f, num2 - base.get_transform().get_eulerAngles().y, 0f);
		Vector3 vector3 = quaternion * (base.get_transform().get_position() - this.role.get_position());
		base.get_transform().set_position(this.role.get_position() + vector3);
		base.get_transform().set_forward(quaternion * base.get_transform().get_forward());
	}

	private void LookAt()
	{
		Vector3 vector = this.lookTgPoint - base.get_transform().get_position();
		Transform expr_1D = base.get_transform();
		expr_1D.set_position(expr_1D.get_position() + vector * Mathf.Min(this.lookTgPoint.get_magnitude(), 3f * Time.get_deltaTime()));
		Vector3 vector2 = this.lookTgForward - base.get_transform().get_forward();
		Transform expr_6B = base.get_transform();
		expr_6B.set_forward(expr_6B.get_forward() + vector2 * Mathf.Min(vector2.get_magnitude(), 2f * Time.get_deltaTime()));
	}

	private void Update()
	{
		if (this.role == null)
		{
			return;
		}
		if (CameraGlobal.cameraType != CameraType.Follow)
		{
			return;
		}
		if (this.isTeleporting)
		{
			return;
		}
		if (this.isLogUpdate)
		{
			Debug.Log(string.Concat(new object[]
			{
				"B点=",
				this.GetPointB(),
				" 相机高度=",
				base.get_transform().get_position().y,
				" 角色高度=",
				this.defaultRoleHeight,
				" 相机角色距离=",
				Vector3.Distance(base.get_transform().get_position().AssignYZero(), this.GetRolePositionWithHeight().AssignYZero()),
				" 相机Boss距离=",
				Vector3.Distance(base.get_transform().get_position().AssignYZero(), this.GetPointB().AssignYZero()),
				" 垂直角A=",
				Vector3.Angle(this.GetPointB() - this.GetRolePositionWithHeight(), Vector3.get_down())
			}));
		}
		if (this.isBossBorn)
		{
			this.BossBorn();
		}
		else if (this.isPetSkill)
		{
			this.PetSkill();
		}
		else if (this.isPetUltraSKill)
		{
			base.PetUltraSKill();
		}
		else if (this.isPathfinding)
		{
			this.Pathfinding();
		}
		else if (this.isLooking)
		{
			this.LookAt();
		}
		else
		{
			this.Refresh();
		}
		this.roleBodyNodeHeightLast = this.roleBodyNode.get_position().y;
		this.cameraHeightOffsetLast = this.GetCameraHeightOffset();
		base.SetRolePositionLast();
	}

	private void FindNodeOfBody()
	{
		this.FindNodeRecursive(this.role, ref this.roleBodyNode, "body_y_node");
	}

	private void FindNodeRecursive(Transform parent, ref Transform node, string name)
	{
		if (node != null)
		{
			return;
		}
		if (parent == null)
		{
			return;
		}
		if (parent.get_name().TrimEnd(new char[]
		{
			' '
		}) == name)
		{
			node = parent;
			return;
		}
		for (int i = 0; i < parent.get_childCount(); i++)
		{
			Transform child = parent.GetChild(i);
			this.FindNodeRecursive(child, ref node, name);
		}
	}
}
