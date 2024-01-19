using CommunityToolkit.Mvvm.ComponentModel;
using HanumanInstitute.MvvmDialogs;

namespace ProductsInventory.View.DialogViewModels;

public class ClosableDialogViewModelBase : ObservableObject, IModalDialogViewModel, ICloseable
{
    public bool? DialogResult { get; protected set; }
    
    public event EventHandler? RequestClose;

    protected void InvokeRequestClose() => RequestClose?.Invoke(this, EventArgs.Empty);
}