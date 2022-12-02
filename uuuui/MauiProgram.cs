namespace uuuui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
<<<<<<< HEAD
                fonts.AddFont("malgun.ttf", "malg");
            });
=======
			});
>>>>>>> 3d5290191ac59befdcf1d19f186015af4146ffe8

		return builder.Build();
	}
}
