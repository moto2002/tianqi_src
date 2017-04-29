using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Runes")]
	[Serializable]
	public class Runes : IExtensible
	{
		private int _id;

		private int _lv;

		private int _nextLv;

		private readonly List<int> _templateId = new List<int>();

		private readonly List<int> _materials = new List<int>();

		private int _successRate;

		private readonly List<int> _protect = new List<int>();

		private int _desc;

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

		[ProtoMember(3, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "nextLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nextLv
		{
			get
			{
				return this._nextLv;
			}
			set
			{
				this._nextLv = value;
			}
		}

		[ProtoMember(5, Name = "templateId", DataFormat = DataFormat.TwosComplement)]
		public List<int> templateId
		{
			get
			{
				return this._templateId;
			}
		}

		[ProtoMember(6, Name = "materials", DataFormat = DataFormat.TwosComplement)]
		public List<int> materials
		{
			get
			{
				return this._materials;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "successRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int successRate
		{
			get
			{
				return this._successRate;
			}
			set
			{
				this._successRate = value;
			}
		}

		[ProtoMember(8, Name = "protect", DataFormat = DataFormat.TwosComplement)]
		public List<int> protect
		{
			get
			{
				return this._protect;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "desc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc
		{
			get
			{
				return this._desc;
			}
			set
			{
				this._desc = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
