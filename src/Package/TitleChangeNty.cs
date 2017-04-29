using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6668), ForSend(6668), ProtoContract(Name = "TitleChangeNty")]
	[Serializable]
	public class TitleChangeNty : IExtensible
	{
		public static readonly short OP = 6668;

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
