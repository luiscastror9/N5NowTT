using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace N5NowTT.Domain.DTO
{
    public class OperationDTO
    {
        public static readonly string get = "get";
        public static readonly string modify = "modify";
        public static readonly string request = "request";

        public OperationDTO() {
        
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Operation { get; set; }
        
    }
}
