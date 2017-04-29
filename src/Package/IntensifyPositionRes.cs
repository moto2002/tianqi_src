using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(945), ForSend(945), ProtoContract(Name = "IntensifyPositionRes")]
	[Serializable]
	public class IntensifyPositionRes : IExtensible
	{
		public static readonly short OP = 945;

		private bool _isSuccess;

		private IntensifyInfo _intensifyInfo;

		private int _itemId = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "isSuccess", DataFormat = DataFormat.Default)]
		public bool isSuccess
		{
			get
			{
				return this._isSuccess;
			}
			set
			{
				this._isSuccess = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "intensifyInfo", DataFormat = DataFormat.Default)]
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

		[ProtoMember(3, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
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
