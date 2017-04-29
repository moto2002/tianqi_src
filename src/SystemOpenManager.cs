using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SystemOpenManager
{
	private static List<SystemOpen> m_ButtonsOfLeftMiddle;

	private static List<SystemOpen> m_ButtonsOfRightTop;

	private static List<SystemOpen> ButtonsOfLeftMiddle
	{
		get
		{
			if (SystemOpenManager.m_ButtonsOfLeftMiddle == null)
			{
				SystemOpenManager.m_ButtonsOfLeftMiddle = new List<SystemOpen>();
				List<SystemOpen> dataList = DataReader<SystemOpen>.DataList;
				for (int i = 0; i < dataList.get_Count(); i++)
				{
					if (dataList.get_Item(i).area == 2)
					{
						SystemOpenManager.m_ButtonsOfLeftMiddle.Add(dataList.get_Item(i));
					}
				}
			}
			return SystemOpenManager.m_ButtonsOfLeftMiddle;
		}
	}

	private static List<SystemOpen> ButtonsOfRightTop
	{
		get
		{
			if (SystemOpenManager.m_ButtonsOfRightTop == null)
			{
				SystemOpenManager.m_ButtonsOfRightTop = new List<SystemOpen>();
				List<SystemOpen> dataList = DataReader<SystemOpen>.DataList;
				for (int i = 0; i < dataList.get_Count(); i++)
				{
					if (dataList.get_Item(i).area == 3)
					{
						SystemOpenManager.m_ButtonsOfRightTop.Add(dataList.get_Item(i));
					}
				}
			}
			return SystemOpenManager.m_ButtonsOfRightTop;
		}
	}

	public static bool IsSystemClickOpen(int systemId, int levelTipID = 0, bool isTip = true)
	{
		if (ClientGMManager.Instance.IsAlwaysOpen)
		{
			return true;
		}
		if (systemId <= 0)
		{
			return true;
		}
		if (!SystemConfig.IsOpenPay)
		{
			switch (systemId)
			{
			case 7:
			case 9:
			case 12:
				goto IL_74;
			case 8:
			case 10:
			case 11:
				IL_46:
				switch (systemId)
				{
				case 29:
				case 32:
					goto IL_74;
				case 30:
				case 31:
					IL_5F:
					if (systemId != 62 && systemId != 70)
					{
						goto IL_80;
					}
					goto IL_74;
				}
				goto IL_5F;
			}
			goto IL_46;
			IL_74:
			Debug.Log("充值未开放!!!");
			return false;
		}
		IL_80:
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(systemId);
		if (systemOpen != null && EntityWorld.Instance.EntSelf != null)
		{
			if (EntityWorld.Instance.EntSelf.Lv < systemOpen.level)
			{
				if (isTip)
				{
					if (levelTipID == 0)
					{
						if (systemId == 19)
						{
							UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(502255, false), systemOpen.level));
						}
						else
						{
							UIManagerControl.Instance.ShowToastText("系统未开放, 开放等级" + systemOpen.level);
						}
					}
					else if (levelTipID > 0)
					{
						UIManagerControl.Instance.ShowToastText(string.Format(GameDataUtils.GetChineseContent(levelTipID, false), systemOpen.level));
					}
				}
				return false;
			}
			if (systemOpen.taskId > 0 && !MainTaskManager.Instance.IsFinishedTask(systemOpen.taskId))
			{
				if (isTip)
				{
					RenWuPeiZhi renWuPeiZhi = DataReader<RenWuPeiZhi>.Get(systemOpen.taskId);
					if (renWuPeiZhi != null)
					{
						UIManagerControl.Instance.ShowToastText(string.Format("系统未开放, [{0}]任务未完成", GameDataUtils.GetChineseContent(renWuPeiZhi.dramaIntroduce, false)));
					}
					else
					{
						UIManagerControl.Instance.ShowToastText("系统未开放, 依赖任务未完成");
					}
				}
				return false;
			}
			if (systemOpen.levelClose > 0 && EntityWorld.Instance.EntSelf.Lv > systemOpen.levelClose)
			{
				if (isTip)
				{
					UIManagerControl.Instance.ShowToastText("系统已经关闭, 关闭等级" + systemOpen.levelClose);
				}
				return false;
			}
		}
		return true;
	}

	public static bool IsSystemHideEntrance(int systemId)
	{
		if (ClientGMManager.Instance.IsAlwaysOpen)
		{
			return true;
		}
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(systemId);
		return systemOpen != null && systemOpen.effect == 2 && !SystemOpenManager.IsSystemOn(systemId);
	}

	public static bool IsSystemOn(int systemId)
	{
		if (ClientGMManager.Instance.IsAlwaysOpen)
		{
			return true;
		}
		if (systemId <= 0)
		{
			return true;
		}
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(systemId);
		if (systemOpen != null && EntityWorld.Instance.EntSelf != null)
		{
			if (EntityWorld.Instance.EntSelf.Lv < systemOpen.level)
			{
				return false;
			}
			if (systemOpen.taskId > 0 && !MainTaskManager.Instance.IsFinishedTask(systemOpen.taskId))
			{
				return false;
			}
			if (systemOpen.levelClose > 0 && EntityWorld.Instance.EntSelf.Lv > systemOpen.levelClose)
			{
				return false;
			}
		}
		return true;
	}

	public static bool IsSystemOn(int systemId, out SystemOpenFailedType failedType, out int failedValue)
	{
		if (ClientGMManager.Instance.IsAlwaysOpen)
		{
			failedType = SystemOpenFailedType.None;
			failedValue = 0;
			return true;
		}
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(systemId);
		if (systemOpen != null && EntityWorld.Instance.EntSelf != null)
		{
			if (EntityWorld.Instance.EntSelf.Lv < systemOpen.level)
			{
				failedType = SystemOpenFailedType.LevelOpen;
				failedValue = systemOpen.level;
				return false;
			}
			if (systemOpen.taskId > 0 && !MainTaskManager.Instance.IsFinishedTask(systemOpen.taskId))
			{
				failedType = SystemOpenFailedType.Task;
				failedValue = systemOpen.taskId;
				return false;
			}
			if (systemOpen.levelClose > 0 && EntityWorld.Instance.EntSelf.Lv > systemOpen.levelClose)
			{
				failedType = SystemOpenFailedType.LevelClose;
				failedValue = systemOpen.levelClose;
				return false;
			}
		}
		failedType = SystemOpenFailedType.None;
		failedValue = 0;
		return true;
	}

	public static int GetSystemOpenLv(int systemId)
	{
		SystemOpen systemOpen = DataReader<SystemOpen>.Get(systemId);
		if (systemOpen != null)
		{
			return systemOpen.level;
		}
		return 0;
	}

	public static bool GetTargetPosition(int areaId, int widgetId, ref Vector3 position, ref Vector2 offset)
	{
		offset = Vector2.get_zero();
		TownUI instance = TownUI.Instance;
		if (instance == null)
		{
			Debug.LogError("TownUI is null");
			return false;
		}
		Transform transform = null;
		switch (areaId)
		{
		case 1:
			transform = instance.FindTransform("HeadIcon");
			break;
		case 2:
		{
			transform = instance.FindTransform("ButtonsLeftBase");
			int indexOfLeftMiddle = SystemOpenManager.GetIndexOfLeftMiddle(areaId);
			offset = new Vector2(0f, (float)(-(float)indexOfLeftMiddle * 110));
			break;
		}
		case 3:
			transform = WidgetSystem.FindWidgetOnUI(widgetId, true);
			if (transform != null)
			{
				transform.get_gameObject().SetActive(true);
			}
			instance.SwitchTopRightButtonToShow();
			break;
		case 5:
			transform = WidgetSystem.FindWidgetOnUI(widgetId, true);
			if (transform != null)
			{
				transform.get_gameObject().SetActive(true);
			}
			instance.ForceOpenRightBottom();
			break;
		case 6:
			transform = instance.FindTransform("BigFight");
			break;
		case 7:
			instance.CloseRightMoreButton();
			transform = instance.FindTransform("DailyTask");
			break;
		case 8:
			transform = instance.FindTransform("GodWeaponButton");
			break;
		}
		if (transform == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"指导目标为空!!!区域:",
				areaId,
				", 组件:",
				widgetId
			}));
			return false;
		}
		position = transform.get_position();
		Debug.Log(string.Concat(new object[]
		{
			"指导目标:",
			transform.get_gameObject().get_name(),
			", 坐标:",
			position
		}));
		return true;
	}

	public static List<Transform> GetBehinds(int area, int areaIndex, out List<Vector2> offsets)
	{
		offsets = null;
		return null;
	}

	private static int MailSortCompare(MailInformation AF1, MailInformation AF2)
	{
		int result = 0;
		if (AF1.mailInfo.status == 1 && AF2.mailInfo.status == 0)
		{
			result = 1;
		}
		else if (AF1.mailInfo.status == 0 && AF2.mailInfo.status == 1)
		{
			result = -1;
		}
		else if ((AF1.mailInfo.content.items.get_Count() == 0 || AF1.mailInfo.drawMark == 0) && AF2.mailInfo.content.items.get_Count() > 0 && AF2.mailInfo.drawMark != 0)
		{
			result = 1;
		}
		else if (AF1.mailInfo.content.items.get_Count() > 0 && AF1.mailInfo.drawMark != 0 && (AF2.mailInfo.content.items.get_Count() == 0 || AF2.mailInfo.drawMark == 0))
		{
			result = -1;
		}
		else if (AF1.mailInfo.buildDate > AF2.mailInfo.buildDate)
		{
			result = -1;
		}
		else if (AF1.mailInfo.buildDate < AF2.mailInfo.buildDate)
		{
			result = 1;
		}
		else if (AF1.mailInfo.id > AF2.mailInfo.id)
		{
			result = -1;
		}
		else if (AF1.mailInfo.id < AF2.mailInfo.id)
		{
			result = 1;
		}
		return result;
	}

	public static int GetIndexOfLeftMiddle(int areaIndex)
	{
		int num = 1;
		for (int i = 0; i < SystemOpenManager.ButtonsOfLeftMiddle.get_Count(); i++)
		{
			SystemOpen systemOpen = SystemOpenManager.ButtonsOfLeftMiddle.get_Item(i);
			if (systemOpen.areaIndex < areaIndex && WidgetSystem.IsWidgetActiveSelf(systemOpen.widgetId))
			{
				num++;
			}
		}
		return num;
	}

	private static Transform GetWidget(int widgetId)
	{
		return WidgetSystem.FindWidgetOnUI(widgetId, true);
	}

	public static int GetIndexOfRightTop(int areaIndex)
	{
		int num = 1;
		for (int i = 0; i < SystemOpenManager.ButtonsOfRightTop.get_Count(); i++)
		{
			SystemOpen systemOpen = SystemOpenManager.ButtonsOfRightTop.get_Item(i);
			if (systemOpen.areaIndex < areaIndex && WidgetSystem.IsWidgetActiveSelf(systemOpen.widgetId))
			{
				num++;
			}
		}
		return num;
	}
}
