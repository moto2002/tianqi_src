using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(823), ForSend(823), ProtoContract(Name = "ReplaceCurrTitleRes")]
	[Serializable]
	public class ReplaceCurrTitleRes : IExtensible
	{
		public static readonly short OP = 823;

		private int _currId;

		private int _sourceId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "currId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "sourceId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sourceId
		{
			get
			{
				return this._sourceId;
			}
			set
			{
				this._sourceId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
