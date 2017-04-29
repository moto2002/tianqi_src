using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2843), ForSend(2843), ProtoContract(Name = "StageChangeNty")]
	[Serializable]
	public class StageChangeNty : IExtensible
	{
		public static readonly short OP = 2843;

		private int _stageCfgId;

		private int _finishedTaskId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "stageCfgId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int stageCfgId
		{
			get
			{
				return this._stageCfgId;
			}
			set
			{
				this._stageCfgId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "finishedTaskId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int finishedTaskId
		{
			get
			{
				return this._finishedTaskId;
			}
			set
			{
				this._finishedTaskId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
