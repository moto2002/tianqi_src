using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YeWaiBOSSXinXi")]
	[Serializable]
	public class YeWaiBOSSXinXi : IExtensible
	{
		private int _Id;

		private int _ModelId;

		private int _TeamBoss;

		private readonly List<int> _face = new List<int>();

		private readonly List<int> _DropRule = new List<int>();

		private int _DungeonId;

		private readonly List<int> _Lv = new List<int>();

		private int _RefreshNum;

		private readonly List<int> _Day = new List<int>();

		private int _Formula;

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

		[ProtoMember(3, IsRequired = false, Name = "ModelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ModelId
		{
			get
			{
				return this._ModelId;
			}
			set
			{
				this._ModelId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "TeamBoss", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int TeamBoss
		{
			get
			{
				return this._TeamBoss;
			}
			set
			{
				this._TeamBoss = value;
			}
		}

		[ProtoMember(5, Name = "face", DataFormat = DataFormat.TwosComplement)]
		public List<int> face
		{
			get
			{
				return this._face;
			}
		}

		[ProtoMember(6, Name = "DropRule", DataFormat = DataFormat.TwosComplement)]
		public List<int> DropRule
		{
			get
			{
				return this._DropRule;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "DungeonId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int DungeonId
		{
			get
			{
				return this._DungeonId;
			}
			set
			{
				this._DungeonId = value;
			}
		}

		[ProtoMember(8, Name = "Lv", DataFormat = DataFormat.TwosComplement)]
		public List<int> Lv
		{
			get
			{
				return this._Lv;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "RefreshNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int RefreshNum
		{
			get
			{
				return this._RefreshNum;
			}
			set
			{
				this._RefreshNum = value;
			}
		}

		[ProtoMember(10, Name = "Day", DataFormat = DataFormat.TwosComplement)]
		public List<int> Day
		{
			get
			{
				return this._Day;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "Formula", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Formula
		{
			get
			{
				return this._Formula;
			}
			set
			{
				this._Formula = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
