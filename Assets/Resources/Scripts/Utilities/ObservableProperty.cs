using System;

[Serializable]
public class ObservableProperty<T>
{
    private T value;

    public Action<T> OnValueChanged;

    public ObservableProperty(T initialValue = default)
    {
        value = initialValue;
    }

    public T Value
    {
        get => value;
        set
        {
            if (!Equals(this.value, value))
            {
                this.value = value;
                OnValueChanged?.Invoke(this.value);
            }
        }
    }

    public void Subscribe(Action<T> callback)
    {
        OnValueChanged += callback;
    }

    public void Unsubscribe(Action<T> callback)
    {
        OnValueChanged -= callback;
    }
}
