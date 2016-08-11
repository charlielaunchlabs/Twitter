using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Twitter
{
	public class Entity1 
	{
		public string TwitterId { get; set; }
		public string Name { get; set; }
		public string ScreenName { get; set; }
		public string Token { get; set; }
		public string TokenSecret { get; set; }
		public string CToken { get; set; }
		public string CTokenSecret { get; set; }
		public List<LinqToTwitter.Status> timeline{ get; set; }
		public bool IsAuthenticated
		{
			get
			{
				return !string.IsNullOrWhiteSpace(Token);
			}
		}
	
	}
}


