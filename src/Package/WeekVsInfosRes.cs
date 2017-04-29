using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(92), ForSend(92), ProtoContract(Name = "WeekVsInfosRes")]
	[Serializable]
	public class WeekVsInfosRes : IExtensible
	{
		public static readonly short OP = 92;

		private int _week;

		private readonly List<GuildVsInfo> _first8Infos = new List<GuildVsInfo>();

		private readonly List<GuildVsInfo> _second4Infos = new List<GuildVsInfo>();

		private readonly List<GuildVsInfo> _third2Infos = new List<GuildVsInfo>();

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

		[ProtoMember(2, Name = "first8Infos", DataFormat = DataFormat.Default)]
		public List<GuildVsInfo> first8Infos
		{
			get
			{
				return this._first8Infos;
			}
		}

		[ProtoMember(3, Name = "second4Infos", DataFormat = DataFormat.Default)]
		public List<GuildVsInfo> second4Infos
		{
			get
			{
				return this._second4Infos;
			}
		}

		[ProtoMember(4, Name = "third2Infos", DataFormat = DataFormat.Default)]
		public List<GuildVsInfo> third2Infos
		{
			get
			{
				return this._third2Infos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
