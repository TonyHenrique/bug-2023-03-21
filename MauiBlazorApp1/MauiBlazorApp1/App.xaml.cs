namespace MauiBlazorApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            //this.MainPage = new MainPage();
            this.MainPage = new NavigationPage(new MainPage())
            //{
            //    Title = "Título",
            //    BarBackgroundColor = Color.Parse("Yellow")
            //}
            ;
        }
    }
}