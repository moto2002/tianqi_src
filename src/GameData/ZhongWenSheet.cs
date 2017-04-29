using ProtoBuf;
using System;

namespace GameData
{
	[ProtoContract(Name = "ZhongWenSheet")]
	[Serializable]
	public class ZhongWenSheet : IExtensible
	{
		private int _id;

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
