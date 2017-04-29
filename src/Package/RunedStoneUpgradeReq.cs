using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2302), ForSend(2302), ProtoContract(Name = "RunedStoneUpgradeReq")]
	[Serializable]
	public class RunedStoneUpgradeReq : IExtensible
	{
		public static readonly short OP = 2302;

		private int _skillId;

		private int _runedStoneCfgId;

		private int _protectedStoneCfgId;

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

		[ProtoMember(3, IsRequired = false, Name = "protectedStoneCfgId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int protectedStoneCfgId
		{
			get
			{
				return this._protectedStoneCfgId;
			}
			set
			{
				this._protectedStoneCfgId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
