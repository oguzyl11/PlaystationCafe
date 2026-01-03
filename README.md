# GameCenterAI - Playstation Cafe Simulator

A Windows Forms application built with .NET Framework 4.8, DevExpress WinForms, and SQL Server, following a strict 5-layer architecture pattern.

## Project Structure

The solution consists of 5 projects organized in a layered architecture:

```
GameCenterAI/
├── GameCenterAI.Entity/          # Entity Layer - POCO classes
├── GameCenterAI.DataAccess/       # Data Access Layer - Database connections
├── GameCenterAI.Interface/        # Interface Layer - Service contracts
├── GameCenterAI.Service/          # Service Layer - Business logic & AI
└── GameCenterAI.WinForms/         # UI Layer - DevExpress WinForms
```

## Architecture Layers

### 1. GameCenterAI.Entity
Contains Plain Old CLR Objects (POCO) representing database tables:
- `Uyeler` - Members/Users
- `Masalar` - Tables/Desks
- `Oyunlar` - Games
- `Turnuvalar` - Tournaments
- `Hareketler` - Transactions/Movements

### 2. GameCenterAI.DataAccess
Handles database connectivity using ADO.NET:
- `Tools` - Singleton pattern for SqlConnection management

### 3. GameCenterAI.Interface
Defines service contracts:
- `IUyeler` - Member service interface
- `ITurnuva` - Tournament service interface
- `IAiService` - AI service interface

### 4. GameCenterAI.Service
Implements business logic and AI services:
- `SUyeler` - Member operations (Login, Register)
- `STurnuva` - Tournament bracket generation
- `SAiService` - Face recognition and game recommendations (placeholders)

### 5. GameCenterAI.WinForms
User interface layer using DevExpress controls:
- `FrmGiris` - Login form
- `FrmAnaMenu` - Main menu (RibbonForm)
- `FrmMasalar` - Tables management (TileControl)

## Coding Standards

### Naming Conventions
- **Service classes**: Must start with 'S' (e.g., `SUyeler`, `STurnuva`)
- **Interface classes**: Must start with 'I' (e.g., `IUyeler`, `ITurnuva`)
- **Form classes**: Must start with 'Frm' (e.g., `FrmGiris`, `FrmAnaMenu`)
- **Private variables**: Must start with underscore (e.g., `_connection`, `_uyeID`)
- **Methods**: PascalCase (e.g., `GirisYap`, `Login`)
- **Parameters**: camelCase (e.g., `kullaniciAdi`, `sifre`)

### Architecture Rules
- **NO SQL in Forms**: Forms must NEVER contain SQL queries. All database operations must go through Service layer.
- **Global Variables**: Do not use global error variables in Services.
- **Comments**: Every class and method must have XML summary comments (`///`).
- **Form Specs**: All forms must have `AutoScroll = true`.

## Database Schema

The application expects the following SQL Server tables:

### Uyeler (Members)
- `UyeID` (int, PK)
- `AdSoyad` (nvarchar)
- `KullaniciAdi` (nvarchar)
- `Sifre` (nvarchar)
- `FaceEncoding` (varbinary(max), nullable)
- `Bakiye` (decimal)
- `Durum` (bit)

### Masalar (Tables)
- `MasaID` (int, PK)
- `MasaAdi` (nvarchar)
- `SaatlikUcret` (decimal)
- `Durum` (bit)

### Masalar (Tables)
- `MasaID` (int, PK)
- `MasaAdi` (nvarchar)
- `SaatlikUcret` (decimal)
- `Durum` (bit)

### Oyunlar (Games)
- `OyunID` (int, PK)
- `OyunAdi` (nvarchar)
- `Kategori` (nvarchar)
- `Platform` (nvarchar)

### Turnuvalar (Tournaments)
- `TurnuvaID` (int, PK)
- `TurnuvaAdi` (nvarchar)
- `BaslangicTarihi` (datetime)
- `Odul` (decimal)

### Hareketler (Transactions)
- `HareketID` (int, PK)
- `UyeID` (int, FK)
- `MasaID` (int, FK)
- `Baslangic` (datetime)
- `Bitis` (datetime, nullable)
- `Ucret` (decimal)

## Prerequisites

- Visual Studio 2022
- .NET Framework 4.8
- SQL Server (LocalDB or full instance)
- DevExpress WinForms Components v23.1

## Configuration

1. Update the connection string in `GameCenterAI.WinForms/App.config`:
```xml
<connectionStrings>
    <add name="GameCenterAIConnection" 
         connectionString="Server=localhost;Database=GameCenterAI;Integrated Security=true;" />
</connectionStrings>
```

2. Ensure DevExpress references are properly configured in the WinForms project.

## Features

### Implemented
- ✅ User authentication (Login)
- ✅ User registration
- ✅ Main menu with Ribbon interface
- ✅ Tables/Desks visualization using TileControl
  - Green tiles = Available (Durum = true)
  - Red tiles = Occupied (Durum = false)
- ✅ Tournament bracket generation (placeholder)
- ✅ Face recognition service (placeholder - ready for EmguCV integration)
- ✅ Game recommendation service (placeholder - ready for ML.NET integration)

### To Be Implemented
- Full tournament management UI
- Member management UI
- Game management UI
- Transaction management
- Face recognition with EmguCV
- ML.NET game recommendations

## Usage

1. Build the solution in Visual Studio 2022
2. Ensure SQL Server database is set up with the required tables
3. Update connection string in App.config
4. Run `GameCenterAI.WinForms` project
5. Login with valid credentials or register a new user

## Notes

- Face recognition and game recommendation services are implemented as placeholders with basic logic. They are ready for integration with EmguCV and ML.NET respectively.
- All database operations use ADO.NET directly (no Entity Framework).
- The application follows strict separation of concerns with no SQL queries in the UI layer.

## License

Copyright © 2024


