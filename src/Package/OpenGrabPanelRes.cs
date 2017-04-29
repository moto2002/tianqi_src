using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2753), ForSend(2753), ProtoContract(Name = "OpenGrabPanelRes")]
	[Serializable]
	public class OpenGrabPanelRes : IExtensible
	{
		public static readonly short OP = 2753;

		private readonly List<TramcarRoomInfo> _infoList = new List<TramcarRoomInfo>();

		private int _lastGrabTime;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infoList", DataFormat = DataFormat.Default)]
		public List<TramcarRoomInfo> infoList
		{
			get
			{
				return this._infoList;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "lastGrabTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastGrabTime
		{
			get
			{
				return this._lastGrabTime;
			}
			set
			{
				this._lastGrabTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
