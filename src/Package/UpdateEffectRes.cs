using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(628), ForSend(628), ProtoContract(Name = "UpdateEffectRes")]
	[Serializable]
	public class UpdateEffectRes : IExtensible
	{
		public static readonly short OP = 628;

		private long _casterId;

		private long _uniqueId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "casterId", DataFormat = DataFormat.TwosComplement)]
		public long casterId
		{
			get
			{
				return this._casterId;
			}
			set
			{
				this._casterId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "uniqueId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long uniqueId
		{
			get
			{
				return this._uniqueId;
			}
			set
			{
				this._uniqueId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
