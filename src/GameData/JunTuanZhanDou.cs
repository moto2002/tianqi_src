using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanDou")]
	[Serializable]
	public class JunTuanZhanDou : IExtensible
	{
		private int _CollectionId;

		private int _Occupy;

		private int _AddOccupy;

		private int _Multiple;

		private int _CoordinateTime;

		private int _Resource;

		private int _AddResource;

		private int _KillResource;

		private int _People;

		private int _Id;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "CollectionId", DataFormat = DataFormat.TwosComplement)]
		public int CollectionId
		{
			get
			{
				return this._CollectionId;
			}
			set
			{
				this._CollectionId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "Occupy", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Occupy
		{
			get
			{
				return this._Occupy;
			}
			set
			{
				this._Occupy = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "AddOccupy", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int AddOccupy
		{
			get
			{
				return this._AddOccupy;
			}
			set
			{
				this._AddOccupy = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "Multiple", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Multiple
		{
			get
			{
				return this._Multiple;
			}
			set
			{
				this._Multiple = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "CoordinateTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int CoordinateTime
		{
			get
			{
				return this._CoordinateTime;
			}
			set
			{
				this._CoordinateTime = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "Resource", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Resource
		{
			get
			{
				return this._Resource;
			}
			set
			{
				this._Resource = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "AddResource", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int AddResource
		{
			get
			{
				return this._AddResource;
			}
			set
			{
				this._AddResource = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "KillResource", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int KillResource
		{
			get
			{
				return this._KillResource;
			}
			set
			{
				this._KillResource = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "People", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int People
		{
			get
			{
				return this._People;
			}
			set
			{
				this._People = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "Id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
