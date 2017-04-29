using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Tips")]
	[Serializable]
	public class Tips : IExtensible
	{
		private int _lvId;

		private int _tipsId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "lvId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lvId
		{
			get
			{
				return this._lvId;
			}
			set
			{
				this._lvId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "tipsId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int tipsId
		{
			get
			{
				return this._tipsId;
			}
			set
			{
				this._tipsId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
