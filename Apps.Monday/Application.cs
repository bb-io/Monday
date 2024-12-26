﻿using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.Monday;

public class Application : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [
            ApplicationCategory.ProjectManagementAndProductivity
        ];
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}