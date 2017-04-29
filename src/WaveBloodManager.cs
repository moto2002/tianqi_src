using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WaveBloodManager
{
	private const float ToRatio = 0.003921569f;

	private const int trace_miss = 2;

	private static WaveBloodManager m_Instance;

	public float blood_number_deviation_width;

	public float blood_number_deviation_height;

	private List<Piaoxue> PiaoXueDatas = new List<Piaoxue>();

	private Dictionary<long, WaveBloodControl> WaveBloodControls = new Dictionary<long, WaveBloodControl>();

	private int NumOfControl = 8;

	private int NumOfUnit = 2;

	private int NumOfNumImage = 4;

	private static UIPool WaveBloodControlPool;

	public static Transform Pool2WaveBloodControl;

	public static readonly string WaveBloodControlPosName = "Position2WaveBloodControl";

	private static UIPool WaveBloodUnitPool;

	public static Transform Pool2WaveBloodUnit;

	private static UIPool WaveBloodNumPool;

	private static Transform Pool2WaveBloodNum;

	public static WaveBloodManager Instance
	{
		get
		{
			if (WaveBloodManager.m_Instance == null)
			{
				WaveBloodManager.m_Instance = new WaveBloodManager();
			}
			return WaveBloodManager.m_Instance;
		}
	}

	public WaveBloodManager()
	{
		WaveBloodManager.CreatePools();
		this.PiaoXueDatas = DataReader<Piaoxue>.DataList;
		string[] array = DataReader<GlobalParams>.Get("blood_number_deviation").value.Split(new char[]
		{
			";".get_Chars(0)
		});
		if (array.Length >= 2)
		{
			this.blood_number_deviation_width = float.Parse(array[0]);
			this.blood_number_deviation_height = float.Parse(array[1]);
		}
	}

	private static void CreatePools()
	{
		WaveBloodManager.CreateWaveBloodControlPool();
		WaveBloodManager.CreateWaveBloodUnitPool();
		WaveBloodManager.CreateWaveBloodNumPool();
	}

	public void PreInstantiate()
	{
		WaveBloodManager.WaveBloodControlPool.LoadInstantiateOfNum(this.NumOfControl, 2f, 0.5f);
		WaveBloodManager.WaveBloodUnitPool.LoadInstantiateOfNum(this.NumOfControl * this.NumOfUnit, 2f, 0.5f);
		WaveBloodManager.WaveBloodNumPool.LoadInstantiateOfNum(this.NumOfControl * this.NumOfUnit * this.NumOfNumImage, 2f, 0f);
	}

	private static void CreateWaveBloodControlPool()
	{
		Transform transform = new GameObject("Pool2WaveBloodControl").get_transform();
		transform.set_parent(UINodesManager.NoEventsUIRoot);
		transform.get_gameObject().set_layer(LayerSystem.NameToLayer("UI"));
		WaveBloodManager.Pool2WaveBloodControl = transform;
		UGUITools.ResetTransform(WaveBloodManager.Pool2WaveBloodControl);
		WaveBloodManager.WaveBloodControlPool = new UIPool("WaveBloodControl", WaveBloodManager.Pool2WaveBloodControl, false);
	}

	private static void CreateWaveBloodUnitPool()
	{
		WaveBloodManager.Pool2WaveBloodUnit = new GameObject("Pool2WaveBloodUnit").get_transform();
		WaveBloodManager.Pool2WaveBloodUnit.set_parent(UINodesManager.NoEventsUIRoot);
		WaveBloodManager.Pool2WaveBloodUnit.get_gameObject().SetActive(false);
		UGUITools.ResetTransform(WaveBloodManager.Pool2WaveBloodUnit);
		WaveBloodManager.WaveBloodUnitPool = new UIPool("WaveBloodUnit", WaveBloodManager.Pool2WaveBloodUnit, true);
	}

	public WaveBloodUnit GetUnit()
	{
		WaveBloodUnit component = WaveBloodManager.WaveBloodUnitPool.Get(string.Empty).GetComponent<WaveBloodUnit>();
		component.AwakeSelf();
		return component;
	}

	public void ReuseUnit(GameObject go)
	{
		WaveBloodManager.WaveBloodUnitPool.ReUse(go);
	}

	private static void CreateWaveBloodNumPool()
	{
		WaveBloodManager.Pool2WaveBloodNum = new GameObject("Pool2WaveBloodNum").get_transform();
		WaveBloodManager.Pool2WaveBloodNum.set_parent(UINodesManager.NoEventsUIRoot);
		WaveBloodManager.Pool2WaveBloodNum.get_gameObject().SetActive(false);
		UGUITools.ResetTransform(WaveBloodManager.Pool2WaveBloodNum);
		WaveBloodManager.WaveBloodNumPool = new UIPool("WaveBloodNum", WaveBloodManager.Pool2WaveBloodNum, true);
	}

	public GameObject GetNum()
	{
		return WaveBloodManager.WaveBloodNumPool.Get(string.Empty);
	}

	public void ReuseNum(GameObject go)
	{
		WaveBloodManager.WaveBloodNumPool.ReUse(go);
	}

	public void Init()
	{
	}

	public void ThrowBlood(HPChangeMessage data)
	{
		XTaskManager.instance.StartCoroutine(this.BeginThrowBlood(data));
	}

	[DebuggerHidden]
	private IEnumerator BeginThrowBlood(HPChangeMessage data)
	{
		WaveBloodManager.<BeginThrowBlood>c__Iterator30 <BeginThrowBlood>c__Iterator = new WaveBloodManager.<BeginThrowBlood>c__Iterator30();
		<BeginThrowBlood>c__Iterator.data = data;
		<BeginThrowBlood>c__Iterator.<$>data = data;
		<BeginThrowBlood>c__Iterator.<>f__this = this;
		return <BeginThrowBlood>c__Iterator;
	}

	private void DoThrowBlood(HPChangeMessage data)
	{
		if (data.targetID > 0L && BillboardManager.Instance.IsBillboardInfoOff(data.targetID, BillboardManager.BillboardInfoOffOption.WaveBlood))
		{
			return;
		}
		if (data.modeType == HPChangeMessage.ModeType.None)
		{
			return;
		}
		if (!this.WaveBloodControls.ContainsKey(data.targetID))
		{
			Debuger.Error("飘血不存在, ID = " + data.targetID, new object[0]);
			return;
		}
		WaveBloodControl waveBloodControl = this.WaveBloodControls.get_Item(data.targetID);
		if (waveBloodControl == null)
		{
			return;
		}
		switch (data.hpChangeType)
		{
		case HPChangeMessage.HPChangeType.Miss:
			if (data.modeType == HPChangeMessage.ModeType.Self)
			{
				waveBloodControl.ShowText((int)data.hpChangeValue, Color.get_white(), 3, WaveBloodPerformance.MissToSelf, 2, 1, (int)data.hpChangeType, null);
			}
			else
			{
				waveBloodControl.ShowText((int)data.hpChangeValue, Color.get_white(), 3, WaveBloodPerformance.MissToOther, 2, 1, (int)data.hpChangeType, null);
			}
			return;
		case HPChangeMessage.HPChangeType.Break:
			waveBloodControl.ShowText((int)data.hpChangeValue, Color.get_white(), 3, WaveBloodPerformance.Break, 2, 1, (int)data.hpChangeType, null);
			return;
		}
		int damageType = (int)data.hpChangeType;
		if (data.hpChangeType == HPChangeMessage.HPChangeType.KillRecover || data.hpChangeType == HPChangeMessage.HPChangeType.HpRestore)
		{
			damageType = 5;
		}
		else if (data.elementType != HPChangeMessage.ElementType.Normal)
		{
			switch (data.elementType)
			{
			case HPChangeMessage.ElementType.Earth:
				damageType = 7;
				break;
			case HPChangeMessage.ElementType.Fire:
				damageType = 8;
				break;
			case HPChangeMessage.ElementType.Water:
				damageType = 9;
				break;
			case HPChangeMessage.ElementType.Thunder:
				damageType = 10;
				break;
			}
		}
		Piaoxue piaoxue = this.FindPiaoxue(damageType, (int)data.modeType);
		if (piaoxue != null)
		{
			List<int> color = piaoxue.color;
			Color white = Color.get_white();
			white.r = (float)color.get_Item(0) * 0.003921569f;
			white.g = (float)color.get_Item(1) * 0.003921569f;
			white.b = (float)color.get_Item(2) * 0.003921569f;
			white.a = 1f;
			waveBloodControl.ShowText((int)data.hpChangeValue, white, (int)data.elementType, WaveBloodManager.ToWBP(piaoxue.resource), piaoxue.track, piaoxue.resource, damageType, piaoxue.picSize);
		}
	}

	private Piaoxue FindPiaoxue(int damageType, int modeType)
	{
		for (int i = 0; i < this.PiaoXueDatas.get_Count(); i++)
		{
			if (this.PiaoXueDatas.get_Item(i).damageType == damageType && this.PiaoXueDatas.get_Item(i).target == modeType)
			{
				return this.PiaoXueDatas.get_Item(i);
			}
		}
		return null;
	}

	public void ClearWaveBlood()
	{
		using (Dictionary<long, WaveBloodControl>.ValueCollection.Enumerator enumerator = this.WaveBloodControls.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				WaveBloodControl current = enumerator.get_Current();
				current.DeleteAll();
			}
		}
		List<GameObject> useds = WaveBloodManager.WaveBloodUnitPool.m_useds;
		for (int i = 0; i < useds.get_Count(); i++)
		{
			WaveBloodManager.WaveBloodUnitPool.ReUse(useds.get_Item(i));
		}
	}

	public void AddWaveBloodControl(Transform actorRoot, float height, long uuid)
	{
		if (!SystemConfig.IsBillboardOn)
		{
			return;
		}
		if (!actorRoot)
		{
			return;
		}
		this.RemoveWaveBloodControlByKey(uuid);
		WaveBloodControl waveBloodControl = this.GetWaveBloodControl(actorRoot, height, uuid);
		this.WaveBloodControls.Add(uuid, waveBloodControl);
	}

	public void RemoveWaveBloodControlByKey(long uuid)
	{
		if (!this.WaveBloodControls.ContainsKey(uuid))
		{
			return;
		}
		WaveBloodControl waveBloodControl = this.WaveBloodControls.get_Item(uuid);
		if (waveBloodControl != null)
		{
			waveBloodControl.DeleteAll();
			if (waveBloodControl.WaveBloodRTransform != null)
			{
				WaveBloodManager.WaveBloodControlPool.ReUse(waveBloodControl.WaveBloodRTransform.get_gameObject());
			}
		}
		this.WaveBloodControls.Remove(uuid);
	}

	public void EnableWaveBloodControl(long uuid, bool isShow)
	{
		if (this.WaveBloodControls.ContainsKey(uuid))
		{
			this.WaveBloodControls.get_Item(uuid).set_enabled(isShow);
		}
	}

	private WaveBloodControl GetWaveBloodControl(Transform parent, float height, long uuid)
	{
		WaveBloodControl component = this.AddWaveBloodControlHost(parent, height).GetComponent<WaveBloodControl>();
		if (component)
		{
			component.SetID(uuid);
			component.WaveBloodRTransform = (this.CreateWaveBloodControlPrefab(uuid) as RectTransform);
		}
		return component;
	}

	private Transform AddWaveBloodControlHost(Transform parent, float height)
	{
		Transform transform = parent.FindChild(WaveBloodManager.WaveBloodControlPosName);
		if (transform == null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("WaveBloodPosition");
			instantiate2Prefab.set_name(WaveBloodManager.WaveBloodControlPosName);
			instantiate2Prefab.get_transform().set_parent(parent);
			instantiate2Prefab.get_transform().set_localPosition(new Vector3(0f, BillboardManager.GetRealHeight(height), 0f));
			transform = instantiate2Prefab.get_transform();
		}
		return transform;
	}

	private Transform CreateWaveBloodControlPrefab(long uuid)
	{
		Transform transform = WaveBloodManager.WaveBloodControlPool.Get(string.Empty).get_transform();
		transform.set_name(uuid.ToString());
		Vector3 localPosition = transform.get_localPosition();
		localPosition.z = 0f;
		transform.set_localPosition(localPosition);
		transform.set_localScale(Vector3.get_one());
		return transform;
	}

	public static string GetNumPrefix(int resource)
	{
		string result = "zd_";
		if (resource == 1)
		{
			result = "fight_normalfont_";
		}
		else if (resource == 2)
		{
			result = "fight_critfont_";
		}
		else if (resource == 3)
		{
			result = "fight_bloodfont_";
		}
		else if (resource == 4)
		{
			result = "fight_hurtfont_";
		}
		else if (resource == 5)
		{
			result = "fight_pet_";
		}
		return result;
	}

	public static WaveBloodPerformance ToWBP(int resource)
	{
		if (resource == 2)
		{
			return WaveBloodPerformance.NumToCritical;
		}
		if (resource == 3)
		{
			return WaveBloodPerformance.NumToTreat;
		}
		return WaveBloodPerformance.NumToHurt;
	}

	public static bool IsNum(WaveBloodPerformance wbp)
	{
		return wbp == WaveBloodPerformance.NumToHurt || wbp == WaveBloodPerformance.NumToCritical || wbp == WaveBloodPerformance.NumToTreat;
	}
}
