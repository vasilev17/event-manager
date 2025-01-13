using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Services.Models.Picture
{
    public class EventPictureServiceModel
    {
        public Guid EventId { get; set; }

        public UploadPictureServiceModel Picture { get; set; }
    }
}
