using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"Fuben"
	}), ProtoContract(Name = "DarkProving")]
	[Serializable]
	public class DarkProving : IExtensible
	{
		private int _Fuben;

		private int _Chapter;

		private int _Mode;

		private readonly List<int> _ID = new List<int>();

		private int _TitleId;

		private int _MapId;

		private int _Lv;

		private int _Name;

		private int _Reward;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Fuben", DataFormat = DataFormat.TwosComplement)]
		public int Fuben
		{
			get
			{
				return this._Fuben;
			}
			set
			{
				this._Fuben = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "Chapter", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Chapter
		{
			get
			{
				return this._Chapter;
			}
			set
			{
				this._Chapter = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Mode", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Mode
		{
			get
			{
				return this._Mode;
			}
			set
			{
				this._Mode = value;
			}
		}

		[ProtoMember(5, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public List<int> ID
		{
			get
			{
				return this._ID;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "TitleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int TitleId
		{
			get
			{
				return this._TitleId;
			}
			set
			{
				this._TitleId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "MapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int MapId
		{
			get
			{
				return this._MapId;
			}
			set
			{
				this._MapId = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "Lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "Name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "Reward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Reward
		{
			get
			{
				return this._Reward;
			}
			set
			{
				this._Reward = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
