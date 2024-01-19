using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using ProductsInventory.Services;

namespace ProductsInventory.View.DialogViewModels;

public partial class EditProductDialogViewModel
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
            UpdateProductCommand.NotifyCanExecuteChanged();
        }
    }
    
    [ObservableProperty] private int _productQuantity;
    #endregion
    
    [RelayCommand(CanExecute = nameof(CanUpdateProduct))]
    private async Task UpdateProduct()
    {
        try
        {
            productRepository.UpdateProductQuantity(ProductName, ProductQuantity);
            
            DialogResult = true;
            InvokeRequestClose();
        }
        catch (Exception e)
        {
            await dialogService.ShowMessageBoxAsync(this, e.Message, "Error");
        }
    }
    private bool CanUpdateProduct() => !string.IsNullOrWhiteSpace(ProductName);
}