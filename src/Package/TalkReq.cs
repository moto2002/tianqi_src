using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(289), ForSend(289), ProtoContract(Name = "TalkReq")]
	[Serializable]
	public class TalkReq : IExtensible
	{
		public static readonly short OP = 289;

		private readonly List<Audience> _audiences = new List<Audience>();

		private ArticleContent _content;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "audiences", DataFormat = DataFormat.Default)]
		public List<Audience> audiences
		{
			get
			{
				return this._audiences;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "content", DataFormat = DataFormat.Default)]
		public ArticleContent content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
