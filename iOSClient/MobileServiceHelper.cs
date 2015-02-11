using Microsoft.WindowsAzure.MobileServices;

namespace iOSClient
{
	/// <summary>
	/// Mobile service helper singleton.
	/// </summary>
    public class MobileServiceHelper
    {
        private static MobileServiceHelper _instance;

		// .Net backend URL and Key.
		public const string DotNetURL = @"https://dotnet1.azure-mobile.net/";
		public const string DotNetKey = @"SqbNCQdEyoxKVoDUwqCZeOPSaSwqXd35";
		// JavaScript backend URL and Key.
		public const string JavaScriptURL = @"https://javascript1.azure-mobile.net/";
		public const string JavaScriptKey = @"PIEGJZrDOjaRNrktRnavFAGvUJPiOj51";
		// The current URL and Key.
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

		/// <summary>
		/// Select the .Net backend.
		/// </summary>
		public void selectDotNet()
		{
			_client = new MobileServiceClient(DotNetURL, DotNetKey);
		}

		/// <summary>
		/// Select the JavaScript backend.
		/// </summary>
		public void selectJavaScript()
		{
			_client = new MobileServiceClient(JavaScriptURL, JavaScriptKey);
		}

		/// <summary>
		/// Select the backend based on URL and Key.
		/// </summary>
		/// <param name="URL">The URL</param>
		/// <param name="Key">The Key</param>
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
