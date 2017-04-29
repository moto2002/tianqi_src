using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "HongBaoShiJie")]
	[Serializable]
	public class HongBaoShiJie : IExtensible
	{
		private int _Task;

		private int _Type;

		private readonly List<int> _priority = new List<int>();

		private int _Number;

		private int _Probability;

		private readonly List<int> _Reward = new List<int>();

		private int _Time;

		private int _Chinese;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Task", DataFormat = DataFormat.TwosComplement)]
		public int Task
		{
			get
			{
				return this._Task;
			}
			set
			{
				this._Task = value;
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

		[ProtoMember(4, Name = "priority", DataFormat = DataFormat.TwosComplement)]
		public List<int> priority
		{
			get
			{
				return this._priority;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "Number", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Number
		{
			get
			{
				return this._Number;
			}
			set
			{
				this._Number = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "Probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Probability
		{
			get
			{
				return this._Probability;
			}
			set
			{
				this._Probability = value;
			}
		}

		[ProtoMember(7, Name = "Reward", DataFormat = DataFormat.TwosComplement)]
		public List<int> Reward
		{
			get
			{
				return this._Reward;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "Time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Time
		{
			get
			{
				return this._Time;
			}
			set
			{
				this._Time = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "Chinese", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Chinese
		{
			get
			{
				return this._Chinese;
			}
			set
			{
				this._Chinese = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
