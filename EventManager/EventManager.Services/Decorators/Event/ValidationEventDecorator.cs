﻿using EventManager.Common.Constants;
using EventManager.Services.Models.Event;
using EventManager.Services.Services.Interfaces;

namespace EventManager.Services.Decorators.Event
{
    public class ValidationEventDecorator : IEventService
    {
        private readonly IEventService _parent;


        public ValidationEventDecorator(IEventService parent)
        {
            _parent = parent;
        }

        public Task CreateEventAsync(CreateEventServiceModel newEvent)
        {
            ValidateCreateEventModel(newEvent);

            return _parent.CreateEventAsync(newEvent);
        }

        public void ValidateCreateEventModel(CreateEventServiceModel newEvent)
        {
            if (newEvent.Name.Length < 3 || newEvent.Address.Length < 3)
            {
                throw new ArgumentException(ExceptionConstants.InvalidEventDataInput);
            }
        }

    }
}
