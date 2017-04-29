using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class ServerGMManager
{
	private static ServerGMManager instance;

	public static ServerGMManager Instance
	{
		get
		{
			if (ServerGMManager.instance == null)
			{
				ServerGMManager.instance = new ServerGMManager();
			}
			return ServerGMManager.instance;
		}
	}

	protected ServerGMManager()
	{
	}

	public void Init()
	{
		NetworkManager.AddListenEvent<TestNty>(new NetCallBackMethod<TestNty>(this.OnTestNty));
		NetworkManager.AddListenEvent<TestClientRes>(new NetCallBackMethod<TestClientRes>(this.OnTestNetworkRes));
		NetworkManager.AddListenEvent<StringTipNty>(new NetCallBackMethod<StringTipNty>(this.OnStringTipNty));
	}

	public void Release()
	{
		NetworkManager.RemoveListenEvent<TestNty>(new NetCallBackMethod<TestNty>(this.OnTestNty));
		NetworkManager.RemoveListenEvent<TestClientRes>(new NetCallBackMethod<TestClientRes>(this.OnTestNetworkRes));
		NetworkManager.RemoveListenEvent<StringTipNty>(new NetCallBackMethod<StringTipNty>(this.OnStringTipNty));
	}

	protected void OnTestNty(short state, TestNty down = null)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnTestNty: ",
			state,
			" ",
			down.opType,
			" ",
			down.intParam,
			" ",
			down.strParam
		}));
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		TestType.TT opType = down.opType;
		if (opType != TestType.TT.ENTER_COPY)
		{
			if (opType == TestType.TT.ClI_DRV_COPY_ST)
			{
				Debug.Log(down.strParam);
			}
		}
		else
		{
			this.EnterServerGMInstance(down.intParam);
		}
	}

	protected void OnTestNetworkRes(short state, TestClientRes down = null)
	{
		Debug.LogError("OnTestNetworkRes: " + down.items.get_Count());
	}

	protected void OnStringTipNty(short state, StringTipNty down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		for (int i = 0; i < down.types.get_Count(); i++)
		{
			this.ShowServerStringTip(down.types.get_Item(i), down.strings);
		}
	}

	protected void EnterServerGMInstance(int instanceID)
	{
		InstanceManager.ChangeInstanceManager(instanceID, false);
	}

	protected void ShowServerStringTip(int type, List<string> args)
	{
		if (args == null)
		{
			return;
		}
		if (args.get_Count() < 1)
		{
			return;
		}
		switch (type)
		{
		case 0:
			UIManagerControl.Instance.ShowToastText(args.get_Item(0), 1f, 1f);
			break;
		case 1:
			BroadcastManager.Instance.AddQueue(args.get_Item(0));
			break;
		case 2:
			ChatManager.Instance.ServerStringReceive(args.get_Item(0));
			break;
		case 3:
			ServerTipDialogUIViewModel.Instance.ShowAsConfirm((args.get_Count() <= 1) ? string.Empty : args.get_Item(1), args.get_Item(0), null, "确 定", "button_orange_1");
			ServerTipDialogUIView.Instance.isClick = false;
			break;
		case 4:
			ServerTipDialogUIViewModel.Instance.ShowAsConfirm((args.get_Count() <= 1) ? string.Empty : args.get_Item(1), args.get_Item(0), new Action(ClientApp.Instance.ReInit), "确 定", "button_orange_1");
			ServerTipDialogUIView.Instance.isClick = false;
			break;
		case 5:
			ServerTipDialogUIViewModel.Instance.ShowAsOKCancel((args.get_Count() <= 1) ? string.Empty : args.get_Item(1), args.get_Item(0), null, new Action(ClientApp.Instance.ReInit), "取 消", "确 定", "button_orange_1", "button_yellow_1");
			ServerTipDialogUIView.Instance.isClick = false;
			break;
		}
	}
}
