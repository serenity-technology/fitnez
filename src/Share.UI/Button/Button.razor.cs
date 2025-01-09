namespace Share;

public partial class Button
{
    #region Public
    [Parameter] public string Label { get; set; } = default!;
    [Parameter] public ButtonMotif Motif { get; set; } = ButtonMotif.Primary;
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    #endregion

    #region Private
    private string Style()
    {
        var style = new ElementClass();
        style.Add(Class);
        style.Add("px-5 py-2.5 m-2");
        style.Add("rounded-lg");
        style.Add("text-sm font-medium");

        if (Disabled)
        {
            style.Add("text-gray-600");
            style.Add("border border-gray-600");
        }
        else
        {
            switch (Motif)
            {
                case ButtonMotif.Primary:
                    style.Add("text-white");
                    style.Add("bg-orange-400 hover:bg-orange-500");
                    style.Add("focus:ring-4 focus:outline-none");
                    style.Add("focus:ring-orange-500");
                    break;
                case ButtonMotif.Secondary:
                    style.Add("text-white hover:text-orange-400");
                    style.Add("bg-transparent");
                    style.Add("border border-orange-400 hover:border-orange-500");
                    style.Add("focus:ring-4 focus:outline-none");
                    style.Add("focus:ring-orange-500");
                    break;
                case ButtonMotif.Warning:
                    style.Add("text-white");
                    style.Add("bg-red-700 hover:bg-red-800");
                    style.Add("border border-red-700 hover:border-red-800");
                    style.Add("focus:ring-4 focus:outline-none");
                    style.Add("focus:ring-red-800");
                    break;
            }
        }

        return style;
    }
    #endregion
}