using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1117), ForSend(1117), ProtoContract(Name = "AutoIntensifyPositionRes")]
	[Serializable]
	public class AutoIntensifyPositionRes : IExtensible
	{
		public static readonly short OP = 1117;

		private IntensifyInfo _intensifyInfo;

		private int _itemId = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "intensifyInfo", DataFormat = DataFormat.Default)]
		public IntensifyInfo intensifyInfo
		{
			get
			{
				return this._intensifyInfo;
			}
			set
			{
				this._intensifyInfo = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
