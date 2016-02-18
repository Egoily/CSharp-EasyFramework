namespace com.etak.core.model.dto
{
    /// <summary>
    /// DTO object corresponding to the Promotion
    /// </summary>
    public class PromotionDTO
    {
        /// <summary>
        /// Name of the Promotion
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Current Limit of the Promotion
        /// </summary>
        public decimal CurrentLimit { get; set; }
    }
}
