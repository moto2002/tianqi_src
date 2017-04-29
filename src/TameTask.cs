using GameData;
using Package;
using System;
using UnityEngine;

public class TameTask : CollectTask
{
	public TameTask(Package.Task task) : base(task)
	{
	}

	protected override void SetData(Package.Task task)
	{
		base.SetData(task);
		if (base.Data != null)
		{
			this.mGoodsList.Clear();
			if (base.Targets != null && base.Targets.get_Count() > 0)
			{
				for (int i = 0; i < base.Targets.get_Count(); i++)
				{
					CaiJiPeiZhi caiJiPeiZhi = DataReader<CaiJiPeiZhi>.Get(base.Targets.get_Item(i));
					if (caiJiPeiZhi != null)
					{
						this.mGoodsList.Add(caiJiPeiZhi);
					}
				}
			}
			else
			{
				Debug.LogError(string.Format("驯服任务[{0}]配置参数[{1}]有误！", base.Data.id, base.Data.target));
			}
		}
	}

	protected override void OnFinishCollect()
	{
		if (base.Task.count < this.mGoodsList.get_Count() && this.mCurGoods != null && MainTaskManager.Instance.GoodsModels.ContainsKey(this.mCurGoods.id))
		{
			this.mLastGoodsId = this.mCurGoods.id;
			MainTaskManager.Instance.GoodsModels[this.mLastGoodsId] = 2;
			EventDispatcher.Broadcast(EventNames.FinishCollecting);
			MainTaskManager.Instance.RemoveGoodsById(this.mLastGoodsId);
		}
		base.Task.count++;
		int num = Random.Range(1, 100);
		Debug.Log("<驯服几率:" + num + "%>");
		if (base.Task.count >= this.mGoodsList.get_Count() || num < this.mCurGoods.probability)
		{
			base.Task.count = this.mGoodsList.get_Count();
			this.SendFinish();
		}
		else
		{
			base.IsCollecting = false;
			base.RefreshCollect();
			EventDispatcher.Broadcast<Package.Task>(EventNames.UpdateTaskData, base.Task);
			base.Execute(false, false);
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(310041, false));
		}
	}
}
