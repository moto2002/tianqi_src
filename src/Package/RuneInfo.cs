using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "RuneInfo")]
	[Serializable]
	public class RuneInfo : IExtensible
	{
		private int _runeId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "runeId", DataFormat = DataFormat.TwosComplement)]
		public int runeId
		{
			get
			{
				return this._runeId;
			}
			set
			{
				this._runeId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
