using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"ID"
	}), ProtoContract(Name = "ResourceSplit")]
	[Serializable]
	public class ResourceSplit : IExtensible
	{
		private int _ID;

		private int _Packlist;

		private int _MinLv;

		private int _LvLimit;

		private float _SizeMbit;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "Packlist", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Packlist
		{
			get
			{
				return this._Packlist;
			}
			set
			{
				this._Packlist = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "MinLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int MinLv
		{
			get
			{
				return this._MinLv;
			}
			set
			{
				this._MinLv = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "LvLimit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int LvLimit
		{
			get
			{
				return this._LvLimit;
			}
			set
			{
				this._LvLimit = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "SizeMbit", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float SizeMbit
		{
			get
			{
				return this._SizeMbit;
			}
			set
			{
				this._SizeMbit = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
