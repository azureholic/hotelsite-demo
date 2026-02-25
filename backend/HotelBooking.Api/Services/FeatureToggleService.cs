using HotelBooking.Api.Models;

namespace HotelBooking.Api.Services;

public class FeatureToggleService
{
    private readonly FeatureConfig _config = new();

    public FeatureConfig GetConfig() => _config;

    public void SetChatEnabled(bool enabled) => _config.ChatEnabled = enabled;
}
