using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TuiJianZhuangBei")]
	[Serializable]
	public class TuiJianZhuangBei : IExtensible
	{
		private int _id;

		private readonly List<float> _recommended4 = new List<float>();

		private readonly List<float> _recommended7 = new List<float>();

		private readonly List<float> _recommended8 = new List<float>();

		private int _recommendedPower;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
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

		[ProtoMember(3, Name = "recommended4", DataFormat = DataFormat.FixedSize)]
		public List<float> recommended4
		{
			get
			{
				return this._recommended4;
			}
		}

		[ProtoMember(4, Name = "recommended7", DataFormat = DataFormat.FixedSize)]
		public List<float> recommended7
		{
			get
			{
				return this._recommended7;
			}
		}

		[ProtoMember(5, Name = "recommended8", DataFormat = DataFormat.FixedSize)]
		public List<float> recommended8
		{
			get
			{
				return this._recommended8;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "recommendedPower", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int recommendedPower
		{
			get
			{
				return this._recommendedPower;
			}
			set
			{
				this._recommendedPower = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
