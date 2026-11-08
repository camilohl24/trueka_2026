using AutoMapper;
using Trueka.Entities;
using Trueka.Entities.Enums;
using Trueka.Repositories.Interfaces;
using Trueka.Services.DTOs;
using Trueka.Services.Interfaces;


namespace Trueka.Services.Implementations;

public class OfferService : IOfferService
{
    private readonly IOfferRepository _offerRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public OfferService(
        IOfferRepository offerRepository,
        IItemRepository itemRepository,
        IMapper mapper)
    {
        _offerRepository = offerRepository;
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task MakeOfferAsync(string userId, OfferFormDto dto)
    {
        // Rule #1: the item must exist and be active
        var item = await _itemRepository.GetByIdAsync(dto.ItemId);
        if (item is null || !item.IsActive)
            throw new InvalidOperationException("El objeto no existe.");

        // Rule #2: you cannot offer on your own item
        if (item.OwnerId == userId)
            throw new InvalidOperationException(
                "No puedes ofertar por tu propio objeto.");

        // Rule #3: only one pending offer per user and item
        if (await _offerRepository.ExistsPendingAsync(dto.ItemId, userId))
            throw new InvalidOperationException(
                "Ya tienes una oferta pendiente en este objeto.");

        // The server decides who offers and the starting status
        var offer = new Offer
        {
            ItemId = dto.ItemId,
            OfferedById = userId,
            Message = dto.Message,
            Status = OfferStatus.Pending
        };

        await _offerRepository.AddAsync(offer);   // SaveChangesAsync happens here
    }

    public async Task<IEnumerable<OfferDto>> GetMyOffersAsync(string userId)
        => _mapper.Map<IEnumerable<OfferDto>>(await _offerRepository.GetByUserAsync(userId));
}