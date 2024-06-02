using Imkery.Application.Apiaries.Commands.CreateApiary;

namespace Imkery.TestShared.Apiaries;

public static class ApiaryCommandsFactory
{
    public static CreateApiaryCommand CreateApiaryCommand(
        string name = "test_apiary",
        decimal latitude = 1,
        decimal longitude = 1
    )
    {
        return new CreateApiaryCommand(name, latitude, longitude);
    }
}