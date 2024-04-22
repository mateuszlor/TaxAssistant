namespace TaxAssistant.JPK.Shared.Adapter
{
    public interface IJpkAdapter<TJpkType, TDatabaseModel>
    {
        TDatabaseModel Adapt(TJpkType item);
    }
}
