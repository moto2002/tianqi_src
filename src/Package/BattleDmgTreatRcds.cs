using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "BattleDmgTreatRcds")]
	[Serializable]
	public class BattleDmgTreatRcds : IExtensible
	{
		private readonly List<BattleDmgTreatRcd> _actives = new List<BattleDmgTreatRcd>();

		private readonly List<BattleDmgTreatRcd> _inActives = new List<BattleDmgTreatRcd>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "actives", DataFormat = DataFormat.Default)]
		public List<BattleDmgTreatRcd> actives
		{
			get
			{
				return this._actives;
			}
		}

		[ProtoMember(2, Name = "inActives", DataFormat = DataFormat.Default)]
		public List<BattleDmgTreatRcd> inActives
		{
			get
			{
				return this._inActives;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
