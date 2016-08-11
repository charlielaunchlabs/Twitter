using System;
using System.Collections.Generic;

namespace Twitter
{
	public class parser
	{
		public static List<Entity2> lista = new List<Entity2>();
		public parser(List<LinqToTwitter.Status> tline)
		{
			for (int i = 0; i < tline.Count; i++)
			{
				lista.Add(new Entity2() { Text = tline[i].Text });
			}
		}
	}
}

