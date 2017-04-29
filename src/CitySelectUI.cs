using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CitySelectUI : UIBase
{
	protected const string Connector = "To";

	protected const int PathPointDistance = 16;

	protected const int PathTotalTime = 1000;

	protected const int PathBeginTime = 50;

	protected const int PathEndTime = 100;

	protected Transform CityUnits;

	protected Dictionary<int, CitySelectUnit> cityUnitDic = new Dictionary<int, CitySelectUnit>();

	protected int cityCurrentUnitKey;

	protected Transform CityPaths;

	protected List<GameObject> cityPathPoint = new List<GameObject>();

	protected Dictionary<string, List<Vector3>> cityPathPointPositionDic = new Dictionary<string, List<Vector3>>();

	protected List<uint> animationTimer = new List<uint>();

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = false;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.CityUnits = base.FindTransform("CityUnits");
		List<ZhuChengPeiZhi> dataList = DataReader<ZhuChengPeiZhi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (!this.cityUnitDic.ContainsKey(dataList.get_Item(i).scene))
			{
				if (dataList.get_Item(i).mainSenceIcon != 0)
				{
					if (dataList.get_Item(i).mainSenceIconPoint.get_Count() >= 2)
					{
						CitySelectUnit citySelectUnit = ResourceManager.GetInstantiate2Prefab("CitySelectUnit").AddUniqueComponent<CitySelectUnit>();
						citySelectUnit.get_transform().SetParent(this.CityUnits);
						citySelectUnit.get_transform().set_localPosition(Vector3.get_zero());
						citySelectUnit.SetData(dataList.get_Item(i).scene, dataList.get_Item(i).mainSenceIcon, dataList.get_Item(i).mainSenceIconPoint);
						this.cityUnitDic.Add(dataList.get_Item(i).scene, citySelectUnit);
					}
				}
			}
		}
		this.CityPaths = base.FindTransform("CityPaths");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110035), string.Empty, delegate
		{
			base.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
	}

	protected override void OnDisable()
	{
		for (int i = 0; i < this.animationTimer.get_Count(); i++)
		{
			TimerHeap.DelTimer(this.animationTimer.get_Item(i));
		}
		this.animationTimer.Clear();
		CurrenciesUIViewModel.Show(false);
		base.OnDisable();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(CitySelectUIEvent.OnUnitClick, new Callback<int>(this.OnUnitClick));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(CitySelectUIEvent.OnUnitClick, new Callback<int>(this.OnUnitClick));
	}

	public void OnUnitClick(int cityID)
	{
		if (this.cityCurrentUnitKey == 0 || !this.cityUnitDic.ContainsKey(cityID) || this.cityCurrentUnitKey == cityID)
		{
			EventDispatcher.Broadcast<int>(CityManagerEvent.ChangeCityByIntegrationHearth, cityID);
		}
		else
		{
			WaitingUIView waitingUIView = UIManagerControl.Instance.OpenUI("WaitingUI", null, false, UIType.NonPush) as WaitingUIView;
			waitingUIView.SetAlpha(0f);
			string text = this.cityCurrentUnitKey + "To" + cityID;
			if (!this.cityPathPointPositionDic.ContainsKey(text))
			{
				this.CreatePath(text, this.cityUnitDic.get_Item(this.cityCurrentUnitKey).get_transform(), this.cityUnitDic.get_Item(cityID).get_transform());
			}
			this.PlayPathAnima(text, delegate
			{
				if (WaitingUIView.Instance != null)
				{
					waitingUIView.SetAlpha(1f);
					WaitingUIView.Instance.Show(false);
				}
				EventDispatcher.Broadcast<int>(CityManagerEvent.ChangeCityByIntegrationHearth, cityID);
			});
		}
	}

	public void UpdateData(int currentCityID, List<int> openCityID)
	{
		bool flag = false;
		using (Dictionary<int, CitySelectUnit>.Enumerator enumerator = this.cityUnitDic.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, CitySelectUnit> current = enumerator.get_Current();
				if (current.get_Key() == currentCityID)
				{
					flag = true;
					this.cityCurrentUnitKey = currentCityID;
					current.get_Value().SetCurrent(true);
				}
				else
				{
					current.get_Value().SetCurrent(false);
				}
				current.get_Value().Show(openCityID.Contains(current.get_Key()));
			}
		}
		using (List<GameObject>.Enumerator enumerator2 = this.cityPathPoint.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				GameObject current2 = enumerator2.get_Current();
				current2.SetActive(false);
			}
		}
		if (!flag)
		{
			this.cityCurrentUnitKey = 0;
		}
	}

	protected void CreatePath(string pathKey, Transform fromTransform, Transform toTransform)
	{
		this.cityPathPointPositionDic.Add(pathKey, new List<Vector3>());
		float num = toTransform.get_localPosition().x - fromTransform.get_localPosition().x;
		float num2 = toTransform.get_localPosition().y - fromTransform.get_localPosition().y;
		int num3 = (int)(Mathf.Sqrt(Mathf.Pow(num, 2f) + Mathf.Pow(num2, 2f)) / 16f);
		for (int i = 0; i < num3; i++)
		{
			this.cityPathPointPositionDic.get_Item(pathKey).Add(new Vector3(fromTransform.get_localPosition().x + num / (float)(num3 + 1) * (float)(i + 1), fromTransform.get_localPosition().y + num2 / (float)(num3 + 1) * (float)(i + 1), fromTransform.get_localPosition().z));
		}
		int num4 = num3 - this.cityPathPoint.get_Count();
		if (num4 > 0)
		{
			for (int j = 0; j < num4; j++)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("CityPathPoint");
				instantiate2Prefab.get_transform().SetParent(this.CityPaths);
				instantiate2Prefab.get_transform().set_localPosition(Vector3.get_zero());
				instantiate2Prefab.get_transform().set_localScale(Vector3.get_one());
				instantiate2Prefab.SetActive(false);
				this.cityPathPoint.Add(instantiate2Prefab);
			}
		}
	}

	protected void PlayPathAnima(string pathKey, Action callback = null)
	{
		if (this.cityPathPoint.get_Count() < this.cityPathPointPositionDic.get_Item(pathKey).get_Count())
		{
			if (callback != null)
			{
				callback.Invoke();
			}
			return;
		}
		int num = 850 / this.cityPathPointPositionDic.get_Item(pathKey).get_Count();
		for (int i = 0; i < this.cityPathPointPositionDic.get_Item(pathKey).get_Count(); i++)
		{
			int index = i;
			if (index == this.cityPathPointPositionDic.get_Item(pathKey).get_Count() - 1)
			{
				this.animationTimer.Add(TimerHeap.AddTimer((uint)(50 + index * num), 0, delegate
				{
					this.cityPathPoint.get_Item(index).get_transform().set_localPosition(this.cityPathPointPositionDic.get_Item(pathKey).get_Item(index));
					this.cityPathPoint.get_Item(index).SetActive(true);
					this.animationTimer.Add(TimerHeap.AddTimer(100u, 0, delegate
					{
						if (callback != null)
						{
							callback.Invoke();
						}
					}));
				}));
			}
			else
			{
				this.animationTimer.Add(TimerHeap.AddTimer((uint)(50 + index * num), 0, delegate
				{
					this.cityPathPoint.get_Item(index).get_transform().set_localPosition(this.cityPathPointPositionDic.get_Item(pathKey).get_Item(index));
					this.cityPathPoint.get_Item(index).SetActive(true);
				}));
			}
		}
	}
}
