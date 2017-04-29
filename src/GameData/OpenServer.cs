using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "OpenServer")]
	[Serializable]
	public class OpenServer : IExtensible
	{
		private int _id;

		private int _time;

		private readonly List<int> _itemId = new List<int>();

		private readonly List<int> _num = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(4, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemId
		{
			get
			{
				return this._itemId;
			}
		}

		[ProtoMember(5, Name = "num", DataFormat = DataFormat.TwosComplement)]
		public List<int> num
		{
			get
			{
				return this._num;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
