using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using Microsoft.Extensions.DependencyInjection;
using ProductsInventory.Avalonia.DataTemplates;
using ProductsInventory.Services;
using ProductsInventory.View.DialogViewModels;
using ProductsInventory.View.WindowViewModels;

namespace ProductsInventory.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Ioc.Default.ConfigureServices
        (
            new ServiceCollection()
                .AddSingleton<IProductRepository>(new LocalProductRepository(150))
				.AddSingleton<IDialogService>
				(
					new DialogService
                    (
                        new DialogManager
                        (
                            viewLocator: new ViewLocator(),
                            dialogFactory: new DialogFactory().AddMessageBox()
                        ),
                        viewModelFactory: x => Ioc.Default.GetService(x)
                    )
				)
                .AddSingleton<MainWindowViewModel>()
                .AddTransient<CreateProductDialogViewModel>()
                .AddTransient<EditProductDialogViewModel>()
                .AddTransient<DeleteProductDialogViewModel>()
                .BuildServiceProvider()
        );

        var dialogService = Ioc.Default.GetService<IDialogService>()!;
        var mainWindowViewModel = Ioc.Default.GetService<MainWindowViewModel>()!;
        
        GC.KeepAlive(dialogService);
        dialogService.Show(null, mainWindowViewModel);
        
        // switch (ApplicationLifetime)
        // {
        //     case IClassicDesktopStyleApplicationLifetime desktop:
        //         desktop.MainWindow = new Window { Content = productRepositoryVm };
        //         break;
        //     
        //     case ISingleViewApplicationLifetime singleViewPlatform:
        //         singleViewPlatform.MainView = new ContentControl { Content = productRepositoryVm };
        //         break;
        // }

        base.OnFrameworkInitializationCompleted();
    }
}