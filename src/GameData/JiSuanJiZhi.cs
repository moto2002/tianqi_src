using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JiSuanJiZhi")]
	[Serializable]
	public class JiSuanJiZhi : IExtensible
	{
		private int _type;

		private readonly List<int> _parameter = new List<int>();

		private readonly List<int> _value = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, Name = "parameter", DataFormat = DataFormat.TwosComplement)]
		public List<int> parameter
		{
			get
			{
				return this._parameter;
			}
		}

		[ProtoMember(4, Name = "value", DataFormat = DataFormat.TwosComplement)]
		public List<int> value
		{
			get
			{
				return this._value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
