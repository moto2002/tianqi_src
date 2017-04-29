using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(997), ForSend(997), ProtoContract(Name = "MessagePushSettingPush")]
	[Serializable]
	public class MessagePushSettingPush : IExtensible
	{
		public static readonly short OP = 997;

		private readonly List<ItemSetting> _setting = new List<ItemSetting>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "setting", DataFormat = DataFormat.Default)]
		public List<ItemSetting> setting
		{
			get
			{
				return this._setting;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
