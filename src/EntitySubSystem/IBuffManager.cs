using System;
using System.Collections.Generic;

namespace EntitySubSystem
{
	public interface IBuffManager
	{
		bool AddBuff(int buffID, int time);

		void RemoveBuff(int buffID);

		void ClearBuff();

		bool HasTypeBuff(int type);

		bool HasBuff(int buffID);

		List<int> GetBuffList();
	}
}
