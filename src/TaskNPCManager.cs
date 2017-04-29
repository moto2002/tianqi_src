using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskNPCManager : BaseSubSystemManager
{
	protected static TaskNPCManager instance;

	protected XDict<int, ActorNPC> taskNPCData = new XDict<int, ActorNPC>();

	public static TaskNPCManager Instance
	{
		get
		{
			if (TaskNPCManager.instance == null)
			{
				TaskNPCManager.instance = new TaskNPCManager();
			}
			return TaskNPCManager.instance;
		}
	}

	public XDict<int, ActorNPC> TaskNPCData
	{
		get
		{
			return this.taskNPCData;
		}
	}

	protected TaskNPCManager()
	{
	}

	public override void Release()
	{
		this.taskNPCData.Clear();
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener(EventNames.UpdateMainTask, new Callback(this.InitNPC));
		EventDispatcher.AddListener<int>(TaskNPCBehavior.OnNPCDieEnd, new Callback<int>(this.RemoveNPC));
	}

	public void InitNPC()
	{
		if (MySceneManager.Instance.CurSceneID == 0)
		{
			return;
		}
		if (MainTaskManager.Instance.MainTaskId == 0)
		{
			return;
		}
		int curSceneID = MySceneManager.Instance.CurSceneID;
		List<int> list = new List<int>();
		List<NPC> dataList = DataReader<NPC>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).scene == curSceneID && dataList.get_Item(i).position.get_Count() == 3)
			{
				list.Add(dataList.get_Item(i).id);
			}
		}
		if (list.get_Count() == 0)
		{
			return;
		}
		XDict<int, NPCInformation> xDict = MainTaskManager.Instance.SelectShowNPC(list);
		XDict<int, NPCInformation> xDict2 = new XDict<int, NPCInformation>();
		XDict<int, NPCInformation> xDict3 = new XDict<int, NPCInformation>();
		List<int> list2 = new List<int>();
		for (int j = 0; j < xDict.Count; j++)
		{
			if (this.taskNPCData.ContainsKey(xDict.ElementKeyAt(j)))
			{
				xDict3.Add(xDict.ElementKeyAt(j), xDict.ElementValueAt(j));
			}
			else
			{
				xDict2.Add(xDict.ElementKeyAt(j), xDict.ElementValueAt(j));
			}
		}
		for (int k = 0; k < this.taskNPCData.Count; k++)
		{
			if (!xDict.ContainsKey(this.taskNPCData.ElementKeyAt(k)))
			{
				list2.Add(this.taskNPCData.ElementKeyAt(k));
			}
		}
		for (int l = 0; l < xDict2.Count; l++)
		{
			this.CreateNPC(xDict2.ElementKeyAt(l), xDict2.ElementValueAt(l));
		}
		for (int m = 0; m < xDict3.Count; m++)
		{
			this.UpdateNPC(xDict3.ElementKeyAt(m), xDict3.ElementValueAt(m));
		}
		for (int n = 0; n < list2.get_Count(); n++)
		{
			this.RemoveNPC(list2.get_Item(n));
		}
	}

	public void CreateNPC(int npcDataID, NPCInformation info)
	{
		this.RemoveNPC(npcDataID);
		ActorNPC value = NPCManager.Instance.CreateNPC(npcDataID, DataReader<NPC>.Get(npcDataID).model, new TaskNPCBehavior(npcDataID, this.SetNPCCurrentState(npcDataID, info)));
		this.taskNPCData.Add(npcDataID, value);
	}

	public void RemoveNPC(int npcDataID)
	{
		if (!this.taskNPCData.ContainsKey(npcDataID))
		{
			return;
		}
		NPCManager.Instance.RemoveNPC(this.taskNPCData[npcDataID]);
		this.taskNPCData.Remove(npcDataID);
	}

	public void ClearNPC()
	{
		for (int i = 0; i < this.taskNPCData.Count; i++)
		{
			NPCManager.Instance.RemoveNPC(this.taskNPCData.ElementValueAt(i));
		}
		this.taskNPCData.Clear();
	}

	public void UpdateNPC(int npcDataID, NPCInformation info)
	{
		if (!this.taskNPCData.ContainsKey(npcDataID))
		{
			return;
		}
		this.taskNPCData[npcDataID].UpdateState(this.SetNPCCurrentState(npcDataID, info));
	}

	public void UpdateNPC(int npcDataID, TaskNPCBehavior.TaskNPCState taskNPCState)
	{
		if (!this.taskNPCData.ContainsKey(npcDataID))
		{
			return;
		}
		this.taskNPCData[npcDataID].UpdateState(taskNPCState);
	}

	public void UpdateAllShopNPC()
	{
		for (int i = 0; i < this.taskNPCData.Count; i++)
		{
			this.taskNPCData[this.taskNPCData.ElementKeyAt(i)].UpdateHeadInfoState();
		}
	}

	public void LookAtActorSelf(int npcDataID)
	{
		if (!this.taskNPCData.ContainsKey(npcDataID))
		{
			return;
		}
		int state = this.taskNPCData[npcDataID].GetState();
		if (state != 0 && state != 1)
		{
			return;
		}
		if (!DataReader<NPC>.Contains(npcDataID))
		{
			return;
		}
		if (DataReader<NPC>.Get(npcDataID).turn != 1)
		{
			return;
		}
		Vector3 direction = Quaternion.get_identity() * new Vector3(EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position().x - this.taskNPCData[npcDataID].get_transform().get_position().x, 0f, EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position().z - this.taskNPCData[npcDataID].get_transform().get_position().z);
		this.taskNPCData[npcDataID].UpdateState(this.SetNPCCurrentState(npcDataID, NPCStatus.STATIC, this.taskNPCData[npcDataID].get_transform().get_position(), direction));
	}

	public void ResetFaceDirection(int npcDataID)
	{
		if (!this.taskNPCData.ContainsKey(npcDataID))
		{
			return;
		}
		int state = this.taskNPCData[npcDataID].GetState();
		if (state != 0 && state != 1)
		{
			return;
		}
		if (!DataReader<NPC>.Contains(npcDataID))
		{
			return;
		}
		if (DataReader<NPC>.Get(npcDataID).turn != 1)
		{
			return;
		}
		this.taskNPCData[npcDataID].UpdateState(this.SetNPCCurrentState(npcDataID, NPCStatus.STATIC, this.GetDefaultPos(npcDataID), this.GetDefaultDir(npcDataID)));
	}

	protected TaskNPCBehavior.TaskNPCState SetNPCCurrentState(int npcDataID, NPCInformation info)
	{
		NPCStatus status = info.status;
		if (status == NPCStatus.FOLLOW)
		{
			return new TaskNPCBehavior.TaskNPCState
			{
				state = TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Follow,
				Position = Vector3.get_zero(),
				Direction = Vector3.get_forward()
			};
		}
		if (status != NPCStatus.NAV_TO_POINT)
		{
			return new TaskNPCBehavior.TaskNPCState
			{
				state = TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Stand,
				Position = new Vector3(info.position.x * 0.01f, info.position.y * 0.01f, info.position.z * 0.01f),
				Direction = this.GetDefaultDir(npcDataID)
			};
		}
		return new TaskNPCBehavior.TaskNPCState
		{
			state = TaskNPCBehavior.TaskNPCState.TaskNPCStateType.NavToPos,
			Position = new Vector3(info.position.x * 0.01f, info.position.y * 0.01f, info.position.z * 0.01f),
			Direction = this.GetDefaultDir(npcDataID)
		};
	}

	protected TaskNPCBehavior.TaskNPCState SetNPCCurrentState(int npcDataID, NPCStatus state, Vector3 position, Vector3 direction)
	{
		if (state == NPCStatus.FOLLOW)
		{
			return new TaskNPCBehavior.TaskNPCState
			{
				state = TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Follow,
				Position = Vector3.get_zero(),
				Direction = Vector3.get_forward()
			};
		}
		if (state != NPCStatus.NAV_TO_POINT)
		{
			return new TaskNPCBehavior.TaskNPCState
			{
				state = TaskNPCBehavior.TaskNPCState.TaskNPCStateType.Stand,
				Position = position,
				Direction = direction
			};
		}
		return new TaskNPCBehavior.TaskNPCState
		{
			state = TaskNPCBehavior.TaskNPCState.TaskNPCStateType.NavToPos,
			Position = position,
			Direction = direction
		};
	}

	protected Vector3 GetDefaultPos(int npcDataID)
	{
		NPC nPC = DataReader<NPC>.Get(npcDataID);
		if (nPC == null)
		{
			return Vector3.get_zero();
		}
		if (nPC.position.get_Count() < 3)
		{
			return Vector3.get_zero();
		}
		return new Vector3((float)nPC.position.get_Item(0) * 0.01f, (float)nPC.position.get_Item(1) * 0.01f, (float)nPC.position.get_Item(2) * 0.01f);
	}

	protected Vector3 GetDefaultDir(int npcDataID)
	{
		NPC nPC = DataReader<NPC>.Get(npcDataID);
		if (nPC == null)
		{
			return Vector3.get_zero();
		}
		if (nPC.face.get_Count() < 3)
		{
			return Vector3.get_zero();
		}
		return Quaternion.Euler(new Vector3((float)nPC.face.get_Item(0) * 0.01f, (float)nPC.face.get_Item(1) * 0.01f, (float)nPC.face.get_Item(2) * 0.01f)) * Vector3.get_forward();
	}
}
