using System;
using System.Collections.Generic;
namespace AssemblyCSharp
{
	public class EmptyClass
	{
		List<int> list;


		public EmptyClass()
		{
			list = new List<int>();

		}

		void Print()
        {
			Console.WriteLine(list);
        }
	}
}

