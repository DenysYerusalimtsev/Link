﻿using Link.Common.Domain.Framework.Frameworks;
using Link.EventManagement.Domain.Model.Entities;
using Link.EventManagement.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Link.EventManagement.Application.Features.AddOrUpdateEvent
{
    public sealed class AddOrUpdateEventCommandHandler
        : CommandHandler<AddOrUpdateEventCommand, AddOrUpdateEventCommand.Reply>
    {
        private readonly IEventRepository _events;

        public AddOrUpdateEventCommandHandler(
            ICommandValidator<AddOrUpdateEventCommand, AddOrUpdateEventCommand.Reply> validator,
            IEventRepository events)
            : base(validator)
        {
            _events = events;
        }

        protected override async Task<AddOrUpdateEventCommand.Reply> Handle(AddOrUpdateEventCommand command)
        {
            var ev = new Event(
                id: command.Id,
                userId: command.UserId,
                name: command.Name,
                expertType: command.ExpertType,
                status: command.Status,
                latitude:command.Latitude,
                longitude: command.Longitude,
                countOfNeededExperts: command.CountOfNeededExperts,
                experts: new List<ExpertId>()
            );

            if (ev.Id.Id == null)
            {
                Event newEvent = await _events.Create(ev);

                return new AddOrUpdateEventCommand.Reply(newEvent.Id);
            }

            Event existedEvent = await _events.Get(command.Id);
            if (existedEvent != null)
            {
                _events.Update(command.Id, ev);
            }

            return new AddOrUpdateEventCommand.Reply(command.Id);
        }
    }
}