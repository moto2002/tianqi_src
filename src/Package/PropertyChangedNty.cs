using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(819), ForSend(819), ProtoContract(Name = "PropertyChangedNty")]
	[Serializable]
	public class PropertyChangedNty : IExtensible
	{
		public static readonly short OP = 819;

		private ObjectType.GameObject _object;

		private readonly List<PropertyInfo> _propertyInfo = new List<PropertyInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "object", DataFormat = DataFormat.TwosComplement)]
		public ObjectType.GameObject @object
		{
			get
			{
				return this._object;
			}
			set
			{
				this._object = value;
			}
		}

		[ProtoMember(2, Name = "propertyInfo", DataFormat = DataFormat.Default)]
		public List<PropertyInfo> propertyInfo
		{
			get
			{
				return this._propertyInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
