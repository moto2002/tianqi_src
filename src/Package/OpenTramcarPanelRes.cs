using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3286), ForSend(3286), ProtoContract(Name = "OpenTramcarPanelRes")]
	[Serializable]
	public class OpenTramcarPanelRes : IExtensible
	{
		public static readonly short OP = 3286;

		private int _mapId;

		private readonly List<DropItem> _item = new List<DropItem>();

		private int _curQuality;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "mapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mapId
		{
			get
			{
				return this._mapId;
			}
			set
			{
				this._mapId = value;
			}
		}

		[ProtoMember(2, Name = "item", DataFormat = DataFormat.Default)]
		public List<DropItem> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "curQuality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int curQuality
		{
			get
			{
				return this._curQuality;
			}
			set
			{
				this._curQuality = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
