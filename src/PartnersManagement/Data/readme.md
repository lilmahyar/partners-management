dotnet ef migrations add initial --context PartnerManagementDbContext  -o "Data\Migrations"
dotnet ef database update --context PartnerManagementDbContext
