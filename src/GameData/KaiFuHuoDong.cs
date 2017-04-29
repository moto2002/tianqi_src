using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "KaiFuHuoDong")]
	[Serializable]
	public class KaiFuHuoDong : IExtensible
	{
		private int _Type;

		private readonly List<int> _openDay = new List<int>();

		private readonly List<int> _taskList = new List<int>();

		private int _name;

		private int _chinese;

		private readonly List<int> _link = new List<int>();

		private int _picture;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Type", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, Name = "openDay", DataFormat = DataFormat.TwosComplement)]
		public List<int> openDay
		{
			get
			{
				return this._openDay;
			}
		}

		[ProtoMember(4, Name = "taskList", DataFormat = DataFormat.TwosComplement)]
		public List<int> taskList
		{
			get
			{
				return this._taskList;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "chinese", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chinese
		{
			get
			{
				return this._chinese;
			}
			set
			{
				this._chinese = value;
			}
		}

		[ProtoMember(7, Name = "link", DataFormat = DataFormat.TwosComplement)]
		public List<int> link
		{
			get
			{
				return this._link;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "picture", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int picture
		{
			get
			{
				return this._picture;
			}
			set
			{
				this._picture = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
