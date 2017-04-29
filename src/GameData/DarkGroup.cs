using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"MissionGroupId"
	}), ProtoContract(Name = "DarkGroup")]
	[Serializable]
	public class DarkGroup : IExtensible
	{
		private int _MissionGroupId;

		private int _Icon;

		private int _Name;

		private int _Lv;

		private readonly List<int> _FubenId = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "MissionGroupId", DataFormat = DataFormat.TwosComplement)]
		public int MissionGroupId
		{
			get
			{
				return this._MissionGroupId;
			}
			set
			{
				this._MissionGroupId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "Icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Icon
		{
			get
			{
				return this._Icon;
			}
			set
			{
				this._Icon = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "Lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, Name = "FubenId", DataFormat = DataFormat.TwosComplement)]
		public List<int> FubenId
		{
			get
			{
				return this._FubenId;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
