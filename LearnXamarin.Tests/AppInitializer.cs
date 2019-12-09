using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace LearnXamarin.Tests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .Debug()
                    .ApkFile(@"C:\Users\Miko\Documents\GitHub\LearnXamarin\LearnXamarin.Android\bin\Debug\com.companyname.learnxamarin.apk")
                    .StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}