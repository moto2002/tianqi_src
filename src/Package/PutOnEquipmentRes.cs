using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(769), ForSend(769), ProtoContract(Name = "PutOnEquipmentRes")]
	[Serializable]
	public class PutOnEquipmentRes : IExtensible
	{
		public static readonly short OP = 769;

		private int _position;

		private long _equipId;

		private int _blessValue;

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

		[ProtoMember(2, IsRequired = true, Name = "equipId", DataFormat = DataFormat.TwosComplement)]
		public long equipId
		{
			get
			{
				return this._equipId;
			}
			set
			{
				this._equipId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "blessValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int blessValue
		{
			get
			{
				return this._blessValue;
			}
			set
			{
				this._blessValue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
