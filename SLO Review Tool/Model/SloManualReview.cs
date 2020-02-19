using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloReviewTool.Model
{
    class SloManualReview
    {
        public SloManualReview(SloRecord record)
        {
            ServiceId = Guid.Parse(record.ServiceId);
            ReviewPassed = record.ReviewPassed;
            ReviewDetails = record.ReviewDetails;

            // Update Review Date to be the current date
            ReviewDate = DateTime.Now;

            // Update Reviewed By to be the current user
            ReviewedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        public Guid ServiceId { get; set; }
        public bool ReviewPassed{ get; set; }
        public string ReviewDetails { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewedBy { get; set; }
    }
}
