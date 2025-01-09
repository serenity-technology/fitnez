using System.Text;

namespace Share;

public class ElementClass
{
    #region Members
    private readonly List<string> _items = new();
    #endregion

    #region Operators
    public static implicit operator ElementClass(string name)
    {
        var cssClass = new ElementClass();
        cssClass.Add(name);

        return cssClass;
    }

    public static implicit operator string(ElementClass instance) => instance.Value;
    #endregion

    #region Public
    public void Add(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            if (!_items.Any(w => w.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                _items.Add(name);
            }
        }
    }

    public void Remove(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            var index = _items.FindIndex(w => w.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            if (index != -1)
                _items.RemoveAt(index);
        }
    }

    public void Replace(string oldName, string newName)
    {
        var index = _items.FindIndex(w => w.Equals(oldName, StringComparison.CurrentCultureIgnoreCase));
        if (index != -1)
            _items[index] = newName;
    }

    public void Clear()
    {
        _items.Clear();
    }
    #endregion

    #region Private
    private string Value
    {
        get
        {
            var value = new StringBuilder();
            foreach (var item in _items)
            {
                value.Append($"{item} ");
            }

            return value.ToString().Trim();
        }
    }
    #endregion
}