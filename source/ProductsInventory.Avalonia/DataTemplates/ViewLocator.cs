using HanumanInstitute.MvvmDialogs.Avalonia;
using ProductsInventory.Avalonia.Dialogs;
using ProductsInventory.Avalonia.Windows;
using ProductsInventory.View.DialogViewModels;
using ProductsInventory.View.WindowViewModels;

namespace ProductsInventory.Avalonia.DataTemplates;

public class ViewLocator : StrongViewLocator
{
    public ViewLocator()
    {
        Register<MainWindowViewModel, MainWindow>();
        Register<CreateProductDialogViewModel, CreateProductDialog>();
        Register<EditProductDialogViewModel, EditProductDialog>();
        Register<DeleteProductDialogViewModel, DeleteProductDialog>();
    }
}