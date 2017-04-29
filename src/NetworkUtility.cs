using Package;
using System;
using System.Collections.Generic;

public class NetworkUtility
{
	public const uint NetworkTimeoutTime = 3000u;

	public const int MaxPacketsOnce = 2147483647;

	public const int InstantlyAckWaitTime = 5;

	public const int PingWaitInterval = 5000;

	protected static Dictionary<short, Type> RecvCode;

	protected static Dictionary<Type, short> SendPackets;

	protected static Dictionary<Type, short> RecvPackets;

	public static XDict<Type, short> NeedConfirmAckNumber = new XDict<Type, short>();

	protected static Type wrapType = typeof(InstantlyAckReq);

	public static Type WrapType
	{
		get
		{
			return NetworkUtility.wrapType;
		}
	}

	public static short InstantlyAckReqOpCode
	{
		get
		{
			return InstantlyAckReq.OP;
		}
	}

	public static short AckNtyOpCode
	{
		get
		{
			return AckNty.OP;
		}
	}

	public static short PingReqOpCode
	{
		get
		{
			return PingReq.OP;
		}
	}

	public static short PingResOpCode
	{
		get
		{
			return PingRes.OP;
		}
	}

	public static void Init(Dictionary<short, Type> unCoder = null, Dictionary<short, Type> enCoder = null, Dictionary<Type, short> send = null, Dictionary<Type, short> recv = null)
	{
		NetworkUtility.InitPackageInfo(unCoder, enCoder, send, recv);
		NetworkUtility.InitNeedConfirmAckNumber();
	}

	protected static void InitPackageInfo(Dictionary<short, Type> unCoder = null, Dictionary<short, Type> enCoder = null, Dictionary<Type, short> send = null, Dictionary<Type, short> recv = null)
	{
		if (NetworkUtility.RecvCode == null)
		{
			NetworkUtility.RecvCode = enCoder;
		}
		if (NetworkUtility.SendPackets == null)
		{
			NetworkUtility.SendPackets = send;
		}
		if (NetworkUtility.RecvPackets == null)
		{
			NetworkUtility.RecvPackets = recv;
		}
	}

	protected static void InitNeedConfirmAckNumber()
	{
		if (NetworkUtility.NeedConfirmAckNumber.Count == 0)
		{
			NetworkUtility.NeedConfirmAckNumber.Add(NetworkUtility.WrapType, NetworkUtility.InstantlyAckReqOpCode);
			NetworkUtility.NeedConfirmAckNumber.Add(typeof(ClientDrvBattleEffectDmgReportReq), ClientDrvBattleEffectDmgReportReq.OP);
			NetworkUtility.NeedConfirmAckNumber.Add(typeof(ClientDrvBattleBuffDmgReportReq), ClientDrvBattleBuffDmgReportReq.OP);
		}
	}

	public static Type GetRecvType(short code)
	{
		if (NetworkUtility.RecvCode.ContainsKey(code))
		{
			return NetworkUtility.RecvCode.get_Item(code);
		}
		return null;
	}

	public static short GetSendPacketsType(Type dataType)
	{
		if (NetworkUtility.SendPackets.ContainsKey(dataType))
		{
			return NetworkUtility.SendPackets.get_Item(dataType);
		}
		return 0;
	}

	public static short GetRecvPacketsType(Type dataType)
	{
		if (NetworkUtility.RecvPackets.ContainsKey(dataType))
		{
			return NetworkUtility.RecvPackets.get_Item(dataType);
		}
		return 0;
	}

	public static InstantlyAckReq GetNewWrapData()
	{
		return new InstantlyAckReq();
	}
}
