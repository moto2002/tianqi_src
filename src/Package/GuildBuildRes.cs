using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3682), ForSend(3682), ProtoContract(Name = "GuildBuildRes")]
	[Serializable]
	public class GuildBuildRes : IExtensible
	{
		public static readonly short OP = 3682;

		private GuildBuildType.GBT _buildType;

		private int _contribution;

		private int _fund;

		private int _builtedCount;

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

		[ProtoMember(2, IsRequired = false, Name = "contribution", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int contribution
		{
			get
			{
				return this._contribution;
			}
			set
			{
				this._contribution = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "fund", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fund
		{
			get
			{
				return this._fund;
			}
			set
			{
				this._fund = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "builtedCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int builtedCount
		{
			get
			{
				return this._builtedCount;
			}
			set
			{
				this._builtedCount = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
