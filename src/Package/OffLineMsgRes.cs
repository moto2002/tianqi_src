using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(521), ForSend(521), ProtoContract(Name = "OffLineMsgRes")]
	[Serializable]
	public class OffLineMsgRes : IExtensible
	{
		public static readonly short OP = 521;

		private int _hasTime;

		private long _needExp;

		private long _hourExp;

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

		[ProtoMember(2, IsRequired = false, Name = "needExp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long needExp
		{
			get
			{
				return this._needExp;
			}
			set
			{
				this._needExp = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "hourExp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long hourExp
		{
			get
			{
				return this._hourExp;
			}
			set
			{
				this._hourExp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
