using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ClientDrvBuffInfo")]
	[Serializable]
	public class ClientDrvBuffInfo : IExtensible
	{
		private int _buffId;

		private long _casterId;

		private int _skillId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "buffId", DataFormat = DataFormat.TwosComplement)]
		public int buffId
		{
			get
			{
				return this._buffId;
			}
			set
			{
				this._buffId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "casterId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "skillId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[buffID: ",
				this.buffId,
				" casterID: ",
				this.casterId,
				" skillID: ",
				this.skillId,
				"] "
			});
		}
	}
}
