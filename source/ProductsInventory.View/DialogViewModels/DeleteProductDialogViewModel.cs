using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using ProductsInventory.Exceptions;
using ProductsInventory.Services;

namespace ProductsInventory.View.DialogViewModels;

public partial class DeleteProductDialogViewModel
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
            DeleteProductCommand.NotifyCanExecuteChanged();
        }
    }
    #endregion
    
    [RelayCommand(CanExecute = nameof(CanDeleteProduct))]
    private async Task DeleteProduct()
    {
        try
        {
            productRepository.DeleteProduct(ProductName);
            
            DialogResult = true;
            InvokeRequestClose();
        }
        catch (Exception e)
        {
            await dialogService.ShowMessageBoxAsync(this, e.Message, "Error");
        }
    }
    private bool CanDeleteProduct() => !string.IsNullOrWhiteSpace(ProductName);
}