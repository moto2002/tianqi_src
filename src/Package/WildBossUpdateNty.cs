using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(608), ForSend(608), ProtoContract(Name = "WildBossUpdateNty")]
	[Serializable]
	public class WildBossUpdateNty : IExtensible
	{
		public static readonly short OP = 608;

		private int _idx;

		private int _updateType;

		private int _updateValue;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "idx", DataFormat = DataFormat.TwosComplement)]
		public int idx
		{
			get
			{
				return this._idx;
			}
			set
			{
				this._idx = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "updateType", DataFormat = DataFormat.TwosComplement)]
		public int updateType
		{
			get
			{
				return this._updateType;
			}
			set
			{
				this._updateType = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "updateValue", DataFormat = DataFormat.TwosComplement)]
		public int updateValue
		{
			get
			{
				return this._updateValue;
			}
			set
			{
				this._updateValue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
