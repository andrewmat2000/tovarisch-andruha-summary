NAME=$1

if [[ -z $NAME ]]; then
  echo "Enter migration name."
fi

dotnet ef migrations add ${NAME} --project Identity.Infrastructure.csproj -s ../Identity.Web/ -c AppDbContext
dotnet ef migrations add ${NAME} --project Identity.Infrastructure.csproj -s ../Identity.Web/ -c ConfigurationDbContext
dotnet ef migrations add ${NAME} --project Identity.Infrastructure.csproj -s ../Identity.Web/ -c PersistedGrantDbContext