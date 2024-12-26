# Blackbird.io Monday

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

Monday.com is a cloud-based work operating system that can be used for a variety of needs, including project management, collaboration, and scheduling.

## Before you set up

Before setting up the integration, ensure that you have access to a Monday.com account. Our app requires an access token for authentication. You can find instructions on obtaining an access token [here](https://developer.monday.com/api-reference/docs/authentication#developer-tab).

If you are a member or admin of a Monday.com account, follow these steps to access your API token:

1. Log in to your Monday.com account.
2. Click on your profile picture in the top-right corner.
3. Select **Developers**. This will open the Developer Center in another tab.
4. Go to **My Access Tokens** and click **Show**.
5. Copy your personal token.

**Note**: If you regenerate a new token, all previous tokens will expire.

## Connecting to Monday

1. Navigate to the **Apps** section and locate the **Monday** app (you can search for it).
2. Click **Add Connection**.
3. Name your connection for future reference (e.g., 'My Organization').
4. Enter the **API Token** in the corresponding field.
5. Click **Authorize connection**.
6. Confirm that the connection is established and that the status shows **Connected**.

![Connecting](image/README/connecting.png)

## Actions

### Boards

- **Search boards**: Retrieves all boards filtered by the specified parameters.
- **Get board**: Retrieves a board by its specified ID.

### Items

- **Search items**: Retrieves all items from a specific board.
- **Get item**: Retrieves an item by its specified ID.
- **Create item**: Creates an item with the specified parameters.
- **Delete item**: Deletes an item by its specified ID.
- **Archive item**: Archives an item by its specified ID.

### Updates

- **Get update**: Retrieves an update based on the specified Item ID.
- **Add update**: Creates an update (comment) in a specific item based on its ID.
- **Add attachment to update**: Adds an attachment (file) to an update based on the specified ID.
- **Edit update**: Edits an update (comment) based on the specified ID.
- **Delete update**: Deletes an update (comment) based on the specified ID.

## Events

### Items

- **On item created**: This event is triggered when an item is created.
- **On item changed**: This event is triggered when an item is changed.
- **On item archived**: This event is triggered when an item is archived.
- **On item deleted**: This event is triggered when an item is deleted.

### Updates

- **On update created**: This event is triggered when an update is created.
- **On update edited**: This event is triggered when an update is edited.

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
