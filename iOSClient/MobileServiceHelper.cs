using Microsoft.WindowsAzure.MobileServices;

namespace iOSClient
{

    public class MobileServiceHelper
    {

        private static MobileServiceHelper _instance;

		public const string DotNetURL = @"https://dotnet1.azure-mobile.net/";
		public const string DotNetKey = @"SqbNCQdEyoxKVoDUwqCZeOPSaSwqXd35";
		public const string JavaScriptURL = @"https://javascript1.azure-mobile.net/";
		public const string JavaScriptKey = @"PIEGJZrDOjaRNrktRnavFAGvUJPiOj51";
		public string applicationURL = DotNetURL;
		public string applicationKey = DotNetKey;

        private MobileServiceClient _client;

        private MobileServiceHelper()
        {
            CurrentPlatform.Init();
            //SQLitePCL.CurrentPlatform.Init();

            // Initialize the Mobile Service client with your URL and key
            _client = new MobileServiceClient(applicationURL, applicationKey);
        }

        private static volatile object _syncRoot = new object();

        public MobileServiceClient ServiceClient { get { return _client; } }

		public void selectDotNet()
		{
			_client = new MobileServiceClient(DotNetURL, DotNetKey);
		}

		public void selectJavaScript()
		{
			_client = new MobileServiceClient(JavaScriptURL, JavaScriptKey);
		}

		public void selectUser(string URL, string Key)
		{
			_client = new MobileServiceClient(URL, Key);
		}

        public static MobileServiceHelper DefaultService
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new MobileServiceHelper();
                        }
                    }
                }

                return _instance;
            }
        }

    }
}
