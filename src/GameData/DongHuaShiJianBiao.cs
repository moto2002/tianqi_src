using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DongHuaShiJianBiao")]
	[Serializable]
	public class DongHuaShiJianBiao : IExtensible
	{
		private int _instanceId;

		private readonly List<int> _beginEventId = new List<int>();

		private int _beginEventTime;

		private readonly List<int> _endEventId = new List<int>();

		private int _endEventTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "instanceId", DataFormat = DataFormat.TwosComplement)]
		public int instanceId
		{
			get
			{
				return this._instanceId;
			}
			set
			{
				this._instanceId = value;
			}
		}

		[ProtoMember(2, Name = "beginEventId", DataFormat = DataFormat.TwosComplement)]
		public List<int> beginEventId
		{
			get
			{
				return this._beginEventId;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "beginEventTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int beginEventTime
		{
			get
			{
				return this._beginEventTime;
			}
			set
			{
				this._beginEventTime = value;
			}
		}

		[ProtoMember(4, Name = "endEventId", DataFormat = DataFormat.TwosComplement)]
		public List<int> endEventId
		{
			get
			{
				return this._endEventId;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "endEventTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int endEventTime
		{
			get
			{
				return this._endEventTime;
			}
			set
			{
				this._endEventTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
