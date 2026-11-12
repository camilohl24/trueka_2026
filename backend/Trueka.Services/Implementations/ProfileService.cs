using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Trueka.Entities;
using Trueka.Services.DTOs;
using Trueka.Services.Interfaces;

namespace Trueka.Services.Implementations;

public class ProfileService : IProfileService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public ProfileService(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ProfileDto?> GetAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user is null ? null : _mapper.Map<ProfileDto>(user);
    }

    public async Task<ProfileFormDto?> GetForEditAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user is null ? null : _mapper.Map<ProfileFormDto>(user);
    }

    public async Task UpdateAsync(string userId, ProfileFormDto dto)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new KeyNotFoundException($"User {userId} not found.");

        _mapper.Map(dto, user);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new InvalidOperationException(
                string.Join(" ", result.Errors.Select(e => e.Description)));
    }
}