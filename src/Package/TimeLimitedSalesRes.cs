using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(7212), ForSend(7212), ProtoContract(Name = "TimeLimitedSalesRes")]
	[Serializable]
	public class TimeLimitedSalesRes : IExtensible
	{
		public static readonly short OP = 7212;

		private int _beginTime;

		private int _endTime;

		private readonly List<SingleGoods> _goods = new List<SingleGoods>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "beginTime", DataFormat = DataFormat.TwosComplement)]
		public int beginTime
		{
			get
			{
				return this._beginTime;
			}
			set
			{
				this._beginTime = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "endTime", DataFormat = DataFormat.TwosComplement)]
		public int endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		[ProtoMember(3, Name = "goods", DataFormat = DataFormat.Default)]
		public List<SingleGoods> goods
		{
			get
			{
				return this._goods;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
