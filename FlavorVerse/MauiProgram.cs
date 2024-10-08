﻿using FlavorVerse.Interfaces;
using FlavorVerse.Services;
using Microsoft.Extensions.Logging;

namespace FlavorVerse;

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
            });

        builder.Services.AddScoped<IRecipeService, RecipeService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}