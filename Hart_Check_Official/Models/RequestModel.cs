namespace Hart_Check_Official.Models
{
    public class RequestModel
    {
        public string invoiceNumber { get; set; }
        public string type { get; set; }
        public TotalAmountModel totalAmount { get; set; }
        public List<ItemModel> items { get; set; }
        public RedirectModel redirectUrl { get; set; }
        public string requestReferenceNumber { get; set; }
        public object metadata { get; set; }
    }
}
