using Imkery.Domain.Apiaries;
using Imkery.Domain.Locations;

namespace Imkery.TestShared.Apiaries;

public class ApiaryBuilder
{
    private Guid _id;
    private string _name = string.Empty;
    private decimal _latitude;
    private decimal _longitude;

    public ApiaryBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ApiaryBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ApiaryBuilder WithCoordinates(decimal latitide, decimal  longitude)
    {
        _latitude = latitide; 
        _longitude = longitude;
        return this;
    }

    public Apiary Build()
    {
        var coordinates = new Coordinates(_latitude, _longitude);
        return new Apiary(_name, coordinates, _id);
    }
}