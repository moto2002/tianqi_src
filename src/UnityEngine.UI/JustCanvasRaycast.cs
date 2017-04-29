using System;

namespace UnityEngine.UI
{
	public class JustCanvasRaycast : MaskableGraphic
	{
		protected JustCanvasRaycast()
		{
			base.set_useLegacyMeshGeneration(false);
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
		}
	}
}
