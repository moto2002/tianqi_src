using GameData;
using Package;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XNetwork;

public class CharacterManager : BaseSubSystemManager
{
	public bool isMaxStep;

	private int stage;

	private int curExp;

	private List<int> brightPoint = new List<int>();

	private Dictionary<int, List<LianTiShuXing>> localData;

	private Dictionary<int, List<int>> pipleLine = new Dictionary<int, List<int>>();

	private List<RefineBodyItemInfo> goodList = new List<RefineBodyItemInfo>();

	private int newBrightPoint;

	public List<int> AllLightPoint = new List<int>();

	private static CharacterManager instance;

	public int Stage
	{
		get
		{
			return this.stage;
		}
	}

	public int CurExp
	{
		get
		{
			return this.curExp;
		}
	}

	public List<int> BrightPoint
	{
		get
		{
			return this.brightPoint;
		}
	}

	public Dictionary<int, List<LianTiShuXing>> LocalData
	{
		get
		{
			if (this.localData == null)
			{
				this.InitLocalData();
			}
			return this.localData;
		}
	}

	public Dictionary<int, List<int>> PipleLine
	{
		get
		{
			return this.pipleLine;
		}
	}

	public int NewBrightPoint
	{
		get
		{
			return this.newBrightPoint;
		}
		set
		{
			this.newBrightPoint = value;
		}
	}

	public static CharacterManager Instance
	{
		get
		{
			if (CharacterManager.instance == null)
			{
				CharacterManager.instance = new CharacterManager();
			}
			return CharacterManager.instance;
		}
	}

