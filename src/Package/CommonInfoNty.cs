using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(576), ForSend(576), ProtoContract(Name = "CommonInfoNty")]
	[Serializable]
	public class CommonInfoNty : IExtensible
	{
		public static readonly short OP = 576;

		private readonly List<CommonKV1> _kvs1 = new List<CommonKV1>();

		private readonly List<CommonKV2> _kvs2 = new List<CommonKV2>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "kvs1", DataFormat = DataFormat.Default)]
		public List<CommonKV1> kvs1
		{
			get
			{
				return this._kvs1;
			}
		}

		[ProtoMember(2, Name = "kvs2", DataFormat = DataFormat.Default)]
		public List<CommonKV2> kvs2
		{
			get
			{
				return this._kvs2;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
