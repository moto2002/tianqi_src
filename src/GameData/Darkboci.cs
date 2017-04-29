using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"ID"
	}), ProtoContract(Name = "Darkboci")]
	[Serializable]
	public class Darkboci : IExtensible
	{
		private int _ID;

		private readonly List<int> _DropId = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		[ProtoMember(3, Name = "DropId", DataFormat = DataFormat.TwosComplement)]
		public List<int> DropId
		{
			get
			{
				return this._DropId;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
