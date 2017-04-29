using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(682), ForSend(682), ProtoContract(Name = "OffLineRes")]
	[Serializable]
	public class OffLineRes : IExtensible
	{
		public static readonly short OP = 682;

		private int _hasTime;

		private int _itemId;

		private int _GetTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "hasTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hasTime
		{
			get
			{
				return this._hasTime;
			}
			set
			{
				this._hasTime = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "GetTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int GetTime
		{
			get
			{
				return this._GetTime;
			}
			set
			{
				this._GetTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
