using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(6713), ForSend(6713), ProtoContract(Name = "ActivityChangeNty")]
	[Serializable]
	public class ActivityChangeNty : IExtensible
	{
		public static readonly short OP = 6713;

		private readonly List<ActivityInfo> _activitiesInfo = new List<ActivityInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "activitiesInfo", DataFormat = DataFormat.Default)]
		public List<ActivityInfo> activitiesInfo
		{
			get
			{
				return this._activitiesInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
