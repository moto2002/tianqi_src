using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RiseItem : MonoBehaviour
{
	private LinePool poolLine;

	private ListPool poolPoint;

	private int stage;

	private bool isAction;

	private List<LianTiShuXing> data;

	private List<int> nexts = new List<int>();

	private void Awake()
	{
		this.poolPoint = base.GetComponent<ListPool>();
		this.poolLine = base.get_transform().FindChild("Line").GetComponent<LinePool>();
		EventDispatcher.AddListener<int, bool>(EventNames.UpdateRiseItem, new Callback<int, bool>(this.OnUpdateRiseItem));
		EventDispatcher.AddListener<int>(EventNames.LightEndPoint, new Callback<int>(this.OnLightEndPoint));
	}

	private void OnDestroy()
	{
		EventDispatcher.RemoveListener<int, bool>(EventNames.UpdateRiseItem, new Callback<int, bool>(this.OnUpdateRiseItem));
		EventDispatcher.RemoveListener<int>(EventNames.LightEndPoint, new Callback<int>(this.OnLightEndPoint));
	}

	private void OnLightEndPoint(int receiveStage)
	{
	}

	private void OnUpdateRiseItem(int sta, bool isFi)
	{
		if (sta == this.stage)
		{
			this.nexts.Clear();
			List<int> currentPoints = this.poolLine.CreateAllLine(sta, out this.nexts, false);
			this.UpdatePointAlpha(currentPoints, delegate
			{
				this.poolLine.PointToPointAnim(isFi);
			});
		}
	}

	private void UpdatePointAlpha(List<int> currentPoints, Action callBack)
	{
		int num = 0;
		if (num >= 0 && currentPoints.get_Count() > 0)
		{
			int i;
			for (i = 0; i < currentPoints.get_Count(); i++)
			{
				if (currentPoints.get_Item(i) == 0 && callBack != null)
				{
					callBack.Invoke();
					return;
				}
				num = this.data.FindIndex((LianTiShuXing e) => e.id == currentPoints.get_Item(i));
				if (num >= 0)
				{
					this.poolPoint.Items.get_Item(num).GetComponent<RisePoint>().FadeInOutAlpha((i != 0) ? null : callBack);
				}
			}
		}
	}

	public void UpdateLine(int id)
	{
		int num = this.data.FindIndex((LianTiShuXing e) => e.id == id);
		if (num >= 0)
		{
			this.poolPoint.Items.get_Item(num).GetComponent<RisePoint>().OnLightPoint(this.data.get_Item(num).id, false, false);
		}
		CharacterManager.Instance.NewBrightPoint = 0;
	}

	public void StartAllAnim()
	{
		this.poolLine.AllLineAnim();
	}

	public void UpdateDataActive(int id, bool isAct = true)
	{
		this.stage = id;
		this.isAction = isAct;
		this.poolLine.CreateAllLine(id, out this.nexts, this.isAction);
		this.data = CharacterManager.Instance.GetStageLocalData(this.stage);
		this.poolPoint.Create(this.data.get_Count(), delegate(int index)
		{
			if (this.isAction)
			{
				List<int> piplelienPoints = CharacterManager.Instance.GetPiplelienPoints(this.stage);
				if (index < this.data.get_Count() && index < this.poolPoint.Items.get_Count())
				{
					bool isYellow = piplelienPoints.Exists((int e) => e == this.data.get_Item(index).id);
					this.poolPoint.Items.get_Item(index).GetComponent<RisePoint>().SetData(this.data.get_Item(index), this.isAction, isYellow, false);
				}
				this.OnLightEndPoint(this.stage);
			}
			else
			{
				List<int> brightPoint = CharacterManager.Instance.BrightPoint;
				if (index < this.data.get_Count() && index < this.poolPoint.Items.get_Count())
				{
					bool flag = brightPoint.Exists((int e) => e == this.data.get_Item(index).id);
					this.poolPoint.Items.get_Item(index).GetComponent<RisePoint>().SetData(this.data.get_Item(index), flag, false, false);
				}
			}
		});
	}
}
