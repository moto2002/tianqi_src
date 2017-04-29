using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "zJinJiePeiZhi")]
	[Serializable]
	public class zJinJiePeiZhi : IExtensible
	{
		private int _id;

		private int _targetId;

		private int _evoEXP;

		private int _evoLevel;

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

		[ProtoMember(3, IsRequired = false, Name = "targetId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int targetId
		{
			get
			{
				return this._targetId;
			}
			set
			{
				this._targetId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "evoEXP", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int evoEXP
		{
			get
			{
				return this._evoEXP;
			}
			set
			{
				this._evoEXP = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "evoLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int evoLevel
		{
			get
			{
				return this._evoLevel;
			}
			set
			{
				this._evoLevel = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
