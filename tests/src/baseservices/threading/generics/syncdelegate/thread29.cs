// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.Threading;

class Gen 
{
	public static void Target<U>()
	{		
		//dummy line to avoid warnings
		Test.Eval(typeof(U)!=null);	 
		Interlocked.Increment(ref Test.Xcounter);
	}
	public static void DelegateTest<U>()
	{
		ThreadStart d = new ThreadStart(Gen.Target<U>);
		
		
		d();
		Test.Eval(Test.Xcounter==1);
		Test.Xcounter = 0;
	}
}

public class Test
{
	public static int nThreads =50;
	public static int counter = 0;
	public static int Xcounter = 0;
	public static bool result = true;
	public static void Eval(bool exp)
	{
		counter++;
		if (!exp)
		{
			result = exp;
			Console.WriteLine("Test Failed at location: " + counter);
		}
	
	}
	
	public static int Main()
	{
		Gen.DelegateTest<object>();
		Gen.DelegateTest<string>();
		Gen.DelegateTest<Guid>();
		Gen.DelegateTest<int>(); 
		Gen.DelegateTest<double>(); 

		if (result)
		{
			Console.WriteLine("Test Passed");
			return 100;
		}
		else
		{
			Console.WriteLine("Test Failed");
			return 1;
		}
	}
}		


