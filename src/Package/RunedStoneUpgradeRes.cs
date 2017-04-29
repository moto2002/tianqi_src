using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2303), ForSend(2303), ProtoContract(Name = "RunedStoneUpgradeRes")]
	[Serializable]
	public class RunedStoneUpgradeRes : IExtensible
	{
		public static readonly short OP = 2303;

		private int _skillId;

		private int _runedStoneCfgId;

		private int _runedStoneLv;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "runedStoneCfgId", DataFormat = DataFormat.TwosComplement)]
		public int runedStoneCfgId
		{
			get
			{
				return this._runedStoneCfgId;
			}
			set
			{
				this._runedStoneCfgId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "runedStoneLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int runedStoneLv
		{
			get
			{
				return this._runedStoneLv;
			}
			set
			{
				this._runedStoneLv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
