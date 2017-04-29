using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "HanHuaBiao")]
	[Serializable]
	public class HanHuaBiao : IExtensible
	{
		[ProtoContract(Name = "DialoguenpcPair")]
		[Serializable]
		public class DialoguenpcPair : IExtensible
		{
			private int _key;

			private string _value = string.Empty;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
			public string value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		[ProtoContract(Name = "DialoguelasttimePair")]
		[Serializable]
		public class DialoguelasttimePair : IExtensible
		{
			private int _key;

			private string _value = string.Empty;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
			public string value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _dialogueId;

		private readonly List<int> _dialogueMoment = new List<int>();

		private readonly List<int> _refreshBatch = new List<int>();

		private readonly List<HanHuaBiao.DialoguenpcPair> _dialogueNpc = new List<HanHuaBiao.DialoguenpcPair>();

		private readonly List<HanHuaBiao.DialoguelasttimePair> _dialogueLastTime = new List<HanHuaBiao.DialoguelasttimePair>();

		private int _dialogueMonster;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "dialogueId", DataFormat = DataFormat.TwosComplement)]
		public int dialogueId
		{
			get
			{
				return this._dialogueId;
			}
			set
			{
				this._dialogueId = value;
			}
		}

		[ProtoMember(3, Name = "dialogueMoment", DataFormat = DataFormat.TwosComplement)]
		public List<int> dialogueMoment
		{
			get
			{
				return this._dialogueMoment;
			}
		}

		[ProtoMember(4, Name = "refreshBatch", DataFormat = DataFormat.TwosComplement)]
		public List<int> refreshBatch
		{
			get
			{
				return this._refreshBatch;
			}
		}

		[ProtoMember(5, Name = "dialogueNpc", DataFormat = DataFormat.Default)]
		public List<HanHuaBiao.DialoguenpcPair> dialogueNpc
		{
			get
			{
				return this._dialogueNpc;
			}
		}

		[ProtoMember(6, Name = "dialogueLastTime", DataFormat = DataFormat.Default)]
		public List<HanHuaBiao.DialoguelasttimePair> dialogueLastTime
		{
			get
			{
				return this._dialogueLastTime;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "dialogueMonster", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dialogueMonster
		{
			get
			{
				return this._dialogueMonster;
			}
			set
			{
				this._dialogueMonster = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
