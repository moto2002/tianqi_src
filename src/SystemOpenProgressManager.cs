using GameData;
using System;
using System.Collections.Generic;

public class SystemOpenProgressManager : BaseSubSystemManager
{
	private List<SystemOpen> m_listSystemOpen;

	private SystemOpen CurSystemData;

	private static SystemOpenProgressManager instance;

	private List<SystemOpen> listSystemOpen
	{
		get
		{
			this.SetSystemOpenList();
			return this.m_listSystemOpen;
		}
	}

	public static SystemOpenProgressManager Instance
	{
		get
		{
			if (SystemOpenProgressManager.instance == null)
			{
				SystemOpenProgressManager.instance = new SystemOpenProgressManager();
			}
			return SystemOpenProgressManager.instance;
		}
	}

	private SystemOpenProgressManager()
	{
	}

	private void SetSystemOpenList()
	{
		if (this.m_listSystemOpen == null)
		{
			this.m_listSystemOpen = new List<SystemOpen>();
			List<SystemOpen> dataList = DataReader<SystemOpen>.DataList;
			for (int i = 0; i < dataList.get_Count(); i++)
			{
				SystemOpen systemOpen = dataList.get_Item(i);
				if (systemOpen != null && systemOpen.notice == 1)
				{
					this.m_listSystemOpen.Add(systemOpen);
				}
			}
			this.m_listSystemOpen.Sort(new Comparison<SystemOpen>(SystemOpenProgressManager.SortCompare2SO));
		}
	}

	private static int SortCompare2SO(SystemOpen dataSO1, SystemOpen dataSO2)
	{
		int result = 0;
		if (dataSO1.id == dataSO2.id)
		{
			result = 0;
		}
		else if (dataSO1.level < dataSO2.level)
		{
			result = -1;
		}
		else if (dataSO1.level > dataSO2.level)
		{
			result = 1;
		}
		else if (dataSO1.id < dataSO2.id)
		{
			result = -1;
		}
		else if (dataSO1.id > dataSO2.id)
		{
			result = 1;
		}
		return result;
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.m_listSystemOpen = null;
		this.CurSystemData = null;
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener("UpgradeManager.RoleSelfLevelUp", new Callback(this.OnLevelUp));
	}

	private void OnLevelUp()
	{
	}

	public void Refresh()
	{
		this.SetCurrentData();
		this.RefreshUI();
	}

	public void RefreshUI()
	{
		if (!UIManagerControl.Instance.IsOpen("TownUI"))
		{
			return;
		}
		if (SystemOpenProgressUIView.Instance == null)
		{
			return;
		}
		if (!this.IsSystemProgressOn())
		{
			SystemOpenProgressUIView.Instance.get_gameObject().SetActive(false);
		}
		else
		{
			SystemOpenProgressUIView.Instance.get_gameObject().SetActive(true);
			SystemOpenProgressUIView.Instance.SetSystemIcon(this.CurSystemData.icon, this.CurSystemData.icon2);
			SystemOpenProgressUIView.Instance.SetSystemDesc(GameDataUtils.GetChineseContent(this.CurSystemData.description, false));
			SystemOpenProgressUIView.Instance.SetSystemLevel(this.CurSystemData.level);
		}
	}

	public bool IsSystemProgressOn()
	{
		return this.CurSystemData != null;
	}

	public void OpenSystemOpenDescUI()
	{
		if (this.CurSystemData != null)
		{
			UIManagerControl.Instance.OpenUI("SystemOpenDescUI", UINodesManager.TopUIRoot, false, UIType.NonPush);
			SystemOpenDescUIView.Instance.RefreshUI(this.CurSystemData);
		}
	}

	private void SetCurrentData()
	{
		this.CurSystemData = null;
		for (int i = 0; i < this.listSystemOpen.get_Count(); i++)
		{
			SystemOpen systemOpen = this.listSystemOpen.get_Item(i);
			if (!this.IsSystemOpened(systemOpen))
			{
				this.CurSystemData = systemOpen;
				return;
			}
		}
	}

	private bool IsSystemOpened(SystemOpen dataSO)
	{
		return dataSO != null && EntityWorld.Instance.EntSelf != null && dataSO.level <= EntityWorld.Instance.EntSelf.Lv && (dataSO.taskId <= 0 || MainTaskManager.Instance.IsFinishedTask(dataSO.taskId));
	}
}
