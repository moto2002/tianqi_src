using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(940), ForSend(940), ProtoContract(Name = "IntensifyPositionReq")]
	[Serializable]
	public class IntensifyPositionReq : IExtensible
	{
		public static readonly short OP = 940;

		private int _position;

		private int _intensifyStoneId;

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

		[ProtoMember(2, IsRequired = false, Name = "intensifyStoneId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int intensifyStoneId
		{
			get
			{
				return this._intensifyStoneId;
			}
			set
			{
				this._intensifyStoneId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
