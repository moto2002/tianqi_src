using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(892), ForSend(892), ProtoContract(Name = "GangFightPersonalInfo")]
	[Serializable]
	public class GangFightPersonalInfo : IExtensible
	{
		public static readonly short OP = 892;

		private int _combatWin;

		private int _totalWin;

		private int _topCombatWin;

		private int _historyCombatWin;

		private string _openTime = string.Empty;

		private string _closeTime = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "combatWin", DataFormat = DataFormat.TwosComplement)]
		public int combatWin
		{
			get
			{
				return this._combatWin;
			}
			set
			{
				this._combatWin = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "totalWin", DataFormat = DataFormat.TwosComplement)]
		public int totalWin
		{
			get
			{
				return this._totalWin;
			}
			set
			{
				this._totalWin = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "topCombatWin", DataFormat = DataFormat.TwosComplement)]
		public int topCombatWin
		{
			get
			{
				return this._topCombatWin;
			}
			set
			{
				this._topCombatWin = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "historyCombatWin", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int historyCombatWin
		{
			get
			{
				return this._historyCombatWin;
			}
			set
			{
				this._historyCombatWin = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "openTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string openTime
		{
			get
			{
				return this._openTime;
			}
			set
			{
				this._openTime = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "closeTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string closeTime
		{
			get
			{
				return this._closeTime;
			}
			set
			{
				this._closeTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
