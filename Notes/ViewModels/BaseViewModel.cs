using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notes.ViewModels
{
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public abstract class BaseViewModel<_Input> : BaseViewModel, IQueryAttributable, INotifyPropertyChanged
    {
        public _Input Input { get; set; }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query == null || !query.ContainsKey("parameters"))
            {
                InputChangedCommand?.Execute(this);
                return;
            }
            Input = (_Input)query["parameters"];
            InputChangedCommand?.Execute(this);
        }
        
        public ICommand InputChangedCommand { get; set; }
    }
    public abstract class BaseViewModel : NotifyPropertyChanged, INotifyPropertyChanged
    {
        public BaseViewModel()
        {

        }

        #region IsBusy

        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; RaisePropertyChanged(nameof(IsBusy)); }
        }

        #endregion
        #region IsRefreshing

        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; RaisePropertyChanged(nameof(IsRefreshing)); }
        }

        #endregion

        //public async Task SetBusyAsync(Func<Task> func, Func<Exception, Task> onError = null, Func<Task> always = null)
        //{
        //    IsBusy = true;
        //    try
        //    {
        //        await func();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (onError != null)
        //            await onError(ex);
        //    }
        //    finally
        //    {
        //        if (always != null)
        //            await always();
        //        IsBusy = false;
        //    }
        //}
        //public async Task SetBusySafeAsync(Func<Task> func, Func<Exception, Task> onError = null, Func<Task> always = null)
        //{
        //    bool canExecute = !isBusy;
        //    IsBusy = true;
        //    try
        //    {
        //        if (!canExecute)
        //            throw new Exception("Poczekaj na zakończenie wszystkich zadań");

        //        if (func != null)
        //            await func();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (onError != null)
        //            await onError(ex);
        //    }
        //    finally
        //    {
        //        if (always != null)
        //            await always();
        //        IsBusy = false;
        //    }
        //}


    }
}
