using Plugin.Fingerprint.Abstractions;

namespace BalanceBiometric;

public partial class AuthenticationPage : ContentPage
{
	private CancellationTokenSource _cancel;
	private bool _initialized;

	public AuthenticationPage()
	{
		InitializeComponent();
		var currentTheme = Application.Current!.UserAppTheme;		
		if(currentTheme != AppTheme.Dark)
		{
			Application.Current!.UserAppTheme = AppTheme.Dark;
		}
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		if (!_initialized)
		{
			_initialized = true;
		}
	}

	private async void Forgot_Clicked(object sender, EventArgs e)
	{
		await AppShell.DisplayToastAsync("You forgot, bummer!");
	}

	private async void Login_Clicked(object sender, EventArgs e)
	{
		if(!string.IsNullOrEmpty(UsernameEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text))
		{
			await AppShell.DisplayToastAsync("Welcome back!");
			this.Window.Page = new AppShell();			
		}
		else
		{
			await AppShell.DisplayToastAsync("Please enter your username and password.");
		}
	}

	private async void OnAuthenticate(object sender, EventArgs e)
	{
		await AuthenticateAsync("Prove you have fingers!");
	}

	private async Task AuthenticateAsync(string reason, string cancel = null, string fallback = null, string tooFast = null)
	{
		_cancel = new CancellationTokenSource(TimeSpan.FromSeconds(10));

		var dialogConfig = new AuthenticationRequestConfiguration("My App", reason)
		{ // all optional
			CancelTitle = cancel,
			FallbackTitle = fallback,
			AllowAlternativeAuthentication = true,
			ConfirmationRequired = false
		};

		// optional
		dialogConfig.HelpTexts.MovedTooFast = tooFast;

		var result = await Plugin.Fingerprint.CrossFingerprint.Current.AuthenticateAsync(dialogConfig, _cancel.Token);

		await SetResultAsync(result);
	}

	private async Task SetResultAsync(FingerprintAuthenticationResult result)
	{
		if (result.Authenticated)
		{
			this.Window.Page = new AppShell();
		}
		else
		{
			await AppShell.DisplayToastAsync($"{result.ErrorMessage}");
		}
	}
}