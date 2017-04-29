using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1024), ForSend(1024), ProtoContract(Name = "GuildWarRoleResourceNty")]
	[Serializable]
	public class GuildWarRoleResourceNty : IExtensible
	{
		public static readonly short OP = 1024;

		private int _totalResource;

		private int _fieldResource;

		private int _killCount;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "totalResource", DataFormat = DataFormat.TwosComplement)]
		public int totalResource
		{
			get
			{
				return this._totalResource;
			}
			set
			{
				this._totalResource = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "fieldResource", DataFormat = DataFormat.TwosComplement)]
		public int fieldResource
		{
			get
			{
				return this._fieldResource;
			}
			set
			{
				this._fieldResource = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "killCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int killCount
		{
			get
			{
				return this._killCount;
			}
			set
			{
				this._killCount = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
