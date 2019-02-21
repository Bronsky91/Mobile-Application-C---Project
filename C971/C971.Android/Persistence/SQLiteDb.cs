using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using C971.Droid;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Dependency(typeof(SQLiteDb))]

namespace C971.Droid
{
	public class SQLiteDb : ISQLiteDb
	{
        
		public SQLiteAsyncConnection GetConnection()
		{
			var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			var path = Path.Combine(documentsPath, "MySQLite.db3");

			return new SQLiteAsyncConnection(path);
		}
        
	}
}

