using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(731), ForSend(731), ProtoContract(Name = "BattleTimeInfoNty")]
	[Serializable]
	public class BattleTimeInfoNty : IExtensible
	{
		public static readonly short OP = 731;

		private BattleTimeInfo _timeInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "timeInfo", DataFormat = DataFormat.Default)]
		public BattleTimeInfo timeInfo
		{
			get
			{
				return this._timeInfo;
			}
			set
			{
				this._timeInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
