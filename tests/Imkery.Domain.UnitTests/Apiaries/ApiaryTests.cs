using FluentAssertions;
using Imkery.Domain.Apiaries;
using Imkery.Domain.Hives;
using Imkery.Domain.Locations;
using Xunit;

namespace Imkery.Domain.UnitTests.Apiaries;

public class ApiaryTests
{
    [Fact]    
    public void AddHive_ShouldThrowError_WhenDuplicateIsAdded()
    {
        var coordinates = new Coordinates(1,1);
        var apiary = new Apiary("", coordinates);
        
        var hive = new Hive(new Guid());

        apiary.AddHive(hive);
        apiary.Invoking(apiary => apiary.AddHive(hive))
            .Should().Throw<ArgumentException>();
    }
}