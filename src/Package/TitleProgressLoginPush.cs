using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(5162), ForSend(5162), ProtoContract(Name = "TitleProgressLoginPush")]
	[Serializable]
	public class TitleProgressLoginPush : IExtensible
	{
		public static readonly short OP = 5162;

		private readonly List<TitleProgress> _progress = new List<TitleProgress>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "progress", DataFormat = DataFormat.Default)]
		public List<TitleProgress> progress
		{
			get
			{
				return this._progress;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
