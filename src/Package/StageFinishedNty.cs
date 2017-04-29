using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3547), ForSend(3547), ProtoContract(Name = "StageFinishedNty")]
	[Serializable]
	public class StageFinishedNty : IExtensible
	{
		public static readonly short OP = 3547;

		private int _curStageCfgId;

		private int _nextStageCfgId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "curStageCfgId", DataFormat = DataFormat.TwosComplement)]
		public int curStageCfgId
		{
			get
			{
				return this._curStageCfgId;
			}
			set
			{
				this._curStageCfgId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "nextStageCfgId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nextStageCfgId
		{
			get
			{
				return this._nextStageCfgId;
			}
			set
			{
				this._nextStageCfgId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
