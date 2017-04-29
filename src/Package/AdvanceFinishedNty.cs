using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2618), ForSend(2618), ProtoContract(Name = "AdvanceFinishedNty")]
	[Serializable]
	public class AdvanceFinishedNty : IExtensible
	{
		public static readonly short OP = 2618;

		private int _curAdvancedCfgId;

		private int _nextAdvancedCfgId = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "curAdvancedCfgId", DataFormat = DataFormat.TwosComplement)]
		public int curAdvancedCfgId
		{
			get
			{
				return this._curAdvancedCfgId;
			}
			set
			{
				this._curAdvancedCfgId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "nextAdvancedCfgId", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int nextAdvancedCfgId
		{
			get
			{
				return this._nextAdvancedCfgId;
			}
			set
			{
				this._nextAdvancedCfgId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
