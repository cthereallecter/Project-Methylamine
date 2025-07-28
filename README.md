# Project Methylamine

A command-line application for managing maps, items, and content packaging with built-in encryption capabilities. Project Methylamine provides a modular framework for content creation and management through a console-based interface.

## Features

- **Interactive Command System**: Console-based command interface with logging
- **Map Management**: Create, edit, delete, and list game maps with seasonal variants
- **Content Packaging**: Pack and unpack directories with optional encryption
- **Encryption Support**: AES-256 encryption for secure content distribution
- **Extensible Architecture**: Modular design for easy feature expansion

## Getting Started

### Prerequisites

- .NET 6.0 or later
- Windows/Linux/macOS

### Installation

1. Clone the repository
2. Build the project:
   ```bash
   dotnet build
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

### Command Line Arguments

The application supports version and build information through command line arguments:

```bash
# Set version and build
ProjectMethylamine.exe -v 1.0.0 -b RELEASE
```

If no arguments are provided, the application will:
- Read version from `version.txt` if it exists
- Default to DEBUG build mode

## Core Components

### Command System

The application uses a command-based architecture where each command implements the `ICommand` interface:

- **Command Registration**: Commands are registered in the `CommandHandler` dictionary
- **Input Processing**: Commands receive logger instance and full input string
- **Help System**: Each command provides contextual help information

### Logging System

Comprehensive logging with both console and file output:

- **Console Output**: Real-time feedback with timestamps
- **File Logging**: Persistent logs stored in `Logs/latest.log`
- **Configurable Output**: Control line breaks, silent mode, and file writing

### Abstract Base Classes

- **Map**: Base class for all map implementations
- **Item**: Base class for all item implementations

## Available Commands

### Map Management (`mappr`)

Create and manage game maps with support for seasonal variants.

#### Basic Operations

```bash
# Create a new map
mapr -c <mapname> <size>

# Create seasonal map variants (base + spring/summer/autumn/winter)
mapr -c /S <mapname> <size>

# Edit existing map line by line
mapr -e <mapname>

# Delete map and remove references
mapr -d <mapname>

# List all available maps
mapr -L

# Show help
mapr -h
```

#### Map Features

- **Size-based Creation**: Square maps with customizable dimensions
- **Seasonal Variants**: Automatic creation of seasonal map versions
- **Interactive Editing**: Line-by-line map editing with validation
- **Class Integration**: Automatic C# class generation and updates
- **File Format**: `.lmf` (Level Map Format) with space-separated tile data

#### Map File Structure

```
16
. . . . . . . . . . . . . . . .
. . . . . . . . . . . . . . . .
...
```

First line contains the map size, followed by tile data where each character represents a tile type.

#### Naming Conventions

- Use dots for variants: `nexus.christmas` → stored as `nexus_christmas.lmf`
- Seasonal suffixes: `_spring`, `_summer`, `_autumn`, `_winter`
- Class names converted to PascalCase: `void_cradle_ruins` → `VoidCradleRuins`

### Content Packaging (`pakr`)

Pack and unpack directories with optional encryption support.

#### Pack Operations

```bash
# Basic packing
pakr -p /D <source_dir> <dest_dir> <archive_name>

# Pack with encryption
pakr -p /D /E <source_dir> <dest_dir> <archive_name>

# Pack multiple directories using wildcards
pakr -p /D Mods/* Data/ PackedMods

# Prevent overwrite (use -o flag)
pakr -p /D -o <source_dir> <dest_dir> <archive_name>
```

#### Unpack Operations

```bash
# Basic unpacking
pakr -u /D <archive_path> <dest_dir>

# Unpack encrypted archive
pakr -u /D /e <archive_path> <dest_dir>
```

#### Archive Features

- **Compression**: Optimal ZIP compression
- **Encryption**: AES-256 encryption with SHA-256 key derivation
- **Manifest Generation**: Automatic metadata inclusion
- **Source Cleanup**: Original directories cleared after successful packing
- **Wildcard Support**: Pack multiple directories in one operation
- **File Extensions**: `.zip` for unencrypted, `.pakr` for encrypted archives

#### Manifest Structure

Each archive includes a `manifest.pakr.json` file containing:
- Archive name and version
- Creation timestamp
- Source directory information
- Encryption status
- File listing

### Utility Commands

```bash
# Clear console
clear

# Show command help
<command> -h
```

## Project Structure

```
ProjectMethylamine/
├── Source/
│   ├── Items/           # Item system base classes
│   ├── Maps/            # Map system base classes
│   └── Utility/
│       ├── Commands/    # Command implementations
│       │   └── Testing/ # Development/testing commands
│       └── Cryptography/# Encryption utilities
├── Content/
│   └── Maps/           # Generated map files (.lmf)
├── Logs/               # Application logs
└── Program.cs          # Application entry point
```

## File Formats

### Map Files (`.lmf`)
Level Map Format files containing:
- Size declaration (first line)
- Space-separated tile data
- Square grid layout

### Archive Files
- `.zip`: Standard ZIP archives
- `.pakr`: Encrypted ZIP archives with AES-256

## Security

### Encryption Details
- **Algorithm**: AES-256 in CBC mode
- **Key Derivation**: SHA-256 hash of string key
- **IV Generation**: Cryptographically secure random IV per file
- **Padding**: PKCS7
- **Default Key**: "PAKR_Key_2025" (PLEASE CHANGE THIS!!! - For example, I will be able to unpack your content if you dont.)

## Development

### Adding New Commands

1. Create a class implementing `ICommand` interface
2. Implement `Execute()` and `ShowHelp()` methods
3. Register command in `CommandHandler.commands` dictionary (auto invoke is nice)

### Extending Base Classes

- **Maps**: Inherit from `Map` abstract class
- **Items**: Inherit from `Item` abstract class

### Error Handling

The application includes comprehensive error handling:
- File operation failures
- Invalid command syntax
- Encryption/decryption errors
- Map format validation

## Logging

All operations are logged with timestamps:
- **INFO**: General information
- **MAPR**: Map operation results
- **PAKR**: Packaging operation results
- **TERMINAL**: User input/output
- **HELP**: Help and usage information
   - These are but a few examples of custom tags for
      the logging system.

## Future Expansion

The modular architecture supports easy expansion:

### Planned Features (X + -)
- [-] Item management system (`itemz` command)
- [+] NuGet package generated for redundency
- [ ] Realtime content validation system (dedicated thread)
- [ ] Network content distribution (ehh)
- [ ] Configuration management (this sounded cool)
- [-] Give ```pakr /D``` actual functionality (lol)

### Extension Points
- Command system for new operations
- Abstract base classes for content types
- Pluggable encryption providers
- Customizable file formats
- Logger implementations

## Contributing

1. Follow existing code patterns
2. Implement `ICommand` for new commands
3. Add comprehensive logging
4. Include help documentation
5. Handle errors gracefully

## License

[License information to be added]

---

**Note**: This is a development version. Features and APIs may change in future releases.