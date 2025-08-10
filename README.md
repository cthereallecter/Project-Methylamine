# Project Methylamine

A comprehensive content management and web server application for game development, providing both command-line tools and a GUI interface for managing maps, content packaging, and web services.

## 🚀 Features

### Core Functionality
- **Content Generator GUI**: Windows Forms application for easy content management
- **Web Server**: Built-in HTTP/HTTPS server with Flash/SWF support
- **Command System**: Extensible console-based interface
- **Map Management**: Create, edit, delete, and manage game maps with seasonal variants
- **Content Packaging**: Pack/unpack directories with AES-256 encryption
- **Dual Architecture**: Both server and client applications

### Advanced Capabilities
- **Encryption Support**: Secure content distribution with AES-256
- **Logging System**: Comprehensive file and console logging
- **Modular Design**: Extensible command and class system
- **Cross-Domain Support**: CORS and Flash crossdomain.xml configuration
- **NuGet Package Generation**: Automated package creation

## 🏗️ Architecture

### Project Structure
```
Project Methylamine/
├── Server/                   # Core server application
│   ├── Source/
│   │   ├── Items/            # Item system base classes
│   │   ├── Maps/             # Map system base classes
│   │   └── Utility/
│   │       ├── Commands/     # Command implementations
│   │       ├── Cryptography/ # Encryption utilities
│   │       └── Netting/      # Web server components
│   └── Web/                  # Static web files
├── Content Generator/        # Windows Forms GUI application
├── Content/
│   └── Maps/                 # Generated map files (.lmf)
├── Logs/                     # Application logs
└── Binaries/                 # Build output
```

### Applications

#### 1. **Server Application** (`Server.exe`)
- **Web Server**: Hosts Flash/SWF games with crossdomain support
- **Command Interface**: Terminal-based content management
- **API Endpoints**: RESTful services for content operations
- **Target Framework**: .NET 8.0 Windows

#### 2. **Content Generator** (`Content Generator.exe`)
- **GUI Interface**: Windows Forms application for content management
- **P.A.K.R. System**: Pack/unpack operations with encryption
- **M.A.P.P.R. System**: Map creation and management
- **Visual Feedback**: Real-time logging and status updates

## 🛠️ Getting Started

### Prerequisites
- **.NET 8.0 Runtime** (Windows)
- **Windows** (for GUI application)
- **Administrator privileges** (for HTTPS binding)

### Installation

#### Option 1: Binary Release
1. Download the latest release from GitHub
2. Extract to desired location
3. Run `Server.exe` or `Content Generator.exe`

#### Option 2: Build from Source
```bash
# Clone repository
git clone https://github.com/cthereallecter/Project-Methylamine.git
cd Project-Methylamine

# Build solution
dotnet build --configuration Release

# Run server
cd Binaries/Release/net8.0-windows
./Server.exe

# Or run content generator
./Content\ Generator.exe
```

### Command Line Usage
```bash
# Set version and build information
Server.exe -v 1.0.0 -b RELEASE

# If no arguments provided:
# - Reads version from version.txt
# - Defaults to DEBUG build
```

## 📋 Available Commands

### Map Management (`mapr`)

Create and manage game maps with seasonal variant support.

```bash
# Create basic map
mapr -c <mapname> <size>

# Create seasonal variants (base + spring/summer/autumn/winter)
mapr -c /S <mapname> <size>

# Edit existing map interactively
mapr -e <mapname>

# Delete map and references
mapr -d <mapname>

# List all maps
mapr -L

# Show help
mapr -h
```

**Map Features:**
- Square grid maps with customizable dimensions
- Seasonal variants with automatic suffix handling
- Interactive line-by-line editing
- Automatic C# class generation and updates
- `.lmf` (Level Map Format) file support

**Naming Conventions:**
- Use dots for variants: `nexus.christmas` → `nexus_christmas.lmf`
- Seasonal suffixes: `_spring`, `_summer`, `_autumn`, `_winter`
- Class names: `void_cradle_ruins` → `VoidCradleRuins`

### Content Packaging (`pakr`)

Pack and unpack directories with optional AES-256 encryption.

```bash
# Pack directory
pakr -p /D <source_dir> <dest_dir> <archive_name>

# Pack with encryption
pakr -p /D /E <source_dir> <dest_dir> <archive_name>

# Unpack archive
pakr -u /D <archive_path> <dest_dir>

# Unpack encrypted archive
pakr -u /D /e <archive_path> <dest_dir>

# Pack multiple directories with wildcards
pakr -p /D Mods/* Data/ PackedMods

# Prevent overwrite
pakr -p /D -o <source_dir> <dest_dir> <archive_name>
```

**Archive Features:**
- ZIP compression with optimal settings
- AES-256 encryption with SHA-256 key derivation
- Automatic manifest generation with metadata
- Source directory cleanup after packing
- File extensions: `.zip` (unencrypted), `.pakr` (encrypted)

### Web Server Features

