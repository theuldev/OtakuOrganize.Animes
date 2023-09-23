using OtakuOrganize.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuOrganize.Application.Models.InputModels
{
    public class AnimeInputModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Category { get; set; }
        public DateTime PostAt { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
