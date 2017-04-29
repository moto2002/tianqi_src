using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "PVPMeiRiJiangLi")]
	[Serializable]
	public class PVPMeiRiJiangLi : IExtensible
	{
		private int _id;

		private int _type;

		private readonly List<int> _completeTarget = new List<int>();

		private readonly List<int> _reward = new List<int>();

		private int _name;

		private int _icon;

		private int _state;

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

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(4, Name = "completeTarget", DataFormat = DataFormat.TwosComplement)]
		public List<int> completeTarget
		{
			get
			{
				return this._completeTarget;
			}
		}

		[ProtoMember(5, Name = "reward", DataFormat = DataFormat.TwosComplement)]
		public List<int> reward
		{
			get
			{
				return this._reward;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "state", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int state
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
