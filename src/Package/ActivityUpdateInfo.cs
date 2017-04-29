using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(805), ForSend(805), ProtoContract(Name = "ActivityUpdateInfo")]
	[Serializable]
	public class ActivityUpdateInfo : IExtensible
	{
		public static readonly short OP = 805;

		private int _typeId;

		private int _startDay;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "startDay", DataFormat = DataFormat.TwosComplement)]
		public int startDay
		{
			get
			{
				return this._startDay;
			}
			set
			{
				this._startDay = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
