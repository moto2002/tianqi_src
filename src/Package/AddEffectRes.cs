using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(459), ForSend(459), ProtoContract(Name = "AddEffectRes")]
	[Serializable]
	public class AddEffectRes : IExtensible
	{
		public static readonly short OP = 459;

		private long _casterId;

		private int _effectId;

		private long _uniqueId;

		private int _skillId;

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

		[ProtoMember(2, IsRequired = true, Name = "effectId", DataFormat = DataFormat.TwosComplement)]
		public int effectId
		{
			get
			{
				return this._effectId;
			}
			set
			{
				this._effectId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "uniqueId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
