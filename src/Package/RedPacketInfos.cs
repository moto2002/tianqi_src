using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "RedPacketInfos")]
	[Serializable]
	public class RedPacketInfos : IExtensible
	{
		public static readonly short OP = 583;

		private int _id;

		private int _times;

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

		[ProtoMember(2, IsRequired = false, Name = "times", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int times
		{
			get
			{
				return this._times;
			}
			set
			{
				this._times = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
