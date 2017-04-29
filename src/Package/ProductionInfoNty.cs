using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(6838), ForSend(6838), ProtoContract(Name = "ProductionInfoNty")]
	[Serializable]
	public class ProductionInfoNty : IExtensible
	{
		public static readonly short OP = 6838;

		private readonly List<ProductionInfo> _productions = new List<ProductionInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "productions", DataFormat = DataFormat.Default)]
		public List<ProductionInfo> productions
		{
			get
			{
				return this._productions;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
