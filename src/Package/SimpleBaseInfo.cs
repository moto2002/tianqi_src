using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "SimpleBaseInfo")]
	[Serializable]
	public class SimpleBaseInfo : IExtensible
	{
		private int _MoveSpeed;

		private int _AtkSpeed;

		private int _Lv;

		private long _Fighting;

		private int _VipLv;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "MoveSpeed", DataFormat = DataFormat.TwosComplement)]
		public int MoveSpeed
		{
			get
			{
				return this._MoveSpeed;
			}
			set
			{
				this._MoveSpeed = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "AtkSpeed", DataFormat = DataFormat.TwosComplement)]
		public int AtkSpeed
		{
			get
			{
				return this._AtkSpeed;
			}
			set
			{
				this._AtkSpeed = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "Lv", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = true, Name = "Fighting", DataFormat = DataFormat.TwosComplement)]
		public long Fighting
		{
			get
			{
				return this._Fighting;
			}
			set
			{
				this._Fighting = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "VipLv", DataFormat = DataFormat.TwosComplement)]
		public int VipLv
		{
			get
			{
				return this._VipLv;
			}
			set
			{
				this._VipLv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
