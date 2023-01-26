using NickApp.SqliteServices;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NickApp
{
    public partial class App : Application
    {

        static SQLiteHelper db;

        public App()
        {
            InitializeComponent();

            Startup.ConfigureServices();

            MainPage = new AppShell();
        }


        public static SQLiteHelper SQLiteDb
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(GetLibraryPath(), "NickSQLite.db3"));


                }
                return db;
            }
        }
        private static string GetLibraryPath()
        {
#if __IOS__
            // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
            // (they don't want non-user-generated data in Documents)
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder instead
#else
            // Just use whatever directory SpecialFolder.Personal returns
            string libraryPath = Xamarin.Essentials.FileSystem.AppDataDirectory;
#endif

            return libraryPath;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
