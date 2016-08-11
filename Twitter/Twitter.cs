using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Twitter
{
	public class App : Application
	{
		public static Entity1 User;
		public static List<Entity2> lista;
		public App()
		{
			
			Button btn = new Button();
			btn.Text = "Press";

			// The root page of your application
			var content = new ContentPage
			{
				Title = "Twitter",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					Children =
				{
					new Label { Text = "Hello MaiPage" },
					btn
				}
				}
			};


			MainPage = new NavigationPage(content);

			btn.Clicked += async (sender, e) =>
			{
				await content.Navigation.PushAsync(new LoginPage());
			};

			MessagingCenter.Subscribe<object, List<LinqToTwitter.Status>>(this, "ListUpdated", parser, null);

		}

		public async static Task NavigateToHome()
		{
			await App.Current.MainPage.Navigation.PushAsync(new HomePage(lista));
		}
		public static Action HideLoginView
		{
			get
			{
				return new Action(() => App.Current.MainPage.Navigation.PopModalAsync());
			}
		}

		//public static List<Entity2> lista;

		public void parser(object sender, List<LinqToTwitter.Status> tline)
		{
			lista = new List<Entity2>();
			for (int i = 0; i < tline.Count; i++)
			{
				lista.Add(new Entity2() { Text = tline[i].Text,Name=tline[i].User.Name,Image=tline[i].User.ProfileImageUrl });
		
			}
		}


		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

