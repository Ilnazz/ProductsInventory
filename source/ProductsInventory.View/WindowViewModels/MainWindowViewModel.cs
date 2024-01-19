using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using ProductsInventory.Models;
using ProductsInventory.Services;
using ProductsInventory.View.DialogViewModels;

namespace ProductsInventory.View.WindowViewModels;

public partial class MainWindowViewModel
(
    IProductRepository productRepository,
    IDialogService dialogService
)
    : ObservableObject, IViewClosing
{
    #region Properties
    public ObservableCollection<Product> Products { get; } = new(productRepository.Products);

    public decimal ProductsTotalPrice => Products.Sum(x => x.Quantity * x.Price);
    #endregion

    #region Commands
    [RelayCommand]
    private Task CreateProduct() => ShowDialogAsync<CreateProductDialogViewModel>();

    [RelayCommand(CanExecute = nameof(IsThereProduct))]
    private Task EditProductQuantity() => ShowDialogAsync<EditProductDialogViewModel>();

    [RelayCommand(CanExecute = nameof(IsThereProduct))]
    private Task DeleteProduct() => ShowDialogAsync<DeleteProductDialogViewModel>();
    #endregion

    #region Event handlers
    public void OnClosing(CancelEventArgs e) => e.Cancel = true;

    public async Task OnClosingAsync(CancelEventArgs e)
    {
        await dialogService.ShowMessageBoxAsync(this, "The application is closing.", "Information");
        
        e.Cancel = false;
    }
    #endregion

    #region Private methods
    private async Task ShowDialogAsync<TViewModel>() where TViewModel : IModalDialogViewModel
    {
        var dialogViewModel = dialogService.CreateViewModel<TViewModel>();

        var result = await dialogService.ShowDialogAsync(this, dialogViewModel).ConfigureAwait(true);
        if (result is true)
        {
            NotifyProductsAndTotalPriceChanged();
            NotifyCommandsCanExecuteChanged();
        }
    }
    
    private bool IsThereProduct() => productRepository.Products.Any();

    private void NotifyProductsAndTotalPriceChanged()
    {
        Products.Clear();
            
        foreach (var product in productRepository.Products)
            Products.Add(product);
        
        OnPropertyChanged(nameof(ProductsTotalPrice));
    }

    private void NotifyCommandsCanExecuteChanged()
    {
        CreateProductCommand.NotifyCanExecuteChanged();
        EditProductQuantityCommand.NotifyCanExecuteChanged();
        DeleteProductCommand.NotifyCanExecuteChanged();
    }
    #endregion
}