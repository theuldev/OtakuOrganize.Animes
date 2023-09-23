using OtakuOrganize.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Models.InputModels
{
    public class CustomerInputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public Guid UserId { get; set; }

    }
}
