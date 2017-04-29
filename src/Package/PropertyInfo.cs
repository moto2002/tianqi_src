using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "PropertyInfo")]
	[Serializable]
	public class PropertyInfo : IExtensible
	{
		private int _propertyId;

		private int _propertyValue;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "propertyId", DataFormat = DataFormat.TwosComplement)]
		public int propertyId
		{
			get
			{
				return this._propertyId;
			}
			set
			{
				this._propertyId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "propertyValue", DataFormat = DataFormat.TwosComplement)]
		public int propertyValue
		{
			get
			{
				return this._propertyValue;
			}
			set
			{
				this._propertyValue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
