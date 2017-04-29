using System;

public class NcDontActive : NcEffectBehaviour
{
	private void Awake()
	{
		base.get_gameObject().SetActive(false);
	}

	private void OnEnable()
	{
		base.get_gameObject().SetActive(false);
	}
}
