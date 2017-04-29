using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JuQingZhiYinBuZou")]
	[Serializable]
	public class JuQingZhiYinBuZou : IExtensible
	{
		private int _id;

		private int _off;

		private int _priority;

		private int _triggerType;

		private readonly List<int> _counterpart = new List<int>();

		private readonly List<string> _args = new List<string>();

		private int _type;

		private readonly List<int> _typeArgs = new List<int>();

		private int _time;

		private int _probability;

		private int _amount;

		private int _delayTime;

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

		[ProtoMember(3, IsRequired = false, Name = "off", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int off
		{
			get
			{
				return this._off;
			}
			set
			{
				this._off = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "priority", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				this._priority = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "triggerType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int triggerType
		{
			get
			{
				return this._triggerType;
			}
			set
			{
				this._triggerType = value;
			}
		}

		[ProtoMember(6, Name = "counterpart", DataFormat = DataFormat.TwosComplement)]
		public List<int> counterpart
		{
			get
			{
				return this._counterpart;
			}
		}

		[ProtoMember(7, Name = "args", DataFormat = DataFormat.Default)]
		public List<string> args
		{
			get
			{
				return this._args;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, Name = "typeArgs", DataFormat = DataFormat.TwosComplement)]
		public List<int> typeArgs
		{
			get
			{
				return this._typeArgs;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				this._probability = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "amount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				this._amount = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "delayTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int delayTime
		{
			get
			{
				return this._delayTime;
			}
			set
			{
				this._delayTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
