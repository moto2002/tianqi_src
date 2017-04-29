using System;

public class WarningMessage : GraghMessage
{
	public float casterRadius;

	public WarningMessage(XPoint theFixBasePoint, GraghShape theAreaShape, float theRange, float theAngle, float theWidth, float theHeight, float theCasterRadius) : base(theFixBasePoint, theAreaShape, theRange, theAngle, theWidth, theHeight)
	{
		this.casterRadius = theCasterRadius;
	}
}
