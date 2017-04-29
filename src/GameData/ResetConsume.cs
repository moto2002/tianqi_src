using ProtoBuf;
using System;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"resetTimes"
	}), ProtoContract(Name = "ResetConsume")]
	[Serializable]
	public class ResetConsume : IExtensible
	{
		private int _resetTimes;

		private int _needDiamond;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "resetTimes", DataFormat = DataFormat.TwosComplement)]
		public int resetTimes
		{
			get
			{
				return this._resetTimes;
			}
			set
			{
				this._resetTimes = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "needDiamond", DataFormat = DataFormat.TwosComplement)]
		public int needDiamond
		{
			get
			{
				return this._needDiamond;
			}
			set
			{
				this._needDiamond = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
