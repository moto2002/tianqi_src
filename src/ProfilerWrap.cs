using System;
using System.Collections.Generic;
using UnityEngine;

internal class ProfilerWrap : IDisposable
{
	private string Name;

	private static Queue<ProfilerWrap> Pool;

	private bool IsBeginSample;

	public static void InitPool()
	{
		Debug.Log("ProfilerWrap init pool");
		ProfilerWrap.Pool = new Queue<ProfilerWrap>();
		for (int i = 0; i < 1000; i++)
		{
			ProfilerWrap.Pool.Enqueue(new ProfilerWrap());
		}
	}

	private static void Push(ProfilerWrap destroy)
	{
	}

	private static ProfilerWrap Pop()
	{
		return new ProfilerWrap();
	}

	public static ProfilerWrap Begin(string name)
	{
		ProfilerWrap profilerWrap = ProfilerWrap.Pop();
		profilerWrap.Name = name;
		profilerWrap.IsBeginSample = false;
		profilerWrap.BeginSample("begin");
		return profilerWrap;
	}

	public void BeginSample(string msg)
	{
		this.EndSample();
		this.IsBeginSample = true;
	}

	public void EndSample()
	{
		if (this.IsBeginSample)
		{
			this.IsBeginSample = false;
		}
	}

	public void Dispose()
	{
		this.EndSample();
		ProfilerWrap.Push(this);
	}
}
