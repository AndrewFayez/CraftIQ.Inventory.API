var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.CraftIQ_Inventory_Aspire_ApiService>("apiservice");

builder.AddProject<Projects.CraftIQ_Inventory_Aspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
