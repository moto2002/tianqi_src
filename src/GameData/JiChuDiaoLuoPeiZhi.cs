using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JiChuDiaoLuoPeiZhi")]
	[Serializable]
	public class JiChuDiaoLuoPeiZhi : IExtensible
	{
		[ProtoContract(Name = "DropPair")]
		[Serializable]
		public class DropPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _LV;

		private readonly List<JiChuDiaoLuoPeiZhi.DropPair> _drop = new List<JiChuDiaoLuoPeiZhi.DropPair>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "LV", DataFormat = DataFormat.TwosComplement)]
		public int LV
		{
			get
			{
				return this._LV;
			}
			set
			{
				this._LV = value;
			}
		}

		[ProtoMember(3, Name = "drop", DataFormat = DataFormat.Default)]
		public List<JiChuDiaoLuoPeiZhi.DropPair> drop
		{
			get
			{
				return this._drop;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
