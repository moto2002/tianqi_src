using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanLianShengJiangLi")]
	[Serializable]
	public class JunTuanZhanLianShengJiangLi : IExtensible
	{
		[ProtoContract(Name = "EndrewardPair")]
		[Serializable]
		public class EndrewardPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		[ProtoContract(Name = "StraightrewardPair")]
		[Serializable]
		public class StraightrewardPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _Id;

		private int _WinTime;

		private int _Ranking;

		private int _RankingLv;

		private readonly List<JunTuanZhanLianShengJiangLi.EndrewardPair> _EndReward = new List<JunTuanZhanLianShengJiangLi.EndrewardPair>();

		private readonly List<JunTuanZhanLianShengJiangLi.StraightrewardPair> _StraightReward = new List<JunTuanZhanLianShengJiangLi.StraightrewardPair>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "WinTime", DataFormat = DataFormat.TwosComplement)]
		public int WinTime
		{
			get
			{
				return this._WinTime;
			}
			set
			{
				this._WinTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Ranking", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Ranking
		{
			get
			{
				return this._Ranking;
			}
			set
			{
				this._Ranking = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "RankingLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int RankingLv
		{
			get
			{
				return this._RankingLv;
			}
			set
			{
				this._RankingLv = value;
			}
		}

		[ProtoMember(6, Name = "EndReward", DataFormat = DataFormat.Default)]
		public List<JunTuanZhanLianShengJiangLi.EndrewardPair> EndReward
		{
			get
			{
				return this._EndReward;
			}
		}

		[ProtoMember(7, Name = "StraightReward", DataFormat = DataFormat.Default)]
		public List<JunTuanZhanLianShengJiangLi.StraightrewardPair> StraightReward
		{
			get
			{
				return this._StraightReward;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
