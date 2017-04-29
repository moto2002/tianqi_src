using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectTask : TriggerTask
{
	protected CaiJiPeiZhi mCurGoods;

	protected List<CaiJiPeiZhi> mGoodsList = new List<CaiJiPeiZhi>();

	protected int mLastGoodsId;

	public bool IsCollecting
	{
		get;
		protected set;
	}

	public CaiJiPeiZhi CurGoods
	{
		get
		{
			return this.mCurGoods;
		}
	}

	public CollectTask(Package.Task task) : base(task)
	{
	}

	public override void SetTask(Package.Task task, bool isRecycle = true)
	{
		base.SetTask(task, isRecycle);
		this.IsCollecting = false;
		this.InitCollect();
		this.RefreshCollect();
	}

	protected void InitCollect()
	{
		if (base.Data != null && base.Targets != null)
		{
			this.mGoodsList.Clear();
			for (int i = 0; i < base.Targets.get_Count() - 1; i++)
			{
				CaiJiPeiZhi caiJiPeiZhi = DataReader<CaiJiPeiZhi>.Get(base.Targets.get_Item(i));
				if (caiJiPeiZhi != null)
				{
					this.mGoodsList.Add(caiJiPeiZhi);
				}
			}
		}
	}

	protected void RefreshCollect()
	{
		bool flag = false;
		for (int i = 0; i < this.mGoodsList.get_Count(); i++)
		{
			if (i >= base.Task.count)
			{
				CaiJiPeiZhi caiJiPeiZhi = this.mGoodsList.get_Item(i);
				if (caiJiPeiZhi.scene == MySceneManager.Instance.CurSceneID && caiJiPeiZhi.model > 0 && base.Task.status == Package.Task.TaskStatus.TaskReceived)
				{
					if (MainTaskManager.Instance.GoodsModels.ContainsKey(caiJiPeiZhi.id))
					{
						MainTaskManager.Instance.GoodsModels[caiJiPeiZhi.id] = 0;
					}
					else
					{
						MainTaskManager.Instance.GoodsModels.Add(caiJiPeiZhi.id, 0);
					}
					flag = true;
				}
			}
		}
		if (flag)
		{
			EventDispatcher.Broadcast(EventNames.RefreshCollect);
		}
		this.mCurGoods = this.GetGoodsData();
	}

	public override void KillTask()
	{
		if (this.mGoodsList != null)
		{
			for (int i = 0; i < this.mGoodsList.get_Count(); i++)
			{
				MainTaskManager.Instance.GoodsModels.Remove(this.mGoodsList.get_Item(i).id);
			}
			this.mGoodsList.Clear();
		}
		base.KillTask();
	}

	public override void Dispose()
	{
		base.Dispose();
		this.mGoodsList = null;
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(CollectionNPCBehavior.OnEnterNPC, new Callback<int>(this.OnEnterCollect));
		EventDispatcher.AddListener<int>(CollectionNPCBehavior.OnExitNPC, new Callback<int>(this.OnExitCollect));
		EventDispatcher.AddListener<bool>(EventNames.FlyShoeTransportRes, new Callback<bool>(this.OnFlyShoeTransportRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(CollectionNPCBehavior.OnEnterNPC, new Callback<int>(this.OnEnterCollect));
		EventDispatcher.RemoveListener<int>(CollectionNPCBehavior.OnExitNPC, new Callback<int>(this.OnExitCollect));
		EventDispatcher.RemoveListener<bool>(EventNames.FlyShoeTransportRes, new Callback<bool>(this.OnFlyShoeTransportRes));
	}

	protected override void StartExecute(bool isFastNav)
	{
		EntitySelf entSelf = EntityWorld.Instance.EntSelf;
		if (entSelf != null && entSelf.Actor)
		{
			if (this.mCurGoods != null)
			{
				Vector3 vector = new Vector3((float)this.mCurGoods.position.get_Item(0), (float)this.mCurGoods.position.get_Item(1), (float)this.mCurGoods.position.get_Item(2)) * 0.01f;
				Vector3 vector2 = new Vector3((float)this.mCurGoods.flyPosition.get_Item(0), (float)this.mCurGoods.flyPosition.get_Item(1), (float)this.mCurGoods.flyPosition.get_Item(2)) * 0.01f;
				if (this.mCurGoods.scene == MySceneManager.Instance.CurSceneID)
				{
					Vector3 vector3 = Vector3.get_zero();
					if (this.mCurGoods.face.get_Count() >= 3)
					{
						vector3 = new Vector3((float)this.mCurGoods.face.get_Item(0), (float)this.mCurGoods.face.get_Item(1), (float)this.mCurGoods.face.get_Item(2)) * 0.01f;
					}
					Quaternion quaternion = Quaternion.Euler(vector3.x, vector3.y, vector3.z);
					Vector3 vector4 = quaternion * Vector3.get_forward() * 2f;
					float distanceNoY = XUtility.GetDistanceNoY(entSelf.Actor.FixTransform.get_position(), vector);
					if (isFastNav)
					{
						this.FastCollectGoods(distanceNoY, vector, vector2);
					}
					else
					{
						this.NormalCollectGoods(distanceNoY, vector);
					}
				}
				else if (isFastNav)
				{
					base.IsAutoNav = true;
					MainTaskManager.Instance.DelaySendFlyShoe(true, this.mCurGoods.scene, vector2);
				}
				else
				{
					base.IsAutoNav = false;
					TownCountdownUI townCountdownUI = UIManagerControl.Instance.OpenUI("TownCountdownUI", null, false, UIType.NonPush) as TownCountdownUI;
					townCountdownUI.StartTransmit(base.Task.taskId, this.mCurGoods.scene);
				}
			}
			else
			{
				Debug.LogFormat("<color=red>Error:</color>采集任务[{0}]数据已被销毁!!!", new object[]
				{
					base.Task.taskId
				});
			}
		}
	}

	protected void FastCollectGoods(float dist, Vector3 goodsPoint, Vector3 flyPoint)
	{
		if (dist > MainTaskManager.Instance.UseFlyShoeMinDist && !base.IsTrigger)
		{
			base.IsAutoNav = true;
			MainTaskManager.Instance.DelaySendFlyShoe(false, this.mCurGoods.scene, flyPoint);
		}
		else if (MainTaskManager.Instance.IsAutoFast)
		{
			this.NormalCollectGoods(dist, goodsPoint);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505300, false), 1f, 1f);
		}
	}

	protected void NormalCollectGoods(float dist, Vector3 goodsPoint)
	{
		if (dist > 1f && !base.IsTrigger)
		{
			base.IsAutoNav = true;
			EventDispatcher.Broadcast(EventNames.BeginNav);
			EntityWorld.Instance.EntSelf.NavToNPC(this.mCurGoods.scene, goodsPoint, 0.5f, new Action(this.OnStopNav));
		}
		else if (this.mCurGoods != null)
		{
			base.IsAutoNav = false;
			this.EnterCollect(this.mCurGoods.id);
		}
	}

	protected void OnStopNav()
	{
		if (base.IsAutoNav && this.mCurGoods != null)
		{
			this.EnterCollect(this.mCurGoods.id);
		}
		else
		{
			Debug.Log("采集物品数据为空!!!");
		}
	}

	protected Vector3 GetGoodsPosition(List<int> list, int index)
	{
		int num = list.get_Count() / 3;
		if (num > 0 && list.get_Count() % 3 == 0)
		{
			int num2 = index % num * 3;
			return new Vector3((float)list.get_Item(num2), (float)list.get_Item(num2 + 1), (float)list.get_Item(num2 + 2)) * 0.01f;
		}
		Debug.Log("<color=red>Error:</color>采集物品位置配置有误！");
		return Vector3.get_zero();
	}

	protected void OnEnterCollect(int goodsId)
	{
		Debug.Log("====>>>进入[" + goodsId + "]采集范围");
		if (this.IsActive)
		{
			if (this.mCurGoods != null && this.mCurGoods.id == goodsId)
			{
				base.IsTrigger = true;
			}
			if (base.Task.status == Package.Task.TaskStatus.TaskReceived)
			{
				if (CityManager.Instance.NeedDelayEnterNPC)
				{
					TimerHeap.AddTimer(600u, 0, delegate
					{
						this.EnterCollect(goodsId);
					});
				}
				else if (base.IsAutoNav)
				{
					this.EnterCollect(goodsId);
				}
			}
		}
	}

	protected void EnterCollect(int goodsId)
	{
		if (!this.IsCollecting && this.mCurGoods != null && this.mCurGoods.id == goodsId)
		{
			MainTaskManager.Instance.StopToNPC(false);
			this.PlayCollect(this.mCurGoods, this.mCurGoods.action);
		}
		CityManager.Instance.NeedDelayEnterNPC = false;
	}

	protected virtual void OnExitCollect(int goodsId)
	{
		Debug.Log("<<<====离开[" + goodsId + "]采集范围");
		if (this.IsActive && this.mCurGoods != null && this.mCurGoods.id == goodsId)
		{
			base.IsTrigger = false;
		}
	}

	protected void PlayCollect(CaiJiPeiZhi goods, string ActionName)
	{
		if (goods == null)
		{
			return;
		}
		this.IsCollecting = true;
		EntityWorld.Instance.EntSelf.Actor.GetComponentInChildren<Animator>().Play(ActionName);
		if (MainTaskManager.Instance.GoodsModels.ContainsKey(goods.id))
		{
			MainTaskManager.Instance.GoodsModels[goods.id] = 1;
			EventDispatcher.Broadcast(EventNames.StartCollecting);
		}
		TownCountdownUI townCountdownUI = UIManagerControl.Instance.OpenUI("TownCountdownUI", null, false, UIType.NonPush) as TownCountdownUI;
		townCountdownUI.StartCountdown((float)goods.time / 1000f, GameDataUtils.GetChineseContent(goods.tips, false), base.Task.taskId, new Action(this.OnFinishCollect), new Action<bool>(this.OnStopCollect));
	}

	protected void PlayStand()
	{
		EntityWorld.Instance.EntSelf.Actor.GetComponentInChildren<Animator>().Play("idle_city");
	}

	protected virtual void OnFinishCollect()
	{
		if (base.Task.count < this.mGoodsList.get_Count() && this.mCurGoods != null && MainTaskManager.Instance.GoodsModels.ContainsKey(this.mCurGoods.id))
		{
			this.mLastGoodsId = this.mCurGoods.id;
			MainTaskManager.Instance.GoodsModels[this.mLastGoodsId] = 2;
			EventDispatcher.Broadcast(EventNames.FinishCollecting);
			MainTaskManager.Instance.RemoveGoodsById(this.mLastGoodsId);
		}
		base.Task.count++;
		if (base.Task.count >= this.mGoodsList.get_Count())
		{
			base.Task.count = this.mGoodsList.get_Count();
			this.SendFinish();
		}
		else
		{
			this.IsCollecting = false;
			this.RefreshCollect();
			EventDispatcher.Broadcast<Package.Task>(EventNames.UpdateTaskData, base.Task);
			this.FinishAfter();
		}
	}

	public override void FinishAfter()
	{
		if (this.mCurGoods != null)
		{
			if (string.IsNullOrEmpty(this.mCurGoods.action2))
			{
				base.Execute(false, false);
			}
			else
			{
				TimerHeap.AddTimer(600u, 0, delegate
				{
					base.Execute(false, false);
				});
			}
		}
	}

	public void OnStopCollect(bool isAuto)
	{
		if (EntityWorld.Instance.EntSelf.Actor.CurActionStatus != "run_city" && EntityWorld.Instance.EntSelf.Actor.CurActionStatus != "idle_city")
		{
			this.PlayStand();
		}
		if (!isAuto && this.mCurGoods != null && MainTaskManager.Instance.GoodsModels.ContainsKey(this.mCurGoods.id))
		{
			MainTaskManager.Instance.GoodsModels[this.mCurGoods.id] = 0;
			EventDispatcher.Broadcast(EventNames.StopCollecting);
		}
		this.IsCollecting = false;
	}

	protected void OnNpcDieEnd(int goodsId)
	{
		if (this.IsActive && ((base.Task.status == Package.Task.TaskStatus.WaitingToClaimPrize && base.Task.taskId == MainTaskManager.Instance.CurTaskId) || (base.Task.status == Package.Task.TaskStatus.TaskReceived && this.mCurGoods != null && this.mCurGoods.model > 0 && this.mLastGoodsId == goodsId)))
		{
			base.Execute(false, false);
		}
	}

	protected void OnFlyShoeTransportRes(bool isSuccess)
	{
		if (MainTaskManager.Instance.CurTaskId == base.Task.taskId)
		{
			if (CityManager.Instance.NeedSwitchCity)
			{
				MainTaskManager.Instance.IsSwitchCityByTask = true;
			}
			else
			{
				TimerHeap.AddTimer(600u, 0, delegate
				{
					base.Execute(false, false);
				});
			}
		}
	}

	public virtual CaiJiPeiZhi GetGoodsData()
	{
		if (this.mGoodsList != null && this.mGoodsList.get_Count() > 0)
		{
			return this.mGoodsList.get_Item(base.Task.count % this.mGoodsList.get_Count());
		}
		if (this.mGoodsList == null)
		{
			Debug.LogFormat("<color=red>Error:</color>采集任务[{0}]数据已被销毁!!!", new object[]
			{
				base.Task.taskId
			});
		}
		else if (this.mGoodsList.get_Count() <= 0)
		{
			Debug.LogFormat("<color=red>Error:</color>采集任务[{0}]配置有误!!!", new object[]
			{
				base.Task.taskId
			});
		}
		return null;
	}

	public List<CaiJiPeiZhi> GetGoodsDataList()
	{
		return this.mGoodsList;
	}
}
