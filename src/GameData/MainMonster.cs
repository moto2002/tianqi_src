using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "MainMonster")]
	[Serializable]
	public class MainMonster : IExtensible
	{
		private uint _id;

		private readonly List<int> _partId = new List<int>();

		private int _independent;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public uint id
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

		[ProtoMember(3, Name = "partId", DataFormat = DataFormat.TwosComplement)]
		public List<int> partId
		{
			get
			{
				return this._partId;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "independent", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int independent
		{
			get
			{
				return this._independent;
			}
			set
			{
				this._independent = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
