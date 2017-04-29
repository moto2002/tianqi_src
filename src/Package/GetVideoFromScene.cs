using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(158), ForSend(158), ProtoContract(Name = "GetVideoFromScene")]
	[Serializable]
	public class GetVideoFromScene : IExtensible
	{
		public static readonly short OP = 158;

		private readonly List<SaveVideoToScene> _videos = new List<SaveVideoToScene>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "videos", DataFormat = DataFormat.Default)]
		public List<SaveVideoToScene> videos
		{
			get
			{
				return this._videos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
