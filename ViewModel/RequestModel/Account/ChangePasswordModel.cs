using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModel.RequestModel.Account
{
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
