using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YeWaiBOSSJieJi")]
	[Serializable]
	public class YeWaiBOSSJieJi : IExtensible
	{
		private int _Lv;

		private int _SingleRank;

		private int _ManyRank;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Lv", DataFormat = DataFormat.TwosComplement)]
		public int Lv
		{
			get
			{
				return this._Lv;
			}
			set
			{
				this._Lv = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "SingleRank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int SingleRank
		{
			get
			{
				return this._SingleRank;
			}
			set
			{
				this._SingleRank = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "ManyRank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ManyRank
		{
			get
			{
				return this._ManyRank;
			}
			set
			{
				this._ManyRank = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
