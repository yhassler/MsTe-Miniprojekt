using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{

    [DataContract]
    public class OptimisticConcurrencyFault
    {
        [DataMember]
        public string Issue { get; set; }

        [DataMember]
        public string Details { get; set; }

    }
}