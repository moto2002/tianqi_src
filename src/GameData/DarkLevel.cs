using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"key"
	}), ProtoContract(Name = "DarkLevel")]
	[Serializable]
	public class DarkLevel : IExtensible
	{
		private int _key;

		private int _Id;

		private int _Lv;

		private int _Time;

		private readonly List<int> _DropId = new List<int>();

		private readonly List<int> _Reward = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "Id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Lv
		{
			get
			{
				return this._Lv;
			}
			set
			{
				this._Lv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "Time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Time
		{
			get
			{
				return this._Time;
			}
			set
			{
				this._Time = value;
			}
		}

		[ProtoMember(6, Name = "DropId", DataFormat = DataFormat.TwosComplement)]
		public List<int> DropId
		{
			get
			{
				return this._DropId;
			}
		}

		[ProtoMember(7, Name = "Reward", DataFormat = DataFormat.TwosComplement)]
		public List<int> Reward
		{
			get
			{
				return this._Reward;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
