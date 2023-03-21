using Microsoft.AspNetCore.Components;

namespace MauiBlazorApp1;

public partial class NewPage1 : ContentPage
{
    internal NavigationManager NavigationManager { get;set;}
    public NewPage1(NavigationManager NavigationManager)
	{
		InitializeComponent();
        this.NavigationManager = NavigationManager;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        NavigationManager.NavigateTo($"/counter", forceLoad: true, true);
        await Task.Delay(500);
        await App.Current.MainPage.Navigation.PopAsync();
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await App.Current.MainPage.Navigation.PopAsync();
    }
}