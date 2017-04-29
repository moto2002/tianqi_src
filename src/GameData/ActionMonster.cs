using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"action"
	}), ProtoContract(Name = "ActionMonster")]
	[Serializable]
	public class ActionMonster : IExtensible
	{
		private string _action;

		private int _slot;

		private int _priority;

		private int _status;

		private int _loop;

		private int _reverse;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "action", DataFormat = DataFormat.Default)]
		public string action
		{
			get
			{
				return this._action;
			}
			set
			{
				this._action = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "slot", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int slot
		{
			get
			{
				return this._slot;
			}
			set
			{
				this._slot = value;
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

		[ProtoMember(5, IsRequired = false, Name = "status", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "loop", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int loop
		{
			get
			{
				return this._loop;
			}
			set
			{
				this._loop = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "reverse", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reverse
		{
			get
			{
				return this._reverse;
			}
			set
			{
				this._reverse = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
