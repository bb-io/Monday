using Apps.Monday.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using FluentAssertions;
using Newtonsoft.Json;
using Tests.Monday.Base;

namespace Tests.Monday;

[TestClass]
public class UpdateActionsTests : TestBase
{
    [TestMethod]
    public async Task GetUpdate_ExistingUpdate_ShouldReturnAnUpdate()
    {
        var updateActions = new UpdateActions(InvocationContext, FileManager);
        var update = await updateActions.GetUpdateAsync(new()
        {
            ItemId = ItemId,
            UpdateId = UpdateId
        });

        update.Should().NotBeNull();
        update.Id.Should().NotBeNullOrEmpty();

        Console.WriteLine(JsonConvert.SerializeObject(update, Formatting.Indented));
    }
    
    [TestMethod]
    public async Task GetUpdate_InvalidId_ShouldThrowException()
    {
        var updateActions = new UpdateActions(InvocationContext, FileManager);

        await Assert.ThrowsExceptionAsync<PluginApplicationException>(async () =>
        {
            await updateActions.GetUpdateAsync(new()
            {
                ItemId = ItemId,
                UpdateId = "111"
            });
        });
    }
    
    [TestMethod]
    public async Task CreateAndDeleteUpdate_ValidData_ShouldNotThrowException()
    {
        var updateActions = new UpdateActions(InvocationContext, FileManager);

        var createdUpdate = await updateActions.AddUpdateAsync(new()
        {
            ItemId = ItemId,
            Body = $"Tests.Monday {DateTime.Now:G}"
        });
        
        
        createdUpdate.Should().NotBeNull();
        createdUpdate.Id.Should().NotBeNullOrEmpty();

        Console.WriteLine(JsonConvert.SerializeObject(createdUpdate, Formatting.Indented));

        await updateActions.DeleteUpdateAsync(new()
        {
            ItemId = ItemId,
            UpdateId = createdUpdate.Id
        });
        
        Console.WriteLine("\nSuccessfully deleted newly created update");
    }
    
        
    [TestMethod]
    public async Task EditUpdate_ValidData_ShouldNotThrowException()
    {
        var updateActions = new UpdateActions(InvocationContext, FileManager);

        var createdUpdate = await updateActions.EditUpdateAsync(new()
        {
            ItemId = ItemId,
            UpdateId = UpdateId,
            Body = $"Tests.Monday updated {DateTime.Now:G}"
        });
        
        
        createdUpdate.Should().NotBeNull();
        createdUpdate.Id.Should().NotBeNullOrEmpty();

        Console.WriteLine(JsonConvert.SerializeObject(createdUpdate, Formatting.Indented));
    }
    
    [TestMethod]
    public async Task AddAttachmentToUpdate_ValidData_ShouldNotThrowException()
    {
        var updateActions = new UpdateActions(InvocationContext, FileManager);

        var update = await updateActions.AddAttachmentAsync(new()
        {
            ItemId = ItemId,
            UpdateId = UpdateId,
            File = new()
            {
                Name = "test.docx",
                ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            }
        });
        
        
        update.Should().NotBeNull();
        update.Id.Should().NotBeNullOrEmpty();
        update.Assets.Should().NotBeEmpty();

        Console.WriteLine(JsonConvert.SerializeObject(update, Formatting.Indented));
    }
}