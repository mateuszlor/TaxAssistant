namespace TaxAssistant.JPK.Shared.Adapter
{
    public interface IJpkAdapter<in TJpkType, out TDatabaseModel>
    {
        TDatabaseModel? Adapt(TJpkType item);
    }
}
