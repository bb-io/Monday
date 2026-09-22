using Apps.Monday.Actions;
using Apps.Monday.Models.Identifiers;
using Apps.Monday.Models.Requests;
using Blackbird.Applications.Sdk.Common.Files;
using Tests.Monday.Base;

namespace Tests.Monday;

[TestClass]
public class FileActionsTests : TestBase
{
    public FileActions Actions => new(InvocationContext, FileManager);
    
    [TestMethod]
    public async Task AddFileToColumnAsync_ReturnsAddedFile()
    {
        // Arrange
        var item = new ItemIdentifier
        {
            ItemId = "3237187394",
            BoardId = BoardId,
        };
        var addRequest = new AddFileToColumnRequest
        {
            File = new FileReference { Name = "test.docx" },
            ColumnId = "file_mm7evjf8"
        };

        // Act
        var result = await Actions.AddFileToColumnAsync(item, addRequest);

        // Assert
        PrintResult(result);
        Assert.IsNotNull(result);
    }
}