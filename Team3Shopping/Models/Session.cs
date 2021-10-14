using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team3Shopping.Models
{
    public class Session
    {
        public Session()
        {
            Id = new Guid();
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
        public Guid Id { get; set; }
        [Required]
        public long Timestamp { get; set; }

        public virtual string UserId { get; set; }

        public virtual User User { get; set; }
    }
}

