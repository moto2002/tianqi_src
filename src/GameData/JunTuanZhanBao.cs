using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanBao")]
	[Serializable]
	public class JunTuanZhanBao : IExtensible
	{
		private int _Id;

		private readonly List<int> _Parameter = new List<int>();

		private int _Chinese;

		private int _Time;

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

		[ProtoMember(3, Name = "Parameter", DataFormat = DataFormat.TwosComplement)]
		public List<int> Parameter
		{
			get
			{
				return this._Parameter;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Chinese", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "Time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
