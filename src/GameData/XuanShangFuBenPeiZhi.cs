using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XuanShangFuBenPeiZhi")]
	[Serializable]
	public class XuanShangFuBenPeiZhi : IExtensible
	{
		private int _id;

		private int _missionType;

		private int _fubenID;

		private int _probability;

		private int _lessLv;

		private int _drop;

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

		[ProtoMember(3, IsRequired = false, Name = "missionType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int missionType
		{
			get
			{
				return this._missionType;
			}
			set
			{
				this._missionType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "fubenID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fubenID
		{
			get
			{
				return this._fubenID;
			}
			set
			{
				this._fubenID = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				this._probability = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "lessLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lessLv
		{
			get
			{
				return this._lessLv;
			}
			set
			{
				this._lessLv = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "drop", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int drop
		{
			get
			{
				return this._drop;
			}
			set
			{
				this._drop = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
