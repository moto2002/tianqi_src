using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(886), ForSend(886), ProtoContract(Name = "ElementCopyLoginPush")]
	[Serializable]
	public class ElementCopyLoginPush : IExtensible
	{
		public static readonly short OP = 886;

		private string _lastBlock;

		private int _exploreEnergy;

		private int _purchaseNum;

		private int _residueRecoverTime;

		private readonly List<string> _mineBlockId = new List<string>();

		private readonly List<ExploreLog> _exploreLogs = new List<ExploreLog>();

		private readonly List<BlockInfo> _activateBlocks = new List<BlockInfo>();

		private readonly List<PropertyInfo> _playerProperty = new List<PropertyInfo>();

		private readonly List<PropertyInfo> _petProperty = new List<PropertyInfo>();

		private readonly List<MinePetInfo> _minePetInfos = new List<MinePetInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "lastBlock", DataFormat = DataFormat.Default)]
		public string lastBlock
		{
			get
			{
				return this._lastBlock;
			}
			set
			{
				this._lastBlock = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "exploreEnergy", DataFormat = DataFormat.TwosComplement)]
		public int exploreEnergy
		{
			get
			{
				return this._exploreEnergy;
			}
			set
			{
				this._exploreEnergy = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "purchaseNum", DataFormat = DataFormat.TwosComplement)]
		public int purchaseNum
		{
			get
			{
				return this._purchaseNum;
			}
			set
			{
				this._purchaseNum = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "residueRecoverTime", DataFormat = DataFormat.TwosComplement)]
		public int residueRecoverTime
		{
			get
			{
				return this._residueRecoverTime;
			}
			set
			{
				this._residueRecoverTime = value;
			}
		}

		[ProtoMember(5, Name = "mineBlockId", DataFormat = DataFormat.Default)]
		public List<string> mineBlockId
		{
			get
			{
				return this._mineBlockId;
			}
		}

		[ProtoMember(6, Name = "exploreLogs", DataFormat = DataFormat.Default)]
		public List<ExploreLog> exploreLogs
		{
			get
			{
				return this._exploreLogs;
			}
		}

		[ProtoMember(7, Name = "activateBlocks", DataFormat = DataFormat.Default)]
		public List<BlockInfo> activateBlocks
		{
			get
			{
				return this._activateBlocks;
			}
		}

		[ProtoMember(8, Name = "playerProperty", DataFormat = DataFormat.Default)]
		public List<PropertyInfo> playerProperty
		{
			get
			{
				return this._playerProperty;
			}
		}

		[ProtoMember(9, Name = "petProperty", DataFormat = DataFormat.Default)]
		public List<PropertyInfo> petProperty
		{
			get
			{
				return this._petProperty;
			}
		}

		[ProtoMember(10, Name = "minePetInfos", DataFormat = DataFormat.Default)]
		public List<MinePetInfo> minePetInfos
		{
			get
			{
				return this._minePetInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
