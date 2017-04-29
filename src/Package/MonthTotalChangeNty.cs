using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(3929), ForSend(3929), ProtoContract(Name = "MonthTotalChangeNty")]
	[Serializable]
	public class MonthTotalChangeNty : IExtensible
	{
		public static readonly short OP = 3929;

		private readonly List<MonthTotalInfo> _monthTotalInfo = new List<MonthTotalInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "monthTotalInfo", DataFormat = DataFormat.Default)]
		public List<MonthTotalInfo> monthTotalInfo
		{
			get
			{
				return this._monthTotalInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
