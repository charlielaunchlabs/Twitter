using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Twitter;
using Twitter.Droid;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Linq;
using LinqToTwitter;


[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace Twitter.Droid
{
	public class LoginPageRenderer : PageRenderer
	{
		
		public LoginPageRenderer()
		{
			var activity = this.Context as Activity;
			List<LinqToTwitter.Status> tl;
			var auth = new OAuth1Authenticator(
				  consumerKey: "GommFIG3ClHfqDoVHHKVaf9lR",
				  consumerSecret: "HxUMkmYTp9ecYmiBIz1UAk1G4RDXF0H1U7OCNvFmLBaqflUaJ5",
				  requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
				  authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
				  accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
				callbackUrl: new Uri("https://www.facebook.com/connect/login_success.html"));

			auth.Completed += async(sender, e) =>
			{


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


					tl = new List<LinqToTwitter.Status>();

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
					tl = await(from tweet in TwitterCtx.Status
							   where tweet.Type == LinqToTwitter.StatusType.Home
							   select tweet).ToListAsync();

					Console.WriteLine("Tweets Returned: " + tl.Count.ToString());
					//App.User.timeline = tl;
					//App.parser(tl);

					MessagingCenter.Send<object, List<LinqToTwitter.Status>>(this, "ListUpdated",tl);
					Console.WriteLine("[Android] sent message");

					await App.NavigateToHome();
				}
				else {
					
				}
			};

			activity.StartActivity(auth.GetUI(activity));
		}
	}
}


