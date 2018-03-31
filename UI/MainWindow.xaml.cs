using Services.DataService;
using Services.StorageService;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private  void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var init = new Initilize();
            init.InsertTestData();

            var repo = new UStoreRepository();
            var accounts = repo.GetAll<GoogleAccount>();

            foreach (var acc in accounts)
            {
                acc.AuthorizeAsync().ContinueWith(c => {
                    acc.GetFilesAsync().ContinueWith(fs =>
                    {
                        foreach (var file in fs.Result)
                        {
                            logger.Info("{0} - {1}", acc.DisplayName, file.FileName);
                        }
                    });
                });
            }
        }
    }
}
