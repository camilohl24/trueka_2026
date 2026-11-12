using Trueka.Services.DTOs;

namespace Trueka.Services.Interfaces;

public interface IProfileService
{
    Task<ProfileDto?> GetAsync(string userId);
    Task<ProfileFormDto?> GetForEditAsync(string userId);
    Task UpdateAsync(string userId, ProfileFormDto dto);
}
