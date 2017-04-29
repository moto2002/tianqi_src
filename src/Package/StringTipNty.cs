using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(554), ForSend(554), ProtoContract(Name = "StringTipNty")]
	[Serializable]
	public class StringTipNty : IExtensible
	{
		public static readonly short OP = 554;

		private readonly List<int> _types = new List<int>();

		private readonly List<string> _strings = new List<string>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "types", DataFormat = DataFormat.TwosComplement)]
		public List<int> types
		{
			get
			{
				return this._types;
			}
		}

		[ProtoMember(2, Name = "strings", DataFormat = DataFormat.Default)]
		public List<string> strings
		{
			get
			{
				return this._strings;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
