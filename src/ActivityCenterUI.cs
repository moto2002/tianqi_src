using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityCenterUI : UIBase
{
	private List<float>[] itemIconPositionX;

	public ActivityCenterUI()
	{
		List<float>[] expr_07 = new List<float>[3];
		int arg_1B_1 = 0;
		List<float> list = new List<float>();
		list.Add(0f);
		expr_07[arg_1B_1] = list;
		int arg_3B_1 = 1;
		list = new List<float>();
		list.Add(-60f);
		list.Add(60f);
		expr_07[arg_3B_1] = list;
		int arg_66_1 = 2;
		list = new List<float>();
		list.Add(-90f);
		list.Add(0f);
		list.Add(90f);
		expr_07[arg_66_1] = list;
		this.itemIconPositionX = expr_07;
		base..ctor();
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110038), string.Empty, delegate
		{
			this.Show(false);
			SoundManager.SetBGMFade(true);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.Init();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private DateTime ToDateTime(string timeStamp)
	{
		DateTime dateTime = TimeZone.get_CurrentTimeZone().ToLocalTime(new DateTime(1970, 1, 1));
		long num = long.Parse(timeStamp + "0000000");
		TimeSpan timeSpan = new TimeSpan(num);
		return dateTime.Add(timeSpan);
	}

	private int ToTimeStamp(DateTime time)
	{
		DateTime dateTime = TimeZone.get_CurrentTimeZone().ToLocalTime(new DateTime(1970, 1, 1));
		return (int)(time - dateTime).get_TotalSeconds();
	}

	private int GetTimeStamp(string strTime)
	{
		string[] array = strTime.Split(new char[]
		{
			':'
		});
		int num = int.Parse((!array[0].StartsWith("0")) ? array[0] : array[0].Substring(1));
		int num2 = int.Parse((!array[1].StartsWith("0")) ? array[1] : array[1].Substring(1));
		DateTime time = new DateTime(DateTime.get_Now().get_Year(), DateTime.get_Now().get_Month(), DateTime.get_Now().get_Day(), num, num2, 0);
		return this.ToTimeStamp(time);
	}

	private int GetOpenTimeSection(HuoDongZhongXin activityInfo)
	{
		int num = this.ToTimeStamp(DateTime.get_Now());
		for (int i = 0; i < activityInfo.starttime.get_Count(); i++)
		{
			if (num <= this.GetTimeStamp(activityInfo.starttime.get_Item(i)) || num <= this.GetTimeStamp(activityInfo.endtime.get_Item(i)))
			{
				return i;
			}
		}
		return Math.Max(0, activityInfo.starttime.get_Count() - 1);
	}

	private string GetFormatOpenTime(HuoDongZhongXin activityInfo)
	{
		int openTimeSection = this.GetOpenTimeSection(activityInfo);
		return activityInfo.starttime.get_Item(openTimeSection) + "-" + activityInfo.endtime.get_Item(openTimeSection);
	}

	private int GetOpenTimeStart(HuoDongZhongXin activityInfo)
	{
		int openTimeSection = this.GetOpenTimeSection(activityInfo);
		return this.GetTimeStamp(activityInfo.starttime.get_Item(openTimeSection));
	}

	private int GetOpenTimeEnd(HuoDongZhongXin activityInfo)
	{
		int openTimeSection = this.GetOpenTimeSection(activityInfo);
		return this.GetTimeStamp(activityInfo.endtime.get_Item(openTimeSection));
	}

	private bool IsCanLook(HuoDongZhongXin activityInfo)
	{
		int num = this.ToTimeStamp(DateTime.get_Now());
		int openTimeEnd = this.GetOpenTimeEnd(activityInfo);
		return num <= openTimeEnd + activityInfo.delaytime * 60;
	}

	private void Reset(Transform cell)
	{
		cell.Find("imgLock").get_gameObject().SetActive(false);
		cell.Find("txtOprate").GetComponent<Text>().set_text(string.Empty);
		cell.Find("txtOprate").GetComponent<Text>().set_color(new Color(1f, 0.490196079f, 0.294117659f));
	}

	private void SetActivityOutline(Transform cell, Color color)
	{
		for (int i = 1; i <= 3; i++)
		{
			cell.Find("Text" + i).GetComponent<Outline>().set_effectColor(color);
		}
	}

	private void SetActivityWithState(Transform cell, int activityId, ActiveCenterInfo.ActiveStatus.AS activityState)
	{
		this.Reset(cell);
		HuoDongZhongXin huoDongZhongXin = DataReader<HuoDongZhongXin>.Get(activityId);
		if (activityState == ActiveCenterInfo.ActiveStatus.AS.NotOpen)
		{
			cell.Find("imgLock").get_gameObject().SetActive(true);
			cell.Find("imgLock").Find("txtRequireLv").GetComponent<Text>().set_text(huoDongZhongXin.minLv + "级开启");
		}
		else if (activityState == ActiveCenterInfo.ActiveStatus.AS.Wait || activityState == ActiveCenterInfo.ActiveStatus.AS.PrepareOpen)
		{
			cell.Find("txtOprate").GetComponent<Text>().set_text("时间未到");
		}
		else if (activityState == ActiveCenterInfo.ActiveStatus.AS.Start)
		{
			cell.Find("txtOprate").GetComponent<Text>().set_text("点击参加");
			cell.Find("txtOprate").GetComponent<Text>().set_color(Color.get_green());
		}
		else if (activityState == ActiveCenterInfo.ActiveStatus.AS.Close)
		{
			cell.Find("txtOprate").GetComponent<Text>().set_text("已经结束");
		}
	}

	private Color GetOutlineColor(List<int> origin)
	{
		return new Color((float)origin.get_Item(0) / 255f, (float)origin.get_Item(1) / 255f, (float)origin.get_Item(2) / 255f);
	}

	private void SetOneActivity(Transform cell, int activityId)
	{
		Debug.LogError("SetOneActivity activityId=" + activityId);
		HuoDongZhongXin huoDongZhongXin = DataReader<HuoDongZhongXin>.Get(activityId);
		ResourceManager.SetTexture(cell.Find("imgIcon").GetComponent<RawImage>(), huoDongZhongXin.picture);
		cell.Find("imgIcon").GetComponent<RawImage>().SetNativeSize();
		Button component = cell.Find("imgIcon").GetComponent<Button>();
		component.get_onClick().RemoveAllListeners();
		component.get_onClick().AddListener(delegate
		{
			this.OnClickImgIcon(int.Parse(cell.get_name()));
		});
		ActiveCenterInfo activeCenterInfo = ActivityCenterManager.infoDict.get_Item(activityId);
		cell.Find("txtOpenTime").GetComponent<Text>().set_text(this.GetFormatOpenTime(huoDongZhongXin));
		string text = (activeCenterInfo.remainTimes != -1) ? activeCenterInfo.remainTimes.ToString() : "不限";
		cell.Find("txtRemainNum").GetComponent<Text>().set_text(text);
		cell.Find("txtPeopleNum").GetComponent<Text>().set_text(huoDongZhongXin.people.ToString());
		for (int i = 0; i < huoDongZhongXin.award.get_Count(); i++)
		{
			int icon = DataReader<Items>.Get(huoDongZhongXin.award.get_Item(i)).icon;
			Debug.LogError("itemIconId=" + icon);
			GameObject gameObject = ItemShow.ShowItem(cell, huoDongZhongXin.award.get_Item(i), -1L, false, null, 2001);
			gameObject.get_transform().set_localScale(new Vector3(0.8f, 0.8f, 0.8f));
			float num = this.itemIconPositionX[huoDongZhongXin.award.get_Count() - 1].get_Item(i);
			gameObject.get_transform().set_localPosition(new Vector3(num, -201f));
		}
		this.SetActivityWithState(cell, activityId, ActivityCenterManager.infoDict.get_Item(activityId).status);
	}

	private void OnClickImgIcon(int activityId)
	{
		Debug.LogError("OnClickImgIcon activityId=" + activityId);
		HuoDongZhongXin huoDongZhongXin = DataReader<HuoDongZhongXin>.Get(activityId);
		if (huoDongZhongXin == null)
		{
			return;
		}
		if (huoDongZhongXin.minLv > EntityWorld.Instance.EntSelf.Lv)
		{
			string text = string.Format(GameDataUtils.GetChineseContent(513512, false), huoDongZhongXin.minLv);
			UIManagerControl.Instance.ShowToastText(text);
			return;
		}
		if (ActivityCenterManager.infoDict.get_Item(activityId).status == ActiveCenterInfo.ActiveStatus.AS.Close)
		{
			if (!this.IsCanLook(huoDongZhongXin))
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513538, false), 2f, 2f);
				return;
			}
		}
		else if (ActivityCenterManager.infoDict.get_Item(activityId).status != ActiveCenterInfo.ActiveStatus.AS.Start)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(513526, false), 2f, 2f);
			return;
		}
		if (activityId == 10001)
		{
			InstanceManagerUI.OpenGangFightUI();
		}
		else if (activityId == 10002)
		{
			MultiPlayerManager.Instance.OpenMultiPlayerUI(10002, "多人活动");
		}
	}

	private int GetStateWeight(int activityId)
	{
		ActiveCenterInfo.ActiveStatus.AS status = ActivityCenterManager.infoDict.get_Item(activityId).status;
		ActiveCenterInfo.ActiveStatus.AS[] array = new ActiveCenterInfo.ActiveStatus.AS[]
		{
			ActiveCenterInfo.ActiveStatus.AS.Start,
			ActiveCenterInfo.ActiveStatus.AS.PrepareOpen,
			ActiveCenterInfo.ActiveStatus.AS.Wait,
			ActiveCenterInfo.ActiveStatus.AS.NotOpen,
			ActiveCenterInfo.ActiveStatus.AS.Close
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (status == array[i])
			{
				return i;
			}
		}
		return -1;
	}

	private int GetTimeWeight(int activityId)
	{
		HuoDongZhongXin activityInfo = DataReader<HuoDongZhongXin>.Get(activityId);
		ActiveCenterInfo.ActiveStatus.AS status = ActivityCenterManager.infoDict.get_Item(activityId).status;
		if (status == ActiveCenterInfo.ActiveStatus.AS.Wait || status == ActiveCenterInfo.ActiveStatus.AS.NotOpen || status == ActiveCenterInfo.ActiveStatus.AS.Close)
		{
			return this.GetOpenTimeStart(activityInfo);
		}
		return -1;
	}

	private void Init()
	{
		List<ActiveCenterInfo> activityInfoList = ActivityCenterManager.Instance.GetActivityInfoList();
		activityInfoList.Sort(delegate(ActiveCenterInfo a, ActiveCenterInfo b)
		{
			int stateWeight = this.GetStateWeight(a.id);
			int stateWeight2 = this.GetStateWeight(b.id);
			if (stateWeight != stateWeight2)
			{
				return stateWeight.CompareTo(stateWeight2);
			}
			int timeWeight = this.GetTimeWeight(a.id);
			int timeWeight2 = this.GetTimeWeight(b.id);
			if (timeWeight != timeWeight2)
			{
				return timeWeight.CompareTo(timeWeight2);
			}
			return 0;
		});
		using (List<ActiveCenterInfo>.Enumerator enumerator = activityInfoList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ActiveCenterInfo current = enumerator.get_Current();
				Debug.LogError("sort status=" + ActivityCenterManager.infoDict.get_Item(current.id).status);
			}
		}
		Transform transform = base.get_transform().Find("centre").Find("scrollRect").Find("gridLayoutGroup");
		for (int i = 0; i < transform.get_childCount(); i++)
		{
			Object.Destroy(transform.GetChild(i).get_gameObject());
		}
		int num = Mathf.CeilToInt((float)ActivityCenterManager.infoDict.get_Count() / 3f);
		for (int j = 0; j < num; j++)
		{
			GameObject gameObject = new GameObject();
			gameObject.AddComponent<RectTransform>();
			gameObject.get_transform().SetParent(transform, false);
			for (int k = 0; k < 3; k++)
			{
				int num2 = 3 * j + k;
				GameObject gameObject2;
				if (num2 >= ActivityCenterManager.infoDict.get_Count())
				{
					gameObject2 = new GameObject();
					ResourceManager.SetTexture(gameObject2.AddComponent<RawImage>(), "qidaikapai");
					gameObject2.GetComponent<RawImage>().SetNativeSize();
				}
				else
				{
					gameObject2 = ResourceManager.GetInstantiate2Prefab("ActivityCenterCell");
					gameObject2.set_name(activityInfoList.get_Item(num2).id.ToString());
					this.SetOneActivity(gameObject2.get_transform(), activityInfoList.get_Item(num2).id);
				}
				gameObject2.get_transform().SetParent(gameObject.get_transform(), false);
				gameObject2.SetActive(true);
				gameObject2.get_transform().set_localRotation(Quaternion.get_identity());
				gameObject2.get_transform().set_localPosition(new Vector3((float)(-360 + k * 360), 0f));
			}
		}
	}

	private Transform GetOneCell(int id)
	{
		Transform transform = base.get_transform().Find("centre").Find("scrollRect").Find("gridLayoutGroup");
		for (int i = 0; i < transform.get_childCount(); i++)
		{
			Transform child = transform.GetChild(i);
			for (int j = 0; j < child.get_childCount(); j++)
			{
				Transform child2 = child.GetChild(j);
				if (int.Parse(child2.get_name()) == id)
				{
					return child2;
				}
			}
		}
		return null;
	}

	public void Refresh(int id)
	{
		Debug.LogError("UpdateOneActivity id=" + id);
		Transform oneCell = this.GetOneCell(id);
		if (oneCell != null)
		{
			this.SetOneActivity(oneCell, id);
		}
	}
}
