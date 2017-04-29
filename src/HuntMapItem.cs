using System;
using UnityEngine;
using UnityEngine.UI;

public class HuntMapItem : MonoBehaviour
{
	public enum State
	{
		OPEN,
		LOW,
		HIGH
	}

	public Predicate<HuntMapItem> EventHandler;

	private Text mTxCityName;

	private Text mTxLevel;

	private GameObject mGoLock;

	private GameObject mGoSelect;

	public int Id
	{
		get;
		private set;
	}

	public int MinLevel
	{
		get;
		private set;
	}

	public int MaxLevel
	{
		get;
		private set;
	}

	public int Name
	{
		get;
		private set;
	}

	public object Data
	{
		get;
		private set;
	}

	public HuntMapItem.State CurState
	{
		get;
		private set;
	}

	public bool IsLock
	{
		get
		{
			return this.mGoLock.get_activeSelf();
		}
		set
		{
			this.mGoLock.SetActive(value);
		}
	}

	public bool IsSelect
	{
		get
		{
			return this.mGoSelect.get_activeSelf();
		}
		set
		{
			this.mGoSelect.SetActive(value);
		}
	}

	private void Awake()
	{
		this.mTxCityName = UIHelper.GetText(base.get_transform(), "background/txName");
		this.mTxLevel = UIHelper.GetText(base.get_transform(), "background/txLevel");
		this.mGoLock = UIHelper.GetObject(base.get_transform(), "background/Lock");
		this.mGoSelect = UIHelper.GetObject(base.get_transform(), "background/Select");
		base.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickMap);
	}

	private void OnClickMap(GameObject go)
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke(this);
		}
	}

	public void SetData(int id, int minLv, int maxLv, int name, object data)
	{
		this.Id = id;
		this.MinLevel = minLv;
		this.MaxLevel = maxLv;
		this.Name = name;
		this.Data = data;
		this.mTxCityName.set_text(GameDataUtils.GetChineseContent(name, false));
		this.Refresh();
	}

	public void Refresh()
	{
		int lv = EntityWorld.Instance.EntSelf.Lv;
		if (lv < this.MinLevel)
		{
			this.CurState = HuntMapItem.State.HIGH;
			this.IsLock = true;
		}
		else if (lv > this.MaxLevel)
		{
			this.CurState = HuntMapItem.State.LOW;
			this.IsLock = true;
		}
		else
		{
			this.CurState = HuntMapItem.State.OPEN;
			this.IsLock = false;
		}
		this.mTxLevel.set_text(string.Format(GameDataUtils.GetChineseContent(511637, false), this.MinLevel, this.MaxLevel));
	}
}
