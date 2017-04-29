using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DianFengZhanChangPaiMingJiangLi")]
	[Serializable]
	public class DianFengZhanChangPaiMingJiangLi : IExtensible
	{
		private int _rank;

		private readonly List<int> _rewards = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "rank", DataFormat = DataFormat.TwosComplement)]
		public int rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				this._rank = value;
			}
		}

		[ProtoMember(3, Name = "rewards", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewards
		{
			get
			{
				return this._rewards;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
