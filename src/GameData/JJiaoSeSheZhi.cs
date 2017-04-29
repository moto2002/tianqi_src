using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JJiaoSeSheZhi")]
	[Serializable]
	public class JJiaoSeSheZhi : IExtensible
	{
		private uint _id;

		private int _hitTips;

		private int _dieTips;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public uint id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "hitTips", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hitTips
		{
			get
			{
				return this._hitTips;
			}
			set
			{
				this._hitTips = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "dieTips", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dieTips
		{
			get
			{
				return this._dieTips;
			}
			set
			{
				this._dieTips = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
