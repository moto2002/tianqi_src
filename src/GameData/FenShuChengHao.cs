using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "FenShuChengHao")]
	[Serializable]
	public class FenShuChengHao : IExtensible
	{
		private int _id;

		private int _fraction;

		private int _title;

		private readonly List<int> _reward = new List<int>();

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

		[ProtoMember(3, IsRequired = true, Name = "fraction", DataFormat = DataFormat.TwosComplement)]
		public int fraction
		{
			get
			{
				return this._fraction;
			}
			set
			{
				this._fraction = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "title", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		[ProtoMember(6, Name = "reward", DataFormat = DataFormat.TwosComplement)]
		public List<int> reward
		{
			get
			{
				return this._reward;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
