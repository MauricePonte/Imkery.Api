using Imkery.Application.Apiaries.Commands.CreateApiary;

namespace Imkery.TestShared.Apiaries.Commands;

public static class ApiaryCommandsFactory
{
    public static CreateApiaryCommand CreateApiaryCommand(
        string name = "",
        decimal latitude = 1,
        decimal longitude = 1
    )
    {
        return new CreateApiaryCommand(name, latitude, longitude);
    }
}