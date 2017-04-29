using System;
using UnityEngine;

public class PosRecord
{
	public long id;

	public int sceneID;

	public Vector3 pos;

	public int way;

	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"[id: ",
			this.id,
			" sceneID: ",
			this.sceneID,
			" pos:",
			PosDirUtility.ToDetailString(this.pos),
			" way: ",
			this.way,
			"] "
		});
	}
}
