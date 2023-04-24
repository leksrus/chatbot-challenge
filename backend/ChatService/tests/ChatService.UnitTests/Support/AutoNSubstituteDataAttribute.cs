using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace ChatService.UnitTests.Support
{
    /// <summary>
    /// Attribute to generate mocked inputs with NSubstitute
    /// </summary>
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }

        public AutoNSubstituteDataAttribute(bool configureMembers)
            : base(() => new Fixture().Customize(new AutoNSubstituteCustomization() { ConfigureMembers = configureMembers }))
        {
        }

    }
}
