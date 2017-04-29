using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2884), ForSend(2884), ProtoContract(Name = "EnterMapUiRes")]
	[Serializable]
	public class EnterMapUiRes : IExtensible
	{
		public static readonly short OP = 2884;

		private readonly List<RoomUiInfo> _roomUiInfo = new List<RoomUiInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "roomUiInfo", DataFormat = DataFormat.Default)]
		public List<RoomUiInfo> roomUiInfo
		{
			get
			{
				return this._roomUiInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
