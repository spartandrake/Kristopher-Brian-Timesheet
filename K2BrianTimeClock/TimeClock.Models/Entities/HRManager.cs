using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeClock.Models.Entities
{
    [Table("HRManagers", Schema = "TimeClock")]
    public class HRManager : Manager
    {
    }
}
