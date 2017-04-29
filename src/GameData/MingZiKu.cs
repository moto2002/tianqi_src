using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "MingZiKu")]
	[Serializable]
	public class MingZiKu : IExtensible
	{
		private int _familyName;

		private int _boyName;

		private int _girlName;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "familyName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int familyName
		{
			get
			{
				return this._familyName;
			}
			set
			{
				this._familyName = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "boyName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int boyName
		{
			get
			{
				return this._boyName;
			}
			set
			{
				this._boyName = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "girlName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int girlName
		{
			get
			{
				return this._girlName;
			}
			set
			{
				this._girlName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
