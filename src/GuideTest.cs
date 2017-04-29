using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuideTest : MonoBehaviour
{
	private string m_input = string.Empty;

	public string uiName = "PetChooseUI";

	public string widgetName = "Item_112";

	private void OnGUI()
	{
		int num = -1;
		num++;
		this.m_input = GUI.TextField(this.GetRect(num), this.m_input);
		num++;
		if (GUI.Button(this.GetRect(num), "Stop&Clear\n(停止指引并清除指引队列)"))
		{
			GuideManager.Instance.StopGuideOfGM();
		}
		num++;
		if (GUI.Button(this.GetRect(num), "trigger\n(触发指引)") && !string.IsNullOrEmpty(this.m_input))
		{
			GuideManager.Instance.TriggerPassOfGM(int.Parse(this.m_input));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "trigger now\n(立即触发指引)") && !string.IsNullOrEmpty(this.m_input))
		{
			GuideManager.Instance.TriggerPassOfGMNow(int.Parse(this.m_input));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "关闭GM"))
		{
			GuideManager.Instance.StopGM();
			Object.Destroy(base.get_gameObject().GetComponent<GuideTest>());
		}
		num++;
		if (GUI.Button(this.GetRect(num), "guidelog\n(日志)"))
		{
			GuideManager.Instance.PrintMessage();
		}
		num++;
		if (GUI.Button(this.GetRect(num), "检测配置表\nGuide"))
		{
			this.CheckDataOfGuide();
		}
		num++;
		if (GUI.Button(this.GetRect(num), "检测配置表\nStep"))
		{
			this.CheckDataOfStep();
		}
		num++;
		if (GUI.Button(this.GetRect(num), "level\n(等级事件广播)"))
		{
			EventDispatcher.Broadcast<int>("GuideManager.LevelUp", 1);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "find\n(查找控件,填在脚本Inspector)"))
		{
			this.FindWidget();
		}
		num++;
		if (GUI.Button(this.GetRect(num), "check ui open"))
		{
			GuideManager.Instance.CheckQueue(true, false);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "check ui close"))
		{
			GuideManager.Instance.CheckQueue(false, false);
		}
		num++;
		if (GUI.Button(this.GetRect(num), "任务触发指引\n只对指引作用") && !string.IsNullOrEmpty(this.m_input))
		{
			EventDispatcher.Broadcast<int>("GuideManager.TaskFinish", int.Parse(this.m_input));
		}
		num++;
		if (GUI.Button(this.GetRect(num), "主线任务跳转到\n指定任务") && !string.IsNullOrEmpty(this.m_input))
		{
			ChatManager.Instance.SendGMCommand(0, "#maintask " + int.Parse(this.m_input));
		}
	}

	public void FindWidget()
	{
		UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist(this.uiName);
		if (uIIfExist != null && uIIfExist.get_gameObject().get_activeSelf())
		{
			Transform transform = XUtility.RecursiveFindTransform(uIIfExist.get_transform(), this.widgetName);
			if (transform != null)
			{
				DepthOfGuide depthOfGuide = transform.get_gameObject().AddUniqueComponent<DepthOfGuide>();
				depthOfGuide.set_enabled(true);
				depthOfGuide.GuideOn();
			}
		}
	}

	private Rect GetRect(int index)
	{
		return new Rect((float)(200 + 160 * (index % 2)), (float)(150 + 50 * (index / 2 - 1)), 150f, 45f);
	}

	private void CheckDataOfGuide()
	{
		List<Guide> dataList = DataReader<Guide>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			Guide data_guide = dataList.get_Item(i);
			this.CheckNumberOfArgs(data_guide);
			this.CheckTaskId(data_guide);
			this.CheckFinishTaskId(data_guide);
			this.CheckGuideId(data_guide);
			this.CheckFinishGuideId(data_guide);
			this.CheckInstanceId(data_guide);
			this.CheckFinishInstanceId(data_guide);
		}
	}

	private void CheckDataOfStep()
	{
		List<GuideStep> dataList = DataReader<GuideStep>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			GuideStep data_step = dataList.get_Item(i);
			this.CheckLockMode(data_step);
			this.CheckType(data_step);
			this.CheckGuideId(data_step);
			this.CheckSuccessMode(data_step);
		}
	}

	private void CheckNumberOfArgs(Guide data_guide)
	{
		int num = -1;
		if (data_guide.triggerType == 1)
		{
			num = 1;
		}
		else if (data_guide.triggerType != 2)
		{
			if (data_guide.triggerType == 3)
			{
				num = 2;
			}
			else if (data_guide.triggerType == 4)
			{
				num = 1;
			}
			else if (data_guide.triggerType == 5)
			{
				num = 4;
			}
			else if (data_guide.triggerType == 6)
			{
				num = 3;
			}
			else if (data_guide.triggerType == 7)
			{
				num = 3;
			}
			else if (data_guide.triggerType != 8)
			{
				if (data_guide.triggerType == 9)
				{
					num = 2;
				}
				else if (data_guide.triggerType == 10)
				{
					num = 1;
				}
				else if (data_guide.triggerType == 11)
				{
					num = 1;
				}
				else if (data_guide.triggerType == 12)
				{
					num = 1;
				}
				else if (data_guide.triggerType == 13)
				{
					num = 2;
				}
				else if (data_guide.triggerType != 14)
				{
					if (data_guide.triggerType != 15)
					{
						if (data_guide.triggerType != 16)
						{
							if (data_guide.triggerType != 17)
							{
								if (data_guide.triggerType == 18)
								{
									num = 1;
								}
							}
						}
					}
				}
			}
		}
		if (num >= 0 && num != data_guide.args.get_Count())
		{
			Debug.LogWarning("args参数个数不合法, 指引id = " + data_guide.id);
		}
	}

	private void CheckTaskId(Guide data_guide)
	{
		if (data_guide.triggerType == 2)
		{
			for (int i = 0; i < data_guide.args.get_Count(); i++)
			{
				if (!this.IsTaskIdExist(data_guide.args.get_Item(i)))
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"args任务id不存在, 指引id = ",
						data_guide.id,
						", args不合法任务id = ",
						data_guide.args.get_Item(i)
					}));
				}
			}
		}
	}

	private void CheckFinishTaskId(Guide data_guide)
	{
		if (data_guide.finishTaskId > 0 && !this.IsTaskIdExist(data_guide.finishTaskId))
		{
			Debug.LogWarning("finishTaskId不存在, 指引id = " + data_guide.id);
		}
	}

	private bool IsTaskIdExist(int taskId)
	{
		List<RenWuPeiZhi> dataList = DataReader<RenWuPeiZhi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).id == taskId)
			{
				return true;
			}
		}
		return false;
	}

	private void CheckGuideId(Guide data_guide)
	{
		if (data_guide.triggerType == 14 || data_guide.triggerType == 15)
		{
			for (int i = 0; i < data_guide.args.get_Count(); i++)
			{
				if (!this.IsGuideIdExist(data_guide.args.get_Item(i)))
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"args指引id不存在, 指引id = ",
						data_guide.id,
						", args不合法指引id = ",
						data_guide.args.get_Item(i)
					}));
				}
			}
		}
	}

	private void CheckFinishGuideId(Guide data_guide)
	{
		if (data_guide.finishGuideId > 0 && !this.IsGuideIdExist(data_guide.finishGuideId))
		{
			Debug.LogWarning("finishGuideId不存在, 指引id = " + data_guide.id);
		}
	}

	private bool IsGuideIdExist(int guideId)
	{
		List<Guide> dataList = DataReader<Guide>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).id == guideId)
			{
				return true;
			}
		}
		return false;
	}

	private void CheckInstanceId(Guide data_guide)
	{
		if (data_guide.triggerType == 3 || data_guide.triggerType == 4 || data_guide.triggerType == 5 || data_guide.triggerType == 6 || data_guide.triggerType == 7 || data_guide.triggerType == 9 || data_guide.triggerType == 13)
		{
			if (data_guide.args.get_Count() > 0)
			{
				int num = data_guide.args.get_Item(0);
				if (!this.IsInstanceIdExist(num))
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"instance_id不存在, 指引id = ",
						data_guide.id,
						", instance_id = ",
						num
					}));
				}
			}
		}
		else if (data_guide.triggerType == 16 || data_guide.triggerType == 18)
		{
			for (int i = 0; i < data_guide.args.get_Count(); i++)
			{
				int num2 = data_guide.args.get_Item(i);
				if (!this.IsInstanceIdExist(num2))
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"instance_id不存在, 指引id = ",
						data_guide.id,
						", instance_id = ",
						num2
					}));
				}
			}
		}
	}

	private void CheckFinishInstanceId(Guide data_guide)
	{
		if (data_guide.finishInstanceId > 0 && !this.IsInstanceIdExist(data_guide.finishInstanceId))
		{
			Debug.LogWarning("finishInstanceId不存在, 指引id = " + data_guide.id);
		}
	}

	private bool IsInstanceIdExist(int instanceId)
	{
		List<FuBenJiChuPeiZhi> dataList = DataReader<FuBenJiChuPeiZhi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).id == instanceId)
			{
				return true;
			}
		}
		return false;
	}

	private void CheckLockMode(GuideStep data_step)
	{
		if (data_step.lockMode == 0)
		{
			Debug.LogWarning("step id = " + data_step.id + ", lockMode = 0, 需要填写");
		}
	}

	private void CheckType(GuideStep data_step)
	{
		if (data_step.type == 0)
		{
			Debug.LogWarning("step id = " + data_step.id + ", type = 0, 需要填写");
		}
	}

	private void CheckGuideId(GuideStep data_step)
	{
		if (!this.IsGuideIdExist(data_step.group))
		{
			Debug.LogWarning("groupId, 指引id = " + data_step.group);
		}
	}

	private void CheckSuccessMode(GuideStep data_step)
	{
		if (data_step.successMode.get_Count() == 0)
		{
			Debug.LogWarning("step id = " + data_step.id + ", successMode size = 0, 需要填写");
		}
	}
}
