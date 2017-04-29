using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(91), ForSend(91), ProtoContract(Name = "WeekVsInfosReq")]
	[Serializable]
	public class WeekVsInfosReq : IExtensible
	{
		public static readonly short OP = 91;

		private int _week;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "week", DataFormat = DataFormat.TwosComplement)]
		public int week
		{
			get
			{
				return this._week;
			}
			set
			{
				this._week = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
