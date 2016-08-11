using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Twitter
{
	public class HomePageModel: INotifyPropertyChanged
	{
		public ObservableCollection<Entity2> lista
		{
			get { return _lista; }
			set { _lista = value; OnPropertyChanged("lista"); }
		} private ObservableCollection<Entity2> _lista;

		public int a { get; set; }

		public HomePageModel()
		{
			MessagingCenter.Subscribe<object, List<LinqToTwitter.Status>>(this, "ListUpdated", (arg1, arg2) =>
			{
				System.Diagnostics.Debug.WriteLine("got message");
				parser(arg2);
			}, null);
		}

		public void parser(List<LinqToTwitter.Status> tline)
		{
			lista = new ObservableCollection<Entity2>();
			for (int i = 0; i < tline.Count; i++)
			{
				lista.Add(new Entity2() { Text = tline[i].Text, Name = tline[i].User.Name, Image = tline[i].User.ProfileImageUrl });

			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

