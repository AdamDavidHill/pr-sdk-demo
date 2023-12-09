namespace Payroc.Sdk.Errors;

public enum PayrocSdkError
{
    Unknown = 0,

    BadRequest = 400,
    NotAuthorized = 403,
    NotFound = 404,
    NotAcceptable = 406,
    PayloadTooLarge = 413,
    UnsupportedMediaType = 415,
    ApiError = 500,

    // 400 extended
    FundingAccountsRestricted = 4001,
    InsufficientFunds = 4002,
    KycCheckFailed = 4003,
    NoControlProngOrAuthorizedSignatory = 4004,
    ProcessingTerminalWasNotAccepted = 4005,
    SearchTooBroad = 4006,
    TooManyControlProngs = 4006,
    VolumeLimitHasBeenReached = 4007,

    // 409 extended
    CannotModify = 4091,
    NationalIdInUse = 4092,
    IdempotencyKeyInUse = 4093,
    ResourceAlreadyExists = 4094,
    TaxIdInUse = 4095,
}
