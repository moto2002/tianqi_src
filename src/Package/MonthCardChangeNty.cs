using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(7831), ForSend(7831), ProtoContract(Name = "MonthCardChangeNty")]
	[Serializable]
	public class MonthCardChangeNty : IExtensible
	{
		public static readonly short OP = 7831;

		private readonly List<MonthCardInfo> _monthCardInfos = new List<MonthCardInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "monthCardInfos", DataFormat = DataFormat.Default)]
		public List<MonthCardInfo> monthCardInfos
		{
			get
			{
				return this._monthCardInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
