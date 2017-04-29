using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "DecomposeEquipInfo")]
	[Serializable]
	public class DecomposeEquipInfo : IExtensible
	{
		private int _position;

		private readonly List<long> _equipIds = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(2, Name = "equipIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> equipIds
		{
			get
			{
				return this._equipIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
