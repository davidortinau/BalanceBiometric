using BalanceBiometric.Models;
using BalanceBiometric.PageModels;
using Plugin.Fingerprint.Abstractions;

namespace BalanceBiometric.Pages;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}

	
}