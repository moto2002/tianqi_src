using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(224), ForSend(224), ProtoContract(Name = "GuildWarLogNty")]
	[Serializable]
	public class GuildWarLogNty : IExtensible
	{
		public static readonly short OP = 224;

		private int _ChinesexId;

		private readonly List<string> _Parameters = new List<string>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "ChinesexId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ChinesexId
		{
			get
			{
				return this._ChinesexId;
			}
			set
			{
				this._ChinesexId = value;
			}
		}

		[ProtoMember(2, Name = "Parameters", DataFormat = DataFormat.Default)]
		public List<string> Parameters
		{
			get
			{
				return this._Parameters;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
