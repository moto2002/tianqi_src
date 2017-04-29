using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuanJianZuHe")]
	[Serializable]
	public class GuanJianZuHe : IExtensible
	{
		private int _id;

		private readonly List<int> _key = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, Name = "key", DataFormat = DataFormat.TwosComplement)]
		public List<int> key
		{
			get
			{
				return this._key;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
