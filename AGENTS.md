# AGENTS.md

You are working on a WSL environment. Use `dotnet.exe` over `dotnet` for all commands.

## Development

- AVOID running project. Do build & format, user will run it manually.
```bash
dotnet build --disable-build-servers --verbosity minimal <SOLUTION>.sln # build+lint
dotnet format <SOLUTION>.sln # fmt
```

## Locale

- All website content must be in Spanish (WebLibCs.Web/)
