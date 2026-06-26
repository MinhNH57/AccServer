namespace Supply.Domain.AggregatesModel.SUIncrementAggregate;

public class SUIncrementDetail : Entity
{
    public int SortOrder { get; private set; }

    public string Description { get; private set; }

    public string NumberNo { get; private set; }

    public int State { get; private set; } = 0;

    public int? EditVersion { get; private set; }

    protected SUIncrementDetail() { }

    public SUIncrementDetail(
        int sortOrder,
        string description,
        string numberNo,
        int state,
        int? editVersion)
        : this()
    {
        //Id = Guid.NewGuid();
        SortOrder = sortOrder;
        Description = description;
        NumberNo = numberNo;
        State = state;
        EditVersion = editVersion;
    }

    public SUIncrementDetail Update(
        int sortOrder,
        string description,
        string numberNo,
        int state,
        int? editVersion)
    {
        SortOrder = sortOrder;
        Description = description;
        NumberNo = numberNo;
        State = state;
        EditVersion = editVersion;

        return this;
    }
}
