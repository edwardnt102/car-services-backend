using Microsoft.AspNetCore.Http;

namespace ViewModel.RequestModel.Jobs
{
    public class CheckoutModel
    {
        public long JobId { get; set; }
        public string ChemicalUsed { get; set; }

        public string MaterialsUsed { get; set; }

        public IFormFile MainPhotoBeforeWiping { get; set; }

        public IFormFile TheSecondaryPhotoBeforeWiping { get; set; }

        public IFormFile MainPhotoAfterWiping { get; set; }

        public IFormFile SubPhotoAfterWiping { get; set; }
    }
}
