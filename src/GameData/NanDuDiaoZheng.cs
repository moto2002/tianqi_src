using ProtoBuf;
using System;

namespace GameData
{
	[ProtoContract(Name = "NanDuDiaoZheng")]
	[Serializable]
	public class NanDuDiaoZheng : IExtensible
	{
		private int _min;

		private int _max;

		private int _adjustment;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "min", DataFormat = DataFormat.TwosComplement)]
		public int min
		{
			get
			{
				return this._min;
			}
			set
			{
				this._min = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "max", DataFormat = DataFormat.TwosComplement)]
		public int max
		{
			get
			{
				return this._max;
			}
			set
			{
				this._max = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "adjustment", DataFormat = DataFormat.TwosComplement)]
		public int adjustment
		{
			get
			{
				return this._adjustment;
			}
			set
			{
				this._adjustment = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
