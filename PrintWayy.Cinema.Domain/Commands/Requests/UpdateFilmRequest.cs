﻿using MediatR;
using PrintWayy.Cinema.Domain.Commands.Responses;

namespace PrintWayy.Cinema.Domain.Commands.Requests
{
    public class UpdateFilmRequest:IRequest<UpdateFilmResponse>
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
