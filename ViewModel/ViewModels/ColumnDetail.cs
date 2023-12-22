using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.ViewModels
{
    public class ColumnDetail
    {
        public ColumnsDetail Column { get; set; }
        public List<InProgressWorker> InProgressWorkers { get; set; }
        public CarNotIncluded NotWorkingCar { get; set; }
    }
    public class InProgressWorker {
        public string Name { get; set; }
        public List<string> CarInProgress { get; set; }
        public List<string> CarBooked { get; set; }
        public List<string> CarDone { get; set; }
    }

    public class CarNotIncluded {
        public List<string> CarCode { get; set; }
    }
}
