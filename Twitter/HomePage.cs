using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Twitter
{
	public class HomePage : ContentPage
	{
		public static ListView list { get; set; }
		public HomePage(List<Entity2> items)
		{
			list = new ListView();
			list.ItemTemplate = new DataTemplate(typeof(CustomListChuchu));
			list.HorizontalOptions = LayoutOptions.FillAndExpand;
			list.ItemsSource = items;
			list.HasUnevenRows = true;
			Content = new StackLayout
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					new Label { HorizontalTextAlignment=TextAlignment.Center,HorizontalOptions = LayoutOptions.CenterAndExpand,Text = App.User.ScreenName +"\n"+ App.User.TwitterId },
					list,
				}
			};
		}
		//public void parser(List<LinqToTwitter.Status> tline)
		//{
		//	lista = new ObservableCollection<Entity2>();
		//	for (int i = 0; i < tline.Count; i++)
		//	{
		//		lista.Add(new Entity2() { Text = tline[i].Text, Name = tline[i].User.Name, Image = tline[i].User.ProfileImageUrl });
		//	}
		//}
	}

	class CustomListChuchu : ViewCell
	{


		public CustomListChuchu()
		{

			Image image = new Image
			{
				WidthRequest = 50,
				HeightRequest = 50
			};
			image.SetBinding(Image.SourceProperty,new Binding("Image"));

			Label titlelbl = new Label
			{
				TextColor = Color.Accent,
				FontSize = 15,
				FontAttributes = Xamarin.Forms.FontAttributes.Italic,
			};
			titlelbl.SetBinding(Label.TextProperty, "Text");

			Label deslbl = new Label
			{
				TextColor = Color.Black,
				FontSize = 10,
				FontAttributes = Xamarin.Forms.FontAttributes.Bold,
			};
			deslbl.SetBinding(Label.TextProperty, "Name");


			StackLayout firstStack = new StackLayout
			{
				Padding = new Thickness(5,5,5,5),
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { titlelbl, deslbl }
			};

			StackLayout main = new StackLayout
			{
				Padding = new Thickness(5, 5, 5, 5),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { image,firstStack }
			};

			View = main;
		}


	}
}


