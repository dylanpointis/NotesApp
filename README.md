# Notes App

A note-taking application developed with .NET Web API, Entity Framework Core, React, and SQL Server.

### Homepage
![UIScreenshot](https://github.com/user-attachments/assets/40afdb46-0a43-4a71-bbab-98bb75718b6a)
<br><br>
### Edit Note page
![UIEditScreenshot](https://github.com/user-attachments/assets/27041246-b946-490e-b35c-f4342dd183b9)


# Technologies Used

### Backend
- **.NET SDK**: `9.0.106`
- **ORM**: Entity Framework Core
  - `Microsoft.EntityFrameworkCore.SqlServer`: `9.0.5`
  - `Microsoft.EntityFrameworkCore.Tools`: `9.0.5`
- **Database**: SQL Server 2022
  - Version: `16.0.1135.2`
- Language: `C#`
- Framework: `ASP.NET Core Web API`


### Frontend
- **Node.js**: `v22.13.1`
- **npm**: `11.1.0`
- Main Libraries:
  - React: `^19.1.0`
  - React Router: `^7.6.2`
  - Bootstrap: `^5.3.6`
  - Bootstrap Icons: `^1.13.1`

## Prerequisites

Make sure to have the following installed:
- [.NET SDK 9.0.106](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js v22.13.1 and npm 11.1.0](https://nodejs.org/)
- [SQL Server 2022](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## The run.sh script is set up to automatically start both the backend (http://localhost:5072) and the frontend (http://localhost:3000). CTRL+C to stop it.

Use bash:
```bash
chmod +x run.sh
./run.sh
```
