namespace Imkery.Application.IntegrationTests.Common;

[CollectionDefinition(CollectionName)]
public class MediatorFactoryCollection : ICollectionFixture<MediatorFactory>
{
    public const string CollectionName = "MediatorFactoryCollection";
}