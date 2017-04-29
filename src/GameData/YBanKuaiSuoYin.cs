using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YBanKuaiSuoYin")]
	[Serializable]
	public class YBanKuaiSuoYin : IExtensible
	{
		private string _ballId;

		private int _num;

		private readonly List<string> _around = new List<string>();

		private int _mine;

		private readonly List<int> _event = new List<int>();

		private readonly List<int> _probability = new List<int>();

		private int _mineProbability;

		private int _pentagon;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "ballId", DataFormat = DataFormat.Default)]
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

		[ProtoMember(3, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, Name = "around", DataFormat = DataFormat.Default)]
		public List<string> around
		{
			get
			{
				return this._around;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "mine", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, Name = "event", DataFormat = DataFormat.TwosComplement)]
		public List<int> @event
		{
			get
			{
				return this._event;
			}
		}

		[ProtoMember(8, Name = "probability", DataFormat = DataFormat.TwosComplement)]
		public List<int> probability
		{
			get
			{
				return this._probability;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "mineProbability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "pentagon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pentagon
		{
			get
			{
				return this._pentagon;
			}
			set
			{
				this._pentagon = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
