//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMS_Library.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Campaign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Campaign()
        {
            this.Coupons = new HashSet<Coupon>();
        }
    
        public int ID { get; set; }
        public string CampaignName { get; set; }
        public string CampaignDescription { get; set; }
        public string CampaignContent { get; set; }
        public string Webkey { get; set; }
        public string ProductTypeApply { get; set; }
        public string ProductApply { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Coupon> Coupons { get; set; }
    }
}