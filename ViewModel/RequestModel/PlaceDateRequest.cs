using System;

namespace ViewModel.RequestModel
{
    public class PlaceDateRequest
    {
        public DateTime? Day { get; set; }
        public long? PlaceID { get; set; }
    }
    public class PlaceDateTeamRequest : PlaceDateRequest
    {
        public long? TeamID { get; set; }
    }
}
