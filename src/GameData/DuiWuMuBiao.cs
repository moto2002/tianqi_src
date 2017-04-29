using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DuiWuMuBiao")]
	[Serializable]
	public class DuiWuMuBiao : IExtensible
	{
		private int _Id;

		private int _Type;

		private int _Group;

		private int _label;

		private readonly List<int> _FuBen = new List<int>();

		private int _Word;

		private int _Button;

		private int _Set;

		private int _SystemId;

		private int _Lv;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "Type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Group", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Group
		{
			get
			{
				return this._Group;
			}
			set
			{
				this._Group = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "label", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int label
		{
			get
			{
				return this._label;
			}
			set
			{
				this._label = value;
			}
		}

		[ProtoMember(6, Name = "FuBen", DataFormat = DataFormat.TwosComplement)]
		public List<int> FuBen
		{
			get
			{
				return this._FuBen;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "Word", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Word
		{
			get
			{
				return this._Word;
			}
			set
			{
				this._Word = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "Button", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Button
		{
			get
			{
				return this._Button;
			}
			set
			{
				this._Button = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "Set", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Set
		{
			get
			{
				return this._Set;
			}
			set
			{
				this._Set = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "SystemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int SystemId
		{
			get
			{
				return this._SystemId;
			}
			set
			{
				this._SystemId = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "Lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
