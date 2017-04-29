using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "BanKuaiSuoYin")]
	[Serializable]
	public class BanKuaiSuoYin : IExtensible
	{
		private string _ballId = string.Empty;

		private int _num;

		private readonly List<string> _around = new List<string>();

		private int _mine;

		private readonly List<int> _event = new List<int>();

		private readonly List<int> _probability = new List<int>();

		private int _mineProbability;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "ballId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string ballId
		{
			get
			{
				return this._ballId;
			}
			set
			{
				this._ballId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(4, Name = "around", DataFormat = DataFormat.Default)]
		public List<string> around
		{
			get
			{
				return this._around;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "mine", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mine
		{
			get
			{
				return this._mine;
			}
			set
			{
				this._mine = value;
			}
		}

		[ProtoMember(6, Name = "event", DataFormat = DataFormat.TwosComplement)]
		public List<int> @event
		{
			get
			{
				return this._event;
			}
		}

		[ProtoMember(7, Name = "probability", DataFormat = DataFormat.TwosComplement)]
		public List<int> probability
		{
			get
			{
				return this._probability;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "mineProbability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mineProbability
		{
			get
			{
				return this._mineProbability;
			}
			set
			{
				this._mineProbability = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
