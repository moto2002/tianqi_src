using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(7621), ForSend(7621), ProtoContract(Name = "TitleProgressNty")]
	[Serializable]
	public class TitleProgressNty : IExtensible
	{
		public static readonly short OP = 7621;

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
