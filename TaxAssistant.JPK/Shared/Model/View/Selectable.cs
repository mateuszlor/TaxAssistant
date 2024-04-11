namespace TaxAssistant.JPK.Shared.Model.View
{
    public class Selectable<T>
    {
        public Selectable(T value)
        {
            Value = value;
            IsSelected = false;
        }

        public bool IsSelected { get; set; }

        public T Value { get; set; }
    }
}
