# Set the build directory
BUILD_DIR="dist"
mkdir -p "$BUILD_DIR"

# Define target platforms
PLATFORMS=(
    "win-x64"
    "win-arm64"
    "linux-x64"
    "linux-arm64"
    "osx-arm64"
)

# Build for each platform
for PLATFORM in "${PLATFORMS[@]}"; do
    echo "Building for $PLATFORM..."
    dotnet publish -c Release -r $PLATFORM -v m --nologo -o "$BUILD_DIR/$PLATFORM"
    if [ $? -ne 0 ]; then
        echo "Error building for $PLATFORM!"
        exit 1
    fi
    echo "Done: $BUILD_DIR/$PLATFORM"
done

echo "All builds completed!"