using System;
using System.Collections.Generic;
using UnityEngine;

public class UIStackManager : BaseSubSystemManager
{
	private List<UIStack> m_listUIStacks = new List<UIStack>();

	private static UIStackManager instance;

	private List<UIStack> m_listOpen = new List<UIStack>();

	private List<UIStack> m_lisRemove = new List<UIStack>();

	public static UIStackManager Instance
	{
		get
		{
			if (UIStackManager.instance == null)
			{
				UIStackManager.instance = new UIStackManager();
			}
			return UIStackManager.instance;
		}
	}

	private void AddToStack(UIStack uistack)
	{
		this.m_listUIStacks.Add(uistack);
	}

	private void RemoveFromStack(UIStack uistack)
	{
		this.m_listUIStacks.Remove(uistack);
	}

	private void RemoveFromStack(int index)
	{
		if (index < this.m_listUIStacks.get_Count())
		{
			this.m_listUIStacks.RemoveAt(index);
		}
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.m_listUIStacks.Clear();
	}

	protected override void AddListener()
	{
	}

	public void PushUI(UIBase uiBase)
	{
		if (uiBase == null)
		{
			return;
		}
		this.PushUI(uiBase.prefabName, uiBase.uiType, uiBase.get_transform().get_parent());
	}

	public void PushUI(string prefabName, UIType uiType, Transform parentNode)
	{
		if (UIBase.IsNeedPush(uiType))
		{
			if (!this.ReorganizePush(prefabName))
			{
				this.AddToStack(new UIStack
				{
					Node = parentNode,
					prefabName = prefabName,
					type = uiType
				});
			}
			if (this.m_listUIStacks.get_Count() > 0 && uiType == UIType.FullScreen)
			{
				List<UIStack> list = new List<UIStack>();
				for (int i = this.m_listUIStacks.get_Count() - 2; i >= 0; i--)
				{
					if (this.m_listUIStacks.get_Item(i).type != UIType.Pop)
					{
						list.Add(this.m_listUIStacks.get_Item(i));
						break;
					}
					list.Add(this.m_listUIStacks.get_Item(i));
				}
				for (int j = 0; j < list.get_Count(); j++)
				{
					UIManagerControl.Instance.HideUI(list.get_Item(j).prefabName);
				}
			}
		}
	}

	public void PopUI(string kPrefabName, UIType utype = UIType.FullScreen)
	{
		if (!this.ReorganizePush(kPrefabName))
		{
			this.AddToStack(new UIStack
			{
				Node = UINodesManager.NormalUIRoot,
				prefabName = kPrefabName,
				type = utype
			});
		}
		UIStack uIStack = this.m_listUIStacks.Find((UIStack e) => e.prefabName == kPrefabName);
		if (uIStack != null)
		{
			if (uIStack.type == UIType.Pop)
			{
				this.PopUILast_FullScreen();
			}
			else if (!UIManagerControl.Instance.IsOpen(uIStack.prefabName))
			{
				UIManagerControl.Instance.OpenUI(uIStack.prefabName, uIStack.Node, false, uIStack.type);
			}
		}
	}

	public void PopUIPrevious(UIType uitp)
	{
		if (uitp == UIType.FullScreen)
		{
			if (this.GetStackCount() > 1)
			{
				this.PopRemove_FullScreen(1);
			}
		}
		else if (uitp == UIType.Pop && this.GetStackCount() > 0)
		{
			this.PopRemoveOne_Pop();
		}
		this.PopUILast_FullScreen();
	}

	public void PopUIRemovePop()
	{
		if (this.GetStackCount() > 0)
		{
			this.PopRemove_Pop();
		}
	}

	public void PopTownUI()
	{
		this.PopUI("TownUI", UIType.FullScreen);
		if (!UIManagerControl.Instance.IsOpen("TownUI"))
		{
			UIManagerControl.Instance.OpenUI("TownUI", null, false, UIType.FullScreen);
		}
	}

	public void PopUILast_FullScreen()
	{
		this.m_listOpen.Clear();
		int i = this.m_listUIStacks.get_Count() - 1;
		while (i >= 0)
		{
			if (this.m_listUIStacks.get_Item(i).type == UIType.Pop)
			{
				this.m_listOpen.Add(this.m_listUIStacks.get_Item(i));
				i--;
			}
			else
			{
				if (this.m_listUIStacks.get_Item(i).type == UIType.FullScreen)
				{
					this.m_listOpen.Add(this.m_listUIStacks.get_Item(i));
					break;
				}
				break;
			}
		}
		this.OpenUI();
	}

	private void OpenUI()
	{
		if (this.m_listOpen.get_Count() > 0)
		{
			for (int i = this.m_listOpen.get_Count() - 1; i >= 0; i--)
			{
				if (!UIManagerControl.Instance.IsOpen(this.m_listOpen.get_Item(i).prefabName))
				{
					UIManagerControl.Instance.OpenUI(this.m_listOpen.get_Item(i).prefabName, this.m_listOpen.get_Item(i).Node, false, this.m_listOpen.get_Item(i).type);
				}
			}
		}
		else if (EntityWorld.Instance.EntSelf != null && !EntityWorld.Instance.EntSelf.IsInBattle)
		{
			UIManagerControl.Instance.OpenUI("TownUI", null, false, UIType.FullScreen);
			if (CamerasMgr.CameraMain != null && !CamerasMgr.CameraMain.get_enabled())
			{
				CamerasMgr.CameraMain.set_enabled(true);
			}
		}
	}

