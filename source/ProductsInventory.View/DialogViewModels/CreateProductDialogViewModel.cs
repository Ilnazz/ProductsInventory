using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using ProductsInventory.Exceptions;
using ProductsInventory.Services;

namespace ProductsInventory.View.DialogViewModels;

public partial class CreateProductDialogViewModel
(
    IProductRepository productRepository,
    IDialogService dialogService
)
    : ClosableDialogViewModelBase
{
    #region Properties
    private string _productName = string.Empty;
    public string ProductName
    {
        get => _productName;
        set
        {
            _productName = value;
            
            OnPropertyChanged();
            CreateProductCommand.NotifyCanExecuteChanged();
        }
    }

    [ObservableProperty] private int _productQuantity;

    [ObservableProperty] private int _productPrice;
    #endregion
    
    [RelayCommand(CanExecute = nameof(CanCreateProduct))]
    private async Task CreateProduct()
    {
        try
        {
            productRepository.CreateProduct(ProductName, ProductQuantity, ProductPrice);
            
            DialogResult = true;
            InvokeRequestClose();
        }
        catch (Exception e)
        {
            await dialogService.ShowMessageBoxAsync(this, e.Message, "Error");
        }
    }
    private bool CanCreateProduct() => !string.IsNullOrWhiteSpace(ProductName);
}