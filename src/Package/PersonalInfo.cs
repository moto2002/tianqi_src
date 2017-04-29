using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "PersonalInfo")]
	[Serializable]
	public class PersonalInfo : IExtensible
	{
		private int _points;

		private int _exchanges;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "points", DataFormat = DataFormat.TwosComplement)]
		public int points
		{
			get
			{
				return this._points;
			}
			set
			{
				this._points = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "exchanges", DataFormat = DataFormat.TwosComplement)]
		public int exchanges
		{
			get
			{
				return this._exchanges;
			}
			set
			{
				this._exchanges = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
