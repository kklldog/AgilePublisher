namespace AgilePublisher.Models;

public record PlaywrightSettings(
    bool Headless = true,
    string? UserDataDir = null,
    float SlowMo = 0,
    int? LaunchTimeout = null);
