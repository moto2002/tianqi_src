using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(8839), ForSend(8839), ProtoContract(Name = "SvrRemoveNty")]
	[Serializable]
	public class SvrRemoveNty : IExtensible
	{
		public static readonly short OP = 8839;

		private int _titleId;

		private int _currId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "titleId", DataFormat = DataFormat.TwosComplement)]
		public int titleId
		{
			get
			{
				return this._titleId;
			}
			set
			{
				this._titleId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "currId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int currId
		{
			get
			{
				return this._currId;
			}
			set
			{
				this._currId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
