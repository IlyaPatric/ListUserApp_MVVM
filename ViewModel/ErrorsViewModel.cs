﻿using System.Collections;
using System.ComponentModel;

namespace LIstUserApp.ViewModel;

public class ErrorsViewModel : INotifyDataErrorInfo
{

    private readonly Dictionary<string, List<string>> _propertyErrors = new ();

    public bool HasErrors => _propertyErrors.Any();

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public IEnumerable GetErrors(string propertyName)
    {
        return _propertyErrors.GetValueOrDefault(propertyName, null);
    }

    public void AddError(string propertyName, string errorMessage)
    {
        if (!_propertyErrors.ContainsKey(propertyName))
        {
            _propertyErrors.Add(propertyName, new List<string>());
        }

        _propertyErrors[propertyName].Add(errorMessage);
        OnErrorChanged(propertyName);
    }

    public void ClearError(string propertyName)
    {
        if (_propertyErrors.Remove(propertyName))
            OnErrorChanged(propertyName);
    }

    private void OnErrorChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}