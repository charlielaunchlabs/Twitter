using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twitter;
using Twitter.iOS;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Linq;
using LinqToTwitter;



[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace Twitter.iOS
{
	public class LoginPageRenderer : PageRenderer
	{
		bool done = false;
		public List<LinqToTwitter.Status> tls;
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			if (done)
				return;
			

			var auth = new OAuth1Authenticator(
								"GommFIG3ClHfqDoVHHKVaf9lR",
				  				"HxUMkmYTp9ecYmiBIz1UAk1G4RDXF0H1U7OCNvFmLBaqflUaJ5",
				  				new Uri("https://api.twitter.com/oauth/request_token"),
				  				new Uri("https://api.twitter.com/oauth/authorize"),
				  				new Uri("https://api.twitter.com/oauth/access_token"),
								new Uri("https://www.facebook.com/connect/login_success.html")
											);

			auth.Completed +=  async(sender, e) =>
			{
				DismissViewController(true, null);

				App.HideLoginView();

				if (e.IsAuthenticated)
				{
					var account = e.Account;
					App.User = new Entity1();

					App.User.CToken = account.Properties["oauth_consumer_key"];
					App.User.CTokenSecret = account.Properties["oauth_consumer_secret"];

					App.User.Token = account.Properties["oauth_token"];
					App.User.TokenSecret = account.Properties["oauth_token_secret"];

					App.User.TwitterId = account.Properties["user_id"];
					App.User.ScreenName = account.Properties["screen_name"];

					AccountStore.Create().Save(account, "Twitter");
					//await App.NavigateToHome();

					tls = new List<LinqToTwitter.Status>();

					var cred = new LinqToTwitter.InMemoryCredentialStore();
					cred.ConsumerKey = account.Properties["oauth_consumer_key"];
					cred.ConsumerSecret = account.Properties["oauth_consumer_secret"];
					cred.OAuthToken = account.Properties["oauth_token"];
					cred.OAuthTokenSecret = account.Properties["oauth_token_secret"];
					var auth0 = new LinqToTwitter.PinAuthorizer()
					{
						CredentialStore = cred,
					};
					var TwitterCtx = new LinqToTwitter.TwitterContext(auth0);
					Console.WriteLine(TwitterCtx.User);
					tls = await (from tweet in TwitterCtx.Status
								where tweet.Type == LinqToTwitter.StatusType.Home
								select tweet).ToListAsync();
					
					Console.WriteLine("Tweets Returned: " + tls.Count.ToString());
					//App.User.timeline = tls;
					//App.parser(tls);

					MessagingCenter.Send<object, List<LinqToTwitter.Status>>(this, "ListUpdated", tls);

					await App.NavigateToHome();
				}
				else {
					//await App.NavigateToLogin();
				}	
			};

			auth.Error += (sender, e) =>
				 {
					  // Do Error work
				  };


			PresentViewController(auth.GetUI(), true, null);
			done = true;
		}
	}


}