#### Static File Serving
- **Default Document**: `index.html`
- **MIME Type Detection**: Automatic content type headers
- **404/500 Handling**: Proper error responses

#### API Endpoints
- **GET /status**: Server status check
- **GET /crossdomain.xml**: Flash crossdomain policy
- **POST /api/test**: Test endpoint with JSON response

#### CORS Support
- **All Origins**: `Access-Control-Allow-Origin: *`
- **All Methods**: GET, POST, PUT, DELETE, OPTIONS
- **All Headers**: Complete header support

## 🎮 Game Integration

### Flash/SWF Support
The server includes built-in support for hosting Flash games:

```html
<!-- Automatic embed generation -->
<embed src="Methylamine-Client.swf" 
       width="800" 
       height="600" 
       type="application/x-shockwave-flash">
```

### Cross-Domain Configuration
Automatic crossdomain.xml generation for Flash security:

```xml
<?xml version="1.0"?>
<cross-domain-policy>
    <allow-access-from domain="*" secure="false"/>
</cross-domain-policy>
```

## 🔐 Security Features

### Encryption Implementation
- **Algorithm**: AES-256 in CBC mode
- **Key Derivation**: SHA-256 hash of string key
- **IV Generation**: Cryptographically secure random IV per operation
- **Padding**: PKCS7 standard
- **Default Key**: `PAKR_Key_2025` (CHANGE THIS FOR PRODUCTION!)

### Security Best Practices
```csharp
// Custom encryption key
var customKey = "YourSecureKey2025";
var encrypted = BitShiftCrypto.Encrypt(data, customKey);
```

## 🗂️ File Formats

### Map Files (`.lmf`)
Level Map Format with space-separated tile data:
```
16                                  # Map size (16x16)
0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0    # Tile row 0
0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0    # Tile row 1
...                                 # Additional rows
```

### Archive Manifest
Each `.pakr` archive includes `manifest.pakr.json`:
```json
{
  "name": "archive_name",
  "packedAt": "2025-01-15T10:30:00.000Z",
  "source": "Content/",
  "version": "1.0.0",
  "encrypted": true,
  "files": ["file1.txt", "file2.png"]
}
```

## 🔧 Development

### Adding New Commands
1. Implement `ICommand` interface:
```csharp
internal class MyCommand : ICommand
{
    public void Execute(ConsoleLogger logger, string input) { }
    public void ShowHelp(ConsoleLogger logger) { }
}
```

2. Register in `CommandHandler`:
```csharp
["mycommand"] = new MyCommand()
```

### Extending Base Classes
- **Maps**: Inherit from `Map` abstract class
- **Items**: Inherit from `Item` abstract class
- **Commands**: Implement `ICommand` interface

### Logging System
```csharp
logger.Log("INFO", "Message");
logger.Log("CUSTOM_TAG", "Custom message");
logger.Log("ERROR", exception, "Error occurred");
```

## 📊 Monitoring & Logging

### Log Categories
- **INFO**: General information
- **MAPR**: Map operations
- **PAKR**: Packaging operations  
- **TERMINAL**: User input/output
- **HELP**: Usage information
- **ERROR**: Error conditions

### Log Files
- **Location**: `Logs/latest.log`
- **Format**: `[HH:mm:ss] [LEVEL] Message`
- **Rotation**: Manual (append mode)

## 🌐 Network Configuration

### Default Ports
- **HTTPS**: 443 (thehideout.cthereallecter.com)
- **HTTP**: 8080 (localhost)

### Domain Setup
Configure your domain in `WebServer` constructor:
```csharp
var webServer = new WebServer(
    domain: "yourdomain.com",
    httpsPort: 443,
    httpPort: 8080,
    staticFileRoot: "Web"
);
```

## 🚧 Roadmap

### Planned Features (v0.0.2+)
- [ ] **Item Management System** (`itemz` command)
- [ ] **Real-time Content Validation** (dedicated thread)
- [ ] **Network Content Distribution** 
- [ ] **Configuration Management System**
- [ ] **Enhanced pakr /D functionality**
- [ ] **HTTPS Certificate Management**
- [ ] **Database Integration**
- [ ] **Plugin Architecture**

### Extension Points
- Command system for new operations
- Abstract base classes for content types
- Pluggable encryption providers
- Customizable file formats
- Logger implementations

## 📄 License

[License to be specified]

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Follow existing code patterns
4. Implement comprehensive logging
5. Add help documentation
6. Handle errors gracefully
7. Submit a pull request

## ⚠️ Important Notes

- **Default Encryption Key**: Change `PAKR_Key_2025` for production use
- **Administrator Privileges**: Required for HTTPS port binding
- **Flash Support**: Ensure Flash player/Ruffle is available for SWF content
- **Development Version**: Features and APIs may change in future releases

## 🆘 Support

- **Documentation**: Check inline code documentation
- **Issues**: Report bugs via GitHub Issues
- **Discussions**: Use GitHub Discussions for questions

---

**Project Methylamine** - A comprehensive content management and web server solution for game development.

*Version 0.0.2 - Built with .NET 8.0*