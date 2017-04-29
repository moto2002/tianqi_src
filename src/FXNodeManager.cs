using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FXNodeManager : MonoBehaviour
{
	public enum State
	{
		SelectedUI,
		SelectedNode
	}

	public enum EffectType
	{
		Particle
	}

	public FXNodeManager.EffectType FXType;

	public Transform FXParticle;

	public int UIFxId;

	[SetProperty("CurrentUI"), SerializeField]
	private string _CurrentUI;

	[SetProperty("CurrentNode"), SerializeField]
	private string _CurrentNode;

	[SetProperty("CurrentState"), SerializeField]
	private FXNodeManager.State _CurrentState;

	[HideInInspector]
	public List<string> UINames = new List<string>();

	[HideInInspector]
	public List<string> Nodes = new List<string>();

	private Dictionary<string, List<string>> mFXNodeTable = new Dictionary<string, List<string>>();

	public string CurrentUI
	{
		get
		{
			return this._CurrentUI;
		}
		set
		{
			this._CurrentUI = value;
			this.CurrentState = FXNodeManager.State.SelectedNode;
		}
	}

	public string CurrentNode
	{
		get
		{
			return this._CurrentNode;
		}
		set
		{
			this._CurrentNode = value;
		}
	}

	public FXNodeManager.State CurrentState
	{
		get
		{
			return this._CurrentState;
		}
		set
		{
			this._CurrentState = value;
			if (this._CurrentState == FXNodeManager.State.SelectedNode)
			{
				this.SetNodes();
			}
		}
	}

	public void RefreshNames()
	{
		this.Nodes.Clear();
		this.UINames.Clear();
		this.mFXNodeTable.Clear();
		List<FXNodeTable> dataList = DataReader<FXNodeTable>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			FXNodeTable fXNodeTable = dataList.get_Item(i);
			UINameTable uINameTable = DataReader<UINameTable>.Get(fXNodeTable.uiId);
			if (uINameTable != null)
			{
				if (this.mFXNodeTable.ContainsKey(uINameTable.name))
				{
					this.mFXNodeTable.get_Item(uINameTable.name).Add(fXNodeTable.nodeName);
				}
				else
				{
					this.mFXNodeTable.set_Item(uINameTable.name, new List<string>());
					this.mFXNodeTable.get_Item(uINameTable.name).Add(fXNodeTable.nodeName);
					this.UINames.Add(uINameTable.name);
				}
			}
		}
	}

	public void ResetPosition()
	{
		FXNodeManager.EffectType fXType = this.FXType;
		if (fXType == FXNodeManager.EffectType.Particle)
		{
			this.ResetPosition2Particle();
		}
	}

	private void ResetPosition2Particle()
	{
		if (this.FXParticle == null)
		{
			Debuger.Error("FX is null", new object[0]);
			return;
		}
		if (string.IsNullOrEmpty(this.CurrentUI))
		{
			Debuger.Error("CurrentUI is null", new object[0]);
			return;
		}
		FXNodeTool.ResetPosition(this.FXParticle, this.GetParentNode(), false);
	}

	private Transform GetParentNode()
	{
		Transform result = null;
		UIBase uIBase = UIManagerControl.Instance.OpenUI(this.CurrentUI, GameObject.Find("UICanvas").get_transform(), false, UIType.NonPush);
		if (uIBase != null)
		{
			BaseUIBehaviour component = base.get_transform().GetComponent<BaseUIBehaviour>();
			component.FillTransform2Editor(uIBase.get_transform());
			result = component.FindTransform(this.CurrentNode);
		}
		return result;
	}

	private void SetNodes()
	{
		if (!string.IsNullOrEmpty(this.CurrentUI) && this.mFXNodeTable.ContainsKey(this.CurrentUI))
		{
			this.Nodes = this.mFXNodeTable.get_Item(this.CurrentUI);
		}
	}
}
