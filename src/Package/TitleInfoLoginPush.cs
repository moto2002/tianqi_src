using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6521), ForSend(6521), ProtoContract(Name = "TitleInfoLoginPush")]
	[Serializable]
	public class TitleInfoLoginPush : IExtensible
	{
		public static readonly short OP = 6521;

		private readonly List<TitleInfo> _infos = new List<TitleInfo>();

		private int _currId;

		private int _svrTime;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infos", DataFormat = DataFormat.Default)]
		public List<TitleInfo> infos
		{
			get
			{
				return this._infos;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "currId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int currId
		{
			get
			{
				return this._currId;
			}
			set
			{
				this._currId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "svrTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int svrTime
		{
			get
			{
				return this._svrTime;
			}
			set
			{
				this._svrTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