	private CharacterManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.localData = null;
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener("ChangeCareerManager.RoleSelfProfessionChange", new Callback(this.OnRoleSelfProfessionChange));
		NetworkManager.AddListenEvent<ResOpenRefineBody>(new NetCallBackMethod<ResOpenRefineBody>(this.OnGetRiseData));
		NetworkManager.AddListenEvent<PlanLightPointRes>(new NetCallBackMethod<PlanLightPointRes>(this.OnPlanLightPointRes));
		NetworkManager.AddListenEvent<ResRefineBody>(new NetCallBackMethod<ResRefineBody>(this.OnGetRiseUpdateData));
		NetworkManager.AddListenEvent<LightAllKeyPointsNty>(new NetCallBackMethod<LightAllKeyPointsNty>(this.OnLightAllKeyPointsNty));
	}

	private void OnRoleSelfProfessionChange()
	{
		this.InitLocalData();
	}

	private void OnLightAllKeyPointsNty(short state, LightAllKeyPointsNty down = null)
	{
		if (state == 0)
		{
			if (down != null)
			{
				for (int i = 0; i < down.linePointIds.get_Count(); i++)
				{
					this.SetAllPipeline(down.linePointIds.get_Item(i), i + 1);
				}
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetRiseData(short state, ResOpenRefineBody down = null)
	{
		if (state == 0)
		{
			if (down != null)
			{
				this.curExp = down.curExp;
				this.stage = down.stage;
				this.brightPoint.Clear();
				for (int i = 0; i < down.brightPoint.get_Count(); i++)
				{
					this.brightPoint.Add(down.brightPoint.get_Item(i).index);
				}
				this.isMaxStep = down.isMaxStage;
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetRiseUpdateData(short state, ResRefineBody down = null)
	{
		if (state == 0)
		{
			if (down != null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					this.stage,
					"-----",
					down.stage,
					"    ",
					down.curExp,
					"       ",
					down.brightPoint.get_Count()
				}));
				this.curExp = down.curExp;
				if (down.brightPoint.get_Count() == 0 && this.stage == down.stage)
				{
					Debug.LogError("最后一阶");
					this.isMaxStep = true;
				}
				else
				{
					int i;
					for (i = 0; i < down.brightPoint.get_Count(); i++)
					{
						if (!this.brightPoint.Exists((int e) => e == down.brightPoint.get_Item(i).index))
						{
							this.brightPoint.Add(down.brightPoint.get_Item(i).index);
						}
					}
				}
				if (this.stage != down.stage || this.isMaxStep)
				{
					CharacterManager.<OnGetRiseUpdateData>c__AnonStoreyDD <OnGetRiseUpdateData>c__AnonStoreyDD = new CharacterManager.<OnGetRiseUpdateData>c__AnonStoreyDD();
					this.AllLightPoint.Clear();
					<OnGetRiseUpdateData>c__AnonStoreyDD.statePoints = this.GetStageLocalData(this.stage);
					int i;
					for (i = 0; i < <OnGetRiseUpdateData>c__AnonStoreyDD.statePoints.get_Count(); i++)
					{
						if (!this.brightPoint.Exists((int e) => e == <OnGetRiseUpdateData>c__AnonStoreyDD.statePoints.get_Item(i).id) && !this.GetPiplelienPoints(this.stage).Exists((int e) => e == <OnGetRiseUpdateData>c__AnonStoreyDD.statePoints.get_Item(i).id))
						{
							this.AllLightPoint.Add(<OnGetRiseUpdateData>c__AnonStoreyDD.statePoints.get_Item(i).id);
						}
					}
					this.brightPoint.Clear();
				}
				this.stage = down.stage;
				EventDispatcher.Broadcast<int>(EventNames.UpdateRiseUIPoint, this.newBrightPoint);
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	public void SendRise()
	{
		if (this.goodList.get_Count() > 0)
		{
			ReqRefineBody reqRefineBody = new ReqRefineBody();
			reqRefineBody.itemInfo.AddRange(this.goodList);
			NetworkManager.Send(reqRefineBody, ServerType.Data);
			this.goodList.Clear();
		}
	}

	public void SendPlanLightPointReq(List<RefineBodyItemInfo> list)
	{
		this.goodList.Clear();
		this.goodList.AddRange(list);
		NetworkManager.Send(new PlanLightPointReq(), ServerType.Data);
	}

	private void OnPlanLightPointRes(short state, PlanLightPointRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"OnPlanLightPointRes",
				down.plantLightPoint,
				"  ",
				down.isLastPoint,
				"  ",
				down.linePointId
			}));
			this.newBrightPoint = down.plantLightPoint;
			if (down.isLastPoint)
			{
				this.SetAllPipeline(down.linePointId, this.stage);
			}
			EventDispatcher.Broadcast<int, bool>(EventNames.UpdateRiseItem, this.stage, down.isLastPoint);
		}
	}

	private void InitLocalData()
	{
		CharacterManager.<InitLocalData>c__AnonStoreyDF <InitLocalData>c__AnonStoreyDF = new CharacterManager.<InitLocalData>c__AnonStoreyDF();
		if (this.localData == null)
		{
			this.localData = new Dictionary<int, List<LianTiShuXing>>();
		}
		this.localData.Clear();
		<InitLocalData>c__AnonStoreyDF.stages = DataReader<RenWuLianTiXiTong>.DataList;
		List<LianTiShuXing> list = DataReader<LianTiShuXing>.DataList.FindAll((LianTiShuXing e) => e.career == EntityWorld.Instance.EntSelf.TypeID);
		int i;
		for (i = 0; i < <InitLocalData>c__AnonStoreyDF.stages.get_Count(); i++)
		{
			if (!this.localData.ContainsKey(<InitLocalData>c__AnonStoreyDF.stages.get_Item(i).id))
			{
				this.localData.Add(<InitLocalData>c__AnonStoreyDF.stages.get_Item(i).id, list.FindAll((LianTiShuXing e) => e.stage == <InitLocalData>c__AnonStoreyDF.stages.get_Item(i).id));
			}
		}
	}

	private void SetAllPipeline(int id, int stage)
	{
		CharacterManager.<SetAllPipeline>c__AnonStoreyE1 <SetAllPipeline>c__AnonStoreyE = new CharacterManager.<SetAllPipeline>c__AnonStoreyE1();
		if (this.pipleLine.ContainsKey(stage))
		{
			return;
		}
		List<int> list = new List<int>();
		<SetAllPipeline>c__AnonStoreyE.gj = DataReader<GuanJianZuHe>.Get(id);
		List<LianTiShuXing> stageLocalData = this.GetStageLocalData(stage);
		int i;
		for (i = 0; i < <SetAllPipeline>c__AnonStoreyE.gj.key.get_Count(); i++)
		{
			LianTiShuXing lianTiShuXing = stageLocalData.Find((LianTiShuXing e) => e.points == <SetAllPipeline>c__AnonStoreyE.gj.key.get_Item(i));
			if (lianTiShuXing != null)
			{
				list.Add(lianTiShuXing.id);
			}
		}
		this.pipleLine.set_Item(stage, list);
	}

	public List<int> GetPiplelienPoints(int stage)
	{
		if (this.pipleLine.ContainsKey(stage))
		{
			return this.pipleLine.get_Item(stage);
		}
		return null;
	}

	public List<LianTiShuXing> GetStageLocalData(int sta)
	{
		if (this.LocalData.ContainsKey(sta))
		{
			return this.LocalData.get_Item(sta);
		}
		return null;
	}

	public RenWuLianTiXiTong GetRiseDataTpl()
	{
		return this.GetRiseDataTpl(this.stage);
	}

	public RenWuLianTiXiTong GetRiseDataTpl(int id)
	{
		return DataReader<RenWuLianTiXiTong>.Get(id);
	}

	public string GetAttrText()
	{
		Dictionary<int, float> dictionary = new Dictionary<int, float>();
		List<int> allListIds = this.GetAllListIds();
		Debuger.Error(allListIds.get_Count());
		for (int i = 0; i < allListIds.get_Count(); i++)
		{
			int attributeTemplateID = DataReader<LianTiShuXing>.Get(allListIds.get_Item(i)).attributeTemplateID;
			Attrs attrs = DataReader<Attrs>.Get(attributeTemplateID);
			for (int j = 0; j < attrs.attrs.get_Count(); j++)
			{
				if (dictionary.ContainsKey(attrs.attrs.get_Item(j)))
				{
					Dictionary<int, float> dictionary2;
					Dictionary<int, float> expr_60 = dictionary2 = dictionary;
					int num;
					int expr_71 = num = attrs.attrs.get_Item(j);
					float num2 = dictionary2.get_Item(num);
					expr_60.set_Item(expr_71, num2 + (float)attrs.values.get_Item(j));
				}
				else
				{
					dictionary.Add(attrs.attrs.get_Item(j), (float)attrs.values.get_Item(j));
				}
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		using (Dictionary<int, float>.Enumerator enumerator = dictionary.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, float> current = enumerator.get_Current();
				stringBuilder.Append(string.Concat(new object[]
				{
					GameDataUtils.GetChineseContent(current.get_Key(), false),
					":",
					current.get_Value(),
					"\n"
				}));
			}
		}
		return stringBuilder.ToString();
	}

	public string GetPointAttr(int id)
	{
		Attrs attrs = DataReader<Attrs>.Get(DataReader<LianTiShuXing>.Get(id).attributeTemplateID);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < attrs.attrs.get_Count(); i++)
		{
			stringBuilder.Append(string.Concat(new object[]
			{
				GameDataUtils.GetChineseContent(attrs.attrs.get_Item(i), false),
				"+",
				attrs.values.get_Item(i),
				"\n"
			}));
		}
		return stringBuilder.ToString();
	}

	public List<int> GetListIds(int sta)
	{
		List<int> list = new List<int>();
		if (this.stage == sta && !this.isMaxStep)
		{
			list.AddRange(this.brightPoint);
		}
		else
		{
			List<LianTiShuXing> stageLocalData = this.GetStageLocalData(sta);
			for (int i = 0; i < stageLocalData.get_Count(); i++)
			{
				list.Add(stageLocalData.get_Item(i).id);
			}
		}
		return list;
	}

	public List<int> GetAllListIds()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.stage - 1; i++)
		{
			list.AddRange(this.GetListIds(i + 1));
		}
		list.AddRange(this.GetListIds(this.stage));
		return list;
	}

	public List<XianLuShengCheng> GetStateLine(int state)
	{
		List<XianLuShengCheng> dataList = DataReader<XianLuShengCheng>.DataList;
		return dataList.FindAll((XianLuShengCheng e) => e.stage == state).FindAll((XianLuShengCheng e) => e.career == EntityWorld.Instance.EntSelf.TypeID);
	}

	public int GetStageRiseValue()
	{
		return DataReader<RenWuLianTiXiTong>.Get(this.stage).experience;
	}

	public void IsRise()
	{
		if (!this.isMaxStep && DataReader<RenWuLianTiXiTong>.Get(this.stage).minLv <= EntityWorld.Instance.EntSelf.Lv && (float)EntityWorld.Instance.EntSelf.Gold > (float)this.GetStageRiseValue() * float.Parse(DataReader<GlobalParams>.Get("refine_body_exp_to_gold_coin").value))
		{
			int num = 0;
			for (int i = 0; i < BackpackManager.Instance.RiseGoods.get_Count(); i++)
			{
				num += BackpackManager.Instance.RiseGoods.get_Item(i).LocalItem.point;
				if (this.GetStageRiseValue() <= num)
				{
					return;
				}
			}
		}
	}
}
