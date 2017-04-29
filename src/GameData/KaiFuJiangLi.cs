using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "KaiFuJiangLi")]
	[Serializable]
	public class KaiFuJiangLi : IExtensible
	{
		private int _taskId;

		private int _Type;

		private int _objective;

		private int _ranking1;

		private int _ranking2;

		private int _parameter;

		private string _rewardItem = string.Empty;

		private int _chinese;

		private readonly List<int> _role = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "taskId", DataFormat = DataFormat.TwosComplement)]
		public int taskId
		{
			get
			{
				return this._taskId;
			}
			set
			{
				this._taskId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "Type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "objective", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int objective
		{
			get
			{
				return this._objective;
			}
			set
			{
				this._objective = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "ranking1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ranking1
		{
			get
			{
				return this._ranking1;
			}
			set
			{
				this._ranking1 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "ranking2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ranking2
		{
			get
			{
				return this._ranking2;
			}
			set
			{
				this._ranking2 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "parameter", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int parameter
		{
			get
			{
				return this._parameter;
			}
			set
			{
				this._parameter = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "rewardItem", DataFormat = DataFormat.Default), DefaultValue("")]
		public string rewardItem
		{
			get
			{
				return this._rewardItem;
			}
			set
			{
				this._rewardItem = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "chinese", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chinese
		{
			get
			{
				return this._chinese;
			}
			set
			{
				this._chinese = value;
			}
		}

		[ProtoMember(10, Name = "role", DataFormat = DataFormat.TwosComplement)]
		public List<int> role
		{
			get
			{
				return this._role;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
