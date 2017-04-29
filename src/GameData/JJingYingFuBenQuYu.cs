using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JJingYingFuBenQuYu")]
	[Serializable]
	public class JJingYingFuBenQuYu : IExtensible
	{
		private int _map;

		private int _mapname;

		private int _openOrNot;

		private int _mission;

		private int _level;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "map", DataFormat = DataFormat.TwosComplement)]
		public int map
		{
			get
			{
				return this._map;
			}
			set
			{
				this._map = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "mapname", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mapname
		{
			get
			{
				return this._mapname;
			}
			set
			{
				this._mapname = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "openOrNot", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openOrNot
		{
			get
			{
				return this._openOrNot;
			}
			set
			{
				this._openOrNot = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "mission", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mission
		{
			get
			{
				return this._mission;
			}
			set
			{
				this._mission = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
