using AutoMapper;
using MovieApp.Application.Feature.Bill.Dtos;
using MovieApp.Domain.Bill.Entities;

namespace MovieApp.Application.Feature.Bill;

public class BillProfile : Profile
{
    public BillProfile()
    {
        CreateMap<Domain.Bill.Entities.Bill, BillInfo>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status));

        CreateMap<Domain.Bill.Entities.Bill, BillDetail>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Tickets,
                opt => opt.MapFrom(src => src.Tickets.Select(ticket => new BillDetail.TicketDtoInBillDetail
                    {
                        Id = ticket.Id,
                        SeatName = ticket.Seat.RowName + ticket.Seat.RowIndex,
                        Type = ticket.Seat.Type.Name,
                        Price = ticket.Seat.Type.Price
                    })
                    .OrderBy(ticketDto => ticketDto.SeatName) 
                    .ToList()
            ));
        
        CreateMap<BillStatus, BillStatusDto>();
    }
}