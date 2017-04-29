using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Package
{
	[ForRecv(1400), ForSend(1400), ProtoContract(Name = "ClientDrvBattleEffectDmgReportReq")]
	[Serializable]
	public class ClientDrvBattleEffectDmgReportReq : IExtensible
	{
		public static readonly short OP = 1400;

		private long _fromId;

		private long _toId;

		private int _skillId;

		private int _effectId;

		private bool _calcCasterActPoint;

		private long _realId;

		private Pos _basePos;

		private Vector2 _baseVec;

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

		[ProtoMember(3, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "effectId", DataFormat = DataFormat.TwosComplement)]
		public int effectId
		{
			get
			{
				return this._effectId;
			}
			set
			{
				this._effectId = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "calcCasterActPoint", DataFormat = DataFormat.Default)]
		public bool calcCasterActPoint
		{
			get
			{
				return this._calcCasterActPoint;
			}
			set
			{
				this._calcCasterActPoint = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "realId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(7, IsRequired = false, Name = "basePos", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Pos basePos
		{
			get
			{
				return this._basePos;
			}
			set
			{
				this._basePos = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "baseVec", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Vector2 baseVec
		{
			get
			{
				return this._baseVec;
			}
			set
			{
				this._baseVec = value;
			}
		}

		[ProtoMember(9, Name = "fromBuffInfos", DataFormat = DataFormat.Default)]
		public List<ClientDrvBuffInfo> fromBuffInfos
		{
			get
			{
				return this._fromBuffInfos;
			}
		}

		[ProtoMember(10, Name = "toBuffInfos", DataFormat = DataFormat.Default)]
		public List<ClientDrvBuffInfo> toBuffInfos
		{
			get
			{
				return this._toBuffInfos;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "calcTargetVp", DataFormat = DataFormat.Default), DefaultValue(false)]
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

		[ProtoMember(12, IsRequired = false, Name = "vertifyInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
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
			stringBuilder.Append("\tskillId: ");
			stringBuilder.AppendLine(this.skillId.ToString());
			stringBuilder.Append("\teffectId: ");
			stringBuilder.AppendLine(this.effectId.ToString());
			stringBuilder.Append("\trealId: ");
			stringBuilder.AppendLine(this.realId.ToString());
			stringBuilder.Append("\tcalcCasterActPoint: ");
			stringBuilder.AppendLine(this.calcCasterActPoint.ToString());
			stringBuilder.Append("\tbasePos: ");
			stringBuilder.AppendLine(this.basePos.ToString());
			stringBuilder.Append("\tbaseVec: ");
			stringBuilder.AppendLine(this.baseVec.ToString());
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
