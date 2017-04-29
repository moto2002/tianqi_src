using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "CeLveZu")]
	[Serializable]
	public class CeLveZu : IExtensible
	{
		private string _StrategyId;

		private readonly List<int> _StrategyPercentage = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "StrategyId", DataFormat = DataFormat.Default)]
		public string StrategyId
		{
			get
			{
				return this._StrategyId;
			}
			set
			{
				this._StrategyId = value;
			}
		}

		[ProtoMember(10, Name = "StrategyPercentage", DataFormat = DataFormat.TwosComplement)]
		public List<int> StrategyPercentage
		{
			get
			{
				return this._StrategyPercentage;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
