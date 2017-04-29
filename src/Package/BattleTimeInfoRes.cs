using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3810), ForSend(3810), ProtoContract(Name = "BattleTimeInfoRes")]
	[Serializable]
	public class BattleTimeInfoRes : IExtensible
	{
		public static readonly short OP = 3810;

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
