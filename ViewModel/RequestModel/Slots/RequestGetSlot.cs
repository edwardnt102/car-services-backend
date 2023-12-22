using Common.Pagging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.RequestModel.Slots
{
    public class RequestGetSlot : PagingRequest
    {
        public string WorkerName { get; set; }
        public string TeamName { get; set; }
        public string PlaceName { get; set; }
        public DateTime? Day { get; set; }
    }
}
