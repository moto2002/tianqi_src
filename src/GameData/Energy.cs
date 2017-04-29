using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "Energy")]
	[Serializable]
	public class Energy : IExtensible
	{
		private uint _initEnergy;

		private string _energyLmts;

		private uint _recoverSpeed;

		private readonly List<uint> _fixedRecoverTime = new List<uint>();

		private uint _fixedRecoverEnergy;

		private uint _buyEnergyVal;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "initEnergy", DataFormat = DataFormat.TwosComplement)]
		public uint initEnergy
		{
			get
			{
				return this._initEnergy;
			}
			set
			{
				this._initEnergy = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "energyLmts", DataFormat = DataFormat.Default)]
		public string energyLmts
		{
			get
			{
				return this._energyLmts;
			}
			set
			{
				this._energyLmts = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "recoverSpeed", DataFormat = DataFormat.TwosComplement)]
		public uint recoverSpeed
		{
			get
			{
				return this._recoverSpeed;
			}
			set
			{
				this._recoverSpeed = value;
			}
		}

		[ProtoMember(4, Name = "fixedRecoverTime", DataFormat = DataFormat.TwosComplement)]
		public List<uint> fixedRecoverTime
		{
			get
			{
				return this._fixedRecoverTime;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "fixedRecoverEnergy", DataFormat = DataFormat.TwosComplement)]
		public uint fixedRecoverEnergy
		{
			get
			{
				return this._fixedRecoverEnergy;
			}
			set
			{
				this._fixedRecoverEnergy = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "buyEnergyVal", DataFormat = DataFormat.TwosComplement)]
		public uint buyEnergyVal
		{
			get
			{
				return this._buyEnergyVal;
			}
			set
			{
				this._buyEnergyVal = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
