using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChengChangJiHua")]
	[Serializable]
	public class ChengChangJiHua : IExtensible
	{
		private int _lv;

		private int _ItemId;

		private long _ItemNum;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "ItemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ItemId
		{
			get
			{
				return this._ItemId;
			}
			set
			{
				this._ItemId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "ItemNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long ItemNum
		{
			get
			{
				return this._ItemNum;
			}
			set
			{
				this._ItemNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
