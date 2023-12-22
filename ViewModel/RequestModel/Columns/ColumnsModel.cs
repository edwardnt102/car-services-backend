using System;

namespace ViewModel.RequestModel.Columns
{
    public class ColumnsModel
    {
        public long Id { get; set; }
        public string History { get; set; }
        public string Chat { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public long? BasementId { get; set; }
        public string AttachmentFileOriginalName { get; set; }
        public string AttachmentFileReName { get; set; }
    }
}
