using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4782), ForSend(4782), ProtoContract(Name = "ForgingSuitRes")]
	[Serializable]
	public class ForgingSuitRes : IExtensible
	{
		public static readonly short OP = 4782;

		private long _equipId;

		private int _suitId;

		private int _libPosition;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "equipId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(2, IsRequired = false, Name = "suitId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int suitId
		{
			get
			{
				return this._suitId;
			}
			set
			{
				this._suitId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "libPosition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int libPosition
		{
			get
			{
				return this._libPosition;
			}
			set
			{
				this._libPosition = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
