namespace com.etak.core.model.operation.messages
{
    public abstract class ModifyOrderRequest : RequestBase
    {
        public virtual Order OrderToModify { get; set; }
    }
}
