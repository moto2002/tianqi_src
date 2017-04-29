using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(79), ForSend(79), ProtoContract(Name = "MinorLoginNty")]
	[Serializable]
	public class MinorLoginNty : IExtensible
	{
		public static readonly short OP = 79;

		private int _minorLogin;

		private readonly List<ItemBriefInfo> _items = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "minorLogin", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minorLogin
		{
			get
			{
				return this._minorLogin;
			}
			set
			{
				this._minorLogin = value;
			}
		}

		[ProtoMember(2, Name = "items", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> items
		{
			get
			{
				return this._items;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
