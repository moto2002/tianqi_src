using System;
using System.Collections.Generic;
using UnityEngine;

public class FXBrightnessRange : MonoBehaviour
{
	private const float BrightnessRangeMin = 0f;

	private const float BrightnessRangeMax = 4f;

	private const string NAME = "_BrightnessRatio";

	private float minBrightness;

	private float maxBrightness;

	private float deltaBrightness;

	private Material targetMaterial;

	private float currentBrightness;

	private bool isAdd = true;

	private void OnDisable()
	{
	}

	private void Update()
	{
		if (this.isAdd)
		{
			this.currentBrightness += Time.get_deltaTime() * this.deltaBrightness;
			if (this.currentBrightness >= this.maxBrightness)
			{
				this.isAdd = false;
				this.SetBrightness(this.maxBrightness);
			}
			else
			{
				this.SetBrightness(this.currentBrightness);
			}
		}
		else
		{
			this.currentBrightness -= Time.get_deltaTime() * this.deltaBrightness;
			if (this.currentBrightness <= this.minBrightness)
			{
				this.isAdd = true;
				this.SetBrightness(this.minBrightness);
			}
			else
			{
				this.SetBrightness(this.currentBrightness);
			}
		}
	}

	private void SetBrightness(float brightness)
	{
		this.currentBrightness = brightness;
		this.targetMaterial.SetFloat("_BrightnessRatio", brightness);
	}

	public void SetRange(Material material, float min, float max, float duration)
	{
		this.targetMaterial = material;
		this.currentBrightness = material.GetFloat("_BrightnessRatio");
		this.minBrightness = Mathf.Max(min, 0f);
		this.maxBrightness = Mathf.Min(max, 4f);
		this.deltaBrightness = (max - min) / duration;
	}

	public void SetRange(Material material, List<float> brightnessRange)
	{
		if (brightnessRange.get_Count() >= 3)
		{
			this.SetRange(material, brightnessRange.get_Item(0), brightnessRange.get_Item(1), brightnessRange.get_Item(2));
		}
	}
}
