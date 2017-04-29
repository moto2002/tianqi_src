using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Package
{
	[ForRecv(1316), ForSend(1316), ProtoContract(Name = "ClientDrvBattleBuffDmgReportReq")]
	[Serializable]
	public class ClientDrvBattleBuffDmgReportReq : IExtensible
	{
		public static readonly short OP = 1316;

		private long _fromId;

		private long _toId;

		private int _buffId;

		private long _realId;

		private readonly List<ClientDrvBuffInfo> _fromBuffInfos = new List<ClientDrvBuffInfo>();

		private readonly List<ClientDrvBuffInfo> _toBuffInfos = new List<ClientDrvBuffInfo>();

		private bool _calcTargetVp;

		private BattleVertifyInfo _vertifyInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "fromId", DataFormat = DataFormat.TwosComplement)]
		public long fromId
		{
			get
			{
				return this._fromId;
			}
			set
			{
				this._fromId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "toId", DataFormat = DataFormat.TwosComplement)]
		public long toId
		{
			get
			{
				return this._toId;
			}
			set
			{
				this._toId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "buffId", DataFormat = DataFormat.TwosComplement)]
		public int buffId
		{
			get
			{
				return this._buffId;
			}
			set
			{
				this._buffId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "realId", DataFormat = DataFormat.TwosComplement)]
		public long realId
		{
			get
			{
				return this._realId;
			}
			set
			{
				this._realId = value;
			}
		}

		[ProtoMember(5, Name = "fromBuffInfos", DataFormat = DataFormat.Default)]
		public List<ClientDrvBuffInfo> fromBuffInfos
		{
			get
			{
				return this._fromBuffInfos;
			}
		}

		[ProtoMember(6, Name = "toBuffInfos", DataFormat = DataFormat.Default)]
		public List<ClientDrvBuffInfo> toBuffInfos
		{
			get
			{
				return this._toBuffInfos;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "calcTargetVp", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool calcTargetVp
		{
			get
			{
				return this._calcTargetVp;
			}
			set
			{
				this._calcTargetVp = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "vertifyInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public BattleVertifyInfo vertifyInfo
		{
			get
			{
				return this._vertifyInfo;
			}
			set
			{
				this._vertifyInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("ClientDrvBattleEffectDmgReportReq: ");
			stringBuilder.AppendLine("{");
			stringBuilder.Append("\tfromId: ");
			stringBuilder.AppendLine(this.fromId.ToString());
			stringBuilder.Append("\ttoId: ");
			stringBuilder.AppendLine(this.toId.ToString());
			stringBuilder.Append("\tbuffId: ");
			stringBuilder.AppendLine(this.buffId.ToString());
			stringBuilder.Append("\trealId: ");
			stringBuilder.AppendLine(this.realId.ToString());
			stringBuilder.AppendLine("\tfromBuffInfos: ");
			stringBuilder.AppendLine("\t{");
			for (int i = 0; i < this.fromBuffInfos.get_Count(); i++)
			{
				stringBuilder.Append("\t\t");
				stringBuilder.AppendLine(this.fromBuffInfos.get_Item(i).ToString());
			}
			stringBuilder.AppendLine("\t}");
			stringBuilder.AppendLine("\ttoBuffInfos: ");
			stringBuilder.AppendLine("\t{");
			for (int j = 0; j < this.toBuffInfos.get_Count(); j++)
			{
				stringBuilder.Append("\t\t");
				stringBuilder.AppendLine(this.toBuffInfos.get_Item(j).ToString());
			}
			stringBuilder.AppendLine("\t}");
			stringBuilder.Append("\tcalcTargetVp: ");
			stringBuilder.AppendLine(this.calcTargetVp.ToString());
			stringBuilder.Append("\tvertifyInfo: ");
			stringBuilder.AppendLine(this.vertifyInfo.ToString());
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}
	}
}
