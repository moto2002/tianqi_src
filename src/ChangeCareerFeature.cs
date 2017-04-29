using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCareerFeature : MonoBehaviour
{
	public void SetName(string name)
	{
		base.get_transform().FindChild("FeatureName").GetComponent<Text>().set_text(name);
	}

	public void SetStar(int num)
	{
		for (int i = 1; i <= 5; i++)
		{
			Transform transform = base.get_transform().FindChild("FeatureStars").FindChild("FeatureStar" + i);
			if (i <= num)
			{
				ResourceManager.SetSprite(transform.GetComponent<Image>(), ResourceManager.GetIconSprite("fb_star_1"));
			}
			else
			{
				ResourceManager.SetSprite(transform.GetComponent<Image>(), ResourceManager.GetIconSprite("fb_star_2"));
			}
		}
	}
}
