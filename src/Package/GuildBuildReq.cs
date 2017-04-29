using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(541), ForSend(541), ProtoContract(Name = "GuildBuildReq")]
	[Serializable]
	public class GuildBuildReq : IExtensible
	{
		public static readonly short OP = 541;

		private GuildBuildType.GBT _buildType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "buildType", DataFormat = DataFormat.TwosComplement)]
		public GuildBuildType.GBT buildType
		{
			get
			{
				return this._buildType;
			}
			set
			{
				this._buildType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
