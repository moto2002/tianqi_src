using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JiChuPeiZhi")]
	[Serializable]
	public class JiChuPeiZhi : IExtensible
	{
		private int _id;

		private int _refreshTime;

		private int _description;

		private int _description2;

		private int _description3;

		private int _description4;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "refreshTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshTime
		{
			get
			{
				return this._refreshTime;
			}
			set
			{
				this._refreshTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "description", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "description2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int description2
		{
			get
			{
				return this._description2;
			}
			set
			{
				this._description2 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "description3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int description3
		{
			get
			{
				return this._description3;
			}
			set
			{
				this._description3 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "description4", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int description4
		{
			get
			{
				return this._description4;
			}
			set
			{
				this._description4 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
