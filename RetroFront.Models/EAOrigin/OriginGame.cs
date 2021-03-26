using System;
using System.Text;

namespace RetroFront.Models.EAOrigin
{
    public class OriginGame
    {
        public string itemId { get; set; }
        public string storeGroupId { get; set; }
        public string financeId { get; set; }
        public string defaultLocale { get; set; }
        public Countries countries { get; set; }
        public BaseAttributes baseAttributes { get; set; }
        public CustomAttributes customAttributes { get; set; }
        public LocalizableAttributes localizableAttributes { get; set; }
        public Publishing publishing { get; set; }
        public MdmHierarchies mdmHierarchies { get; set; }
        public EcommerceAttributes ecommerceAttributes { get; set; }
        public DateTime updatedDate { get; set; }
        public FirstParties firstParties { get; set; }
        public string itemName { get; set; }
        public string offerType { get; set; }
        public string offerId { get; set; }
        public string projectNumber { get; set; }
    }
}