	public void ClearPush()
	{
		this.m_listUIStacks.Clear();
	}

	private void PopRemove_FullScreen(int numToRemove)
	{
		this.m_lisRemove.Clear();
		for (int i = this.m_listUIStacks.get_Count() - 1; i >= 0; i--)
		{
			if (numToRemove <= 0)
			{
				break;
			}
			if (this.m_listUIStacks.get_Item(i).type == UIType.Pop)
			{
				this.m_lisRemove.Add(this.m_listUIStacks.get_Item(i));
			}
			else
			{
				numToRemove--;
				this.m_lisRemove.Add(this.m_listUIStacks.get_Item(i));
			}
		}
		for (int j = 0; j < this.m_lisRemove.get_Count(); j++)
		{
			UIManagerControl.Instance.HideUI(this.m_lisRemove.get_Item(j).prefabName);
			this.RemoveFromStack(this.m_lisRemove.get_Item(j));
		}
	}

	private void PopRemove_Pop()
	{
		this.m_lisRemove.Clear();
		for (int i = this.m_listUIStacks.get_Count() - 1; i >= 0; i--)
		{
			if (this.m_listUIStacks.get_Item(i).type != UIType.Pop)
			{
				break;
			}
			this.m_lisRemove.Add(this.m_listUIStacks.get_Item(i));
		}
		for (int j = 0; j < this.m_lisRemove.get_Count(); j++)
		{
			UIManagerControl.Instance.UnLoadUIPrefab(this.m_lisRemove.get_Item(j).prefabName);
			this.RemoveFromStack(this.m_lisRemove.get_Item(j));
		}
	}

	private void PopRemoveOne_Pop()
	{
		if (this.m_listUIStacks.get_Count() > 0 && this.m_listUIStacks.get_Item(this.m_listUIStacks.get_Count() - 1).type == UIType.Pop)
		{
			this.RemoveFromStack(this.m_listUIStacks.get_Count() - 1);
		}
	}

	public void PopUIIfTarget(string prefab)
	{
		UIStack theLastUIStack = this.GetTheLastUIStack();
		if (theLastUIStack != null && theLastUIStack.prefabName.Equals(prefab))
		{
			this.PopUIRemovePop();
		}
	}

	private UIStack GetTheLastUIStack()
	{
		if (this.m_listUIStacks.get_Count() == 0)
		{
			return null;
		}
		return this.m_listUIStacks.get_Item(this.m_listUIStacks.get_Count() - 1);
	}

	public void RemoveStack(string prefab)
	{
		for (int i = 0; i < this.m_listUIStacks.get_Count(); i++)
		{
			if (this.m_listUIStacks.get_Item(i).prefabName == prefab)
			{
				this.RemoveFromStack(i);
			}
		}
	}

	private bool ReorganizePush(string kPrefabName)
	{
		int num = this.m_listUIStacks.FindIndex((UIStack e) => e.prefabName == kPrefabName);
		if (num < 0)
		{
			return false;
		}
		if (num == this.m_listUIStacks.get_Count() - 1)
		{
			return true;
		}
		num++;
		if (num < this.m_listUIStacks.get_Count())
		{
			this.m_listUIStacks.RemoveRange(num, this.m_listUIStacks.get_Count() - num);
		}
		return true;
	}

	private int GetStackCount()
	{
		int num = 0;
		for (int i = 0; i < this.m_listUIStacks.get_Count(); i++)
		{
			if (this.m_listUIStacks.get_Item(i).type == UIType.FullScreen)
			{
				num++;
			}
		}
		return num;
	}

	public void DebugStack()
	{
		for (int i = 0; i < this.m_listUIStacks.get_Count(); i++)
		{
			Debug.LogError("listUIStacks[i].prefabName  " + this.m_listUIStacks.get_Item(i).prefabName);
		}
	}

	public void PrintDebug()
	{
		for (int i = 0; i < this.m_listUIStacks.get_Count(); i++)
		{
			if (this.m_listUIStacks.get_Item(i) != null && this.m_listUIStacks.get_Item(i).Node != null)
			{
				Debug.LogError("=>listUIStacks, Node = " + this.m_listUIStacks.get_Item(i).Node.get_name());
			}
		}
	}

	public void CheckBattleUI()
	{
		for (int i = 0; i < this.m_listUIStacks.get_Count(); i++)
		{
			if (this.m_listUIStacks.get_Item(i).prefabName == "BattleUI")
			{
				Debug.LogError("==>重要调试日志, 战斗界面在栈中");
				this.DebugStack();
				return;
			}
		}
	}
}
