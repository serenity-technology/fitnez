﻿namespace Share;

public partial class Password
{
    #region Members
    [CascadingParameter(Name = "form")] private Form InputForm { get; set; } = default!;
    #endregion

    #region Public
    [Parameter] public int MaxLength { get; set; } = 50;
    [Parameter] public string Value { get; set; } = "";
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    #endregion

    #region Private        
    private void OnValueChange(ChangeEventArgs e)
    {
        Value = e.Value?.ToString() ?? "";
        _ = ValueChanged.InvokeAsync(Value);
    }
    #endregion

    #region Protected
    protected override void OnInitialized()
    {
        InputForm?.AddInput(this);
    }
    #endregion
}