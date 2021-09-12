using Bogus;
using FluentAssertions;
using Xunit;

namespace Contoso.FoodDelivery.UnitTests;

public class RestaurantTests
{
    private readonly Faker _faker;

    public RestaurantTests()
    {
        _faker = new Faker();
    }


    private Restaurant CreateRestaurant(PlanType? planType = null)
    {
        planType ??= _faker.PickRandom<PlanType>();
        var address = CreateAddress();
        return new Restaurant(_faker.Name.FirstName(), planType.Value, address);
    }

    private Address CreateAddress()
    {
        return new Address(_faker.Address.StreetAddress(), _faker.Address.ZipCode(), _faker.Address.City(), _faker.Address.State());
    }

    [Fact]
    public void When_Creating_Must_Provider_Parameters()
    {
        Assert.Throws<ArgumentException>(() => new Restaurant(null!, PlanType.Basic, CreateAddress()));
        Assert.Throws<ArgumentNullException>(() => new Restaurant(_faker.Name.FirstName(), PlanType.Basic, null!));
    }

    [Fact]
    public void When_Adding_With_Invalid_Input_Should_Throw()
    {
        var sut = CreateRestaurant();
        Assert.Throws<ArgumentException>(() => sut.AddMenuItem(null!, _faker.Random.Words()));
        Assert.Throws<ArgumentException>(() => sut.AddMenuItem(_faker.Random.Word(), null!));
    }

    [Theory]
    [InlineData(PlanType.Professional)]
    [InlineData(PlanType.Unlimited)]
    public void When_Adding_Menu_Item_Should_Add_To_List(PlanType planType)
    {
        var sut = CreateRestaurant(planType);

        var actual = sut.AddMenuItem(_faker.Random.Word(), _faker.Random.Words());
        actual.Should().NotBeNull();

        sut.MenuItems.Should().HaveCount(1);
    }

    [Fact]
    public void When_Adding_Menu_Item_In_Basic_Plan_Should_Throw()
    {
        var sut = CreateRestaurant(PlanType.Basic);
        Assert.Throws<CannotAddMenuItemException>(() => sut.AddMenuItem(_faker.Random.Word(), _faker.Random.Words()));
    }

    [Fact]
    public void When_Adding_11th_Menu_Item_In_Professional_Plan_Should_Throw()
    {
        var sut = CreateRestaurant(PlanType.Professional);
        for (var i = 0; i < Restaurant.MaxMenuItemsInProfessionalPlan; i++)
        {
            sut.AddMenuItem(_faker.Name.FirstName(), _faker.Random.Words());
        }

        Assert.Throws<CannotAddMenuItemException>(() => sut.AddMenuItem(_faker.Random.Word(), _faker.Random.Words()));
    }

    [Fact]
    public void When_Adding_11th_Menu_Item_In_Unlimited_Plan_Should_Accept()
    {
        var sut = CreateRestaurant(PlanType.Unlimited);
        const int menuItemCount = Restaurant.MaxMenuItemsInProfessionalPlan + 1;
        for (var i = 0; i < menuItemCount; i++)
        {
            sut.AddMenuItem(_faker.Name.FirstName(), _faker.Random.Words());
        }

        sut.MenuItems.Should().HaveCount(menuItemCount);
    }

    [Fact]
    public void When_Setting_Feature_Menu_Item_In_Professional_Plan_Should_Throw()
    {
        var sut = CreateRestaurant(PlanType.Professional);
        var menuItem = sut.AddMenuItem(_faker.Name.FirstName(), _faker.Random.Words());
        Assert.Throws<CannotSetMenuItemFeaturedException>(() => menuItem.SetFeatured());
    }

    [Fact]
    public void When_Setting_Feature_Menu_Item_In_Unlimited_Plan_Should_Succeed()
    {
        var sut = CreateRestaurant(PlanType.Unlimited);
        var menuItem = sut.AddMenuItem(_faker.Name.FirstName(), _faker.Random.Words());
        menuItem.SetFeatured();
        menuItem.Featured.Should().BeTrue();
    }
}
